using System;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.FunctionAlgebra;

public interface IVectorFieldProcessor<T>
{
    IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

    Pair<BasisVectorFrame<T>> BasisVectorFrame { get; }

    Func<GaVector<T>, GaVector<T>> Negative(Func<GaVector<T>, GaVector<T>> f1);

    Func<GaVector<T>, GaVector<T>> Add(Func<GaVector<T>, GaVector<T>> f1, T f2);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaVector<T>> f1, GaBivector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaVector<T>> f1, GaKVector<T> f2);

    Func<GaVector<T>, GaVector<T>> Add(Func<GaVector<T>, GaVector<T>> f1, GaVector<T> f2);
    
    Func<GaVector<T>, GaMultivector<T>> Add(T f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(GaBivector<T> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(GaKVector<T> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaVector<T>> Add(GaVector<T> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaVector<T>> Add(Func<GaVector<T>, GaVector<T>> f1, Func<GaVector<T>, GaVector<T>> f2);
    
    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaVector<T>> f1, T f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaVector<T>> f1, GaBivector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaVector<T>> f1, GaKVector<T> f2);

    Func<GaVector<T>, GaVector<T>> Subtract(Func<GaVector<T>, GaVector<T>> f1, GaVector<T> f2);
    
    Func<GaVector<T>, GaMultivector<T>> Subtract(T f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(GaBivector<T> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(GaKVector<T> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaVector<T>> Subtract(GaVector<T> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaVector<T>> Subtract(Func<GaVector<T>, GaVector<T>> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaVector<T>> Times(Func<GaVector<T>, GaVector<T>> f1, T f2);

    Func<GaVector<T>, GaVector<T>> Times(T f1, Func<GaVector<T>, GaVector<T>> f2);
    
    Func<GaVector<T>, GaVector<T>> Divide(Func<GaVector<T>, GaVector<T>> f1, T f2);

    Func<GaVector<T>, Scalar<T>> Sp(Func<GaVector<T>, GaVector<T>> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaBivector<T>> Op(Func<GaVector<T>, GaVector<T>> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Gp(Func<GaVector<T>, GaVector<T>> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, Scalar<T>> Lcp(Func<GaVector<T>, GaVector<T>> f1, Func<GaVector<T>, GaVector<T>> f2);

    Func<GaVector<T>, Scalar<T>> Rcp(Func<GaVector<T>, GaVector<T>> f1, Func<GaVector<T>, GaVector<T>> f2);

    GaVector<T> GetVectorDerivativeValue(Func<GaVector<T>, GaVector<T>> multivectorFunction, GaVector<T> v, GaVector<T> w);

    GaVector<T> GetGeometricDerivativeValue(Func<GaVector<T>, GaVector<T>> multivectorFunction, GaVector<T> v);
}