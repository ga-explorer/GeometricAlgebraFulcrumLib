using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresLib.BooleanPattern
{
    public class BooleanPattern : IEnumerable<bool>
    {
        protected const int Int32BlockSize = 32;
        protected const int UInt32BlockSize = 32;
        protected const int Int64BlockSize = 64;
        protected const int UInt64BlockSize = 64;


        protected readonly List<bool> PatternValues = new List<bool>();

        public int Count { get { return PatternValues.Count; } }

        public bool this[int i]
        {
            get
            {
                return PatternValues[i];
            }
        }


        protected BooleanPattern(int size)
        {
            PatternValues.Capacity = size;
        }

        public BooleanPattern(int size, bool value)
        {
            PatternValues.Capacity = size;

            for (var i = 0; i < size; i++)
                PatternValues.Add(value);
        }

        public BooleanPattern(IEnumerable<bool> patternValues)
        {
            foreach (var value in patternValues)
                PatternValues.Add(value);
        }


        public int TrueCount
        {
            get
            {
                return PatternValues.Count(value => value);
            }
        }

        public int FalseCount
        {
            get
            {
                return PatternValues.Count(value => !value);
            }
        }

        public int FirstTrueIndex
        {
            get
            {
                var count = 0;

                foreach (var value in PatternValues)
                    if (value)
                        return count;
                    else
                        count++;

                return -1;
            }
        }

        public int FirstFalseIndex
        {
            get
            {
                var count = 0;

                foreach (var value in PatternValues)
                    if (!value)
                        return count;
                    else
                        count++;

                return -1;
            }
        }

        public IEnumerable<int> TrueIndexes
        {
            get
            {
                for (var i = 0; i < PatternValues.Count; i++)
                    if (PatternValues[i])
                        yield return i;
            }
        }

        public IEnumerable<int> FalseIndexes
        {
            get
            {
                for (var i = 0; i < PatternValues.Count; i++)
                    if (!PatternValues[i])
                        yield return i;
            }
        }


        public bool AnyTrue
        {
            get { return PatternValues.Any(value => value); }
        }

        public bool AnyFalse
        {
            get { return PatternValues.Any(value => value == false); }
        }

        public bool AllTrue
        {
            get { return PatternValues.All(value => value); }
        }

        public bool AllFalse
        {
            get { return PatternValues.All(value => !value); }
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var pattern2 = obj as BooleanPattern;

            if (ReferenceEquals(pattern2, null) || pattern2.Count != Count)
                return false;

            for (var i = 0; i < Count; i++)
                if (PatternValues[i] != pattern2.PatternValues[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            return 
                ToUInt64Enumerator()
                .Aggregate(0, (current, buffer) => current ^ buffer.GetHashCode());
        }


        public Int32 ToInt32()
        {
            if (Count > Int32BlockSize)
                throw new InvalidOperationException();

            var buffer = 0;

            for (var i = 0; i < Count; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public Int32 ToInt32(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length > Int32BlockSize || length < 1)
                throw new InvalidOperationException();

            var buffer = 0;

            for (var i = 0; i < length; i++)
            {
                if (PatternValues[i + startIndex])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public UInt32 ToUInt32()
        {
            if (Count > UInt32BlockSize)
                throw new InvalidOperationException();

            UInt32 buffer = 0;

            for (var i = 0; i < Count; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1u;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public UInt32 ToUInt32(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length > UInt32BlockSize || length < 1)
                throw new InvalidOperationException();

            UInt32 buffer = 0;

            for (var i = 0; i < length; i++)
            {
                if (PatternValues[i + startIndex])
                    buffer = buffer | 1u;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public Int64 ToInt64()
        {
            if (Count > Int64BlockSize)
                throw new InvalidOperationException();

            Int64 buffer = 0;

            for (var i = 0; i < Count; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1L;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public Int64 ToInt64(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length > Int64BlockSize || length < 1)
                throw new InvalidOperationException();

            Int64 buffer = 0;

            for (var i = 0; i < length; i++)
            {
                if (PatternValues[i + startIndex])
                    buffer = buffer | 1L;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public UInt64 ToUInt64()
        {
            if (Count > UInt64BlockSize)
                throw new InvalidOperationException();

            UInt64 buffer = 0;

            for (var i = 0; i < Count; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1UL;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public UInt64 ToUInt64(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length > UInt64BlockSize || length < 1)
                throw new InvalidOperationException();

            UInt64 buffer = 0;

            for (var i = 0; i < length; i++)
            {
                if (PatternValues[i + startIndex])
                    buffer = buffer | 1UL;

                buffer = buffer << 1;
            }

            return buffer;
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            foreach (var value in PatternValues)
                s.Append(value ? "1" : "0");

            return s.ToString();
        }

        public string ToString(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var s = new StringBuilder();

            for (var i = 0; i < length; i++)
                s.Append(PatternValues[i + startIndex] ? "1" : "0");

            return s.ToString();
        }

        public string ToString(string trueValue, string falseValue)
        {
            var s = new StringBuilder();

            foreach (var value in PatternValues)
                s.Append(value ? trueValue : falseValue);

            return s.ToString();
        }

        public string ToString(int startIndex, int length, string trueValue, string falseValue)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var s = new StringBuilder();

            for (var i = 0; i < length; i++)
                s.Append(PatternValues[i + startIndex] ? trueValue : falseValue);

            return s.ToString();
        }

        public List<T> ToList<T>(T trueValue, T falseValue)
        {
            var list = new List<T>(Count);
            
            list.AddRange(PatternValues.Select(value => value ? trueValue : falseValue));

            return list;
        }

        public List<T> ToList<T>(int startIndex, int length, T trueValue, T falseValue)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var list = new List<T>(Count);

            for (var i = 0; i < length; i++)
                list.Add(PatternValues[i + startIndex] ? trueValue : falseValue);

            return list;
        }

        public List<bool> ToList()
        {
            var list = new List<bool>(Count);
            
            list.AddRange(PatternValues);

            return list;
        }

        public List<bool> ToList(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var list = new List<bool>(Count);

            for (var i = 0; i < length; i++)
                list.Add(PatternValues[i + startIndex]);

            return list;
        }

        public BitArray ToBitArray()
        {
            return new BitArray(PatternValues.ToArray());
        }

        public BitArray ToBitArray(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var result = new BitArray(length);

            for (var i = 0; i < length; i++)
                if (PatternValues[i + startIndex])
                    result[i] = true;

            return result;
        }

        public bool[] ToArray()
        {
            return PatternValues.ToArray();
        }

        public bool[] ToArray(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var result = new bool[length];

            for (var i = 0; i < length; i++)
                if (PatternValues[i + startIndex])
                    result[i] = true;

            return result;
        }


        public IEnumerable<bool> ToBooleanEnumerator()
        {
            return PatternValues;
        }

        public IEnumerable<bool> ToBooleanEnumerator(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            for (var i = 0; i < length; i++)
                yield return PatternValues[i + startIndex];
        }

        public IEnumerable<Int32> ToInt32Enumerator()
        {
            var blockCount = Count / Int32BlockSize;
            var lastBlockSize = Count % Int32BlockSize;

            Int32 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = blockIndex * Int32BlockSize;
                lastIndex = firstIndex + Int32BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = blockCount * Int32BlockSize;
            lastIndex = firstIndex + Int32BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }

        public IEnumerable<Int32> ToInt32Enumerator(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var blockCount = length / Int32BlockSize;
            var lastBlockSize = length % Int32BlockSize;

            Int32 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = startIndex + blockIndex * Int32BlockSize;
                lastIndex = firstIndex + Int32BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = startIndex + blockCount * Int32BlockSize;
            lastIndex = firstIndex + Int32BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }

        public IEnumerable<UInt32> ToUInt32Enumerator()
        {
            var blockCount = Count / UInt32BlockSize;
            var lastBlockSize = Count % UInt32BlockSize;

            UInt32 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = blockIndex * UInt32BlockSize;
                lastIndex = firstIndex + UInt32BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = blockCount * UInt32BlockSize;
            lastIndex = firstIndex + UInt32BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }

        public IEnumerable<UInt32> ToUInt32Enumerator(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var blockCount = length / UInt32BlockSize;
            var lastBlockSize = length % UInt32BlockSize;

            UInt32 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = startIndex + blockIndex * UInt32BlockSize;
                lastIndex = firstIndex + UInt32BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = startIndex + blockCount * UInt32BlockSize;
            lastIndex = firstIndex + UInt32BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }

        public IEnumerable<Int64> ToInt64Enumerator()
        {
            var blockCount = Count / Int32BlockSize;
            var lastBlockSize = Count % Int32BlockSize;

            Int64 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = blockIndex * Int32BlockSize;
                lastIndex = firstIndex + Int32BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = blockCount * Int32BlockSize;
            lastIndex = firstIndex + Int32BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }

        public IEnumerable<Int64> ToInt64Enumerator(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var blockCount = length / Int64BlockSize;
            var lastBlockSize = length % Int64BlockSize;

            Int64 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = startIndex + blockIndex * Int64BlockSize;
                lastIndex = firstIndex + Int64BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = startIndex + blockCount * Int64BlockSize;
            lastIndex = firstIndex + Int64BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }

        public IEnumerable<UInt64> ToUInt64Enumerator()
        {
            var blockCount = Count / UInt32BlockSize;
            var lastBlockSize = Count % UInt32BlockSize;

            UInt64 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = blockIndex * UInt64BlockSize;
                lastIndex = firstIndex + UInt64BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = blockCount * UInt64BlockSize;
            lastIndex = firstIndex + UInt64BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }

        public IEnumerable<UInt64> ToUInt64Enumerator(int startIndex, int length)
        {
            length = Math.Min(Count - startIndex, length);

            if (length < 1)
                throw new InvalidOperationException();

            var blockCount = length / UInt64BlockSize;
            var lastBlockSize = length % UInt64BlockSize;

            UInt64 buffer;

            int firstIndex;
            int lastIndex;

            for (var blockIndex = 0; blockIndex < blockCount; blockIndex++)
            {
                buffer = 0;

                firstIndex = startIndex + blockIndex * UInt64BlockSize;
                lastIndex = firstIndex + UInt64BlockSize;

                for (var i = firstIndex; i < lastIndex; i++)
                {
                    if (PatternValues[i])
                        buffer = buffer | 1;

                    buffer = buffer << 1;
                }

                yield return buffer;
            }

            if (lastBlockSize <= 0) 
                yield break;

            buffer = 0;

            firstIndex = startIndex + blockCount * UInt64BlockSize;
            lastIndex = firstIndex + UInt64BlockSize;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                if (PatternValues[i])
                    buffer = buffer | 1;

                buffer = buffer << 1;
            }

            yield return buffer;
        }


        public IEnumerator<bool> GetEnumerator()
        {
            return PatternValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return PatternValues.GetEnumerator();
        }


        public static bool operator ==(BooleanPattern pattern1, BooleanPattern pattern2)
        {
            if (ReferenceEquals(pattern1, null) || ReferenceEquals(pattern2, null))
                throw new ArgumentNullException();

            if (ReferenceEquals(pattern1, pattern2))
                return true;

            if (pattern1.Count != pattern2.Count)
                throw new InvalidOperationException();

            //var result = new BooleanPattern(pattern1.Count);

            return !pattern1.Where((t, i) => t != pattern2[i]).Any();
        }

        public static bool operator !=(BooleanPattern pattern1, BooleanPattern pattern2)
        {
            if (ReferenceEquals(pattern1, null) || ReferenceEquals(pattern2, null))
                throw new ArgumentNullException();

            if (ReferenceEquals(pattern1, pattern2))
                return false;

            if (pattern1.Count != pattern2.Count)
                throw new InvalidOperationException();

            //var result = new BooleanPattern(pattern1.Count);

            return pattern1.Where((t, i) => t != pattern2[i]).Any();
        }

        public static BooleanPattern operator &(BooleanPattern pattern1, BooleanPattern pattern2)
        {
            if (pattern1.Count != pattern2.Count)
                throw new InvalidOperationException("Pattern lengths mismatch");

            var result = new BooleanPattern(pattern1.Count);

            for (var i = 0; i < pattern1.Count; i++)
                result.PatternValues.Add(pattern1[i] & pattern2[i]);

            return result;
        }

        public static BooleanPattern operator |(BooleanPattern pattern1, BooleanPattern pattern2)
        {
            if (pattern1.Count != pattern2.Count)
                throw new InvalidOperationException("Pattern lengths mismatch");

            var result = new BooleanPattern(pattern1.Count);

            for (var i = 0; i < pattern1.Count; i++)
                result.PatternValues.Add(pattern1[i] | pattern2[i]);

            return result;
        }

        public static BooleanPattern operator ^(BooleanPattern pattern1, BooleanPattern pattern2)
        {
            if (pattern1.Count != pattern2.Count)
                throw new InvalidOperationException("Pattern lengths mismatch");

            var result = new BooleanPattern(pattern1.Count);

            for (var i = 0; i < pattern1.Count; i++)
                result.PatternValues.Add(pattern1[i] ^ pattern2[i]);

            return result;
        }

        public static BooleanPattern operator !(BooleanPattern pattern1)
        {
            var result = new BooleanPattern(pattern1.Count);

            foreach (var t in pattern1)
                result.PatternValues.Add(!t);

            return result;
        }


        public static BooleanPattern CreateFromInt32(int size, Int32 pattern)
        {
            if (size > Int32BlockSize)
                throw new InvalidOperationException();

            var result = new BooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1 << i)) != 0);

            return result;
        }

        public static BooleanPattern CreateFromUInt32(int size, UInt32 pattern)
        {
            if (size > UInt32BlockSize)
                throw new InvalidOperationException();

            var result = new BooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1u << i)) != 0);

            return result;
        }

        public static BooleanPattern CreateFromInt64(int size, Int64 pattern)
        {
            if (size > Int64BlockSize)
                throw new InvalidOperationException();

            var result = new BooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1L << i)) != 0);

            return result;
        }

        public static BooleanPattern CreateFromUInt64(int size, UInt64 pattern)
        {
            if (size > UInt64BlockSize)
                throw new InvalidOperationException();

            var result = new BooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1UL << i)) != 0);

            return result;
        }

        public static BooleanPattern CreateFromTrueIndexes(int size, IEnumerable<int> indexes)
        {
            var result = new BooleanPattern(size, false);

            foreach (var i in indexes)
                result.PatternValues[i] = true;

            return result;
        }

        public static BooleanPattern CreateFromFalseIndexes(int size, IEnumerable<int> indexes)
        {
            var result = new BooleanPattern(size, true);

            foreach (var i in indexes)
                result.PatternValues[i] = false;

            return result;
        }
    }
}
