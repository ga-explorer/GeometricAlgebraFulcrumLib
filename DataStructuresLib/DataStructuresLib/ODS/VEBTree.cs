using System;
using System.Collections.Generic;

namespace DataStructuresLib.ODS
{
    // TODO: add version increment to all mutating operations and check to enumerator
    public partial class VebTree<T> : SortedDictionaryBase<T>
    {
        private readonly VebTree<T>[] _cluster;
        private readonly VebTree<uint> _summary;
        private uint? _minKey;
        private T _minValue;
        private uint? _maxKey;
        private T _maxValue;
        private readonly int _width;
        private int _count;
        private int _version;

        public VebTree()
            : this(32)
        { }

        public VebTree(int width)
        {
            if (width > 32 || width < 1)
                throw new ArgumentOutOfRangeException();

            this._width = width;
            if (width > 1)
            {
                var highHalf = width / 2 + (width & 1);
                var lowhHalf = width / 2;
                var halfSize = (int)BitHacks.MaxValue(highHalf) + 1;
                _summary = new VebTree<uint>(highHalf);
                _cluster = new VebTree<T>[halfSize];
                for (var i = 0; i < halfSize; i++)
                    _cluster[i] = new VebTree<T>(lowhHalf);
            }
        }

        internal uint HighBits(uint x)
        {
            var leftShift = 32 - _width;
            return (x >> _width /2);
        }

        internal uint LowBits(uint x)
        {
            var shift = 32 - (_width/2);
            return ((x << shift) >> shift);
        }

        internal uint Index(uint x, uint y)
        {
            return (x << (_width/2)) | y;
        }

        private void AddChecked(uint key, T value, bool overwrite)
        {

            if (key >= (1 << _width))
                throw new ArgumentOutOfRangeException();

            if ((key == _minKey || key == _maxKey) && !overwrite)
                throw new ArgumentException();

            if (key != _minKey && key != _maxKey)
                _count++;

            if (_minKey == null)
            {
                EmptyAdd(key, value);
                return;
            }

            // I use <= nistead of < to indicate case when we want
            // to set new value associated with key already in the set
            if (key <= _minKey)
            {
                var tempKey = key;
                var tempValue = value;
                key = _minKey.Value;
                value = _minValue;
                _minKey = tempKey;
                _minValue = tempValue;
            }

            if (!IsLeaf)
            {
                var highX = HighBits(key);
                if (_cluster[highX]._minKey == null)
                {
                    _summary[highX] = highX;
                    var lowX = LowBits(key);
                    _cluster[highX].EmptyAdd(lowX, value);
                }
                else
                {
                    _cluster[highX].AddChecked(LowBits(key), value, overwrite);
                }
            }

            // I use >= nistead of > to indicate case when we want
            // to set new value associated with key already in the set
            if (key >= _maxKey)
            {
                _maxKey = key;
                _maxValue = value;
            }
        }

        public override void Add(uint key, T value)
        {
            AddChecked(key, value, false);
        }

        private void EmptyAdd(uint key, T value)
        {
            _minKey = key;
            _minValue = value;
            _maxKey = key;
            _maxValue = value;
        }

        public override bool ContainsKey(uint key)
        {
            if (key == _minKey || key == _maxKey)
                return true;
            else if (IsLeaf)
                return false;
            else
                return _cluster[HighBits(key)].ContainsKey(LowBits(key));
        }

        public override bool Remove(uint key)
        {
            if (RemoveCore(key))
            {
                _count--;
                return true;
            }
            return false;
        }

        private bool RemoveCore(uint key)
        {
            if (_minKey == _maxKey)
            {
                if (_minKey == key)
                {
                    _minKey = null;
                    _minValue = default(T);
                    _maxKey = null;
                    _maxValue = default(T);
                    return true;
                }
            }
            // minkey and maxkey are different and we are within leaf
            else if (IsLeaf)
            {
                if (key == 0)
                {
                    _minKey = 1;
                    _minValue = _maxValue;
                    _maxValue = default(T);
                }
                else
                {
                    _maxKey = 0;
                    _maxValue = _minValue;
                    _minValue = default(T);
                }
                return true;
            }
            // minkey and maxkey are different and we are not inside leaf
            else
            {
                if (_minKey == key)
                {
                    var firstCluster = _summary._minKey.Value;
                    key = Index(firstCluster, _cluster[firstCluster]._minKey.Value);
                    _minKey = key;
                    // update the value
                    _minValue = _cluster[firstCluster]._minValue;
                }
                var result = _cluster[HighBits(key)].RemoveCore(LowBits(key));
                if(_cluster[HighBits(key)]._minKey == null)
                {
                    _summary.RemoveCore(HighBits(key));
                    if(key == _maxKey)
                    {
                        var summaryMax = _summary._maxKey;
                        if (summaryMax == null)
                        {
                            _maxKey = _minKey;
                            _maxValue = _minValue;
                        }
                        else
                        {
                            _maxKey = Index(summaryMax.Value, _cluster[summaryMax.Value]._maxKey.Value);
                            _maxValue = _cluster[summaryMax.Value]._maxValue;
                        }
                    }
                }
                else if (key == _maxKey)
                {
                    _maxKey = Index(HighBits(key), _cluster[HighBits(key)]._maxKey.Value);
                    _maxValue = _cluster[HighBits(key)]._maxValue;
                }
                return result;
            }
            return false;
        }

        public override bool TryGetValue(uint key, out T value)
        {
            if (key == _minKey)
            {
                value = _minValue;
                return true;
            }
            else if (key == _maxKey)
            {
                value = _maxValue;
                return true;
            }
            else if (IsLeaf)
            {
                value = default(T);
                return false;
            }
            else
            {
                return _cluster[HighBits(key)].TryGetValue(LowBits(key), out value);
            }
        }

        public override T this[uint key]
        {
            get
            {
                T value;
                if (TryGetValue(key, out value))
                    return value;
                else
                    throw new KeyNotFoundException();
            }
            set => AddChecked(key, value, true);
        }

        public override void Clear()
        {
            _count = 0;
            _version++;
            _minKey = null;
            _minValue = default(T);
            _maxKey = null;
            _maxValue = default(T);
            if (!IsLeaf)
            {
                _summary.Clear();
                for (var i = 0; i < _cluster.Length; i++)
                    _cluster[i].Clear();
            }
        }

        public override int Count => _count;

        public override IEnumerator<KeyValuePair<uint, T>> GetEnumerator()
        {
            return GetEnumerator(0);
        }

        // TODO: speed-up sparse iteration by checking summary trees
        // TODO: speed up iteration by avoiding recursive GetEnumerator calls - we could cheat by unrolling them, uint tree will only have lg(32)+1 levels.
        private IEnumerator<KeyValuePair<uint, T>> GetEnumerator(uint parent)
        {
            if (_minKey != null)
            {
                yield return new KeyValuePair<uint, T>((parent << _width) + _minKey.Value, _minValue);
            }
            if(IsLeaf)
            {
                if(_minKey != _maxKey)
                    yield return new KeyValuePair<uint, T>((parent << _width) + _maxKey.Value, _maxValue);
            }
            else
            {
                for(uint i = 0; i < _cluster.Length; i++)
                {
                    var iter = _cluster[i].GetEnumerator((parent << (_width/2)) + i);
                    while (iter.MoveNext())
                    {
                        yield return iter.Current;
                    }
                }
            }
        }

        #region ISorted


        public override KeyValuePair<uint, T>? First()
        {
            if (_minKey == null)
                return null;
            return new KeyValuePair<uint, T>(_minKey.Value, _minValue);
        }

        public override KeyValuePair<uint, T>? Last()
        {
            if (_maxKey == null)
                return null;
            return new KeyValuePair<uint, T>(_maxKey.Value, _maxValue);
        }

        public override KeyValuePair<uint, T>? Lower(uint key)
        {
            if (IsLeaf)
            {
                if (key == 1 && _minKey == 0)
                    return new KeyValuePair<uint, T>(0, _minValue);
                else
                    return null;
            }
            else if (_maxKey != null && key > _maxKey.Value)
            {
                return new KeyValuePair<uint, T>(_maxKey.Value, _maxValue);
            }
            else
            {
                var highBits = HighBits(key);
                var minLow = _cluster[HighBits(key)]._minKey;
                if (minLow != null && LowBits(key) > minLow.Value)
                {
                    var offset = _cluster[highBits].Lower(LowBits(key));
                    var returnKey = Index(highBits, offset.Value.Key);
                    return new KeyValuePair<uint, T>(returnKey, offset.Value.Value);
                }
                else
                {
                    var predCluster = _summary.Lower(HighBits(key));
                    if (predCluster == null)
                    {
                        if (_minKey != null && key > _minKey.Value)
                            return new KeyValuePair<uint, T>(_minKey.Value, _minValue);
                        else
                            return null;
                    }
                    else
                    {
                        var offset = _cluster[predCluster.Value.Key]._maxKey;
                        return new KeyValuePair<uint, T>(Index(predCluster.Value.Key, offset.Value), _cluster[predCluster.Value.Key]._maxValue);
                    }
                }
            }
        }

        public override KeyValuePair<uint, T>? Higher(uint key)
        {
            if (IsLeaf)
            {
                if (key == 0 && _maxKey == 1)
                    return new KeyValuePair<uint, T>(1, _maxValue);
                else
                    return null;
            }
            else if (_minKey != null && key < _minKey.Value)
            {
                return new KeyValuePair<uint, T>(_minKey.Value, _minValue);
            }
            else
            {
                var highBits =  HighBits(key);
                var maxLow = _cluster[highBits]._maxKey;
                if (maxLow != null && LowBits(key) < maxLow.Value)
                {
                    var offset = _cluster[highBits].Higher(LowBits(key));
                    var returnKey = Index(highBits, offset.Value.Key);
                    return new KeyValuePair<uint, T>(returnKey, offset.Value.Value);
                }
                else
                {
                    var succCluster = _summary.Higher(HighBits(key));
                    if (succCluster == null)
                    {
                        return null;
                    }
                    else
                    {
                        var offset = _cluster[succCluster.Value.Key]._minKey;
                        return new KeyValuePair<uint, T>(Index(succCluster.Value.Key, offset.Value), _cluster[succCluster.Value.Key]._minValue);
                    }
                }
            }

        }

        private bool IsLeaf => _width == 1;

        #endregion
    }
}
