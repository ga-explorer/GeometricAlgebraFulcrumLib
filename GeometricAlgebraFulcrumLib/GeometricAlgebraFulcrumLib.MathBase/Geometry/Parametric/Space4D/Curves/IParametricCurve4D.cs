using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space4D.Curves
{
    /// <summary>
    /// A parametric 4D curve with continuous first derivative
    /// </summary>
    public interface IParametricCurve4D :
        IGeometricElement
    {
        Float64Range1D ParameterRange { get; }
        
        Float64Vector4D GetPoint(double parameterValue);

        Float64Vector4D GetDerivative1Point(double parameterValue);
    }
}