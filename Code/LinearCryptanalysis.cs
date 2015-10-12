using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCryptanalysis
{
    class LinearCryptanalysis
    {
        static BitString16 partial_decrypt(BitString16 ciphertext, BitString16 key)
        {
            SBox sbox = new SBox();

            //Pre
            BitString16 plaintext = Util.xor(ciphertext, key);

            //Round 1
            plaintext = sbox.backwardsTransform(plaintext);

            return plaintext;
        }

        static bool testPartialKey(BitString16 plaintext, BitString16 partialPlaintext)
        {
            String p = plaintext.BitString;
            String u = partialPlaintext.BitString;

            //Implementation of formula 5: U[4,6] XOR U[4,8] XOR U[4,14] XOR U[4,16] XOR P[5] XOR P[7] XOR P[8]
            char result = Util.bit_xor(u[5], Util.bit_xor(u[7], Util.bit_xor(u[13], Util.bit_xor(u[15], Util.bit_xor(p[4], Util.bit_xor(p[6], p[7]))))));
            if (result == '0')
                return true;
            return false;
        }

        private static List<BitString16> generateAllPartialSubkeys()
        {
            List<BitString16> partialSubKeys = new List<BitString16>();

            //Generate the binary representation of all numbers from 0-16 (i.e. 0000-1111)
            List<String> partials = new List<string>();
            for (int i = 0; i < 16; ++i)
            {
                var result = Convert.ToString(i, 2);

                while (result.Length < 4)
                    result = "0" + result;
                partials.Add(result);
            }
            
            //Generate the 256 possible subkeys. These keys satisfy 0000[000-1111]0000[0000-1111]
            foreach (string s in partials)
                foreach (string t in partials)
                    partialSubKeys.Add(new BitString16("0000" + s + "0000" + t));

            return partialSubKeys;
        }

        public static BitString16 PerformLinearCryptanalysis(Dictionary<String, String> plain_cipher_pairs)
        {
            //Get the list of 256 possible subkeys
            List<BitString16> partialSubKeys = generateAllPartialSubkeys();

            double highestBias = 0; //Keep track of the highest bias
            String subkey = "";     //Keep track of the subkey with the highest bias


            //Iterate through each possible subkey and check which has the highest bias, this is the actual subkey.
            foreach (BitString16 partialSubKey in partialSubKeys)
            {
                int count = 0;

                foreach (var x in plain_cipher_pairs)
                {
                    BitString16 plaintext = new BitString16(x.Key);
                    BitString16 ciphertext = new BitString16(x.Value);
                    BitString16 partial_decrypted = partial_decrypt(ciphertext, partialSubKey);

                    //Check if the partially decrypted ciphertext satisfies equation 5
                    if (testPartialKey(plaintext, partial_decrypted))
                        count++;
                }
                double bias = Math.Abs(count - 5000.0) / 10000.0;   //Get this key's bias
                String _subkey = partialSubKey.BitString;      //Get this subkey

                System.Console.WriteLine("KEY: {1}, BIAS: {0}", bias, _subkey);

                //If this key has the highest bias so far, store it
                if (bias > highestBias)
                {
                    highestBias = bias;
                    subkey = _subkey;
                }
            }

            //Return the best subkey we found
            return new BitString16(subkey);
        }
    }
}
