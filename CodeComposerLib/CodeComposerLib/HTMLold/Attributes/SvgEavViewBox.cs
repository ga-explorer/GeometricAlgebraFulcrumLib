using System.Text;
using CodeComposerLib.HTMLold.Elements;

namespace CodeComposerLib.HTMLold.Attributes;

public sealed class HtmlEavViewBox<TParentElement> 
    : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
{
    private double _minX;
    public double MinX
    {
        get { return _minX; }
        set
        {
            _minX = value;
            IsValueComputed = true;
        }
    }

    private double _minY;
    public double MinY
    {
        get { return _minY; }
        set
        {
            _minY = value;
            IsValueComputed = true;
        }
    }

    private double _width;
    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            IsValueComputed = true;
        }
    }

    private double _height;
    public double Height
    {
        get { return _height; }
        set
        {
            _height = value;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText 
        => new StringBuilder()
            .Append(MinX.ToHtmlNumberText())
            .Append(", ")
            .Append(MinY.ToHtmlNumberText())
            .Append(", ")
            .Append(Width.ToHtmlNumberText())
            .Append(", ")
            .Append(Height.ToHtmlNumberText())
            .ToString();


    internal HtmlEavViewBox(TParentElement parentElement)
        : base(parentElement, HtmlAttributeUtils.ViewBox)
    {
    }


    public override IHtmlAttributeValue CreateCopy()
    {
        throw new System.NotImplementedException();
    }

    public override IHtmlAttributeValue UpdateFrom(IHtmlAttributeValue sourceAttributeValue)
    {
        throw new System.NotImplementedException();
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