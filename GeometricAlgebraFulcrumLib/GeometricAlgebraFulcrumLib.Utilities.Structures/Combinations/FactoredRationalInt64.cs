using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using Open.Numeric.Primes;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

/// <summary>
/// This class represents a rational number stored internally using
/// a product of powers of primes. This is useful for automatically
/// simplifying expressions involving products of factorials, permutations,
/// and combinations.
/// </summary>
public sealed class FactoredRationalInt64 :
    IReadOnlyDictionary<ulong, int>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FactoredRationalInt64 Create()
    {
        return new FactoredRationalInt64();
    }


    private bool _negativeSign;
    private readonly SortedDictionary<ulong, int> _primePowerDictionary
        = new SortedDictionary<ulong, int>();


    public long Sign 
        => _negativeSign ? -1L : 1L;

        
    public int Count 
        => _primePowerDictionary.Count;
        
    public IEnumerable<ulong> Keys 
        => _primePowerDictionary.Keys;

    public IEnumerable<int> Values 
        => _primePowerDictionary.Values;

    public int this[ulong key]
    {
        get
        {
            if (!Number.IsPrime(key))
                throw new ArgumentException($"{key} is not a prime number!");

            return GetPrimeFactorPower(key);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FactoredRationalInt64()
    {
    }


    public FactoredRationalInt64 GetCopy()
    {
        var rational = new FactoredRationalInt64
        {
            _negativeSign = _negativeSign
        };

        foreach (var (a, b) in _primePowerDictionary)
            rational._primePowerDictionary.Add(a, b);

        return rational;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FactoredRationalInt64 SetNegativeSign(bool isNegative)
    {
        _negativeSign = isNegative;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FactoredRationalInt64 SetNegative()
    {
        _negativeSign = !_negativeSign;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FactoredRationalInt64 SetValue(long n)
    {
        Clear();
        return TimesValue(n);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FactoredRationalInt64 Clear()
    {
        _negativeSign = false;
        _primePowerDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(ulong primeFactor)
    {
        return _primePowerDictionary.Remove(primeFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(ulong key)
    {
        return _primePowerDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong key, out int value)
    {
        return _primePowerDictionary.TryGetValue(key, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetPrimeFactorPower(ulong primeFactor)
    {
        return _primePowerDictionary.TryGetValue(primeFactor, out var power)
            ? power 
            : 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetPrimeFactorPower(ulong primeFactor, int power)
    {
        if (power == 0) _primePowerDictionary.Remove(primeFactor);

        if (_primePowerDictionary.ContainsKey(primeFactor))
            _primePowerDictionary[primeFactor] = power;
        else
            _primePowerDictionary.Add(primeFactor, power);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddPrimeFactorPower(ulong primeFactor, int power)
    {
        if (_primePowerDictionary.TryGetValue(primeFactor, out var powerOld))
        {
            power += powerOld;

            if (power == 0)
                _primePowerDictionary.Remove(primeFactor);
            else
                _primePowerDictionary[primeFactor] = power;
        }
        else if (power != 0)
            _primePowerDictionary.Add(primeFactor, power);
    }
        

    public FactoredRationalInt64 TimesValue(int n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (uint) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;

        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, 1);

        return this;
    }
        
    public FactoredRationalInt64 TimesValue(uint n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, 1);

        return this;
    }

    public FactoredRationalInt64 TimesValue(long n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (ulong) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;
            
        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, 1);

        return this;
    }

    public FactoredRationalInt64 TimesValue(ulong n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, 1);

        return this;
    }
        

    public FactoredRationalInt64 DivideValue(int n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (uint) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;
            
        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -1);

        return this;
    }
        
    public FactoredRationalInt64 DivideValue(uint n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -1);

        return this;
    }

    public FactoredRationalInt64 DivideValue(long n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (ulong) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;
            
        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -1);

        return this;
    }

    public FactoredRationalInt64 DivideValue(ulong n)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -1);

        return this;
    }


    public FactoredRationalInt64 TimesValues(IEnumerable<int> nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }
        
    public FactoredRationalInt64 TimesValues(IEnumerable<uint> nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }
        
    public FactoredRationalInt64 TimesValues(IEnumerable<long> nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }
        
    public FactoredRationalInt64 TimesValues(IEnumerable<ulong> nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }


    public FactoredRationalInt64 DivideValues(IEnumerable<int> nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }
        
    public FactoredRationalInt64 DivideValues(IEnumerable<uint> nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }
        
    public FactoredRationalInt64 DivideValues(IEnumerable<long> nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }
        
    public FactoredRationalInt64 DivideValues(IEnumerable<ulong> nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }


    public FactoredRationalInt64 TimesValues(params int[] nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }
        
    public FactoredRationalInt64 TimesValues(params uint[] nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }
        
    public FactoredRationalInt64 TimesValues(params long[] nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }
        
    public FactoredRationalInt64 TimesValues(params ulong[] nValues)
    {
        foreach (var n in nValues)
            TimesValue(n);

        return this;
    }


    public FactoredRationalInt64 DivideValues(params int[] nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }
        
    public FactoredRationalInt64 DivideValues(params uint[] nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }
        
    public FactoredRationalInt64 DivideValues(params long[] nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }
        
    public FactoredRationalInt64 DivideValues(params ulong[] nValues)
    {
        foreach (var n in nValues)
            DivideValue(n);

        return this;
    }

        
    public FactoredRationalInt64 TimesPower(int n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (uint) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;

        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, power);

        return this;
    }
        
    public FactoredRationalInt64 TimesPower(uint n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, power);

        return this;
    }

    public FactoredRationalInt64 TimesPower(long n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (ulong) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;
            
        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, power);

        return this;
    }

    public FactoredRationalInt64 TimesPower(ulong n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, power);

        return this;
    }

        
    public FactoredRationalInt64 DividePower(int n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (uint) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;

        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -power);

        return this;
    }
        
    public FactoredRationalInt64 DividePower(uint n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -power);

        return this;
    }

    public FactoredRationalInt64 DividePower(long n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));

        var nUnsigned = (ulong) (n < 0 ? -n : n);

        if (n < 0) _negativeSign = !_negativeSign;
            
        if (nUnsigned == 1)
            return this;

        var primeFactors = 
            nUnsigned.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -power);

        return this;
    }

    public FactoredRationalInt64 DividePower(ulong n, int power)
    {
        if (n == 0)
            throw new ArgumentException(nameof(n));
            
        if (n == 1)
            return this;

        var primeFactors = 
            n.GetFactors(true);

        foreach (var factor in primeFactors) 
            AddPrimeFactorPower(factor, -power);

        return this;
    }
        

    public FactoredRationalInt64 TimesFactorial(int n)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (n == 0) return this;

        while (n > 1)
        {
            TimesValue(n);

            n--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesFactorial(uint n)
    {
        if (n == 0) return this;

        while (n > 1)
        {
            TimesValue(n);

            n--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesFactorial(long n)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (n == 0) return this;

        while (n > 1)
        {
            TimesValue(n);

            n--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesFactorial(ulong n)
    {
        if (n == 0) return this;

        while (n > 1)
        {
            TimesValue(n);

            n--;
        }

        return this;
    }


    public FactoredRationalInt64 DivideFactorial(int n)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (n == 0) return this;

        while (n > 1)
        {
            DivideValue(n);

            n--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DivideFactorial(uint n)
    {
        if (n == 0) return this;

        while (n > 1)
        {
            DivideValue(n);

            n--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DivideFactorial(long n)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (n == 0) return this;

        while (n > 1)
        {
            DivideValue(n);

            n--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DivideFactorial(ulong n)
    {
        if (n == 0) return this;

        while (n > 1)
        {
            DivideValue(n);

            n--;
        }

        return this;
    }


    public FactoredRationalInt64 TimesPermutation(int n, int k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            TimesValue(n);
                
            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesPermutation(uint n, uint k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            TimesValue(n);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesPermutation(long n, long k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            TimesValue(n);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesPermutation(ulong n, ulong k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            TimesValue(n);

            n--;
            k--;
        }

        return this;
    }


    public FactoredRationalInt64 DividePermutation(int n, int k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            DivideValue(n);
                
            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DividePermutation(uint n, uint k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            DivideValue(n);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DividePermutation(long n, long k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            DivideValue(n);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DividePermutation(ulong n, ulong k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        while (k > 0)
        {
            DivideValue(n);

            n--;
            k--;
        }

        return this;
    }
        

    public FactoredRationalInt64 TimesCombination(int n, int k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            TimesValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesCombination(uint n, uint k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            TimesValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }

    public FactoredRationalInt64 TimesCombination(long n, long k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            TimesValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 TimesCombination(ulong n, ulong k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            TimesValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }
        

    public FactoredRationalInt64 DivideCombination(int n, int k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            DivideValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DivideCombination(uint n, uint k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            DivideValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }

    public FactoredRationalInt64 DivideCombination(long n, long k)
    {
        if (n < 0)
            throw new ArgumentException(nameof(n));

        if (k < 0 || k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            DivideValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }
        
    public FactoredRationalInt64 DivideCombination(ulong n, ulong k)
    {
        if (k > n)
            throw new ArgumentException(nameof(k));

        if (n == 0) return this;

        if (k >= n / 2) 
            k = n - k;

        while (k > 0)
        {
            DivideValue(n);
            DivideValue(k);

            n--;
            k--;
        }

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, int> TryToInt32()
    {
        try
        {
            return new Tuple<bool, int>(true, ToInt32());
        }
        catch
        {
            return Tuple.Create(false, 0);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, uint> TryToUInt32()
    {
        try
        {
            return new Tuple<bool, uint>(true, ToUInt32());
        }
        catch
        {
            return Tuple.Create(false, 0u);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, long> TryToInt64()
    {
        try
        {
            return new Tuple<bool, long>(true, ToInt64());
        }
        catch
        {
            return Tuple.Create(false, 0L);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, ulong> TryToUInt64()
    {
        try
        {
            return new Tuple<bool, ulong>(true, ToUInt64());
        }
        catch
        {
            return Tuple.Create(false, 0UL);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, float> TryToFloat32()
    {
        try
        {
            return new Tuple<bool, float>(true, ToFloat32());
        }
        catch
        {
            return Tuple.Create(false, 0f);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, double> TryToFloat64()
    {
        try
        {
            return new Tuple<bool, double>(true, ToFloat64());
        }
        catch
        {
            return Tuple.Create(false, 0d);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, EFloat> TryToEFloat()
    {
        try
        {
            return new Tuple<bool, EFloat>(true, ToEFloat());
        }
        catch
        {
            return Tuple.Create(false, EFloat.Zero);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, EDecimal> TryToEDecimal()
    {
        try
        {
            return new Tuple<bool, EDecimal>(true, ToEDecimal());
        }
        catch
        {
            return Tuple.Create(false, EDecimal.Zero);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, EInteger> TryToEInteger()
    {
        try
        {
            return new Tuple<bool, EInteger>(true, ToEInteger());
        }
        catch
        {
            return Tuple.Create(false, EInteger.Zero);
        }
    }


    public int ToInt32()
    {
        var value = 1UL;

        foreach (var (primeFactor, power) in _primePowerDictionary)
        {
            if (power < 0)
                throw new InvalidOperationException();

            checked
            {
                value *= primeFactor.Power(power);
            }
        }
            
        checked
        {
            return _negativeSign ? -(int)value : (int)value;
        }
    }
        
    public uint ToUInt32()
    {
        if (_negativeSign)
            throw new InvalidOperationException();

        var value = 1UL;

        foreach (var (primeFactor, power) in _primePowerDictionary)
        {
            if (power < 0)
                throw new InvalidOperationException();

            checked
            {
                value *= primeFactor.Power(power);
            }
        }

        checked
        {
            return (uint)value;
        }
    }

    public long ToInt64()
    {
        var value = 1UL;

        foreach (var (primeFactor, power) in _primePowerDictionary)
        {
            if (power < 0)
                throw new InvalidOperationException();

            checked
            {
                value *= primeFactor.Power(power);
            }
        }

        checked
        {
            return _negativeSign ? -(long)value : (long)value;
        }
    }
        
    public ulong ToUInt64()
    {
        if (_negativeSign)
            throw new InvalidOperationException();

        var value = 1UL;

        foreach (var (primeFactor, power) in _primePowerDictionary)
        {
            if (power < 0)
                throw new InvalidOperationException();

            checked
            {
                value *= primeFactor.Power(power);
            }
        }
            
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ToFloat32()
    {
        return (float)ToFloat64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64()
    {
        return ToEFloat().ToDouble();

        //var value = 1d;

        //foreach (var (prime, power) in _primePowerDictionary)
        //{
        //    value *= Math.Pow(prime, power);

        //    if (double.IsNaN(value))
        //        throw new InvalidOperationException();
        //}

        //return _negativeSign ? -value : value;
    }
        
    public EInteger ToEInteger()
    {
        var value = _negativeSign ? -EInteger.One : EInteger.One;

        foreach (var (prime, power) in _primePowerDictionary)
        {
            if (power < 0)
                throw new InvalidOperationException();

            value *= EInteger.FromUInt64(prime).Pow(power);
        }

        return value;
    }

    public EFloat ToEFloat()
    {
        var value = _negativeSign ? -EFloat.One : EFloat.One;

        foreach (var (prime, power) in _primePowerDictionary)
            value *= EFloat.FromUInt64(prime).Pow(power);

        return value;
    }
        
    public EDecimal ToEDecimal()
    {
        var value = _negativeSign ? -EDecimal.One : EDecimal.One;

        foreach (var (prime, power) in _primePowerDictionary)
            value *= EDecimal.FromUInt64(prime).Pow(power);

        return value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<ulong, int>> GetEnumerator()
    {
        return _primePowerDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}