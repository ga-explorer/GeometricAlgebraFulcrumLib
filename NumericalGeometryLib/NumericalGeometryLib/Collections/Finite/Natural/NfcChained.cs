using System.Collections.Generic;
using System.Linq;

namespace NumericalGeometryLib.Collections.Finite.Natural
{
    /// <summary>
    /// This class represents a concatenation of several base collections into a single one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NfcChained<T> : NaturalFiniteCollection<T>
    {
        public static NfcChained<T> Create()
        {
            return new NfcChained<T>();
        }

        public static NfcChained<T> Create(params FiniteCollection<T>[] colList)
        {
            var result = new NfcChained<T>();
            result.BaseCollections.AddRange(colList);
            return result;
        }

        public static NfcChained<T> Create(IEnumerable<FiniteCollection<T>> colList)
        {
            var result = new NfcChained<T>();
            result.BaseCollections.AddRange(colList);
            return result;
        }


        public List<FiniteCollection<T>> BaseCollections { get; } 
            = new List<FiniteCollection<T>>();

        public override int Count
        {
            get
            {
                return BaseCollections
                    .Where(c => c != null)
                    .Sum(c => c.MaxIndex + 1);
            }
        }

        public T this[int index]
        {
            get
            {
                foreach (var c in BaseCollections.Where(cl => cl != null))
                {
                    var cnt = c.Count;

                    if (index < cnt)
                        return c.GetItem(c.MinIndex + index);
                    
                    index -= cnt;
                }

                return DefaultValue;
            }
        }

        public override T GetItem(int index)
        {
            foreach (var c in BaseCollections.Where(cl => cl != null))
            {
                var cnt = c.Count;

                if (index < cnt)
                    return c.GetItem(c.MinIndex + index);
                
                index -= cnt;
            }

            return DefaultValue;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new NfcChainedEnumerator<T>(this);
        }
    }
}
