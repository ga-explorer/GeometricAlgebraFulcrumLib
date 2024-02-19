using System;
using System.Collections;
using System.Collections.Generic;
using CodeComposerLib.MathML.Values.Color;

namespace CodeComposerLib.MathML.Elements.Layout.General;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/mfrac
/// </summary>
public sealed class MathMlFraction
    : MathMlLayoutElement, IMathMlLayoutElement<IMathMlElement>
{
    public static MathMlFraction Create()
    {
        return new MathMlFraction();
    }

    public static MathMlFraction Create(IMathMlElement numeratorElement, IMathMlElement denominatorElement)
    {
        return new MathMlFraction()
        {
            Numerator = numeratorElement,
            Denominator = denominatorElement
        };
    }


    public override string XmlTagName 
        => "mfrac";

    public override IEnumerable<IMathMlElement> Contents
    {
        get
        {
            yield return Numerator;
            yield return Denominator;
        }
    }

    public override string ContentsText
        => Numerator + Environment.NewLine + Denominator;

    public int Count 
        => 2;

    public IMathMlElement this[int index]
    {
        get
        {
            if (index == 0) return Numerator;

            if (index == 1) return Denominator;

            throw new IndexOutOfRangeException();
        }

        set
        {
            if (index == 0)
                Numerator = value;
            else if (index == 1)
                Denominator = value;
            else
                throw new IndexOutOfRangeException();
        }
    }

    public IMathMlElement Numerator { get; set; }

    public IMathMlElement Denominator { get; set; }

    public MathMlColorValue BackgroundColor { get; set; }
        = MathMlColorValue.Empty;

    public MathMlColorValue TextColor { get; set; }
        = MathMlColorValue.Empty;


    internal MathMlFraction()
    {
    }


    public MathMlFraction SetContents(IMathMlElement numeratorElement, IMathMlElement denominatorElement)
    {
        Numerator = numeratorElement;
        Denominator = denominatorElement;

        return this;
    }

    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer
            .SetAttributeValue("mathcolor", TextColor)
            .SetAttributeValue("mathbackground", BackgroundColor);
    }

    public IEnumerator<IMathMlElement> GetEnumerator()
    {
        yield return Numerator;
        yield return Denominator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return Numerator;
        yield return Denominator;
    }
}