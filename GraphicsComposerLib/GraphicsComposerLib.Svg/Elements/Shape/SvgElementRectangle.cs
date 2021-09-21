using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;
using GraphicsComposerLib.Svg.Transforms;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Elements.Shape
{
    public sealed class SvgElementRectangle : SvgElement, ISvgBasicShapeElement
    {
        public static SvgElementRectangle Create()
        {
            return new SvgElementRectangle();
        }

        public static SvgElementRectangle Create(string id)
        {
            return new SvgElementRectangle() {Id = id};
        }


        public override string ElementName => "rect";


        //public SvgEavString<SvgElementRectangle> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementRectangle>;

        //        var attrValue1 = new SvgEavString<SvgElementRectangle>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementRectangle> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementRectangle>;

                var attrValue1 = new SvgEavString<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementRectangle> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementRectangle>;

                var attrValue1 = new SvgEavString<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementRectangle> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementRectangle>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementRectangle> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementRectangle>;

                var attrValue1 = new SvgEavString<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementRectangle> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementRectangle>;

        //        var attrValue1 = new SvgEavStyle<SvgElementRectangle>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementRectangle> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementRectangle>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementRectangle> MinX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.X;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementRectangle>;

                var attrValue1 = new SvgEavLength<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementRectangle> MinY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Y;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementRectangle>;

                var attrValue1 = new SvgEavLength<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementRectangle> Width
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Width;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementRectangle>;

                var attrValue1 = new SvgEavLength<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementRectangle> Height
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Height;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementRectangle>;

                var attrValue1 = new SvgEavLength<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementRectangle> RadiusX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RadiusX;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementRectangle>;

                var attrValue1 = new SvgEavLength<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementRectangle> RadiusY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RadiusY;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementRectangle>;

                var attrValue1 = new SvgEavLength<SvgElementRectangle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementRectangle()
        {
        }


        public SvgElementRectangle SetSize(double width, double height)
        {
            Width.Length = width;
            Height.Length = height;

            return this;
        }

        public SvgElementRectangle SetSize(double width, double height, SvgValueLengthUnit unit)
        {
            Width.SetTo(width, unit);
            Height.SetTo(height, unit);

            return this;
        }

        public SvgElementRectangle SetRectangle(double x, double y, double width, double height)
        {
            MinX.Length = x;
            MinY.Length = y;
            Width.Length = width;
            Height.Length = height;

            return this;
        }

        public SvgElementRectangle SetRectangle(double x, double y, double width, double height, SvgValueLengthUnit unit)
        {
            MinX.SetTo(x, unit);
            MinY.SetTo(y, unit);
            Width.SetTo(width, unit);
            Height.SetTo(height, unit);

            return this;
        }

        public SvgElementRectangle SetRadii(double radiusX, double radiusY)
        {
            RadiusX.Length = radiusX;
            RadiusY.Length = radiusY;

            return this;
        }

        public SvgElementRectangle SetRadii(double radiusX, double radiusY, SvgValueLengthUnit unit)
        {
            RadiusX.SetTo(radiusX, unit);
            RadiusY.SetTo(radiusY, unit);

            return this;
        }
    }
}
