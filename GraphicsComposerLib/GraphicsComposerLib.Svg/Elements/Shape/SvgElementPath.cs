using System.Collections.Generic;
using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;
using GraphicsComposerLib.Svg.Paths;
using GraphicsComposerLib.Svg.Transforms;

namespace GraphicsComposerLib.Svg.Elements.Shape
{
    /// <summary>
    /// https://www.w3.org/TR/SVG/paths.html#PathElement
    /// </summary>
    public sealed class SvgElementPath : SvgElement, ISvgShapeElement
    {
        public static SvgElementPath Create()
        {
            return new SvgElementPath();
        }

        public static SvgElementPath Create(string id)
        {
            return new SvgElementPath() { Id = id };
        }


        public override string ElementName => "path";


        //public SvgEavString<SvgElementPath> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementPath>;

        //        var attrValue1 = new SvgEavString<SvgElementPath>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementPath> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementPath>;

                var attrValue1 = new SvgEavString<SvgElementPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPath> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementPath>;

                var attrValue1 = new SvgEavString<SvgElementPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementPath> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementPath>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPath> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementPath>;

                var attrValue1 = new SvgEavString<SvgElementPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementPath> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementPath>;

        //        var attrValue1 = new SvgEavStyle<SvgElementPath>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementPath> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementPath>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementPath> PathLength
        {
            get
            {
                var attrInfo = SvgAttributeUtils.PathLength;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementPath>;

                var attrValue1 = new SvgEavLength<SvgElementPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgPathCommand, SvgElementPath> PathData
        {
            get
            {
                var attrInfo = SvgAttributeUtils.PathData;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgPathCommand, SvgElementPath>;

                var attrValue1 = new SvgEav<SvgPathCommand, SvgElementPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementPath()
        {
        }
        

        public SvgElementPath SetPathData(SvgPathCommand value)
        {
            PathData.SetTo(value);

            return this;
        }

        public SvgElementPath SetPathData(IEnumerable<SvgPathCommand> commands)
        {
            PathData.SetTo(SvgPathCommandComposer.Create(commands));

            return this;
        }

        public SvgElementPath SetPathData(params SvgPathCommand[] commands)
        {
            PathData.SetTo(SvgPathCommandComposer.Create(commands));

            return this;
        }
    }
}
