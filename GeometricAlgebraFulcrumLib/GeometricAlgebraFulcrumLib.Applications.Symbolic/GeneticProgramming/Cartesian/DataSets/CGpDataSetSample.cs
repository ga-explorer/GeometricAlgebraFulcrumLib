﻿using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.DataSets;

public sealed record CGpDataSetSample
{
    public IReadOnlyList<double> Input { get; }

    public IReadOnlyList<double> Output { get; }

    public int InputSize
        => Input.Count;

    public int OutputSize
        => Input.Count;


    internal CGpDataSetSample(IReadOnlyList<double> Input, IReadOnlyList<double> Output)
    {
        this.Input = Input;
        this.Output = Output;
    }

    public void Deconstruct(out IReadOnlyList<double> input, out IReadOnlyList<double> output)
    {
        input = Input;
        output = Output;
    }

    public override string ToString()
    {
        var inputText =
            Input.Select(
                v => v.ToString("G")
            ).Concatenate(", ");

        var outputText =
            Output.Select(
                v => v.ToString("G")
            ).Concatenate(", ");

        return new StringBuilder()
            .Append('(')
            .Append(inputText)
            .Append(") => (")
            .Append(outputText)
            .Append(')')
            .ToString();
    }
}