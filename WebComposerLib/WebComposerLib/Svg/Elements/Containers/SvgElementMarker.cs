using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Elements.Containers;

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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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