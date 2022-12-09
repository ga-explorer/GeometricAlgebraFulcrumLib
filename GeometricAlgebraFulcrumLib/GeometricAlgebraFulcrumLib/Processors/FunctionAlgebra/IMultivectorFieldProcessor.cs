using System;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.FunctionAlgebra;

public interface IMultivectorFieldProcessor<T>
{
    IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

    Pair<BasisVectorFrame<T>> BasisVectorFrame { get; }

    Func<GaVector<T>, GaMultivector<T>> Negative(Func<GaVector<T>, GaMultivector<T>> f1);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaMultivector<T>> f1, T f2);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaMultivector<T>> f1, GaVector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaMultivector<T>> f1, GaBivector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaMultivector<T>> f1, GaKVector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaMultivector<T>> f1, GaMultivector<T> f2);
    
    Func<GaVector<T>, GaMultivector<T>> Add(T f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(GaVector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(GaBivector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(GaKVector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(GaMultivector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Add(Func<GaVector<T>, GaMultivector<T>> f1, Func<GaVector<T>, GaMultivector<T>> f2);
    
    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaMultivector<T>> f1, T f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaMultivector<T>> f1, GaVector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaMultivector<T>> f1, GaBivector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaMultivector<T>> f1, GaKVector<T> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaMultivector<T>> f1, GaMultivector<T> f2);
    
    Func<GaVector<T>, GaMultivector<T>> Subtract(T f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(GaVector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(GaBivector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(GaKVector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(GaMultivector<T> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Subtract(Func<GaVector<T>, GaMultivector<T>> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Times(Func<GaVector<T>, GaMultivector<T>> f1, T f2);

    Func<GaVector<T>, GaMultivector<T>> Times(T f1, Func<GaVector<T>, GaMultivector<T>> f2);
    
    Func<GaVector<T>, GaMultivector<T>> Divide(Func<GaVector<T>, GaMultivector<T>> f1, T f2);

    Func<GaVector<T>, Scalar<T>> Sp(Func<GaVector<T>, GaMultivector<T>> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Op(Func<GaVector<T>, GaMultivector<T>> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Gp(Func<GaVector<T>, GaMultivector<T>> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Lcp(Func<GaVector<T>, GaMultivector<T>> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    Func<GaVector<T>, GaMultivector<T>> Rcp(Func<GaVector<T>, GaMultivector<T>> f1, Func<GaVector<T>, GaMultivector<T>> f2);

    GaMultivector<T> GetVectorDerivativeValue(Func<GaVector<T>, GaMultivector<T>> multivectorFunction, GaVector<T> v, GaVector<T> w);

    GaMultivector<T> GetGeometricDerivativeValue(Func<GaVector<T>, GaMultivector<T>> multivectorFunction, GaVector<T> v);
}