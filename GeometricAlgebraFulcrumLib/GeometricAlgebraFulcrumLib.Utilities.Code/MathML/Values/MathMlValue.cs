namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values;

public abstract class MathMlValue : IMathMlValue
{
    public abstract string ValueText { get; }


    public override string ToString()
    {
        return ValueText;
    }
}