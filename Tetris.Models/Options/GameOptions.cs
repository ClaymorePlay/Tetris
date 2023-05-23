using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Options
{
    public class GameOptions
    {
        /// <summary>
        /// Высота поля в квадратах
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Ширина поля в квадратах
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Время движения падающей фигуры в милисекундах
        /// </summary>
        public double TimerWalkShapeMs { get; set; }
    }
}
