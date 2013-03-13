using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace OCRDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog();
            filedlg.Multiselect = true;
            string imageFolder = Application.ExecutablePath;
            int pos = imageFolder.LastIndexOf("\\Samples\\");
            if (pos != -1)
            {
                imageFolder = imageFolder.Substring(0, imageFolder.IndexOf(@"\", pos)) + @"\Demos\OCRImages\";
            }
            filedlg.InitialDirectory = imageFolder;
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string strfilename in filedlg.FileNames)
                {
                    this.dynamicDotNetTwain1.LoadImage(strfilename);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string languageFolder = Application.ExecutablePath;
            int pos = languageFolder.LastIndexOf("\\Samples\\");
            if (pos != -1)
            {
                languageFolder = languageFolder.Substring(0, languageFolder.IndexOf(@"\", pos)) + @"\";
            }

            this.dynamicDotNetTwain1.OCRTessDataPath = languageFolder;
            this.dynamicDotNetTwain1.OCRLanguage = "eng";

            this.dynamicDotNetTwain1.OCRResultFormat = (Dynamsoft.DotNet.TWAIN.OCR.ResultFormat)this.ddlResultFormat.SelectedIndex;
            this.dynamicDotNetTwain1.OCRDllPath = "../../../../../../";

            byte[] sbytes = this.dynamicDotNetTwain1.OCR(this.dynamicDotNetTwain1.CurrentSelectedImageIndicesInBuffer);

            if(sbytes != null)
            {
                SaveFileDialog filedlg = new SaveFileDialog();
                if (this.ddlResultFormat.SelectedIndex != 0)
                {
                    filedlg.Filter = "PDF File(*.pdf)| *.pdf";
                }
                else
                {
                    filedlg.Filter = "Text File(*.txt)| *.txt";
                }
                if (filedlg.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = File.OpenWrite(filedlg.FileName);
                    fs.Write(sbytes, 0, sbytes.Length);
                    fs.Close();
                }
            }
            else
            {
                MessageBox.Show(this.dynamicDotNetTwain1.ErrorString);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dynamicDotNetTwain1.SetViewMode(2,2);
            this.dynamicDotNetTwain1.AllowMultiSelect = true;

            this.ddlResultFormat.Items.Add("Text");
            this.ddlResultFormat.Items.Add("PDF Plain Text");
            this.ddlResultFormat.Items.Add("PDF Image Over Text");
            this.ddlResultFormat.SelectedIndex = 0;

            string imageFolder = Application.ExecutablePath;
            int pos = imageFolder.LastIndexOf("\\Samples\\");
            if (pos != -1)
            {
                imageFolder = imageFolder.Substring(0, imageFolder.IndexOf(@"\", pos)) + @"\Demos\OCRImages\";
            }

            this.dynamicDotNetTwain1.LoadImage(imageFolder + @"\DNTImage1.tif");
            this.dynamicDotNetTwain1.LoadImage(imageFolder + @"\DNTImage2.tif");
            this.dynamicDotNetTwain1.LoadImage(imageFolder + @"\DNTImage3.tif");
            this.dynamicDotNetTwain1.LoadImage(imageFolder + @"\DNTImage4.tif");
            this.dynamicDotNetTwain1.LoadImage(imageFolder + @"\DNTImage5.tif");
            this.dynamicDotNetTwain1.LoadImage(imageFolder + @"\DNTImage6.tif");
            this.dynamicDotNetTwain1.LoadImage(imageFolder + @"\DNTImage7.tif");
        }

    }
}