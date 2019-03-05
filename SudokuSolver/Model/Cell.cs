using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel67.BusyBuffalo.SudokuSolver.Model
{
    class Cell
    {
        public int Index { get; set; }
        public int ValorDefinitivo { get; set; }
        public int ValorTentativo { get; set; }

        public List<int> ValoresPosibles { get; set; }

        public int getValue()
        {
            return ValorDefinitivo != 0 ? ValorDefinitivo : ValorTentativo;
        }
    }

    class Board
    {
        List<int> collection = (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }).ToList();

        public Board()
        {
            Cells = new Cell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }
        }

        public Cell[,] Cells { get; set; }

        public void GenerarValoresPosibles()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Cells[i, j].ValorDefinitivo == 0)
                    {
                        Cells[i, j].ValoresPosibles = GenerarValoresPosiblesCelda(i, j);
                    }
                }
            }
        }

        private List<int> GenerarValoresPosiblesCelda(int fila, int columna)
        {
            var result = new List<int>();
            var h = fila / 3;
            var k = columna / 3;
            var cuadrito = 3 * h + k;
            foreach (var item in collection)
            {
                if (Validator.ValidarEnFila(this, fila, item) &&
                    Validator.ValidarEnColumna(this, columna, item) &&
                    Validator.ValidarEnCuadrito(this, cuadrito, item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sb.AppendFormat(" {0} |", Cells[i, j].getValue());
                }
                sb.AppendLine();

            }
            return sb.ToString();
        }
    }

    class FuerzaBruta
    {
        private Board board;

        public FuerzaBruta(Board board)
        {
            this.board = board;
        }

        public bool Solve()
        {
            var result = true;

            Cell celda;
            var path = new List<Cell>();
            var index = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    celda = board.Cells[i, j];

                    if (celda.ValorDefinitivo == 0)
                    {
                        celda.Index = index;
                        index++;
                        path.Add(celda);
                    }
                }
            }

            var continuar = true;
            celda = path.FirstOrDefault();
            do
            {
                celda.ValorTentativo = celda.ValoresPosibles.FirstOrDefault(number => number > celda.ValorTentativo);
                Console.WriteLine(board);
                Console.WriteLine();
                if (celda.ValorTentativo != 0)
                {
                    var hits = Validator.HitCounter(board);
                    if (hits == 81)
                    {
                        Console.WriteLine("READY!!!");
                        Console.ReadLine();
                        continuar = false;
                    }
                    else if (hits == 0)
                    {
                        // ese valor no es valido. intentar con el siguiente
                        // dejamos pasar para generar un nuevo valor
                        //celda.ValorTentativo = celda.ValoresPosibles.FirstOrDefault(number => number > celda.ValorTentativo);
                        
                    }
                    else
                    {
                        // el valor es válido pero no hemos encontrado una solución porque aún tengo celdas vacias
                        // continuar generando valores
                        celda = path.Find(c => c.Index == celda.Index + 1);
                    }
                }
                else
                {
                    // se ejecuta cuando 
                    // se agotaron los valores posibles para esta celda, dar marcha atras
                    //celda.ValorTentativo = 0;
                    celda = path.Find(c => c.Index == celda.Index - 1);
                    if (celda == null)
                    {
                        // hemos vuelto al principio sin encontrar una solución
                        Console.WriteLine("FALLO!!!");
                        Console.ReadLine();
                        continuar = false;
                    }
                }
            } while (continuar);

            return result;   
        }


    }



    class Validator
    {

        // los cuadros se numeran del 0 al 8 de izquierda a derecha y de arriba a abajo
        public static bool ValidarEnCuadrito(Board board, int cuadro, int valor)
        {
            var h = cuadro / 3;
            var k = cuadro % 3;

            var result = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board.Cells[i + 3 * h, j + 3 * k].ValorDefinitivo == valor)
                    {
                        return false;
                    }
                }

            }
            return result;
        }
        public static bool ValidarEnFila(Board board, int fila, int valor)
        {
            var result = true;
            for (int j = 0; j < 9; j++)
            {
                if (board.Cells[fila, j].ValorDefinitivo == valor)
                {
                    return false;
                }
            }
            return result;
        }
        public static bool ValidarEnColumna(Board board, int columna, int valor)
        {
            var result = true;
            for (int i = 0; i < 9; i++)
            {
                if (board.Cells[i, columna].ValorDefinitivo == valor)
                {
                    return false;
                }
            }
            return result;
        }

        // Verifica que un board se encuentra en estado válido 
        // VALIDANDO TODO EL TABLERO
        public static bool IsValid(Board board)
        {
            var result = true;

            // revisar filas
            for (int i = 0; i < 9; i++)
            {
                var colected = new List<int>();
                var member = 0;
                for (int j = 0; j < 9; j++)
                {
                    member = board.Cells[i, j].getValue();
                    if (member != 0)
                    {
                        if (colected.Contains(member))
                        {
                            return false;
                        }
                        colected.Add(member);
                    }
                }
            }

            // revisar columnas
            for (int j = 0; j < 9; j++)
            {
                var colected = new List<int>();
                var member = 0;
                for (int i = 0; i < 9; i++)
                {
                    member = board.Cells[i, j].getValue();
                    if (member != 0)
                    {
                        if (colected.Contains(member))
                        {
                            return false;
                        }
                        colected.Add(member);
                    }
                }
            }

            // revisar cuadritos
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var colected = new List<int>();
                    var member = 0;

                    for (int k = 0; k < 3; k++)
                    {
                        for (int h = 0; h < 3; h++)
                        {
                            member = board.Cells[k + 3 * i, h + 3 * j].getValue();
                            Console.Write(member);
                            if (member != 0)
                            {
                                if (colected.Contains(member))
                                {
                                    return false;
                                }
                                colected.Add(member);
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }

            return result;
        }

        public static bool IsReady(Board board)
        {
            var result = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board.Cells[i,j].getValue() == 0)
                    {
                        return false;
                    }
                }
            }
            return result;
        }

        // Devuelve el número de celdas validados
        // Si valor = 81 se tiene una solución
        // Si se encuentra un valor invalido = 0
        public static int HitCounter(Board board)
        {
            var result = 0;    // retorna si hay conflicto (valor no válido encontrado)
            var counter = 0;
            // revisar filas
            for (int i = 0; i < 9; i++)
            {
                var colected = new List<int>();
                var member = 0;
                for (int j = 0; j < 9; j++)
                {
                    member = board.Cells[i, j].getValue();
                    if (member != 0)
                    {
                        if (colected.Contains(member))
                        {
                            return result;
                        }
                        colected.Add(member);
                    }
                }
            }

            // revisar columnas
            for (int j = 0; j < 9; j++)
            {
                var colected = new List<int>();
                var member = 0;
                for (int i = 0; i < 9; i++)
                {
                    member = board.Cells[i, j].getValue();
                    if (member != 0)
                    {
                        if (colected.Contains(member))
                        {
                            return result;
                        }
                        colected.Add(member);
                    }
                }
            }

            // revisar cuadritos
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var colected = new List<int>();
                    var member = 0;

                    for (int k = 0; k < 3; k++)
                    {
                        for (int h = 0; h < 3; h++)
                        {
                            member = board.Cells[k + 3 * i, h + 3 * j].getValue();
                            if (member != 0)
                            {
                                if (colected.Contains(member))
                                {
                                    return result;
                                }
                                colected.Add(member);
                                counter++;
                            }
                        }
                    }
                }
            }

            return counter;
        }

    }
}
