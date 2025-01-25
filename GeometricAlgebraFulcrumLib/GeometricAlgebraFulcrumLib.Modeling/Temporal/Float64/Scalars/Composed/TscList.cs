using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscList :
    TemporalFloat64Scalar,
    IReadOnlyList<TemporalFloat64Scalar>
{
    public static TscList Create(TemporalFloat64Scalar scalar1, TemporalFloat64Scalar scalar2)
    {
        return new TscList(new[] { scalar1, scalar2 });
    }

    public static TscList Create(TemporalFloat64Scalar scalar1, TemporalFloat64Scalar scalar2, params TemporalFloat64Scalar[] scalarList)
    {
        var scalars = new List<TemporalFloat64Scalar>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return new TscList(scalars);
    }

    public static TscList Create(IReadOnlyCollection<TemporalFloat64Scalar> scalarList)
    {
        return new TscList(scalarList);
    }


    private readonly List<TemporalFloat64Scalar> _baseScalars
        = new List<TemporalFloat64Scalar>();


    public int Count
        => _baseScalars.Count;

    public TemporalFloat64Scalar this[int index]
        => _baseScalars[index];

    public override Float64ScalarRange TimeRange { get; }


    private TscList(IReadOnlyCollection<TemporalFloat64Scalar> scalarList)
    {
        if (scalarList.Count < 2)
            throw new InvalidOperationException();

        Add(scalarList);

        TimeRange = Float64ScalarRange.Create(
            _baseScalars[0].MinTime, 
            _baseScalars[^1].MaxTime
        );

        Debug.Assert(IsValid());
    }


    private void Add(TemporalFloat64Scalar scalar)
    {
        if (scalar is not TscList scalarList)
        {
            if (_baseScalars.Count == 0)
            {
                _baseScalars.Add(scalar);
                return;
            }

            var timeMax = _baseScalars[^1].MaxTime;

            _baseScalars.Add(
                scalar.OffsetTimeMinTo(timeMax)
            );

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

    protected override Float64ScalarRange FindValueRange()
    {
        var minValue = double.PositiveInfinity;
        var maxValue = double.NegativeInfinity;

        foreach (var scalar in _baseScalars)
        {
            var (rangeMin, rangeMax) = scalar.ValueRange;

            if (minValue > rangeMin.ScalarValue) minValue = rangeMin.ScalarValue;
            if (maxValue < rangeMax.ScalarValue) maxValue = rangeMax.ScalarValue;
        }

        return Float64ScalarRange.Create(minValue, maxValue);
    }
    
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return _baseScalars.First(
            scalar => scalar.ContainsTime(t)
        ).GetValue(t);
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