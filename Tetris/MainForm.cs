using System.Timers;
using Tetris.Models;
using Tetris.Options;
using Timer = System.Timers.Timer;

namespace Tetris
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объект графики для рисования объектов
        /// </summary>
        public Graphics _gr;

        /// <summary>
        /// Игровое поле
        /// </summary>
        private GameField Field { get; set; }

        private static object _locker = new object();


        public MainForm()
        {
            InitializeComponent();
            _gr = this.CreateGraphics();

            KeyPreview = true;

            if(File.Exists("./Save.txt"))
                MaxScore.Text = File.ReadAllText("./Save.txt");
        }
        

        /// <summary>
        /// Обработчик нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Field.CurrentShape?.BlockShape == true)
                return;
            lock(_locker) 
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        Field.CurrentShape?.MoveLeft(_gr, Field.Squares);
                        break;
                    case Keys.D:
                        Field.CurrentShape?.MoveRight(_gr, Field.Squares);
                        break;
                    case Keys.S:
                        Field.CurrentShape?.MoveDown(_gr, Field.Squares);
                        break;
                    case Keys.W:
                        Field.CurrentShape?.Rotate(_gr, Field.Squares);
                        break;
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку начала игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGameButton(object sender, EventArgs e)
        {
            Field = new GameField(new GameOptions
            {
                Width = Convert.ToInt32(WidthField.Text),
                Height = Convert.ToInt32(HeightField.Text),
                TimerWalkShapeMs = Convert.ToInt32(TimeField.Text)
            },
            _gr) ;

            Field.OnCompleteLine += Field_OnCompleteLine;
            Field.BuildField();
            Field.DrawField();

            this.KeyDown += Form1_KeyDown;

        }

        /// <summary>
        /// Делегат для выхода из потока
        /// </summary>
        /// <param name="text"></param>
        delegate void SetTextCallback(int text);

        /// <summary>
        /// Установка очков
        /// </summary>
        /// <param name="text"></param>
        private void SetScore(int text)
        {
            if (this.ScoresCount.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetScore);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                var score = Convert.ToInt32(ScoresCount.Text) + 100;
                ScoresCount.Text = score.ToString();

                var max = Convert.ToInt32(MaxScore.Text);

                if (max < score)
                {
                    MaxScore.Text = score.ToString();
                    File.WriteAllText("./Save.txt", score.ToString());
                }
            }
        }

        private void Field_OnCompleteLine(object? sender, EventArgs e)
        {
            SetScore(100);
        }
    }
}