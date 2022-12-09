using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.FunctionAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Calculus;

public class MultivectorField<T> :
    IMultivectorField<T>
{
    public IGeometricAlgebraProcessor<T> GeometricProcessor 
        => FieldProcessor.GeometricProcessor;

    public IMultivectorFieldProcessor<T> FieldProcessor { get; }

    public Func<GaVector<T>, GaMultivector<T>> MultivectorFunc { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MultivectorField(IMultivectorFieldProcessor<T> functionProcessor, Func<GaVector<T>, GaMultivector<T>> multivectorFunc)
    {
        FieldProcessor = functionProcessor;
        MultivectorFunc = multivectorFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivector<T> GetValue(GaVector<T> v)
    {
        return MultivectorFunc(v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivector<T> GetVectorDerivativeValue(GaVector<T> v, GaVector<T> w)
    {
        return FieldProcessor.GetVectorDerivativeValue(MultivectorFunc, v, w);
    }
}