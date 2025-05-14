using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Spaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;

public interface IXGaFloat64MultivectorFieldProcessor
{
    XGaFloat64Space Space { get; }

    Pair<XGaFloat64BasisVectorFrame> XGaBasisVectorFrame { get; }

    Func<XGaFloat64Vector, XGaFloat64Multivector> Negative(Func<XGaFloat64Vector, XGaFloat64Multivector> f1);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Vector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Bivector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64KVector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Multivector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(double f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64Vector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64Bivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64KVector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64Multivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Vector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Bivector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64KVector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Multivector f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(double f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64Vector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64Bivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64KVector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64Multivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Times(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Times(double f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Divide(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2);

    Func<XGaFloat64Vector, XGaFloat64Scalar> Sp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Op(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Gp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Lcp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    Func<XGaFloat64Vector, XGaFloat64Multivector> Rcp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2);

    XGaFloat64Multivector GetVectorDerivativeValue(Func<XGaFloat64Vector, XGaFloat64Multivector> multivectorFunction, XGaFloat64Vector v, XGaFloat64Vector w);

    XGaFloat64Multivector GetGeometricDerivativeValue(Func<XGaFloat64Vector, XGaFloat64Multivector> multivectorFunction, XGaFloat64Vector v);
}