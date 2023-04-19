using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Open.Numeric.Primes;

namespace DataStructuresLib.Extensions
{
    public static class IntegerExtensions
    {
        #region PrimeFactoringSerial(Int64 Num) algorithm
        /// <summary>
        /// =======================================================
        /// Copyright(C)2012 Alexander Bell
        /// =======================================================
        /// get list of prime factors of Int64 Num in serial mode
        /// </summary>
        /// <param name="inputNum">Int64</param>
        /// <returns>List<Int64></returns>
        public static List<long> PrimeFactoringSerial(this long inputNum)
        {
            // list of Prime Factors to return
            var lstFactors = new List<long>();

            // temp var to store intermediate result (_num/factors)
            var num = inputNum; 

            #region factorize on base 2
            // divide by mod 2, add to the list if successful,
            // try again on new number (_num / 2)
            while (num % 2 == 0)
            {
                lstFactors.Add(2);
                num = num / 2;
            }

            // if _num==1 then stop factoring and return
            if (num == 1) { return lstFactors; }
            #endregion

            #region try factoring in range: 3 ... Math.Sqrt(_num) + 1;
            // set initial upper limit
            var upMargin = (long)Math.Sqrt(num) + 1;

            // loop with variable upper limit;
            // notice increment of 2 to loop through odd numbers only
            for (long i=3; i<=upMargin; i+=2)
            {
                if (num % i == 0)
                {
                    // add prime factor to the List
                    lstFactors.Add(i);

                    // get new integer to factorize (_num)
                    num = num / i;

                    // calculate the loop upper limit corresponding to new _num
                    upMargin = (long) Math.Sqrt(num) + 1;

                    // keep value of (i) the same in next iteration
                    i-=2;
                }
            }

            // add the last factor
            lstFactors.Add(num);

            // sort list
            lstFactors.Sort();

            return lstFactors;
            #endregion
        }
        #endregion

        #region GetFirstFactorParallel(Int64 Num) algorithm
        /// <summary>
        /// ===================================================================
        /// Copyright(C)2012 Alexander Bell
        /// ===================================================================
        /// parallel algorithm accepts Int64 Num 
        /// and returns either first found not-trivial factor, or number 1.
        /// Note: not-trivial factor can be relatively small composite number,
        /// thus it requires subsequent factoring of itself, adding very 
        /// small overhead, because its value is far less than SQRT(Num)
        /// This algorithm provides significant performance boost 
        /// in comparison to serial implementation while prime factoring
        /// big prime numbers, like for e.g. 18-digit primes:
        /// 999999999999999989
        /// 999999999999999967
        /// 999999999999999877
        /// 999999999999999863
        /// 999999999999999829
        /// or 17-digit primes:
        /// 99999999999999997
        /// 99999999999999977
        /// 99999999999999961
        /// 99999999999999943
        /// 99999999999999919
        /// </summary>
        /// <param name="inputNum">Int64</param>
        /// <returns>Int64</returns>
        public static long GetFirstFactorParallel(this long inputNum)
        {
            // use concurrent stack to store non-trivial factor if found
            var stack = new ConcurrentStack<long>();
                    
            // object to specify degrees of parallelism
            var po = new ParallelOptions();

            try
            {
                // return value initially set to 1

                // step 1: try to factor on base 2, return if OK
                if (inputNum % 2 == 0) return 2;

                // step 2: try to factor on base 3, return if OK
                if (inputNum % 3 == 0) return 3;

                #region parallel algo to find first non-trivial factor if exists

                // set upper limit
                var upMargin = (long)Math.Sqrt(inputNum) + 1;

                // number of CPU cores
                var countCpu = Environment.ProcessorCount;

                // max degree of parallelism set equal to _cpuCount
                po.MaxDegreeOfParallelism = countCpu;

                var stack1 = stack;
                Parallel.For(0, 2, po, (i, _) =>
                {
                    // starting number for inner loops (5 and 7)
                    var seed = 5 + 2 * i;

                    // inner loops running in parallel;
                    // notice that because input Num was already tested for factors 2 and 3,
                    // then increment of 6 is used to speed up the processing,
                    // thus in dual core CPU it looks like:
                    // 5, 11, 17, 23, 29, etc. in first thread
                    // 7, 13, 19, 25, 31, etc, in second thread
                    for (long j = seed; j < upMargin; j += 6)
                    {
                        // exit loop if stack contains value
                        if (stack1.Count != 0)
                            break;

                        // check divisibility
                        if (inputNum % j != 0) continue;

                        // push non-trivial factor to ConcurrentStack and exit loop
                        if (stack1.Count == 0) 
                            stack1.Push(j);

                        break;
                    }
                });

                #endregion

                // return the value in ConcurrentStack if exists, or 1
                return (stack.TryPop(out var ret)) ? ret : 1;
            }
            finally
            {
                po = null; 
                stack = null;
            }

        }
        #endregion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<byte> GetFactors(this byte x, bool properOnly = false)
        {
            var xLong = (ulong)x;

            return Prime.Factors(xLong, properOnly).Select(f => (byte)f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<short> GetFactors(this short x, bool properOnly = false)
        {
            var xLong = (long)x;

            return Prime.Factors(xLong, properOnly).Select(f => (short)f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ushort> GetFactors(this ushort x, bool properOnly = false)
        {
            var xLong = (ulong)x;

            return Prime.Factors(xLong, properOnly).Select(f => (ushort)f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetFactors(this int x, bool properOnly = false)
        {
            var xLong = (long)x;

            return Prime.Factors(xLong, properOnly).Select(f => (int)f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetFactors(this uint x, bool properOnly = false)
        {
            var xLong = (ulong)x;

            return Prime.Factors(xLong, properOnly).Select(f => (uint)f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetFactors(this ulong x, bool properOnly = false)
        {
            return Prime.Factors(x, properOnly);
        }

        /// <summary>
        /// Trivial trial by division factorization
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetFactorsTrivial(this int x, bool properOnly = false)
        {      
            
            for (var factor = 1; factor * factor <= x; factor++)
            {
                if (x % factor != 0) continue;

                yield return factor;

                if (factor * factor != x)
                    yield return x / factor;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(this int a)
        {
            return a >= 0 ? a : -a;
        }

        /// <summary>
        /// Greatest Common Divisor
        /// https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// https://stackoverflow.com/questions/18541832/c-sharp-find-the-greatest-common-divisor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Gcd(this int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;

            //while (b != 0)
            //{
            //    var temp = b;
            //    b = a % b;
            //    a = temp;
            //}

            //return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Gcd(this IEnumerable<int> intList)
        {
            return intList.Aggregate(
                0,
                (a, b) => a.Gcd(b)
            );
        }

        /// <summary>
        /// Least Common Multiple
        /// https://en.wikipedia.org/wiki/Least_common_multiple
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Lcm(this int a, int b)
        {
            if (a == 0 || b == 0) 
                return 0;

            var aAbs = a.Abs();
            var bAbs = b.Abs();
            var gcd = a.Gcd(b);

            return aAbs >= bAbs 
                ? (aAbs / gcd) * bAbs 
                : aAbs * (bAbs / gcd);
        }

        public static int Lcm(this IEnumerable<int> intList)
        {
            var lcm = 0;
            var firstIteration = true;

            foreach (var a in intList)
            {
                if (firstIteration)
                {
                    lcm = a;
                    firstIteration = false;
                }
                else
                {
                    lcm = lcm.Lcm(a);
                }
            }

            return lcm;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOf2(this int n)
        {
            return (n > 0 && ~(n & (n - 1)) > 0);
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Power2Ceiling(this int n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOf2(this long n)
        {
            return (n > 0 && ~(n & (n - 1)) > 0);
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Power2Ceiling(this long n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }

        /// <summary>
        /// Compute the integer power on an integer using
        /// exponentiation by squaring method
        /// https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        /// </summary>
        /// <param name="n"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static long Power(this int n, int exp)
        {
            if (exp < 0)
                throw new ArgumentException(nameof(exp));

            if (exp == 1) 
                return n;

            if (exp == 2)
                return n * n;

            if (n == 2) 
                return 1L << exp;

            if (n == -2)
                return (exp & 1) == 0 ? 1L << exp : -(1L << exp);

            var result = 1L;

            checked
            {
                while (exp > 0)
                {
                    if ((exp & 1) != 0)
                        result *= n;

                    exp >>= 1;

                    n *= n;
                }
            }

            return result; 
        }
        
        /// <summary>
        /// Compute the integer power on an integer using
        /// exponentiation by squaring method
        /// https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        /// </summary>
        /// <param name="n"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static ulong Power(this uint n, int exp)
        {
            if (exp < 0)
                throw new ArgumentException(nameof(exp));

            if (exp == 1) 
                return n;

            if (exp == 2)
                return n * n;

            if (n == 2) 
                return 1UL << exp;
            
            var result = 1UL;

            checked
            {
                while (exp > 0)
                {
                    if ((exp & 1) != 0)
                        result *= n;

                    exp >>= 1;

                    n *= n;
                }
            }

            return result; 
        }

        /// <summary>
        /// Compute the integer power on an integer using
        /// exponentiation by squaring method
        /// https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        /// </summary>
        /// <param name="n"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static long Power(this long n, int exp)
        {
            if (exp < 0)
                throw new ArgumentException(nameof(exp));

            if (exp == 1) 
                return n;

            if (exp == 2)
                return n * n;

            if (n == 2) 
                return 1L << exp;

            if (n == -2)
                return (exp & 1) == 0 ? 1L << exp : -(1L << exp);

            var result = 1L;

            checked
            {
                while (exp > 0)
                {
                    if ((exp & 1) != 0)
                        result *= n;

                    exp >>= 1;

                    n *= n;
                }
            }

            return result; 
        }
        
        /// <summary>
        /// Compute the integer power on an integer using
        /// exponentiation by squaring method
        /// https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        /// </summary>
        /// <param name="n"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static ulong Power(this ulong n, int exp)
        {
            if (exp < 0)
                throw new ArgumentException(nameof(exp));

            if (exp == 1) 
                return n;

            if (exp == 2)
                return n * n;

            if (n == 2) 
                return 1UL << exp;
            
            var result = 1UL;

            checked
            {
                while (exp > 0)
                {
                    if ((exp & 1) != 0)
                        result *= n;

                    exp >>= 1;

                    n *= n;
                }
            }

            return result; 
        }
    }
}

