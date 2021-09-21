using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;
using GraphicsComposerLib.Svg.Transforms;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Elements.Containers
{
    /// <summary>
    /// The 'marker' element defines the graphics that is to be used for drawing arrowheads
    /// or polymarkers on a given 'path', 'line', 'polyline' or 'polygon' element.
    /// http://docs.w3cub.com/svg/element/marker/
    /// https://www.w3.org/TR/svg-markers/
    /// </summary>
    public sealed class SvgElementMarker : SvgElement, ISvgContainerElement
    {
        public static SvgElementMarker Create()
        {
            return new SvgElementMarker();
        }

        public static SvgElementMarker Create(string id)
        {
            return new SvgElementMarker() { Id = id };
        }


        public override string ElementName => "marker";


        //public SvgEavString<SvgElementMarker> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementMarker>;

        //        var attrValue1 = new SvgEavString<SvgElementMarker>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementMarker> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementMarker>;

                var attrValue1 = new SvgEavString<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementMarker> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementMarker>;

                var attrValue1 = new SvgEavString<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementMarker> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementMarker>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementMarker> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementMarker>;

                var attrValue1 = new SvgEavString<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementMarker> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementMarker>;

        //        var attrValue1 = new SvgEavStyle<SvgElementMarker>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementMarker> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementMarker>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavViewBox<SvgElementMarker> ViewBox
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ViewBox;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavViewBox<SvgElementMarker>;

                var attrValue1 = new SvgEavViewBox<SvgElementMarker>(this);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavPreserveAspectRatio<SvgElementMarker> PreserveAspectRatio
        {
            get
            {
                var attrInfo = SvgAttributeUtils.PreserveAspectRatio;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavPreserveAspectRatio<SvgElementMarker>;

                var attrValue1 = new SvgEavPreserveAspectRatio<SvgElementMarker>(this);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueMarkerUnits, SvgElementMarker> MarkerUnits
        {
            get
            {
                var attrInfo = SvgAttributeUtils.MarkerUnits;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgValueMarkerUnits, SvgElementMarker>;

                var attrValue1 = new SvgEav<SvgValueMarkerUnits, SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMarker> RefX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RefX;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementMarker>;

                var attrValue1 = new SvgEavLength<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMarker> RefY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RefY;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementMarker>;

                var attrValue1 = new SvgEavLength<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMarker> MarkerWidth
        {
            get
            {
                var attrInfo = SvgAttributeUtils.MarkerWidth;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementMarker>;

                var attrValue1 = new SvgEavLength<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMarker> MarkerHeight
        {
            get
            {
                var attrInfo = SvgAttributeUtils.MarkerHeight;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementMarker>;

                var attrValue1 = new SvgEavLength<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAngle, SvgElementMarker> Orient
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Orient;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgValueAngle, SvgElementMarker>;

                var attrValue1 = new SvgEav<SvgValueAngle, SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMarker> Position
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Position;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementMarker>;

                var attrValue1 = new SvgEavLength<SvgElementMarker>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }
        

        private SvgElementMarker()
        {
        }
    }
}