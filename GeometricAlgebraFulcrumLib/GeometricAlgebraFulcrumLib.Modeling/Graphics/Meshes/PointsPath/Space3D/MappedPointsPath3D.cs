using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class MappedPointsPath3D : 
    PSeqMapped1D<LinFloat64Vector3D>, 
    IPointsPath3D
{
    public IPointsPath3D BasePath { get; }

    public Func<LinFloat64Vector3D, LinFloat64Vector3D> Mapping { get; set; }


    public MappedPointsPath3D(IPointsPath3D basePath)
        : base(basePath)
    {
        BasePath = basePath;
        Mapping = (point => point);
    }

    public MappedPointsPath3D(IPointsPath3D basePath, Func<LinFloat64Vector3D, LinFloat64Vector3D> mapping)
        : base(basePath)
    {
        BasePath = basePath;
        Mapping = mapping;
    }


    protected override LinFloat64Vector3D MappingFunction(LinFloat64Vector3D input)
    {
        return Mapping(input);
    }


    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> pointMapping)
    {
        return new MappedPointsPath3D(
            BasePath,
            p => pointMapping(Mapping(p))
        );
    }
}