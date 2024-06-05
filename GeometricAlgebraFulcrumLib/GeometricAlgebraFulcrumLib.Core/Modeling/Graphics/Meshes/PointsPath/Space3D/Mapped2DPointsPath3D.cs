using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class Mapped2DPointsPath3D : 
    PSeqMapped1D<ILinFloat64Vector2D, ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public Func<ILinFloat64Vector2D, ILinFloat64Vector3D> Mapping { get; set; }


    public Mapped2DPointsPath3D(IPointsPath2D basePath, Func<ILinFloat64Vector2D, ILinFloat64Vector3D> mapping)
        : base(basePath)
    {
        Mapping = mapping;
    }


    protected override ILinFloat64Vector3D MappingFunction(ILinFloat64Vector2D input)
    {
        return Mapping(input);
    }


    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            this.Select(pointMapping).ToArray()
        );
    }
}