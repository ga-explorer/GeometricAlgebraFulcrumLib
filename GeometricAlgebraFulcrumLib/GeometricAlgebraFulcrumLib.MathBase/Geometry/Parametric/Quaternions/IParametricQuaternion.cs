using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Quaternions
{
    /// <summary>
    /// A parametric 4D curve with continuous first derivative
    /// </summary>
    public interface IParametricQuaternion :
        IGeometricElement
    {
        Float64Range1D ParameterRange { get; }
        
        Float64Quaternion GetPoint(double parameterValue);

        Float64Quaternion GetDerivative1Point(double parameterValue);
    }
}