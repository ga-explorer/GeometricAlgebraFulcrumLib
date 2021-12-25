using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Structures.Vertices
{
    /// <summary>
    /// This interface represents an object with vertex information like
    /// position, normal, color, or texture coordinates
    /// </summary>
    public interface IGraphicsVertexData3D
    {
        GraphicsVertexDataKind3D DataKind { get; }

        Color Color { get; set; }

        ITuple2D TextureUv { get; set; }

        GrNormal3D Normal { get; }

        double TextureU { get; }

        double TextureV { get; }

        double NormalX { get; }

        double NormalY { get; }

        double NormalZ { get; }

        bool HasTextureUv { get; }

        bool HasNormal { get; }

        bool HasColor { get; }
    }
}