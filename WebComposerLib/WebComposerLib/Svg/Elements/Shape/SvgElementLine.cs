using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Elements.Shape;

public sealed class SvgElementLine : 
    SvgElement, 
    ISvgBasicShapeElement
{
    public static SvgElementLine Create()
    {
        return new SvgElementLine();
    }

    public static SvgElementLine Create(string id)
    {
        return new SvgElementLine() { Id = id };
    }


    public override string ElementName => "line";


    //public SvgEavString<SvgElementLine> Id
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Id;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavString<SvgElementLine>;

    //        var attrValue1 = new SvgEavString<SvgElementLine>(this, attrInfo);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEavString<SvgElementLine> XmlBase
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementLine>;

            var attrValue1 = new SvgEavString<SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementLine> XmlLanguage
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementLine>;

            var attrValue1 = new SvgEavString<SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavStruct<bool, SvgElementLine> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavStruct<bool, SvgElementLine>;

            var attrValue1 = new SvgEavStruct<bool, SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementLine> Class
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Class;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementLine>;

            var attrValue1 = new SvgEavString<SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    //public SvgEavStyle<SvgElementLine> Style
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Style;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavStyle<SvgElementLine>;

    //        var attrValue1 = new SvgEavStyle<SvgElementLine>(this);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEav<SvgTransform, SvgElementLine> Transform
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Transform;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgTransform, SvgElementLine>;

            var attrValue1 = new SvgEav<SvgTransform, SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementLine> StartPointX
    {
        get
        {
            var attrInfo = SvgAttributeUtils.X1;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementLine>;

            var attrValue1 = new SvgEavLength<SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementLine> StartPointY
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Y1;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementLine>;

            var attrValue1 = new SvgEavLength<SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementLine> EndPointX
    {
        get
        {
            var attrInfo = SvgAttributeUtils.X2;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementLine>;

            var attrValue1 = new SvgEavLength<SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementLine> EndPointY
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Y2;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementLine>;

            var attrValue1 = new SvgEavLength<SvgElementLine>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }


    private SvgElementLine()
    {
    }


    public SvgElementLine SetStartPoint(double x1, double y1)
    {
        StartPointX.Length = x1;
        StartPointY.Length = y1;

        return this;
    }

    public SvgElementLine SetStartPoint(double x1, double y1, SvgLengthUnit unit)
    {
        StartPointX.SetTo(x1, unit);
        StartPointY.SetTo(y1, unit);

        return this;
    }

    public SvgElementLine SetEndPoint(double x2, double y2)
    {
        EndPointX.Length = x2;
        EndPointY.Length = y2;

        return this;
    }

    public SvgElementLine SetEndPoint(double x2, double y2, SvgLengthUnit unit)
    {
        EndPointX.SetTo(x2, unit);
        EndPointY.SetTo(y2, unit);

        return this;
    }

    public SvgElementLine SetLine(double x1, double y1, double x2, double y2)
    {
        StartPointX.Length = x1;
        StartPointY.Length = y1;
        EndPointX.Length = x2;
        EndPointY.Length = y2;

        return this;
    }

    public SvgElementLine SetLine(double x1, double y1, double x2, double y2, SvgLengthUnit unit)
    {
        StartPointX.SetTo(x1, unit);
        StartPointY.SetTo(y1, unit);
        EndPointX.SetTo(x2, unit);
        EndPointY.SetTo(y2, unit);

        return this;
    }
}