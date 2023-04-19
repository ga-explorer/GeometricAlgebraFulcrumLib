namespace DataStructuresLib.Basic
{
    public sealed record Pair2D<T> : 
        IPair2D<T>
    {
        public T Item11 { get; }
        
        public T Item12 { get; }
        
        public T Item21 { get; }
        
        public T Item22 { get; }


        public Pair2D(T item11, T item12, T item21, T item22)
        {
            Item11 = item11;
            Item12 = item12;
            Item21 = item21;
            Item22 = item22;
        }


        public void Deconstruct(out T item11, out T item12, out T item21, out T item22)
        {
            item11 = Item11;
            item12 = Item12;
            item21 = Item21;
            item22 = Item22;
        }
    }
}
