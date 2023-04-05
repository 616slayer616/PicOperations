using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace PicOperations
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 
        public static string TargetDirectory;
        static Form1 MainForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Form1();
            Application.Run(MainForm);

            TargetDirectory = Environment.CurrentDirectory;
        }

        public static void ChoosePath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
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
            string[] files = Directory.GetFiles(dir);
            Array.Sort(files, new AlphanumComparatorFast());

            decimal j;
            System.IO.Directory.CreateDirectory(dir + "\\renamed");
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = new FileInfo(files[i]);
                j = i + start;
                file.CopyTo(file.Directory.FullName + "\\renamed\\" + pre + j + file.Extension, true);
                //Console.WriteLine(files[i]);
            }
        }

        public class AlphanumComparatorFast : IComparer
        {
            public int Compare(object x, object y)
            {
                string s1 = x as string;
                if (s1 == null)
                {
                    return 0;
                }
                string s2 = y as string;
                if (s2 == null)
                {
                    return 0;
                }

                int len1 = s1.Length;
                int len2 = s2.Length;
                int marker1 = 0;
                int marker2 = 0;

                // Walk through two the strings with two markers.
                while (marker1 < len1 && marker2 < len2)
                {
                    char ch1 = s1[marker1];
                    char ch2 = s2[marker2];

                    // Some buffers we can build up characters in for each chunk.
                    char[] space1 = new char[len1];
                    int loc1 = 0;
                    char[] space2 = new char[len2];
                    int loc2 = 0;

                    // Walk through all following characters that are digits or
                    // characters in BOTH strings starting at the appropriate marker.
                    // Collect char arrays.
                    do
                    {
                        space1[loc1++] = ch1;
                        marker1++;

                        if (marker1 < len1)
                        {
                            ch1 = s1[marker1];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                    do
                    {
                        space2[loc2++] = ch2;
                        marker2++;

                        if (marker2 < len2)
                        {
                            ch2 = s2[marker2];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                    // If we have collected numbers, compare them numerically.
                    // Otherwise, if we have strings, compare them alphabetically.
                    string str1 = new string(space1);
                    string str2 = new string(space2);

                    int result;

                    if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                    {
                        int thisNumericChunk = int.Parse(str1);
                        int thatNumericChunk = int.Parse(str2);
                        result = thisNumericChunk.CompareTo(thatNumericChunk);
                    }
                    else
                    {
                        result = str1.CompareTo(str2);
                    }

                    if (result != 0)
                    {
                        return result;
                    }
                }
                return len1 - len2;
            }
        }

    }

}
