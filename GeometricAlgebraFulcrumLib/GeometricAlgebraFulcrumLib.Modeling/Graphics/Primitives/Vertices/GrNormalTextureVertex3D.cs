using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
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


    public GrNormalTextureVertex3D(int index, ITriplet<Float64Scalar> point)
    {
        Index = index;
        Point = point.ToLinVector3D();
        ParameterValue = LinFloat64Vector2D.Zero;
    }

    public GrNormalTextureVertex3D(int index, ITriplet<Float64Scalar> point, IPair<Float64Scalar> textureUv)
    {
        Index = index;
        Point = point.ToLinVector3D();
        ParameterValue = textureUv.ToLinVector2D();
    }

    public GrNormalTextureVertex3D(int index, ITriplet<Float64Scalar> point, IPair<Float64Scalar> textureUv, ITriplet<Float64Scalar> normal)
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