using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PicOperations
{
    static class Program
    {
        public static string TargetDirectory;
        static Form1 MainForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Form1();
            Application.Run(MainForm);

        }

        public static void ChoosePath()
        {
            TargetDirectory = Environment.CurrentDirectory;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = TargetDirectory;
            DialogResult result = fbd.ShowDialog();
            TargetDirectory = fbd.SelectedPath;
            MainForm.Dir = TargetDirectory;

            //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
        }

        public static void Rename()
        {
            string dirname = "";
            if (MainForm.DirName)
            {
                dirname = TargetDirectory.Substring(TargetDirectory.LastIndexOf("\\") + 1);
            }

            if (MainForm.SubDirs){
                dirname = "";
                string[] folders = GetFolders(TargetDirectory);
                for (int i = 0; i < folders.Length; i++)
                {
                    if (MainForm.DirName)
                    {
                        dirname = folders[i].Substring(TargetDirectory.LastIndexOf("\\") + 1);
                    }
                    RenameFiles(dirname, MainForm.StartValue, folders[i]);
                }
            }
            else
            {
                RenameFiles(dirname, MainForm.StartValue, TargetDirectory);
            }
            System.Windows.Forms.MessageBox.Show("Done", "Success");
        }

        static string[] GetFolders(String path)
        {
            return Directory.GetDirectories(path);
        }

        static void RenameFiles(string pre, decimal start , string dir)
        {
            DirectoryInfo info = new DirectoryInfo(dir);
            FileInfo[] files = info.GetFiles().OrderBy(f => f.LastWriteTime).ToArray();
           
            decimal j;
            System.IO.Directory.CreateDirectory(dir + "\\renamed");
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                j = i + start;
                file.CopyTo(file.Directory.FullName + "\\renamed\\" + pre + j + file.Extension, true);
            }
        }
    }

}
