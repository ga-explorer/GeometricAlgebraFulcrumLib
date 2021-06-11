using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Collections.PeriodicLists;

namespace DataStructuresLib.Collections.PeriodicLists2D
{
    public class ProListInterpolatedColumns2D<TValue> : 
        IPeriodicReadOnlyList2D<TValue>
    {
        public IPeriodicReadOnlyList<TValue> SourceList1 { get; }

        public IPeriodicReadOnlyList<TValue> SourceList2 { get; }

        public Func<double, TValue, TValue, TValue> InterpolationFunc { get; }


        public int Count 
            => SourceList1.Count * Count2;

        public TValue this[int index]
        {
            get
            {
                var (index1, index2) = 
                    this.GetItemIndexTuple(index);

                return InterpolationFunc(
                    ((double)index2) / (Count2 - 1),
                    SourceList1[index1],
                    SourceList2[index2]
                );
            }
        }

        public int Count1 
            => SourceList1.Count;

        public int Count2 { get; }

        public TValue this[int index1, int index2]
            => InterpolationFunc(
                ((double)index2) / (Count2 - 1),
                SourceList1[index1],
                SourceList2[index2]
            );


        public ProListInterpolatedColumns2D(int count2, [NotNull] IPeriodicReadOnlyList<TValue> sourceList1, [NotNull] IPeriodicReadOnlyList<TValue> sourceList2, [NotNull] Func<double, TValue, TValue, TValue> interpolationFunc)
        {
            Debug.Assert(sourceList1.Count == sourceList2.Count && count2 > 1);

            Count2 = count2;
            SourceList1 = sourceList1;
            SourceList2 = sourceList2;
            InterpolationFunc = interpolationFunc;
        }


        public TValue[,] ToArray2D()
        {
            var valuesArray = new TValue[Count1, Count2];

            var valuesArray1 = SourceList1.ToArray();
            var valuesArray2 = SourceList2.ToArray();

            for (var index1 = 0; index1 < valuesArray1.Length; index1++)
            {
                var value1 = valuesArray1[index1];
                var value2 = valuesArray2[index1];

                for (var index2 = 0; index2 < valuesArray2.Length; index2++)
                    valuesArray[index1, index2] = InterpolationFunc(
                        ((double)index2) / (Count2 - 1),
                        value1,
                        value2
                    );
            }

            return valuesArray;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            var valuesArray1 = SourceList1.ToArray();
            var valuesArray2 = SourceList2.ToArray();

            for (var index2 = 0; index2 < valuesArray2.Length; index2++)
            {
                var paramValue = ((double) index2) / (Count2 - 1);

                for (var index1 = 0; index1 < valuesArray1.Length; index1++)
                    yield return InterpolationFunc(
                        paramValue,
                        valuesArray1[index1],
                        valuesArray2[index1]
                    );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}