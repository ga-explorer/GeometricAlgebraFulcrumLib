namespace NumericalGeometryLib.BasicMath.Tuples.Immutable
{
    public sealed record TupleQuad2D
    {
        public Tuple2D Tuple1 { get; }

        public Tuple2D Tuple2 { get; }

        public Tuple2D Tuple3 { get; }

        public Tuple2D Tuple4 { get; }


        public TupleQuad2D(ITuple2D tuple1, ITuple2D tuple2, ITuple2D tuple3, ITuple2D tuple4)
        {
            Tuple1 = tuple1.ToTuple2D();
            Tuple2 = tuple2.ToTuple2D();
            Tuple3 = tuple3.ToTuple2D();
            Tuple4 = tuple4.ToTuple2D();
        }
    }
}
