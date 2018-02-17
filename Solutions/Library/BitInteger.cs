using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions.Library
{
    public class BitInteger
    {
        public static int IntegerSize = 32;
        private bool[] bits;

        public BitInteger()
        {
            bits = new bool[IntegerSize];
        }

        /* Creates a number equal to given value. Takes time proportional 
         * to INTEGER_SIZE. */
        public BitInteger(int value)
        {
            bits = new bool[IntegerSize];

            for (var j = 0; j < IntegerSize; j++)
            {
                if (((value >> j) & 1) == 1)
                {
                    bits[IntegerSize - 1 - j] = true;
                }
                else
                {
                    bits[IntegerSize - 1 - j] = false;
                }
            }
        }

        /** Returns k-th most-significant bit. */
        public int Fetch(int k)
        {
            if (bits[k])
            {
                return 1;
            }

            return 0;
        }

        /** Sets k-th most-significant bit. */
        public void Set(int k, int bitValue)
        {
            if (bitValue == 0)
            {
                bits[k] = false;
            }
            else
            {
                bits[k] = true;
            }
        }

        /** Sets k-th most-significant bit. */
        public void Set(int k, char bitValue)
        {
            if (bitValue == '0')
            {
                bits[k] = false;
            }
            else
            {
                bits[k] = true;
            }
        }

        /** Sets k-th most-significant bit. */
        public void Set(int k, bool bitValue)
        {
            bits[k] = bitValue;
        }

        public void SwapValues(BitInteger number)
        {
            for (var i = 0; i < IntegerSize; i++)
            {
                var temp = number.Fetch(i);
                number.Set(i, Fetch(i));
                Set(i, temp);
            }
        }

        public int ToInt()
        {
            var number = 0;

            for (var j = IntegerSize - 1; j >= 0; j--)
            {
                number = number | Fetch(j);
                if (j > 0)
                {
                    number = number << 1;
                }
            }

            return number;
        }
    }
}
