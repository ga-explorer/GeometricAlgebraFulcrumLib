namespace GraphicsComposerLib.Xeogl
{
    /// <summary>
    /// This class represents a xeogl component like a geometry, camera, light, etc.
    /// </summary>
    public abstract class XeoglComponent : IXeoglComponent
    {
        public abstract string JavaScriptClassName { get; }

        public string JavaScriptVariableName { get; set; } 
            = string.Empty;

        public string Description { get; set; } 
            = string.Empty;

        public string Id { get; set; } 
            = string.Empty;


        internal virtual void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            composer.SetAttributeValue(
                "id", 
                Id, 
                string.IsNullOrEmpty
            );
        }


        public override string ToString()
        {
            var composer = new XeoglCodeComposer();

            UpdateAttributesComposer(composer);

            return composer
                .AppendConstructorCallCode(this)
                .ToString();
        }
    }
}
