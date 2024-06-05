namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

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

        _n = n;
        _k = k;
        _data = new long[k];
        for (long i = 0; i < k; ++i)
            _data[i] = i;
    } // Combination(n,k)

    public Combination(long n, long k, long[] a) // Combination from a[]
    {
        if (k != a.Length)
            throw new Exception("Array length does not equal k");

        _n = n;
        _k = k;
        _data = new long[k];
        for (long i = 0; i < a.Length; ++i)
            _data[i] = a[i];

        if (!IsValid())
            throw new Exception("Bad value from array");
    } // Combination(n,k,a)

    public bool IsValid()
    {
        if (_data.Length != _k)
            return false; // corrupted

        for (long i = 0; i < _k; ++i)
        {
            if (_data[i] < 0 || _data[i] > _n - 1)
                return false; // value out of range

            for (var j = i + 1; j < _k; ++j)
                if (_data[i] >= _data[j])
                    return false; // duplicate or not lexicographic
        }

        return true;
    } // IsValid()

    public override string ToString()
    {
        var s = "{ ";
        for (long i = 0; i < _k; ++i)
            s += _data[i].ToString() + " ";
        s += "}";
        return s;
    } // ToString()

    public Combination Successor()
    {
        if (_data[0] == _n - _k)
            return null;

        var ans = new Combination(_n, _k);

        long i;
        for (i = 0; i < _k; ++i)
            ans._data[i] = _data[i];

        for (i = _k - 1; i > 0 && ans._data[i] == _n - _k + i; --i)
            ;

        ++ans._data[i];

        for (var j = i; j < _k - 1; ++j)
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