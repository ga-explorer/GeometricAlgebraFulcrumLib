using System.Collections.Generic;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Primitives;
using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Geometry.Primitives
{
    public abstract class XeoglPrimitivesGeometry : XeoglGeometry
    {
        public abstract IGraphicsPrimitiveGeometry3D GraphicsGeometry { get; }

        public GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsGeometry.PrimitiveType;

        public string PrimitiveTypeName 
            => GraphicsGeometry.PrimitiveType.GetName();

        public override string JavaScriptClassName 
            => "Geometry";

        public int VertexCount 
            => GraphicsGeometry.VertexCount;

        public IEnumerable<ITuple3D> VertexPoints
            => GraphicsGeometry.GeometryPoints;

        public IEnumerable<int> VertexIndices
            => GraphicsGeometry.GeometryIndices;

        public ITuple3D GetVertexPoint(int vertexIndex)
        {
            return GraphicsGeometry.GetGeometryPoint(vertexIndex);
        }
    }
}