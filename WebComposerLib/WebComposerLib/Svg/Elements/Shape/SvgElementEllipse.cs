using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Elements.Shape
{
    public sealed class SvgElementEllipse : SvgElement, ISvgBasicShapeElement
    {
        public static SvgElementEllipse Create()
        {
            return new SvgElementEllipse();
        }

        public static SvgElementEllipse Create(string id)
        {
            return new SvgElementEllipse() { Id = id };
        }


        public override string ElementName => "ellipse";


        //public SvgEavString<SvgElementEllipse> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementEllipse>;

        //        var attrValue1 = new SvgEavString<SvgElementEllipse>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementEllipse> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementEllipse>;

                var attrValue1 = new SvgEavString<SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementEllipse> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementEllipse>;

                var attrValue1 = new SvgEavString<SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementEllipse> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementEllipse>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementEllipse> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementEllipse>;

                var attrValue1 = new SvgEavString<SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementEllipse> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementEllipse>;

        //        var attrValue1 = new SvgEavStyle<SvgElementEllipse>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementEllipse> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementEllipse>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementEllipse> CenterX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.CenterX;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementEllipse>;

                var attrValue1 = new SvgEavLength<SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementEllipse> CenterY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.CenterY;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementEllipse>;

                var attrValue1 = new SvgEavLength<SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementEllipse> RadiusX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RadiusX;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementEllipse>;

                var attrValue1 = new SvgEavLength<SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementEllipse> RadiusY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RadiusY;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementEllipse>;

                var attrValue1 = new SvgEavLength<SvgElementEllipse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementEllipse()
        {
        }


        public SvgElementEllipse SetCenter(double centerX, double centerY)
        {
            CenterX.Length = centerX;
            CenterY.Length = centerY;

            return this;
        }

        public SvgElementEllipse SetCenter(double centerX, double centerY, SvgValueLengthUnit unit)
        {
            CenterX.SetTo(centerX, unit);
            CenterY.SetTo(centerY, unit);

            return this;
        }

        public SvgElementEllipse SetRadii(double radiusX, double radiusY)
        {
            RadiusX.Length = radiusX;
            RadiusY.Length = radiusY;

            return this;
        }

        public SvgElementEllipse SetRadii(double radiusX, double radiusY, SvgValueLengthUnit unit)
        {
            RadiusX.SetTo(radiusX, unit);
            RadiusY.SetTo(radiusY, unit);

            return this;
        }

        public SvgElementEllipse SetEllipse(double centerX, double centerY, double radiusX, double radiusY)
        {
            CenterX.Length = centerX;
            CenterY.Length = centerY;
            RadiusX.Length = radiusX;
            RadiusY.Length = radiusY;

            return this;
        }

        public SvgElementEllipse SetEllipse(double centerX, double centerY, double radiusX, double radiusY, SvgValueLengthUnit unit)
        {
            CenterX.SetTo(centerX, unit);
            CenterY.SetTo(centerY, unit);
            RadiusX.SetTo(radiusX, unit);
            RadiusY.SetTo(radiusY, unit);

            return this;
        }
    }
}