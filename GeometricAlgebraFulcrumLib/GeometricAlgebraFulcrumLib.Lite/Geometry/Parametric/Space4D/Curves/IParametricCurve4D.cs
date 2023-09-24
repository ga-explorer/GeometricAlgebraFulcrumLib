using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space4D.Curves
{
    /// <summary>
    /// A parametric 4D curve with continuous first derivative
    /// </summary>
    public interface IParametricCurve4D :
        IGeometricElement
    {
        Float64ScalarRange ParameterRange { get; }
        
        Float64Vector4D GetPoint(double parameterValue);

        Float64Vector4D GetDerivative1Point(double parameterValue);
    }
}