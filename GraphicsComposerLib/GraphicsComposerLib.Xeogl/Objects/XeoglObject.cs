using System.Drawing;
using GraphicsComposerLib.Xeogl.Transforms;
using TextComposerLib;

namespace GraphicsComposerLib.Xeogl.Objects
{
    /// <summary>
    /// http://xeogl.org/docs/classes/Object.html
    /// </summary>
    public abstract class XeoglObject : XeoglComponent
    {
        private readonly string _colorizeDefaultValue =
            Color.White.ToXeoglRgbNumbersArrayText();


        public string ParentSceneName { get; internal set; }
            = string.Empty;

        public string EntityType { get; set; }

        public IXeoglTransform Transform { get; set; }
            = new XeoglERotateScaleTranslateTransform();

        public bool Visible { get; set; } = true;

        public bool EnableCulling { get; set; }

        public bool EnablePicking { get; set; } = true;

        public bool EnableClipping { get; set; } = true;

        public bool EnableColliding { get; set; } = true;

        public bool EnableShadowCasting { get; set; } = true;

        public bool EnableShadowReceiving { get; set; } = true;

        public bool IsOutlined { get; set; }

        public bool IsGhosted { get; set; }

        public bool IsHighlighted { get; set; }

        public bool IsSelected { get; set; }

        public bool AreEdgesEmphasized { get; set; }

        public bool AaBbVisible { get; set; }

        public Color ColorFactor { get; set; } = Color.White;

        public bool InheritStates { get; set; } = true;


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("entityType", EntityType.DoubleQuote(), "\"\"");

            composer
                .SetTransformAttributes(Transform)
                .SetAttributeValue("visible", Visible, true)
                .SetAttributeValue("culled", EnableCulling, false)
                .SetAttributeValue("pickable", EnablePicking, true)
                .SetAttributeValue("clippable", EnableClipping, true)
                .SetAttributeValue("collidable", EnableColliding, true)
                .SetAttributeValue("castShadow", EnableShadowCasting, true)
                .SetAttributeValue("receiveShadow", EnableShadowReceiving, true)
                .SetAttributeValue("outlined", IsOutlined, false)
                .SetAttributeValue("ghosted", IsGhosted, false)
                .SetAttributeValue("highlighted", IsHighlighted, false)
                .SetAttributeValue("selected", IsSelected, false)
                .SetAttributeValue("edges", AreEdgesEmphasized, false)
                .SetAttributeValue("aabbVisible", AaBbVisible, false)
                .SetAttributeValue("inheritStates", InheritStates, true)
                .SetAttributeValueRgb("colorize", ColorFactor, Color.White)
                .SetAttributeValue("opacity", ColorFactor.A / 255.0d, 1);
        }
    }
}
