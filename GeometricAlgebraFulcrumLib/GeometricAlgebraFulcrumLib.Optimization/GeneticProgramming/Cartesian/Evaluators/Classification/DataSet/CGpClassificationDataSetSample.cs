using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Classification.DataSet;

public sealed record CGpClassificationDataSetSample
{
    public IReadOnlyList<double> Input { get; }

    public int ClassIndex { get; }

    public int InputSize
        => Input.Count;


    internal CGpClassificationDataSetSample(IReadOnlyList<double> input, int classIndex)
    {
        Input = input;
        ClassIndex = classIndex;
    }

    public void Deconstruct(out IReadOnlyList<double> input, out int classLabel)
    {
        input = Input;
        classLabel = ClassIndex;
    }

    public override string ToString()
    {
        var inputText =
            Input.Select(
                v => v.ToString("G")
            ).Concatenate(", ");

        var outputText =
            ClassIndex.ToString();

        return new StringBuilder()
            .Append('(')
            .Append(inputText)
            .Append(") => (")
            .Append(outputText)
            .Append(')')
            .ToString();
    }
}