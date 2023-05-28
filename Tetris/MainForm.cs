using System.Security.Cryptography;
using System.Text;
using System.Timers;
using Tetris.Models;
using Tetris.Options;
using static System.Formats.Asn1.AsnWriter;
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

        /// <summary>
        /// Блокировка
        /// </summary>
        private static object _locker = new object();

        /// <summary>
        /// Ключ для шифровки/дешифровки
        /// </summary>
        byte[] key = new byte[] { 45, 98, 156, 22, 33, 11, 16, 7, 96, 201, 18, 29, 77, 44, 102, 133 };

        byte[] iv = new byte[] { 4, 15, 56, 87, 15, 26, 97, 48, 21, 35, 7, 92, 17, 28, 39, 45 };


        public MainForm()
        {
            InitializeComponent();
            _gr = this.CreateGraphics();

            KeyPreview = true;


            if (File.Exists("./Save"))
            {
                var fileInfo = File.ReadAllText("./Save");
                var info = CryptoService.AesDecrypt(fileInfo, key, iv);

                MaxScore.Text = info;
            }
        }
        

        /// <summary>
        /// Обработчик нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_KeyDown(object sender, KeyEventArgs e)
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

            this.KeyDown += mainForm_KeyDown;

        }

        /// <summary>
        /// Делегат для выхода из потока
        /// </summary>
        /// <param name="text"></param>
        delegate void SetTextCallback(int score);

        /// <summary>
        /// Установка очков
        /// </summary>
        /// <param name="score"></param>
        private void SetScore(int score)
        {
            if (this.ScoresCount.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetScore);
                this.Invoke(d, new object[] { score });
            }
            else
            {
                var scores = Convert.ToInt32(ScoresCount.Text) + 100;
                ScoresCount.Text = scores.ToString();
                var max = Convert.ToInt32(MaxScore.Text);

                if (max < scores)
                {
                    MaxScore.Text = scores.ToString();
                    File.WriteAllText("./Save", CryptoService.AesEncrypt(scores.ToString(), key, iv));
                }
            }
        }

        /// <summary>
        /// Обработчик заполнения линий игрового поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Field_OnCompleteLine(object? sender, EventArgs e)
        {
            SetScore(100);
        }
    }
}