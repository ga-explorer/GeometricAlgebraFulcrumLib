using System;
using System.Collections.Generic;

namespace DataStructuresLib.BooleanPattern
{
    public sealed class MutableBooleanPattern : BooleanPattern
    {
        private MutableBooleanPattern(int size)
            : base(size)
        {
        }

        public MutableBooleanPattern(int size, bool value)
            : base(size, value)
        {
        }

        public MutableBooleanPattern(IEnumerable<bool> patternValues)
            : base(patternValues)
        {
        }


        public new bool this[int i]
        {
            get
            {
                return PatternValues[i];
            }
            set
            {
                PatternValues[i] = value;
            }
        }


        public MutableBooleanPattern Reset()
        {
            PatternValues.Clear();

            return this;
        }

        public MutableBooleanPattern Reset(int size, bool value)
        {
            PatternValues.Clear();
            PatternValues.Capacity = size;

            for (var i = 0; i < size; i++)
                PatternValues.Add(value);

            return this;
        }

        public MutableBooleanPattern Reset(IEnumerable<bool> patternValues)
        {
            PatternValues.Clear();

            foreach (var value in patternValues)
                PatternValues.Add(value);

            return this;
        }


        public MutableBooleanPattern Append(bool value)
        {
            PatternValues.Add(value);

            return this;
        }

        public MutableBooleanPattern Append(int size, bool value)
        {
            PatternValues.Capacity += size;

            for (var i = 0; i < size; i++)
                PatternValues.Add(value);

            return this;
        }

        public MutableBooleanPattern Append(IEnumerable<bool> patternValues)
        {
            foreach (var value in patternValues)
                PatternValues.Add(value);

            return this;
        }


        public MutableBooleanPattern SetAll(bool value)
        {
            for (var i = 0; i < PatternValues.Count; i++)
                PatternValues[i] = value;

            return this;
        }

        public MutableBooleanPattern SetAll(IEnumerable<int> indexes, bool value)
        {
            foreach (var i in indexes)
                PatternValues[i] = value;

            return this;
        }


        public MutableBooleanPattern AndWith(BooleanPattern pattern)
        {
            if (Count != pattern.Count)
                throw new InvalidOperationException();

            for (var i = 0; i < Count; i++)
                PatternValues[i] = PatternValues[i] & pattern[i];

            return this;
        }

        public MutableBooleanPattern OrWith(BooleanPattern pattern)
        {
            if (Count != pattern.Count)
                throw new InvalidOperationException();

            for (var i = 0; i < Count; i++)
                PatternValues[i] = PatternValues[i] | pattern[i];

            return this;
        }

        public MutableBooleanPattern XorWith(BooleanPattern pattern)
        {
            if (Count != pattern.Count)
                throw new InvalidOperationException();

            for (var i = 0; i < Count; i++)
                PatternValues[i] = PatternValues[i] ^ pattern[i];

            return this;
        }

        public MutableBooleanPattern Not()
        {
            for (var i = 0; i < Count; i++)
                PatternValues[i] = !PatternValues[i];

            return this;
        }


        public new static MutableBooleanPattern CreateFromInt32(int size, Int32 pattern)
        {
            if (size > 32)
                throw new InvalidOperationException();

            var result = new MutableBooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1 << i)) != 0);

            return result;
        }

        public new static MutableBooleanPattern CreateFromUInt32(int size, UInt32 pattern)
        {
            if (size > 32)
                throw new InvalidOperationException();

            var result = new MutableBooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1u << i)) != 0);

            return result;
        }

        public new static MutableBooleanPattern CreateFromInt64(int size, Int64 pattern)
        {
            if (size > 64)
                throw new InvalidOperationException();

            var result = new MutableBooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1L << i)) != 0);

            return result;
        }

        public new static MutableBooleanPattern CreateFromUInt64(int size, UInt64 pattern)
        {
            if (size > 64)
                throw new InvalidOperationException();

            var result = new MutableBooleanPattern(size);

            for (var i = 0; i < size; i++)
                result.PatternValues.Add((pattern & (1UL << i)) != 0);

            return result;
        }

        public new static MutableBooleanPattern CreateFromTrueIndexes(int size, IEnumerable<int> indexes)
        {
            var result = new MutableBooleanPattern(size, false);

            foreach (var i in indexes)
                result.PatternValues[i] = true;

            return result;
        }

        public new static MutableBooleanPattern CreateFromFalseIndexes(int size, IEnumerable<int> indexes)
        {
            var result = new MutableBooleanPattern(size, true);

            foreach (var i in indexes)
                result.PatternValues[i] = false;

            return result;
        }
    }
}
