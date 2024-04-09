using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfSecLab.Ciphers
{
    public interface ICipher
    {
        public string EncryptMessage(string inputMessage);
        public string DecryptMessage(string inputMessage);
    }
}
