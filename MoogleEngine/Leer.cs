
namespace MoogleEngine;

public static class Leer
{
    //direccion de la carpeta donde estan los txt
    const string direccion = @"/home/dario/Desktop/Programs/moogle/moogle/Content";
    public static string[] DocumentosaLeer = new string[] { };
    public static string[] textos = new string[] { };
    public static void ObtenerArchivos()
    {
        DocumentosaLeer = Directory.GetFiles(direccion, "*.txt");
    }
    public static void ObtenerTextos()
    {
        textos = new string[DocumentosaLeer.Length];
        for (int i = 0; i < DocumentosaLeer.Length; i++)
        {
            textos[i] = File.ReadAllText(DocumentosaLeer[i]);
        }
    }
    //metodo para dividir en palabras
    public static string[] Dividir(string A)
    {
        char[] ignorar = { ' ', ',', '.', ';', '`', '!', '@', '#', '/', '<', '>', '?', '$', '%', '^', '&', '*', '(', ')', '_', '+', '[', ']', '{', '}', '|' };
        string[] palabras = A.ToLower().Split(ignorar).Select(palabra => palabra.Trim()).ToArray();

        return palabras;
    }
}

