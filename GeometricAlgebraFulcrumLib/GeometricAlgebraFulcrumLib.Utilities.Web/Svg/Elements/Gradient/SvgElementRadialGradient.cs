using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements.Categories;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Transforms;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements.Gradient;

public sealed class SvgElementRadialGradient : SvgElement, ISvgGradientElement
{
    public static SvgElementRadialGradient Create()
    {
        return new SvgElementRadialGradient();
    }

    public static SvgElementRadialGradient Create(string id)
    {
        return new SvgElementRadialGradient() { Id = id };
    }


    public override string ElementName => "radialGradient";


    public SvgEavString<SvgElementRadialGradient> Class
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Class;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavString<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementRadialGradient> XmlBase
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavString<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementRadialGradient> XmlLanguage
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavString<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavStruct<bool, SvgElementRadialGradient> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavStruct<bool, SvgElementRadialGradient>;

            var attrValue1 = new SvgEavStruct<bool, SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementRadialGradient> FocalCenterX
    {
        get
        {
            var attrInfo = SvgAttributeUtils.FocalCenterX;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavLength<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementRadialGradient> FocalCenterY
    {
        get
        {
            var attrInfo = SvgAttributeUtils.FocalCenterY;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavLength<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementRadialGradient> CenterX
    {
        get
        {
            var attrInfo = SvgAttributeUtils.CenterX;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavLength<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementRadialGradient> CenterY
    {
        get
        {
            var attrInfo = SvgAttributeUtils.CenterY;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavLength<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavLength<SvgElementRadialGradient> Radius
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Radius;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavLength<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavLength<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEav<SvgValueGradientUnits, SvgElementRadialGradient> GradientUnits
    {
        get
        {
            var attrInfo = SvgAttributeUtils.GradientUnits;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgValueGradientUnits, SvgElementRadialGradient>;

            var attrValue1 = new SvgEav<SvgValueGradientUnits, SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEav<SvgTransform, SvgElementRadialGradient> GradientTransform
    {
        get
        {
            var attrInfo = SvgAttributeUtils.GradientTransform;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgTransform, SvgElementRadialGradient>;

            var attrValue1 = new SvgEav<SvgTransform, SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEav<SvgValueGradientSpreadMethod, SvgElementRadialGradient> GradientSpreadMethod
    {
        get
        {
            var attrInfo = SvgAttributeUtils.GradientSpreadMethod;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgValueGradientSpreadMethod, SvgElementRadialGradient>;

            var attrValue1 = new SvgEav<SvgValueGradientSpreadMethod, SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementRadialGradient> HRef
    {
        get
        {
            var attrInfo = SvgAttributeUtils.HRef;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementRadialGradient>;

            var attrValue1 = new SvgEavString<SvgElementRadialGradient>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }


    private SvgElementRadialGradient()
    {
    }


    public SvgElementRadialGradient SetFocalCenter(double centerX, double centerY)
    {
        FocalCenterX.Length = centerX;
        FocalCenterY.Length = centerY;

        return this;
    }

    public SvgElementRadialGradient SetFocalCenter(double centerX, double centerY, SvgLengthUnit unit)
    {
        FocalCenterX.SetTo(centerX, unit);
        FocalCenterY.SetTo(centerY, unit);

        return this;
    }

    public SvgElementRadialGradient SetLargestCircleCenter(double centerX, double centerY)
    {
        CenterX.Length = centerX;
        CenterY.Length = centerY;

        return this;
    }

    public SvgElementRadialGradient SetLargestCircleCenter(double centerX, double centerY, SvgLengthUnit unit)
    {
        CenterX.SetTo(centerX, unit);
        CenterY.SetTo(centerY, unit);

        return this;
    }

    public SvgElementRadialGradient SetLargestCircle(double centerX, double centerY, double radius)
    {
        CenterX.Length = centerX;
        CenterY.Length = centerY;
        Radius.Length = radius;

        return this;
    }

    public SvgElementRadialGradient SetLargestCircle(double centerX, double centerY, double radius, SvgLengthUnit unit)
    {
        CenterX.SetTo(centerX, unit);
        CenterY.SetTo(centerY, unit);
        Radius.SetTo(radius, unit);

        return this;
    }


    public SvgElementRadialGradient AppendAbsoluteStop(double offset, Color color)
    {
        Contents.Append(
            SvgElementGradientStop
                .Create()
                .SetAbsoluteStop(offset, color)
        );

        return this;
    }

    public SvgElementRadialGradient AppendAbsoluteStop(double offset, Color color, double opacity)
    {
        Contents.Append(
            SvgElementGradientStop
                .Create()
                .SetAbsoluteStop(offset, color, opacity)
        );

        return this;
    }

    public SvgElementRadialGradient AppendRelativeStop(double offset, Color color)
    {
        Contents.Append(
            SvgElementGradientStop
                .Create()
                .SetRelativeStop(offset, color)
        );

        return this;
    }

    public SvgElementRadialGradient AppendRelativeStop(double offset, Color color, double opacity)
    {
        Contents.Append(
            SvgElementGradientStop
                .Create()
                .SetRelativeStop(offset, color, opacity)
        );

        return this;
    }

    public SvgElementRadialGradient PrependAbsoluteStop(double offset, Color color)
    {
        Contents.Prepend(
            SvgElementGradientStop
                .Create()
                .SetAbsoluteStop(offset, color)
        );

        return this;
    }

    public SvgElementRadialGradient PrependAbsoluteStop(double offset, Color color, double opacity)
    {
        Contents.Prepend(
            SvgElementGradientStop
                .Create()
                .SetAbsoluteStop(offset, color, opacity)
        );

        return this;
    }

    public SvgElementRadialGradient PrependRelativeStop(double offset, Color color)
    {
        Contents.Prepend(
            SvgElementGradientStop
                .Create()
                .SetRelativeStop(offset, color)
        );

        return this;
    }

    public SvgElementRadialGradient PrependRelativeStop(double offset, Color color, double opacity)
    {
        Contents.Prepend(
            SvgElementGradientStop
                .Create()
                .SetRelativeStop(offset, color, opacity)
        );

        return this;
    }

    public SvgElementRadialGradient InsertAbsoluteStop(int index, double offset, Color color)
    {
        Contents.Insert(
            index,
            SvgElementGradientStop
                .Create()
                .SetAbsoluteStop(offset, color)
        );

        return this;
    }

    public SvgElementRadialGradient InsertAbsoluteStop(int index, double offset, Color color, double opacity)
    {
        Contents.Insert(
            index,
            SvgElementGradientStop
                .Create()
                .SetAbsoluteStop(offset, color, opacity)
        );

        return this;
    }

    public SvgElementRadialGradient InsertRelativeStop(int index, double offset, Color color)
    {
        Contents.Insert(
            index,
            SvgElementGradientStop
                .Create()
                .SetRelativeStop(offset, color)
        );

        return this;
    }

    public SvgElementRadialGradient InsertRelativeStop(int index, double offset, Color color, double opacity)
    {
        Contents.Insert(
            index,
            SvgElementGradientStop
                .Create()
                .SetRelativeStop(offset, color, opacity)
        );

        return this;
    }
}