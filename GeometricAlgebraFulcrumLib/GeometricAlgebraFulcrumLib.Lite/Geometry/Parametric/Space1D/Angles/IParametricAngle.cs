using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles
{
    /// <summary>
    /// A parametric angle with continuous first derivative
    /// </summary>
    public interface IParametricAngle :
        IGeometricElement
    {
        Float64ScalarRange ParameterRange { get; }

        Float64PlanarAngle GetAngle(double parameterValue);

        Float64PlanarAngle GetDerivative1Angle(double parameterValue);
    }
}