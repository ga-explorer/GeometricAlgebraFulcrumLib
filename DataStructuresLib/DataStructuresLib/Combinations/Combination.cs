using System;

namespace DataStructuresLib.Combinations
{


    /// <summary>
    /// https://www.developertyrone.com/blog/generating-the-mth-lexicographical-element-of-a-mathematical-combination/
    /// </summary>
    public class Combination
    {
        private readonly long _n = 0;
        private readonly long _k = 0;
        private readonly long[] _data = null;

        public Combination(long n, long k)
        {
            if (n < 0 || k < 0) // normally n >= k
                throw new Exception("Negative parameter in constructor");

            this._n = n;
            this._k = k;
            this._data = new long[k];
            for (long i = 0; i < k; ++i)
                this._data[i] = i;
        } // Combination(n,k)

        public Combination(long n, long k, long[] a) // Combination from a[]
        {
            if (k != a.Length)
                throw new Exception("Array length does not equal k");

            this._n = n;
            this._k = k;
            this._data = new long[k];
            for (long i = 0; i < a.Length; ++i)
                this._data[i] = a[i];

            if (!this.IsValid())
                throw new Exception("Bad value from array");
        } // Combination(n,k,a)

        public bool IsValid()
        {
            if (this._data.Length != this._k)
                return false; // corrupted

            for (long i = 0; i < this._k; ++i)
            {
                if (this._data[i] < 0 || this._data[i] > this._n - 1)
                    return false; // value out of range

                for (var j = i + 1; j < this._k; ++j)
                    if (this._data[i] >= this._data[j])
                        return false; // duplicate or not lexicographic
            }

            return true;
        } // IsValid()

        public override string ToString()
        {
            var s = "{ ";
            for (long i = 0; i < this._k; ++i)
                s += this._data[i].ToString() + " ";
            s += "}";
            return s;
        } // ToString()

        public Combination Successor()
        {
            if (this._data[0] == this._n - this._k)
                return null;

            var ans = new Combination(this._n, this._k);

            long i;
            for (i = 0; i < this._k; ++i)
                ans._data[i] = this._data[i];

            for (i = this._k - 1; i > 0 && ans._data[i] == this._n - this._k + i; --i)
                ;

            ++ans._data[i];

            for (var j = i; j < this._k - 1; ++j)
                ans._data[j + 1] = ans._data[j] + 1;

            return ans;
        } // Successor()

        public static long Choose(long n, long k)
        {
            if (n < 0 || k < 0)
                throw new Exception("Invalid negative parameter in Choose()");
            if (n < k)
                return 0;  // special case
            if (n == k)
                return 1;

            long delta, iMax;

            if (k < n - k) // ex: Choose(100,3)
            {
                delta = n - k;
                iMax = k;
            }
            else         // ex: Choose(100,97)
            {
                delta = k;
                iMax = n - k;
            }

            var ans = delta + 1;

            for (long i = 2; i <= iMax; ++i)
            {
                checked { ans = (ans * (delta + i)) / i; }
            }

            return ans;
        } // Choose()

    } // Combination class

}