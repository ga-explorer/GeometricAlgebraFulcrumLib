using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class MappedPointsPath2D
    : PSeqMapped1D<ILinFloat64Vector2D>, IPointsPath2D
{
    public IPointsPath2D BasePath { get; }

    public Func<ILinFloat64Vector2D, ILinFloat64Vector2D> Mapping { get; }


    public MappedPointsPath2D(IPointsPath2D basePath, Func<ILinFloat64Vector2D, ILinFloat64Vector2D> mapping)
        : base(basePath)
    {
        BasePath = basePath;
        Mapping = mapping;
    }


    protected override ILinFloat64Vector2D MappingFunction(ILinFloat64Vector2D input)
    {
        return Mapping(input);
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new MappedPointsPath2D(
            BasePath,
            p => pointMapping(Mapping(p))
        );
    }
}