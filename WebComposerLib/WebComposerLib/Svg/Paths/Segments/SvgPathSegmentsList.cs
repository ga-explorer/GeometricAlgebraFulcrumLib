using System.Text;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths.Segments
{
    public class SvgPathSegmentsList<T> : SvgComputedValue where T : ISvgPathSegment
    {
        public static SvgPathSegmentsList<T> Create()
        {
            return new SvgPathSegmentsList<T>();
        }

        public static SvgPathSegmentsList<T> Create(SvgValueLengthUnit unit)
        {
            return new SvgPathSegmentsList<T>() {Unit = unit};
        }

        public static SvgPathSegmentsList<T> Create(T segment)
        {
            var segmentsList = new SvgPathSegmentsList<T>();

            segmentsList.AddSegment(segment);

            return segmentsList;
        }

        public static SvgPathSegmentsList<T> Create(SvgValueLengthUnit unit, T segment)
        {
            var segmentsList = new SvgPathSegmentsList<T> { Unit = unit };

            segmentsList.AddSegment(segment);

            return segmentsList;
        }

        public static SvgPathSegmentsList<T> Create(IEnumerable<T> segments)
        {
            var segmentsList = new SvgPathSegmentsList<T>();

            segmentsList.AddSegments(segments);

            return segmentsList;
        }

        public static SvgPathSegmentsList<T> Create(SvgValueLengthUnit unit, IEnumerable<T> segments)
        {
            var segmentsList = new SvgPathSegmentsList<T> { Unit = unit };

            segmentsList.AddSegments(segments);

            return segmentsList;
        }

        public static SvgPathSegmentsList<T> Create(params T[] segments)
        {
            var segmentsList = new SvgPathSegmentsList<T>();

            segmentsList.AddSegments(segments);

            return segmentsList;
        }

        public static SvgPathSegmentsList<T> Create(SvgValueLengthUnit unit, params T[] segments)
        {
            var segmentsList = new SvgPathSegmentsList<T> { Unit = unit };

            segmentsList.AddSegments(segments);

            return segmentsList;
        }


        private readonly List<T> _segmentsList
            = new List<T>();


        public IEnumerable<T> Segments
            => _segmentsList;

        private SvgValueLengthUnit _unit = SvgValueLengthUnit.None;
        public SvgValueLengthUnit Unit
        {
            get => _unit;
            set => _unit = value ?? SvgValueLengthUnit.None;
        }

        public T this[int index]
            => _segmentsList[index];

        public override string ValueText
        {
            get
            {
                if (_segmentsList.Count == 0)
                    return string.Empty;

                var composer = new StringBuilder();

                foreach (var segment in _segmentsList)
                    composer.Append(segment.SegmentText(_unit)).Append(" ");

                composer.Length -= " ".Length;

                return composer.Append(Environment.NewLine).ToString();
            }
        }


        private SvgPathSegmentsList()
        {
        }


        public SvgPathSegmentsList<T> ClearSegments()
        {
            _segmentsList.Clear();

            return this;
        }

        public SvgPathSegmentsList<T> AddSegment(T segment)
        {
            _segmentsList.Add(segment);

            return this;
        }

        public SvgPathSegmentsList<T> AddSegments(IEnumerable<T> segmentsList)
        {
            _segmentsList.AddRange(segmentsList);

            return this;
        }

        public SvgPathSegmentsList<T> AddSegments(params T[] segmentsList)
        {
            _segmentsList.AddRange(segmentsList);

            return this;
        }

        public SvgPathSegmentsList<T> InsertSegment(int index, T segment)
        {
            _segmentsList.Insert(index, segment);

            return this;
        }

        public SvgPathSegmentsList<T> InsertSegments(int index, IEnumerable<T> segmentsList)
        {
            _segmentsList.InsertRange(index, segmentsList);

            return this;
        }

        public SvgPathSegmentsList<T> InsertSegments(int index, params T[] segmentsList)
        {
            _segmentsList.InsertRange(index, segmentsList);

            return this;
        }

        public SvgPathSegmentsList<T> RemoveSegment(int index)
        {
            _segmentsList.RemoveAt(index);

            return this;
        }
    }
}