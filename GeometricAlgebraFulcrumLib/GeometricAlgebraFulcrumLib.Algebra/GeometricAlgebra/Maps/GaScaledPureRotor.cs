//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

//namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Maps;

//public class XGaFloat64PureScalingRotor :
//    IAlgebraicElement
//{
//    public XGaFloat64Processor Processor 
//        => Multivector.Processor;

//    public double ScalingFactor { get; }

//    public XGaFloat64Multivector Multivector { get; }

//    public XGaFloat64Multivector MultivectorReverse { get; }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal XGaFloat64PureScalingRotor(XGaFloat64Multivector multivector)
//    {
//        Multivector = multivector;
//        MultivectorReverse = multivector.Reverse();
//        ScalingFactor = Multivector.Sp(MultivectorReverse).ScalarValue;
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal XGaFloat64PureScalingRotor(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse)
//    {
//        Multivector = multivector;
//        MultivectorReverse = multivectorReverse;
//        ScalingFactor = multivector.Sp(multivectorReverse).ScalarValue;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal XGaFloat64PureScalingRotor(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse, double scalingFactor)
//    {
//        Multivector = multivector;
//        MultivectorReverse = multivectorReverse;
//        ScalingFactor = scalingFactor;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsValid()
//    {
//        return Multivector.IsValid() &&
//               MultivectorReverse.IsValid() &&
//               Multivector.Gp(MultivectorReverse).IsScalar();
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double GetScalingFactor()
//    {
//        return Multivector.Sp(MultivectorReverse).ScalarValue;
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64PureScalingRotor GetPureRotor()
//    {
//        var normSquared = Multivector.Sp(MultivectorReverse).ScalarValue;

//        var mv = Processor.IsEuclidean
//            ? Multivector.Divide(normSquared.Sqrt())
//            : Multivector.Divide(normSquared.SqrtOfAbs());

//        return new XGaFloat64PureScalingRotor(mv);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64PureScalingRotor GetPureScalingRotorInverse()
//    {
//        var scalingFactor = GetScalingFactor();
            
//        return new XGaFloat64PureScalingRotor(
//            MultivectorReverse.Divide(scalingFactor),
//            Multivector.Divide(scalingFactor)
//        );
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinFloat64Vector2D OmMap(ILinFloat64Vector2D vector)
//    {
//        return Multivector.Gp(vector.ToXGaFloat64Vector(Processor)).Gp(MultivectorReverse).VectorPartToVector2D();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinFloat64Vector3D OmMap(ILinFloat64Vector3D vector)
//    {
//        return Multivector.Gp(vector.ToXGaFloat64Vector(Processor)).Gp(MultivectorReverse).VectorPartToVector3D();
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
//    {
//        return Multivector.Gp(multivector).Gp(MultivectorReverse);
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64PureScalingRotor CreatePureScalingRotor(double scalingFactor)
//    {
//        var mv = scalingFactor.Sqrt() * Multivector;
//        var mvReverse = mv.Reverse();

//        return new XGaFloat64PureScalingRotor(
//            mv,
//            mvReverse
//        );
//    }

//}