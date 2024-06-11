using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;

public sealed record GraphicsVertexDataKind3D
{
    public static GraphicsVertexDataKind3D VoidData { get; }
        = new GraphicsVertexDataKind3D(false, false, false);

    public static GraphicsVertexDataKind3D ColorData { get; }
        = new GraphicsVertexDataKind3D(true, false, false);

    public static GraphicsVertexDataKind3D TextureData { get; }
        = new GraphicsVertexDataKind3D(false, true, false);

    public static GraphicsVertexDataKind3D NormalData { get; }
        = new GraphicsVertexDataKind3D(false, false, true);

    public static GraphicsVertexDataKind3D NormalColorData { get; }
        = new GraphicsVertexDataKind3D(true, false, true);

    public static GraphicsVertexDataKind3D NormalTextureData { get; }
        = new GraphicsVertexDataKind3D(false, true, true);
        
    public static GraphicsVertexDataKind3D NormalTextureColorData { get; }
        = new GraphicsVertexDataKind3D(true, true, true);


    public bool HasColor { get; }

    public bool HasTextureUv { get; }

    public bool HasNormal { get; }

    public bool HasTangent { get; }


    private GraphicsVertexDataKind3D(bool hasColor, bool hasTextureUv, bool hasNormal)
    {
        HasColor = hasColor;
        HasTextureUv = hasTextureUv;
        HasNormal = hasNormal;
        HasTangent = false; //TODO: Add vertex tangent information as a 4D vector to this library
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("Vertex Data Kind:")
            .Append(HasNormal ? " Normal" : "")
            .AppendLine(HasColor ? " Color" : "")
            .AppendLine(HasTextureUv ? " Texture" : "")
            .ToString();
    }
}