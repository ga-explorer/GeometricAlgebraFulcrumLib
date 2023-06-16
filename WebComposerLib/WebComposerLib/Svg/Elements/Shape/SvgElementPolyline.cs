using DataStructuresLib.Basic;
using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Elements.Shape
{
    public sealed class SvgElementPolyline : SvgElement, ISvgBasicShapeElement
    {
        public static SvgElementPolyline Create()
        {
            return new SvgElementPolyline();
        }

        public static SvgElementPolyline Create(string id)
        {
            return new SvgElementPolyline() { Id = id };
        }


        public override string ElementName => "polyline";


        //public SvgEavString<SvgElementPolyline> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementPolyline>;

        //        var attrValue1 = new SvgEavString<SvgElementPolyline>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementPolyline> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementPolyline>;

                var attrValue1 = new SvgEavString<SvgElementPolyline>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPolyline> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementPolyline>;

                var attrValue1 = new SvgEavString<SvgElementPolyline>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementPolyline> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementPolyline>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementPolyline>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPolyline> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementPolyline>;

                var attrValue1 = new SvgEavString<SvgElementPolyline>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementPolyline> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementPolyline>;

        //        var attrValue1 = new SvgEavStyle<SvgElementPolyline>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementPolyline> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementPolyline>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementPolyline>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValuePointsList, SvgElementPolyline> Points
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Points;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValuePointsList, SvgElementPolyline>;

                var attrValue1 = new SvgEav<SvgValuePointsList, SvgElementPolyline>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementPolyline()
        {
        }


        public SvgElementPolyline SetPoints(SvgValuePointsList points)
        {
            Points.SetTo(points);

            return this;
        }

        public SvgElementPolyline SetPoints(IEnumerable<IPair<double>> points)
        {
            return SetPoints(SvgValuePointsList.Create(points));
        }

        public SvgElementPolyline SetPoints(SvgValueLengthUnit unit, IEnumerable<IPair<double>> points)
        {
            return SetPoints(SvgValuePointsList.Create(unit, points));
        }

        public SvgElementPolyline SetPoints(params double[] points)
        {
            return SetPoints(SvgValuePointsList.Create(points));
        }

        public SvgElementPolyline SetPoints(SvgValueLengthUnit unit, params double[] points)
        {
            return SetPoints(SvgValuePointsList.Create(unit, points));
        }
    }
}