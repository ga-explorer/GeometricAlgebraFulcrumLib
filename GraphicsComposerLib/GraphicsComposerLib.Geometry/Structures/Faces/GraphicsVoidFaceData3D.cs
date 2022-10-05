using System;

using GraphicsComposerLib.Geometry.Primitives.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Structures.Faces
{
    public sealed record GraphicsVoidFaceData3D 
        : IGraphicsFaceData3D
    {
        public static GraphicsVoidFaceData3D DefaultData { get; }
            = new GraphicsVoidFaceData3D();


        public GrNormal3D Normal
            => null;

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }
        
        public bool HasNormal 
            => false;

        public bool HasColor 
            => false;

        public GraphicsFaceDataKind3D DataKind
            => GraphicsFaceDataKind3D.VoidData;
        
        public double NormalX 
            => 0;

        public double NormalY 
            => 0;

        public double NormalZ 
            => 0;
        

        private GraphicsVoidFaceData3D()
        {
        }
    }
}