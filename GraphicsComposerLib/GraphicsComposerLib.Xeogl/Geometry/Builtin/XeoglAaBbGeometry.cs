using EuclideanGeometryLib.Borders.Space3D.Mutable;

namespace GraphicsComposerLib.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// An AABBGeometry is a Geometry that shows the extents of a
    /// World-space axis-aligned bounding box (AABB).
    /// http://xeogl.org/docs/classes/AABBGeometry.html
    /// </summary>
    public sealed class XeoglAaBbGeometry 
        : XeoglGeometry
    {
        public string Target { get; set; }

        public MutableBoundingBox3D TargetAaBb { get; }
            = MutableBoundingBox3D.Create();

        public override string JavaScriptClassName => "AABBGeometry";


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("target", Target)
                .SetAttributeValue("targetAABB", TargetAaBb.ToXeoglNumbersArrayText(), "[]");
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