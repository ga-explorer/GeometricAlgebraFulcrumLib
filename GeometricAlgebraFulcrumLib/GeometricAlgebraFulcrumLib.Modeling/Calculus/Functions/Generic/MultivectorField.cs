using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;

public class XGaMultivectorField<T> :
    IXGaMultivectorField<T>
{
    public XGaProcessor<T> GeometricProcessor
        => FieldProcessor.GeometricProcessor;

    public IXGaMultivectorFieldProcessor<T> FieldProcessor { get; }

    public Func<XGaVector<T>, XGaMultivector<T>> MultivectorFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaMultivectorField(IXGaMultivectorFieldProcessor<T> functionProcessor, Func<XGaVector<T>, XGaMultivector<T>> multivectorFunc)
    {
        FieldProcessor = functionProcessor;
        MultivectorFunc = multivectorFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> GetValue(XGaVector<T> v)
    {
        return MultivectorFunc(v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> GetVectorDerivativeValue(XGaVector<T> v, XGaVector<T> w)
    {
        return FieldProcessor.GetVectorDerivativeValue(MultivectorFunc, v, w);
    }
}