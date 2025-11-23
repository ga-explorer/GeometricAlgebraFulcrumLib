using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories;

public class Float64AdaptiveSamplingOptions
{
    private int _minDepth = 4;
    /// <summary>
    /// Minimum subdivision tree depth
    /// </summary>
    public int MinDepth
    {
        get => _minDepth;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be >= 0");

            _minDepth = value;
        }
    }

    private int _maxDepth = 12;
    /// <summary>
    /// Maximum subdivision tree depth
    /// </summary>
    public int MaxDepth
    {
        get => _maxDepth;
        set
        {
            if (value is < 2 or > 30)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be >= 2 and <= 30");

            _maxDepth = value;
        }
    }
        
    private LinFloat64Angle _frameAngleDifference = LinFloat64PolarAngle.CreateFromDegrees(3);
    /// <summary>
    /// Maximum angle between edge frames of a leaf node
    /// </summary>
    public LinFloat64Angle FrameAngleDifference
    {
        get => _frameAngleDifference;
        set
        {
            if (value.DegreesValue is <= 0 or > 180)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0 and <= 180 degrees");

            _frameAngleDifference = value;
        }
    }

    private double _parameterSpan = 1e-8;
    /// <summary>
    /// Avoid tiny parameter spans
    /// </summary>
    public double ParameterSpan
    {
        get => _parameterSpan;
        set
        {
            if (double.IsNaN(value) || value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

            _parameterSpan = value;
        }
    }
        
    private double _relativeChordError = 1e-4;
    /// <summary>
    /// Relative difference tolerance (arcLength - chord) / chord 
    /// </summary>
    public double RelativeChordError
    {
        get => _relativeChordError;
        set
        {
            if (double.IsNaN(value) || value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

            _relativeChordError = value;
        }
    }

    private double _relativeChordImprovement = 1e-3;
    /// <summary>
    /// Relative arc/chord-length improvement required to subdivide.
    /// </summary>
    public double RelativeChordImprovement
    {
        get => _relativeChordImprovement;
        set
        {
            if (double.IsNaN(value) || value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

            _relativeChordImprovement = value;
        }
    }

    /// <summary>
    /// Heuristic for initial dictionary/list sizing
    /// </summary>
    public int ExpectedLeaves 
        => 1 << Math.Min(8, MaxDepth);
}