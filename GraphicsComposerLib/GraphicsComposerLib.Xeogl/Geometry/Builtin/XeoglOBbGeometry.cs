namespace GraphicsComposerLib.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// An OBBGeometry is a Geometry that shows the extents of an
    /// oriented bounding box (OBB).
    /// http://xeogl.org/docs/classes/OBBGeometry.html
    /// </summary>
    public sealed class XeoglOBbGeometry
        : XeoglGeometry
    {
        public string TargetOBb { get; set; }

        public string Target { get; set; }

        public override string JavaScriptClassName => "OBBGeometry";


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("target", Target)
                .SetAttributeValue("targetOBB", TargetOBb, "[]");
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