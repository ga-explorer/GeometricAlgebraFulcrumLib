using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Faces;

public sealed class GraphicsNormalColorFaceData3D 
    : IGraphicsFaceData3D
{
    public Color Color { get; set; }
        = Color.Black;
        
    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();
        
    public double NormalX 
        => Normal.X;

    public double NormalY 
        => Normal.Y;

    public double NormalZ 
        => Normal.Z;

    public bool HasColor 
        => true;
        
    public bool HasNormal 
        => true;

    public GraphicsFaceDataKind3D DataKind
        => GraphicsFaceDataKind3D.NormalColorData;
        

    public GraphicsNormalColorFaceData3D()
    {
    }

    public GraphicsNormalColorFaceData3D(Color color)
    {
        Color = color;
    }

    public GraphicsNormalColorFaceData3D(Color color, ILinFloat64Vector3D normal)
    {
        Color = color;
        Normal.Set(normal);
    }

    public GraphicsNormalColorFaceData3D(IGraphicsFaceData3D point)
    {
        Color = point.Color;
    }
}