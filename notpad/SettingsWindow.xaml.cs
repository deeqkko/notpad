using System;
using System.Windows;
using System.Windows.Controls;

namespace notpad
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public string languageSelection;
        public SettingsWindow()
        {
            InitializeComponent();
            Lang.Items.Add(notpad.Properties.Resources.CB_Item_English);
            Lang.Items.Add(notpad.Properties.Resources.CB_Item_Swedish);
            Lang.SelectedIndex = Properties.Settings.Default.languageSelected;
        }

        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(Lang.SelectedIndex) == 1)
            {
                languageSelection = "sv-SE";
            } else
            {
                languageSelection = "en";
            }
        }

        private void Button_OK(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).languageSetting = languageSelection;
            Properties.Settings.Default.languageSelected = Lang.SelectedIndex;
            DialogResult = true;
            Close();
        }
    }
}
