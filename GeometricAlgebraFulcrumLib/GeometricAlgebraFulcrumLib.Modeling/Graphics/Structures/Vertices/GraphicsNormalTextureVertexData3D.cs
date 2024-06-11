using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;

public sealed class GraphicsNormalTextureVertexData3D 
    : IGraphicsVertexData3D
{
    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }

    public ILinFloat64Vector2D TextureUv { get; set; }

    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();
        
    public double TextureU 
        => TextureUv.X;

    public double TextureV 
        => TextureUv.Y;

    public double NormalX 
        => Normal.X;

    public double NormalY 
        => Normal.Y;

    public double NormalZ 
        => Normal.Z;

    public bool HasColor 
        => false;

    public bool HasTextureUv 
        => true;

    public bool HasNormal 
        => true;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.NormalTextureData;
        

    public GraphicsNormalTextureVertexData3D()
    {
        TextureUv = LinFloat64Vector2D.Create((Float64Scalar)0, 0);
    }

    public GraphicsNormalTextureVertexData3D(ILinFloat64Vector2D textureUv)
    {
        TextureUv = textureUv;
    }

    public GraphicsNormalTextureVertexData3D(ILinFloat64Vector2D textureUv, ILinFloat64Vector3D normal)
    {
        TextureUv = textureUv;
        Normal.Set(normal);
    }

    public GraphicsNormalTextureVertexData3D(IGraphicsVertexData3D vertex)
    {
        TextureUv = vertex.TextureUv;
        Normal.Set(vertex.Normal);
    }
}