using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Geometry.Builtin;

public abstract class XeoglBuiltinSolidGeometry : XeoglGeometry
{
    private GraphicsPrimitiveType3D _primitiveType 
        = GraphicsPrimitiveType3D.TriangleList;

    public GraphicsPrimitiveType3D PrimitiveType
    {
        get => _primitiveType;
        set
        {
            switch (_primitiveType)
            {
                case GraphicsPrimitiveType3D.PointList:
                case GraphicsPrimitiveType3D.LineList:
                case GraphicsPrimitiveType3D.TriangleList:
                    _primitiveType = value;
                    return;
            }

            throw new InvalidOperationException();
        }
    }


    public string PrimitiveTypeName
        => PrimitiveType.GetName();
}