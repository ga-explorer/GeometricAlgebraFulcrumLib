using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects
{
    public sealed record E2DBivector<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public T Xy { get; }
    
    

        public bool AssumeUnit { get; }


        internal E2DBivector(IScalarProcessor<T> scalarProcessor, T xy, bool assumeUnit = false)
        {
            ScalarProcessor = scalarProcessor;
            Xy = xy;
            AssumeUnit = assumeUnit;
        }
    }
}