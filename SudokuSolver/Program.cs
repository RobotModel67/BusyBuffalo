﻿using RobotModel67.BusyBuffalo.SudokuSolver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel67.BusyBuffalo.SudokuSolver
{
    class Program
    {
        // http://www.sudokuwiki.org/A_New_Metric_for_Difficult_Sudoku_Puzzles

        static void Main(string[] args)
        {
            var board = BuildBoard02();


            // Experimento
            /*
            var result = Validator.HitCounter(board);
            Console.Write("Hits: {0}", result);

            board.Cells[8, 2].ValorTentativo = 4;
            result = Validator.HitCounter(board);
            Console.Write("Hits: {0}", result);

            board.Cells[8, 2].ValorTentativo = 9;
            result = Validator.HitCounter(board);
            Console.Write("Hits: {0}", result);
            */
            //var result = Validator.ValidarEnFila(board, 4, 1);
            //var result = Validator.ValidarEnColumna(board, 4, 3);
            //var result = Validator.ValidarEnCuadrito(board, 3, 7);
            board.GenerarValoresPosibles();
            //var result = FuerzaBruta.Solve(board);
            //Console.WriteLine(board);

            var solver = new FuerzaBruta(board);
            var result2 = solver.Solve();

            Console.Write(result2);
            Console.ReadLine();
        }

        static Board BuildBoard01()
        {
            // https://es.wikipedia.org/wiki/Sudoku

            Board board = new Board();

            board.Cells[0, 0].ValorDefinitivo = 5;
            board.Cells[0, 1].ValorDefinitivo = 3;
            board.Cells[0, 4].ValorDefinitivo = 7;

            board.Cells[1, 0].ValorDefinitivo = 6;
            board.Cells[1, 3].ValorDefinitivo = 1;
            board.Cells[1, 4].ValorDefinitivo = 9;
            board.Cells[1, 5].ValorDefinitivo = 5;

            board.Cells[2, 1].ValorDefinitivo = 9;
            board.Cells[2, 2].ValorDefinitivo = 8;
            board.Cells[2, 7].ValorDefinitivo = 6;

            board.Cells[3, 0].ValorDefinitivo = 8;
            board.Cells[3, 4].ValorDefinitivo = 6;
            board.Cells[3, 8].ValorDefinitivo = 3;

            board.Cells[4, 0].ValorDefinitivo = 4;
            board.Cells[4, 3].ValorDefinitivo = 8;
            board.Cells[4, 5].ValorDefinitivo = 3;
            board.Cells[4, 8].ValorDefinitivo = 1;

            board.Cells[5, 0].ValorDefinitivo = 7;
            board.Cells[5, 4].ValorDefinitivo = 2;
            board.Cells[5, 8].ValorDefinitivo = 6;

            board.Cells[6, 1].ValorDefinitivo = 6;
            board.Cells[6, 6].ValorDefinitivo = 2;
            board.Cells[6, 7].ValorDefinitivo = 8;

            board.Cells[7, 3].ValorDefinitivo = 4;
            board.Cells[7, 4].ValorDefinitivo = 1;
            board.Cells[7, 5].ValorDefinitivo = 9;
            board.Cells[7, 8].ValorDefinitivo = 5;

            board.Cells[8, 4].ValorDefinitivo = 8;
            board.Cells[8, 7].ValorDefinitivo = 7;
            board.Cells[8, 8].ValorDefinitivo = 9;

            return board;
        }

        static Board BuildBoard02()
        {
            // https://ztfnews.wordpress.com/2012/06/29/el-sudoku-mas-dificil/

            Board board = new Board();

            board.Cells[0, 0].ValorDefinitivo = 8;

            board.Cells[1, 2].ValorDefinitivo = 3;
            board.Cells[1, 3].ValorDefinitivo = 6;

            board.Cells[2, 1].ValorDefinitivo = 7;
            board.Cells[2, 4].ValorDefinitivo = 9;
            board.Cells[2, 6].ValorDefinitivo = 2;

            board.Cells[3, 1].ValorDefinitivo = 5;
            board.Cells[3, 5].ValorDefinitivo = 7;

            board.Cells[4, 4].ValorDefinitivo = 4;
            board.Cells[4, 5].ValorDefinitivo = 5;
            board.Cells[4, 6].ValorDefinitivo = 7;

            board.Cells[5, 3].ValorDefinitivo = 1;
            board.Cells[5, 7].ValorDefinitivo = 3;

            board.Cells[6, 2].ValorDefinitivo = 1;
            board.Cells[6, 7].ValorDefinitivo = 6;
            board.Cells[6, 8].ValorDefinitivo = 8;

            board.Cells[7, 2].ValorDefinitivo = 8;
            board.Cells[7, 3].ValorDefinitivo = 5;
            board.Cells[7, 7].ValorDefinitivo = 1;

            board.Cells[8, 1].ValorDefinitivo = 9;
            board.Cells[8, 6].ValorDefinitivo = 4;

            return board;
        }

    }
}
