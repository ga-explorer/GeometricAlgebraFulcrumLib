using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes;

public interface IGraphicsParametricVolume3D : 
    IAlgebraicElement
{
    LinFloat64Vector3D GetPoint(ILinFloat64Vector3D parameterValue);

    LinFloat64Vector3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3);

    double GetScalarDistance(ILinFloat64Vector3D parameterValue);

    double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3);
        
    GrParametricVolumeLocalFrame3D GetFrame(ILinFloat64Vector3D parameterValue);

    GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3);
}