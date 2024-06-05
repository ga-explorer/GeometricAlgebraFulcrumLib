namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ODS;

public partial class HashTable<T> : IDictionary<uint, T>
{
    private const float Fill = 0.66f;

    // SetSize ~= ((8/15)*sqrt(30)) / 1,5
    //private static uint SetSize = 2;

    private readonly System.Random _random;
    private int _count;
    private uint _pseudoCount;
    private uint _limit;
    private InnerHashTable[] _inner;
    private uint _a;
    private uint _b;
    private int _width;

    public HashTable()
    {
        _random = new System.Random();
        RehashAll(null);
    }

    // add overwrites
    public void Add(uint key, T value)
    {
        var firstHash = GetHash(key);
        var innerHashed = _inner[firstHash];
        var secondHash = innerHashed.GetHash(key);
        // overwrite
        if (innerHashed.Table[secondHash] != null && innerHashed.Table[secondHash].Value.Key == key)
        {
            throw new ArgumentException();
        }
        _count++;
        if (++_pseudoCount > _limit)
        {
            RehashAll(new KeyValuePair<uint, T>(key, value));
            return;
        }
        // empty spot - just plop the value there and call it a day
        if (innerHashed.IsDeleted((int)secondHash))
        {
            innerHashed.Count++;
            innerHashed.Table[secondHash] = new KeyValuePair<uint, T>(key, value);
        }
        // We've got collision now, do something about it
        else
        {
            // Note that original algorithm does this in a roundabout way due to their weird data structures
            if (++innerHashed.Count <= innerHashed.Limit)
            {
                // Rehash second level
                innerHashed.RehashWith(key, value, this, innerHashed.Table, innerHashed.Table.Length);
            }
            else
            {
                // Grow the second level
                var newLimit = _limit = 2 * Math.Max(1, innerHashed.Limit);
                var newSize = BitHacks.RoundToPower(2 * newLimit * (newLimit - 1));
                innerHashed.Limit = newLimit;
                innerHashed.Width = BitHacks.Power2Msb(newSize);
                innerHashed.RehashWith(key, value, this, innerHashed.Table, (int)newSize);
            }
        }
    }

    public bool Remove(uint key)
    {
        _pseudoCount++;
        var firstHash = GetHash(key);
        var secondHash = _inner[firstHash].GetHash(key);
        var result = false;
        if (_inner[firstHash].Table[secondHash] != null && _inner[firstHash].Table[secondHash].Value.Key == key)
        {
            _inner[firstHash].RemoveAt((int)secondHash);
            result = true;
            _count--;
        }

        if (_pseudoCount >= _limit)
            RehashAll(null);

        return result;
    }

    public bool TryGetValue(uint key, out T value)
    {
        var firstHash = GetHash(key);
        var secondHash = _inner[firstHash].GetHash(key);
        if (_inner[firstHash].IsContained((int)secondHash) && _inner[firstHash].Table[secondHash].Value.Key == key)
        {
            value = _inner[firstHash].Table[secondHash].Value.Value;
            return true;
        }
        value = default;
        return false;
    }

    internal uint GetHash(uint x)
    {
#if TEST
            return ((a * x + b) % 997) % (uint)Math.Pow(2, this.width);
#else
        return ((_a * x + _b) >> (31 - _width)) >> 1;
#endif
    }

    /*
    internal Func<uint, uint> GetRandomHashMethod(uint size)
    {
        System.Diagnostics.Debug.Assert(size == BitHacks.RoundToPower(size));
        uint a = (uint)random.Next();
        uint b = (uint)(random.Next(65536) << 16);
        int shift = 31 - (int)BitHacks.Log2Ceiling(size);
        // weird shifting because c# can't shift uint by more than 31 bits
        return (x) =>  ((a * x + b) >> shift) >> 1;
    }
     * */

    private void RehashAll(KeyValuePair<uint, T>? newValue)
    {
        var elements = new List<KeyValuePair<uint,T>>((int)(newValue == null ? _pseudoCount : _pseudoCount + 1));
        if(_inner != null)
        {
            var j = 0;
            foreach(var table in _inner)
            {
                for(var i = 0; i< table.AllocatedSize; i++)
                {
                    if (table.IsContained(i))
                    {
                        elements.Add(table.Table[i].Value);
                        j++;
                    }
                }
            }
            if(newValue.HasValue)
                elements.Add(newValue.Value);
        }
        _pseudoCount = (uint)elements.Count;
        var newLimit = (1.0f + Fill) * Math.Max(_pseudoCount, 4.0f);
        _limit = (uint)newLimit;
        // hashSize = s(M)
        var hashSize = BitHacks.RoundToPower(_limit << 1);
        _width = BitHacks.Power2Msb(hashSize);
        List<KeyValuePair<uint, T>>[] hashList = null;
        // find suitable higher level function
        for(var injective = false; !injective;)
        {
            InitializeRandomHash(out _a, out _b);
            hashList = new List<KeyValuePair<uint, T>>[hashSize];
            // initialize provisional list of elemnts going into second level table
            for (var i = 0; i < hashList.Length; i++)
                hashList[i] = new List<KeyValuePair<uint,T>>();
            // run first level hashes
            foreach (var elm in elements)
                hashList[GetHash(elm.Key)].Add(elm);
            var testTable = new InnerHashTable[hashSize];
            injective = SatisfiesMagicalCondition(hashList, _limit);
        }
        // find suitable lower level function
        _inner = new InnerHashTable[hashSize];
        for (var i = 0; i < hashSize; i++)
        {
            // We deviate from original algorithm here,
            // if we've got empty second level we initialize it to size one to avoid out-of-bounds access in other functions.
            _inner[i] = new InnerHashTable(Math.Max((uint)hashList[i].Count,1));
            if (hashList[i].Count == 0)
                _inner[i].Count = 0;
            while (true)
            {
                _inner[i].Clear();
                _inner[i].InitializeRandomHash(this);
                for (var j = 0; j < hashList[i].Count; j++)
                {
                    var hash = _inner[i].GetHash(hashList[i][j].Key);
                    if(_inner[i].IsContained((int)hash))
                        // don't judge me
                        goto Failed;
                    _inner[i].Table[hash] = hashList[i][j];
                }
                break;
                Failed:
                continue;
            }
        }
    }

    internal void InitializeRandomHash(out uint a, out uint b)
    {
#if TEST
            a = (uint)random.Next(1,997);
            b = (uint)random.Next(997);
#else
        a = ((uint)_random.Next() * 2) + 1;
        b = (uint)(_random.Next(65536) << 16);
#endif
    }

    private bool SatisfiesMagicalCondition(List<KeyValuePair<uint,T>>[] inner, uint currLimit)
    {
        uint sum = 0;
        foreach (var tab in inner)
        {
            sum += (uint)((tab.Count << 3) - (4*tab.Count));
        }
        return sum <= ((32 * (long)inner.Length * inner.Length) / currLimit) + 4 * inner.Length;
    }
    bool IDictionary<uint, T>.ContainsKey(uint key)
    {
        throw new NotImplementedException();
    }

    ICollection<uint> IDictionary<uint, T>.Keys => throw new NotImplementedException();

    ICollection<T> IDictionary<uint, T>.Values => throw new NotImplementedException();

    T IDictionary<uint, T>.this[uint key]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    void ICollection<KeyValuePair<uint, T>>.Add(KeyValuePair<uint, T> item)
    {
        throw new NotImplementedException();
    }

    void ICollection<KeyValuePair<uint, T>>.Clear()
    {
        throw new NotImplementedException();
    }

    bool ICollection<KeyValuePair<uint, T>>.Contains(KeyValuePair<uint, T> item)
    {
        throw new NotImplementedException();
    }

    void ICollection<KeyValuePair<uint, T>>.CopyTo(KeyValuePair<uint, T>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public int Count => _count;

    bool ICollection<KeyValuePair<uint, T>>.IsReadOnly => throw new NotImplementedException();

    bool ICollection<KeyValuePair<uint, T>>.Remove(KeyValuePair<uint, T> item)
    {
        throw new NotImplementedException();
    }

    IEnumerator<KeyValuePair<uint, T>> IEnumerable<KeyValuePair<uint, T>>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}