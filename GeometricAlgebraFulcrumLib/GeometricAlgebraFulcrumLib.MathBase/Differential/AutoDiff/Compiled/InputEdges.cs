namespace GeometricAlgebraFulcrumLib.MathBase.Differential.AutoDiff.Compiled
{
    internal readonly struct InputEdges
    {
        private readonly InputEdge[] _edges;
        private readonly int _offset;

        public int Length { get; }


        private InputEdges(InputEdge[] edges, int offset, int length)
        {
            _edges = edges;
            _offset = offset;
            Length = length;
        }

        public InputEdges(int offset, int length)
            : this(null, offset, length)
        {

        }


        public InputEdges Remap(InputEdge[] newEdges)
        {
            return new InputEdges(newEdges, _offset, Length);
        }

        public TapeElement Element(int i)
        {
            return _edges[_offset + i].Element;
        }

        public double Weight(int i)
        {
            return _edges[_offset + i].Weight;
        }

        public void SetWeight(int i, double w)
        {
            _edges[_offset + i].Weight = w;
        }
    }
}