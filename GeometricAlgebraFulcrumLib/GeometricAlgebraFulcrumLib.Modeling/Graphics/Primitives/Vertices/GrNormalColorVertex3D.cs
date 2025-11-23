using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
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

    public LinFloat64Vector2D ParameterValue 
        => LinFloat64Vector2D.Zero;

    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();

    public double X 
        => Point.X;

    public double Y 
        => Point.Y;

    public double Z 
        => Point.Z;
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
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


    public GrNormalColorVertex3D(int index, ITriplet<double> point)
    {
        Index = index;
        Point = point.ToLinVector3D();
    }

    public GrNormalColorVertex3D(int index, ITriplet<double> point, Color color)
    {
        Index = index;
        Point = point.ToLinVector3D();
        Color = color;
    }

    public GrNormalColorVertex3D(int index, ITriplet<double> point, Color color, ITriplet<double> normal)
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