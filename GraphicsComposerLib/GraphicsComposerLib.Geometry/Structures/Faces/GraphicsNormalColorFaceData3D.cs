using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Structures.Faces
{
    public sealed class GraphicsNormalColorFaceData3D 
        : IGraphicsFaceData3D
    {
        public Color Color { get; set; }
            = Color.Black;
        
        public GrNormal3D Normal { get; }
            = new GrNormal3D();
        
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

        public GraphicsNormalColorFaceData3D(Color color, ITuple3D normal)
        {
            Color = color;
            Normal.Set(normal);
        }

        public GraphicsNormalColorFaceData3D(IGraphicsFaceData3D point)
        {
            Color = point.Color;
        }
    }
}