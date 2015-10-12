using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCryptanalysis
{
    class FourRoundSPN
    {

        //Encrypt a message under a key using the 4 round SPN
        public static BitString16 Encrypt(BitString16 message, Key key)
        {
            SBox sbox = new SBox();

            //Rounds 1-3
            for (int round = 1; round < 4; ++round)
            {
                BitString16 subkey = key.getEncryptionSubKey(round);
                message = Util.xor(message, subkey);
                message = sbox.forwardTransform(message);
                message = Permutation.forwardPermutation(message);
            }

            //Round 4
            BitString16 subkey_4 = key.getEncryptionSubKey(4);
            message = Util.xor(message, subkey_4);
            message = sbox.forwardTransform(message);

            //Post
            BitString16 last_subkey = key.getEncryptionSubKey(5);
            message = Util.xor(message, last_subkey);

            return message;
        }

        //Decrypt a ciphertext using a key and the 4 round SPN
        public static BitString16 Decrypt(BitString16 ciphertext, Key key)
        {
            SBox sbox = new SBox();

            //Pre
            BitString16 first_subkey = key.getDecryptionSubKey(1);
            BitString16 plaintext = Util.xor(ciphertext, first_subkey);

            //Round 1
            BitString16 second_subkey = key.getDecryptionSubKey(2);
            plaintext = sbox.backwardsTransform(plaintext);
            plaintext = Util.xor(plaintext, second_subkey);

            //Rounds 2-4
            for (int round = 2; round < 5; ++round)
            {
                BitString16 subkey = key.getDecryptionSubKey(round + 1);
                plaintext = Permutation.backwardsPermutation(plaintext);
                plaintext = sbox.backwardsTransform(plaintext);
                plaintext = Util.xor(plaintext, subkey);
            }

            return plaintext;
        }
    }
}
