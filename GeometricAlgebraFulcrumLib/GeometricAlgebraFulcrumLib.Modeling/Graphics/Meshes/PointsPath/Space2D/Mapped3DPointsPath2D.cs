using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class Mapped3DPointsPath2D
    : PSeqMapped1D<ILinFloat64Vector3D, ILinFloat64Vector2D>, IPointsPath2D
{
    public Func<ILinFloat64Vector3D, ILinFloat64Vector2D> Mapping { get; set; }


    public Mapped3DPointsPath2D(IPointsPath3D basePath, Func<ILinFloat64Vector3D, ILinFloat64Vector2D> mapping)
        : base(basePath)
    {
        Mapping = mapping;
    }


    protected override ILinFloat64Vector2D MappingFunction(ILinFloat64Vector3D input)
    {
        return Mapping(input);
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(
            this.Select(pointMapping).ToArray()
        );
    }
}