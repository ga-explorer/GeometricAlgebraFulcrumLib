using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

public sealed class XGaEuclideanScaledRotorSquared2D<T>
    : XGaScaledRotorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaEuclideanScaledRotorSquared2D<T> Create(XGaProcessor<T> processor, IScalar<T> scalar0)
    {
        return new XGaEuclideanScaledRotorSquared2D<T>(processor, scalar0, processor.ScalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaEuclideanScaledRotorSquared2D<T> Create(XGaProcessor<T> processor, IScalar<T> scalar0, IScalar<T> scalar12)
    {
        return new XGaEuclideanScaledRotorSquared2D<T>(processor, scalar0, scalar12);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator Multivector<T>(ScaledPureRotor<T> rotor)
    //{
    //    return rotor.Multivector;
    //}


    public Scalar<T> Scalar0 { get; }

    public Scalar<T> Scalar12 { get; }


    private XGaEuclideanScaledRotorSquared2D(XGaProcessor<T> processor, T scalar0, T scalar12)
        : base(processor)
    {
        Scalar0 = scalar0.ScalarFromValue(processor.ScalarProcessor);
        Scalar12 = scalar12.ScalarFromValue(processor.ScalarProcessor);
    }
    
    private XGaEuclideanScaledRotorSquared2D(XGaProcessor<T> processor, IScalar<T> scalar0, IScalar<T> scalar12)
        : base(processor)
    {
        Scalar0 = scalar0.ToScalar();
        Scalar12 = scalar12.ToScalar();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaEuclideanScaledRotorSquared2D<T> GetPureScaledRotorSquared2DInverse()
    {
        var scalingFactorSquared = GetScalingFactorSquared();

        return new XGaEuclideanScaledRotorSquared2D<T>(
            Processor,
            Scalar0.Divide(scalingFactorSquared).ScalarValue,
            Scalar12.NegativeDivide(scalingFactorSquared).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetScalingFactor()
    {
        return GetScalingFactorSquared().Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetScalingFactorSquared()
    {
        return Scalar0 * Scalar0 + Scalar12 * Scalar12;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaEuclideanScaledRotorSquared2D<T> GetPureRotorSquared2D()
    {
        var scalingFactor = 
            GetScalingFactor();

        return Create(
            Processor,
            Scalar0 / scalingFactor,
            Scalar12 / scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaScaledRotor<T> GetScaledRotorInverse()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> OmMap(T u1, T u2)
    {
        var v1 = Scalar0 * u1 + Scalar12 * u2;
        var v2 = Scalar0 * u2 - Scalar12 * u1;

        return Processor.Vector(v1, v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMap(XGaVector<T> mv)
    {
        var u1 = mv.Scalar(0).ScalarValue;
        var u2 = mv.Scalar(1).ScalarValue;

        return OmMap(u1, u2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> mv)
    {
        return GetScalingFactor() * mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        throw new InvalidOperationException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
    {
        var mv0 = mv.Scalar();
        var mv1 = mv.Scalar(0);
        var mv2 = mv.Scalar(1);
        var mv12 = mv.GetBasisBladeScalar(3UL.BitPatternToIndexSet());
            
        var scalingFactor = GetScalingFactor();

        var v0 = scalingFactor * mv0;
        var v1 = Scalar0 * mv1 + Scalar12 * mv2;
        var v2 = Scalar0 * mv2 - Scalar12 * mv1;
        var v12 = scalingFactor * mv12;

        return Processor.Multivector2D(
            v0.ScalarValue, 
            v1.ScalarValue, 
            v2.ScalarValue, 
            v12.ScalarValue
        );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public EuclideanScaledRotorSquared2D<T> GpSquared()
    //{
    //    var s0 = ScalarProcessor.Subtract(
    //        ScalarProcessor.Times(Scalar0, Scalar0),
    //        ScalarProcessor.Times(Scalar12, Scalar12)
    //    );

    //    var s12 = ScalarProcessor.Times(
    //        ScalarProcessor.ScalarTwo,
    //        ScalarProcessor.Times(Scalar0, Scalar12)
    //    );

    //    return new EuclideanScaledRotorSquared2D<T>(ScalarProcessor, s0, s12);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaEuclideanScaledRotorSquared2D<T> Gp(XGaEuclideanScaledRotorSquared2D<T> rotor2)
    {
        var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
        var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

        return new XGaEuclideanScaledRotorSquared2D<T>(Processor, s0.ScalarValue, s12.ScalarValue);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public VectorStorage<T> Gp(VectorStorage<T> vector)
    //{
    //    var u1 = ScalarProcessor.GetTermScalarByIndex(vector, 0);
    //    var u2 = ScalarProcessor.GetTermScalarByIndex(vector, 1);

    //    var v1 = ScalarProcessor.Add(
    //        ScalarProcessor.Times(Scalar0, u1),
    //        ScalarProcessor.Times(Scalar12, u2)
    //    );

    //    var v2 = ScalarProcessor.Subtract(
    //        ScalarProcessor.Times(Scalar0, u2),
    //        ScalarProcessor.Times(Scalar12, u1)
    //    );

    //    return ScalarProcessor.CreateVectorStorage(v1, v2);
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivector()
    {
        return Processor
            .CreateComposer()
            .SetTerm(0, Scalar0)
            .SetTerm(3, Scalar12)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorReverse()
    {
        return Processor
            .CreateComposer()
            .SetTerm(0, Scalar0)
            .SetTerm(3, -Scalar12)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorInverse()
    {
        var scalingFactor = GetScalingFactor().Inverse();

        return Processor
            .CreateComposer()
            .SetTerm(0, Scalar0 * scalingFactor)
            .SetTerm(3, -Scalar12 * scalingFactor)
            .GetMultivector();
    }
}