namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Lights
{
    /// <summary>
    /// http://xeogl.org/docs/classes/AmbientLight.html
    /// </summary>
    public sealed class XeoglAmbientLight : XeoglLight
    {
        public override string JavaScriptClassName => "AmbientLight";

        
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