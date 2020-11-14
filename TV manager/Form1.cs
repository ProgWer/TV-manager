using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;

namespace TV_manager
{
    public partial class Form1 : Form
    {
        // делегат для манипулирования лейбом  
        public delegate void DelegateForTime(Label label);
        // поле типа делегата  
        DelegateForTime DelTime;
        int sec = 0; string test = "";
        // поток  
        Thread t1, t2;
        public Form1()
        {
            InitializeComponent();
            DelTime = new DelegateForTime(StartTime);// указываем метод делегату  
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;//см. текст ниже.
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            pictureBox1.Image = Image.FromFile(@"C:\6-23.jpg");


            /*---------------------------------------------------------------------*/

            t1 = new Thread(LabelTime); // создаем поток  
            t1.IsBackground = true; // задаем фоновый режым  
            t1.Priority = ThreadPriority.Lowest; // указываем свмый низкий приоритет  
            t1.Start(); // стартуем  
            t2 = new Thread(Pict);
            t2.IsBackground = true; // задаем фоновый режым  
            t2.Priority = ThreadPriority.Lowest; // указываем свмый низкий приоритет  
        }
        void Pict()
        {
            while (true)
            {
                if (sec == 15)
                {
                    test = "hi"; sec = 0;
                    pictureBox1.Image = Image.FromFile(@"C:\7864.jpg");
                }
            }
        }
        void StartTime(Label label)
        {
            // выводим всегда две цыфры   
            // (00:00)  
            string s = DateTime.Now.Hour.ToString("00");
            s += " : ";
            s += DateTime.Now.Minute.ToString("00");

            s += " : " + DateTime.Now.Second.ToString("00");
            sec += 1;
            label.Text = s;
        }
        void LabelTime()
        {
            // безконечный цыкл  
            while (true)
            {
                Invoke(DelTime, label1);// запускаем метод с главного потока          
            }
        }
    }
}
