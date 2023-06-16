using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class ParametricPointsPath3D : 
        PSeqMapped1D<double, IFloat64Tuple3D>, 
        IPointsPath3D
    {
        public Func<double, IFloat64Tuple3D> Mapping { get; }

        
        public ParametricPointsPath3D(IEnumerable<double> parameterSequence, Func<double, IFloat64Tuple3D> mapping)
            : base(new PSeqArray1D<double>(parameterSequence))
        {
            Mapping = mapping;
        }
        
        public ParametricPointsPath3D(IPeriodicSequence1D<double> parameterSequence, Func<double, IFloat64Tuple3D> mapping)
            : base(parameterSequence)
        {
            Mapping = mapping;
        }


        protected override IFloat64Tuple3D MappingFunction(double input)
        {
            return Mapping(input);
        }


        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath3D MapPoints(Func<IFloat64Tuple3D, IFloat64Tuple3D> pointMapping)
        {
            return new ParametricPointsPath3D(
                BaseSequence,
                t => pointMapping(Mapping(t))
            );
        }
    }
}