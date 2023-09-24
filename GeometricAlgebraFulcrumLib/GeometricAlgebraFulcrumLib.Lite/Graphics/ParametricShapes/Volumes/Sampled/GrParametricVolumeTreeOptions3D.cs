using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes.Sampled
{
    public sealed class GrParametricVolumeTreeOptions3D
    {
        private double _maxEdgeFrameDistance = 1e-5d;
        public double MaxEdgeFrameDistance
        {
            get => _maxEdgeFrameDistance;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

                _maxEdgeFrameDistance = value;
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
        
        public bool ForceBalancedTree { get; set; } = true;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTreeOptions3D(double maxDistanceError, int minLevelCount, int maxLevelCount)
        {
            MaxEdgeFrameDistance = maxDistanceError;
            MinLevelCount = minLevelCount;
            MaxLevelCount = maxLevelCount;
        }
    }
}