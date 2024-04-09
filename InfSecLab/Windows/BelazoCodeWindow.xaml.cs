using InfSecLab.Ciphers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InfSecLab
{
    /// <summary>
    /// Логика взаимодействия для BelazoCodeWindow.xaml
    /// </summary>
    public partial class BelazoCodeWindow : Window
    {
        MainWindow mainWindow;
        SelectedCipher selectedCipher;
        public BelazoCodeWindow(MainWindow mainWindow, SelectedCipher selectedCipher)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.mainWindow = mainWindow;
            this.selectedCipher = selectedCipher;
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsLanguageSelected() && IsInputKeyTextBoxIntroducedCorrect())
            {
                EncryptMessage();
                EntropyInputMessageTextBox.Text = Convert.ToString(Math.Round(FindEntropy(OriginalMessageTextBox.Text), 2));
                EntropyEncryptedMessageTextBox.Text = Convert.ToString(Math.Round(FindEntropy(EncryptMessageTextBox.Text), 2));
            }
        }
        private void EncryptMessage()
        {
            string inputMessage = OriginalMessageTextBox.Text;
            string encryptedMessage = selectedCipher.EncryptMessage(inputMessage);
            EncryptMessageTextBox.Text = encryptedMessage;
        }
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsLanguageSelected() && IsInputKeyTextBoxIntroducedCorrect())
            {
                DecryptMessage();
                EntropyInputMessageTextBox.Text = Convert.ToString(Math.Round(FindEntropy(OriginalMessageTextBox.Text), 2));
                EntropyEncryptedMessageTextBox.Text = Convert.ToString(Math.Round(FindEntropy(EncryptMessageTextBox.Text), 2));
            }
        }
        private void DecryptMessage()
        {
            string inputMessage = EncryptMessageTextBox.Text;
            string encryptedMessage = selectedCipher.DecryptMessage(inputMessage);
            OriginalMessageTextBox.Text = encryptedMessage;
        }
        private bool IsLanguageSelected()
        {
            if (LanguageComboBox.SelectedIndex > -1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Выберите язык!");
                return false;
            }
        }
        private bool IsInputKeyTextBoxIntroducedCorrect()
        {
            if (IsInputKeyTextBoxEmpty())
            {
                MessageBox.Show("Введите ключ!");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool IsInputKeyTextBoxEmpty()
        {
            return InputKeyTextBox.Text == "";
        }
        private double FindEntropy(string message)
        {
            Dictionary<char, int> charCounts = new Dictionary<char, int>();
            int totalChars = message.Length;

            foreach (char c in message)
            {
                if (charCounts.ContainsKey(c))
                {
                    charCounts[c]++;
                }
                else
                {
                    charCounts[c] = 1;
                }
            }

            double entropy = 0;
            foreach (var count in charCounts.Values)
            {
                double probability = (double)count / totalChars;
                entropy -= probability * Math.Log(probability, 2);
            }

            return entropy;
        }
        private void InputKeyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-zа-яА-я]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            OriginalMessageTextBox.Text = string.Empty;
            EncryptMessageTextBox.Text = string.Empty;
            EntropyEncryptedMessageTextBox.Text = string.Empty;
            EntropyInputMessageTextBox.Text = string.Empty;
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Денис\Desktop\Учеба\3 курс 2 семестр\Информационная безопасность\InfSecLab\InfSecLab\Tests\";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                OriginalMessageTextBox.Text = WorkWithFile.ReadFile(openFileDialog.FileName);
        }
        private void WriteFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\Users\Денис\Desktop\Учеба\3 курс 2 семестр\Информационная безопасность\InfSecLab\InfSecLab\Tests\";
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
                WorkWithFile.WriteFile(saveFileDialog.FileName, EncryptMessageTextBox.Text);
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ChangeCipherSettings();
            }
            catch (InvalidInputKeyTextBoxException)
            {
                return;
            }
        }
        private void InputKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ChangeCipherSettings();
            }
            catch (InvalidInputKeyTextBoxException)
            {
                return;
            }
        }
        private void ChangeCipherSettings()
        {
            if (IsInputKeyTextBoxNotInitialized())
            {
                throw new InvalidInputKeyTextBoxException();
            }
            else if (IsInputKeyTextBoxEmpty())
            {
                throw new InvalidInputKeyTextBoxException();
            }
            string language = SelectLanguage();
            string key = InputKeyTextBox.Text;
            selectedCipher.SetCipherStrategy(new BelazoCipher(language, key));
        }
        private bool IsInputKeyTextBoxNotInitialized()
        {
            return InputKeyTextBox == null;
        }
        private string SelectLanguage()
        {
            TextBlock selectedLanguage = (TextBlock)LanguageComboBox.SelectedItem;
            return selectedLanguage.Name;
        }
    }
}
