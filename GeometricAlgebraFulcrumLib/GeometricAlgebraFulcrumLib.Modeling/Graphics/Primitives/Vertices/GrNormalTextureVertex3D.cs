using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;

public sealed class GrNormalTextureVertex3D 
    : IGraphicsVertex3D
{
    public int Index { get; }
        
    public int VSpaceDimensions 
        => 3;

    public LinFloat64Vector3D Point { get; }

    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }

    public LinFloat64Vector2D ParameterValue { get; set; }

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
        => ParameterValue.Item1;

    public bool HasColor 
        => false;

    public bool HasParameterValue 
        => true;

    public bool HasNormal 
        => true;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.NormalTextureData;

    public bool IsValid() =>
        Point.IsValid() &&
        ParameterValue.Item1.IsValid() &&
        ParameterValue.Item2.IsValid() &&
        Normal.IsValid();


    public GrNormalTextureVertex3D(int index, ITriplet<double> point)
    {
        Index = index;
        Point = point.ToLinVector3D();
        ParameterValue = LinFloat64Vector2D.Zero;
    }

    public GrNormalTextureVertex3D(int index, ITriplet<double> point, IPair<double> textureUv)
    {
        Index = index;
        Point = point.ToLinVector3D();
        ParameterValue = textureUv.ToLinVector2D();
    }

    public GrNormalTextureVertex3D(int index, ITriplet<double> point, IPair<double> textureUv, ITriplet<double> normal)
    {
        Index = index;
        Point = point.ToLinVector3D();
        ParameterValue = textureUv.ToLinVector2D();
        Normal.Set(normal);
    }

    public GrNormalTextureVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
    {
        Index = index;
        Point = vertex.Point;
        ParameterValue = vertex.ParameterValue;
        Normal.Set(vertex.Normal);
    }

}