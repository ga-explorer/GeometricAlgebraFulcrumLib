using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.Xeogl.Geometry.Primitives
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

        public IEnumerable<IFloat64Tuple3D> VertexPoints
            => GraphicsGeometry.GeometryPoints;

        public IEnumerable<int> VertexIndices
            => GraphicsGeometry.GeometryIndices;

        public IFloat64Tuple3D GetVertexPoint(int vertexIndex)
        {
            return GraphicsGeometry.GetGeometryPoint(vertexIndex);
        }
    }
}