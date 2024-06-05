using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Vertices;

public sealed class GrVertex3D 
    : IGraphicsVertex3D
{
    public int Index { get; }
        
    public int VSpaceDimensions 
        => 3;

    public LinFloat64Vector3D Point { get; }

    public Pair<Float64Scalar> ParameterValue 
        => new Pair<Float64Scalar>(0, 0);

    public LinFloat64Normal3D Normal
        => null;

    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }

    public bool HasParameterValue 
        => false;

    public bool HasNormal 
        => false;

    public bool HasColor 
        => false;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.VoidData;

    public Float64Scalar X 
        => Point.X;

    public Float64Scalar Y 
        => Point.Y;

    public Float64Scalar Z 
        => Point.Z;

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;

    public bool IsValid() => Point.IsValid();


    public GrVertex3D(int index, double x, double y, double z)
    {
        Index = index;
        Point = LinFloat64Vector3D.Create(x, y, z);
    }

    public GrVertex3D(int index, ITriplet<Float64Scalar> point)
    {
        Index = index;
        Point = point.ToLinVector3D();
    }

    public GrVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
    {
        Index = index;
        Point = vertex.Point;
    }

}