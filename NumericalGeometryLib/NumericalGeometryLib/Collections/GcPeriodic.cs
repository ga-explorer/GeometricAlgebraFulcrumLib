using DataStructuresLib.Basic;
using NumericalGeometryLib.Collections.Finite;

namespace NumericalGeometryLib.Collections
{
    public sealed class GcPeriodic<T> : GenerativeCollection<T>
    {
        public static GcPeriodic<T> Create(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
        {
            return new GcPeriodic<T>(baseCollection, firstIndex, lastIndex);
        }

        public static GcPeriodic<T> Create(FiniteCollection<T> baseCollection)
        {
            return new GcPeriodic<T>(baseCollection, baseCollection.MinIndex, baseCollection.MaxIndex);
        }


        public GenerativeCollection<T> BaseCollection { get; private set; }

        public int FirstIndex { get; set; }

        public int LastIndex { get; set; }

        public int PeriodSize =>
            LastIndex >= FirstIndex
                ? LastIndex - FirstIndex + 1
                : FirstIndex - LastIndex + 1;

        public T this[int index]
        {
            get
            {
                if (BaseCollection == null) return DefaultValue;

                return LastIndex >= FirstIndex
                    ? BaseCollection.GetItem(FirstIndex + index.Mod(PeriodSize))
                    : BaseCollection.GetItem(FirstIndex - index.Mod(PeriodSize));
            }
        }


        private GcPeriodic(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
        {
            BaseCollection = baseCollection;
            FirstIndex = firstIndex;
            LastIndex = lastIndex;
        }


        public override T GetItem(int index)
        {
            if (BaseCollection == null) return DefaultValue;
            
            return LastIndex >= FirstIndex 
                ? BaseCollection.GetItem(FirstIndex + index.Mod(PeriodSize)) 
                : BaseCollection.GetItem(FirstIndex - index.Mod(PeriodSize));
        }
    }
}
