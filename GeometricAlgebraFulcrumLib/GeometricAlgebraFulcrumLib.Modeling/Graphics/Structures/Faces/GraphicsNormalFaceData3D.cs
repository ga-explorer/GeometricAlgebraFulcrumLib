using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Faces;

public sealed class GraphicsNormalFaceData3D 
    : IGraphicsFaceData3D
{
    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }
        
    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();
        
    public double NormalX 
        => Normal.X;

    public double NormalY 
        => Normal.Y;

    public double NormalZ 
        => Normal.Z;

    public bool HasColor 
        => false;
        
    public bool HasNormal 
        => true;

    public GraphicsFaceDataKind3D DataKind
        => GraphicsFaceDataKind3D.NormalData;
        

    public GraphicsNormalFaceData3D()
    {
    }

    public GraphicsNormalFaceData3D(ILinFloat64Vector3D normal)
    {
        Normal.Set(normal);
    }

    public GraphicsNormalFaceData3D(IGraphicsFaceData3D vertex)
    {
        Normal.Set(vertex.Normal);
    }
}