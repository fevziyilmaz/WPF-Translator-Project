using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace WPF_Language_Translator
{
    public partial class MainWindow : Window
    {
        private Translator langTranslator = new Translator();
        private string textToTrans;
        private string fromLang;
        private string toLang;
        private string translation;

        public MainWindow()
        {
            InitializeComponent();
            FromLanguageCmbBox.ItemsSource = langTranslator.DillerListesi;
            ToLanguageCmbBox.ItemsSource = langTranslator.DillerListesi;
            FromLanguageCmbBox.SelectedIndex = 1;
            ToLanguageCmbBox.SelectedIndex = 2;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextToTranslateTxtBox.Text != string.Empty)
            {
                textToTrans = TextToTranslateTxtBox.Text;
                fromLang = ((Language)FromLanguageCmbBox.SelectedValue).dil_kod;
                toLang = ((Language)ToLanguageCmbBox.SelectedValue).dil_kod;

                TranslatedTextTxtBox.Clear();
                TranslateButton.IsEnabled = false;

                Thread td = new Thread(GetTranslation);
                td.Start();
            }
            else
            {
                 MessageBox.Show("Boş geçmeyiniz", "Translator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                 TextToTranslateTxtBox.Focus();
            }
        }

        private void GetTranslation()
        {
            translation = langTranslator.GetTranslatedText(textToTrans, fromLang, toLang);
            this.Dispatcher.BeginInvoke(new ThreadStart(ShowTranslatedText), DispatcherPriority.Normal, null);
        }

        private void ShowTranslatedText()
        {
            TranslatedTextTxtBox.Text = translation;
            TranslateButton.IsEnabled = true;
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextToTranslateTxtBox.Clear();
            TranslatedTextTxtBox.Clear();
            TextToTranslateTxtBox.Focus();
        }        
  
    }
}
