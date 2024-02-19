using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.FunctionAlgebra;

public interface IXGaVectorFieldProcessor<T>
{
    XGaProcessor<T> GeometricProcessor { get; }

    Pair<XGaBasisVectorFrame<T>> XGaBasisVectorFrame { get; }

    Func<XGaVector<T>, XGaVector<T>> Negative(Func<XGaVector<T>, XGaVector<T>> f1);

    Func<XGaVector<T>, XGaVector<T>> Add(Func<XGaVector<T>, XGaVector<T>> f1, T f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaVector<T>> f1, XGaBivector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(Func<XGaVector<T>, XGaVector<T>> f1, XGaKVector<T> f2);

    Func<XGaVector<T>, XGaVector<T>> Add(Func<XGaVector<T>, XGaVector<T>> f1, XGaVector<T> f2);
    
    Func<XGaVector<T>, XGaMultivector<T>> Add(T f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(XGaBivector<T> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Add(XGaKVector<T> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaVector<T>> Add(XGaVector<T> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaVector<T>> Add(Func<XGaVector<T>, XGaVector<T>> f1, Func<XGaVector<T>, XGaVector<T>> f2);
    
    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaVector<T>> f1, T f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaVector<T>> f1, XGaBivector<T> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(Func<XGaVector<T>, XGaVector<T>> f1, XGaKVector<T> f2);

    Func<XGaVector<T>, XGaVector<T>> Subtract(Func<XGaVector<T>, XGaVector<T>> f1, XGaVector<T> f2);
    
    Func<XGaVector<T>, XGaMultivector<T>> Subtract(T f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(XGaBivector<T> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Subtract(XGaKVector<T> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaVector<T>> Subtract(XGaVector<T> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaVector<T>> Subtract(Func<XGaVector<T>, XGaVector<T>> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaVector<T>> Times(Func<XGaVector<T>, XGaVector<T>> f1, T f2);

    Func<XGaVector<T>, XGaVector<T>> Times(T f1, Func<XGaVector<T>, XGaVector<T>> f2);
    
    Func<XGaVector<T>, XGaVector<T>> Divide(Func<XGaVector<T>, XGaVector<T>> f1, T f2);

    Func<XGaVector<T>, Scalar<T>> Sp(Func<XGaVector<T>, XGaVector<T>> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaBivector<T>> Op(Func<XGaVector<T>, XGaVector<T>> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, XGaMultivector<T>> Gp(Func<XGaVector<T>, XGaVector<T>> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, Scalar<T>> Lcp(Func<XGaVector<T>, XGaVector<T>> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    Func<XGaVector<T>, Scalar<T>> Rcp(Func<XGaVector<T>, XGaVector<T>> f1, Func<XGaVector<T>, XGaVector<T>> f2);

    XGaVector<T> GetVectorDerivativeValue(Func<XGaVector<T>, XGaVector<T>> multivectorFunction, XGaVector<T> v, XGaVector<T> w);

    XGaVector<T> GetGeometricDerivativeValue(Func<XGaVector<T>, XGaVector<T>> multivectorFunction, XGaVector<T> v);
}