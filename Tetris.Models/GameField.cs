using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tetris.Options;
using Timer = System.Timers.Timer;

namespace Tetris.Models
{
    public class GameField
    {
        /// <summary>
        /// Щирина
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; set; }  

        /// <summary>
        /// Падающая фигура
        /// </summary>
        public Shape? CurrentShape { get; set; }

        /// <summary>
        /// Квадраты
        /// </summary>
        public ConcurrentBag<Square> Squares { get; set; } = new ConcurrentBag<Square>();

        /// <summary>
        /// Таймер
        /// </summary>
        private Timer _timer { get; set; }

        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        private Random _rnd = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Объект графики
        /// </summary>
        private Graphics _graph { get; set; }

        /// <summary>
        /// Блокатор
        /// </summary>
        public static object _locker = new object();

        /// <summary>
        /// Событие завершения заполнения полосы
        /// </summary>
        public event EventHandler OnCompleteLine;

        public GameField(GameOptions options, Graphics graph)
        {
            Width = options.Width;
            Height = options.Height;
            _graph = graph;



            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Enabled = false;
            _timer.Interval = options.TimerWalkShapeMs;
            _timer.Elapsed += _timer_Elapsed;
        }

        /// <summary>
        /// Создание и движение фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
                if (CurrentShape != null)
                {
                    var valid = CurrentShape?.MoveDown(_graph, Squares);
                    if (valid == false)
                    {
                        CheckLine();
                        CurrentShape = null;
                    }
                }
                else
                    GenerateRandomShape();


                _timer.Start();
            }
        }

        /// <summary>
        /// Проверка заполненной линии
        /// </summary>
        private void CheckLine()
        {
            if (CurrentShape == null)
                return;

            foreach(var groupSquares in CurrentShape.Squares.GroupBy(c => c.Y).OrderByDescending(c => c.Key))
            {
                var square = groupSquares.First();
                var valid = Squares.Where(c => c.Y == groupSquares.Key).All(c => c.Color.HasValue);

                if(valid)
                {
                    foreach (var item in Squares.Where(c => c.Y == groupSquares.Key))
                        item.HideSquare(_graph);

                    var upperSquares = Squares.Where(c => c.Y < groupSquares.Key).Where(c => c.Color.HasValue);
                    foreach(var item in upperSquares)
                    {
                        item.HideSquare(_graph);
                        var next = Squares.FirstOrDefault(c => c.X == item.X && c.Y == item.Y + 20);
                        next?.ViewSquare(_graph);
                    }

                    OnCompleteLine?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Получение случайного цвета
        /// </summary>
        /// <returns></returns>
        private Color RandomColor()
        {
            switch(_rnd.Next(0, 7))
            {
                case 0:
                    return Color.Green;
                case 1:
                    return Color.Red;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Peru;
                case 5:
                    return Color.Coral;
                case 6:
                    return Color.Chocolate;
                default:
                    return Color.BurlyWood;
            }
            
        }

        /// <summary>
        /// Построение фигуры
        /// </summary>
        /// <param name="color"></param>
        /// <param name="coords"></param>
        /// <returns></returns>
        private Shape BuildShape(Color color, params (int x, int y)[] coords)
        {
            var shape = new Shape() { Squares = new ConcurrentBag<Square>() };
            foreach(var coord in coords)
            {
                var square = Squares.First(c => c.X == coord.x && c.Y == coord.y);
                square.Color = color;
                square.ViewSquare(_graph);

                shape.Squares.Add(square);
            }

            shape.CenterSquare = shape.Squares.First();
            return shape;
        }


        /// <summary>
        /// Генерация случайной фигуры
        /// </summary>
        private void GenerateRandomShape()
        {
            var randColor = RandomColor();
            switch (_rnd.Next(7))
            {
                case 0:
                    CurrentShape = BuildShape(randColor, (80, 0), (80, 20), (80, 40), (60, 40));
                    break;
                case 1:
                    CurrentShape = BuildShape(randColor, (80, 0), (80, 20), (80, 40), (80, 60));
                    break;
                case 2:
                    CurrentShape = BuildShape(randColor, (80, 0), (80, 20), (100, 0), (100, 20));
                    break;
                case 3:
                    CurrentShape = BuildShape(randColor, (80, 0), (80, 20), (80, 40), (100, 40));
                    break;
                case 4:
                    CurrentShape = BuildShape(randColor, (80, 0), (100, 0), (100, 20), (120, 20));
                    break;
                case 5:
                    CurrentShape = BuildShape(randColor, (80, 0), (80, 20), (100, 20), (60, 20));
                    break;
                case 6:
                    CurrentShape = BuildShape(randColor, (80, 20), (100, 20), (100, 0), (120, 0));
                    break;
            }
        }


        /// <summary>
        /// Построение поля
        /// </summary>
        public void BuildField()
        {
            for (int i = 0; i < Width * 20; i += 20)
            {
                for(int j = 0; j < Height * 20; j += 20)
                {
                    Squares.Add(new Square
                    {
                        X = i,
                        Y = j
                    });
                }
            }
        }

        /// <summary>
        /// Отображение поля
        /// </summary>
        public void DrawField()
        {
            Pen blackPen = new Pen(Color.Black);

            foreach (var square in Squares)
            {
                _graph.DrawRectangle(blackPen, (int)square.X, (int)square.Y, 20, 20);
                
                if(square.Color.HasValue)
                    _graph.FillRectangle(new SolidBrush(square.Color.Value), (int)square.X, (int)square.Y, 20, 20);

            }
            //GenerateRandomShape();
            _timer.Start();
        }
    }
}

