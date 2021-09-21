﻿using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using EuclideanGeometryLib.GraphicsGeometry;

namespace GraphicsComposerLib.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// A PlaneGeometry is a parameterized Geometry that defines a
    /// plane-shaped mesh for attached Meshes.
    /// http://xeogl.org/docs/classes/PlaneGeometry.html
    /// </summary>
    public sealed class XeoglPlaneGeometry : XeoglBuiltinSolidGeometry
    {
        public static XeoglPlaneGeometry Create(double size, int segments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size,
                ZSize = size,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(double size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size,
                ZSize = size,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }

        public static XeoglPlaneGeometry Create(ITuple2D size, int segments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(ITuple2D size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }

        public static XeoglPlaneGeometry Create(ITuple3D center, double size, int segments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size,
                ZSize = size,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(ITuple3D center, double size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size,
                ZSize = size,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }

        public static XeoglPlaneGeometry Create(ITuple3D center, ITuple2D size, int segments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(ITuple3D center, ITuple2D size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }


        public MutableTuple3D Center { get; }
            = new MutableTuple3D();

        public double XSize { get; set; } = 1;

        public double ZSize { get; set; } = 1;

        public int XSegments { get; set; } = 1;

        public int ZSegments { get; set; } = 1;

        public override string JavaScriptClassName => "PlaneGeometry";


        public XeoglPlaneGeometry()
        {
        }

        public XeoglPlaneGeometry(ITuple3D center)
        {
            Center.SetTuple(center);
        }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetAttributeValue("center", Center, Tuple3D.Zero)
                .SetAttributeValue("xSize", XSize, 1)
                .SetAttributeValue("ZSize", ZSize, 1)
                .SetAttributeValue("xSegments", XSegments, 1)
                .SetAttributeValue("zSegments", ZSegments, 1);
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