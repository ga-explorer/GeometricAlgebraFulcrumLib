using System.Runtime.CompilerServices;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This structure represents an immutable hexad of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record Hexad<T> : IHexad<T>
    {
        public T Item1 { get; }
    
        public T Item2 { get; }
    
        public T Item3 { get; }
    
        public T Item4 { get; }
    
        public T Item5 { get; }
    
        public T Item6 { get; }


        /// <summary>
        /// This structure represents an immutable hexad of items of the same type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Hexad(T item1, T item2, T item3, T item4, T item5, T item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out T item1, out T item2, out T item3, out T item4, out T item5, out T item6)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
            item6 = Item6;
        }
    }
}