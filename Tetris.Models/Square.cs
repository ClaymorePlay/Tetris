using System.Drawing;

namespace Tetris.Models
{
    public class Square
    {
        public Color? Color { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        private static object _locker = new object();

        /// <summary>
        /// Отобразить
        /// </summary>
        public void ViewSquare(Graphics graph)
        {
            lock (_locker)
            {
                graph.FillRectangle(new SolidBrush(this.Color ?? System.Drawing.Color.Green), (int)X, (int)Y, 20, 20);
            }
        }

        /// <summary>
        /// Скрыть
        /// </summary>
        public void HideSquare(Graphics graph)
        {
            lock (_locker)
            {
                graph.FillRectangle(new SolidBrush(System.Drawing.Color.GhostWhite), (int)X, (int)Y, 20, 20);
                graph.DrawRectangle(new Pen(System.Drawing.Color.Black), (int)X, (int)Y, 20, 20);

                Color = null;
            }
        }
    }
}