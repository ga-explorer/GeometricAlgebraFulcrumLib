using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors
{
    /// <summary>
    /// A parametric 3D bivector with continuous first derivative
    /// </summary>
    public interface IParametricBivector3D :
        IGeometricElement
    {
        Float64ScalarRange ParameterRange { get; }
        
        Float64Bivector3D GetBivector(double parameterValue);

        IParametricCurve3D GetNormalVectorCurve(Float64Vector3D? zeroNormal = null);
    }
}