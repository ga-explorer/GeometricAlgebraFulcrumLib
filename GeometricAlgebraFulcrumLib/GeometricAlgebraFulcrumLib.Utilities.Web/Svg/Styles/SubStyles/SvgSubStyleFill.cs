using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.Properties;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.SubStyles;

public sealed class SvgSubStyleFill : SvgSubStyle
{
    private static readonly SvgAttributeInfo[] UsedPropertyInfos =
    {
        SvgAttributeUtils.Fill,
        SvgAttributeUtils.FillOpacity,
        SvgAttributeUtils.FillRule
    };


    public static SvgSubStyleFill Create()
    {
        return new SvgSubStyleFill();
    }

    public static SvgSubStyleFill Create(SvgStyle baseStyle)
    {
        return new SvgSubStyleFill(baseStyle);
    }


    public override IEnumerable<SvgAttributeInfo> PropertyInfos
        => UsedPropertyInfos;

    public SvgSpvPaint Paint => BaseStyle.Fill;

    public SvgSpvNumber Opacity => BaseStyle.FillOpacity;

    public SvgSpv<SvgValueFillRule> Rule => BaseStyle.FillRule;


    private SvgSubStyleFill() : base(null)
    {
    }

    private SvgSubStyleFill(SvgStyle baseStyle) : base(baseStyle)
    {
    }


    public override void UpdateTargetStyle(SvgStyle targetStyle)
    {
        targetStyle.Fill.UpdateFrom(Paint);
        targetStyle.FillOpacity.UpdateFrom(Opacity);
        targetStyle.FillRule.UpdateFrom(Rule);
    }

    public void ClearPaint()
    {
        BaseStyle.ClearProperty(SvgAttributeUtils.Fill);
    }

    public void ClearOpacity()
    {
        BaseStyle.ClearProperty(SvgAttributeUtils.FillOpacity);
    }

    public void ClearRule()
    {
        BaseStyle.ClearProperty(SvgAttributeUtils.FillRule);
    }
}