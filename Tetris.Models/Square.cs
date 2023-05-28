using System.Drawing;

namespace Tetris.Models
{
    public class Square
    {
        /// <summary>
        /// Цвет
        /// </summary>
        public Color? Color { get; set; }

        /// <summary>
        /// Положение по X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Положение по Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Отобразить
        /// </summary>
        public void ViewSquare(Graphics graph)
        {
            
            graph.FillRectangle(new SolidBrush(this.Color ?? System.Drawing.Color.Green), (int)X, (int)Y, 20, 20);
            
        }

        /// <summary>
        /// Скрыть
        /// </summary>
        public void HideSquare(Graphics graph)
        {
            
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.GhostWhite), (int)X, (int)Y, 20, 20);
            graph.DrawRectangle(new Pen(System.Drawing.Color.Black), (int)X, (int)Y, 20, 20);

            Color = null;
            
        }
    }
}