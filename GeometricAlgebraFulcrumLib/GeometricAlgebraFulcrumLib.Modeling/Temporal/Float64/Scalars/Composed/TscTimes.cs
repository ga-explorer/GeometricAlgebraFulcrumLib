using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscTimes :
    TemporalFloat64Scalar,
    IReadOnlyList<TemporalFloat64Scalar>
{
    public static TscTimes Create(TemporalFloat64Scalar scalar1, TemporalFloat64Scalar scalar2)
    {
        return new TscTimes(new[] { scalar1, scalar2 });
    }

    public static TscTimes Create(TemporalFloat64Scalar scalar1, TemporalFloat64Scalar scalar2, params TemporalFloat64Scalar[] scalarList)
    {
        var scalars = new List<TemporalFloat64Scalar>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return new TscTimes(scalars);
    }

    public static TscTimes Create(IReadOnlyCollection<TemporalFloat64Scalar> scalarList)
    {
        return new TscTimes(scalarList);
    }


    private readonly List<TemporalFloat64Scalar> _baseScalars
        = new List<TemporalFloat64Scalar>();


    public int Count
        => _baseScalars.Count;

    public TemporalFloat64Scalar this[int index]
        => _baseScalars[index];

    public override Float64ScalarRange TimeRange { get; }


    private TscTimes(IReadOnlyCollection<TemporalFloat64Scalar> scalarList)
    {
        if (scalarList.Count < 2)
            throw new InvalidOperationException();

        Add(scalarList);

        TimeRange = Float64ScalarRange.Create(
            _baseScalars.Select(s => s.MinTime).Min(), 
            _baseScalars.Select(s => s.MaxTime).Max()
        );

        Debug.Assert(IsValid());
    }


    private void Add(TemporalFloat64Scalar scalar)
    {
        if (scalar is not TscTimes scalarList)
        {
            _baseScalars.Add(scalar);

            return;
        }

        foreach (var s in scalarList)
            Add(s);
    }

    private void Add(IEnumerable<TemporalFloat64Scalar> scalarList)
    {
        foreach (var scalar in scalarList)
            Add(scalar);
    }


    public override bool IsValid()
    {
        return _baseScalars.Count > 0 &&
               _baseScalars.All(s => s.IsValid());
    }

    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return _baseScalars.Select(
            s => s.GetValue(t)
        ).Aggregate(1d, (product, value) => product * value);
    }


    public IEnumerator<TemporalFloat64Scalar> GetEnumerator()
    {
        return _baseScalars.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}