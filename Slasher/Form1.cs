using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyExploits;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace Slasher
{
    public partial class Form1 : Form
    {
        Module easyexploits = new Module();
        public Form1()
        {
            InitializeComponent();
            WebClient wc = new WebClient
            {
                Proxy = null
            };
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                string friendlyName = AppDomain.CurrentDomain.FriendlyName;
                bool flag2 = registryKey.GetValue(friendlyName) == null;
                if (flag2)
                {
                    registryKey.SetValue(friendlyName, 11001, RegistryValueKind.DWord);
                }
                registryKey = null;
                friendlyName = null;
            }
            catch (Exception)
            {
            }
            webBrowser1.Url = new Uri(string.Format("file:///{0}/Monaco/Monaco.html", Directory.GetCurrentDirectory()));

            webBrowser1.Document.InvokeScript("SetTheme", new string[]
            {
                   "Dark"
                   /*
                    There are 2 Themes Dark and Light
                   */
            });
            addBase();
            addMath();
            addGlobalNS();
            addGlobalV();
            addGlobalF();
            webBrowser1.Document.InvokeScript("SetText", new object[]
            {
                 "-- Text here"

            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlDocument text = webBrowser1.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = text.InvokeScript(scriptName, args);
            string script = obj.ToString();
            easyexploits.ExecuteScript(script);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("SetText", new object[]
         {
                ""
         });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            easyexploits.LaunchExploit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    webBrowser1.Document.InvokeScript("SetText", new object[]
                    {
                          MainText
                    });

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo leer el archivo del disco. Error original: " + ex.Message);
                    Console.WriteLine("No se pudo abrir el archivo");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
            private string defPath = Application.StartupPath + "//Monaco//"; // some varibles

        private void addIntel(string label, string kind, string detail, string insertText)
        {
            string text = "\"" + label + "\"";
            string text2 = "\"" + kind + "\"";
            string text3 = "\"" + detail + "\"";
            string text4 = "\"" + insertText + "\"";
            webBrowser1.Document.InvokeScript("AddIntellisense", new object[] // some monaco shit
            {
                label,
                kind,
                detail,
                insertText
            });
        }
        // Credits to Main_EX for this!
        private void addGlobalF()
        {
            string[] array = File.ReadAllLines(defPath + "//globalf.txt");
            foreach (string text in array)
            {
                bool flag = text.Contains(':');
                if (flag)
                {
                    addIntel(text, "Function", text, text.Substring(1));
                }
                else
                {
                    addIntel(text, "Function", text, text);
                }
            }
        }

        private void addGlobalV()
        {
            foreach (string text in File.ReadLines(defPath + "//globalv.txt"))
            {
                addIntel(text, "Variable", text, text);
            }
        }

        private void addGlobalNS()
        {
            foreach (string text in File.ReadLines(defPath + "//globalns.txt"))
            {
                addIntel(text, "Class", text, text);
            }
        }

        private void addMath()
        {
            foreach (string text in File.ReadLines(defPath + "//classfunc.txt"))
            {
                addIntel(text, "Method", text, text);
            }
        }

        private void addBase()
        {
            foreach (string text in File.ReadLines(defPath + "//base.txt"))
            {
                addIntel(text, "Keyword", text, text);
            }

        }
    }
}
