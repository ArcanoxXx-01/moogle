# Moogle!

![](moogle.png)

> Proyecto de Programación I.
> Facultad de Matemática y Computación - Universidad de La Habana.
> Cursos 2021, 2022.

Moogle! es una aplicación *totalmente original* cuyo propósito es buscar inteligentemente un texto en un conjunto de documentos.

Es una aplicación web, desarrollada con tecnología .NET Core 6.0, específicamente usando Blazor como *framework* web para la interfaz gráfica, y en el lenguaje C#.
La aplicación está dividida en dos componentes fundamentales:

- `MoogleServer` es un servidor web que renderiza la interfaz gráfica y sirve los resultados.
- `MoogleEngine` es una biblioteca de clases donde está implementada la lógica del algoritmo de búsqueda.

- `MoogleEngine` esta compuesta por 6 clases principales:

-Moogle.
-Leer.
-Documentos.
-Query.
-Searchitem.
-searchResult.

La static class Moogle es la clase principal y es donde se ejecuta el la mayor parte del programa.Tiene 
como objetivo crear un tipo SearchResult de acuerdo a la busqueda introducida por el usuario.
Entre las propiedades de la clase Moogle se encuentran:


    public static int cantidadDeDocumentos //como lo dice su nombre almacena la cantidad de documentos en la base de datos.

    public static Documento[] documentos // arreglo de variables de Tipo Documento.

    puublic static Dictionary<string, float> IDF // diccionario que se utilizara como vector IDF de todas las palabras de todos los documentos.

    Entre sus metodos encontramos:

    - static void ConstruirDocumentos() // es la encargada de crear todoass los documentos del Documento[] documentos.

    -public static void LlenarIDF() // llena el vector IDF con los adecuados de los documentos.

    -public static void CrearTF_IDF() // encargado de llenar los vectores TF_IDF de cada documento.

    -public static float CalcularScore(Documento A, Query B) //es el encargado de asignarle un score a cada documento utilizando el metodo de similitud de cosenos entre los vectores TF_IDF de la cuery y el documento.

    -static string Snippet(Documento A, Query B) // con este metodo creamos el snippet correspondiente a la palabra de mayor relevancia de un documento con respecto a la query.Para mas detalles vease el codigo.

    -ademas hay metodos como son     
    static bool ComprobarNoPuedenEstar(Documento A, Query B)   static bool ComprobarTienenQueEstar(Documento A, Query B)
    static bool SonIguales(string A, string B)
    static void Agregar(List<SearchItem> A, SearchItem B)


    //que se emplean para comprobar que dos string son iguales y para verificar que los documentos contienen o no a los elementos deseados, asi como agregar de forma inteligente nuevos elementos a la lista de reultados.

    tambien esyta el metodo :

    public static SearchResult Query(string query) // en el cual se llama al constructor de la query. Y donde a demas se crea el arreglo con los searchitems que se daran como respuesta.


    LA CLASE LEER.

    Esta clase tiene como propiedades:

    -const string direccion = @"/home/dario/Desktop/Programs/moogle/moogle/Content";  // que es la direccion donde esyta la base de datos de los .txt
    
    ademas tiene como propiedades:

    -public static string[] DocumentosaLeer = new string[] { };
        //que se encarga de guardar los nombres de los documentos a leer , y q dichos nombres seran pasados com parametros para la construccion de los documentos en el metodo static void ConstruirDocumentos() de la clase Moogle.

    -public static void ObtenerTextos() //que se encarga de leer el .txt y guardar todo su contenido como un string.

    ta,bien esta el metodo 
    public static string[] Dividir(string A) // q se encarga de coger el string q devuekve ObtenerTextos y dividirlo en palabras , obviando a los caracteres innecesarios.

    LA CLASE DOCUMENTO

    como bien lo dice su nombre es la clase q representa de manera abstracta lo q seria cada documento, almacenando como propiedades:

    -public string titulo;
    -public string texto;

    -public string[] TodasLasPalabras; // es el valor devuelto por el metodo Dividir de la clase Leer cuando se le pasa como parametro el texto de este documento

    public Dictionary<string, int> palabras; // cada llave de este diccionario representa una palabra del documento y el cvalor asociado a cada llave(palabra) es la cantidad de veces q se repite dicha palabra en el doc.Este vector sera de vital importancia en la construccion del vector TF_IDF.

    public Dictionary<string, float> TF_IDF; //este diccionario es la representacion abstracta del vector TF*IDF que es necesario para realizar la similitud de cosenos.Este diccionario tiene las mismas llaves q el diccionario palabras , solo q en este a cada una de las llaves se le asocia el valor del TF*IDF de la llave.
    es importante llenar este "vetor" luego de haber calculado los valores del TF y de IDF ;

    public float score = new float(); //este score mide que tan relevante es este documento a la busqueda realizada.

    Los metodos de esta clase son solo dos 

    public void LlenarPalabras()
    
    public void LlenarTF_IDF()

    que como lo dice sus nombres son los encargados de darle los valores a los "vectores" palabras y TF_IDF.

    LA CLASE QUERY

    esta clase es muy parecida a la clase Documento debido a sus propiedades y metodos 

    Propiedades:

    string texto; // lo mismo q para los documentos pero este guarda el texto de la query
    string[] TodasLasPalabras; // ...XD
    
    string[] TodasLasPalabrasConCaracteres; //
    
    public Dictionary<string, int> palabras; // ... 
    
    public List<string> TienenQueEstar = new 
    // 
    
    
    List<string>();
    public List<string> NoPuedenEstar = new List<string>();






El tipo `SearchResult` recibe en su constructor dos argumentos: `items` y `suggestion`. El parámetro `items` es un array de objetos de tipo `SearchItem`. Cada uno de estos objetos representa un posible documento que coincide al menos parcialmente con la consulta en `query`.

Cada `SearchItem` recibe 3 argumentos en su constructor: `title`, `snippet` y `score`. El parámetro `title` debe ser el título del documento (el nombre del archivo de texto correspondiente). El parámetro `snippet` debe contener una porción del documento donde se encontró el contenido del `query`. El parámetro `score` tendrá un valor de tipo `float` que será más alto mientras más relevante sea este item.


## Sobre la búsqueda

La busqueda esta basada en el principio de similitud de coseno entre los vectores TF-IDF.

Para ello en primer lugar el usuario debe escribir una frase a buscar 



PARA EL USUARIO :

1-los txt de la base de datos estan en español

2-escriba una frase a buscar 

3-recibira una lista ordenada de mayor a menor relevancia de todos los documentos con respecto 
a la query realizada.

4-Ademas para cada documento que se muestre como respuesta lo acompañara un pequeño fragmento del 
documento que contiene la palabra con mayor relevancia con respecto a la query (vease el codigo para comprender como funciona)

5-en caso de q no se encuentren documentos relacionados con la busqueda se le informara

6-si introduce una query vacia no le saldra ningun documento y se le informara

7-utilizando los caracteres ! , ^ , * se pueden realizar busquedas mas personalizadas 

 Ejemplo #1: al utilizar el caracte ! delante de una palabra (tiene q estar pegado a la palabra)
 se le mostraran como resultado todos los txt q tengan relevancia con la query , pero solo mostrara 
 los q no contienen a la palabra q se le puso el ! delante (ojo: si solo escribe una palabra acompa;ada del ! no le saldra ningun resultado)
 De igual forma funciona el caracter ^ , solo q te mostrara solo los txt q si contienen a la palabra.

 El caracter * se utiliza de igual forma q los otros, per lo q hace es darle el doble de relevancia a la 
 palabra q acompaña, por lo q la relevancia de la palabra se multiplica por 2^(cantidad de * q tenga delante)
 porque se pueden poner mas de un * delate , lo q todos tienen q estar juntos,

 Pruebe hacer busqueda con algunas combinaciones de estos caracteres lo q siempre ponga los caracteres delante de la palabra y pegada a ella y no poner mas de un ! o un ^ (el programa no explota xq lo hagas pero no tiene sentido hacerlo) y por supuesto no poner juntos !^ xq no tiene ningun sentido.









