using System.Drawing;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    /// <summary>
    /// This interface represents an object with vertex information like
    /// position, normal, color, or texture coordinates
    /// </summary>
    public interface IGraphicsVertex3D : 
        ITuple3D
    {
        int Index { get; }

        ITuple3D Point { get; }

        Color Color { get; set; }

        ITuple2D TextureUv { get; set; }

        IGraphicsNormal3D Normal { get; }

        double TextureU { get; }

        double TextureV { get; }

        double NormalX { get; }

        double NormalY { get; }

        double NormalZ { get; }

        bool HasTextureUv { get; }

        bool HasNormal { get; }

        bool HasColor { get; }

        GraphicsVertexDataKind3D DataKind { get; }
    }
}