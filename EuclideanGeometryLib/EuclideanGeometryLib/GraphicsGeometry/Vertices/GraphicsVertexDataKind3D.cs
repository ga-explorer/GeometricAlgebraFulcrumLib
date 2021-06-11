using System.Text;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public readonly struct GraphicsVertexDataKind3D
    {
        public static GraphicsVertexDataKind3D PositionVertex { get; }
            = new GraphicsVertexDataKind3D(false, false, false);

        public static GraphicsVertexDataKind3D PositionColorVertex { get; }
            = new GraphicsVertexDataKind3D(true, false, false);

        public static GraphicsVertexDataKind3D PositionTextureVertex { get; }
            = new GraphicsVertexDataKind3D(false, true, false);

        public static GraphicsVertexDataKind3D PositionNormalVertex { get; }
            = new GraphicsVertexDataKind3D(false, false, true);

        public static GraphicsVertexDataKind3D PositionNormalColorVertex { get; }
            = new GraphicsVertexDataKind3D(true, false, true);

        public static GraphicsVertexDataKind3D PositionNormalTextureVertex { get; }
            = new GraphicsVertexDataKind3D(false, true, true);
        

        public bool HasColor { get; }

        public bool HasTextureUv { get; }

        public bool HasNormal { get; }


        private GraphicsVertexDataKind3D(bool hasColor, bool hasTextureUv, bool hasNormal)
        {
            HasColor = hasColor;
            HasTextureUv = hasTextureUv;
            HasNormal = hasNormal;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Vertex Data Kind: Position")
                .Append(HasNormal ? ", Normal" : "")
                .AppendLine(HasColor ? ", Color" : "")
                .AppendLine(HasTextureUv ? ", Texture" : "")
                .ToString();
        }
    }
}
