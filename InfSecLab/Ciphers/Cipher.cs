using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfSecLab.Ciphers
{
    public abstract class Cipher : ICipher
    {
        public abstract string EncryptMessage(string inputMessage);
        public abstract string DecryptMessage(string inputMessage);
    }
}
