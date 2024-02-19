using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;

public sealed class Mapped3DPointsPath2D
    : PSeqMapped1D<IFloat64Vector3D, IFloat64Vector2D>, IPointsPath2D
{
    public Func<IFloat64Vector3D, IFloat64Vector2D> Mapping { get; set; }


    public Mapped3DPointsPath2D(IPointsPath3D basePath, Func<IFloat64Vector3D, IFloat64Vector2D> mapping)
        : base(basePath)
    {
        Mapping = mapping;
    }


    protected override IFloat64Vector2D MappingFunction(IFloat64Vector3D input)
    {
        return Mapping(input);
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(
            this.Select(pointMapping).ToArray()
        );
    }
}