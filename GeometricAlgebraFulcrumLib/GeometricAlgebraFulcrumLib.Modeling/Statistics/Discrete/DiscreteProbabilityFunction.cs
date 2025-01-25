using System.Collections;
using System.Globalization;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Statistics.Discrete;

public abstract class DiscreteProbabilityFunction :
    IReadOnlyList<double>
{
    protected SortedDictionary<int, double> SampleProbabilityDictionary { get; }


    public double DomainFirstValue { get; }

    public double DomainLastValue { get; }

    public int DomainSampleCount { get; }

    public double DomainResolution
        => (DomainLastValue - DomainFirstValue) / (DomainSampleCount - 1);

    public double DomainSize
        => (DomainLastValue - DomainFirstValue).Abs();

    public Int64Range1D DomainSampleRange
        => Int64Range1D.Create(
            SampleProbabilityDictionary.Keys.First(),
            SampleProbabilityDictionary.Keys.Last()
        );

    public Float64ScalarRange DomainRange
        => Float64ScalarRange.Create(
            DomainFirstValue,
            DomainLastValue
        );

    public double DomainMinValue
        => Math.Min(DomainFirstValue, DomainLastValue);

    public double DomainMaxValue
        => Math.Max(DomainFirstValue, DomainLastValue);

    public IEnumerable<Pair<double>> ValueProbabilityPairs
        => SampleProbabilityDictionary.Select(p =>
            new Pair<double>(
                GetDomainValue(p.Key),
                p.Value
            )
        );

    public int Count
        => DomainSampleCount;

    public double this[int index]
        => SampleProbabilityDictionary.GetValueOrDefault(index, 0d);


    protected DiscreteProbabilityFunction(double domainFirstValue, double domainLastValue, SortedDictionary<int, double> sampleProbabilityDictionary)
    {
        if (domainFirstValue.IsNaNOrInfinite() || domainLastValue.IsNaNOrInfinite() || sampleProbabilityDictionary.Count == 0)
            throw new InvalidOperationException();

        if ((domainFirstValue - domainLastValue).Abs() <= 1e-12)
            throw new InvalidOperationException();

        DomainFirstValue = domainFirstValue;
        DomainLastValue = domainLastValue;
        DomainSampleCount = sampleProbabilityDictionary.Keys.Last() + 1;

        SampleProbabilityDictionary = sampleProbabilityDictionary;
    }


    public abstract bool IsValid();

    public double GetDomainValue(int sampleIndex)
    {
        var t = sampleIndex / (double)(DomainSampleCount - 1);

        return (1 - t) * DomainFirstValue + t * DomainLastValue;
    }

    public string GetMatlabCode()
    {
        var composer = new LinearTextComposer();

        var domainValueText =
            SampleProbabilityDictionary.Keys
                .Select(i =>
                    GetDomainValue(i).ToString(CultureInfo.InvariantCulture)
                ).Concatenate(", ");

        var probabilityText =
            SampleProbabilityDictionary.Values
                .Select(p =>
                    p.ToString(CultureInfo.InvariantCulture)
                ).Concatenate(", ");

        //composer.AppendLine($"x = linspace({DomainFirstValue:G}, {DomainLastValue:G}, {DomainSampleCount});");
        composer.AppendLine($"x = [{domainValueText}];");
        composer.AppendLine($"y = [{probabilityText}];");
        composer.AppendLine("stem(x, y);");

        return composer.ToString();
    }

    public override string ToString()
    {
        var composer = new LinearTextComposer();

        var pairs = ValueProbabilityPairs.Select(p =>
            $"P({p.Item1:G}) = {p.Item2:G}"
        ).Concatenate("," + Environment.NewLine);

        composer
            .AppendLine("Probability Mass Function [")
            .IncreaseIndentation()
            .AppendLine(pairs)
            .DecreaseIndentation()
            .AppendLine("]");

        return composer.ToString();
    }

    public IEnumerator<double> GetEnumerator()
    {
        return DomainSampleCount
            .MapRange(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}