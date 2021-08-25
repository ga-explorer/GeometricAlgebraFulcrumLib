namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public interface IGaGridEvenSingleKey<T> :
        IGaGridEvenSparse<T>
    {
        public ulong Key1 { get; }

        public ulong Key2 { get; }

        public GaRecordKeyPair Key { get; }

        public T Value { get; set; }
    }
}