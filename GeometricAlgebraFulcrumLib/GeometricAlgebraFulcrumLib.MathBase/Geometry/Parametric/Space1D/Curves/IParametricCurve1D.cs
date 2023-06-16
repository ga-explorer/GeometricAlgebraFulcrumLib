using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves;

/// <summary>
/// A parametric 1D curve with continuous first derivative
/// </summary>
public interface IParametricCurve1D :
    IGeometricElement
{
    Float64Range1D ParameterRange { get; }
    
    double GetPoint(double parameterValue);

    double GetDerivative1Point(double parameterValue);
}