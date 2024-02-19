using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;

public sealed class GraphicsNormalTextureVertexData3D 
    : IGraphicsVertexData3D
{
    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }

    public IFloat64Vector2D TextureUv { get; set; }

    public Normal3D Normal { get; }
        = new Normal3D();
        
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
        TextureUv = Float64Vector2D.Create((Float64Scalar)0, 0);
    }

    public GraphicsNormalTextureVertexData3D(IFloat64Vector2D textureUv)
    {
        TextureUv = textureUv;
    }

    public GraphicsNormalTextureVertexData3D(IFloat64Vector2D textureUv, IFloat64Vector3D normal)
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