using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCryptanalysis
{
    //Container for all the round keys in a SPN
    class Key
    {
        private List<BitString16> keyList = new List<BitString16>();

        /* Constructor for a Key set
         * Generates random keys for each round
         */
        public Key(int rounds, Random rand)
        {
            for (int i = 0; i < rounds+1; ++i)
            {
                keyList.Add(new BitString16(rand));
            }
        }

        /* Constructor for a key set where each key is specified */
        public Key(List<BitString16> keys)
        {
            keyList = keys;

            for(int i=0; i<keys.Count; ++i)
            {
                System.Console.WriteLine("Round {0} key: {1}", i + 1, keys[i].BitString);
            }
        }

        /* Gets the encryption subkey for a given round */
        public BitString16 getEncryptionSubKey(int round)
        {
            return keyList[round - 1];
        }

        /* Gets the decryption subkey for a given round. This returns the keys in the opposite order as getEncryptionSubKey */
        public BitString16 getDecryptionSubKey(int round)
        {
            return keyList[5 - round];
        }
    }
}
