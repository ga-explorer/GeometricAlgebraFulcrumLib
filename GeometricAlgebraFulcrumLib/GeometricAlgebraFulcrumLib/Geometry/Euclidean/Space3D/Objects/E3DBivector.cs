using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    public sealed record E3DBivector<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public T Xy { get; }

        public T Xz { get; }

        public T Yz { get; }

        public bool AssumeUnit { get; }


        internal E3DBivector(IScalarProcessor<T> scalarProcessor, T xy, T xz, T yz, bool assumeUnit = false)
        {
            ScalarProcessor = scalarProcessor;
            Xy = xy;
            Xz = xz;
            Yz = yz;
            AssumeUnit = assumeUnit;
        }
    }
}