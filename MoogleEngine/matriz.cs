
public class Matriz
{
    double[,]matriz;
    public Matriz Suma(Matriz A,Matriz B)
    {
        double[,] suma=new double[A.matriz.GetLongLength(0) ,A.matriz.GetLength(1)];

        for(int i=0;i<A.matriz.GetLength(0);i++)
        {
            for(int j=0;j<A.matriz.GetLength(1);j++)
            {
                suma[i,j]=A.matriz[i,j]+B.matriz[i,j];
            }
        }
        return new Matriz(suma);
    }

    public Matriz Producto(Matriz A,Matriz B)
    {
       double[,]producto=new double[A.matriz.GetLength(0),B.matriz.GetLength(1)];

        for(int i=0;i<A.matriz.GetLength(0);i++)
        {
           for(int j=0;j<B.matriz.GetLength(1);j++)
           {
                for(int k=0;k<A.matriz.GetLength(1);k++)
                {
                    producto[i,j]+=A.matriz[i,k]*B.matriz[k,j];
                }
           }

        }
       return new Matriz(producto);
    }
   
    public Matriz ProductoPorEscalar(Matriz A, float b)
    {
        double[,]Producto=new double[A.matriz.GetLength(0),A.matriz.GetLength(1)];

        for(int i=0;i<A.matriz.GetLength(0);i++)
        {
            for(int j=0;j<A.matriz.GetLength(1);j++)
            {
                Producto[i,j]=A.matriz[i,j]*b;
            }          
        }
        return new Matriz(Producto);
    }
    public Matriz(double[,]a)
    {
        this.matriz=a;
    }
}