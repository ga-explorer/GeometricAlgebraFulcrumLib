using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces.Sampled;

public sealed class GrParametricSurfaceTreeOptions3D
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

    private LinFloat64DirectedAngle _maxEdgeFrameAngle = 45;
    public LinFloat64DirectedAngle MaxEdgeFrameAngle
    {
        get => _maxEdgeFrameAngle;
        set
        {
            if (value.DegreesValue is <= 0 or > 180)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0 and <= 180 degrees");

            _maxEdgeFrameAngle = value;
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


    public GrParametricSurfaceTreeOptions3D(LinFloat64DirectedAngle maxAngleError, int minLevelCount, int maxLevelCount)
    {
        MaxEdgeFrameAngle = maxAngleError;
        MinLevelCount = minLevelCount;
        MaxLevelCount = maxLevelCount;
    }
}