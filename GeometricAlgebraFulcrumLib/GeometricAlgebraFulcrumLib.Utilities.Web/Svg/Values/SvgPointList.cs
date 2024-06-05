using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public class SvgPointList : 
    SvgComputedValue
{
    public static SvgPointList Create()
    {
        return new SvgPointList();
    }

    public static SvgPointList Create(SvgLengthUnit unit)
    {
        return new SvgPointList {Unit = unit};
    }

    public static SvgPointList Create(double x, double y)
    {
        var pointsList = new SvgPointList();

        return pointsList.AddPoint(x, y);
    }

    public static SvgPointList Create(SvgLengthUnit unit, double x, double y)
    {
        var pointsList = new SvgPointList { Unit = unit };

        return pointsList.AddPoint(x, y);
    }

    public static SvgPointList Create(IEnumerable<IPair<double>> points)
    {
        var pointsList = new SvgPointList();

        return pointsList.AddPoints(points);
    }

    public static SvgPointList Create(SvgLengthUnit unit, IEnumerable<IPair<double>> points)
    {
        var pointsList = new SvgPointList { Unit = unit };

        return pointsList.AddPoints(points);
    }

    public static SvgPointList Create(params double[] points)
    {
        var pointsList = new SvgPointList();

        return pointsList.AddPoints(points);
    }

    public static SvgPointList Create(SvgLengthUnit unit, params double[] points)
    {
        var pointsList = new SvgPointList { Unit = unit };

        return pointsList.AddPoints(points);
    }


    private readonly List<IPair<double>> _pointsList
        = new List<IPair<double>>();


    public IEnumerable<IPair<double>> Points
        => _pointsList;

    public SvgLengthUnit Unit { get; set; } 
        = SvgLengthUnit.None;

    public IPair<double> this[int index]
        => _pointsList[index];

    public override string ValueText
    {
        get
        {
            if (_pointsList.Count == 0)
                return string.Empty;

            var composer = new StringBuilder();

            foreach (var point in _pointsList)
                composer.Append(point.Item1.ToSvgLengthText(Unit))
                    .Append(",")
                    .Append(point.Item2.ToSvgLengthText(Unit))
                    .Append(" ");

            composer.Length -= " ".Length;

            return composer.ToString();
        }
    }


    private SvgPointList()
    {
    }


    public SvgPointList ClearPoints()
    {
        _pointsList.Clear();

        return this;
    }

    public SvgPointList AddPoint(double x, double y)
    {
        _pointsList.Add(new Pair<double>(x, y));

        return this;
    }

    public SvgPointList InsertPoint(int index, double x, double y)
    {
        _pointsList.Insert(index, new Pair<double>(x, y));

        return this;
    }

    public SvgPointList AddPoints(IEnumerable<IPair<double>> pointsList)
    {
        _pointsList.AddRange(pointsList);

        return this;
    }

    public SvgPointList InsertPoints(int index, IEnumerable<IPair<double>> pointsList)
    {
        _pointsList.InsertRange(index, pointsList);

        return this;
    }

    public SvgPointList AddPoints(params double[] pointsList)
    {
        if ((pointsList.Length & 1) != 0)
            throw new InvalidOperationException("An even number of coordinates is expected");

        for (var i = 0; i < pointsList.Length; i += 2)
            AddPoint(pointsList[i], pointsList[i + 1]);

        return this;
    }

    public SvgPointList RemovePoint(int index)
    {
        _pointsList.RemoveAt(index);

        return this;
    }
}