using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements.Categories;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements.Containers;

/// <summary>
/// The svg element can be used to embed an SVG fragment inside the current document
/// (for example, an HTML document). This SVG fragment has its own viewport and coordinate system.
/// http://docs.w3cub.com/svg/element/svg/
/// </summary>
public sealed class SvgElementSvg : SvgElement, ISvgContainerElement
{
    public static SvgElementSvg Create()
    {
        return new SvgElementSvg(false);
    }

    public static SvgElementSvg Create(string id)
    {
        return new SvgElementSvg(false) { Id = id };
    }

    public static SvgElementSvg CreateRoot()
    {
        return new SvgElementSvg(true)
            .Version.SetToText("1.1")
            .Xmlns.SetToText("http://www.w3.org/2000/svg")
            .XmlnsXLink.SetToText("http://www.w3.org/1999/xlink");
    }

    public static SvgElementSvg CreateRoot(string id)
    {
        return new SvgElementSvg(true) { Id = id }
            .Version.SetToText("1.1")
            .Xmlns.SetToText("http://www.w3.org/2000/svg")
            .XmlnsXLink.SetToText("http://www.w3.org/1999/xlink");
    }


    public override string ElementName => "svg";

    public bool IsRootSvg { get; }


    //public SvgEavString<SvgElementSvg> Id
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Id;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavString<SvgElementSvg>;

    //        var attrValue1 = new SvgEavString<SvgElementSvg>(this, attrInfo);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEavString<SvgElementSvg> XmlBase
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSvg>;

            var attrValue1 = new SvgEavString<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementSvg> XmlLanguage
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSvg>;

            var attrValue1 = new SvgEavString<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementSvg> Xmlns
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Xmlns;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSvg>;

            var attrValue1 = new SvgEavString<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementSvg> XmlnsXLink
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlnsXLink;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSvg>;

            var attrValue1 = new SvgEavString<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavStruct<bool, SvgElementSvg> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavStruct<bool, SvgElementSvg>;

            var attrValue1 = new SvgEavStruct<bool, SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementSvg> Version
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Version;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSvg>;

            var attrValue1 = new SvgEavString<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementSvg> Class
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Class;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSvg>;

            var attrValue1 = new SvgEavString<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    //public SvgEavStyle<SvgElementSvg> Style
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Style;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavStyle<SvgElementSvg>;

    //        var attrValue1 = new SvgEavStyle<SvgElementSvg>(this);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEavLength<SvgElementSvg> X
    {
        get
        {
            var attrInfo = SvgAttributeUtils.X;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementSvg>;

            var attrValue1 = new SvgEavLength<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementSvg> Y
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Y;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementSvg>;

            var attrValue1 = new SvgEavLength<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementSvg> Width
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Width;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementSvg>;

            var attrValue1 = new SvgEavLength<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementSvg> Height
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Height;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementSvg>;

            var attrValue1 = new SvgEavLength<SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavViewBox<SvgElementSvg> ViewBox
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ViewBox;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavViewBox<SvgElementSvg>;

            var attrValue1 = new SvgEavViewBox<SvgElementSvg>(this);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavPreserveAspectRatio<SvgElementSvg> PreserveAspectRatio
    {
        get
        {
            var attrInfo = SvgAttributeUtils.PreserveAspectRatio;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavPreserveAspectRatio<SvgElementSvg>;

            var attrValue1 = new SvgEavPreserveAspectRatio<SvgElementSvg>(this);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEav<SvgValueZoomAndPan, SvgElementSvg> ZoomAndPan
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ZoomAndPan;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgValueZoomAndPan, SvgElementSvg>;

            var attrValue1 = new SvgEav<SvgValueZoomAndPan, SvgElementSvg>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }


    private SvgElementSvg(bool isRootSvg)
    {
        IsRootSvg = isRootSvg;
    }


    public SvgElementSvg SetCanvasCorner(double x, double y, SvgLengthUnit unit)
    {
        X.SetTo(x, unit);
        Y.SetTo(y, unit);

        return this;
    }

    public SvgElementSvg SetCanvasCorner(double x, double y)
    {
        X.Length = x;
        Y.Length = y;

        return this;
    }

    public SvgElementSvg SetCanvasSize(double width, double height)
    {
        Width.Length = width;
        Height.Length = height;

        return this;
    }

    public SvgElementSvg SetCanvasSize(double width, double height, SvgLengthUnit unit)
    {
        Width.SetTo(width, unit);
        Height.SetTo(height, unit);

        return this;
    }

    public SvgElementSvg SetCanvas(double x, double y, double width, double height)
    {
        X.Length = x;
        Y.Length = y;
        Width.Length = width;
        Height.Length = height;

        return this;
    }

    public SvgElementSvg SetCanvas(double x, double y, double width, double height, SvgLengthUnit unit)
    {
        X.SetTo(x, unit);
        Y.SetTo(y, unit);
        Width.SetTo(width, unit);
        Height.SetTo(height, unit);

        return this;
    }

}