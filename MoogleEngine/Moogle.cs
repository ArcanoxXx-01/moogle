namespace MoogleEngine;

public static class Moogle
{
    public static int cantidadDeDocumentos = 0;
    public static Documento[] documentos = new Documento[] { };
    public static Dictionary<string, float> IDF = new Dictionary<string, float>();
    static Moogle()
    {
        ConstruirDocumentos();
        LlenarIDF();
        CrearTF_IDF();
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

        //productos de vectores 
        foreach (var X in B.palabras)
        {
            if (X.Key.Length == 0 || X.Key == " ") continue;
            if (IDF.ContainsKey(X.Key) && IDF[X.Key] == 0) continue;
            if (A.TF_IDF.ContainsKey(X.Key))
            {
                score += A.TF_IDF[X.Key] * B.palabras[X.Key];
            }
            //Norma del vector de la query
            NormaB += B.palabras[X.Key] * B.palabras[X.Key];
        }
        //Norma del vector deldocumento
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
    static string Snippet(Documento A, Query B)
    {
        string snippet = "";

        //buscar la palabra mas relevante 
        float contador = 0.0f;
        string PalabraMasRelevante = "";
        foreach (var X in B.palabras)
        {
            if (A.TF_IDF.ContainsKey(X.Key) && X.Value * A.TF_IDF[X.Key] >= contador)
            {
                if (X.Key.Length > 2)
                {
                    contador = X.Value * A.TF_IDF[X.Key];
                    PalabraMasRelevante = X.Key;
                }
            }
        }
        //buscar fragmento donde aparezca la palabra mas relevante
        for (int i = 0; i < A.TodasLasPalabras.Length; i++)
        {
            if (SonIguales(PalabraMasRelevante, A.TodasLasPalabras[i]))
            {
                if (i > 7)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        snippet += A.TodasLasPalabras[i - 7 + j];
                        snippet += " ";
                    }
                }
                else
                {
                    for (int j = 0; j <= i; j++)
                    {
                        snippet += A.TodasLasPalabras[j];
                        snippet += " ";
                    }
                }
                if (A.TodasLasPalabras.Length - i < 7)
                {
                    for (int j = i + 1; j < A.TodasLasPalabras.Length; j++)
                    {
                        snippet += A.TodasLasPalabras[j];
                        snippet += " ";
                    }
                }
                else
                {
                    for (int j = i + 1; j < i + 8; j++)
                    {
                        snippet += A.TodasLasPalabras[j];
                        snippet += " ";
                    }
                }
                return snippet;
            }
        }
        return snippet;
    }
    //comprobar que el documento contiene todas las palabras obligatorias
    static bool ComprobarTienenQueEstar(Documento A, Query B)
    {
        if (B.TienenQueEstar.Count == 0) return true;
        foreach (string x in B.TienenQueEstar)
        {
            if (!A.palabras.ContainsKey(x)) return false;
        }
        return true;
    }
    //comprobar que el documento no contiene ninguna de las palabras q no se ueden tener 
    static bool ComprobarNoPuedenEstar(Documento A, Query B)
    {
        if (B.NoPuedenEstar.Count == 0) return true;
        foreach (string x in B.NoPuedenEstar)
        {
            if (A.palabras.ContainsKey(x)) return false;
        }
        return true;
    }
    //Agregar un nuevo SearchItem a la lista pero q quede organizada la lista 
    static void Agregar(List<SearchItem> A, SearchItem B)
    {
        for (int i = 0; i < A.Count; i++)
        {
            if (B.Score >= A[i].Score)
            {
                A.Insert(i, B);
                return;
            }
        }
        A.Add(B);
    }
    public static SearchResult Query(string query)
    {
        Query entrada = new Query(query);

        if (entrada.palabras.Count == 0)
        {
            return new SearchResult(new SearchItem[] { new SearchItem("No introdujo nada en la busqueda", "Introduzca una nueva busqueda", 0) }, query);
        }

        List<SearchItem> list = new List<SearchItem>();

        //obtener el score de cada doc y agregarlo a list de forma conveniente
        for (int i = 0; i < cantidadDeDocumentos; i++)
        {
            documentos[i].score = CalcularScore(documentos[i], entrada);
            if (documentos[i].score != 0)
            {
                if (ComprobarTienenQueEstar(documentos[i], entrada) && ComprobarNoPuedenEstar(documentos[i], entrada))
                {
                    SearchItem A = new SearchItem(documentos[i].titulo, Snippet(documentos[i], entrada), documentos[i].score);
                    Agregar(list, A);
                }
            }
        }
        if (list.Count == 0)
        {
            SearchItem sugerencia = new SearchItem("No existe ningun documento asociado a su busqueda ", "Por Favor introduzca una nueva busqueda con palabras aceptadas", 0.1f);
            list.Add(sugerencia);
        }
        return new SearchResult(list.ToArray(), query);
    }
}
