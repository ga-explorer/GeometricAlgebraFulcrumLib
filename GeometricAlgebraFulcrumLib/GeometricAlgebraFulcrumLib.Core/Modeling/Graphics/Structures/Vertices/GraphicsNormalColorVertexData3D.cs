using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Structures.Vertices;

public sealed class GraphicsNormalColorVertexData3D 
    : IGraphicsVertexData3D
{
    public Color Color { get; set; }
        = Color.Black;

    public ILinFloat64Vector2D TextureUv
    {
        get => LinFloat64Vector2D.Zero;
        set => throw new InvalidOperationException();
    }

    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();
        
    public double TextureU 
        => 0;

    public double TextureV 
        => 0;

    public double NormalX 
        => Normal.X;

    public double NormalY 
        => Normal.Y;

    public double NormalZ 
        => Normal.Z;

    public bool HasColor 
        => true;

    public bool HasTextureUv 
        => false;

    public bool HasNormal 
        => true;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.NormalColorData;
        

    public GraphicsNormalColorVertexData3D()
    {
    }

    public GraphicsNormalColorVertexData3D(Color color)
    {
        Color = color;
    }

    public GraphicsNormalColorVertexData3D(Color color, ILinFloat64Vector3D normal)
    {
        Color = color;
        Normal.Set(normal);
    }

    public GraphicsNormalColorVertexData3D(IGraphicsVertexData3D point)
    {
        Color = point.Color;
    }


    public LinFloat64Vector3D GetDisplacedPoint(ILinFloat64Vector3D point, double d)
    {
        return LinFloat64Vector3D.Create(point.X + d * Normal.X,
            point.Y + d * Normal.Y,
            point.Z + d * Normal.Z);
    }
}