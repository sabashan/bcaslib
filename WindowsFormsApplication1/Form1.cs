using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        JObject bookDetails=null;

        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string isbn = tbISBN.Text;
            laResult.Text = GET("https://www.googleapis.com/books/v1/volumes?q=isbn:"+isbn);

            bookDetails = JObject.Parse(laResult.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = (bookDetails["items"].First["volumeInfo"])["title"].ToString();
            string iamgePath="http://books.google.com/books/content?id=7plQAAAAMAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api";
            pictureBox2.ImageLocation = iamgePath;
        }
    }
}
