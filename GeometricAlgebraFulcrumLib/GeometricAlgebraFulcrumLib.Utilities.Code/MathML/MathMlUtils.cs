using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout.Elementary;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout.Script;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Tokens;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML;

public static class MathMlUtils
{
    public static MathMlText ToMathMlText(this string text)
    {
        return new MathMlText()
        {
            Text = text
        };
    }

    public static MathMlString ToMathMlString(this string text)
    {
        return new MathMlString()
        {
            Text = text
        };
    }

    public static MathMlIdentifier ToMathMlIdentifier(this string text)
    {
        return new MathMlIdentifier()
        {
            Text = text
        };
    }

    public static MathMlNumber ToMathMlNumber(this string value)
    {
        return new MathMlNumber()
        {
            Text = value
        };
    }

    public static MathMlNumber ToMathMlNumber(this int value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString()
        };
    }

    public static MathMlNumber ToMathMlNumber(this long value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString()
        };
    }

    public static MathMlNumber ToMathMlNumber(this uint value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString()
        };
    }

    public static MathMlNumber ToMathMlNumber(this ulong value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString()
        };
    }

    public static MathMlNumber ToMathMlNumber(this float value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString("G")
        };
    }

    public static MathMlNumber ToMathMlNumber(this double value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString("G")
        };
    }

    public static MathMlOperator ToMathMlOperator(this string text)
    {
        return new MathMlOperator()
        {
            Text = text
        };
    }

    public static MathMlSubscript ToMathMlSubscript(this string baseText, string subscriptText)
    {
        return new MathMlSubscript()
        {
            Base = new MathMlIdentifier() { Text = baseText },
            Subscript = new MathMlIdentifier() { Text = subscriptText }
        };
    }

    public static MathMlSubscript ToMathMlSubscript(this string baseText, int subscriptValue)
    {
        return new MathMlSubscript()
        {
            Base = new MathMlIdentifier() { Text = baseText },
            Subscript = subscriptValue.ToMathMlNumber()
        };
    }

    public static MathMlSubscript AddSubscript(this IMathMlElement baseElement)
    {
        return new MathMlSubscript()
        {
            Base = baseElement
        };
    }

    public static MathMlSubscript AddSubscript(this IMathMlElement baseElement, IMathMlElement subscriptElement)
    {
        return new MathMlSubscript()
        {
            Base = baseElement,
            Subscript = subscriptElement
        };
    }

    public static MathMlRow ToMathMlRow(this IMathMlElement element)
    {
        var rowElement = new MathMlRow();
        rowElement.Append(element);

        return rowElement;

    }

    public static MathMlRow ToMathMlRow(this IEnumerable<IMathMlElement> elementsList)
    {
        var rowElement = new MathMlRow();
        rowElement.AppendElements(elementsList);

        return rowElement;
    }

    public static MathMlMath ToMathMlMath(this IMathMlElement element)
    {
        if (element is MathMlMath mathElement) 
            return mathElement;

        mathElement = new MathMlMath();
        mathElement.Append(element);

        return mathElement;

    }

    public static MathMlMath ToMathMlMath(this IEnumerable<IMathMlElement> elementsList)
    {
        var mathElement = new MathMlMath();
        mathElement.AppendElements(elementsList);

        return mathElement;
    }

    public static string ToMathMlCode(this IMathMlElement element)
    {
        if (element is MathMlMath mathElement) 
            return mathElement.ToString();

        mathElement = new MathMlMath();
        mathElement.Append(element);

        return mathElement.ToString();

    }

    public static string ToMathMlCode(this IEnumerable<IMathMlElement> elementsList)
    {
        var mathElement = new MathMlMath();
        mathElement.AppendElements(elementsList);

        return mathElement.ToString();
    }

}