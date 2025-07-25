﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using MathNet.Numerics.LinearAlgebra;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Samples.Algebra.GeometricAlgebra;

public static class RotationSamples
{
    private static XGaVector<Expr> CreateSymbolicVector(this XGaProcessor<Expr> geometricProcessor, string name, string subscript, int termsCount)
    {
        var vector =
            $"Subscript[{name},{subscript}1]".ToExpr() * geometricProcessor.VectorTerm(0);

        for (var i = 2; i <= termsCount; i++)
            vector += $"Subscript[{name},{subscript}{i}]".ToExpr() * geometricProcessor.VectorTerm(i - 1);

        return vector;
    }

    public static void RotationMatrixToSimpleRotationsSample(int n)
    {
        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var randomComposer =
            geometricProcessor.CreateXGaRandomComposer(n);

        var random = randomComposer.RandomGenerator;

        var rotationSequence =
            LinFloat64PlanarRotationSequence.Create(n);

        for (var i = 0; i < n; i++)
            rotationSequence.AppendMap(
                random.GetVectorToVectorRotation(n)
            );

        var matrix =
            Float64ArrayUtils.CreateClarkeRotationArray(n).ToMatrix();
        //rotationSequence.GetRotationMatrix();

        var eigenValueArray =
            matrix.ToComplex().Evd().EigenValues.ToArray();

        var eigenPairsCount =
            matrix.MatrixEigenDecomposition(
                out var realPairs,
                out var imagPairs
            );

        var kVector = Enumerable.Repeat(1d, n).ToArray().CreateUnitLinVector();

        var eigenValueList = new List<Complex>(n / 2);
        for (var i = 0; i < eigenPairsCount; i++)
        {
            var realValue = realPairs[i].Item1;
            var imagValue = imagPairs[i].Item1;

            // Ignore identity rotations
            if ((realValue - 1d).IsNearZero() && imagValue.IsNearZero())
                continue;

            // Ignore complex conjugate eigen values (only take first one of the pair)
            if (eigenValueList.FindIndex(c => c.IsNearConjugateTo(realValue, imagValue)) >= 0)
                continue;

            eigenValueList.Add(
                new Complex(realValue, imagValue)
            );

            var realVector = realPairs[i].Item2;
            var imagVector = imagPairs[i].Item2;

            var rotation =
                LinFloat64VectorToVectorRotation.CreateFromComplexEigenPair(
                    realValue,
                    imagValue,
                    realVector,
                    imagVector
                );

            var kVectorRotated =
                rotation.MapVector(kVector);

            var angle = rotation.RotationAngle;

            var eigenValue = eigenValueArray[i];

            Console.WriteLine($"Rotation {i + 1}:");
            Console.WriteLine($"   Eigen Value: {eigenValue}");
            Console.WriteLine($"         Angle: {angle}");
            Console.WriteLine($"             k: {kVectorRotated}");
            Console.WriteLine();
        }

        //Debug.Assert(
        //    (r1.RotateVector(u) - v).GetLengthSquared().IsNearZero()
        //);
    }

    public static void ClarkeMatrixToSimpleRotationsSample(int n)
    {
        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var textComposer =
            TextComposerFloat64.DefaultComposer;

        geometricProcessor.CreateXGaRandomComposer(n);

        var clarkeMap =
            geometricProcessor.ClarkeRotationOutermorphism(n);

        var clarkeArray =
            clarkeMap.GetVectorMapArray(n);

        var clarkeMatrix =
            clarkeArray.ToMatrix();

        Console.WriteLine("Generated Clarke Matrix:");
        Console.WriteLine(textComposer.GetArrayText(clarkeArray));
        Console.WriteLine();

        var eigenPairsCount = clarkeMatrix.MatrixEigenDecomposition(
            out var realPairs,
            out var imagPairs
        );

        Console.WriteLine(@"Eigen Values\Vectors:");

        for (var i = 0; i < eigenPairsCount; i++)
        {
            var angle = LinFloat64PolarAngle.CreateFromVector(realPairs[i].Item1, imagPairs[i].Item1);

            Console.WriteLine($"Eigen Value {i + 1}");
            Console.WriteLine($"Real part: {textComposer.GetScalarText(realPairs[i].Item1)}");
            Console.WriteLine($"Imag part: {textComposer.GetScalarText(imagPairs[i].Item1)}");
            Console.WriteLine();

            Console.WriteLine($"Eigen Vector {i + 1}");
            Console.WriteLine($"Real part: {textComposer.GetArrayText(realPairs[i].Item2)}");
            Console.WriteLine($"Imag part: {textComposer.GetArrayText(imagPairs[i].Item2)}");
            Console.WriteLine();

            Console.WriteLine($"Angle: {angle}");
            Console.WriteLine();
        }

        var rotorsArray = new XGaFloat64PureRotor[n / 2];
        var eigenValueList = new List<Complex>(n / 2);
        for (var i = 0; i < rotorsArray.Length; i++)
        {
            var realValue = realPairs[i].Item1;
            var imagValue = imagPairs[i].Item1;

            // Ignore identity rotations
            if ((realValue - 1d).IsNearZero() && imagValue.IsNearZero())
                continue;

            // Ignore complex conjugate eigen values (only take first one of the pair)
            if (eigenValueList.FindIndex(c => c.IsNearConjugateTo(realValue, imagValue)) >= 0)
                continue;

            eigenValueList.Add(
                new Complex(realValue, imagValue)
            );

            rotorsArray[i] =
                geometricProcessor.ComplexEigenPairToPureRotor(
                    realValue,
                    imagValue,
                    geometricProcessor.Vector(realPairs[i].Item2),
                    geometricProcessor.Vector(imagPairs[i].Item2)
                );

            var (angle, bivector) =
                rotorsArray[i].GetEuclideanAngleBivector();

            Console.WriteLine($"Rotor {i + 1}:");
            Console.WriteLine($"      Angle: {angle}");
            Console.WriteLine($"   Bivector: {textComposer.GetMultivectorText(bivector)}");
            Console.WriteLine();
        }

        var rotorsSequence =
            XGaFloat64PureRotorSequence.Create(rotorsArray);

        var finalRotor =
            rotorsSequence.GetFinalRotor();

        var finalRotorMatrix =
            rotorsSequence.GetFinalRotorArray(n);

        var finalMatrixDiffNorm =
            clarkeArray.Subtract(finalRotorMatrix).GetVectorNorm();

        Console.WriteLine($"Final Rotor");
        Console.WriteLine(textComposer.GetMultivectorText(finalRotor));
        Console.WriteLine();

        Console.WriteLine($"Final Rotor Matrix");
        Console.WriteLine(textComposer.GetArrayText(finalRotorMatrix));
        Console.WriteLine();

        Console.WriteLine($"Final Rotor Matrix Difference Norm");
        Console.WriteLine(textComposer.GetScalarText(finalMatrixDiffNorm));
        Console.WriteLine();


        var clarkeRotation =
            LinFloat64MatrixRotation.CreateForwardClarkeRotation(n);

        var vectorRotationSequence =
            clarkeRotation.ToVectorToVectorRotationSequence();

        Debug.Assert(
            rotorsSequence.Count == vectorRotationSequence.Count
        );

        for (var i = 0; i < rotorsSequence.Count; i++)
        {
            var vectorRotation = vectorRotationSequence[i];

            var u1 = vectorRotation.BasisVector1;
            var u2 = geometricProcessor.Vector(u1);

            Debug.Assert(
                (u1 - u2.ToLinVector()).GetVectorNormSquared().IsNearZero()
            );

            var v1 = vectorRotation.MapBasisVector1();
            var v2 = geometricProcessor.Vector(v1);

            Debug.Assert(
                (v1 - v2.ToLinVector()).GetVectorNormSquared().IsNearZero()
            );

            var angle1 = vectorRotation.RotationAngle;
            var bivector1 = v2.Op(u2);
            bivector1 /= bivector1.ENorm();

            Console.WriteLine($"Simple Vector Rotation {i + 1}:");
            Console.WriteLine($"      Angle: {angle1}");
            Console.WriteLine($"   Bivector: {textComposer.GetMultivectorText(bivector1)}");
            Console.WriteLine();

            var rotor = rotorsSequence[i]; //u2.GetEuclideanRotorTo(v2);

            var (angle2, bivector2) =
                rotor.GetEuclideanAngleBivector();

            Console.WriteLine($"Rotor {i + 1}:");
            Console.WriteLine($"      Angle: {angle2}");
            Console.WriteLine($"   Bivector: {textComposer.GetMultivectorText(bivector2)}");
            Console.WriteLine();

            var v3 = vectorRotation.MapVector(u1);

            Debug.Assert(
                (v1 - v3).GetVectorNormSquared().IsNearZero()
            );

            var v4 = rotor.OmMap(u2);

            Console.WriteLine($"v from vector rotation: {v3}");
            Console.WriteLine($"          v from rotor: {textComposer.GetMultivectorText(v4)}");
            Console.WriteLine();

            Debug.Assert(
                (v3 - v4.ToLinVector()).GetVectorNormSquared().IsNearZero()
            );
        }
    }

    public static void CirculantMatrixToSimpleRotationsSample(int n)
    {
        var scalarProcessor =
            ScalarProcessorOfWolframExpr.Instance;

        var geometricProcessor =
            XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var laTeXComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

        laTeXComposer.BasisName = @"\boldsymbol{e}";

        var assumeExpr1 =
            n.GetRange()
                .Select(i => $"Subscript[c, {i}]")
                .Concatenate(" | ");

        var assumeExpr =
            $@"Element[{assumeExpr1}, Reals]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var zero =
            geometricProcessor.ScalarZero;

        var nSqrt =
            $"Sqrt[{n}]".ScalarFromText(scalarProcessor);

        var cArray =
            n.GetRange()
                .Select(i => $"Subscript[c,{i}]".ScalarFromText(scalarProcessor))
                .ToArray();

        //var wArray =
        //    n.GetRange()
        //        .Select(i => $"Exp[2 Pi I {i}/{n}]".CreateScalar(geometricProcessor).FullSimplify())
        //        .ToArray();

        var eigenValueRealArray = new Scalar<Expr>[n];
        var eigenValueImagArray = new Scalar<Expr>[n];

        var eigenVectorRealArray = new XGaVector<Expr>[n];
        var eigenVectorImagArray = new XGaVector<Expr>[n];

        Console.WriteLine($"{n}-Dimensions:");
        Console.WriteLine();

        var eigenValueList = new List<Scalar<Expr>>(n / 2);
        for (var j = 0; j < n; j++)
        {
            //var wj = wArray[j];

            var eigenValue =
                n.GetRange()
                    .Select(k =>
                        cArray[(n - k) % n] * $"ExpToTrig[Exp[2 Pi I {j * k}/{n}]]".ToExpr()
                    ).Aggregate(zero, (scalar1, scalar2) => scalar1 + scalar2);

            eigenValueRealArray[j] = scalarProcessor.ScalarFromValue(
                Mfs.Re[eigenValue.ScalarValue].FullSimplify()
            );

            eigenValueImagArray[j] = scalarProcessor.ScalarFromValue(
                Mfs.Im[eigenValue.ScalarValue].FullSimplify()
            );

            var eigenVector =
                geometricProcessor.Vector(
                    n.GetRange().Select(k =>
                        ($"ExpToTrig[Exp[2 Pi I {j * k}/{n}]]".ToExpr() / nSqrt).ScalarValue
                    )
                );

            eigenVectorRealArray[j] =
                eigenVector.MapScalars(s => Mfs.ToRadicals[Mfs.Re[s]].Evaluate());

            eigenVectorImagArray[j] =
                eigenVector.MapScalars(s => Mfs.ToRadicals[Mfs.Im[s]].Evaluate());

            Console.WriteLine($"Transformation {j + 1}:");
            Console.WriteLine($"   Eigen value Real Part: ${laTeXComposer.GetScalarText(eigenValueRealArray[j])}$");
            Console.WriteLine($"   Eigen value Imag Part: ${laTeXComposer.GetScalarText(eigenValueImagArray[j])}$");
            Console.WriteLine();
            Console.WriteLine($"   Eigen vector Real Part: ${laTeXComposer.GetMultivectorText(eigenVectorRealArray[j])}$");
            Console.WriteLine($"   Eigen vector Imag Part: ${laTeXComposer.GetMultivectorText(eigenVectorImagArray[j])}$");
            Console.WriteLine();

            // A real eigenvalue, we have a directional scaling
            if (eigenValueImagArray[j].IsZero())
            {
                eigenValueList.Add(eigenValue.Scalar());

                var s = eigenValueRealArray[j];
                var w = eigenVectorRealArray[j];

                Console.WriteLine($"   Directional Scaling:");
                Console.WriteLine($"      Scaling Factor: ${laTeXComposer.GetScalarText(s)}$");
                Console.WriteLine($"      Scaling Vector: ${laTeXComposer.GetMultivectorText(w)}$");
                Console.WriteLine();

                continue;
            }

            // A complex eigenvalue, we have a possible new rotation
            // Ignore second conjugate eigenvalue
            var addValue = true;
            foreach (var ev in eigenValueList)
            {
                var sameRealPart =
                    (eigenValueRealArray[j] - Mfs.Re[ev.ScalarValue]).FullSimplifyScalar().IsZero();

                var negativeImagPart =
                    (eigenValueImagArray[j] + Mfs.Im[ev.ScalarValue]).FullSimplifyScalar().IsZero();

                if (sameRealPart && negativeImagPart)
                {
                    addValue = false;
                    break;
                }
            }

            if (!addValue)
            {
                Console.WriteLine($"   Redundant Rotation: Ignore");

                continue;
            }

            eigenValueList.Add(eigenValue.Scalar());

            var angle =
                eigenValueRealArray[j].ArcTan2(eigenValueImagArray[j]).FullSimplifyScalar();

            //TODO: Why is this the correct one, but not the reverse??!!
            var u =
                eigenVectorImagArray[j]
                    .DivideByENorm()
                    .FullSimplifyScalars()
                    .MapScalars(s => Mfs.ToRadicals[s].Evaluate());

            var v =
                eigenVectorRealArray[j]
                    .DivideByENorm()
                    .FullSimplifyScalars()
                    .MapScalars(s => Mfs.ToRadicals[s].Evaluate());

            //var rotor = 
            //    u.GetEuclideanRotorTo(v);

            //var (angle, bivector) = 
            //    rotor.GetEuclideanAngleBivector();

            //angle = angle.FullSimplify();

            var bivector =
                v.Op(u)
                    .FullSimplifyScalars()
                    .MapScalars(s => Mfs.ToRadicals[s].Evaluate());

            //return VectorToVectorRotation.Create(u, v, angle);

            Console.WriteLine($"   Rotation:");
            Console.WriteLine($"      Source Vector: ${laTeXComposer.GetMultivectorText(u)}$");
            Console.WriteLine($"      Target Vector: ${laTeXComposer.GetMultivectorText(v)}$");
            Console.WriteLine($"      Angle: ${laTeXComposer.GetScalarText((IScalar<Expr>)angle)}$");
            Console.WriteLine($"      Rotation Bivector: ${laTeXComposer.GetMultivectorText(bivector)}$");
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public static void OutermorphismMatrixToRotationsSample(int n)
    {
        var metric = XGaFloat64Processor.Euclidean;
        var random = new Random(10);

        //var u = random.GetFloat64Tuple(n).InPlaceNormalize();
        //var v = random.GetFloat64Tuple(n).InPlaceNormalize();

        var rotation =
            LinFloat64PlanarRotationSequence.CreateRandomOrthogonal(random, n, 2);

        var matrix1 = rotation.ToMatrix(n, n);
        var matrix2 = matrix1.GetOutermorphismMatrix(2);
        var matrix3 = matrix1.GetOutermorphismMatrix(3);

        var x1 = random.GetLinVector(n);
        var x2 = random.GetLinVector(n);
        var x3 = random.GetLinVector(n);

        var x12 = metric.Vector(x1).Op(metric.Vector(x2));
        x12.Op(metric.Vector(x3));

        var t1 = (matrix2 * x12.MultivectorToArray1D(1 << n).ToMathNetVector()).ToArray().ToLinVector();
        var t2 = metric.Vector(matrix1.MapVector(x1)).Op(metric.Vector(matrix1.MapVector(x2))).MultivectorToArray1D(1 << n).ToLinVector();

        Debug.Assert(
            (t1 - t2).GetVectorNormSquared().IsNearZero()
        );

        Debug.Assert(
            matrix1.Determinant().IsNearOne(1e-7)
        );

        Debug.Assert(
            matrix2.Determinant().IsNearOne(1e-7)
        );

        Debug.Assert(
            matrix3.Determinant().IsNearOne(1e-7)
        );

        matrix1.GetVectorToVectorRotationSequence();

        matrix2.GetVectorToVectorRotationSequence();

        matrix3.GetVectorToVectorRotationSequence();


        var subspaceList =
            matrix1.GetSimpleEigenSubspaces();

        Console.WriteLine($"1-Vector Matrix Subspaces:");
        Console.WriteLine($"Size: {matrix1.RowCount}");

        var j = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {j++}");
            Console.WriteLine(subspace);
        }

        subspaceList =
            matrix2.GetSimpleEigenSubspaces();

        Console.WriteLine($"2-Vector Matrix Subspaces:");
        Console.WriteLine($"Size: {matrix2.RowCount}");

        j = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {j++}");
            Console.WriteLine(subspace);
        }

        subspaceList =
            matrix3.GetSimpleEigenSubspaces();

        Console.WriteLine($"3-Vector Matrix Subspaces:");
        Console.WriteLine($"Size: {matrix3.RowCount}");

        j = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {j++}");
            Console.WriteLine(subspace);
        }
    }

    public static void Example1()
    {
        for (var n = 3; n <= 6; n++)
        {
            Console.WriteLine($"{n}-dimensions");

            var scalarProcessor =
                ScalarProcessorOfWolframExpr.Instance;

            var geometricProcessor =
                XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

            var laTeXComposer =
                LaTeXComposerOfWolframExpr.DefaultComposer;

            var u =
                geometricProcessor.CreateSymbolicVector("u", "", n);


            var v =
                geometricProcessor.CreateSymbolicVector("u", "", n);


            var uvRotor =
                u.GetEuclideanPureRotorTo(v);

            var uvRotorMatrix =
                uvRotor.GetMultivectorMapArray(n, n);

            Console.WriteLine($@"$R_{{u,v}} = {laTeXComposer.GetMultivectorText(uvRotor.Multivector)}$");
            Console.WriteLine($@"$M_{{u,v}} = {laTeXComposer.GetArrayText(uvRotorMatrix)}$");
            Console.WriteLine();

            for (var k = 0; k < n; k++)
            {
                var ep = geometricProcessor.VectorTerm(k);
                var en = -ep;

                var uepRotor =
                    u.GetEuclideanPureRotorTo(ep);

                var uepRotorMatrix =
                    uepRotor.GetMultivectorMapArray(n, n);

                Console.WriteLine($@"$R_{{u,e_{{{k + 1}}}}} = {laTeXComposer.GetMultivectorText(uepRotor.Multivector)}$");
                Console.WriteLine($@"$M_{{u,e_{{{k + 1}}}}} = {laTeXComposer.GetArrayText(uepRotorMatrix)}$");
                Console.WriteLine();


                var uenRotor =
                    u.GetEuclideanPureRotorTo(en);

                var uenRotorMatrix =
                    uenRotor.GetMultivectorMapArray(n, n);

                Console.WriteLine($@"$R_{{u,-e_{{{k + 1}}}}} = {laTeXComposer.GetMultivectorText(uenRotor.Multivector)}$");
                Console.WriteLine($@"$M_{{u,-e_{{{k + 1}}}}} = {laTeXComposer.GetArrayText(uenRotorMatrix)}$");
                Console.WriteLine();
            }
        }
    }

    public static void Example2()
    {
        const int n = 6;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);


        var clarkeRotation =
            LinFloat64MatrixRotation.CreateForwardClarkeRotation(n);

        var sequence =
            clarkeRotation.ToVectorToVectorRotationSequence();


        for (var j = 0; j < 100; j++)
        {
            var x =
                random.GetVector(-10, 10).ToLinVector();

            var y1 = clarkeRotation.MapVector(x);
            var y2 = sequence.MapVector(x);

            Debug.Assert(
                (y1 - y2).GetVectorNormSquared().IsNearZero()
            );
        }

        var i = 1;
        foreach (var rotation in sequence)
        {
            var r = (LinFloat64VectorToVectorRotation)rotation;

            var u = r.BasisVector1;
            var v = r.MapBasisVector1();

            Console.WriteLine($"Rotation {i}:");
            Console.WriteLine($"   u = {u}");
            Console.WriteLine($"   v = {v}");
            Console.WriteLine($"   Cos Angle = {r.RotationAngleCos}");
            Console.WriteLine($"   Angle = {r.RotationAngle}");
            Console.WriteLine();

            i++;
        }
    }

    public static void Example3()
    {
        const int n = 10;

        var random = new Random(10);

        var matrix =
            random.GetMathNetOrthogonalMatrix(n);

        var rotationSequence =
            matrix.GetVectorToVectorRotationSequence();

        var matrix1 =
            rotationSequence.ToMatrix(n, n).GetVectorToVectorRotationSequence().ToMatrix(n, n);

        Debug.Assert(
            (matrix1 - rotationSequence.ToMatrix(n, n)).L2Norm().IsNearZero()
        );

        Debug.Assert(
            (matrix - rotationSequence.ToMatrix(n, n)).L2Norm().IsNearZero()
        );

        for (var k = 0; k < rotationSequence.Count; k++)
        {
            var rotation = rotationSequence[k];

            var sourceVector =
                rotation.BasisVector1;

            var targetVector =
                rotation.MapBasisVector1();

            Console.WriteLine($"Rotation {k + 1}:");
            Console.WriteLine($"Source Vector: {sourceVector}");
            Console.WriteLine($"Target Vector: {targetVector}");
            Console.WriteLine();
        }

        var rotationMatrix =
            rotationSequence.ToMatrix(n, n);

        for (var k = 0; k < rotationSequence.Count; k++)
        {
            var rotation = rotationSequence[k];

            var u = rotation.BasisVector1;
            var v = rotation.MapBasisVector1();
            var uvSubspace = LinFloat64PlaneSubspace.CreateFromUnitVectors(u, v);

            var x = random.GetFloat64() * u + random.GetFloat64() * v;
            var y = rotationSequence.MapVector(x);

            Debug.Assert(
                uvSubspace.NearContains(y) &&
                (x.GetAngleCos(y) - u.GetAngleCos(v)).IsNearZero()
            );

            y = (rotationMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

            Debug.Assert(
                uvSubspace.NearContains(y) &&
                (x.GetAngleCos(y) - u.GetAngleCos(v)).IsNearZero()
            );
        }

        var subspaceList =
            rotationMatrix.GetSimpleEigenSubspaces();

        var i = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {i}:");
            Console.WriteLine(subspace);

            i++;
        }
    }

    /// <summary>
    /// Validate the rotation properties of arbitrary rotation sequences
    /// </summary>
    public static void Example4()
    {
        const int n = 9;

        var random = new Random(10);

        for (var rotationCount = 1; rotationCount <= n / 2; rotationCount++)
        {
            // Create a rotation sequence of 1 or more orthogonal rotations
            var rotationSequence = LinFloat64PlanarRotationSequence.CreateRandomOrthogonal(
                random,
                n,
                rotationCount
            );

            var rotationMatrix =
                rotationSequence.ToMatrix(n, n);

            // Make sure the result is actually a rotation matrix
            Debug.Assert(
                rotationMatrix.Determinant().IsNearOne(1e-7)
            );

            // Make sure the sequence contains only pair-wise orthogonal rotations
            Debug.Assert(
                rotationSequence.IsNearOrthogonalRotationsSequence()
            );

            var subspaceList =
                rotationMatrix.GetSimpleEigenSubspaces();

            Console.WriteLine($"Orthogonal rotations number: {rotationCount}");

            var j = 1;
            foreach (var subspace in subspaceList)
            {
                Console.WriteLine($"Subspace {j++}");
                Console.WriteLine(subspace);
            }

            foreach (var rotation in rotationSequence)
            {
                var u = rotation.BasisVector1;
                var v = rotation.MapBasisVector1();

                var v1 = rotationSequence.MapVector(u);
                var v2 = (rotationMatrix * MathNetNumericsUtils.ToMathNetVector(u, n)).CreateLinVector();

                // Make sure each rotation is performed independently from the others
                Debug.Assert(
                    (v - v1).GetVectorNormSquared().IsNearZero()
                );

                Debug.Assert(
                    (v - v2).GetVectorNormSquared().IsNearZero()
                );

                Debug.Assert(
                    (v1 - v2).GetVectorNormSquared().IsNearZero()
                );

                // Make sure rotation matrix multiplication is the same as
                // rotation sequence computations
                for (var i = 0; i < 100; i++)
                {
                    var x = random.GetLinVector(n).CreateLinVector();

                    var y1 = rotationSequence.MapVector(x);
                    var y2 = (rotationMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

                    Debug.Assert(
                        (y1 - y2).GetVectorNormSquared().IsNearZero()
                    );
                }
            }
        }


        for (var rotationCount = 1; rotationCount <= n; rotationCount++)
        {
            // Create a rotation sequence of 1 or more general rotations
            var rotationSequence = LinFloat64PlanarRotationSequence.CreateRandom(
                random,
                n,
                rotationCount
            );

            var rotationMatrix =
                rotationSequence.ToMatrix(n, n);

            // Make sure the result is actually a rotation matrix
            Debug.Assert(
                rotationMatrix.Determinant().IsNearOne(1e-7)
            );

            var subspaceList =
                rotationMatrix.GetSimpleEigenSubspaces();

            Console.WriteLine($"General rotations number: {rotationCount}");

            var j = 1;
            foreach (var subspace in subspaceList)
            {
                Console.WriteLine($"Subspace {j++}");
                Console.WriteLine(subspace);
            }

            // Make sure rotation matrix multiplication is the same as
            // rotation sequence computations
            for (var i = 0; i < 100; i++)
            {
                var x = random.GetLinVector(n).CreateLinVector();

                var y1 = rotationSequence.MapVector(x);
                var y2 = (rotationMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

                Debug.Assert(
                    (y1 - y2).GetVectorNormSquared().IsNearZero()
                );
            }
        }
    }

    public static void Example5()
    {
        const int n = 9;

        var random = new Random(10);

        var u = random.GetLinVector(n).CreateUnitLinVector();
        var v = random.GetLinVector(n).CreateUnitLinVector();

        var rotation =
            LinFloat64VectorToVectorRotation.CreateFromRotatedVector(u, v);

        for (var i = 0; i < 100; i++)
        {
            var x = random.GetLinVector(n).CreateLinVector();

            var y1 = rotation.MapVectorProjection(x);
            var y2 = rotation.MapVector(rotation.GetVectorProjection(x));

            Debug.Assert(
                (y1 - y2).GetVectorNormSquared().IsNearZero()
            );
        }
    }


    public static void ValidationsExample3D()
    {
        ValidationExample3D_1();
    }

    private static void ValidationExample3D_1()
    {
        var random = new Random(10);

        for (var i = 0; i < 100; i++)
        {
            // Create a random planar rotation
            var vector1 = random.GetLinVector3D();
            var vector2 = random.GetLinVector3D();
            var rotationAngle = random.GetPolarAngleInQuadrant(0);

            var rotation = LinFloat64PlanarRotation3D.CreateFromSpanningVectors(
                vector1,
                vector2,
                rotationAngle
            );

            // Create the rotation inverse
            var rotationInv = rotation.GetInversePlanarRotation();

            // Assert both rotations are valid
            Debug.Assert(
                rotation.IsValid() &&
                rotationInv.IsValid()
            );

            Debug.Assert(
                vector1.ToUnitLinVector3D().IsNearEqual(rotation.BasisVector1)
            );

            Debug.Assert(
                vector2.GetAngleCosWithUnit(rotation.BasisVector2) >= 0
            );

            // Assert rotated basis vectors are unit vectors and have the same angle of rotation
            Debug.Assert(
                rotation.MapBasisVector1().IsNearUnit()
            );

            Debug.Assert(
                rotation.BasisVector1.GetAngle(rotation.MapBasisVector1()).IsNearEqual(rotationAngle)
            );


            Debug.Assert(
                rotation.MapBasisVector2().IsNearUnit()
            );

            Debug.Assert(
                rotation.BasisVector2.GetAngle(rotation.MapBasisVector2()).IsNearEqual(rotationAngle)
            );


            Debug.Assert(
                rotation.MapBasisVector1().IsNearOrthonormalWith(
                    rotation.MapBasisVector2()
                )
            );


            for (var j = 0; j < 10; j++)
            {
                var u = random.GetLinVector3D() * 10;

                var u1 = rotation.MapVector(u);
                var u2 = rotationInv.MapVector(u1);

                Debug.Assert(
                    u.VectorENormSquared().IsNearEqual(u1.VectorENormSquared())
                );

                Debug.Assert(
                    u.VectorENormSquared().IsNearEqual(u2.VectorENormSquared())
                );

                Debug.Assert(
                    u.IsNearEqual(u2)
                );

                Debug.Assert(
                    u.GetAngle(u1).RadiansValue < rotationAngle.RadiansValue
                );
            }
        }
    }


    public static void Validations()
    {
        ValidationExample1();
        ValidationExample2(false);
        ValidationExample2(true);
        ValidationExample3(false);
        ValidationExample3(true);
        ValidationExample4(false, false);
        ValidationExample4(false, true);
        ValidationExample4(true, false);
        ValidationExample4(true, true);
        ValidationExample5();
        ValidationExample6(false);
        ValidationExample6(true);
        ValidationExample7(false);
        ValidationExample7(true);
        ValidationExample8(false, false);
        ValidationExample8(false, true);
        ValidationExample8(true, false);
        ValidationExample8(true, true);
        ValidationExample9();
        ValidationExample10();
    }

    //TODO: Copy these into the unit tests project
    private static void ValidationExample1()
    {
        const int n = 10;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        for (var j = 0; j < 10; j++)
        {
            var u =
                random.GetVector(-1, 1).DivideByNorm();

            var v =
                random.GetVector(-1, 1).DivideByNorm();

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                LinFloat64VectorToVectorRotation.CreateFromRotatedVector(
                    u.ToLinVector(),
                    v.ToLinVector()
                );

            Debug.Assert(
                (uvRotor.OmMap(u) - v).IsNearZero()
            );

            Debug.Assert(
                (uvVectorRotation.MapVector(u.ToLinVector()) - v.ToLinVector()).IsNearZero()
            );

            for (var axisIndex = 0; axisIndex < n; axisIndex++)
            {
                var x =
                    geometricProcessor.VectorTerm(axisIndex);

                var y1 = uvRotor.OmMap(x).ToLinVector();
                var y2 = uvVectorRotation.MapBasisVector(axisIndex);
                //var y2 = uvVectorRotation.MapVector(axisIndex.CreateLinVector());

                Debug.Assert(
                    (y1 - y2).GetVectorNorm().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).ToLinVector();
                var y2 = uvVectorRotation.MapVector(x.ToLinVector());

                Debug.Assert(
                    (y1 - y2).GetVectorNorm().IsNearZero()
                );

                var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                var x1 = bv.ToSubspace().Project(x);

                var z1 = uvRotor.OmMap(x1).ToLinVector();
                var z2 = uvVectorRotation.MapVector(x1.ToLinVector());
                var z3 = uvVectorRotation.MapVectorProjection(x.ToLinVector());

                Debug.Assert(
                    (z1 - z2).GetVectorNorm().IsNearZero()
                );

                Debug.Assert(
                    (z1 - z3).GetVectorNorm().IsNearZero()
                );
            }
        }
    }

    private static void ValidationExample2(bool uAxisNegative)
    {
        const int n = 10;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        for (var j = 0; j < 10; j++)
        {
            var uAxisIndex =
                random.GetBasisVectorIndex();

            var u =
                geometricProcessor.VectorTerm(uAxisIndex);

            if (uAxisNegative)
                u = -u;

            var v =
                random.GetVector(-1, 1).DivideByNorm();

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                LinFloat64AxisToVectorRotation.CreateFromRotatedVector(
                    LinBasisVector.Create(uAxisIndex, uAxisNegative),
                    v.ToLinVector()
                );

            for (var axisIndex = 0; axisIndex < n; axisIndex++)
            {
                var x =
                    geometricProcessor.VectorTerm(axisIndex);

                var y1 = uvRotor.OmMap(x).ToLinVector();
                var y2 = uvVectorRotation.MapBasisVector(axisIndex);

                Debug.Assert(
                    (y1 - y2).GetVectorNorm().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).ToLinVector();
                var y2 = uvVectorRotation.MapVector(x.ToLinVector());

                Debug.Assert(
                    (y1 - y2).GetVectorNorm().IsNearZero()
                );

                var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                var x1 = bv.ToSubspace().Project(x);

                var z1 = uvRotor.OmMap(x1).ToLinVector();
                var z2 = uvVectorRotation.MapVector(x1.ToLinVector());
                var z3 = uvVectorRotation.MapVectorProjection(x.ToLinVector());

                Debug.Assert(
                    (z1 - z2).GetVectorNorm().IsNearZero()
                );

                if (!uvVectorRotation.IsNearIdentity())
                    Debug.Assert(
                        (z1 - z3).GetVectorNorm().IsNearZero()
                    );
            }
        }
    }

    private static void ValidationExample3(bool vAxisNegative)
    {
        const int n = 10;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        for (var j = 0; j < 10; j++)
        {
            var vAxisIndex =
                random.GetBasisVectorIndex();

            var u =
                random.GetVector(-1, 1).DivideByNorm();

            var v =
                geometricProcessor.VectorTerm(vAxisIndex);

            if (vAxisNegative)
                v = -v;

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                LinFloat64VectorToVectorRotation.CreateFromRotatedVector(
                    u.ToLinVector(),
                    vAxisIndex.CreateLinVector(vAxisNegative ? -1 : 1)
                );

            for (var axisIndex = 0; axisIndex < n; axisIndex++)
            {
                var x =
                    geometricProcessor.VectorTerm(axisIndex);

                var y1 = uvRotor.OmMap(x).ToLinVector();
                var y2 = uvVectorRotation.MapBasisVector(axisIndex);

                Debug.Assert(
                    (y1 - y2).GetVectorNorm().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).ToLinVector();
                var y2 = uvVectorRotation.MapVector(x.ToLinVector());

                Debug.Assert(
                    (y1 - y2).GetVectorNorm().IsNearZero()
                );

                var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                var x1 = bv.ToSubspace().Project(x);

                var z1 = uvRotor.OmMap(x1).ToLinVector();
                var z2 = uvVectorRotation.MapVector(x1.ToLinVector());
                var z3 = uvVectorRotation.MapVectorProjection(x.ToLinVector());

                Debug.Assert(
                    (z1 - z2).GetVectorNorm().IsNearZero()
                );

                if (!uvVectorRotation.IsIdentity())
                    Debug.Assert(
                        (z1 - z3).GetVectorNorm().IsNearZero()
                    );
            }
        }
    }

    private static void ValidationExample4(bool uAxisNegative, bool vAxisNegative)
    {
        const int n = 10;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        for (var uAxisIndex = 0; uAxisIndex < n; uAxisIndex++)
        {
            var u =
                geometricProcessor.VectorTerm(uAxisIndex);

            if (uAxisNegative)
                u = -u;

            for (var vAxisIndex = 0; vAxisIndex < n; vAxisIndex++)
            {
                if (uAxisIndex == vAxisIndex) continue;

                var v =
                    geometricProcessor.VectorTerm(vAxisIndex);

                if (vAxisNegative)
                    v = -v;

                var uvRotor =
                    u.GetEuclideanRotorTo(v);

                var uvVectorRotation =
                    LinFloat64AxisToAxisRotation.Create(
                        LinBasisVector.Create(uAxisIndex, uAxisNegative),
                        LinBasisVector.Create(vAxisIndex, vAxisNegative),
                        LinFloat64PolarAngle.Angle90
                    );

                for (var axisIndex = 0; axisIndex < n; axisIndex++)
                {
                    var x =
                        geometricProcessor.VectorTerm(axisIndex);

                    var y1 = uvRotor.OmMap(x).ToLinVector();
                    var y2 = uvVectorRotation.MapBasisVector(axisIndex);

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                }

                for (var i = 0; i < n; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).ToLinVector();
                    var y2 = uvVectorRotation.MapVector(x.ToLinVector());

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );

                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).ToLinVector();
                    var z2 = uvVectorRotation.MapVector(x1.ToLinVector());
                    var z3 = uvVectorRotation.MapVectorProjection(x.ToLinVector());

                    Debug.Assert(
                        (z1 - z2).GetVectorNorm().IsNearZero()
                    );

                    if (!uvVectorRotation.IsIdentity())
                        Debug.Assert(
                            (z1 - z3).GetVectorNorm().IsNearZero()
                        );
                }
            }
        }
    }

    private static void ValidationExample5()
    {
        const int n = 3;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        var axisArray = new[]
        {
            LinBasisVector.Px,
            LinBasisVector.Py,
            LinBasisVector.Pz,
            LinBasisVector.Nx,
            LinBasisVector.Ny,
            LinBasisVector.Nz
        };

        for (var j = 0; j < 10; j++)
        {
            var u =
                random.GetVector(-1, 1).DivideByNorm();

            var v =
                random.GetVector(-1, 1).DivideByNorm();

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                    u.ToVector3D(),
                    v.ToVector3D()
                );

            foreach (var axisIndex in axisArray)
            {
                var y = axisIndex.ToLinVector3D();

                var x =
                    geometricProcessor.Vector(y.X, y.Y, y.Z);

                var y1 = uvRotor.OmMap(x).ToVector3D();
                var y2 = uvVectorRotation.MapVector(axisIndex);

                Debug.Assert(
                    (y1 - y2).VectorENorm().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).ToVector3D();
                var y2 = uvVectorRotation.MapVector(x.ToVector3D());

                Debug.Assert(
                    (y1 - y2).VectorENorm().IsNearZero()
                );

                var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                var x1 = bv.ToSubspace().Project(x);

                var z1 = uvRotor.OmMap(x1).ToVector3D();
                var z2 = uvVectorRotation.MapVector(x1.ToVector3D());
                var z3 = uvVectorRotation.MapVectorProjection(x.ToVector3D());

                Debug.Assert(
                    (z1 - z2).VectorENorm().IsNearZero()
                );

                if (!uvVectorRotation.IsIdentity())
                    Debug.Assert(
                        (z1 - z3).Norm().IsNearZero()
                    );
            }
        }
    }

    private static void ValidationExample6(bool uAxisNegative)
    {
        const int n = 3;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        var axisArray = new[]
        {
            LinBasisVector.Px,
            LinBasisVector.Py,
            LinBasisVector.Pz,
            LinBasisVector.Nx,
            LinBasisVector.Ny,
            LinBasisVector.Nz
        };

        for (var j = 0; j < 10; j++)
        {
            var uAxisIndex =
                random.GetBasisVectorIndex();

            var uAxis = uAxisIndex switch
            {
                0 => uAxisNegative ? LinBasisVector.Nx : LinBasisVector.Px,
                1 => uAxisNegative ? LinBasisVector.Ny : LinBasisVector.Py,
                2 => uAxisNegative ? LinBasisVector.Nz : LinBasisVector.Pz,
                _ => throw new NotImplementedException()
            };

            var u =
                geometricProcessor.VectorTerm(uAxisIndex);

            if (uAxisNegative)
                u = -u;

            var v =
                random.GetVector(-1, 1).DivideByNorm();

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                    uAxis.ToLinVector3D(),
                    v.ToVector3D()
                );

            foreach (var axisIndex in axisArray)
            {
                var y = axisIndex.ToLinVector3D();

                var x =
                    geometricProcessor.Vector(y.X, y.Y, y.Z);

                var y1 = uvRotor.OmMap(x).ToVector3D();
                var y2 = uvVectorRotation.MapVector(axisIndex);

                Debug.Assert(
                    (y1 - y2).VectorENorm().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).ToVector3D();
                var y2 = uvVectorRotation.MapVector(x.ToVector3D());

                Debug.Assert(
                    (y1 - y2).VectorENorm().IsNearZero()
                );

                var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                var x1 = bv.ToSubspace().Project(x);

                var z1 = uvRotor.OmMap(x1).ToVector3D();
                var z2 = uvVectorRotation.MapVector(x1.ToVector3D());
                var z3 = uvVectorRotation.MapVectorProjection(x.ToVector3D());

                Debug.Assert(
                    (z1 - z2).VectorENorm().IsNearZero()
                );

                if (!uvVectorRotation.IsIdentity())
                    Debug.Assert(
                        (z1 - z3).VectorENorm().IsNearZero()
                    );
            }
        }
    }

    private static void ValidationExample7(bool vAxisNegative)
    {
        const int n = 3;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        var axisArray = new[]
        {
            LinBasisVector.Px,
            LinBasisVector.Py,
            LinBasisVector.Pz,
            LinBasisVector.Nx,
            LinBasisVector.Ny,
            LinBasisVector.Nz
        };

        for (var j = 0; j < 10; j++)
        {
            var u =
                random.GetVector(-1, 1).DivideByNorm();

            var vAxisIndex =
                random.GetBasisVectorIndex();

            var vAxis = vAxisIndex switch
            {
                0 => vAxisNegative ? LinBasisVector.Nx : LinBasisVector.Px,
                1 => vAxisNegative ? LinBasisVector.Ny : LinBasisVector.Py,
                2 => vAxisNegative ? LinBasisVector.Nz : LinBasisVector.Pz,
                _ => throw new NotImplementedException()
            };

            var v =
                geometricProcessor.VectorTerm(vAxisIndex);

            if (vAxisNegative)
                v = -v;

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                    u.ToVector3D(),
                    vAxis.ToLinVector3D()
                );

            foreach (var axisIndex in axisArray)
            {
                var y = axisIndex.ToLinVector3D();

                var x =
                    geometricProcessor.Vector(y.X, y.Y, y.Z);

                var y1 = uvRotor.OmMap(x).ToVector3D();
                var y2 = uvVectorRotation.MapVector(axisIndex);

                Debug.Assert(
                    (y1 - y2).VectorENorm().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).ToVector3D();
                var y2 = uvVectorRotation.MapVector(x.ToVector3D());

                Debug.Assert(
                    (y1 - y2).VectorENorm().IsNearZero()
                );

                var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                var x1 = bv.ToSubspace().Project(x);

                var z1 = uvRotor.OmMap(x1).ToVector3D();
                var z2 = uvVectorRotation.MapVector(x1.ToVector3D());
                var z3 = uvVectorRotation.MapVectorProjection(x.ToVector3D());

                Debug.Assert(
                    (z1 - z2).VectorENorm().IsNearZero()
                );

                if (!uvVectorRotation.IsIdentity())
                    Debug.Assert(
                        (z1 - z3).Norm().IsNearZero()
                    );
            }
        }
    }

    private static void ValidationExample8(bool uAxisNegative, bool vAxisNegative)
    {
        const int n = 3;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        var axisArray = new[]
        {
            LinBasisVector.Px,
            LinBasisVector.Py,
            LinBasisVector.Pz,
            LinBasisVector.Nx,
            LinBasisVector.Ny,
            LinBasisVector.Nz
        };

        for (var uAxisIndex = 0; uAxisIndex < n; uAxisIndex++)
        {
            var uAxis = uAxisIndex switch
            {
                0 => uAxisNegative ? LinBasisVector.Nx : LinBasisVector.Px,
                1 => uAxisNegative ? LinBasisVector.Ny : LinBasisVector.Py,
                2 => uAxisNegative ? LinBasisVector.Nz : LinBasisVector.Pz,
                _ => throw new NotImplementedException()
            };

            var u =
                geometricProcessor.VectorTerm(uAxisIndex);

            if (uAxisNegative)
                u = -u;

            for (var vAxisIndex = 0; vAxisIndex < n; vAxisIndex++)
            {
                if (uAxisIndex == vAxisIndex) continue;

                var vAxis = vAxisIndex switch
                {
                    0 => vAxisNegative ? LinBasisVector.Nx : LinBasisVector.Px,
                    1 => vAxisNegative ? LinBasisVector.Ny : LinBasisVector.Py,
                    2 => vAxisNegative ? LinBasisVector.Nz : LinBasisVector.Pz,
                    _ => throw new NotImplementedException()
                };

                var v =
                    geometricProcessor.VectorTerm(vAxisIndex);

                if (vAxisNegative)
                    v = -v;

                var uvRotor =
                    u.GetEuclideanRotorTo(v);

                var uvVectorRotation =
                    LinFloat64PlanarRotation3D.CreateFromOrthonormalVectors(
                        uAxis.ToLinVector3D(),
                        vAxis.ToLinVector3D(),
                        LinFloat64PolarAngle.Angle90
                    );

                foreach (var axis in axisArray)
                {
                    var y = axis.ToLinVector3D();

                    var x =
                        geometricProcessor.Vector(y.X, y.Y, y.Z);

                    var y1 = uvRotor.OmMap(x).ToVector3D();
                    var y2 = uvVectorRotation.MapVector(axis);

                    Debug.Assert(
                        (y1 - y2).VectorENorm().IsNearZero()
                    );
                }

                for (var i = 0; i < n; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).ToVector3D();
                    var y2 = uvVectorRotation.MapVector(x.ToVector3D());

                    Debug.Assert(
                        (y1 - y2).VectorENorm().IsNearZero()
                    );

                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).ToVector3D();
                    var z2 = uvVectorRotation.MapVector(x1.ToVector3D());
                    var z3 = uvVectorRotation.MapVectorProjection(x.ToVector3D());

                    Debug.Assert(
                        (z1 - z2).VectorENorm().IsNearZero()
                    );

                    if (!uvVectorRotation.IsIdentity())
                        Debug.Assert(
                            (z1 - z3).Norm().IsNearZero()
                        );
                }
            }
        }
    }

    private static void ValidationExample9()
    {
        const int n = 10;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        var u =
            random.GetVector(-1, 1).DivideByNorm();

        var v =
            random.GetVector(-1, 1).DivideByNorm();

        var uvRotation =
            LinFloat64VectorToVectorRotation.CreateFromRotatedVector(
                u.ToLinVector(),
                v.ToLinVector()
            );

        var uvRotationSequence =
            uvRotation.ToMatrix(n, n).GetVectorToVectorRotationSequence();

        for (var axisIndex = 0; axisIndex < n; axisIndex++)
        {
            //var x =
            //    geometricProcessor.Vector(axisIndex);

            var y1 = uvRotation.MapBasisVector(axisIndex);
            var y2 = uvRotationSequence.MapBasisVector(axisIndex);

            Debug.Assert(
                (y1 - y2).GetVectorNorm().IsNearZero()
            );
        }

        for (var i = 0; i < 100; i++)
        {
            var x =
                random.GetVector(-1, 1).ToLinVector();

            var y1 = uvRotation.MapVector(x);
            var y2 = uvRotationSequence.MapVector(x);

            Debug.Assert(
                (y1 - y2).GetVectorNorm().IsNearZero()
            );
        }
    }

    private static void ValidationExample10()
    {
        const int n = 10;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var random =
            geometricProcessor.CreateXGaRandomComposer(n);

        var scaling = LinFloat64VectorDirectionalScaling.Create(
            random.GetScalarValue(-10, 10),
            random.RandomGenerator.GetLinVector(n).CreateUnitLinVector()
        );

        var matrix =
            scaling.ToMatrix(n, n);
        //random.GetArray(n, n).ToMatrix();

        var mapSequence =
            matrix.GetVectorDirectionalScalingSequence();

        Debug.Assert(
            (matrix - mapSequence.ToMatrix(n, n)).ToArray().GetVectorNormSquared().IsNearZero()
        );

        //var u =
        //    random.GetVector(-1, 1).DivideByNorm();

        //var v =
        //    random.GetVector(-1, 1).DivideByNorm();

        //var uvRotation =
        //    VectorToVectorRotation.Create(
        //        u.VectorToLinVector(),
        //        v.VectorToLinVector()
        //    );

        //var uvRotationSequence = 
        //    uvRotation.GetMatrix().GetVectorDirectionalScalingSequence();

        for (var axisIndex = 0; axisIndex < n; axisIndex++)
        {
            var x =
                axisIndex.CreateLinVector();

            var y1 = (matrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();
            var y2 = mapSequence.MapBasisVector(axisIndex);

            Debug.Assert(
                (y1 - y2).GetVectorNormSquared().IsNearZero()
            );
        }

        for (var i = 0; i < 100; i++)
        {
            var x =
                random.GetVector(-1, 1).ToLinVector();

            var y1 = matrix.ToArray().MatrixProduct(x.ToArray(n)).CreateLinVector();
            var y2 = mapSequence.MapVector(x);

            Debug.Assert(
                (y1 - y2).GetVectorNormSquared().IsNearZero()
            );
        }
    }
}