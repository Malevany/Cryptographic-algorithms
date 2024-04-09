using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfSecLab.Ciphers
{
    public sealed class SelectedCipher
    {
        private ICipher _cipherStrategy;

        public void SetCipherStrategy(ICipher cipherStrategy)
        {
            _cipherStrategy = cipherStrategy;
        }
        public string EncryptMessage(string inputMessage)
        {
            return _cipherStrategy.EncryptMessage(inputMessage);
        }
        public string DecryptMessage(string inputMessage)
        {
            return _cipherStrategy.DecryptMessage(inputMessage);
        }
    }
}
