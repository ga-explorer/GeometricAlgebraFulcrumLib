using System;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GeometricAlgebraFulcrumLib.MonoGame
{
    public static class MonoGameUtils
    {
        public static PrimitiveType ToXnaPrimitiveType(this GraphicsPrimitiveType3D primitiveType)
        {
            return primitiveType switch
            {
                GraphicsPrimitiveType3D.PointList => PrimitiveType.PointList,
                GraphicsPrimitiveType3D.LineList => PrimitiveType.LineList,
                GraphicsPrimitiveType3D.LineStrip => PrimitiveType.LineStrip,
                GraphicsPrimitiveType3D.TriangleList => PrimitiveType.TriangleList,
                GraphicsPrimitiveType3D.TriangleStrip => PrimitiveType.TriangleStrip,
                _ => throw new ArgumentOutOfRangeException(nameof(primitiveType))
            };
        }


        public static Vector2 ToXnaVector2(this IPair<Float64Scalar> vector)
        {
            return new Vector2(
                (float)vector.Item1.ScalarValue,
                (float)vector.Item2.ScalarValue
            );
        }

        public static Vector3 ToXnaVector3(this ITriplet<Float64Scalar> vector)
        {
            return new Vector3(
                (float)vector.Item1.ScalarValue,
                (float)vector.Item2.ScalarValue,
                (float)vector.Item3.ScalarValue
            );
        }
        
        public static Vector4 ToXnaVector4(this IQuad<Float64Scalar> vector)
        {
            return new Vector4(
                (float)vector.Item1.ScalarValue,
                (float)vector.Item2.ScalarValue,
                (float)vector.Item3.ScalarValue,
                (float)vector.Item4.ScalarValue
            );
        }


        public static Color ToXnaColor(this SixLabors.ImageSharp.Color color)
        {
            var pixel = color.ToPixel<SixLabors.ImageSharp.PixelFormats.Rgba32>();

            return new Color(
                pixel.R, 
                pixel.G, 
                pixel.B, 
                pixel.A
            );
        }

        
        public static VertexPosition ToXnaVertexPosition(this IGraphicsVertex3D vertex)
        {
            return new VertexPosition(
                vertex.Point.ToXnaVector3()
            );
        }

        public static VertexPositionColor ToXnaVertexPositionColor(this IGraphicsVertex3D vertex)
        {
            return new VertexPositionColor(
                vertex.Point.ToXnaVector3(),
                vertex.Color.ToXnaColor()
            );
        }
        
        public static VertexPositionTexture ToXnaVertexPositionTexture(this IGraphicsVertex3D vertex)
        {
            return new VertexPositionTexture(
                vertex.Point.ToXnaVector3(),
                vertex.ParameterValue.ToXnaVector2()
            );
        }

        public static VertexPositionColorNormal ToXnaVertexPositionColorNormal(this IGraphicsVertex3D vertex)
        {
            return new VertexPositionColorNormal(
                vertex.Point.ToXnaVector3(),
                vertex.Color.ToXnaColor(),
                vertex.Normal.ToXnaVector3()
            );
        }
        
        public static VertexPositionColorTexture ToXnaVertexPositionColorTexture(this IGraphicsVertex3D vertex)
        {
            return new VertexPositionColorTexture(
                vertex.Point.ToXnaVector3(),
                vertex.Color.ToXnaColor(),
                vertex.ParameterValue.ToXnaVector2()
            );
        }
        
        public static VertexPositionNormalTexture ToXnaVertexPositionNormalTexture(this IGraphicsVertex3D vertex)
        {
            return new VertexPositionNormalTexture(
                vertex.Point.ToXnaVector3(),
                vertex.Normal.ToXnaVector3(),
                vertex.ParameterValue.ToXnaVector2()
            );
        }

        public static VertexPositionColorNormalTexture ToXnaVertexPositionColorNormalTexture(this IGraphicsVertex3D vertex)
        {
            return new VertexPositionColorNormalTexture(
                vertex.Point.ToXnaVector3(),
                vertex.Color.ToXnaColor(),
                vertex.Normal.ToXnaVector3(),
                vertex.ParameterValue.ToXnaVector2()
            );
        }
    }
}
