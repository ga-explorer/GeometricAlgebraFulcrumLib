using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;

public sealed class SvgEavViewBox<TParentElement> 
    : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
{
    private double _minX;
    public double MinX
    {
        get => _minX;
        set
        {
            _minX = value;
            IsValueComputed = true;
        }
    }

    private double _minY;
    public double MinY
    {
        get => _minY;
        set
        {
            _minY = value;
            IsValueComputed = true;
        }
    }

    private double _width;
    public double Width
    {
        get => _width;
        set
        {
            _width = value;
            IsValueComputed = true;
        }
    }

    private double _height;
    public double Height
    {
        get => _height;
        set
        {
            _height = value;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText 
        => new StringBuilder()
            .Append(MinX.ToSvgNumberText())
            .Append(", ")
            .Append(MinY.ToSvgNumberText())
            .Append(", ")
            .Append(Width.ToSvgNumberText())
            .Append(", ")
            .Append(Height.ToSvgNumberText())
            .ToString();


    internal SvgEavViewBox(TParentElement parentElement)
        : base(parentElement, SvgAttributeUtils.ViewBox)
    {
    }


    public override ISvgAttributeValue CreateCopy()
    {
        throw new NotImplementedException();
    }

    public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
    {
        throw new NotImplementedException();
    }

    public TParentElement SetMinCorner(double minX, double minY)
    {
        MinX = minX;
        MinY = minY;

        return ParentElement;
    }

    public TParentElement SetSize(double width, double height)
    {
        Width = width;
        Height = height;

        return ParentElement;
    }

    public TParentElement SetTo(double minX, double minY, double width, double height)
    {
        MinX = minX;
        MinY = minY;
        Width = width;
        Height = height;

        return ParentElement;
    }
}