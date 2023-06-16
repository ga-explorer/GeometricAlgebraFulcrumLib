using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves
{
    /// <summary>
    /// A parametric 3D curve with continuous first derivative
    /// </summary>
    public interface IParametricCurve3D :
        IGeometricElement
    {
        Float64Range1D ParameterRange { get; }
        
        Float64Vector3D GetPoint(double parameterValue);

        Float64Vector3D GetDerivative1Point(double parameterValue);

        ParametricCurveLocalFrame3D GetFrame(double parameterValue);
    }
}