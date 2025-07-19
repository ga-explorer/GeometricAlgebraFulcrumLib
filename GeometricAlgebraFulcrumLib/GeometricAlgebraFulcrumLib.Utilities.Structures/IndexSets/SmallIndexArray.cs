using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets
{
    public readonly struct SmallIndexArray :
        IReadOnlyList<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SmallIndexArray Create(params int[] indexArray)
        {
            return indexArray.Length switch
            {
                0 => new SmallIndexArray(),
                1 => new SmallIndexArray(indexArray[0]),
                2 => new SmallIndexArray(indexArray[0], indexArray[1]),
                3 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2]),
                4 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3]),
                5 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4]),
                6 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5]),
                7 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5], indexArray[6]),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SmallIndexArray Create(Span<int> indexArray)
        {
            return indexArray.Length switch
            {
                0 => new SmallIndexArray(),
                1 => new SmallIndexArray(indexArray[0]),
                2 => new SmallIndexArray(indexArray[0], indexArray[1]),
                3 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2]),
                4 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3]),
                5 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4]),
                6 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5]),
                7 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5], indexArray[6]),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SmallIndexArray Create(ReadOnlySpan<int> indexArray)
        {
            return indexArray.Length switch
            {
                0 => new SmallIndexArray(),
                1 => new SmallIndexArray(indexArray[0]),
                2 => new SmallIndexArray(indexArray[0], indexArray[1]),
                3 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2]),
                4 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3]),
                5 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4]),
                6 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5]),
                7 => new SmallIndexArray(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5], indexArray[6]),
                _ => throw new NotSupportedException()
            };
        }
        

        private const int Mask2SizeX1 = 32;
        private const ulong Mask2 = (1UL << Mask2SizeX1) - 1;

        private const int Mask3SizeX1 = 21;
        private const int Mask3SizeX2 = Mask3SizeX1 * 2;
        private const ulong Mask3 = (1UL << Mask3SizeX1) - 1;

        private const int Mask4SizeX1 = 16;
        private const int Mask4SizeX2 = Mask4SizeX1 * 2;
        private const int Mask4SizeX3 = Mask4SizeX1 * 3;
        private const ulong Mask4 = (1UL << Mask4SizeX1) - 1;
        
        private const int Mask5SizeX1 = 12;
        private const int Mask5SizeX2 = Mask5SizeX1 * 2;
        private const int Mask5SizeX3 = Mask5SizeX1 * 3;
        private const int Mask5SizeX4 = Mask5SizeX1 * 4;
        private const ulong Mask5 = (1UL << Mask5SizeX1) - 1;
        
        private const int Mask6SizeX1 = 10;
        private const int Mask6SizeX2 = Mask6SizeX1 * 2;
        private const int Mask6SizeX3 = Mask6SizeX1 * 3;
        private const int Mask6SizeX4 = Mask6SizeX1 * 4;
        private const int Mask6SizeX5 = Mask6SizeX1 * 5;
        private const ulong Mask6 = (1UL << Mask6SizeX1) - 1;
        
        private const int Mask7SizeX1 = 9;
        private const int Mask7SizeX2 = Mask7SizeX1 * 2;
        private const int Mask7SizeX3 = Mask7SizeX1 * 3;
        private const int Mask7SizeX4 = Mask7SizeX1 * 4;
        private const int Mask7SizeX5 = Mask7SizeX1 * 5;
        private const int Mask7SizeX6 = Mask7SizeX1 * 6;
        private const ulong Mask7 = (1UL << Mask7SizeX1) - 1;


        private readonly ulong _value;

        public int Count { get; }

        public int Length 
            => Count;

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                return Count switch
                {
                    1 => (int)_value,

                    2 => index == 0 
                        ? (int)(_value & Mask2) 
                        : (int)(_value >> Mask2SizeX1),

                    3 => index switch
                    {
                        0 => (int)(_value & Mask3),
                        1 => (int)((_value >> Mask3SizeX1) & Mask3),
                        _ => (int)(_value >> Mask3SizeX2)
                    },

                    4 => index switch
                    {
                        0 => (int)(_value & Mask4),
                        1 => (int)((_value >> Mask4SizeX1) & Mask4),
                        2 => (int)((_value >> Mask4SizeX2) & Mask4),
                        _ => (int)(_value >> Mask4SizeX3)
                    },
                    
                    5 => index switch
                    {
                        0 => (int)(_value & Mask5),
                        1 => (int)((_value >> Mask5SizeX1) & Mask5),
                        2 => (int)((_value >> Mask5SizeX2) & Mask5),
                        3 => (int)((_value >> Mask5SizeX3) & Mask5),
                        _ => (int)(_value >> Mask5SizeX4)
                    },
                    
                    6 => index switch
                    {
                        0 => (int)(_value & Mask6),
                        1 => (int)((_value >> Mask6SizeX1) & Mask6),
                        2 => (int)((_value >> Mask6SizeX2) & Mask6),
                        3 => (int)((_value >> Mask6SizeX3) & Mask6),
                        4 => (int)((_value >> Mask6SizeX4) & Mask6),
                        _ => (int)(_value >> Mask6SizeX5)
                    },
                    
                    7 => index switch
                    {
                        0 => (int)(_value & Mask7),
                        1 => (int)((_value >> Mask7SizeX1) & Mask7),
                        2 => (int)((_value >> Mask7SizeX2) & Mask7),
                        3 => (int)((_value >> Mask7SizeX3) & Mask7),
                        4 => (int)((_value >> Mask7SizeX4) & Mask7),
                        5 => (int)((_value >> Mask7SizeX5) & Mask7),
                        _ => (int)(_value >> Mask7SizeX6)
                    },

                    _ => throw new NotSupportedException()
                };
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray()
        {
            Count = 0;
            _value = 0UL;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray(int i1)
        {
            Debug.Assert(i1 >= 0);

            Count = 1;
            _value = (ulong)i1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray(int i1, int i2)
        {
            Debug.Assert(i1 >= 0 && i2 >= 0);

            Count = 2;
            _value = 
                (uint)i1 | 
                ((ulong)i2 << Mask2SizeX1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray(int i1, int i2, int i3)
        {
            Debug.Assert(i1 >= 0 && i2 >= 0 && i3 >= 0);

            Count = 3;
            _value = 
                (uint)i1 | 
                ((ulong)i2 << Mask3SizeX1) | 
                ((ulong)i3 << Mask3SizeX2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray(int i1, int i2, int i3, int i4)
        {
            Debug.Assert(i1 >= 0 && i2 >= 0 && i3 >= 0 && i4 >= 0);

            Count = 4;
            _value = 
                (uint)i1 | 
                ((ulong)i2 << Mask4SizeX1) | 
                ((ulong)i3 << Mask4SizeX2) | 
                ((ulong)i4 << Mask4SizeX3);
            
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray(int i1, int i2, int i3, int i4, int i5)
        {
            Debug.Assert(i1 >= 0 && i2 >= 0 && i3 >= 0 && i4 >= 0 && i5 >= 0);

            Count = 5;
            _value = 
                (uint)i1 | 
                ((ulong)i2 << Mask5SizeX1) | 
                ((ulong)i3 << Mask5SizeX2) | 
                ((ulong)i4 << Mask5SizeX3) | 
                ((ulong)i5 << Mask5SizeX4);
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray(int i1, int i2, int i3, int i4, int i5, int i6)
        {
            Debug.Assert(i1 >= 0 && i2 >= 0 && i3 >= 0 && i4 >= 0 && i5 >= 0 && i6 >= 0);

            Count = 6;
            _value = 
                (uint)i1 | 
                ((ulong)i2 << Mask6SizeX1) | 
                ((ulong)i3 << Mask6SizeX2) | 
                ((ulong)i4 << Mask6SizeX3) | 
                ((ulong)i5 << Mask6SizeX4) | 
                ((ulong)i6 << Mask6SizeX5);
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray(int i1, int i2, int i3, int i4, int i5, int i6, int i7)
        {
            Debug.Assert(i1 >= 0 && i2 >= 0 && i3 >= 0 && i4 >= 0 && i5 >= 0 && i6 >= 0 && i7 >= 0);

            Count = 7;
            _value = 
                (uint)i1 | 
                ((ulong)i2 << Mask7SizeX1) | 
                ((ulong)i3 << Mask7SizeX2) | 
                ((ulong)i4 << Mask7SizeX3) | 
                ((ulong)i5 << Mask7SizeX4) | 
                ((ulong)i6 << Mask7SizeX5) | 
                ((ulong)i7 << Mask7SizeX6);
            
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SmallIndexArray ShiftIndices(int offset)
        {
            return Count switch
            {
                0 => this,

                1 => new SmallIndexArray(
                    (int)_value + offset
                ),
                
                2 => new SmallIndexArray(
                    (int)(_value & Mask2) + offset, 
                    (int)(_value >> Mask2SizeX1) + offset
                ),
                
                3 => new SmallIndexArray(
                    (int)(_value & Mask3) + offset,
                    (int)((_value >> Mask3SizeX1) & Mask3) + offset, 
                    (int)(_value >> Mask3SizeX2) + offset
                ),
                
                4 => new SmallIndexArray(
                    (int)(_value & Mask4) + offset, 
                    (int)((_value >> Mask4SizeX1) & Mask4) + offset,
                    (int)((_value >> Mask4SizeX2) & Mask4) + offset, 
                    (int)(_value >> Mask4SizeX3) + offset
                ),
                
                5 => new SmallIndexArray(
                    (int)(_value & Mask5) + offset, 
                    (int)((_value >> Mask5SizeX1) & Mask5) + offset,
                    (int)((_value >> Mask5SizeX2) & Mask5) + offset, 
                    (int)((_value >> Mask5SizeX3) & Mask5) + offset, 
                    (int)(_value >> Mask5SizeX4) + offset
                ),
                
                6 => new SmallIndexArray(
                    (int)(_value & Mask6) + offset, 
                    (int)((_value >> Mask6SizeX1) & Mask6) + offset,
                    (int)((_value >> Mask6SizeX2) & Mask6) + offset, 
                    (int)((_value >> Mask6SizeX3) & Mask6) + offset, 
                    (int)((_value >> Mask6SizeX4) & Mask6) + offset, 
                    (int)(_value >> Mask6SizeX5) + offset
                ),
                
                7 => new SmallIndexArray(
                    (int)(_value & Mask7) + offset, 
                    (int)((_value >> Mask7SizeX1) & Mask7) + offset,
                    (int)((_value >> Mask7SizeX2) & Mask7) + offset, 
                    (int)((_value >> Mask7SizeX3) & Mask7) + offset, 
                    (int)((_value >> Mask7SizeX4) & Mask7) + offset, 
                    (int)((_value >> Mask7SizeX5) & Mask7) + offset, 
                    (int)(_value >> Mask7SizeX6) + offset
                ),

                _ => throw new NotSupportedException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<int> GetEnumerator()
        {
            if (Count == 0) yield break;

            if (Count == 1)
            {
                yield return (int)_value;
            }
            else if (Count == 2)
            {
                yield return (int)(_value & Mask2);
                yield return (int)(_value >> Mask2SizeX1);
            }
            else if (Count == 3)
            {
                
                yield return (int)(_value & Mask3);
                yield return (int)((_value >> Mask3SizeX1) & Mask3);
                yield return (int)(_value >> Mask3SizeX2);
            }
            else if (Count == 4)
            {
                yield return (int)(_value & Mask4);
                yield return (int)((_value >> Mask4SizeX1) & Mask4);
                yield return (int)((_value >> Mask4SizeX2) & Mask4);
                yield return (int)(_value >> Mask4SizeX3);
            }
            else if (Count == 5)
            {
                yield return (int)(_value & Mask5);
                yield return (int)((_value >> Mask5SizeX1) & Mask5);
                yield return (int)((_value >> Mask5SizeX2) & Mask5);
                yield return (int)((_value >> Mask5SizeX3) & Mask5);
                yield return (int)(_value >> Mask5SizeX4);
            }
            else if (Count == 6)
            {
                yield return (int)(_value & Mask6);
                yield return (int)((_value >> Mask6SizeX1) & Mask6);
                yield return (int)((_value >> Mask6SizeX2) & Mask6);
                yield return (int)((_value >> Mask6SizeX3) & Mask6);
                yield return (int)((_value >> Mask6SizeX4) & Mask6);
                yield return (int)(_value >> Mask6SizeX5);
            }
            else if (Count == 7)
            {
                yield return (int)(_value & Mask7);
                yield return (int)((_value >> Mask7SizeX1) & Mask7);
                yield return (int)((_value >> Mask7SizeX2) & Mask7);
                yield return (int)((_value >> Mask7SizeX3) & Mask7);
                yield return (int)((_value >> Mask7SizeX4) & Mask7);
                yield return (int)((_value >> Mask7SizeX5) & Mask7);
                yield return (int)(_value >> Mask7SizeX6);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
