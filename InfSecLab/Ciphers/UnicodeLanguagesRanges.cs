using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfSecLab.Ciphers
{
    public class UnicodeLanguagesRanges
    {
        private int[] _upperCaseRanges;
        private int[] _lowerCaseRanges;

        public int[] UpperCaseRanges { get { return _upperCaseRanges; } }
        public int[] LowerCaseRanges { get { return _lowerCaseRanges; } }

        public UnicodeLanguagesRanges(int[] upperCaseRanges, int[] lowerCaseRanges)
        {
            _upperCaseRanges = upperCaseRanges;
            _lowerCaseRanges = lowerCaseRanges;
        }
    }
}
