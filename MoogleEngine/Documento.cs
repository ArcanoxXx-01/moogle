namespace MoogleEngine;

public class Documento
{
    public string titulo;
    public string texto;
    public string[] TodasLasPalabras;
    public Dictionary<string, int> palabras;
    public Dictionary<string, float> TF_IDF;
    public float score = new float();
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