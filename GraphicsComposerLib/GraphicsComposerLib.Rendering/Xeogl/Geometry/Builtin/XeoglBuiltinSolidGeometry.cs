using GraphicsComposerLib.Geometry.Primitives;

namespace GraphicsComposerLib.Rendering.Xeogl.Geometry.Builtin
{
    public abstract class XeoglBuiltinSolidGeometry : XeoglGeometry
    {
        private GraphicsPrimitiveType3D _primitiveType 
            = GraphicsPrimitiveType3D.Triangles;

        public GraphicsPrimitiveType3D PrimitiveType
        {
            get => _primitiveType;
            set
            {
                switch (_primitiveType)
                {
                    case GraphicsPrimitiveType3D.Points:
                    case GraphicsPrimitiveType3D.Lines:
                    case GraphicsPrimitiveType3D.Triangles:
                        _primitiveType = value;
                        return;
                }

                throw new InvalidOperationException();
            }
        }


        public string PrimitiveTypeName
            => PrimitiveType.GetName();
    }
}