using System.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Structures.Faces;

public sealed record GraphicsFaceDataKind3D
{
    public static GraphicsFaceDataKind3D VoidData { get; }
        = new GraphicsFaceDataKind3D(false, false);

    public static GraphicsFaceDataKind3D ColorData { get; }
        = new GraphicsFaceDataKind3D(true, false);
        
    public static GraphicsFaceDataKind3D NormalData { get; }
        = new GraphicsFaceDataKind3D(false, true);

    public static GraphicsFaceDataKind3D NormalColorData { get; }
        = new GraphicsFaceDataKind3D(true, true);
        

    public bool HasColor { get; }

    public bool HasNormal { get; }


    private GraphicsFaceDataKind3D(bool hasColor, bool hasNormal)
    {
        HasColor = hasColor;
        HasNormal = hasNormal;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("Face Data Kind:")
            .Append(HasNormal ? " Normal" : "")
            .AppendLine(HasColor ? " Color" : "")
            .ToString();
    }
}