namespace MoogleEngine;

public class Documento
{
    public string titulo;
    public string texto;
    public string[] TodasLasPalabras;
    /*este diccionario va a guardar como llaves cada una de las palabras diferentes del texto del 
    Documento y el valor asociado a cada llave sera la cantidad de veces que se repite cada dicha
    palabra en el texto*/
    public Dictionary<string, int> palabras;
    /*Este diccionario va a servir como vector TF-IDF del documento*/
    public Dictionary<string, float> TF_IDF;
    //esta float almacenara el score que va a tener este documento sobre la query relizada
    public float score = new float();
    //metodo para llenar el diccionario palabras xD
    public void LlenarPalabras()
    {
        for (int i = 0; i < TodasLasPalabras.Length; i++)
        {
            if (palabras.ContainsKey(TodasLasPalabras[i]))
            {
                palabras[TodasLasPalabras[i]]++;
            }
            else
            {
                palabras.Add(TodasLasPalabras[i], 1);
            }
        }
    }
    //metodo para llenar el vector TF-IDF del documento
    //cuando llamemos a este metodo ya el vector IDF general (de cada palabra) ya va a estar calculado
    public void LlenarTF_IDF()
    {

        foreach (var A in palabras)
        {
            TF_IDF.Add(A.Key, (float)palabras[A.Key] / TodasLasPalabras.Length * Moogle.IDF[A.Key]);
        }

    }
    //constructor
    public Documento(string titulo, string texto)
    {
        this.titulo = titulo;
        this.texto = texto;
        this.TodasLasPalabras = Leer.Dividir(texto);
        this.palabras = new Dictionary<string, int>();
        this.TF_IDF = new Dictionary<string, float>();
    }
}