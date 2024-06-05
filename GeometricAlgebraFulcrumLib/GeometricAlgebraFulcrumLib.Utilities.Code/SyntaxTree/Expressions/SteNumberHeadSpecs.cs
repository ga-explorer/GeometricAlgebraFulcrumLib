namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;

public record SteNumberHeadSpecs : 
    ISteAtomicHeadSpecs
{
    public string NumberText { get; }

    public bool IsSymbolic { get; }

    public bool IsLiteral => !IsSymbolic;

    public string HeadText => NumberText;


    public SteNumberHeadSpecs(string numberText, bool isSymbolic)
    {
        if (numberText[^1] == '.')
        {
            NumberText = numberText.Substring(0, numberText.Length - 1);
            IsSymbolic = false;
        }
        else
        {
            NumberText = numberText;
            IsSymbolic = isSymbolic;
        }
    }

    public SteNumberHeadSpecs(double number)
    {
        NumberText = number.ToString("G");
        IsSymbolic = false;
    }

    public SteNumberHeadSpecs(float number)
    {
        NumberText = number.ToString("G");
        IsSymbolic = false;
    }

    public SteNumberHeadSpecs(int number)
    {
        NumberText = number.ToString();
        IsSymbolic = false;
    }
        
    public SteNumberHeadSpecs(uint number)
    {
        NumberText = number.ToString();
        IsSymbolic = false;
    }

    public SteNumberHeadSpecs(long number)
    {
        NumberText = number.ToString();
        IsSymbolic = false;
    }
        
    public SteNumberHeadSpecs(ulong number)
    {
        NumberText = number.ToString();
        IsSymbolic = false;
    }


    public override string ToString()
    {
        return NumberText;
    }
}