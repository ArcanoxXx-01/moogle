
namespace MoogleEngine;

public static class Leer
{
    //direccion de la carpeta donde estan los txt
    const string direccion = @"/home/dario/Desktop/Programs/moogle/moogle/Content";
    //arreglo para guardar los nombres de los documentos q se van a analisar
    public static string[] DocumentosaLeer = new string[] { };
    /*en la posicion k de este arreglo se va a guardar el texto del documento que esta en 
    la posicion k de DocumentosaLeer*/
    public static string[] textos = new string[] { };
    //metodo para obtener los nombres de los documentos que se van a anlaisar
    public static void ObtenerArchivos()
    {
        DocumentosaLeer = Directory.GetFiles(direccion, "*.txt");
    }
    //metyodo para obtener y guardar el texto de cada documento
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