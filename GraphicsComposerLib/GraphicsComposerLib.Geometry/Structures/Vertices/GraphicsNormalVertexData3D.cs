using System;
using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Structures.Vertices
{
    public sealed class GraphicsNormalVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public ITuple2D TextureUv
        {
            get => Tuple2D.Zero;
            set => throw new InvalidOperationException();
        }

        public GrNormal3D Normal { get; }
            = new GrNormal3D();
        
        public double TextureU 
            => 0;

        public double TextureV 
            => 0;

        public double NormalX 
            => Normal.X;

        public double NormalY 
            => Normal.Y;

        public double NormalZ 
            => Normal.Z;

        public bool HasColor 
            => false;

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.NormalData;
        

        public GraphicsNormalVertexData3D()
        {
        }

        public GraphicsNormalVertexData3D(ITuple3D normal)
        {
            Normal.Set(normal);
        }

        public GraphicsNormalVertexData3D(IGraphicsVertexData3D vertex)
        {
            Normal.Set(vertex.Normal);
        }

        
        public Tuple3D GetDisplacedPoint(ITuple3D point, double d)
        {
            return new Tuple3D(
                point.X + d * Normal.X,
                point.Y + d * Normal.Y,
                point.Z + d * Normal.Z
            );
        }
    }
}