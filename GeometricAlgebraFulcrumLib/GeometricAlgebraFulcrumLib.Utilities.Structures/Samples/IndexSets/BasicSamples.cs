using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using System.Numerics;
using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Samples.IndexSets
{
    public static class BasicSamples
    {
        //private static string ToText(IReadOnlyList<int> intArray)
        //{
        //    return "[" + intArray
        //        .Select(i => i.ToString())
        //        .ToConcatenatedString(s => s, ", ") + "]";
        //}

        private static TimeSpan GetTime(Action sub, int n)
        {
            var t1 = DateTime.Now;

            for (var i = 0; i < n; i++)
                sub();

            var t2 = DateTime.Now;

            return t2 - t1;
        }


        public static void CreationExample()
        {
            Console.WriteLine(IndexSet.Create(3));
            Console.WriteLine(IndexSet.Create(65));
            Console.WriteLine(IndexSet.Create(3, 29));
            Console.WriteLine(IndexSet.Create(9, 98));
            Console.WriteLine(IndexSet.Create(3, 5, 18));
            Console.WriteLine(IndexSet.Create(13, 22, 70));
            Console.WriteLine(IndexSet.CreateDense(13, 5));
            Console.WriteLine(IndexSet.CreateDense(58, 5));
        }

        public static void IteratorExample1()
        {
            var indexSet = IndexSet.Create(3, 4, 7, 8, 9, 12, 22, 33, 63);

            Console.WriteLine(indexSet.ToString());

            var span = indexSet.AsSpan();
            var count = span.Length;
            var index = 0;

            while (index < count)
            {
                Console.WriteLine($"[{index:D2}]: {span[index]},");

                index++;
            }

            Console.WriteLine();
        }
        
        public static void IteratorExample2()
        {
            //var indexSet = IndexSet.Create(3, 4, 7, 8, 9, 12, 22, 33, 63);
            var indexSet1 = IndexSet.CreateDense(0, 64);
            var indexSet2 = IndexSet.CreateDense(58, 64);

            Console.WriteLine(indexSet1.ToString());
            Console.WriteLine(indexSet2.ToString());
            Console.WriteLine();

            Console.WriteLine(Sum(indexSet1));
            Console.WriteLine(Sum(indexSet2));
            Console.WriteLine();

            Console.WriteLine(GetTime(() => Sum(indexSet1), 10000000).ToString());
            Console.WriteLine(GetTime(() => Sum(indexSet2), 10000000).ToString());
            Console.WriteLine();
            
            return;

            int Sum(IndexSet idxSet)
            {
                var s = 0;
                var span = idxSet.AsSpan();

                foreach (var idx in span)
                    s += idx;
                
                return s;
            }
        }
        
        public static void IteratorExample3()
        {
            //var indexSet = IndexSet.Create(3, 4, 7, 8, 9, 12, 22, 33, 63);
            //var indexSet = IndexSet.CreateDense(0, 64);

            //Console.WriteLine(indexSet.ToString());
            //Console.WriteLine();

            for (var n = 0; n < 64; n++)
            {
                var indexSet = IndexSet.CreateDense(0, n);

                var bitPattern = indexSet.ToUInt64();
                var indexArray = indexSet.GetIndices().ToArray();

                Debug.Assert(Sum1(bitPattern) == Sum2(indexArray));

                var t1 = GetTime(() => Sum1(bitPattern), 10000000);
                var t2 = GetTime(() => Sum2(indexArray), 10000000);

                Console.WriteLine($"n: {n:D2}, t1: {t1}, t2: {t2}, t1/t2: {t1.TotalSeconds/t2.TotalSeconds}");
                //Console.WriteLine();
            }
            
            return;
            
            int Sum1(ulong idxSet)
            {
                var s = 0;
                while (idxSet != 0)
                {
                    var idx = BitOperations.TrailingZeroCount(idxSet);
                    idxSet &= idxSet - 1;

                    s += idx;
                }
                
                return s;
            }

            int Sum2(int[] idxSet)
            {
                var s = 0;
                foreach (var idx in idxSet)
                    s += idx;
                
                return s;
            }
        }
        
        public static void IteratorExample4()
        {
            const ulong maxId = (1UL << 26) - 1;

            var indexArrays = 
                ((int)maxId).GetRange().Select(id => id.PatternToPositions().ToArray()).ToList();

            var t1 = GetTime(() =>
            {
                for (var bitPattern = 0UL; bitPattern < maxId; bitPattern++)
                    Sum1(bitPattern);
            }, 1);
            
            var t2 = GetTime(() =>
            {
                foreach (var indexArray in indexArrays)
                    Sum2(indexArray);
            }, 1);
            
            //var t3 = GetTime(() =>
            //{
            //    Span<int> buffer = stackalloc int[64];

            //    for (var bitPattern = 0UL; bitPattern < maxId; bitPattern++)
            //    {
            //        var value = bitPattern;
            //        var count = 0;

            //        while (value != 0)
            //        {
            //            buffer[count++] = BitOperations.TrailingZeroCount(value);
            //            value &= value - 1; // Clear the least significant bit
            //        }

            //        Sum3(buffer[..count]);
            //        //Sum2(bitPattern.GetSetBitPositionsAsArray());
            //    }
            //}, 1);

            Console.WriteLine($"Bit-pattern iteration time t1: {t1}, Index array iteration time t2: {t2}, t1/t2: {t1.TotalSeconds/t2.TotalSeconds}");
            
            return;
            
            int Sum1(ulong idxSet)
            {
                var s = 0;
                while (idxSet != 0)
                {
                    var idx = BitOperations.TrailingZeroCount(idxSet);
                    idxSet &= idxSet - 1;

                    s += idx;
                }
                
                return s;
            }

            int Sum2(int[] idxSet)
            {
                var s = 0;
                foreach (var idx in idxSet)
                    s += idx;
                
                return s;
            }
            
            int Sum3(Span<int> idxSet)
            {
                var s = 0;
                foreach (var idx in idxSet)
                    s += idx;
                
                return s;
            }
        }
        
        public static void GetHashCodeExample()
        {
            var indexSet = IndexSet.Create(3, 4, 7, 8, 9, 12, 22, 33, 63);

            Console.WriteLine(indexSet.ToString());

            var hashCode1 = 0;
            var bitPattern = indexSet.ToUInt64();
            while (bitPattern != 0)
            {
                var index = BitOperations.TrailingZeroCount(bitPattern);
                bitPattern &= bitPattern - 1;

                hashCode1 ^= index;
            }

            Debug.Assert(hashCode1 == indexSet.GetHashCode());

            var hashCode2 = 0;
            var indexArray = indexSet.GetIndices();

            foreach (var index in indexArray)
                hashCode2 ^= index;

            Debug.Assert(hashCode2 == indexSet.GetHashCode());
        }

        public static void IndexSearchExample()
        {
            const int offset = 35;

            var indexSet = IndexSet.Create(
                3, 4, 7, 8, 9, 12, 22, 23, 30
            ).ShiftIndices(offset);
            
            Console.WriteLine("Original array: " + indexSet);
            Console.WriteLine();

            for (var i = 0; i <= 32; i++)
            {
                var j = indexSet.TryGetIndexPosition(i + offset);
                var foundText = j >= 0 
                    ? $"Found at {j}" 
                    : $"Not Found (returned {j})";

                Console.WriteLine($"{i + offset:D2}: {foundText}" );
            }

            Console.WriteLine();
        }
        
        public static void InsertExample()
        {
            const int offset = 33;

            var indexSet = IndexSet.Create(
                3, 4, 7, 8, 9, 12, 22, 23, 30
            ).ShiftIndices(offset);

            Console.WriteLine("Original array: " + indexSet);
            Console.WriteLine();

            for (var i = 0; i <= 32; i++)
            {
                var index = i + offset;
                if (indexSet.Contains(index)) continue;

                Console.WriteLine($"Inserted {index:D2}: {indexSet.Insert(index)}" );
            }

            Console.WriteLine();
        }
        
        public static void RemoveExample()
        {
            const int offset = 34;

            var indexSet = IndexSet.Create(
                3, 4, 7, 8, 9, 12, 22, 23, 30
            ).ShiftIndices(offset);

            Console.WriteLine("Original array: " + indexSet);
            Console.WriteLine();

            for (var i = 0; i <= 32; i++)
            {
                var index = i + offset;
                if (!indexSet.Contains(index)) continue;

                Console.WriteLine($"Removed {index:D2}: {indexSet.Remove(index)}" );
            }

            Console.WriteLine();
        }

        public static void TryInsertExample()
        {
            const int offset = 33;

            var indexSet = IndexSet.Create(
                3, 4, 7, 8, 9, 12, 22, 23, 30
            ).ShiftIndices(offset);

            Console.WriteLine("Original array: " + indexSet);
            Console.WriteLine();

            for (var i = 0; i <= 32; i++)
            {
                var index = i + offset;

                var result = indexSet.TryInsert(index);
                var resultText = Equals(result, indexSet) 
                    ? $"Not Inserted: {result}" 
                    : $"    Inserted: {result}";

                Console.WriteLine($"Try insert {index:D2}: {resultText}" );
            }

            Console.WriteLine();
        }
        
        public static void TryRemoveExample()
        {
            const int offset = 34;

            var indexSet = IndexSet.Create(
                3, 4, 7, 8, 9, 12, 22, 23, 30
            ).ShiftIndices(offset);

            Console.WriteLine("Original array: " + indexSet);
            Console.WriteLine();

            for (var i = 0; i <= 32; i++)
            {
                var index = i + offset;

                var result = indexSet.TryRemove(index);
                var resultText = result.Equals(indexSet) 
                    ? $"Not Removed: {result}" 
                    : $"    Removed: {result}";
                
                Console.WriteLine($"Try remove {index:D2}: {resultText}" );
            }

            Console.WriteLine();
        }

        public static void OrderingExample()
        {
            var random = new System.Random(10);

            var indexSetList = 
                10.GetRange().Select(_ =>
                    IndexSet.CreateFromUInt64Pattern((ulong)random.NextInt64()).ShiftIndices(3)
                ).OrderBy(i => i);

            foreach (var indexSet in indexSetList)
                Console.WriteLine(indexSet);
        }

        
        /// <summary>
        /// Never remove this code for future validation and reference
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        private static int CountEGpSwapsRefImp(ulong id1, ulong id2)
        {
            if (id1 == 0UL || id2 == 0UL) return 0;

            var swapCount = 0;
            var id = id1;

            //Find the largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1UL;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1UL;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0)
                            swapCount++;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id ^= mask2;

                mask2 <<= 1;
            }

            return swapCount;
        }
        
        private static bool SetContainsRefImp(IndexSet indexSet1, IndexSet indexSet2)
        {
            var set1 = indexSet1.ToImmutableSortedSet();

            return !indexSet1.IsEmptySet && 
                   !indexSet2.IsEmptySet && 
                   indexSet2.All(i => set1.Contains(i));
        }
        
        private static bool SetOverlapsRefImp(IndexSet indexSet1, IndexSet indexSet2)
        {
            var set1 = indexSet1.ToImmutableSortedSet();

            return !indexSet1.IsEmptySet && 
                   !indexSet2.IsEmptySet && 
                   indexSet2.Any(i => set1.Contains(i));
        }

        private static IndexSet SetIntersectRefImp(IndexSet indexSet1, IndexSet indexSet2)
        {
            return IndexSet.Create(
                indexSet1.ToImmutableSortedSet().Intersect(indexSet2).ToArray()
            );
        }
        
        private static IndexSet SetUnionRefImp(IndexSet indexSet1, IndexSet indexSet2)
        {
            return IndexSet.Create(
                indexSet1.ToImmutableSortedSet().Union(indexSet2).ToArray()
            );
        }
        
        private static IndexSet SetMergeRefImp(IndexSet indexSet1, IndexSet indexSet2)
        {
            return IndexSet.Create(
                indexSet1.ToImmutableSortedSet().SymmetricExcept(indexSet2).ToArray()
            );
        }
        
        private static IndexSet SetDifferenceRefImp(IndexSet indexSet1, IndexSet indexSet2)
        {
            return IndexSet.Create(
                indexSet1.ToImmutableSortedSet().Except(indexSet2).ToArray()
            );
        }

        
        public static void ContainsExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp1 = SetContainsRefImp(indexSetList00[id1], indexSetList15[id2]);
                    var refImp2 = SetContainsRefImp(indexSetList15[id1], indexSetList00[id2]);

                    var mergeSetPP1 =
                        indexSetList00[id1].SetContains(indexSetList15[id2]);
                    
                    var mergeSetPP2 =
                        indexSetList15[id1].SetContains(indexSetList00[id2]);

                    var mergeSetPA =
                        indexSetList00[id1].SetContains(indexSetList85[id2]);
                    
                    var mergeSetAP =
                        indexSetList85[id1].SetContains(indexSetList00[id2]);
                    
                    var mergeSetAA1 =
                        indexSetList70[id1].SetContains(indexSetList85[id2]);
                    
                    var mergeSetAA2 =
                        indexSetList85[id1].SetContains(indexSetList70[id2]);
                    
                    Debug.Assert(refImp1 == mergeSetPP1);
                    Debug.Assert(refImp2 == mergeSetPP2);
                    Debug.Assert(refImp1 == mergeSetPA);
                    Debug.Assert(refImp2 == mergeSetAP);
                    Debug.Assert(refImp1 == mergeSetAA1);
                    Debug.Assert(refImp2 == mergeSetAA2);
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp = SetContainsRefImp(indexSetList00[id1], indexSetList00[id2]);

                    var mergeSet00 =
                        indexSetList00[id1].SetContains(indexSetList00[id2]);
                    
                    var mergeSet58 =
                        indexSetList58[id1].SetContains(indexSetList58[id2]);

                    var mergeSet70 =
                        indexSetList70[id1].SetContains(indexSetList70[id2]);

                    Debug.Assert(refImp == mergeSet00);
                    Debug.Assert(refImp == mergeSet58);
                    Debug.Assert(refImp == mergeSet70);
                }
            }
        }
        
        public static void ContainsExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetContains(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetContains(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetContains(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetContains(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }
        
        public static void OverlapsExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp1 = SetOverlapsRefImp(indexSetList00[id1], indexSetList15[id2]);
                    var refImp2 = SetOverlapsRefImp(indexSetList15[id1], indexSetList00[id2]);

                    var mergeSetPP1 =
                        indexSetList00[id1].SetOverlaps(indexSetList15[id2]);
                    
                    var mergeSetPP2 =
                        indexSetList15[id1].SetOverlaps(indexSetList00[id2]);

                    var mergeSetPA =
                        indexSetList00[id1].SetOverlaps(indexSetList85[id2]);
                    
                    var mergeSetAP =
                        indexSetList85[id1].SetOverlaps(indexSetList00[id2]);
                    
                    var mergeSetAA1 =
                        indexSetList70[id1].SetOverlaps(indexSetList85[id2]);
                    
                    var mergeSetAA2 =
                        indexSetList85[id1].SetOverlaps(indexSetList70[id2]);
                    
                    Debug.Assert(refImp1 == mergeSetPP1);
                    Debug.Assert(refImp2 == mergeSetPP2);
                    Debug.Assert(refImp1 == mergeSetPA);
                    Debug.Assert(refImp2 == mergeSetAP);
                    Debug.Assert(refImp1 == mergeSetAA1);
                    Debug.Assert(refImp2 == mergeSetAA2);
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp = SetOverlapsRefImp(indexSetList00[id1], indexSetList00[id2]);

                    var mergeSet00 =
                        indexSetList00[id1].SetOverlaps(indexSetList00[id2]);
                    
                    var mergeSet58 =
                        indexSetList58[id1].SetOverlaps(indexSetList58[id2]);

                    var mergeSet70 =
                        indexSetList70[id1].SetOverlaps(indexSetList70[id2]);

                    Debug.Assert(refImp == mergeSet00);
                    Debug.Assert(refImp == mergeSet58);
                    Debug.Assert(refImp == mergeSet70);
                }
            }
        }
        
        public static void OverlapsExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetOverlaps(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetOverlaps(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetOverlaps(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetOverlaps(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }
        
        public static void IntersectExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp1 = SetIntersectRefImp(indexSetList00[id1], indexSetList15[id2]);
                    var refImp2 = SetIntersectRefImp(indexSetList15[id1], indexSetList00[id2]);

                    var mergeSetPP1 =
                        indexSetList00[id1].SetIntersect(indexSetList15[id2]);
                    
                    var mergeSetPP2 =
                        indexSetList15[id1].SetIntersect(indexSetList00[id2]);

                    var mergeSetPA =
                        indexSetList00[id1].SetIntersect(indexSetList85[id2]);
                    
                    var mergeSetAP =
                        indexSetList85[id1].SetIntersect(indexSetList00[id2]);
                    
                    var mergeSetAA1 =
                        indexSetList70[id1].SetIntersect(indexSetList85[id2]);
                    
                    var mergeSetAA2 =
                        indexSetList85[id1].SetIntersect(indexSetList70[id2]);
                    
                    Debug.Assert(refImp1 == mergeSetPP1);
                    Debug.Assert(refImp2 == mergeSetPP2);
                    Debug.Assert(refImp1 == mergeSetPA.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAP.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp1 == mergeSetAA1.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAA2.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp = SetIntersectRefImp(indexSetList00[id1], indexSetList00[id2]);

                    var mergeSet00 =
                        indexSetList00[id1].SetIntersect(indexSetList00[id2]);
                    
                    var mergeSet58 =
                        indexSetList58[id1].SetIntersect(indexSetList58[id2]);

                    var mergeSet70 =
                        indexSetList70[id1].SetIntersect(indexSetList70[id2]);

                    Debug.Assert(refImp == mergeSet00);
                    Debug.Assert(refImp == mergeSet58.MapIndicesByValue(i => i >= 58 ? i - 58 : i));
                    Debug.Assert(refImp == mergeSet70.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
        }
        
        public static void IntersectExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetIntersect(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetIntersect(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetIntersect(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetIntersect(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }
        
        public static void UnionExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp1 = SetUnionRefImp(indexSetList00[id1], indexSetList15[id2]);
                    var refImp2 = SetUnionRefImp(indexSetList15[id1], indexSetList00[id2]);

                    var mergeSetPP1 =
                        indexSetList00[id1].SetUnion(indexSetList15[id2]);
                    
                    var mergeSetPP2 =
                        indexSetList15[id1].SetUnion(indexSetList00[id2]);

                    var mergeSetPA =
                        indexSetList00[id1].SetUnion(indexSetList85[id2]);
                    
                    var mergeSetAP =
                        indexSetList85[id1].SetUnion(indexSetList00[id2]);
                    
                    var mergeSetAA1 =
                        indexSetList70[id1].SetUnion(indexSetList85[id2]);
                    
                    var mergeSetAA2 =
                        indexSetList85[id1].SetUnion(indexSetList70[id2]);
                    
                    Debug.Assert(refImp1 == mergeSetPP1);
                    Debug.Assert(refImp2 == mergeSetPP2);
                    Debug.Assert(refImp1 == mergeSetPA.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAP.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp1 == mergeSetAA1.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAA2.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp = SetUnionRefImp(indexSetList00[id1], indexSetList00[id2]);

                    var mergeSet00 =
                        indexSetList00[id1].SetUnion(indexSetList00[id2]);
                    
                    var mergeSet58 =
                        indexSetList58[id1].SetUnion(indexSetList58[id2]);

                    var mergeSet70 =
                        indexSetList70[id1].SetUnion(indexSetList70[id2]);

                    Debug.Assert(refImp == mergeSet00);
                    Debug.Assert(refImp == mergeSet58.MapIndicesByValue(i => i >= 58 ? i - 58 : i));
                    Debug.Assert(refImp == mergeSet70.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
        }
        
        public static void UnionExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetUnion(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetUnion(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetUnion(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetUnion(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }
        
        public static void MergeExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp1 = SetMergeRefImp(indexSetList00[id1], indexSetList15[id2]);
                    var refImp2 = SetMergeRefImp(indexSetList15[id1], indexSetList00[id2]);

                    var mergeSetPP1 =
                        indexSetList00[id1].SetMerge(indexSetList15[id2]);
                    
                    var mergeSetPP2 =
                        indexSetList15[id1].SetMerge(indexSetList00[id2]);

                    var mergeSetPA =
                        indexSetList00[id1].SetMerge(indexSetList85[id2]);
                    
                    var mergeSetAP =
                        indexSetList85[id1].SetMerge(indexSetList00[id2]);
                    
                    var mergeSetAA1 =
                        indexSetList70[id1].SetMerge(indexSetList85[id2]);
                    
                    var mergeSetAA2 =
                        indexSetList85[id1].SetMerge(indexSetList70[id2]);
                    
                    Debug.Assert(refImp1 == mergeSetPP1);
                    Debug.Assert(refImp2 == mergeSetPP2);
                    Debug.Assert(refImp1 == mergeSetPA.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAP.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp1 == mergeSetAA1.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAA2.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp = SetMergeRefImp(indexSetList00[id1], indexSetList00[id2]);

                    var mergeSet00 =
                        indexSetList00[id1].SetMerge(indexSetList00[id2]);
                    
                    var mergeSet58 =
                        indexSetList58[id1].SetMerge(indexSetList58[id2]);

                    var mergeSet70 =
                        indexSetList70[id1].SetMerge(indexSetList70[id2]);

                    Debug.Assert(refImp == mergeSet00);
                    Debug.Assert(refImp == mergeSet58.MapIndicesByValue(i => i >= 58 ? i - 58 : i));
                    Debug.Assert(refImp == mergeSet70.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
        }
        
        public static void MergeExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetMerge(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetMerge(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetMerge(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetMerge(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }
        
        public static void DifferenceExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp1 = SetDifferenceRefImp(indexSetList00[id1], indexSetList15[id2]);
                    var refImp2 = SetDifferenceRefImp(indexSetList15[id1], indexSetList00[id2]);

                    var mergeSetPP1 =
                        indexSetList00[id1].SetDifference(indexSetList15[id2]);
                    
                    var mergeSetPP2 =
                        indexSetList15[id1].SetDifference(indexSetList00[id2]);

                    var mergeSetPA =
                        indexSetList00[id1].SetDifference(indexSetList85[id2]);
                    
                    var mergeSetAP =
                        indexSetList85[id1].SetDifference(indexSetList00[id2]);
                    
                    var mergeSetAA1 =
                        indexSetList70[id1].SetDifference(indexSetList85[id2]);
                    
                    var mergeSetAA2 =
                        indexSetList85[id1].SetDifference(indexSetList70[id2]);
                    
                    Debug.Assert(refImp1 == mergeSetPP1);
                    Debug.Assert(refImp2 == mergeSetPP2);
                    Debug.Assert(refImp1 == mergeSetPA.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAP.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp1 == mergeSetAA1.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(refImp2 == mergeSetAA2.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var refImp = SetDifferenceRefImp(indexSetList00[id1], indexSetList00[id2]);

                    var mergeSet00 =
                        indexSetList00[id1].SetDifference(indexSetList00[id2]);
                    
                    var mergeSet58 =
                        indexSetList58[id1].SetDifference(indexSetList58[id2]);

                    var mergeSet70 =
                        indexSetList70[id1].SetDifference(indexSetList70[id2]);

                    Debug.Assert(refImp == mergeSet00);
                    Debug.Assert(refImp == mergeSet58.MapIndicesByValue(i => i >= 58 ? i - 58 : i));
                    Debug.Assert(refImp == mergeSet70.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
        }
        
        public static void DifferenceExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetDifference(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetDifference(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetDifference(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetDifference(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }
        
        public static void CountSwapsExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var swapCountRefImp1 = CountEGpSwapsRefImp(indexSetList00[id1].ToUInt64(), indexSetList15[id2].ToUInt64());
                    var swapCountRefImp2 = CountEGpSwapsRefImp(indexSetList15[id1].ToUInt64(), indexSetList00[id2].ToUInt64());

                    var swapCountPP1 =
                        indexSetList00[id1].SetCountSwaps(indexSetList15[id2]);
                    
                    var swapCountPP2 =
                        indexSetList15[id1].SetCountSwaps(indexSetList00[id2]);

                    var swapCountPA =
                        indexSetList00[id1].SetCountSwaps(indexSetList85[id2]);
                    
                    var swapCountAP =
                        indexSetList85[id1].SetCountSwaps(indexSetList00[id2]);
                    
                    var swapCountAA1 =
                        indexSetList70[id1].SetCountSwaps(indexSetList85[id2]);
                    
                    var swapCountAA2 =
                        indexSetList85[id1].SetCountSwaps(indexSetList70[id2]);
                    
                    Debug.Assert(swapCountRefImp1 == swapCountPP1);
                    Debug.Assert(swapCountRefImp2 == swapCountPP2);
                    Debug.Assert(swapCountRefImp1 == swapCountPA);
                    Debug.Assert(swapCountRefImp2 == swapCountAP);
                    Debug.Assert(swapCountRefImp1 == swapCountAA1);
                    Debug.Assert(swapCountRefImp2 == swapCountAA2);
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var swapCountRefImp = CountEGpSwapsRefImp(indexSetList00[id1].ToUInt64(), indexSetList00[id2].ToUInt64());
                    
                    var swapCount00 =
                        indexSetList00[id1].SetCountSwaps(indexSetList00[id2]);
                    
                    var swapCount58 =
                        indexSetList58[id1].SetCountSwaps(indexSetList58[id2]);

                    var swapCount70 =
                        indexSetList70[id1].SetCountSwaps(indexSetList70[id2]);

                    Debug.Assert(swapCountRefImp == swapCount00);
                    Debug.Assert(swapCountRefImp == swapCount58);
                    Debug.Assert(swapCountRefImp == swapCount70);
                }
            }
        }

        public static void CountSwapsExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetCountSwaps(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetCountSwaps(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetCountSwaps(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetCountSwaps(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }

        public static void MergeCountSwapsExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var swapCountRefImp1 = CountEGpSwapsRefImp(indexSetList00[id1].ToUInt64(), indexSetList15[id2].ToUInt64());
                    var swapCountRefImp2 = CountEGpSwapsRefImp(indexSetList15[id1].ToUInt64(), indexSetList00[id2].ToUInt64());

                    var mergeSetRefImp1 = IndexSet.CreateFromUInt64Pattern(indexSetList00[id1].ToUInt64() ^ indexSetList15[id2].ToUInt64());
                    var mergeSetRefImp2 = IndexSet.CreateFromUInt64Pattern(indexSetList15[id1].ToUInt64() ^ indexSetList00[id2].ToUInt64());

                    var (swapCountPP1, mergeSetPP1) =
                        indexSetList00[id1].SetMergeCountSwaps(indexSetList15[id2]);
                    
                    var (swapCountPP2, mergeSetPP2) =
                        indexSetList15[id1].SetMergeCountSwaps(indexSetList00[id2]);

                    var (swapCountPA, mergeSetPA) =
                        indexSetList00[id1].SetMergeCountSwaps(indexSetList85[id2]);
                    
                    var (swapCountAP, mergeSetAP) =
                        indexSetList85[id1].SetMergeCountSwaps(indexSetList00[id2]);
                    
                    var (swapCountAA1, mergeSetAA1) =
                        indexSetList70[id1].SetMergeCountSwaps(indexSetList85[id2]);
                    
                    var (swapCountAA2, mergeSetAA2) =
                        indexSetList85[id1].SetMergeCountSwaps(indexSetList70[id2]);
                    
                    Debug.Assert(swapCountRefImp1 == swapCountPP1);
                    Debug.Assert(swapCountRefImp2 == swapCountPP2);
                    Debug.Assert(swapCountRefImp1 == swapCountPA);
                    Debug.Assert(swapCountRefImp2 == swapCountAP);
                    Debug.Assert(swapCountRefImp1 == swapCountAA1);
                    Debug.Assert(swapCountRefImp2 == swapCountAA2);
                    
                    Debug.Assert(mergeSetRefImp1 == mergeSetPP1);
                    Debug.Assert(mergeSetRefImp2 == mergeSetPP2);
                    Debug.Assert(mergeSetRefImp1 == mergeSetPA.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(mergeSetRefImp2 == mergeSetAP.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(mergeSetRefImp1 == mergeSetAA1.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(mergeSetRefImp2 == mergeSetAA2.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var swapCountRefImp = CountEGpSwapsRefImp(indexSetList00[id1].ToUInt64(), indexSetList00[id2].ToUInt64());
                    var mergeSetRefImp = IndexSet.CreateFromUInt64Pattern(indexSetList00[id1].ToUInt64() ^ indexSetList00[id2].ToUInt64());
                    
                    var (swapCount00, mergeSet00) =
                        indexSetList00[id1].SetMergeCountSwaps(indexSetList00[id2]);
                    
                    var (swapCount58, mergeSet58) =
                        indexSetList58[id1].SetMergeCountSwaps(indexSetList58[id2]);

                    var (swapCount70, mergeSet70) =
                        indexSetList70[id1].SetMergeCountSwaps(indexSetList70[id2]);

                    Debug.Assert(swapCountRefImp == swapCount00);
                    Debug.Assert(swapCountRefImp == swapCount58);
                    Debug.Assert(swapCountRefImp == swapCount70);

                    Debug.Assert(mergeSetRefImp == mergeSet00);
                    Debug.Assert(mergeSetRefImp == mergeSet58.MapIndicesByValue(i => i >= 58 ? i - 58 : i));
                    Debug.Assert(mergeSetRefImp == mergeSet70.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
        }

        public static void MergeCountSwapsExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetMergeCountSwaps(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetMergeCountSwaps(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetMergeCountSwaps(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetMergeCountSwaps(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }

        public static void MergeCountSwapsTrackCommonExample1()
        {
            const ulong maxId = (1UL << 11) - 1UL;
            const ulong n = maxId + 1;
            
            var indexSetList00 = 
                n.GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList15 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList58 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(58)).ToArray();

            var indexSetList70 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(70)).ToArray();
            
            var indexSetList85 = 
                indexSetList00.Select(indexSet => indexSet.ShiftIndices(85)).ToArray();

            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var swapCountRefImp1 = CountEGpSwapsRefImp(indexSetList00[id1].ToUInt64(), indexSetList15[id2].ToUInt64());
                    var swapCountRefImp2 = CountEGpSwapsRefImp(indexSetList15[id1].ToUInt64(), indexSetList00[id2].ToUInt64());

                    var mergeSetRefImp1 = IndexSet.CreateFromUInt64Pattern(indexSetList00[id1].ToUInt64() ^ indexSetList15[id2].ToUInt64());
                    var mergeSetRefImp2 = IndexSet.CreateFromUInt64Pattern(indexSetList15[id1].ToUInt64() ^ indexSetList00[id2].ToUInt64());

                    var commonSetRefImp1 = IndexSet.CreateFromUInt64Pattern(indexSetList00[id1].ToUInt64() & indexSetList15[id2].ToUInt64());
                    var commonSetRefImp2 = IndexSet.CreateFromUInt64Pattern(indexSetList15[id1].ToUInt64() & indexSetList00[id2].ToUInt64());

                    var (swapCountPP1, mergeSetPP1, commonSetPP1) =
                        indexSetList00[id1].SetMergeCountSwapsTrackCommon(indexSetList15[id2]);
                    
                    var (swapCountPP2, mergeSetPP2, commonSetPP2) =
                        indexSetList15[id1].SetMergeCountSwapsTrackCommon(indexSetList00[id2]);

                    var (swapCountPA, mergeSetPA, commonSetPA) =
                        indexSetList00[id1].SetMergeCountSwapsTrackCommon(indexSetList85[id2]);
                    
                    var (swapCountAP, mergeSetAP, commonSetAP) =
                        indexSetList85[id1].SetMergeCountSwapsTrackCommon(indexSetList00[id2]);
                    
                    var (swapCountAA1, mergeSetAA1, commonSetAA1) =
                        indexSetList70[id1].SetMergeCountSwapsTrackCommon(indexSetList85[id2]);
                    
                    var (swapCountAA2, mergeSetAA2, commonSetAA2) =
                        indexSetList85[id1].SetMergeCountSwapsTrackCommon(indexSetList70[id2]);
                    
                    Debug.Assert(swapCountRefImp1 == swapCountPP1);
                    Debug.Assert(swapCountRefImp2 == swapCountPP2);
                    Debug.Assert(swapCountRefImp1 == swapCountPA);
                    Debug.Assert(swapCountRefImp2 == swapCountAP);
                    Debug.Assert(swapCountRefImp1 == swapCountAA1);
                    Debug.Assert(swapCountRefImp2 == swapCountAA2);
                    
                    Debug.Assert(mergeSetRefImp1 == mergeSetPP1);
                    Debug.Assert(mergeSetRefImp2 == mergeSetPP2);
                    Debug.Assert(mergeSetRefImp1 == mergeSetPA.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(mergeSetRefImp2 == mergeSetAP.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(mergeSetRefImp1 == mergeSetAA1.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(mergeSetRefImp2 == mergeSetAA2.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    
                    Debug.Assert(commonSetRefImp1 == commonSetPP1);
                    Debug.Assert(commonSetRefImp2 == commonSetPP2);
                    Debug.Assert(commonSetRefImp1 == commonSetPA.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(commonSetRefImp2 == commonSetAP.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(commonSetRefImp1 == commonSetAA1.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                    Debug.Assert(commonSetRefImp2 == commonSetAA2.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
            
            for (var id1 = 0UL; id1 < n; id1++)
            {
                for (var id2 = 0UL; id2 < n; id2++)
                {
                    var swapCountRefImp = CountEGpSwapsRefImp(indexSetList00[id1].ToUInt64(), indexSetList00[id2].ToUInt64());
                    var mergeSetRefImp = IndexSet.CreateFromUInt64Pattern(indexSetList00[id1].ToUInt64() ^ indexSetList00[id2].ToUInt64());
                    var commonSetRefImp = IndexSet.CreateFromUInt64Pattern(indexSetList00[id1].ToUInt64() & indexSetList00[id2].ToUInt64());

                    var (swapCount00, mergeSet00, commonSet00) =
                        indexSetList00[id1].SetMergeCountSwapsTrackCommon(indexSetList00[id2]);
                    
                    var (swapCount58, mergeSet58, commonSet58) =
                        indexSetList58[id1].SetMergeCountSwapsTrackCommon(indexSetList58[id2]);

                    var (swapCount70, mergeSet70, commonSet70) =
                        indexSetList70[id1].SetMergeCountSwapsTrackCommon(indexSetList70[id2]);

                    Debug.Assert(swapCountRefImp == swapCount00);
                    Debug.Assert(swapCountRefImp == swapCount58);
                    Debug.Assert(swapCountRefImp == swapCount70);

                    Debug.Assert(mergeSetRefImp == mergeSet00);
                    Debug.Assert(mergeSetRefImp == mergeSet58.MapIndicesByValue(i => i >= 58 ? i - 58 : i));
                    Debug.Assert(mergeSetRefImp == mergeSet70.MapIndicesByValue(i => i >= 70 ? i - 70 : i));

                    Debug.Assert(commonSetRefImp == commonSet00);
                    Debug.Assert(commonSetRefImp == commonSet58.MapIndicesByValue(i => i >= 58 ? i - 58 : i));
                    Debug.Assert(commonSetRefImp == commonSet70.MapIndicesByValue(i => i >= 70 ? i - 70 : i));
                }
            }
        }

        public static void MergeCountSwapsTrackCommonExample2()
        {
            const ulong maxId = (1UL << 14) - 1UL;

            var indexSetList1 = 
                (maxId + 1).GetRange().Select(IndexSet.CreateFromUInt64Pattern).ToArray();

            var indexSetList2 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(15)).ToArray();
            
            var indexSetList3 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65)).ToArray();
            
            var indexSetList4 = 
                indexSetList1.Select(indexSet => indexSet.ShiftIndices(65 + 15)).ToArray();

            var tpp = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetMergeCountSwapsTrackCommon(indexSet2);
            }, 1);
            
            var tpa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList1)
                foreach (var indexSet2 in indexSetList3)
                    indexSet1.SetMergeCountSwapsTrackCommon(indexSet2);
            }, 1);
            
            var tap = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList2)
                    indexSet1.SetMergeCountSwapsTrackCommon(indexSet2);
            }, 1);

            var taa = GetTime(() =>
            {
                foreach (var indexSet1 in indexSetList3)
                foreach (var indexSet2 in indexSetList4)
                    indexSet1.SetMergeCountSwapsTrackCommon(indexSet2);
            }, 1);

            Console.WriteLine($"Pattern-pattern time: {tpp}");
            Console.WriteLine($"  Pattern-array time: {tpa}");
            Console.WriteLine($"  Array-pattern time: {tap}");
            Console.WriteLine($"    Array-array time: {taa}");
        }

        
    }
}
