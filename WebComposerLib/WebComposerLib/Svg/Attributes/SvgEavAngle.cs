﻿using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Elements;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Attributes;

/// <summary>
/// http://docs.w3cub.com/svg/content_type/#Angle
/// </summary>
/// <typeparam name="TParentElement"></typeparam>
public sealed class SvgEavAngle<TParentElement>
    : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
{
    private double _angle;
    public double Angle
    {
        get => _angle;
        set
        {
            _angle = value;
            IsValueComputed = true;
        }
    }

    private SvgValueAngleUnit _unit = SvgValueAngleUnit.None;
    public SvgValueAngleUnit Unit
    {
        get => _unit;
        set
        {
            _unit = value ?? SvgValueAngleUnit.None;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText
        => _angle.ToSvgAngleText(_unit);


    internal SvgEavAngle(TParentElement parentElement, SvgAttributeInfo attributeInfo)
        : base(parentElement, attributeInfo)
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

    public TParentElement SetTo(double angle)
    {
        Angle = angle;
        Unit = SvgValueAngleUnit.None;

        return ParentElement;
    }

    public TParentElement SetTo(double angle, SvgValueAngleUnit unit)
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