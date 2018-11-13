using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace SIMPLE_JASON
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnJson_Click(object sender, EventArgs e)
        {
            //return;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(txtUrl.Text);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string json = tXml.Trim();
            //    streamWriter.Write(json);
            //    streamWriter.Flush();
            //    streamWriter.Close();
            //}
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                if (chkDebug.Checked)
                {
                    if (File.Exists("DebugJson.txt") && chkDebug.Checked)
                    {
                        File.WriteAllText("DebugJson.txt", String.Empty);
                    }
                    if (File.Exists("DebugJson.txt") && chkDebug.Checked)
                    {
                        StreamWriter w = File.AppendText("DebugJson.txt");
                        w.WriteLine(result);
                        w.Flush();
                        w.Close();
                    }
                }
                if (chkTxt.Checked) txtJsonRes.Text = result;
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            DumpToElastic(txtUrl.Text, richTextBox1.Text, "POST");
        }
        private void DumpToElastic(string tUrl, string tXml, string method)
        {
            //return;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(tUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = tXml.Trim();
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                txtJsonRes.Text = result;
            }
        }

        private void btnPut_Click(object sender, EventArgs e)
        {
            DumpToElastic(txtUrl.Text, richTextBox1.Text, "PUT");
        }
    }
}
