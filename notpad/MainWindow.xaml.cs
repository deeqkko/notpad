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
            InitializeComponent();
        }

        
        int lineCount;
        string documentFilename;
     
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
                Console.WriteLine("INFO: Line number " + i + "wrote.");
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
            OpenDialog.FileName = "Document";
            OpenDialog.DefaultExt = ".txt";
            OpenDialog.Filter = "Text Document |*.txt";

            bool? result = OpenDialog.ShowDialog();

                if (result == true)
                {
                    Paper.Clear();
                    documentFilename = OpenDialog.FileName;
                    ReadFromFile(documentFilename);
                }
            
        }

        private void MenuItem_Print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(Paper, "Tähän tiedostonimi");
            }
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_SaveAs(object sender, RoutedEventArgs e)
        {
            var SaveAsDialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "My TextDocument.txt",
                DefaultExt = ".txt",
                Filter = "Text Document |*.txt"
            };

            bool? result = SaveAsDialog.ShowDialog();

            if (result == true)
            {
                int lineCount = Paper.LineCount;
                documentFilename = SaveAsDialog.FileName;
                WriteToFile(documentFilename);
            }
        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            Paper.Clear();
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
    }
}
