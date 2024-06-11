using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Geometry.Primitives;

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

    public IEnumerable<ILinFloat64Vector3D> VertexPoints
        => GraphicsGeometry.GeometryPoints;

    public IEnumerable<int> VertexIndices
        => GraphicsGeometry.GeometryIndices;

    public ILinFloat64Vector3D GetVertexPoint(int vertexIndex)
    {
        return GraphicsGeometry.GetGeometryPoint(vertexIndex);
    }
}