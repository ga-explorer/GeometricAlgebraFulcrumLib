//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.LinearAlgebra.Double;
//using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
//using NumericalGeometryLib.BasicMath.Tuples.Mutable;

//namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;

//public sealed class SimpleRotationSequence :
//    RotationLinearMap,
//    IReadOnlyList<SimpleRotationLinearMap>
//{
//    public static int MatrixEigenDecomposition(Matrix<double> matrix, out Tuple<double, Vector<double>>[] realPairs, out Tuple<double, Vector<double>>[] imagPairs)
//    {
//        var sysExpr = matrix.ToComplex().Evd();

//        var count = sysExpr.EigenValues.Count;

//        realPairs = new Tuple<double, Vector<double>>[count];
//        imagPairs = new Tuple<double, Vector<double>>[count];

//        //Console.WriteLine("Eigen Vectors Matrix");
//        //Console.WriteLine(
//        //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
//        //        sysExpr.EigenVectors.Real().ToArray()
//        //    )
//        //);
//        //Console.WriteLine();

//        //Console.WriteLine(
//        //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
//        //        sysExpr.EigenVectors.Imaginary().ToArray()
//        //    )
//        //);
//        //Console.WriteLine();

//        for (var j = 0; j < count; j++)
//        {
//            var complexEigenValue = sysExpr.EigenValues[j];
//            var complexEigenVector = sysExpr.EigenVectors.Column(j);

//            realPairs[j] = new Tuple<double, Vector<double>>(
//                complexEigenValue.Real,
//                (Vector)complexEigenVector.Real()
//            );

//            imagPairs[j] = new Tuple<double, Vector<double>>(
//                complexEigenValue.Imaginary,
//                (Vector)complexEigenVector.Imaginary()
//            );
//        }

//        return count;
//    }

//    public static VectorToVectorRotation ComplexEigenPairToSimpleVectorRotation(double realValue, double imagValue, Vector<double> realVector, Vector<double> imagVector)
//    {
//        //var scalar = scalarProcessor.Add(
//        //    scalarProcessor.Times(realValue, realValue),
//        //    scalarProcessor.Times(imagValue, imagValue)
//        //);

//        var angle = Math.Atan2(
//            imagValue,
//            realValue
//        );

//        //TODO: Why is this the correct one, but not the reverse??!!
//        var u = Float64Tuple4D.Create(imagVector).InPlaceNormalize();
//        var v = Float64Tuple4D.Create(realVector).InPlaceNormalize();

//        return VectorToVectorRotation.Create(u, v, angle);

//        //Console.WriteLine($"Eigen value real part: {realValue.GetLaTeXDisplayEquation()}");
//        //Console.WriteLine();

//        //Console.WriteLine($"Eigen value imag part: {imagValue.GetLaTeXDisplayEquation()}");
//        //Console.WriteLine();

//        //Console.WriteLine($"Eigen value length: {scalar.GetLaTeXDisplayEquation()}");
//        //Console.WriteLine();

//        //Console.WriteLine($"Eigen value angle: {angle.GetLaTeXDisplayEquation()}");
//        //Console.WriteLine();

//        //Console.WriteLine("Eigen vector real part:");
//        //Console.WriteLine(realVector.TermsToLaTeX().GetLaTeXDisplayEquation());
//        //Console.WriteLine();

//        //Console.WriteLine("Eigen vector imag part:");
//        //Console.WriteLine(imagVector.TermsToLaTeX().GetLaTeXDisplayEquation());
//        //Console.WriteLine();

//        //Console.WriteLine("Blade:");
//        //Console.WriteLine(blade.ToLaTeXEquationsArray("B", @"\mu"));
//        //Console.WriteLine();

//        //Console.WriteLine("Final rotor:");
//        //Console.WriteLine(rotor.ToLaTeXEquationsArray("R", @"\mu"));
//        //Console.WriteLine();

//        //Console.WriteLine($"Is simple rotor? {rotor.IsSimpleRotor()}");
//        //Console.WriteLine();

//        //Console.WriteLine();

//        //return rotor;
//    }

//    public static SimpleRotationSequence CreateFromRotationMatrix(Matrix<double> matrix)
//    {
//        Debug.Assert(
//            matrix.RowCount == matrix.ColumnCount
//        );

//        var rotationSequence = new SimpleRotationSequence(matrix.RowCount);

//        var eigenPairsCount = MatrixEigenDecomposition(
//            matrix,
//            out var realPairs,
//            out var imagPairs
//        );

//        var eigenValueList = new List<System.Numerics.Complex>(eigenPairsCount / 2);
//        for (var i = 0; i < eigenPairsCount; i++)
//        {
//            var realValue = realPairs[i].Item1;
//            var imagValue = imagPairs[i].Item1;
            
//            // Ignore identity rotations
//            if ((realValue - 1d).IsNearZero() && imagValue.IsNearZero())
//                continue;

//            // Ignore complex conjugate eigen values (only take first one of the pair)
//            if (eigenValueList.FindIndex(c => c.IsNearConjugateTo(realValue, imagValue)) >= 0)
//                continue;

//            eigenValueList.Add(
//                new System.Numerics.Complex(realValue, imagValue)
//            );

//            var realVector = realPairs[i].Item2;
//            var imagVector = imagPairs[i].Item2;

//            var rotation =
//                ComplexEigenPairToSimpleVectorRotation(
//                    realValue,
//                    imagValue,
//                    realVector,
//                    imagVector
//                );

//            rotationSequence.AppendRotation(rotation);

//            //Console.WriteLine($"Real Eigen Value {i + 1}: {realValue}");
//            //Console.WriteLine($"Imag Eigen Value {i + 1}: {imagValue}");
//            //Console.WriteLine();

//            //Console.WriteLine($"Real Eigen Vector {i + 1}: {(Float64Tuple4D) realVector}");
//            //Console.WriteLine($"Imag Eigen Vector {i + 1}: {(Float64Tuple4D) imagVector}");
//            //Console.WriteLine();
//        }

//        return rotationSequence;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static SimpleRotationSequence Create(int dimensions)
//    {
//        return new SimpleRotationSequence(dimensions);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static SimpleRotationSequence Create(SimpleRotationLinearMap rotation)
//    {
//        var rotationSequence =
//            new SimpleRotationSequence(rotation.Dimensions);

//        rotationSequence.AppendRotation(rotation);

//        return rotationSequence;
//    }


//    private readonly List<SimpleRotationLinearMap> _rotationList
//        = new List<SimpleRotationLinearMap>();


//    public override int Dimensions { get; }

//    public int Count
//        => _rotationList.Count;

//    public SimpleRotationLinearMap this[int index]
//        => _rotationList[index];


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private SimpleRotationSequence(int dimensions)
//    {
//        Dimensions = dimensions;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public SimpleRotationSequence AppendRotation(SimpleRotationLinearMap rotation)
//    {
//        if (rotation.Dimensions != Dimensions)
//            throw new ArgumentException();

//        _rotationList.Add(rotation);

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public SimpleRotationSequence PrependRotation(SimpleRotationLinearMap rotation)
//    {
//        if (rotation.Dimensions != Dimensions)
//            throw new ArgumentException();

//        _rotationList.Insert(0, rotation);

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public SimpleRotationSequence InsertRotation(int index, SimpleRotationLinearMap rotation)
//    {
//        if (rotation.Dimensions != Dimensions)
//            throw new ArgumentException();

//        _rotationList.Insert(index, rotation);

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override bool IsValid()
//    {
//        return _rotationList.All(r => r.IsValid());
//    }

//    public override bool IsIdentity()
//    {
//        throw new NotImplementedException();
//    }

//    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
//    {
//        throw new NotImplementedException();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override Float64Tuple4D MapVectorBasis(int basisIndex)
//    {
//        Debug.Assert(basisIndex >= 0 && basisIndex < Dimensions);

//        return MapVector(
//            Float64Tuple4D.CreateBasis(Dimensions, basisIndex)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override Float64Tuple4D MapVector(Float64Tuple4D vector)
//    {
//        Debug.Assert(vector.Dimensions == Dimensions);
            
//        if (_rotationList.Count == 0)
//            return Float64Tuple4D.CreateCopy(vector);

//        return _rotationList.Aggregate(
//            vector,
//            (current, rotation) => rotation.MapVector(current)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
//    {
//        var reflection = 
//            HyperPlaneNormalReflectionSequence.Create(Dimensions);

//        reflection.AppendMaps(
//            _rotationList.SelectMany(rotation => rotation.ToHyperPlaneReflectionSequence())
//        );
        
//        return reflection;
//    }

//    public override RotationLinearMap GetVectorRotationInverse()
//    {
//        var rotation = new SimpleRotationSequence(Dimensions);

//        foreach (var r in _rotationList)
//            rotation.PrependRotation(r);

//        return rotation;
//    }

//    public override VectorToVectorRotationSequence ToVectorToVectorRotationSequence()
//    {
//        throw new NotImplementedException();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override SimpleRotationSequence ToSimpleVectorRotationSequence()
//    {
//        return this;
//    }


//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public Matrix<double> GetRotationMatrix()
//    //{
//    //    var columnList =
//    //        Dimensions
//    //            .GetRange()
//    //            .Select(i => MapVectorBasis(i).MathNetVector);

//    //    return Matrix<double>
//    //        .Build
//    //        .SparseOfColumnVectors(columnList);
//    //}


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerator<SimpleRotationLinearMap> GetEnumerator()
//    {
//        if (_rotationList.Count == 0)
//        {
//            yield return IdentityLinearMap.Create(Dimensions);
//        }
//        else
//        {
//            foreach (var rotation in _rotationList)
//                yield return rotation;
//        }
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }
//}