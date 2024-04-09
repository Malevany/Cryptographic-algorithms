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
    /// Логика взаимодействия для CaesarCodeWindow.xaml
    /// </summary>
    public class CryptoAnalysis
    {
        public int offsetRange { get; set; } 
        public double chiSquarePirson { get; set; } 
        public string decodeMessage { get; set; }

        public CryptoAnalysis(int offsetRange, double chiSquarePirson, string decodeMessage)
        {
            this.offsetRange = offsetRange;
            this.chiSquarePirson = chiSquarePirson;
            this.decodeMessage = decodeMessage;

        }
    }
    public partial class CaesarCodeWindow : Window
    {
        private MainWindow mainWindow;
        private SelectedCipher selectedCipher;
        Dictionary<char, double> alphabetFrequency = new Dictionary<char, double>()
        {
            { 'а', 0.07998}, { 'б', 0.01592},
            { 'в', 0.04533}, { 'г', 0.01687},
            { 'д', 0.02977}, { 'е', 0.08483},
            { 'ж', 0.00940}, { 'з', 0.01641},
            { 'и', 0.07367}, { 'й', 0.01208},
            { 'к', 0.03486}, { 'л', 0.04343},
            { 'м', 0.03203}, { 'н', 0.06700},
            { 'о', 0.10983}, { 'п', 0.02804},
            { 'р', 0.04746}, { 'с', 0.05473},
            { 'т', 0.06318}, { 'у', 0.02615},
            { 'ф', 0.00267}, { 'х', 0.00966},
            { 'ц', 0.00486}, { 'ч', 0.01450},
            { 'ш', 0.00718}, { 'щ', 0.00361},
            { 'ъ', 0.00037}, { 'ы', 0.01898},
            { 'ь', 0.01735}, { 'э', 0.00331},
            { 'ю', 0.00639}, { 'я', 0.02001},
            
            { 'a', 0.0817}, { 'b', 0.0149},
            { 'c', 0.0278}, { 'd', 0.0425},
            { 'e', 0.1270}, { 'f', 0.0229}, 
            { 'g', 0.0202}, { 'h', 0.0609}, 
            { 'i', 0.0697}, { 'j', 0.0015}, 
            { 'k', 0.0077}, { 'l', 0.0403}, 
            { 'm', 0.0241}, { 'n', 0.0675}, 
            { 'o', 0.0751}, { 'p', 0.0193}, 
            { 'q', 0.0010}, { 'r', 0.0599}, 
            { 's', 0.0633}, { 't', 0.0906}, 
            { 'u', 0.0276}, { 'v', 0.0098}, 
            { 'w', 0.0236}, { 'x', 0.0015}, 
            { 'y', 0.0197}, { 'z', 0.0007}

        };
        private Dictionary<string, UnicodeLanguagesRanges> unicodeLanguage = new Dictionary<string, UnicodeLanguagesRanges>()
        {
            {"EnglishLanguage", new UnicodeLanguagesRanges(new int[] {65, 90}, new int[] {97, 122}) },
            {"RussianLanguage", new UnicodeLanguagesRanges(new int[] {1040, 1071}, new int[] {1072, 1103}) }
        };
        public CaesarCodeWindow(MainWindow mainWindow, SelectedCipher selectedCipher)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.mainWindow = mainWindow;
            this.selectedCipher = selectedCipher;
            InitializeComponent();
        }
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsOffsetRangeIntroducedCorrect() && IsLanguageSelected())
            {
                EncryptMessage();
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
            if (IsOffsetRangeIntroducedCorrect() && IsLanguageSelected())
            {
                DecryptMessage();
            }
        }
        private void DecryptMessage()
        {
            string inputMessage = EncryptMessageTextBox.Text;
            string encryptedMessage = selectedCipher.DecryptMessage(inputMessage);
            OriginalMessageTextBox.Text = encryptedMessage;
        }
        private bool IsOffsetRangeIntroducedCorrect()
        {
            if (IsOffsetRangeTextBoxEmpty())
            {
                MessageBox.Show("Введите длину смещения!");
                return false;
            }
            else if (IsOffsetRangeCorrect())
            {
                MessageBox.Show("Максимальная длина смещения - 50");
                return false;
            }
            else
                return true;
        }
        private bool IsOffsetRangeTextBoxEmpty()
        {
            return OffsetRangeTextBox.Text == "";
        }
        private bool IsOffsetRangeCorrect()
        {
            int maxOffsetRange = 50;
            return Convert.ToInt32(OffsetRangeTextBox.Text) > maxOffsetRange;
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
        private void CryptoAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsEncryptMessageTextBoxEmpty() && IsLanguageSelected())
            {
                List<CryptoAnalysis> cryptoAnalysisList = new List<CryptoAnalysis>();
                string encryptedMessage = EncryptMessageTextBox.Text;
                UnicodeLanguagesRanges unicodeLanguagesRanges = SelectLanguage();
                int alphabetPower = unicodeLanguagesRanges.LowerCaseRanges[1] - unicodeLanguagesRanges.LowerCaseRanges[0] + 1;
                for (int i = 1; i < alphabetPower; i++)
                {
                    selectedCipher.SetCipherStrategy(new CaesarCipher(unicodeLanguagesRanges, i));
                    string decryptedMessage = selectedCipher.DecryptMessage(encryptedMessage);
                    double chiSquare = CalculateChi(decryptedMessage);
                    cryptoAnalysisList.Add(new CryptoAnalysis(i, chiSquare, decryptedMessage));
                }
                CryptoAnalysisDataGrid.ItemsSource = cryptoAnalysisList;
            }
        }
        private bool IsEncryptMessageTextBoxEmpty()
        {
            return EncryptMessageTextBox.Text != string.Empty;
        }
        private double CalculateChi(string decodeMessage)
        {
            Dictionary<char, int> symbolCounts = new Dictionary<char, int>();
            decodeMessage = decodeMessage.ToLower();
            foreach (char symbol in decodeMessage)
            {
                if (!symbolCounts.ContainsKey(symbol))
                {
                    symbolCounts[symbol] = 0;
                }

                symbolCounts[symbol]++;
            }

            int totalLength = decodeMessage.Length;

            Dictionary<char, double> symbolProbabilities = new Dictionary<char, double>();
            foreach (KeyValuePair<char, int> symbolCount in symbolCounts)
            {
                symbolProbabilities[symbolCount.Key] = (double)symbolCount.Value / totalLength;
            }

            double chiSquare = 0;
            foreach (var item in alphabetFrequency)
            {
                foreach (var sym in symbolProbabilities)
                {
                    if (sym.Key == item.Key)
                    {
                        chiSquare += (Math.Pow((sym.Value - item.Value), 2)) / item.Value;
                    }
                }
            }
            return chiSquare;

        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                OriginalMessageTextBox.Text = WorkWithFile.ReadFile(openFileDialog.FileName);
        }
        private void WriteFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
                WorkWithFile.WriteFile(saveFileDialog.FileName, EncryptMessageTextBox.Text);
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            OriginalMessageTextBox.Text = string.Empty;
            EncryptMessageTextBox.Text = string.Empty;
        }
        private void OffsetLengthTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ChangeCipherSettings();
            }
            catch (InvalidOffsetRangeTextBoxException)
            {
                return;
            }
        }
        private void OffsetRangeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ChangeCipherSettings();
            }
            catch (InvalidOffsetRangeTextBoxException)
            {
                return;
            }
        }
        private void ChangeCipherSettings()
        {
            if (IsOffsetRangeTextBoxNotInitialized())
            {
                throw new InvalidOffsetRangeTextBoxException();
            }
            else if (IsOffsetRangeTextBoxEmpty())
            {
                throw new InvalidOffsetRangeTextBoxException();
            }
            UnicodeLanguagesRanges unicodeLanguagesRanges = SelectLanguage();
            int offsetRange = Convert.ToInt32(OffsetRangeTextBox.Text);
            selectedCipher.SetCipherStrategy(new CaesarCipher(unicodeLanguagesRanges, offsetRange));
                
        }
        private bool IsOffsetRangeTextBoxNotInitialized()
        {
            return OffsetRangeTextBox == null;
        }
        private UnicodeLanguagesRanges SelectLanguage()
        {
            TextBlock selectedLanguage = (TextBlock)LanguageComboBox.SelectedItem;
            return unicodeLanguage[selectedLanguage.Name];
        }
    }
}
