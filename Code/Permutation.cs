using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCryptanalysis
{
    /*Performs the permutation step in the 4 round SPN */
    class Permutation
    {

        public static BitString16 forwardPermutation(BitString16 bitString)
        {
            String input = bitString.BitString;
            String output = "";


            output += input[1 - 1]; //1st bit
            output += input[5 - 1]; //2nd bit
            output += input[9 - 1];
            output += input[13 - 1];
            output += input[2 - 1];
            output += input[6 - 1];
            output += input[10 - 1];
            output += input[14 - 1];
            output += input[3 - 1];
            output += input[7 - 1];
            output += input[11 - 1];
            output += input[15 - 1];
            output += input[4 - 1];
            output += input[8 - 1];
            output += input[12 - 1];
            output += input[16 - 1];

            return new BitString16(output);
        }

        public static BitString16 backwardsPermutation(BitString16 bitString)
        {
            String input = bitString.BitString;
            String output = "";

            output += input[1 - 1]; //1st bit
            output += input[5 - 1]; //2nd bit
            output += input[9 - 1];
            output += input[13 - 1];
            output += input[2 - 1];
            output += input[6 - 1];
            output += input[10 - 1];
            output += input[14 - 1];
            output += input[3 - 1];
            output += input[7 - 1];
            output += input[11 - 1];
            output += input[15 - 1];
            output += input[4 - 1];
            output += input[8 - 1];
            output += input[12 - 1];
            output += input[16 - 1];

            return new BitString16(output);
        }
    }
}
