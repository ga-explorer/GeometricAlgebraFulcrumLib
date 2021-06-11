

//namespace GeometryComposerLib.Graphics.SVG.Values
//{
//    public class HtmlValuePointsList : HtmlComputedValue
//    {
//        public static HtmlValuePointsList Create()
//        {
//            return new HtmlValuePointsList();
//        }

//        public static HtmlValuePointsList Create(HtmlValueLengthUnit unit)
//        {
//            return new HtmlValuePointsList {Unit = unit};
//        }

//        public static HtmlValuePointsList Create(double x, double y)
//        {
//            var pointsList = new HtmlValuePointsList();

//            return pointsList.AddPoint(x, y);
//        }

//        public static HtmlValuePointsList Create(HtmlValueLengthUnit unit, double x, double y)
//        {
//            var pointsList = new HtmlValuePointsList { Unit = unit };

//            return pointsList.AddPoint(x, y);
//        }

//        public static HtmlValuePointsList Create(IEnumerable<ITuple2D> points)
//        {
//            var pointsList = new HtmlValuePointsList();

//            return pointsList.AddPoints(points);
//        }

//        public static HtmlValuePointsList Create(HtmlValueLengthUnit unit, IEnumerable<ITuple2D> points)
//        {
//            var pointsList = new HtmlValuePointsList { Unit = unit };

//            return pointsList.AddPoints(points);
//        }

//        public static HtmlValuePointsList Create(params double[] points)
//        {
//            var pointsList = new HtmlValuePointsList();

//            return pointsList.AddPoints(points);
//        }

//        public static HtmlValuePointsList Create(HtmlValueLengthUnit unit, params double[] points)
//        {
//            var pointsList = new HtmlValuePointsList { Unit = unit };

//            return pointsList.AddPoints(points);
//        }


//        private readonly List<Tuple2D> _pointsList
//            = new List<Tuple2D>();


//        public IEnumerable<Tuple2D> Points
//            => _pointsList;

//        private HtmlValueLengthUnit _unit = HtmlValueLengthUnit.None;
//        public HtmlValueLengthUnit Unit
//        {
//            get { return _unit; }
//            set { _unit = value ?? HtmlValueLengthUnit.None; }
//        }

//        public ITuple2D this[int index]
//            => _pointsList[index];

//        public override string ValueText
//        {
//            get
//            {
//                if (_pointsList.Count == 0)
//                    return string.Empty;

//                var composer = new StringBuilder();

//                foreach (var point in _pointsList)
//                    composer.Append(point.X.ToHtmlLengthText(_unit))
//                        .Append(",")
//                        .Append(point.Y.ToHtmlLengthText(_unit))
//                        .Append(" ");

//                composer.Length -= " ".Length;

//                return composer.ToString();
//            }
//        }


//        private HtmlValuePointsList()
//        {
//        }


//        public HtmlValuePointsList ClearPoints()
//        {
//            _pointsList.Clear();

//            return this;
//        }

//        public HtmlValuePointsList AddPoint(double x, double y)
//        {
//            _pointsList.Add(new Tuple2D(x, y));

//            return this;
//        }

//        public HtmlValuePointsList InsertPoint(int index, double x, double y)
//        {
//            _pointsList.Insert(index, new Tuple2D(x, y));

//            return this;
//        }

//        public HtmlValuePointsList AddPoints(IEnumerable<ITuple2D> pointsList)
//        {
//            _pointsList.AddRange(pointsList.Select(p => p.ToTuple2D()));

//            return this;
//        }

//        public HtmlValuePointsList InsertPoints(int index, IEnumerable<Tuple2D> pointsList)
//        {
//            _pointsList.InsertRange(index, pointsList);

//            return this;
//        }

//        public HtmlValuePointsList AddPoints(params double[] pointsList)
//        {
//            if ((pointsList.Length & 1) != 0)
//                throw new InvalidOperationException("An even number of coordinates is expected");

//            for (var i = 0; i < pointsList.Length; i += 2)
//                AddPoint(pointsList[i], pointsList[i + 1]);

//            return this;
//        }

//        public HtmlValuePointsList RemovePoint(int index)
//        {
//            _pointsList.RemoveAt(index);

//            return this;
//        }
//    }
//}
