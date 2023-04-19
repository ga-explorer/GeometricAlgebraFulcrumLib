using System.Collections;
using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.Xeogl.Objects;
using TextComposerLib;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Scenes
{
    public class XeoglScene : 
        XeoglComponent, 
        IReadOnlyCollection<XeoglObject>
    {
        private readonly List<XeoglObject> _objectsList 
            = new List<XeoglObject>();


        public bool Transparent { get; set; }

        public Color BackgroundColor { get; set; } 
            = Color.Black;

        public string BackgroundImage { get; set; } 
            = string.Empty;

        public int Count 
            => _objectsList.Count;

        public override string JavaScriptClassName 
            => "Scene";


        public XeoglScene()
        {
            JavaScriptVariableName = "scene";
        }


        public XeoglScene Add(XeoglObject sceneObject)
        {
            if (ReferenceEquals(sceneObject, null))
                throw new ArgumentNullException(nameof(sceneObject));

            sceneObject.ParentSceneName = "scene";
            _objectsList.Add(sceneObject);

            return this;
        }

        public XeoglScene Add(params XeoglObject[] sceneObjectsList)
        {
            foreach (var xeoglObject in sceneObjectsList)
            {
                if (ReferenceEquals(xeoglObject, null))
                    throw new ArgumentNullException(nameof(xeoglObject));

                xeoglObject.ParentSceneName = "scene";
                _objectsList.Add(xeoglObject);
            }

            return this;
        }

        public XeoglScene Add(IEnumerable<XeoglObject> sceneObjectsList)
        {
            foreach (var xeoglObject in sceneObjectsList)
            {
                if (ReferenceEquals(xeoglObject, null))
                    throw new ArgumentNullException(nameof(xeoglObject));

                xeoglObject.ParentSceneName = "scene";
                _objectsList.Add(xeoglObject);
            }

            return this;
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("transparent", Transparent, false)
                .SetRgbaNumbersArrayValue("backgroundColor", BackgroundColor.ToSystemDrawingColor(), Color.Black.ToSystemDrawingColor())
                .SetTextValue("backgroundImage", BackgroundImage.ToHtmlSafeLiteral(), "\"\"");
        }


        public IEnumerator<XeoglObject> GetEnumerator()
        {
            return _objectsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _objectsList.GetEnumerator();
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
