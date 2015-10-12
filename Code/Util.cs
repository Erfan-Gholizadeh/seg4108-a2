using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCryptanalysis
{
    /* Contains helper logic used in the linear cryptanalysis */
    class Util
    {
        public static BitString16 xor(BitString16 bs1, BitString16 bs2)
        {
            String output = "";

            String s1 = bs1.BitString;
            String s2 = bs2.BitString;

            for (int i = 0; i < s1.Length; ++i)
            {
                if (s1[i] != s2[i])
                    output += '1';
                else
                    output += '0';
            }

            return new BitString16(output);
        }

        public static char bit_xor(char a, char b)
        {
            if (!a.Equals(b))
                return '1';
            return '0';
        }
    }
}
