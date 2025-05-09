namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite.Natural;

public class NfcLinearIntDoubleMap : NaturalFiniteCollection<double>
{
    public static NfcLinearIntDoubleMap Create(int itemsCount, double firstValue, double lastValue)
    {
        return new NfcLinearIntDoubleMap()
        {
            ValuesCount = itemsCount,
            FirstValue = firstValue,
            LastValue = lastValue,
            Step = (lastValue - firstValue) / (itemsCount - 1)
        };
    }

    public static NfcLinearIntDoubleMap Create(int itemsCount, double firstValue, double lastValue, bool excludeFirstValue, bool excludeLastValue)
    {
        var step = lastValue - firstValue;

        if (excludeFirstValue && excludeLastValue)
        {
            step = step / (itemsCount + 1);
            firstValue += step;
            lastValue -= step;
        }
        else if (!excludeFirstValue && excludeLastValue)
        {
            step = step / itemsCount;
            lastValue -= step;
        }
        else if (excludeFirstValue)
        {
            step = step / itemsCount;
            firstValue += step;
        }

        return new NfcLinearIntDoubleMap()
        {
            ValuesCount = itemsCount,
            FirstValue = firstValue,
            LastValue = lastValue,
            Step = (lastValue - firstValue) / (itemsCount - 1)
        };
    }


    public double FirstValue { get; private set; }

    public double LastValue { get; private set; }

    public double Step { get; private set; }

    public int ValuesCount { get; private set; }

    public override int Count => ValuesCount;

    public double this[int index]
    {
        get
        {
            if (index == 0) return FirstValue;

            var t = index / (double)MaxIndex;
            var s = (MaxIndex - index) / (double)MaxIndex;

            return t * LastValue + s * FirstValue;
        }
    }

    public double[] this[params int[] indexList]
    {
        get
        {
            var items = new double[indexList.Length];

            for (var i = 0; i < indexList.Length; i++)
                items[i] = this[indexList[i]];

            return items;
        }
    }

    public IEnumerable<double> this[IEnumerable<int> indexList]
    {
        get { return indexList.Select(index => this[index]); }
    }


    private NfcLinearIntDoubleMap()
    {
            
    }


    public override double GetItem(int index)
    {
        return this[index];
    }
}