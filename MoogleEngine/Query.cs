
namespace MoogleEngine;

public class Query
{
    string texto;
    string[] TodasLasPalabras;
    string[] TodasLasPalabrasConCaracteres;
    public Dictionary<string, int> palabras;
    public List<string> TienenQueEstar = new List<string>();
    public List<string> NoPuedenEstar = new List<string>();
    public void LlenarPalabras()
    {
        for (int i = 0; i < TodasLasPalabras.Length; i++)
        {
            if(TodasLasPalabras[i].Length==0 || TodasLasPalabras[i]==" ")continue;
            if (!palabras.ContainsKey(TodasLasPalabras[i]))
            {
                palabras.Add(TodasLasPalabras[i], 1);
            }
        }
    }
    //Dividir la query en palabras
    string[] Dividir(string A)
    {
        char[] ignorar = { '!', '^', '*', ' ', ',', '.', ';', '`', '@', '#', '/', '<', '>', '?', '$', '%', '&', '(', ')', '_', '+', '[', ']', '{', '}', '|' };
        string[] TodasLAsPalabras = A.ToLower().Split(ignorar).Select(palabra => palabra.Trim()).ToArray();
        return TodasLAsPalabras;
    }
    //Dividir la query en palabras pero mantener los caracteres '*' , '^' , '!' .
    private string[] DividirYMantenerCaracteres(string A)
    {
        char[] ignorar = { ' ', ',', '.', ';', '`', '@', '#', '/', '<', '>', '?', '$', '%', '&', '(', ')', '_', '+', '[', ']', '{', '}', '|' };
        string[] TodasLasPalabrasConCaracteres = A.ToLower().Split(ignorar).Select(palabra => palabra.Trim()).ToArray();
        return TodasLasPalabrasConCaracteres;
    }

    string CorregirPalabra(string s)
    {
        string NuevaPalabra = "";
        foreach (char c in s)
        {
            if (c == '!' || c == '^' || c == '*') continue;
            NuevaPalabra += c;
        }
        return NuevaPalabra;
    }

    //Llenar TienenQueEstar y NoPuedenEstar
    void LlenarTienenQueEstarYNoPuedenEstar()
    {
        for (int i = 0; i < TodasLasPalabrasConCaracteres.Length; i++)
        {
            TodasLasPalabrasConCaracteres[i].Trim();
            if(TodasLasPalabrasConCaracteres[i].Length==0 || TodasLasPalabrasConCaracteres[i]==" ")continue;

            if (TodasLasPalabrasConCaracteres[i][0] == '!')
            {
                NoPuedenEstar.Add(CorregirPalabra(TodasLasPalabrasConCaracteres[i]));
            }
            if (TodasLasPalabrasConCaracteres[i][0] == '^')
            {
                TienenQueEstar.Add(CorregirPalabra(TodasLasPalabrasConCaracteres[i]));
            }
            if (TodasLasPalabrasConCaracteres[i][0] == '*')
            {
                string NuevaPalabra = CorregirPalabra(TodasLasPalabrasConCaracteres[i]);

                for (int j = 1; j < TodasLasPalabrasConCaracteres[i].Length; j++)
                {
                    if (TodasLasPalabrasConCaracteres[i][j] != '*')
                    {
                        if (palabras.ContainsKey(NuevaPalabra))
                        {
                            palabras[NuevaPalabra] = (int)Math.Pow(2, j);
                        }
                    }
                }
            }
        }
    }
    //constructor
    public Query(string query)
    {
        this.texto = query;
        palabras = new Dictionary<string, int>();
        this.TodasLasPalabras = Dividir(texto);
        this.TodasLasPalabrasConCaracteres = DividirYMantenerCaracteres(texto);
        LlenarPalabras();
        LlenarTienenQueEstarYNoPuedenEstar();
    }
}