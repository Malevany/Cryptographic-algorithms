using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfSecLab.Ciphers
{
    internal class BelazoCipher : ICipher
    {
        private string language;
        private Dictionary<string, char[]> alphabets = new Dictionary<string, char[]>
        {
            {"RussianLanguage", new char[]{ 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з',
                                            'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п',
                                            'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч',
                                            'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' } 
            },
            {"EnglishLanguage", new char[]{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
                                            'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
                                            'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
                                            'y', 'z'} 
            }
        };
        public List<string[]> TrithemiusTable { get { return new List<string[]>(); } }
        private List<string[]> trithemiusTable = new List<string[]>();
        private string key;
        private int[] keyCode;

        public BelazoCipher(string language, string key)
        {
            this.language = language;
            this.key = key;
            GenerateTrithemiusTable();
            GenerateKeyCode();
        }
        private void GenerateTrithemiusTable()
        {
            for (int i = 0; i < alphabets[language].Length; i++)
            {
                int h = i;
                string[] tempArray = new string[alphabets[language].Length];
                for (int j = 0; j < alphabets[language].Length; j++)
                {
                    tempArray[j] = alphabets[language][h].ToString();
                    h++;
                    if (h == tempArray.Length)
                        h = 0;
                }
                trithemiusTable.Add(tempArray);
            }
        }
        private void GenerateKeyCode()
        {
            keyCode = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < trithemiusTable[0].Length; j++)
                {
                    if (trithemiusTable[0][j] == key[i].ToString())
                    {
                        keyCode[i] = j;
                        break;
                    }
                }
            }
        }
        public void OutputTrithemiusTable()
        {
            foreach (string[] tempArray in trithemiusTable)
            {
                for (int i = 0; i < tempArray.Length; i++)
                {
                    Console.Write(" {0}", tempArray[i]);
                }
                Console.WriteLine();
            }
        }
        public string EncryptMessage(string inputMessage)
        {
            string encryptedMessage = string.Empty;
            int[] inputMessageCode = new int[inputMessage.Length];

            for (int i = 0; i < inputMessage.Length; i++)
            {
                for (int j = 0; j < trithemiusTable[0].Length; j++)
                {
                    if (IsMessageLeterFound(j, inputMessage[i].ToString()))
                    {
                        inputMessageCode[i] = j;
                        break;
                    }
                    else
                    {
                        inputMessageCode[i] = -1;
                    }
                }
            }
            int h = 0;
            for (int i = 0; i < inputMessageCode.Length; i++)
            {
                if (inputMessageCode[i] == -1)
                {
                    encryptedMessage += inputMessage[i];
                }
                else
                {
                    encryptedMessage += trithemiusTable[keyCode[h]][inputMessageCode[i]];
                    h++;
                }
                if (h == keyCode.Length)
                {
                    h = 0;
                }
            }
            return encryptedMessage;
        }
        private bool IsMessageLeterFound(int index, string letter)
        {
            return trithemiusTable[0][index] == letter;
        }
        public string DecryptMessage(string inputMessage)
        {
            string decryptedMessage = string.Empty;
            int[] messageCode = new int[inputMessage.Length];
            int h = 0;
            for (int i = 0; i < inputMessage.Length; i++)
            {
                for (int j = 0; j < trithemiusTable[0].Length; j++)
                {
                    if (trithemiusTable[keyCode[h]][j] == inputMessage[i].ToString())
                    {
                        messageCode[i] = j;
                        break;
                    }
                    else
                    {
                        messageCode[i] = -1;
                    }
                }
                h++;
                if (h == keyCode.Length)
                {
                    h = 0;
                }
            }
            for (int i = 0; i < messageCode.Length; i++)
            {
                if (messageCode[i] == -1)
                {
                    decryptedMessage += inputMessage[i];
                }
                decryptedMessage += trithemiusTable[0][messageCode[i]];
            }
            return decryptedMessage;
        }

    }
}
