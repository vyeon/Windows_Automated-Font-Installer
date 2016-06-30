using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FontInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = Directory.GetCurrentDirectory() + "\\fonts";
            try
            {
                var list = Directory.GetFiles(dirPath);
                foreach (string s in list)
                {
                    if (Path.GetExtension(s) != ".ttf")
                    {
                        Console.WriteLine("Err> " + s + " is not a font file!");
                        continue;
                    }
                    PrivateFontCollection fontCol = new PrivateFontCollection();
                    fontCol.AddFontFile(s);
                    Console.WriteLine("Install> " + fontCol.Families[0].Name);
                    File.Copy(s,
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts", Path.GetFileName(s)), true);
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
                    key.SetValue(fontCol.Families[0].Name, Path.GetFileName(s));
                    key.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error!\n" + e.ToString());
                return;
            }
            MessageBox.Show("Success!");
            return;
        }
    }
}
