using System.Collections.Generic;

namespace NumericalGeometryLib.Collections.Finite
{
    /// <summary>
    /// A collection based on two lists of elements one holding positive indices from 0
    /// and the other the negative indices
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FcTwoEndList<T> : FiniteCollection<T>
    {
        public static FcTwoEndList<T> Create()
        {
            return new FcTwoEndList<T>();
        }


        public List<T> ForwardList { get; } = new List<T>();

        public List<T> BackwardList { get; } = new List<T>();


        public override int Count => BackwardList.Count + ForwardList.Count;

        public override int MinIndex => -BackwardList.Count;

        public override int MaxIndex => ForwardList.Count - 1;

        public T this[int index]
        {
            get =>
                index < 0
                    ? BackwardList[-index - 1]
                    : ForwardList[index];
            set
            {
                if (index >= 0)
                    ForwardList[index] = value;
                else
                    BackwardList[-index - 1] = value;
            }
        }


        private FcTwoEndList()
        {
            
        }


        public override T GetItem(int index)
        {
            return index < 0 
                ? BackwardList[-index - 1] 
                : ForwardList[index];
        }

        public FcTwoEndList<T> Clear()
        {
            ForwardList.Clear();
            BackwardList.Clear();

            return this;
        }
    }
}
