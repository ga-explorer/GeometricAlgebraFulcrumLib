using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class ParametricPointsPath3D : 
    PSeqMapped1D<double, ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public Func<double, ILinFloat64Vector3D> Mapping { get; }

        
    public ParametricPointsPath3D(IEnumerable<double> parameterSequence, Func<double, ILinFloat64Vector3D> mapping)
        : base(new PSeqArray1D<double>(parameterSequence))
    {
        Mapping = mapping;
    }
        
    public ParametricPointsPath3D(IPeriodicSequence1D<double> parameterSequence, Func<double, ILinFloat64Vector3D> mapping)
        : base(parameterSequence)
    {
        Mapping = mapping;
    }


    protected override ILinFloat64Vector3D MappingFunction(double input)
    {
        return Mapping(input);
    }


    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ParametricPointsPath3D(
            BaseSequence,
            t => pointMapping(Mapping(t))
        );
    }
}