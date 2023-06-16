using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive
{
    public sealed class AdaptiveCurveSamplingOptions3D
    {
        private double _maxEdgeFramesParameterDistance;
        public double MaxEdgeFramesParameterDistance
        {
            get => _maxEdgeFramesParameterDistance;
            set
            {
                if (double.IsNaN(value) || value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

                _maxEdgeFramesParameterDistance = value;
            }
        }

        private double _maxEdgeFramesDistance = 1e-5d;
        public double MaxEdgeFramesDistance
        {
            get => _maxEdgeFramesDistance;
            set
            {
                if (double.IsNaN(value) || value <= 0)
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
                if (value.Degrees.Value is <= 0 or > 180)
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


        public AdaptiveCurveSamplingOptions3D(Float64PlanarAngle maxAngleError, int minLevelCount, int maxLevelCount)
        {
            MaxEdgeFramesAngle = maxAngleError;
            MinLevelCount = minLevelCount;
            MaxLevelCount = maxLevelCount;
        }
    }
}