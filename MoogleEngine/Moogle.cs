namespace MoogleEngine;

public static class Moogle
{
    public static int cantidadDeDocumentos = 0;
    public static Documento[] documentos = new Documento[] { };
    public static Dictionary<string, float> IDF = new Dictionary<string, float>();
    static Moogle()
    {
        Console.WriteLine("el moogle esta cargando");
        ConstruirDocumentos();
        LlenarIDF();
        CrearTF_IDF();
        Console.WriteLine("el moogle ya cargo");
        
    }
    static void ConstruirDocumentos()
    {
        Leer.ObtenerArchivos();
        Leer.ObtenerTextos();

        cantidadDeDocumentos = Leer.DocumentosaLeer.Length;

        documentos = new Documento[cantidadDeDocumentos];

        for (int i = 0; i < cantidadDeDocumentos; i++)
        {
            string titulo = Path.GetFileName(Leer.DocumentosaLeer[i]);
            documentos[i] = new Documento(titulo, Leer.textos[i]);
            documentos[i].LlenarPalabras();
        }
    }
    public static void LlenarIDF()
    {
        for (int i = 0; i < cantidadDeDocumentos; i++)
        {
            foreach (var A in documentos[i].palabras)
            {
                if (IDF.ContainsKey(A.Key))
                {
                    IDF[A.Key]++;
                }
                else
                {
                    IDF.Add(A.Key, 1);
                }
            }
        }
        foreach (var A in IDF)
        {
            IDF[A.Key] = (float)Math.Log((double)cantidadDeDocumentos / A.Value);
        }

    }
    //llenar los diccionariosTF_IDF de cada documentos
    public static void CrearTF_IDF()
    {
        for (int i = 0; i < cantidadDeDocumentos; i++)
        {
            documentos[i].LlenarTF_IDF();
        }
    }
    //Calcula el Score utilizando la Similitud de Cosenos 
    public static float CalcularScore(Documento A, Query B)
    {
        float score = 0.0f;
        float NormaA = 0.0f;
        float NormaB = 0.0f;

        foreach (var palabra in B.TF_IDF)
        {
            if (A.TF_IDF.ContainsKey(palabra.Key))
            {
                score += A.TF_IDF[palabra.Key] * B.TF_IDF[palabra.Key];
            }
            NormaB += B.TF_IDF[palabra.Key] * B.TF_IDF[palabra.Key];
        }
        foreach (var palabra in A.TF_IDF)
        {
            NormaA += A.TF_IDF[palabra.Key] * A.TF_IDF[palabra.Key];
        }

        score = score / (float)Math.Sqrt(NormaA * NormaB);
        return score;
    }
    //comprobar que dos strings son iguales 
    static bool SonIguales(string A, string B)
    {
        if (A.Length == B.Length)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] != B[i]) return false;
            }
            return true;
        }
        return false;
    }
    //crear snipper
    static string Snipper(Documento A, Query B)
    {
        string snipper = "";


        return snipper;
    }
    //Agregar un nuevo SearchItem a la lista pero q quede organizada la lista 
    static void Agregar(List<SearchItem> A, SearchItem B)
    {
        for (int i = 0; i < A.Count; i++)
        {
            if (B.Score >= A[i].Score)
            {
                A.Insert(i, B);
                break;
            }
            if (i == A.Count) A.Add(B);
        }

    }

    public static SearchResult Query(string query)
    {
        Query entrada = new Query(query);
        List<SearchItem> list = new List<SearchItem>();
        //asignarle el score a cada documento
        for (int i = 0; i < cantidadDeDocumentos; i++)
        {
            documentos[i].score = CalcularScore(documentos[i], entrada);
            if (documentos[i].score != 0)
            {
                SearchItem A = new SearchItem(documentos[i].titulo, Snipper(documentos[i], entrada), documentos[i].score);
                Agregar(list, A);
            }
        }
        return new SearchResult(list.ToArray(), query);
    }
}
