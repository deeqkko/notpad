using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace notpad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
            new System.Globalization.CultureInfo(notpad.Properties.Settings.Default.languageSetting);
            InitializeComponent();
            documentFilename = notpad.Properties.Resources.NewTextFile;
            Title = notpad.Properties.Resources.MainWIndowTitle+documentFilename;
        }


        int lineCount;
        string documentFilename;
        internal string languageSetting = notpad.Properties.Settings.Default.languageSetting;
        internal int selection;

        //Read and write operations
        private void WriteToFile(string documentFilename)
        {
            StreamWriter writer = File.CreateText(documentFilename);
            int i = 0;
            while (i < lineCount)
            {
                string lineText = Paper.GetLineText(i);
                lineText = lineText.TrimEnd();
                writer.WriteLine(lineText);
                i++;
                
            }
            writer.Close();
        }

        private void ReadFromFile(string documentFilename)
        {
            StreamReader reader = File.OpenText(documentFilename);
            string documentLine = reader.ReadLine();
            while (documentLine != null)
            {
                Paper.AppendText(documentLine + "\n");
                documentLine = reader.ReadLine();
            }
            reader.Close();
        }



        //Menu functions
        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog OpenDialog = new Microsoft.Win32.OpenFileDialog();
            OpenDialog.DefaultExt = ".txt";
            OpenDialog.Filter = notpad.Properties.Resources.FiletypeFilter;

            bool? result = OpenDialog.ShowDialog();

                if (result == true)
                {
                    Paper.Clear();
                    documentFilename = OpenDialog.FileName;
                    ReadFromFile(documentFilename);
                    Title = notpad.Properties.Resources.MainWIndowTitle+documentFilename;
                }
            
        }

        private void MenuItem_Print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(Paper, documentFilename);
            }
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_SaveAs(object sender, RoutedEventArgs e)
        {
            var SaveAsDialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = notpad.Properties.Resources.DefaultSaveFileName,
                DefaultExt = ".txt",
                Filter = notpad.Properties.Resources.FiletypeFilter
            };

            bool? result = SaveAsDialog.ShowDialog();

            if (result == true)
            {
                lineCount = Paper.LineCount;
                documentFilename = SaveAsDialog.FileName;
                WriteToFile(documentFilename);
                Title = notpad.Properties.Resources.MainWIndowTitle + documentFilename;
            }
        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            Paper.Clear();
            documentFilename = notpad.Properties.Resources.NewTextFile;
            Title = notpad.Properties.Resources.MainWIndowTitle + documentFilename;
        }

        private void MenuItem_Copy(object sender, EventArgs e)
        {
            if (Paper.SelectionLength > 0)
            {
                Paper.Copy();
            }
        }

        private void MenuItem_Paste(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {
                if (Paper.SelectionLength > 0)
                {
                    Paper.SelectionStart = Paper.SelectionStart + Paper.SelectionLength;
                }
                Paper.Paste();
            }
        }

        private void MenuItem_Cut(object sender, RoutedEventArgs e)
        {
            if (Paper.SelectionLength > 0)
            {
                Paper.Cut();
            }
        }

        private void MenuItem_Font(object sender, RoutedEventArgs e)
        {
            FontWindow ChangeFont = new FontWindow();

            if (ChangeFont.ShowDialog() == true)
            {
                Paper.FontSize = selection;
            }

            
            
        }

        private void MenuItem_Settings(object sender, RoutedEventArgs e)
        {
            SettingsWindow ChangeLanguae = new SettingsWindow();

            if (ChangeLanguae.ShowDialog() == true)
            {
                Properties.Settings.Default.languageSetting = languageSetting;
                
                MessageBox.Show(notpad.Properties.Resources.MessageBoxMsg, notpad.Properties.Resources.MessageBox_Header);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
            new System.Globalization.CultureInfo(languageSetting);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
