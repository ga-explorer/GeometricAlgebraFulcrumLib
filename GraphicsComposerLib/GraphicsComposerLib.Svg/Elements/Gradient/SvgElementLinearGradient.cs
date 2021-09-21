using System.Drawing;
using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;
using GraphicsComposerLib.Svg.Transforms;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Elements.Gradient
{
    /// <summary>
    /// https://www.w3.org/TR/SVG11/pservers.html
    /// </summary>
    public sealed class SvgElementLinearGradient : SvgElement, ISvgGradientElement
    {
        public static SvgElementLinearGradient Create()
        {
            return new SvgElementLinearGradient();
        }

        public static SvgElementLinearGradient Create(string id)
        {
            return new SvgElementLinearGradient() { Id = id };
        }


        public override string ElementName => "linearGradient";


        public SvgEavString<SvgElementLinearGradient> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavString<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementLinearGradient> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavString<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementLinearGradient> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavString<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementLinearGradient> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementLinearGradient>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementLinearGradient> StartPointX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.X1;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavLength<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementLinearGradient> StartPointY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Y1;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavLength<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementLinearGradient> EndPointX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.X2;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavLength<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementLinearGradient> EndPointY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Y2;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavLength<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueGradientUnits, SvgElementLinearGradient> GradientUnits
        {
            get
            {
                var attrInfo = SvgAttributeUtils.GradientUnits;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgValueGradientUnits, SvgElementLinearGradient>;

                var attrValue1 = new SvgEav<SvgValueGradientUnits, SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgTransform, SvgElementLinearGradient> GradientTransform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.GradientTransform;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementLinearGradient>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueGradientSpreadMethod, SvgElementLinearGradient> GradientSpreadMethod
        {
            get
            {
                var attrInfo = SvgAttributeUtils.GradientSpreadMethod;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgValueGradientSpreadMethod, SvgElementLinearGradient>;

                var attrValue1 = new SvgEav<SvgValueGradientSpreadMethod, SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementLinearGradient> HRef
        {
            get
            {
                var attrInfo = SvgAttributeUtils.HRef;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementLinearGradient>;

                var attrValue1 = new SvgEavString<SvgElementLinearGradient>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementLinearGradient()
        {
        }


        public SvgElementLinearGradient SetStartPoint(double x1, double y1)
        {
            StartPointX.Length = x1;
            StartPointY.Length = y1;

            return this;
        }

        public SvgElementLinearGradient SetStartPoint(double x1, double y1, SvgValueLengthUnit unit)
        {
            StartPointX.SetTo(x1, unit);
            StartPointY.SetTo(y1, unit);

            return this;
        }

        public SvgElementLinearGradient SetEndPoint(double x2, double y2)
        {
            EndPointX.Length = x2;
            EndPointY.Length = y2;

            return this;
        }

        public SvgElementLinearGradient SetEndPoint(double x2, double y2, SvgValueLengthUnit unit)
        {
            EndPointX.SetTo(x2, unit);
            EndPointY.SetTo(y2, unit);

            return this;
        }

        public SvgElementLinearGradient SetGradientVector(double x1, double y1, double x2, double y2)
        {
            StartPointX.Length = x1;
            StartPointY.Length = y1;
            EndPointX.Length = x2;
            EndPointY.Length = y2;

            return this;
        }

        public SvgElementLinearGradient SetGradientVector(double x1, double y1, double x2, double y2, SvgValueLengthUnit unit)
        {
            StartPointX.SetTo(x1, unit);
            StartPointY.SetTo(y1, unit);
            EndPointX.SetTo(x2, unit);
            EndPointY.SetTo(y2, unit);

            return this;
        }


        public SvgElementLinearGradient AppendAbsoluteStop(double offset, Color color)
        {
            Contents.Append(
                SvgElementGradientStop
                    .Create()
                    .SetAbsoluteStop(offset, color)
            );

            return this;
        }

        public SvgElementLinearGradient AppendAbsoluteStop(double offset, Color color, double opacity)
        {
            Contents.Append(
                SvgElementGradientStop
                    .Create()
                    .SetAbsoluteStop(offset, color, opacity)
            );

            return this;
        }

        public SvgElementLinearGradient AppendRelativeStop(double offset, Color color)
        {
            Contents.Append(
                SvgElementGradientStop
                    .Create()
                    .SetRelativeStop(offset, color)
            );

            return this;
        }

        public SvgElementLinearGradient AppendRelativeStop(double offset, Color color, double opacity)
        {
            Contents.Append(
                SvgElementGradientStop
                    .Create()
                    .SetRelativeStop(offset, color, opacity)
            );

            return this;
        }

        public SvgElementLinearGradient PrependAbsoluteStop(double offset, Color color)
        {
            Contents.Prepend(
                SvgElementGradientStop
                    .Create()
                    .SetAbsoluteStop(offset, color)
            );

            return this;
        }

        public SvgElementLinearGradient PrependAbsoluteStop(double offset, Color color, double opacity)
        {
            Contents.Prepend(
                SvgElementGradientStop
                    .Create()
                    .SetAbsoluteStop(offset, color, opacity)
            );

            return this;
        }

        public SvgElementLinearGradient PrependRelativeStop(double offset, Color color)
        {
            Contents.Prepend(
                SvgElementGradientStop
                    .Create()
                    .SetRelativeStop(offset, color)
            );

            return this;
        }

        public SvgElementLinearGradient PrependRelativeStop(double offset, Color color, double opacity)
        {
            Contents.Prepend(
                SvgElementGradientStop
                    .Create()
                    .SetRelativeStop(offset, color, opacity)
            );

            return this;
        }

        public SvgElementLinearGradient InsertAbsoluteStop(int index, double offset, Color color)
        {
            Contents.Insert(
                index,
                SvgElementGradientStop
                    .Create()
                    .SetAbsoluteStop(offset, color)
            );

            return this;
        }

        public SvgElementLinearGradient InsertAbsoluteStop(int index, double offset, Color color, double opacity)
        {
            Contents.Insert(
                index,
                SvgElementGradientStop
                    .Create()
                    .SetAbsoluteStop(offset, color, opacity)
            );

            return this;
        }

        public SvgElementLinearGradient InsertRelativeStop(int index, double offset, Color color)
        {
            Contents.Insert(
                index,
                SvgElementGradientStop
                    .Create()
                    .SetRelativeStop(offset, color)
            );

            return this;
        }

        public SvgElementLinearGradient InsertRelativeStop(int index, double offset, Color color, double opacity)
        {
            Contents.Insert(
                index,
                SvgElementGradientStop
                    .Create()
                    .SetRelativeStop(offset, color, opacity)
            );

            return this;
        }
    }
}