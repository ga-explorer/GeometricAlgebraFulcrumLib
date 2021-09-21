﻿using System;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using EuclideanGeometryLib.GraphicsGeometry;

namespace GraphicsComposerLib.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// A TorusGeometry is a parameterized Geometry that defines a
    /// torus-shaped mesh for attached Meshes.
    /// http://xeogl.org/docs/classes/TorusGeometry.html
    /// </summary>
    public sealed class XeoglTorusGeometry : XeoglBuiltinSolidGeometry
    {
        public static XeoglTorusGeometry Create(ITuple3D center, double radius, double tubeRadius)
        {
            return new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius
            };
        }

        public static XeoglTorusGeometry Create(ITuple3D center, double radius, double tubeRadius, double arcAngle)
        {
            return new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle
            };
        }

        public static XeoglTorusGeometry Create(ITuple3D center, double radius, double tubeRadius, int radialSegments, int tubeSegments)
        {
            return new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            };
        }
        
        public static XeoglTorusGeometry Create(ITuple3D center, double radius, double tubeRadius, double arcAngle, int radialSegments, int tubeSegments)
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


        public MutableTuple3D Center { get; }
            = new MutableTuple3D();

        public double Radius { get; set; } = 1;

        public double TubeRadius { get; set; } = 0.3;

        public int RadialSegments { get; set; } = 32;

        public int TubeSegments { get; set; } = 24;

        public double ArcAngle { get; set; } = DefaultArcValue;

        public override string JavaScriptClassName => "TorusGeometry";


        public XeoglTorusGeometry()
        {
        }

        public XeoglTorusGeometry(ITuple3D center)
        {
            Center.SetTuple(center);
        }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetAttributeValue("center", Center, Tuple3D.Zero)
                .SetAttributeValue("radius", Radius, 1)
                .SetAttributeValue("tube", TubeRadius, 0.3)
                .SetAttributeValue("radialSegments", RadialSegments, 32)
                .SetAttributeValue("tubeSegments", TubeSegments, 24)
                .SetAttributeValue("cfg.arc", ArcAngle, DefaultArcValue);
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