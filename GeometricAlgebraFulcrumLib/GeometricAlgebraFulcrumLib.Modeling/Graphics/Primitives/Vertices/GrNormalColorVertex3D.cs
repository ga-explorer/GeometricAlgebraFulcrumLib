using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;

public sealed class GrNormalColorVertex3D 
    : IGraphicsVertex3D
{
    public int Index { get; }
        
    public int VSpaceDimensions 
        => 3;

    public LinFloat64Vector3D Point { get; }

    public Color Color { get; set; }
        = Color.Black;

    public Pair<Float64Scalar> ParameterValue 
        => new Pair<Float64Scalar>(0, 0);

    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();

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

    public double TextureU 
        => 0;

    public bool HasColor 
        => true;

    public bool HasParameterValue 
        => false;

    public bool HasNormal 
        => true;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.NormalColorData;

    public bool IsValid() => Point.IsValid() && Normal.IsValid();


    public GrNormalColorVertex3D(int index, ITriplet<Float64Scalar> point)
    {
        Index = index;
        Point = point.ToLinVector3D();
    }

    public GrNormalColorVertex3D(int index, ITriplet<Float64Scalar> point, Color color)
    {
        Index = index;
        Point = point.ToLinVector3D();
        Color = color;
    }

    public GrNormalColorVertex3D(int index, ITriplet<Float64Scalar> point, Color color, ITriplet<Float64Scalar> normal)
    {
        Index = index;
        Point = point.ToLinVector3D();
        Color = color;
        Normal.Set(normal);
    }

    public GrNormalColorVertex3D(int index, IGraphicsSurfaceLocalFrame3D point)
    {
        Index = index;
        Point = point.Point;
        Color = point.Color;
    }


    public LinFloat64Vector3D GetDisplacedPoint(double d)
    {
        return LinFloat64Vector3D.Create(Point.X + d * Normal.X,
            Point.Y + d * Normal.Y,
            Point.Z + d * Normal.Z);
    }
}