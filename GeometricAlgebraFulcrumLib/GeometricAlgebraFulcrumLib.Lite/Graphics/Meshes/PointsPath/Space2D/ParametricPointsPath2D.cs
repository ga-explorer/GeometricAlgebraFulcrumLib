using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D
{
    public sealed class ParametricPointsPath2D 
        : PSeqMapped1D<double, IFloat64Vector2D>, IPointsPath2D
    {
        public Func<double, IFloat64Vector2D> Mapping { get; }

        
        public ParametricPointsPath2D(IPeriodicSequence1D<double> parameterSequence, Func<double, IFloat64Vector2D> mapping)
            : base(parameterSequence)
        {
            Mapping = mapping;
        }


        protected override IFloat64Vector2D MappingFunction(double input)
        {
            return Mapping(input);
        }

        
        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
        {
            return new ParametricPointsPath2D(
                BaseSequence,
                t => pointMapping(Mapping(t))
            );
        }
    }
}