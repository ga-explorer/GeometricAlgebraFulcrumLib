using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// A TorusGeometry is a parameterized Geometry that defines a
    /// torus-shaped mesh for attached Meshes.
    /// http://xeogl.org/docs/classes/TorusGeometry.html
    /// </summary>
    public sealed class XeoglTorusGeometry : XeoglBuiltinSolidGeometry
    {
        public static XeoglTorusGeometry Create(IFloat64Vector3D center, double radius, double tubeRadius)
        {
            return new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius
            };
        }

        public static XeoglTorusGeometry Create(IFloat64Vector3D center, double radius, double tubeRadius, double arcAngle)
        {
            return new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle
            };
        }

        public static XeoglTorusGeometry Create(IFloat64Vector3D center, double radius, double tubeRadius, int radialSegments, int tubeSegments)
        {
            return new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            };
        }
        
        public static XeoglTorusGeometry Create(IFloat64Vector3D center, double radius, double tubeRadius, double arcAngle, int radialSegments, int tubeSegments)
        {
            return new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            };
        }

        public static XeoglTorusGeometry Create(double radius, double tubeRadius)
        {
            return new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius
            };
        }

        public static XeoglTorusGeometry Create(double radius, double tubeRadius, double arcAngle)
        {
            return new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle
            };
        }

        public static XeoglTorusGeometry Create(double radius, double tubeRadius, int radialSegments, int tubeSegments)
        {
            return new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            };
        }
        
        public static XeoglTorusGeometry Create(double radius, double tubeRadius, double arcAngle, int radialSegments, int tubeSegments)
        {
            return new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            };
        }


        private const double DefaultArcValue = 0.5 * Math.PI;


        public Float64Vector3DComposer Center { get; }
            = Float64Vector3DComposer.Create();

        public double Radius { get; set; } = 1;

        public double TubeRadius { get; set; } = 0.3;

        public int RadialSegments { get; set; } = 32;

        public int TubeSegments { get; set; } = 24;

        public double ArcAngle { get; set; } = DefaultArcValue;

        public override string JavaScriptClassName => "TorusGeometry";


        public XeoglTorusGeometry()
        {
        }

        public XeoglTorusGeometry(IFloat64Vector3D center)
        {
            Center.SetVector(center);
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetNumbersArrayValue("center", Center, Float64Vector3D.Zero)
                .SetValue("radius", Radius, 1)
                .SetValue("tube", TubeRadius, 0.3)
                .SetValue("radialSegments", RadialSegments, 32)
                .SetValue("tubeSegments", TubeSegments, 24)
                .SetValue("cfg.arc", ArcAngle, DefaultArcValue);
        }

        //public override string ToString()
        //{
        //    var composer = new XeoglAttributesTextComposer();

        //    UpdateAttributesComposer(composer);

        //    return composer
        //        .AppendXeoglConstructorCall(this)
        //        .ToString();
        //}
    }
}