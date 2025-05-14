using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;

public interface IXGaMultivectorFieldProcessor<T>
{
    XGaProcessor<T> GeometricProcessor { get; }

    Pair<XGaBasisVectorFrame<T>> XGaBasisVectorFrame { get; }

    Func<XGaVector<T>, XGaMultivector<T>> Negative(Func<XGaVector<T>, XGaMultivector<T>> f1);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaMultivector<T>> f1, T f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaVector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaBivector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaKVector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaMultivector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(T f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(XGaVector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(XGaBivector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(XGaKVector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(XGaMultivector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaMultivector<T>> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaMultivector<T>> f1, T f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaVector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaBivector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaKVector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaMultivector<T>> f1, XGaMultivector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(T f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(XGaVector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(XGaBivector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(XGaKVector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(XGaMultivector<T> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaMultivector<T>> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Times(Func<XGaVector<T>, XGaMultivector<T>> f1, T f2);

    Func<XGaVector<T>, XGaMultivector<T>> Times(T f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Divide(Func<XGaVector<T>, XGaMultivector<T>> f1, T f2);

    Func<XGaVector<T>, Scalar<T>> Sp(Func<XGaVector<T>, XGaMultivector<T>> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Op(Func<XGaVector<T>, XGaMultivector<T>> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Gp(Func<XGaVector<T>, XGaMultivector<T>> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Lcp(Func<XGaVector<T>, XGaMultivector<T>> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Rcp(Func<XGaVector<T>, XGaMultivector<T>> f1, Func<XGaVector<T>, XGaMultivector<T>> f2);

    XGaMultivector<T> GetVectorDerivativeValue(Func<XGaVector<T>, XGaMultivector<T>> multivectorFunction, XGaVector<T> v, XGaVector<T> w);

    XGaMultivector<T> GetGeometricDerivativeValue(Func<XGaVector<T>, XGaMultivector<T>> multivectorFunction, XGaVector<T> v);
}