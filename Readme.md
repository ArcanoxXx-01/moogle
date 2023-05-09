
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

 la base de datos no tiene muchos libros pero se le pueden poner mas XD

 espero q el diseño sea de su agrado , se q es sencillo pero tengo planes de mejorarlo 









