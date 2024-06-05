using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.Properties;

public sealed class SvgSpvMarker : SvgStylePropertyValue
{
    private string _markerElementId;
    public string MarkerElementId
    {
        get => _markerElementId;
        set
        {
            _markerElementId = value ?? string.Empty;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText
        => string.IsNullOrEmpty(MarkerElementId)
            ? string.Empty
            : new StringBuilder()
                .Append("url(#")
                .Append(MarkerElementId)
                .Append(")")
                .ToString();


    internal SvgSpvMarker(SvgStyle parentStyle, SvgAttributeInfo attributeInfo)
        : base(parentStyle, attributeInfo)
    {
    }


    public override SvgStylePropertyValue CreateCopy()
    {
        var result = new SvgSpvMarker(ParentStyle, AttributeInfo);

        if (IsValueStored)
        {
            result._markerElementId = _markerElementId;
            result.ValueStoredText = ValueStoredText;

            return result;
        }

        result.MarkerElementId = MarkerElementId;

        return result;
    }

    public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
    {
        var source = sourcePropertyValue as SvgSpvMarker;

        if (ReferenceEquals(source, null) || source.IsValueStored)
        {
            ValueStoredText = source?.ValueStoredText;

            return this;
        }

        MarkerElementId = source.MarkerElementId;

        return this;
    }

    public SvgStyle SetToNone()
    {
        ValueStoredText = "none";

        return ParentStyle;
    }

    public SvgStyle SetToElement(string markerElementId)
    {
        MarkerElementId = markerElementId;

        return ParentStyle;
    }
}