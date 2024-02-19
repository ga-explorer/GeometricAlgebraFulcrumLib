namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.PhCurves;

///// <summary>
///// This class represents a cubic Pythagorean hodograph curve in 3D Euclidean space
///// </summary>
///// <typeparam name="T"></typeparam>
//public sealed class CanonicalPhCurveDegree3<T>
//{
//    public static CanonicalPhCurveDegree3<T> Create(IGeometricAlgebraProcessor<T> processor, T p1, T p2, T p3)
//    {
//        return new CanonicalPhCurveDegree3<T>(
//            processor, 
//            processor.CreateVector(p1, p2, p3)
//        );
//    }


//    private readonly BernsteinBasisPairProductSet<T> _basisPairProductSet;
//    private readonly BernsteinBasisPairProductIntegralSet<T> _basisPairProductIntegralSet;

//    public BernsteinBasisSet<T> BasisSet { get; }

//    public Scalar<T> Scalar00 { get; }
        
//    public Scalar<T> Scalar01 { get; }
        
//    public Scalar<T> Scalar11 { get; }

//    public Vector<T> Vector00 { get; }
        
//    public Vector<T> Vector01 { get; }
        
//    public Vector<T> Vector11 { get; }


//    public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }


//    private CanonicalPhCurveDegree3(IGeometricAlgebraProcessor<T> processor, Vector<T> p1)
//    {
//        GeometricProcessor = processor;

//        BasisSet = BernsteinBasisSet<T>.Create(processor, 1);
//        _basisPairProductSet = BernsteinBasisPairProductSet<T>.Create(BasisSet);
//        _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet<T>.Create(_basisPairProductSet);

//        var e1 = processor.CreateVectorBasis(0);
//        var d1 = p1 - e1;

//        var f00 = _basisPairProductIntegralSet.GetValueAt1(0, 0);
//        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
//        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1).CreateScalar(processor);

//        Vector00 = e1;
//        Vector11 = d1;
//        Vector01 = ((1 - f11) * p1 - (f00 - f11) * e1) / f01;

//        Scalar00 = e1.ENorm();
//        Scalar11 = d1.ENorm();

//        Scalar01 = ScaledRotor0.Multivector.ESp(ScaledRotor1.MultivectorReverse) +
//                   ScaledRotor1.Multivector.ESp(ScaledRotor0.MultivectorReverse);
//    }


//    public Vector<T> GetHodographPoint(T parameterValue)
//    {
//        var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
//        var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
//        var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);

//        return GeometricProcessor.Add(
//            GeometricProcessor.Times(f00, Vector00),
//            GeometricProcessor.Times(f01, Vector01),
//            GeometricProcessor.Times(f11, Vector11)
//        ).CreateVector(GeometricProcessor);
//    }

//    public Vector<T> GetCurvePoint(T parameterValue)
//    {
//        var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
//        var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
//        var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);

//        return GeometricProcessor.Add(
//        GeometricProcessor.Times(f00, Vector00),
//            GeometricProcessor.Times(f01, Vector01),
//            GeometricProcessor.Times(f11, Vector11)
//        ).CreateVector(GeometricProcessor);
//    }
        
//    public Scalar<T> GetSigmaValue(T parameterValue)
//    {
//        var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
//        var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
//        var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);

//        return GeometricProcessor.Add(
//            GeometricProcessor.Times(f00, Scalar00),
//            GeometricProcessor.Times(f01, Scalar01),
//            GeometricProcessor.Times(f11, Scalar11)
//        ).CreateScalar(GeometricProcessor);
//    }

//    public Scalar<T> GetLength(T parameterValue)
//    {
//        var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
//        var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
//        var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);

//        return GeometricProcessor.Add(
//            GeometricProcessor.Times(f00, Scalar00),
//            GeometricProcessor.Times(f01, Scalar01),
//            GeometricProcessor.Times(f11, Scalar11)
//        ).CreateScalar(GeometricProcessor);
//    }

//    public Scalar<T> GetLength(T parameterValue1, T parameterValue2)
//    {
//        return GetLength(parameterValue2) - GetLength(parameterValue1);
//    }

//    public Scalar<T> GetLength()
//    {
//        var f00 = _basisPairProductIntegralSet.GetValueAt1(0, 0);
//        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
//        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);

//        return GeometricProcessor.Add(
//            GeometricProcessor.Times(f00, Scalar00),
//            GeometricProcessor.Times(f01, Scalar01),
//            GeometricProcessor.Times(f11, Scalar11)
//        ).CreateScalar(GeometricProcessor);
//    }
//}