using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled
{
    public sealed class SampledParametricCurveTreeOptions3D
    {
        private double _maxEdgeFramesDistance = 1e-5d;
        public double MaxEdgeFramesDistance
        {
            get => _maxEdgeFramesDistance;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

                _maxEdgeFramesDistance = value;
            }
        }

        private Float64PlanarAngle _maxEdgeFramesAngle = 45;
        public Float64PlanarAngle MaxEdgeFramesAngle
        {
            get => _maxEdgeFramesAngle;
            set
            {
                if (value.Degrees is <= 0 or > 180)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0 and <= 180 degrees");

                _maxEdgeFramesAngle = value;
            }
        }

        private int _maxLevelCount = 10;
        public int MaxLevelCount
        {
            get => _maxLevelCount;
            set
            {
                if (value is < 2 or > 30)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be >= 2 and <= 30");

                _maxLevelCount = value;
            }
        }

        private int _minLevelCount = 3;
        public int MinLevelCount
        {
            get => _minLevelCount;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be >= 0");

                _minLevelCount = value;
            }
        }


        public SampledParametricCurveTreeOptions3D(Float64PlanarAngle maxAngleError, int minLevelCount, int maxLevelCount)
        {
            MaxEdgeFramesAngle = maxAngleError;
            MinLevelCount = minLevelCount;
            MaxLevelCount = maxLevelCount;
        }
    }
}