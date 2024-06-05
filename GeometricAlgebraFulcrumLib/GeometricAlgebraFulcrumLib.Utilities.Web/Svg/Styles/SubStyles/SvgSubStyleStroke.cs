using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.Properties;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.SubStyles;

public sealed class SvgSubStyleStroke : SvgSubStyle
{
    private static readonly SvgAttributeInfo[] UsedPropertyInfos =
    {
        SvgAttributeUtils.Stroke,
        SvgAttributeUtils.StrokeOpacity,
        SvgAttributeUtils.StrokeWidth,
        SvgAttributeUtils.StrokeLineCap,
        SvgAttributeUtils.StrokeLineJoin,
        SvgAttributeUtils.StrokeMiterLimit,
        SvgAttributeUtils.StrokeDashArray,
        SvgAttributeUtils.StrokeDashOffset
    };


    public static SvgSubStyleStroke Create()
    {
        return new SvgSubStyleStroke();
    }

    public static SvgSubStyleStroke Create(SvgStyle baseStyle)
    {
        return new SvgSubStyleStroke(baseStyle);
    }


    public override IEnumerable<SvgAttributeInfo> PropertyInfos
        => UsedPropertyInfos;

    public SvgSpvPaint Paint => BaseStyle.Stroke;

    public SvgSpvNumber Opacity => BaseStyle.StrokeOpacity;

    public SvgSpvLength Width => BaseStyle.StrokeWidth;

    public SvgSpv<SvgValueStrokeLineCap> LineCap => BaseStyle.StrokeLineCap;

    public SvgSpv<SvgValueStrokeLineJoin> LineJoin => BaseStyle.StrokeLineJoin;

    public SvgSpvNumber MiterLimit => BaseStyle.StrokeMiterLimit;

    public SvgSpvString DashArray => BaseStyle.StrokeDashArray;

    public SvgSpvLength DashOffset => BaseStyle.StrokeDashOffset;


    private SvgSubStyleStroke() : base(null)
    {
    }

    private SvgSubStyleStroke(SvgStyle baseStyle) : base(baseStyle)
    {
    }


    public override void UpdateTargetStyle(SvgStyle targetStyle)
    {
        targetStyle.Stroke.UpdateFrom(Paint);
        targetStyle.StrokeOpacity.UpdateFrom(Opacity);
        targetStyle.StrokeWidth.UpdateFrom(Width);
        targetStyle.StrokeLineCap.UpdateFrom(LineCap);
        targetStyle.StrokeLineJoin.UpdateFrom(LineJoin);
        targetStyle.StrokeMiterLimit.UpdateFrom(MiterLimit);
        targetStyle.StrokeDashArray.UpdateFrom(DashArray);
        targetStyle.StrokeDashOffset.UpdateFrom(DashOffset);
    }

    public void ClearPaint()
    {
        BaseStyle.ClearProperty(SvgAttributeUtils.Stroke);
    }
        
    //TODO: Complete the clear methods for remaining properties
}