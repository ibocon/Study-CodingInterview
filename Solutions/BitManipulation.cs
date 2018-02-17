using Solutions.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    public class BitManipulation
    {
        #region Question 5.1
        /*
         * 두 개의 32비트 수 N과 M이 주어지고, 비트 위치 i와 j가 주어졌을 때, M을 N에 삽입하는 메서드를 구현하라.
         * M은 N의 j번째 비트에서 시작하여 i번째 비트에서 끝나야 한다. j번째 비트에서 시작하여 i번째 비트에서 끝나야 한다.
         * j번째 비트에서 i번째 비트까지에는 M을 담기 충분한 공간이 있다고 가정한다.
         */

        public static int Q1_InsertBits(int n, int m, int i, int j)
        {
            var left = ~0 << (j + 1);
            var right = ((1 << i) - 1);

            var mask = left | right;

            var nCleared = n & mask;
            var mShifted = m << i;

            return nCleared | mShifted;
        }
        #endregion

        #region Question 5.2
        /*
         * 0과 1 사이의 실수가 double 타입의 입력으로 주어졌을 때, 그 값을 이진수 형태로 출력하는 코드를 작성하라.
         * 길이가 32개 이하의 문자열로 출력될 수 없는 경우에는 ERROR를 대신 출력하라.
         */

        public static string Q2_PrintBinary(double number)
        {
            if (number >= 1 || number <= 0)
            {
                return @"ERROR";
            }

            var binary = new StringBuilder();
            binary.Append(@"0.");

            var frac = 0.5;

            while (number > 0)
            {
                /* Setting a limit on length: 32 characters */
                if (binary.Length >= 32)
                {
                    return "ERROR";
                }
                if (number >= frac)
                {
                    binary.Append(1);
                    number -= frac;
                }
                else
                {
                    binary.Append(0);
                }
                frac /= 2;
            }

            return binary.ToString();
        }
        #endregion

        #region Question 5.3
        /*
         * 양의 정수 x가 입력으로 주어진다고 하자. 
         * 이 정수를 이진수로 표현했을 때 1인 비트의 개수가 n이라고 하자.
         * 이진수로 표현했을 때 1인 비트 개수가 n인 다른 정수 중에서 
         * x보다 작은 것 중 가장 큰 정수와, x보다 큰 것 중 가장 작은 정수를 찾아라.
         */

        // 책의 해답에서 제시된 수학적으로 문제를 푸는 방법
        public static int Q3_GetNextArith(int number)
        {
            var c = number;
            var c0 = 0;
            var c1 = 0;

            while (((c & 1) == 0) && (c != 0))
            {
                c0++;
                c >>= 1;
            }

            while ((c & 1) == 1)
            {
                c1++;
                c >>= 1;
            }

            /* If c is 0, then n is a sequence of 1s followed by a sequence of 0s. This is already the biggest
             * number with c1 ones. Return error.
             */
            if (c0 + c1 == 31 || c0 + c1 == 0)
            {
                return -1;
            }

            /* Arithmetically:
             * 2^c0 = 1 << c0
             * 2^(c1-1) = 1 << (c0 - 1)
             * next = n + 2^c0 + 2^(c1-1) - 1;
             */

            return number + (1 << c0) + (1 << (c1 - 1)) - 1;
        }

        public static int Q3_GetPrevArith(int number)
        {
            var temp = number;
            var c0 = 0;
            var c1 = 0;

            while (((temp & 1) == 1) && (temp != 0))
            {
                c1++;
                temp >>= 1;
            }

            /* If temp is 0, then the number is a sequence of 0s followed by a sequence of 1s. This is already
             * the smallest number with c1 ones. Return -1 for an error.
             */
            if (temp == 0)
            {
                return -1;
            }

            while ((temp & 1) == 0 && (temp != 0))
            {
                c0++;
                temp >>= 1;
            }

            /* Arithmetic:
             * 2^c1 = 1 << c1
             * 2^(c0 - 1) = 1 << (c0 - 1)
             */
            return number - (1 << c1) - (1 << (c0 - 1)) + 1;
        }
        #endregion

        #region Question 5.4
        /*
         * 다음의 코드가 하는 일을 설명하라
         * ((n & (n-1)) == 0)
         */

        /*
         * n의 Most Significant Bit (MSB)만 1이고, 나머지는 0인 수인지 확인한다. 즉, n이 2^k인 수인지 확인한다.
         * n-1은 n의 비트에서 1만 제거한 수이므로 n의 비트와 유사한 형태를 가지게 된다.
         * 이때, n과 n-1을 & 연산할 경우 모든 비트가 엇갈려야만 0이 될 수 있다.
         * 그러므로 n-1이 n과 모든 비트가 엇갈리기 위해선 MSB만 1이고 나머지 자리는 0이 되어야 한다.
         */
        #endregion

        #region Question 5.5
        /*
         * 정수 A를 B로 변환하기 위해 바꿔야 하는 비트 개수를 계산하는 함수를 작성하라.
         */

        public static int Q5_BitSwapRequired(int a, int b)
        {
            int c = a ^ b;

            int count = 0;
            while(c != 0)
            {
                c &= c - 1;
                count++;
            }

            return count;
        }

        #endregion

        #region Question 5.6
        /*
         * 주어진 정수의 짝수 번째 비트의 값과 홀수 번째 비트의 값을 바꾸는 프로그램을 작성하라.
         * 가능한 한 적은 수의 명령어가 실행되도록 해야 한다.
         */

        public static int Q6_SwapOddEvenBits(int bits)
        {
            return (int)((bits & 0xAAAAAAAA) >> 1) | ((bits & 0x55555555) << 1);
        }

        #endregion

        #region Question 5.7
        /*
         * 배열 A에 0부터 n까지의 정수가 저장되어 있는데, 빠진 정수가 하나 있다.
         * 한 번의 연산으로 A의 모든 정수를 접근할 수는 없도록 제한되어 있다.
         * A의 모든 원소는 이진수 형태로 표현되며, 여러분이 할 수 있는 연산이라고는
         * "A[i]의 j번째 비트를 가져온다"는 것이 전부다. 이 연산 수행에는 상수 시간이 소요된다. 
         * 배열에 저장되지 않은 빠진 정수 하나를 찾는 코드를 작성하라.
         * O(n) 시간 안에 실행되도록 작성할 수 있겠는가?
         */

        public static int Q7_FindMissing(List<BitInteger> array)
        {
            return FindMissing(array, BitInteger.IntegerSize - 1);
        }

        private static int FindMissing(List<BitInteger> input, int column)
        {
            if (column < 0)
            {
                return 0;
            }

            var oneBits = new List<BitInteger>(input.Count / 2);
            var zeroBits = new List<BitInteger>(input.Count / 2);

            foreach (var bitInteger in input)
            {
                if (bitInteger.Fetch(column) == 0)
                {
                    zeroBits.Add(bitInteger);
                }
                else
                {
                    oneBits.Add(bitInteger);
                }
            }

            // 짝수일 경우
            if (zeroBits.Count <= oneBits.Count)
            {
                var v = FindMissing(zeroBits, column - 1);
                return (v << 1) | 0;
            }
            // 홀수일 경우
            else
            {
                var v = FindMissing(oneBits, column - 1);
                return (v << 1) | 1;
            }
        }

        #endregion

        #region Question 5.8
        /*
         * monochrome 모니터 화면을 하나의 바이트 배열에 저장한다고 하자.
         * 이때 인접한 픽셀 여덟 개를 한 바이트로 저장한다. 화면 폭은 w이며, 8로 나누어 떨어진다.
         * (따라서 어던 바이트도 두 행에 걸치지 않는다)
         * 몰론, 화면 높이는 배열의 길이와 화면 폭 w를 통해 유도해 낼 수 있다.
         * 함수 drawHorizontalLine(byte[] screen, int width, int x1, int x2, int y)를 구현하라.
         * 이 함수는 (x1, y)에서 (x2, y)로 수평선을 긋는다.
         */

        public static void Q8_DrawHorizontalLine(byte[] screen, int width, int x1, int x2, int y)
        {
            var startOffset = x1 % 8;
            var firstFullByte = x1 / 8;

            if (startOffset != 0)
            {
                firstFullByte++;
            }

            var endOffset = x2 % 8;
            var lastFullByte = x2 / 8;

            if (endOffset != 7)
            {
                lastFullByte--;
            }

            // Set full bytes
            for (var b = firstFullByte; b <= lastFullByte; b++)
            {
                screen[(width / 8) * y + b] = (byte)0xFF;
            }

            var startMask = (byte)(0xFF >> startOffset);
            var endMask = (byte)~(0xFF >> (endOffset + 1));

            // Set start and end of line
            if ((x1 / 8) == (x2 / 8))
            {
                // If x1 and x2 are in the same byte
                var mask = (byte)(startMask & endMask);
                screen[(width / 8) * y + (x1 / 8)] |= mask;
            }
            else
            {
                if (startOffset != 0)
                {
                    var byteNumber = (width / 8) * y + firstFullByte - 1;
                    screen[byteNumber] |= startMask;
                }
                if (endOffset != 7)
                {
                    var byteNumber = (width / 8) * y + lastFullByte + 1;
                    screen[byteNumber] |= endMask;
                }
            }
        }

        #endregion
    }
}
