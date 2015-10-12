using System;
using System.Collections.Generic;
using System.IO;

namespace LinearCryptanalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Random rand = new Random(); //Random number generator which will be used to randomize the key

            /*
            * Generate the round keys. For this SPN the keys bits are random and independent. 
            * However, we fix K5 such that [K(5,5)...K(5,8) , K(5,13)...K(5,16)] = [3,1]
            */
            List<BitString16> keys = new List<BitString16>(); //Temporary storage container for the keys
            keys.Add(new BitString16(rand)); //Round 1 key (random)
            keys.Add(new BitString16(rand)); //Round 2 key (random)
            keys.Add(new BitString16(rand)); //Round 3 key (random)
            keys.Add(new BitString16(rand)); //Round 4 key (random)
            keys.Add(new BitString16("0000001100000001")); //Round 5 key (fixed)
            Key key = new Key(keys);

            //Create an instance of SBox which will be used by the SPN
            SBox sbox = new SBox();

            Dictionary<String, String> plain_cipher_pairs = new Dictionary<string, string>();   //Plaintext/Ciphertext pairs
            StreamWriter writer = new StreamWriter("pairs.txt");                               //Text writer

            //Generate 10,000 Plaintext/Ciphertext pairs
            while (plain_cipher_pairs.Keys.Count < 10000)
            {
                BitString16 message = new BitString16(rand); //Generate a random message to encrypt

                //Check if we have already encrypted this plaintext. If so, skip this iteration of the loop
                //Without this check we may end up with duplicate plain/ciphertext pairs
                if (plain_cipher_pairs.ContainsKey(message.BitString))
                    continue;

                //Encrypt the message via the four round SPN
                BitString16 encrypt_message = FourRoundSPN.Encrypt(message, key);

                //Extract the plaintext/ciphertext strings from the objects
                String plaintext = message.BitString;
                String ciphertext = encrypt_message.BitString;
                
                //Write the pair to the textfile and store in the dictionary for further processing
                writer.WriteLine("{0},{1}", plaintext, ciphertext);
                plain_cipher_pairs.Add(plaintext, ciphertext);
                         
            }
            writer.Close();

            //Using only the plaintext/ciphertext pairs, perform linear cryptanalysis
            BitString16 r5BitString = LinearCryptanalysis.PerformLinearCryptanalysis(plain_cipher_pairs);
            String r5Key = r5BitString.BitString;

            String subkey1 = r5Key.Substring(4, 4);
            String subkey2 = r5Key.Substring(12, 4);

            System.Console.WriteLine("RESULT OF LINEAR CRYPTANALYSIS:\nK[5,5]-K[5,8] = {0}\nK[5,13]-K[5,16] = {1}", subkey1, subkey2);
        }
    }
}
