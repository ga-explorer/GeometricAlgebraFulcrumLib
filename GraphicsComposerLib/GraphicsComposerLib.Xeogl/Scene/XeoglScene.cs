using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using GraphicsComposerLib.Xeogl.Objects;
using TextComposerLib;

namespace GraphicsComposerLib.Xeogl.Scene
{
    public class XeoglScene 
        : XeoglComponent, IReadOnlyCollection<XeoglObject>
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


        public XeoglScene AddObject(XeoglObject xeoglObject)
        {
            if (ReferenceEquals(xeoglObject, null))
                throw new ArgumentNullException(nameof(xeoglObject));

            xeoglObject.ParentSceneName = "scene";
            _objectsList.Add(xeoglObject);

            return this;
        }

        public XeoglScene AddObjects(params XeoglObject[] xeoglObjectsList)
        {
            foreach (var xeoglObject in xeoglObjectsList)
            {
                if (ReferenceEquals(xeoglObject, null))
                    throw new ArgumentNullException(nameof(xeoglObject));

                xeoglObject.ParentSceneName = "scene";
                _objectsList.Add(xeoglObject);
            }

            return this;
        }

        public XeoglScene AddObjects(IEnumerable<XeoglObject> xeoglObjectsList)
        {
            foreach (var xeoglObject in xeoglObjectsList)
            {
                if (ReferenceEquals(xeoglObject, null))
                    throw new ArgumentNullException(nameof(xeoglObject));

                xeoglObject.ParentSceneName = "scene";
                _objectsList.Add(xeoglObject);
            }

            return this;
        }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("transparent", Transparent, false)
                .SetAttributeValueRgba("backgroundColor", BackgroundColor, Color.Black)
                .SetAttributeValue("backgroundImage", BackgroundImage.ToHtmlSafeLiteral(), "\"\"");
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
