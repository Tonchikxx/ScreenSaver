using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class MainForm : Form
    {
        private readonly List<Snowflake> snowflakes = new List<Snowflake>();
        private readonly Random random = new Random();
        private readonly Image snowflakeImage = Properties.Resources.snow;
        private readonly System.Windows.Forms.Timer timer;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 100;
        }

        /// <summary>
        /// Создание снежинок
        /// </summary>
        private void CreateSnowflakes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = random.Next(0, this.Width),
                    Y = random.Next(-this.Height, 0),
                    Size = random.Next(10, 40),
                    Speed = random.Next(2, 8)
                });
            }
        }

        /// <summary>
        /// Перемещение снежинок
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var snowflake in snowflakes)
            {
                snowflake.Y += snowflake.Speed;

                if (snowflake.Y > this.Height)
                {
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = random.Next(0, this.Width);
                }
            }
            this.Invalidate();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            CreateSnowflakes(120);

            timer.Start();
        }

        /// <summary>
        /// Отрисовка снежинок
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            foreach (var snowflake in snowflakes)
            {
                var rect = new Rectangle(
                    (int)snowflake.X,
                    (int)snowflake.Y,
                    snowflake.Size,
                    snowflake.Size
                );
                e.Graphics.DrawImage(snowflakeImage, rect);
            }
        }

    }
}