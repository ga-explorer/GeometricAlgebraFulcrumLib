using System.Drawing;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Structures.Faces
{
    public sealed class GraphicsColorFaceData3D 
        : IGraphicsFaceData3D
    {
        public Color Color { get; set; }
            = Color.Black;
        
        public GrNormal3D Normal
            => null;

        public bool HasColor 
            => true;
        
        public bool HasNormal 
            => false;

        public GraphicsFaceDataKind3D DataKind
            => GraphicsFaceDataKind3D.ColorData;
        
        public double NormalX 
            => 0;

        public double NormalY 
            => 0;

        public double NormalZ 
            => 0;
        

        public GraphicsColorFaceData3D()
        {
        }

        public GraphicsColorFaceData3D(Color color)
        {
            Color = color;
        }

        public GraphicsColorFaceData3D(IGraphicsFaceData3D vertex)
        {
            Color = vertex.Color;
        }
    }
}