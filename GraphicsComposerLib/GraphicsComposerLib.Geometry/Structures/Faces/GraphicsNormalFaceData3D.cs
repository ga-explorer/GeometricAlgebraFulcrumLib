using System;

using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Structures.Faces
{
    public sealed class GraphicsNormalFaceData3D 
        : IGraphicsFaceData3D
    {
        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }
        
        public GrNormal3D Normal { get; }
            = new GrNormal3D();
        
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

        public GraphicsNormalFaceData3D(ITuple3D normal)
        {
            Normal.Set(normal);
        }

        public GraphicsNormalFaceData3D(IGraphicsFaceData3D vertex)
        {
            Normal.Set(vertex.Normal);
        }
    }
}