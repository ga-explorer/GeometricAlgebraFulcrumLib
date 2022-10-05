using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Elements.Categories;
using GraphicsComposerLib.Rendering.Svg.Transforms;
using GraphicsComposerLib.Rendering.Svg.Values;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Svg.Elements.Shape
{
    public sealed class SvgElementPolygon : SvgElement, ISvgBasicShapeElement
    {
        public static SvgElementPolygon Create()
        {
            return new SvgElementPolygon();
        }

        public static SvgElementPolygon Create(string id)
        {
            return new SvgElementPolygon() { Id = id };
        }


        public override string ElementName => "polygon";


        //public SvgEavString<SvgElementPolygon> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementPolygon>;

        //        var attrValue1 = new SvgEavString<SvgElementPolygon>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementPolygon> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementPolygon>;

                var attrValue1 = new SvgEavString<SvgElementPolygon>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPolygon> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementPolygon>;

                var attrValue1 = new SvgEavString<SvgElementPolygon>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementPolygon> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementPolygon>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementPolygon>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPolygon> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementPolygon>;

                var attrValue1 = new SvgEavString<SvgElementPolygon>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementPolygon> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementPolygon>;

        //        var attrValue1 = new SvgEavStyle<SvgElementPolygon>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementPolygon> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementPolygon>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementPolygon>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValuePointsList, SvgElementPolygon> Points
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Points;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValuePointsList, SvgElementPolygon>;

                var attrValue1 = new SvgEav<SvgValuePointsList, SvgElementPolygon>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementPolygon()
        {
        }


        public SvgElementPolygon SetPoints(SvgValuePointsList points)
        {
            Points.SetTo(points);

            return this;
        }

        public SvgElementPolygon SetPoints(IEnumerable<ITuple2D> points)
        {
            return SetPoints(SvgValuePointsList.Create(points));
        }

        public SvgElementPolygon SetPoints(SvgValueLengthUnit unit, IEnumerable<ITuple2D> points)
        {
            return SetPoints(SvgValuePointsList.Create(unit, points));
        }

        public SvgElementPolygon SetPoints(params double[] points)
        {
            return SetPoints(SvgValuePointsList.Create(points));
        }

        public SvgElementPolygon SetPoints(SvgValueLengthUnit unit, params double[] points)
        {
            return SetPoints(SvgValuePointsList.Create(unit, points));
        }
    }
}