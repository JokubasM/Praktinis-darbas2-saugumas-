using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Praktinis_darbas_2_Saugumas_
{
    public partial class Form1 : Form
    {

        private DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        public Form1()
        {
            InitializeComponent();
           
        }
        byte[] encryptor; 
        private void button1_Click(object sender, EventArgs e)
        {
          
            UTF8Encoding uTF8 = new UTF8Encoding();
            des.Key = uTF8.GetBytes(textBox2.Text);
            des.Mode = CipherMode.ECB;
            ICryptoTransform transform = des.CreateEncryptor();
            encryptor = transform.TransformFinalBlock(uTF8.GetBytes(textBox1.Text), 0, uTF8.GetBytes(textBox1.Text).Length);
            textBox3.Text = BitConverter.ToString(encryptor);

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(textBox3.Text);

            sw.Close();
            fs.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            UTF8Encoding uTF8 = new UTF8Encoding();

            des.Key = uTF8.GetBytes(textBox2.Text);
            des.Mode = CipherMode.ECB;
            ICryptoTransform transform = des.CreateDecryptor();
            textBox5.Text = uTF8.GetString(transform.TransformFinalBlock(encryptor, 0, encryptor.Length));
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UTF8Encoding uTF8 = new UTF8Encoding();

            var random =  new Random();
            
            des.Key = uTF8.GetBytes(textBox8.Text);
            des.Mode = CipherMode.CBC;
            
            des.IV = uTF8.GetBytes(textBox9.Text);
            ICryptoTransform transform = des.CreateEncryptor();
            encryptor = transform.TransformFinalBlock(uTF8.GetBytes(textBox7.Text), 0 , uTF8.GetBytes(textBox7.Text).Length);
            textBox6.Text = BitConverter.ToString(encryptor);

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(textBox6.Text);

            sw.Close();
            fs.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
           

            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            textBox1.Text = sr.ReadToEnd();

            sr.Close();
            fs.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UTF8Encoding uTF8 = new UTF8Encoding();

            des.Key = uTF8.GetBytes(textBox8.Text);
            des.Mode = CipherMode.CBC;
            des.IV = uTF8.GetBytes(textBox9.Text);
            ICryptoTransform transform = des.CreateDecryptor();
            textBox4.Text = uTF8.GetString(transform.TransformFinalBlock(encryptor, 0, encryptor.Length));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;


            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            textBox7.Text = sr.ReadToEnd();

            sr.Close();
            fs.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
