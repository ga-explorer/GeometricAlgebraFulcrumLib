using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.Borders.Space3D;
using EuclideanGeometryLib.GraphicsGeometry;
using GraphicsComposerLib.Xeogl.Constants;
using GraphicsComposerLib.Xeogl.Geometry;
using GraphicsComposerLib.Xeogl.Materials;
using GraphicsComposerLib.Xeogl.Transforms;
using TextComposerLib;
using TextComposerLib.Text;
using TextComposerLib.Text.Attributes;

namespace GraphicsComposerLib.Xeogl
{
    /// <summary>
    /// This class is internally used to compose code of individual xeogl components
    /// </summary>
    internal sealed class XeoglCodeComposer 
        : AttributesTextComposer
    {
        public XeoglCodeComposer()
        {
            KeyValueSeparator = ": ";
            AttributesSeparator = "," + Environment.NewLine;
        }


        public XeoglCodeComposer SetAttributeValue(string key, double value, double valueDefault)
        {
            base.SetAttributeValue(
                key, 
                value.ToString("G"), 
                valueDefault.ToString("G")
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, IEnumerable<double> value, string valueDefault)
        {
            base.SetAttributeValue(
                key, 
                value.ToXeoglNumbersArrayText(), 
                valueDefault
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, int value, int valueDefault)
        {
            base.SetAttributeValue(
                key, 
                value.ToString(), 
                valueDefault.ToString()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, bool value)
        {
            base.SetAttributeValue(key, value ? "true" : "false");

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, bool value, bool valueDefault)
        {
            base.SetAttributeValue(
                key, 
                value ? "true" : "false",
                valueDefault ? "true" : "false"
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, ITuple2D value, ITuple3D valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglNumbersArrayText(),
                valueDefault.ToXeoglNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, ITuple3D value)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, ITuple3D value, ITuple3D valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglNumbersArrayText(),
                valueDefault.ToXeoglNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, Matrix4X4 value, ITuple3D valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglNumbersArrayText(),
                valueDefault.ToXeoglNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, IBoundingBox3D value, ITuple3D valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglNumbersArrayText(),
                valueDefault.ToXeoglNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValueRgb(string key, Color value, Color valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglRgbNumbersArrayText(),
                valueDefault.ToXeoglRgbNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValueRgb(string key, Color value)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglRgbNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValueRgba(string key, Color value, Color valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglRgbaNumbersArrayText(),
                valueDefault.ToXeoglRgbaNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValueRgba(string key, Color value)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglRgbaNumbersArrayText()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglGeometry value, XeoglGeometry valueDefault)
        {
            base.SetAttributeValue(
                key, 
                value?.ToString() ?? string.Empty, 
                valueDefault?.ToString() ?? string.Empty
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglMaterial value, XeoglMaterial valueDefault)
        {
            base.SetAttributeValue(
                key,
                value?.ToString() ?? string.Empty,
                valueDefault?.ToString() ?? string.Empty
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglFresnelEffect value, XeoglFresnelEffect valueDefault)
        {
            base.SetAttributeValue(
                key,
                value?.ToString() ?? string.Empty,
                valueDefault?.ToString() ?? string.Empty
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, GraphicsPrimitiveType3D value, GraphicsPrimitiveType3D valueDefault)
        {
            base.SetAttributeValue(
                key, 
                value.GetName().DoubleQuote(),
                valueDefault.GetName().DoubleQuote()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglSpace value, XeoglSpace valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.GetName().DoubleQuote(),
                valueDefault.GetName().DoubleQuote()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglWindingDirection value, XeoglWindingDirection valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.GetName().DoubleQuote(),
                valueDefault.GetName().DoubleQuote()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglBillboardBehaviour value, XeoglBillboardBehaviour valueDefault)
        {
            base.SetAttributeValue(
                key, 
                value.GetName().DoubleQuote(),
                valueDefault.GetName().DoubleQuote()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglCameraProjectionType value, XeoglCameraProjectionType valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.GetName().DoubleQuote(),
                valueDefault.GetName().DoubleQuote()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, XeoglPerspectiveFieldOfViewAxis value, XeoglPerspectiveFieldOfViewAxis valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.GetName().DoubleQuote(),
                valueDefault.GetName().DoubleQuote()
            );

            return this;
        }
        
        public XeoglCodeComposer SetAttributeValue(string key, XeoglAlphaMode value, XeoglAlphaMode valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.GetName().DoubleQuote(),
                valueDefault.GetName().DoubleQuote()
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, IEnumerable<ITuple3D> value, string commentPrefix, string valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglNumbersArrayText(commentPrefix),
                valueDefault
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, IEnumerable<Tuple3D> value, string commentPrefix, string valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.Cast<ITuple3D>().ToXeoglNumbersArrayText(commentPrefix),
                valueDefault
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, IEnumerable<ITuple2D> value, string commentPrefix, string valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.ToXeoglNumbersArrayText(commentPrefix),
                valueDefault
            );

            return this;
        }

        public XeoglCodeComposer SetAttributeValue(string key, IEnumerable<Tuple2D> value, string commentPrefix, string valueDefault)
        {
            base.SetAttributeValue(
                key,
                value.Cast<ITuple2D>().ToXeoglNumbersArrayText(commentPrefix),
                valueDefault
            );

            return this;
        }


        public XeoglCodeComposer RemoveTransformAttributes()
        {
            RemoveAttributes(
                "matrix",
                "rotation",
                "scale",
                "position",
                "quaternion"
            );

            return this;
        }

        public XeoglCodeComposer SetTransformAttributes(IXeoglTransform transform)
        {
            RemoveTransformAttributes();

            if (ReferenceEquals(transform, null))
                return this;

            if (transform.ContainsMatrix)
            {
                SetAttributeValue("matrix", transform.GetMatrixText());
                return this;
            }

            if (transform.ContainsQuaternion)
                SetAttributeValue("quaternion", transform.GetQuaternionText());

            if (transform.ContainsRotate)
                SetAttributeValue("rotation", transform.GetRotateText());

            if (transform.ContainsScale)
                SetAttributeValue("scale", transform.GetScaleText());

            if (transform.ContainsTranslate)
                SetAttributeValue("position", transform.GetTranslateText());

            return this;
        }

        //public XeoglAttributesTextComposer SetAttributeValue(string key, XeoglGeometry value)
        //{
        //    base.SetAttributeValue(
        //        key,
        //        value?.ToString() ?? string.Empty
        //    );

        //    return this;
        //}

        
        public XeoglCodeComposer AppendConstructorCallCode(XeoglComponent xeoglComponent, string parentName = "")
        {
            if (string.IsNullOrEmpty(xeoglComponent.JavaScriptVariableName))
            {
                AppendAtNewLine("new xeogl.")
                    .Append(xeoglComponent.JavaScriptClassName);
            }
            else
            {
                if (!string.IsNullOrEmpty(xeoglComponent.Description))
                    AppendAtNewLine(
                        xeoglComponent.Description.PrefixTextLines("//")
                    );

                AppendAtNewLine("const ")
                    .Append(xeoglComponent.JavaScriptVariableName)
                    .Append(" = new xeogl.")
                    .Append(xeoglComponent.JavaScriptClassName);
            }

            if (!ContainsNonDefaultAttributes)
            {
                if (string.IsNullOrEmpty(parentName))
                    Append("()");
                else
                    Append("(").Append(parentName).Append(");");

                return this;
            }

            Append("(");

            if (!string.IsNullOrEmpty(parentName))
                Append(parentName).Append(", ");
            
            AppendLine("{")
                .IncreaseIndentation()
                .AppendAtNewLine(AttributesText)
                .DecreaseIndentation()
                .AppendAtNewLine("})");

            return this;
        }

        public XeoglCodeComposer AppendAttributesSetCode(string variableName)
        {
            foreach (var pair in KeyValuesPairs)
                AppendAtNewLine(variableName)
                    .Append(".")
                    .Append(pair.Key)
                    .Append(" = ")
                    .Append(pair.Value)
                    .Append(";");

            return this;
        }
    }
}
