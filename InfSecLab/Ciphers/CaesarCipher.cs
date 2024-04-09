using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfSecLab.Ciphers
{
    public class CaesarCipher : Cipher
    {
        public int _offsetRange;
        private int _alphabetCapacity;
        private UnicodeLanguagesRanges _unicodeLanguagesRanges;

        public CaesarCipher(UnicodeLanguagesRanges unicodeLanguagesRanges, int offsetRange)
        {
            _offsetRange = offsetRange;
            _unicodeLanguagesRanges = unicodeLanguagesRanges;
            _alphabetCapacity = unicodeLanguagesRanges.UpperCaseRanges[1] - unicodeLanguagesRanges.UpperCaseRanges[0] + 1;
        }
        public override string EncryptMessage(string inputMessage)
        {
            string encryptedMessage = string.Empty;
            for (int i = 0; i < inputMessage.Length; i++)
            {
                var letter = inputMessage[i];
                if (IsSymbolOfStringIsLetter(letter))
                {
                    if (IsSymbolUpper(letter))
                    {
                        encryptedMessage += (char)LetterEncryption(letter, _unicodeLanguagesRanges.UpperCaseRanges[0]);
                    }
                    else
                    {
                        encryptedMessage += (char)LetterEncryption(letter, _unicodeLanguagesRanges.LowerCaseRanges[0]);
                    }
                }
                else
                {
                    encryptedMessage += letter;
                }
            }

            return encryptedMessage;
        }

        public override string DecryptMessage(string encryptedMessage)
        {
            string decryptedMessage = string.Empty;

            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                var letter = encryptedMessage[i];
                if (IsSymbolOfStringIsLetter(letter))
                {
                    if (IsSymbolUpper(letter))
                    {
                        decryptedMessage += (char)LetterDecryption(letter, _unicodeLanguagesRanges.UpperCaseRanges[1]);
                    }
                    else
                    {
                        decryptedMessage += (char)LetterDecryption(letter, _unicodeLanguagesRanges.LowerCaseRanges[1]);
                    }
                }
                else
                {
                    decryptedMessage += letter;
                }

            }

            return decryptedMessage;
        }
        private bool IsSymbolOfStringIsLetter(char symbol)
        {
            return char.IsLetter(symbol);
        }
        private bool IsSymbolUpper(char symbol)
        {
            return char.IsUpper(symbol);
        }
        private int LetterEncryption(char letter, int unicodeAlphabetLeftBorder)
        {
            return unicodeAlphabetLeftBorder + (letter - unicodeAlphabetLeftBorder + _offsetRange) % _alphabetCapacity;
        }
        private int LetterDecryption(char letter, int unicodeAlphabetRightBorder)
        {
            return unicodeAlphabetRightBorder + (letter - unicodeAlphabetRightBorder - _offsetRange) % _alphabetCapacity;
        }
    }
}
