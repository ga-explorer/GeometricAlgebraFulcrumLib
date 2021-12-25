using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Svg.Values
{
    public class SvgValuePointsList : SvgComputedValue
    {
        public static SvgValuePointsList Create()
        {
            return new SvgValuePointsList();
        }

        public static SvgValuePointsList Create(SvgValueLengthUnit unit)
        {
            return new SvgValuePointsList {Unit = unit};
        }

        public static SvgValuePointsList Create(double x, double y)
        {
            var pointsList = new SvgValuePointsList();

            return pointsList.AddPoint(x, y);
        }

        public static SvgValuePointsList Create(SvgValueLengthUnit unit, double x, double y)
        {
            var pointsList = new SvgValuePointsList { Unit = unit };

            return pointsList.AddPoint(x, y);
        }

        public static SvgValuePointsList Create(IEnumerable<ITuple2D> points)
        {
            var pointsList = new SvgValuePointsList();

            return pointsList.AddPoints(points);
        }

        public static SvgValuePointsList Create(SvgValueLengthUnit unit, IEnumerable<ITuple2D> points)
        {
            var pointsList = new SvgValuePointsList { Unit = unit };

            return pointsList.AddPoints(points);
        }

        public static SvgValuePointsList Create(params double[] points)
        {
            var pointsList = new SvgValuePointsList();

            return pointsList.AddPoints(points);
        }

        public static SvgValuePointsList Create(SvgValueLengthUnit unit, params double[] points)
        {
            var pointsList = new SvgValuePointsList { Unit = unit };

            return pointsList.AddPoints(points);
        }


        private readonly List<Tuple2D> _pointsList
            = new List<Tuple2D>();


        public IEnumerable<Tuple2D> Points
            => _pointsList;

        private SvgValueLengthUnit _unit = SvgValueLengthUnit.None;
        public SvgValueLengthUnit Unit
        {
            get { return _unit; }
            set { _unit = value ?? SvgValueLengthUnit.None; }
        }

        public ITuple2D this[int index]
            => _pointsList[index];

        public override string ValueText
        {
            get
            {
                if (_pointsList.Count == 0)
                    return string.Empty;

                var composer = new StringBuilder();

                foreach (var point in _pointsList)
                    composer.Append(point.X.ToSvgLengthText(_unit))
                        .Append(",")
                        .Append(point.Y.ToSvgLengthText(_unit))
                        .Append(" ");

                composer.Length -= " ".Length;

                return composer.ToString();
            }
        }


        private SvgValuePointsList()
        {
        }


        public SvgValuePointsList ClearPoints()
        {
            _pointsList.Clear();

            return this;
        }

        public SvgValuePointsList AddPoint(double x, double y)
        {
            _pointsList.Add(new Tuple2D(x, y));

            return this;
        }

        public SvgValuePointsList InsertPoint(int index, double x, double y)
        {
            _pointsList.Insert(index, new Tuple2D(x, y));

            return this;
        }

        public SvgValuePointsList AddPoints(IEnumerable<ITuple2D> pointsList)
        {
            _pointsList.AddRange(pointsList.Select(p => p.ToTuple2D()));

            return this;
        }

        public SvgValuePointsList InsertPoints(int index, IEnumerable<Tuple2D> pointsList)
        {
            _pointsList.InsertRange(index, pointsList);

            return this;
        }

        public SvgValuePointsList AddPoints(params double[] pointsList)
        {
            if ((pointsList.Length & 1) != 0)
                throw new InvalidOperationException("An even number of coordinates is expected");

            for (var i = 0; i < pointsList.Length; i += 2)
                AddPoint(pointsList[i], pointsList[i + 1]);

            return this;
        }

        public SvgValuePointsList RemovePoint(int index)
        {
            _pointsList.RemoveAt(index);

            return this;
        }
    }
}
