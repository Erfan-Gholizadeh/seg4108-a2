using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCryptanalysis
{
    class BitString16
    {
        private string m_bitString;

        public String BitString
        {
            get { return m_bitString; }
        }

        //Verify that the content of the string is exactly 16 bits
        private void validateBitString(String bitString)
        {
            if (bitString.Length != 16)
                throw new System.ArgumentException("BitString must be of length 64");

            foreach (Char c in bitString)
            {
                if (c != '0' && c != '1')
                    throw new System.ArgumentException("BitString must contain only 0s and 1s");
            }
        }

        //Generate BitString16 with specified contents
        public BitString16(String bitString)
        {
            validateBitString(bitString);
            m_bitString = bitString;

        }

        //Generate random BitString64
        public BitString16(Random rand)
        {
            String s = "";

            for (int i = 0; i < 16; ++i)
            {
                if (rand.Next(0, 2) == 0)
                    s += '0';
                else
                    s += '1';

            }
            m_bitString = s;
        }
    }
}
