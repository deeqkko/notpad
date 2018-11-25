using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace notpad
{
    /// <summary>
    /// Interaction logic for FontWindow.xaml
    /// </summary>
    
    public partial class FontWindow : Window
    {
        public int selection;
        public FontWindow()
        {
            InitializeComponent();
            for (int i = 8; i < 36; i = i +2 )
            {
                Size.Items.Add(i);
            }
            Size.SelectedIndex = 4;
            Title = notpad.Properties.Resources.FontWindowTitle;
        }

        private void Button_OK(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).selection = selection;
            DialogResult = true;
            Close();
        }

        private void Size_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selection = Convert.ToInt32(Size.SelectedItem);
            
            //Console.WriteLine(selection);
        }

       
    }
}
