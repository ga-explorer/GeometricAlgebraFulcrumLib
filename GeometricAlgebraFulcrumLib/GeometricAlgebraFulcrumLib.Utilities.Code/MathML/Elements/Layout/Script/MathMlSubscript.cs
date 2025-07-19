using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout.Script;

public sealed class MathMlSubscript
    : MathMlLayoutElement, IMathMlLayoutElement<IMathMlElement>
{
    public static MathMlSubscript Create()
    {
        return new MathMlSubscript();
    }

    public static MathMlSubscript Create(IMathMlElement baseElement, IMathMlElement subscriptElement)
    {
        return new MathMlSubscript()
        {
            Base = baseElement,
            Subscript = subscriptElement
        };
    }


    public override string XmlTagName 
        => "msub";

    public override IEnumerable<IMathMlElement> Contents
    {
        get
        {
            yield return Base;
            yield return Subscript;
        }
    }

    public override string ContentsText
        => Base + Environment.NewLine + Subscript;

    public int Count 
        => 2;

    public IMathMlElement this[int index]
    {
        get
        {
            if (index == 0) return Base;

            if (index == 1) return Subscript;

            throw new IndexOutOfRangeException();
        }

        set
        {
            if (index == 0)
                Base = value;
            else if (index == 1)
                Subscript = value;
            else
                throw new IndexOutOfRangeException();
        }
    }

    public IMathMlElement Base { get; set; }

    public IMathMlElement Subscript { get; set; }

    public MathMlColorValue BackgroundColor { get; set; }
        = MathMlColorValue.Empty;

    public MathMlColorValue TextColor { get; set; }
        = MathMlColorValue.Empty;


    internal MathMlSubscript()
    {
    }


    public MathMlSubscript SetContents(IMathMlElement baseElement, IMathMlElement subscriptElement)
    {
        Base = baseElement;
        Subscript = subscriptElement;

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
        yield return Base;
        yield return Subscript;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return Base;
        yield return Subscript;
    }
}