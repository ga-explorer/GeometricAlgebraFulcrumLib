namespace GeometricAlgebraFulcrumLib.Core.Modeling.Statistics.Discrete;

public class PmfRandomGenerator :
    Random
{
    public static PmfRandomGenerator Create(Random uniformRandomGenerator, IReadOnlyList<double> inverseCdfArray)
    {
        return new PmfRandomGenerator(
            uniformRandomGenerator,
            inverseCdfArray
        );
    }


    public Random UniformRandomGenerator { get; }

    public IReadOnlyList<double> InverseCdfArray { get; }


    private PmfRandomGenerator(Random uniformRandomGenerator, IReadOnlyList<double> inverseCdfArray)
    {
        UniformRandomGenerator = uniformRandomGenerator;
        InverseCdfArray = inverseCdfArray;
    }


    public override double NextDouble()
    {
        var r = UniformRandomGenerator.NextDouble();

        if (r <= 0d) return InverseCdfArray[0];
        if (r >= 1d) return InverseCdfArray[^1];

        var i = r * (InverseCdfArray.Count - 1);

        var i1 = Math.Truncate(i);
        var i2 = i1 + 1;

        var v1 = InverseCdfArray[(int)i1];
        var v2 = InverseCdfArray[(int)i2];

        var t = i - i1;

        return (1d - t) * v1 + t * v2;
    }
}