using System.Collections;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GraphicsComposerLib.Rendering.Xeogl.Geometry;
using GraphicsComposerLib.Rendering.Xeogl.Materials;
using GraphicsComposerLib.Rendering.Xeogl.Transforms;
using TextComposerLib.Code.JavaScript;
using TextComposerLib.Code.JavaScript.Obsolete;

namespace GraphicsComposerLib.Rendering.Xeogl.Objects
{
    /// <summary>
    /// A Group is an Object that groups other Objects.
    /// http://xeogl.org/docs/classes/Group.html
    /// http://xeogl.org/docs/classes/Object.html#creating-an-object-hierarchy
    /// </summary>
    public class XeoglObjectsGroup 
        : XeoglObject, IReadOnlyCollection<XeoglObject>
    {
        private readonly List<XeoglObject> _childObjects 
            = new List<XeoglObject>();


        public override string JavaScriptClassName => "Group";

        public int Count 
            => _childObjects.Count;


        public XeoglObjectsGroup AddChild(XeoglObject childObject)
        {
            if (ReferenceEquals(childObject, null))
                throw new ArgumentNullException(nameof(childObject));

            _childObjects.Add(childObject);

            return this;
        }

        public XeoglObjectsGroup AddChildren(params XeoglObject[] childObjectsList)
        {
            foreach (var childObject in childObjectsList)
            {
                if (ReferenceEquals(childObject, null))
                    throw new ArgumentNullException(nameof(childObject));

                _childObjects.Add(childObject);
            }

            return this;
        }

        public XeoglObjectsGroup AddChildren(IEnumerable<XeoglObject> childObjectsList)
        {
            foreach (var childObject in childObjectsList)
            {
                if (ReferenceEquals(childObject, null))
                    throw new ArgumentNullException(nameof(childObject));

                _childObjects.Add(childObject);
            }

            return this;
        }

        public XeoglObjectsGroup AddChild(XeoglGeometry geometry, XeoglMaterial material)
        {
            var mesh = new XeoglMesh()
            {
                Geometry = geometry,
                Material = material
            };

            _childObjects.Add(mesh);

            return this;
        }

        public XeoglObjectsGroup AddChild(XeoglGeometry geometry, XeoglMaterial material, SquareMatrix4 matrix)
        {
            var mesh = new XeoglMesh()
            {
                Geometry = geometry,
                Material = material,
                Transform = new XeoglMatrixTransform(matrix)
            };

            _childObjects.Add(mesh);

            return this;
        }

        public XeoglObjectsGroup AddChild(XeoglGeometry geometry, XeoglMaterial material, IXeoglTransform transform)
        {
            var mesh = new XeoglMesh()
            {
                Geometry = geometry,
                Material = material,
                Transform = transform
            };

            _childObjects.Add(mesh);

            return this;
        }


        public IEnumerator<XeoglObject> GetEnumerator()
        {
            return _childObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _childObjects.GetEnumerator();
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetTextValue("children", _childObjects.ToJavaScriptObjectsArrayText(), "[]");
        }
    }
}
