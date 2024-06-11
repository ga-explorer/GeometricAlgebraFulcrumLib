using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Generic.Space2D;

public interface IParametricCurve2D<T> :
    IAlgebraicElement
{
    ScalarRange<T> ParameterRange { get; }

    LinVector2D<T> GetPoint(T t);

    //LinVector2D<T> GetDerivative1Point(double parameterValue);

    //ParametricCurveLocalFrame2D GetFrame(double parameterValue);
}