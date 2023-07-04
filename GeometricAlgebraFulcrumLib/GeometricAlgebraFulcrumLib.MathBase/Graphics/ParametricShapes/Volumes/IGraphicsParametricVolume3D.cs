using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.ParametricShapes.Volumes
{
    public interface IGraphicsParametricVolume3D : 
        IGeometricElement
    {
        Float64Vector3D GetPoint(IFloat64Vector3D parameterValue);

        Float64Vector3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3);

        double GetScalarDistance(IFloat64Vector3D parameterValue);

        double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3);
        
        GrParametricVolumeLocalFrame3D GetFrame(IFloat64Vector3D parameterValue);

        GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3);
    }
}