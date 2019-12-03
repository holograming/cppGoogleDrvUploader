using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BongSecurity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            // 최대,최소,간격을 임의로 조정
            progressBar2.Style = ProgressBarStyle.Continuous;
            progressBar2.Minimum = 0;
            progressBar2.Maximum = 100;
            progressBar2.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // timer.Start();

            var _source = new FileInfo("c:\\1_Image0.bmp");
            var _destination = new FileInfo("d:\\1_Image01.bmp");
            //Check if the file exists, we will delete it
            if (_destination.Exists)
                _destination.Delete();
            
            //Create a tast to run copy file
            Task.Run(() =>
            {
                _source.CopyTo(_destination, x => progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = x; label1.Text = x.ToString() + "%"; })));
            }).GetAwaiter().OnCompleted(() => progressBar1.BeginInvoke(new Action(() => 
            {
                progressBar1.Value = 100; label1.Text = "100%";
                //MessageBox.Show("You have successfully copied the file !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })));

        }

    }

    
}
