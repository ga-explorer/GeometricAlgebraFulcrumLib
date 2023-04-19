using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Structures.Faces
{
    /// <summary>
    /// This interface represents an object with vertex information like
    /// position, normal, color, or texture coordinates
    /// </summary>
    public interface IGraphicsFaceData3D
    {
        Color Color { get; set; }
        
        Normal3D Normal { get; }
        
        double NormalX { get; }

        double NormalY { get; }

        double NormalZ { get; }
        
        bool HasNormal { get; }

        bool HasColor { get; }

        GraphicsFaceDataKind3D DataKind { get; }
    }
}