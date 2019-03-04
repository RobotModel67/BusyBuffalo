using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel67.BusyBuffalo.SudokuSolver.Model
{
    public class Board<T>
    {
        private int length = 9;
        private T[,] matrix;

        public Board(T[,] matrix) {
            this.matrix = matrix;
        }

        /*

        private T[] fila = new T[8];
        private T[] columna = new T[8];

        private T[][] filas = new T[8][];
        private T[][] columnas = new T[8][];

        public T[] getFila(int index)
        {
            var fila = new T[8];
            for (int i = 0; i <= 8; i++)
            {
                fila[i] = matrix[index, i];
            }
            return fila;
        }
        */
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    sb.AppendFormat("{0} ", matrix[i,j]);
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

    }
    /*
    class Cuadrado<T>
    {
        private T[,] matrix = new T[2, 2];
        public List<T> Data { get {
                return ma
            }; set; }

    }

    */
    class Arreglo<T>
    {
        public List<T> Data { get; set; }
    }
}
