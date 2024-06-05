namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Storage;

public sealed class LibTempVariable :
    LibTempStorage
{
    public string VariableName { get; }

    public override string this[int index]
        => index == 0
            ? VariableName
            : throw new IndexOutOfRangeException();


    public LibTempVariable(string variableName)
        : base(1)
    {
        VariableName = variableName;
    }


    public override string GetDeclareCode()
    {
        return $"var {VariableName} = 0d;";
    }
}