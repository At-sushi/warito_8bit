using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace warito8bit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.FileName = textBox1.Text;
            open.Filter = "対応しているであろう画像ファイル(*.bmp;*.png;*.jpg;*.jpeg)|*.bmp;*.png;*.jpg;*.jpeg|すべてのファイル(*.*)|*.*";
            open.RestoreDirectory = true;

            if (open.ShowDialog() == DialogResult.OK)
                textBox1.Text = open.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();

            open.FileName = (textBox2.Text == "") ? textBox1.Text : textBox2.Text;
            open.Filter = "対応しているであろう画像ファイル(*.bmp;*.png)|*.bmp;*.png|すべてのファイル(*.*)|*.*";
            open.RestoreDirectory = true;

            if (open.ShowDialog() == DialogResult.OK)
                textBox2.Text = open.FileName;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor preCursor = Cursor.Current;

            Cursor.Current = Cursors.WaitCursor; 
            if (gazo.GazoController.Process(textBox1.Text, textBox2.Text))
                MessageBox.Show("処理が終了しました");

            // カーソルを元に戻す
            Cursor.Current = preCursor;
        }
    }
}
