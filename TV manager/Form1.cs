using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TV_manager
{
    public partial class Form1 : Form
    {
        public delegate void DelegateForTime(Label label); // делегат для манипулирования лейбом  
        DelegateForTime DelTime; // поле типа делегата  
        int mass; List<string> list = new List<string>();
        
        public Form1()
        {
            InitializeComponent(); DelTime = new DelegateForTime(StartTime); // указываем метод делегату  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal; FormBorderStyle = FormBorderStyle.None; WindowState = FormWindowState.Maximized; // расширение окна

            string put = Environment.CurrentDirectory;
            FileStream file = new FileStream(@"" + put + "\\pictures", FileMode.Open); // открытие файла, от куда брать список пикч, создание файлового потока
            StreamReader reader = new StreamReader(file); // создание потоковый читатель и связывание его с файловым потоком
            int mas = 1;
            while (mas != 0)
            {
                string count = reader.ReadLine();
                if (count != null)
                { list.Add(count); mass++; } else mas = 0;
            }

            /*---------------------------------------------------------------------*/

            Thread t1 = new Thread(new ThreadStart(LabelTime)); // создаем поток
            t1.IsBackground = true; // задаем фоновый режым  
            t1.Priority = ThreadPriority.Lowest; // указываем свмый низкий приоритет  
            t1.Start(); // запускаем поток
            Thread t2 = new Thread(new ThreadStart(Pict)); t2.IsBackground = true; t2.Priority = ThreadPriority.Lowest; t2.Start();
        }

        void StartTime(Label label)
        { label1.Text = timer.Main; }

        void LabelTime()
        {
            while (true)
            { Invoke(DelTime, label1); }
        }

        void Pict()
        {
            int count = 0;
            while (true)
            {
                pictureBox1.Image = Image.FromFile(list[count]); count++;
                if (mass == count) count = 0;
                Thread.Sleep(15000);
            }
        }
    }
}
