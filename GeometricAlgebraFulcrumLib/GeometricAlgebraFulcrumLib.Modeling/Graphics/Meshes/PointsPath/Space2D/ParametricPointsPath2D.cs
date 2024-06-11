using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class ParametricPointsPath2D 
    : PSeqMapped1D<double, ILinFloat64Vector2D>, IPointsPath2D
{
    public Func<double, ILinFloat64Vector2D> Mapping { get; }

        
    public ParametricPointsPath2D(IPeriodicSequence1D<double> parameterSequence, Func<double, ILinFloat64Vector2D> mapping)
        : base(parameterSequence)
    {
        Mapping = mapping;
    }


    protected override ILinFloat64Vector2D MappingFunction(double input)
    {
        return Mapping(input);
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ParametricPointsPath2D(
            BaseSequence,
            t => pointMapping(Mapping(t))
        );
    }
}