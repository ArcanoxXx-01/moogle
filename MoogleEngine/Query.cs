
namespace MoogleEngine;

public class Query
{
    string texto;
    string[] TodasLasPalabras;
    public Dictionary<string, int> palabras;
    public void LlenarPalabras()
    {
        for (int i = 0; i < TodasLasPalabras.Length; i++)
        {
            if (!palabras.ContainsKey(TodasLasPalabras[i]))
            {
                palabras.Add(TodasLasPalabras[i], 1);
            }
        }
    }
    //Dividir la query en palabras
    private string[] Dividir(string A)
    {
        char[] ignorar = { ' ', ',', '.', ';', '`', '@', '#', '/', '<', '>', '?', '$', '%', '&', '(', ')', '_', '+', '[', ']', '{', '}', '|' };
        string[] TodasLAsPalabras = A.ToLower().Split(ignorar).Select(palabra => palabra.Trim()).ToArray();
        return TodasLAsPalabras;
    }
    //Insertar espacios 
    private void InsertarEspacios()
    {
        for (int i = 0; i < texto.Length; i++)
        {
            if ((texto[i] == '!' || texto[i] == '^' || texto[i] == '*') && i != texto.Length)
            {
                texto = texto.Insert(i+1, " ");
            }
        }
    }
    //constructor
    public Query(string query)
    {
        this.texto = query;
        palabras = new Dictionary<string, int>();
        this.TodasLasPalabras = Dividir(texto);
        LlenarPalabras();
    }
}