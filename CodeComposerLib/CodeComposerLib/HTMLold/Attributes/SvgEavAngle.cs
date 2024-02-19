using CodeComposerLib.HTMLold.Elements;
using CodeComposerLib.HTMLold.Values;

namespace CodeComposerLib.HTMLold.Attributes;

/// <summary>
/// http://docs.w3cub.com/svg/content_type/#Angle
/// </summary>
/// <typeparam name="TParentElement"></typeparam>
public sealed class HtmlEavAngle<TParentElement>
    : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
{
    private double _angle;
    public double Angle
    {
        get { return _angle; }
        set
        {
            _angle = value;
            IsValueComputed = true;
        }
    }

    private HtmlValueAngleUnit _unit = HtmlValueAngleUnit.None;
    public HtmlValueAngleUnit Unit
    {
        get { return _unit; }
        set
        {
            _unit = value ?? HtmlValueAngleUnit.None;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText
        => _angle.ToHtmlAngleText(_unit);


    internal HtmlEavAngle(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
        : base(parentElement, attributeInfo)
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

    public TParentElement SetTo(double angle)
    {
        Angle = angle;
        Unit = HtmlValueAngleUnit.None;

        return ParentElement;
    }

    public TParentElement SetTo(double angle, HtmlValueAngleUnit unit)
    {
        Angle = angle;
        Unit = unit;

        return ParentElement;
    }

    public TParentElement SetToAuto()
    {
        ValueStoredText = "auto";

        return ParentElement;
    }

    public TParentElement SetToAutoStartReverse()
    {
        ValueStoredText = "auto-start-reverse";

        return ParentElement;
    }
}