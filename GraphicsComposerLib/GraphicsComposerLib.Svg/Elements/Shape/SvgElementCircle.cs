using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;
using GraphicsComposerLib.Svg.Transforms;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Elements.Shape
{
    public sealed class SvgElementCircle : SvgElement, ISvgBasicShapeElement
    {
        public static SvgElementCircle Create()
        {
            return new SvgElementCircle();
        }

        public static SvgElementCircle Create(string id)
        {
            return new SvgElementCircle() {Id = id};
        }


        public override string ElementName => "circle";


        //public SvgEavString<SvgElementCircle> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementCircle>;

        //        var attrValue1 = new SvgEavString<SvgElementCircle>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementCircle> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementCircle>;

                var attrValue1 = new SvgEavString<SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementCircle> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementCircle>;

                var attrValue1 = new SvgEavString<SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementCircle> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementCircle>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementCircle> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementCircle>;

                var attrValue1 = new SvgEavString<SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementCircle> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementCircle>;

        //        var attrValue1 = new SvgEavStyle<SvgElementCircle>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementCircle> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementCircle>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementCircle> CenterX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.CenterX;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementCircle>;

                var attrValue1 = new SvgEavLength<SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementCircle> CenterY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.CenterY;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementCircle>;

                var attrValue1 = new SvgEavLength<SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementCircle> Radius
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Radius;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementCircle>;

                var attrValue1 = new SvgEavLength<SvgElementCircle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementCircle()
        {
        }


        public SvgElementCircle SetCenter(double centerX, double centerY)
        {
            CenterX.Length = centerX;
            CenterY.Length = centerY;

            return this;
        }

        public SvgElementCircle SetCenter(double centerX, double centerY, SvgValueLengthUnit unit)
        {
            CenterX.SetTo(centerX, unit);
            CenterY.SetTo(centerY, unit);

            return this;
        }

        public SvgElementCircle SetCircle(double centerX, double centerY, double radius)
        {
            CenterX.Length = centerX;
            CenterY.Length = centerY;
            Radius.Length = radius;

            return this;
        }

        public SvgElementCircle SetCircle(double centerX, double centerY, double radius, SvgValueLengthUnit unit)
        {
            CenterX.SetTo(centerX, unit);
            CenterY.SetTo(centerY, unit);
            Radius.SetTo(radius, unit);

            return this;
        }
    }
}
