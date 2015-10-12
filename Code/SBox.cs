using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCryptanalysis
{
    /*Contains the logic for the s-boxes in the four round SPN */
    class SBox
    {
        Dictionary<string, string> sBoxMapping = new Dictionary<string, string>();

        public SBox()
        {
            sBoxMapping.Add("0000", "1110"); //0->E
            sBoxMapping.Add("0001", "0100"); //1->4
            sBoxMapping.Add("0010", "1101"); //2->D
            sBoxMapping.Add("0011", "0001"); //3->1
            sBoxMapping.Add("0100", "0010"); //4->2
            sBoxMapping.Add("0101", "1111"); //5->F
            sBoxMapping.Add("0110", "1011"); //6->B
            sBoxMapping.Add("0111", "1000"); //7->8
            sBoxMapping.Add("1000", "0011"); //8->3
            sBoxMapping.Add("1001", "1010"); //9->A
            sBoxMapping.Add("1010", "0110"); //A->6
            sBoxMapping.Add("1011", "1100"); //B->C
            sBoxMapping.Add("1100", "0101"); //C->5
            sBoxMapping.Add("1101", "1001"); //D->9
            sBoxMapping.Add("1110", "0000"); //E->0
            sBoxMapping.Add("1111", "0111"); //F->7

        }

        public BitString16 forwardTransform(BitString16 bitString)
        {
            String input = bitString.BitString;
            String output = "";           

            for (int i = 0; i < 4; ++i)
            {
                String subBlock = input.Substring(i * 4, 4);
                output += sBoxMapping[subBlock];
       
            }
            return new BitString16(output);
        }

        public BitString16 backwardsTransform(BitString16 bitString)
        {
            String input = bitString.BitString;
            String output = "";

            for (int i = 0; i < 4; ++i)
            {
                String subBlock = input.Substring(i * 4, 4);
                output += sBoxMapping.First(x => x.Value.Equals(subBlock)).Key;

            }

            return new BitString16(output);
        }
    }
}
