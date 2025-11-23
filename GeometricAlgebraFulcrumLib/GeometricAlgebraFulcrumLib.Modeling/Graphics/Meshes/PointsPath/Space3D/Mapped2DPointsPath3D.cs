using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class Mapped2DPointsPath3D : 
    PSeqMapped1D<LinFloat64Vector2D, LinFloat64Vector3D>, 
    IPointsPath3D
{
    public Func<LinFloat64Vector2D, LinFloat64Vector3D> Mapping { get; set; }


    public Mapped2DPointsPath3D(IPointsPath2D basePath, Func<LinFloat64Vector2D, LinFloat64Vector3D> mapping)
        : base(basePath)
    {
        Mapping = mapping;
    }


    protected override LinFloat64Vector3D MappingFunction(LinFloat64Vector2D input)
    {
        return Mapping(input);
    }


    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            this.Select(pointMapping).ToArray()
        );
    }
}