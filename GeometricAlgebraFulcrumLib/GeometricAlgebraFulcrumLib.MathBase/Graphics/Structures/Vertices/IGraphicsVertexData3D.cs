using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Structures.Vertices
{
    /// <summary>
    /// This interface represents an object with vertex information like
    /// position, normal, color, or texture coordinates
    /// </summary>
    public interface IGraphicsVertexData3D
    {
        GraphicsVertexDataKind3D DataKind { get; }

        Color Color { get; set; }

        IFloat64Vector2D TextureUv { get; set; }

        Normal3D Normal { get; }

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