using System;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
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
        
        public Normal3D Normal { get; }
            = new Normal3D();
        
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

        public GraphicsNormalFaceData3D(IFloat64Tuple3D normal)
        {
            Normal.Set(normal);
        }

        public GraphicsNormalFaceData3D(IGraphicsFaceData3D vertex)
        {
            Normal.Set(vertex.Normal);
        }
    }
}