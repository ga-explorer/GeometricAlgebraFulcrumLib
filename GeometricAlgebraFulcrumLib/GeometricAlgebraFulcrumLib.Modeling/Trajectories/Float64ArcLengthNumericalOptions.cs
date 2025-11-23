namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories;

public class Float64ArcLengthNumericalOptions
{
    public Float64IntegrationMode IntegrationMode { get; set; } 
        = Float64IntegrationMode.GaussLegendre7;

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
 
    public int MaxSimpsonRecursionDepth { get; set; } = 15;
}