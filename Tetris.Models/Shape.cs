using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models.Enums;

namespace Tetris.Models
{
    public class Shape
    {
        /// <summary>
        /// Тип фигуры
        /// </summary>
        public ShapeTypeEnum Type { get; set; }

        /// <summary>
        /// Квадраты из которых состоит фигура
        /// </summary>
        public ConcurrentBag<Square> Squares { get; set; }

        /// <summary>
        /// Центральный квадрат относительно которого производится поворот
        /// </summary>
        public Square CenterSquare { get; set; }

        /// <summary>
        /// Блокировка падающей фигуры
        /// </summary>
        public bool BlockShape { get; set; }


        /// <summary>
        /// Движение вниз
        /// </summary>
        public bool MoveDown(Graphics graph, ConcurrentBag<Square> squares)
        {
                if (BlockShape == true)
                    return false;
            try
            {
                BlockShape = true;
                if (squares.Max(c => c.Y) == Squares.Max(c => c.Y))
                    return false;

                var valid = Squares.All(c =>
                {
                    var nextSquare = squares.FirstOrDefault(h => h.X == c.X && h.Y == c.Y + 20);
                    if (nextSquare == null || (nextSquare.Color.HasValue && !Squares.Contains(nextSquare)))
                        return false;

                    return true;
                });

                if (!valid)
                    return false;

                ConcurrentBag<Square> updateSquares = new ConcurrentBag<Square>();
                foreach (var square in Squares.OrderByDescending(c => c.Y))
                {
                    var nextSquare = squares.First(c => c.X == square.X && c.Y == square.Y + 20);

                    nextSquare.Color = square.Color;
                    nextSquare.ViewSquare(graph);

                    square.HideSquare(graph);
                    updateSquares.Add(nextSquare);

                    if (square == CenterSquare)
                        CenterSquare = nextSquare;
                }

                Squares = updateSquares;
                return true;
            }
            finally
            {
                BlockShape = false;
            }
        }



        /// <summary>
        /// Движение вправо
        /// </summary>
        public bool MoveRight(Graphics graph, ConcurrentBag<Square> squares)
        {
           
                if (BlockShape == true)
                    return false;

            try
            {
                BlockShape = true;

                if (squares.Max(c => c.X) == Squares.Max(c => c.X))
                    return false;

                var valid = Squares.All(c =>
                {
                    var nextSquare = squares.FirstOrDefault(h => h.Y == c.Y && h.X == c.X + 20);
                    if (nextSquare == null || (nextSquare.Color.HasValue && !Squares.Contains(nextSquare)))
                        return false;

                    return true;
                });

                if (!valid)
                    return false;

                ConcurrentBag<Square> updateSquares = new ConcurrentBag<Square>();
                foreach (var square in Squares.OrderByDescending(c => c.X))
                {
                    var nextSquare = squares.First(c => c.Y == square.Y && c.X == square.X + 20);

                    nextSquare.Color = square.Color;
                    nextSquare.ViewSquare(graph);

                    square.HideSquare(graph);
                    updateSquares.Add(nextSquare);

                    if (square == CenterSquare)
                        CenterSquare = nextSquare;
                }

                Squares = updateSquares;
                return true;
            }
            finally
            {
                BlockShape = false;
            }
        }

        /// <summary>
        /// Движение влево
        /// </summary>
        public bool MoveLeft(Graphics graph, ConcurrentBag<Square> squares)
        {
            
                if (BlockShape == true)
                    return false;

            try
            {
                BlockShape = true;
                if (squares.Min(c => c.X) == Squares.Min(c => c.X))
                    return false;
                

                var valid = Squares.All(c =>
                {
                    var nextSquare = squares.FirstOrDefault(h => h.Y == c.Y && h.X == c.X - 20);
                    if (nextSquare == null || (nextSquare.Color.HasValue && !Squares.Contains(nextSquare)))
                        return false;
                    
                    return true;
                });

                if (!valid)
                    return false;
                

                ConcurrentBag<Square> updateSquares = new ConcurrentBag<Square>();
                foreach (var square in Squares.OrderBy(c => c.X))
                {
                    var nextSquare = squares.First(c => c.Y == square.Y && c.X == square.X - 20);

                    nextSquare.Color = square.Color;
                    nextSquare.ViewSquare(graph);

                    square.HideSquare(graph);
                    updateSquares.Add(nextSquare);

                    if (square == CenterSquare)
                        CenterSquare = nextSquare;
                }

                Squares = updateSquares;
                return true;

            }
            finally
            {
                BlockShape = false;
            }
        }


        /// <summary>
        /// Поворот
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="squares"></param>
        public bool Rotate(Graphics graph, ConcurrentBag<Square> squares)
        {
                if (BlockShape == true)
                    return false;
            try
            {
                BlockShape = true;

                double radians = 90 * (Math.PI / 180.0);

                //double sin = Math.Sin(radians);
                //double cos = Math.Cos(radians);

                //var valid = Squares.All(c =>
                //{
                //    var nextSquare = squares.FirstOrDefault(h => h.Y == c.Y && h.X == c.X - 20);
                //    if (nextSquare == null || (nextSquare.Color.HasValue && !Squares.Contains(nextSquare)))
                //        return false;
                //    if (nextSquare == null || (nextSquare.Color.HasValue && !Squares.Contains(nextSquare)))
                //        return false;
                //    if (nextSquare == null || (nextSquare.Color.HasValue && !Squares.Contains(nextSquare)))
                //        return false;

 

                //    return true;
                //});

                //if (!valid)
                //    return false;

                ConcurrentBag<Square> updateSquares = new ConcurrentBag<Square>();
                foreach (var square in Squares.Where(c => c != CenterSquare))
                {
                    var newX = (square.X - CenterSquare.X) * Math.Cos(radians) - (square.Y - CenterSquare.Y) * Math.Sin(radians) + CenterSquare.X;
                    var newY = (square.X - CenterSquare.X) * Math.Sin(radians) + (square.Y - CenterSquare.Y) * Math.Cos(radians) + CenterSquare.Y;

                    var nextSquare = squares.FirstOrDefault(h => h.Y == newY && h.X == newX);
                    if (nextSquare == null)
                        continue;

                    if (nextSquare.Color.HasValue && Squares.Contains(nextSquare))
                        continue;

                    nextSquare.Color = square.Color;

                    square.HideSquare(graph);
                    nextSquare.ViewSquare(graph);

                    updateSquares.Add(nextSquare);
                }
                updateSquares.Add(CenterSquare);
                Squares = updateSquares;
                return true;
            }
            finally
            {
                BlockShape = false;
            }
        }
    }
}
