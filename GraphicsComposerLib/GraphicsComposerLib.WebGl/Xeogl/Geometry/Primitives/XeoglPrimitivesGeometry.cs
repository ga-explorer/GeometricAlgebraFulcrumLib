using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.GraphicsGeometry;
using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Geometry.Primitives
{
    public abstract class XeoglPrimitivesGeometry : XeoglGeometry
    {
        public abstract IGraphicsGeometry3D GraphicsGeometry { get; }

        public GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsGeometry.PrimitiveType;

        public string PrimitiveTypeName 
            => GraphicsGeometry.PrimitiveType.GetName();

        public override string JavaScriptClassName 
            => "Geometry";

        public int VerticesCount 
            => GraphicsGeometry.VertexPoints.Count;

        public IEnumerable<ITuple3D> VertexPositions
            => GraphicsGeometry.VertexPoints;

        public IEnumerable<int> VertexIndices
            => GraphicsGeometry.VertexIndices;

        public ITuple3D GetVertexPoint(int vertexIndex)
            => GraphicsGeometry.VertexPoints[vertexIndex];
    }
}