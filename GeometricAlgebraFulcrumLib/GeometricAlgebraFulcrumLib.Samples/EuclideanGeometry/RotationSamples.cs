using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.SubSpaces;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Scaling;
using GeometricAlgebraFulcrumLib.MathBase.Text;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using MathNet.Numerics.LinearAlgebra;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class RotationSamples
    {
        private static XGaVector<Expr> CreateSymbolicVector(this XGaProcessor<Expr> geometricProcessor, string name, string subscript, int termsCount)
        {
            var vector =
                $"Subscript[{name},{subscript}1]".ToExpr() * geometricProcessor.CreateVector(0);

            for (var i = 2; i <= termsCount; i++)
                vector += $"Subscript[{name},{subscript}{i}]".ToExpr() * geometricProcessor.CreateVector(i - 1);

            return vector;
        }

        public static void RotationMatrixToSimpleRotationsSample(int n)
        {
            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;
            
            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var randomComposer =
                geometricProcessor.CreateXGaRandomComposer(10);

            var random = randomComposer.RandomGenerator;

            var rotationSequence = 
                LinFloat64VectorToVectorRotationSequence.Create(n);

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

                var angle = rotation.Angle.Degrees;

                var eigenValue = eigenValueArray[i];

                Console.WriteLine($"Rotation {i + 1}:");
                Console.WriteLine($"   Eigen Value: {eigenValue}");
                Console.WriteLine($"         Angle: {angle} degrees");
                Console.WriteLine($"             k: {kVectorRotated}");
                Console.WriteLine();
            }

            //Debug.Assert(
            //    (r1.RotateVector(u) - v).GetLengthSquared().IsNearZero()
            //);
        }

        public static void ClarkeMatrixToSimpleRotationsSample(int n)
        {
            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;
            
            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            var clarkeMap =
                geometricProcessor.CreateClarkeRotationMap(n);

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
                var angle = Math.Atan2(imagPairs[i].Item1, realPairs[i].Item1).RadiansToAngle();

                Console.WriteLine($"Eigen Value {i + 1}");
                Console.WriteLine($"Real part: {textComposer.GetScalarText(realPairs[i].Item1)}");
                Console.WriteLine($"Imag part: {textComposer.GetScalarText(imagPairs[i].Item1)}");
                Console.WriteLine();

                Console.WriteLine($"Eigen Vector {i + 1}");
                Console.WriteLine($"Real part: {textComposer.GetArrayText(realPairs[i].Item2)}");
                Console.WriteLine($"Imag part: {textComposer.GetArrayText(imagPairs[i].Item2)}");
                Console.WriteLine();

                Console.WriteLine($"Angle: {angle.Degrees} degrees");
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
                        geometricProcessor.CreateVector(realPairs[i].Item2),
                        geometricProcessor.CreateVector(imagPairs[i].Item2)
                    );

                var (angle, bivector) =
                    rotorsArray[i].GetEuclideanAngleBivector();

                Console.WriteLine($"Rotor {i + 1}:");
                Console.WriteLine($"      Angle: {angle.RadiansToDegrees()} degrees");
                Console.WriteLine($"   Bivector: {textComposer.GetMultivectorText(bivector)}");
                Console.WriteLine();
            }

            var rotorsSequence =
                XGaFloat64PureRotorsSequence.Create(rotorsArray);

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

                var u1 = vectorRotation.SourceVector;
                var u2 = geometricProcessor.CreateVector(u1);
            
                Debug.Assert(
                    (u1 - u2.VectorToLinVector()).GetVectorNormSquared().IsNearZero()
                );

                var v1 = vectorRotation.TargetVector;
                var v2 = geometricProcessor.CreateVector(v1);
            
                Debug.Assert(
                    (v1 - v2.VectorToLinVector()).GetVectorNormSquared().IsNearZero()
                );

                var angle1 = vectorRotation.Angle.Degrees;
                var bivector1 = v2.Op(u2);
                bivector1 /= bivector1.ENorm();

                Console.WriteLine($"Simple Vector Rotation {i + 1}:");
                Console.WriteLine($"      Angle: {angle1} degrees");
                Console.WriteLine($"   Bivector: {textComposer.GetMultivectorText(bivector1)}");
                Console.WriteLine();

                var rotor = rotorsSequence[i]; //u2.GetEuclideanRotorTo(v2);

                var (angle2, bivector2) = 
                    rotor.GetEuclideanAngleBivector();

                Console.WriteLine($"Rotor {i + 1}:");
                Console.WriteLine($"      Angle: {angle2.RadiansToDegrees()} degrees");
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
                    (v3 - v4.VectorToLinVector()).GetVectorNormSquared().IsNearZero()
                );
            }
        }

        public static void CirculantMatrixToSimpleRotationsSample(int n)
        {
            var scalarProcessor =
                ScalarProcessorExpr.DefaultProcessor;

            var geometricProcessor =
                XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

            var textComposer =
                TextComposerExpr.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerExpr.DefaultComposer;
        
            laTeXComposer.BasisName = @"\boldsymbol{e}";

            var assumeExpr1 =
                n.GetRange()
                    .Select(i => $"Subscript[c, {i}]")
                    .Concatenate(" | ");
        
            var assumeExpr =
                $@"Element[{assumeExpr1}, Reals]".ToExpr();

            MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            var zero = 
                geometricProcessor.CreateZeroScalar();

            var nSqrt = 
                $"Sqrt[{n}]".CreateScalar(scalarProcessor);
        
            var cArray =
                n.GetRange()
                    .Select(i => $"Subscript[c,{i}]".CreateScalar(scalarProcessor))
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
            
                eigenValueRealArray[j] = scalarProcessor.CreateScalar(
                    Mfs.Re[eigenValue.ScalarValue].FullSimplify()
                );

                eigenValueImagArray[j] = scalarProcessor.CreateScalar(
                    Mfs.Im[eigenValue.ScalarValue].FullSimplify()
                );

                var eigenVector = 
                    geometricProcessor.CreateVector(
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
                    eigenValueList.Add(eigenValue.Scalar);

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

                eigenValueList.Add(eigenValue.Scalar);

                var angle = scalarProcessor.ArcTan2(
                    eigenValueRealArray[j],
                    eigenValueImagArray[j]
                ).FullSimplify().CreateScalar(scalarProcessor);

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
                Console.WriteLine($"      Angle: ${laTeXComposer.GetScalarText(angle)}$");
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
                LinFloat64VectorToVectorRotationSequence.CreateRandomOrthogonal(random, n, 2);

            var matrix1 = rotation.ToMatrix(n, n);
            var matrix2 = matrix1.GetOutermorphismMatrix(2);
            var matrix3 = matrix1.GetOutermorphismMatrix(3);

            var x1 = random.GetFloat64Tuple(n);
            var x2 = random.GetFloat64Tuple(n);
            var x3 = random.GetFloat64Tuple(n);

            var x12 = metric.CreateVector(x1).Op(metric.CreateVector(x2));
            var x123 = x12.Op(metric.CreateVector(x3));

            var t1 = (matrix2 * x12.MultivectorToArray(n).ToVector()).ToArray().CreateTuple();
            var t2 = metric.CreateVector(matrix1.MapVector(x1)).Op(metric.CreateVector(matrix1.MapVector(x2))).MultivectorToArray(n).CreateTuple();

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

            var rotationSequence1 =
                matrix1.GetVectorToVectorRotationSequence();

            var rotationSequence2 =
                matrix2.GetVectorToVectorRotationSequence();

            var rotationSequence3 =
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
                    ScalarProcessorExpr.DefaultProcessor;

                var geometricProcessor =
                    XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

                var textComposer =
                    TextComposerExpr.DefaultComposer;

                var laTeXComposer =
                    LaTeXComposerExpr.DefaultComposer;

                var u =
                    geometricProcessor.CreateSymbolicVector("u", "", n);


                var v =
                    geometricProcessor.CreateSymbolicVector("u", "", n);


                var uvRotor =
                    u.CreatePureRotor(v);

                var uvRotorMatrix =
                    uvRotor.GetMultivectorMapArray(n, n);

                Console.WriteLine($@"$R_{{u,v}} = {laTeXComposer.GetMultivectorText(uvRotor.Multivector)}$");
                Console.WriteLine($@"$M_{{u,v}} = {laTeXComposer.GetArrayText(uvRotorMatrix)}$");
                Console.WriteLine();

                for (var k = 0; k < n; k++)
                {
                    var ep = geometricProcessor.CreateVector(k);
                    var en = -ep;

                    var uepRotor =
                        u.CreatePureRotor(ep);

                    var uepRotorMatrix =
                        uepRotor.GetMultivectorMapArray(n, n);

                    Console.WriteLine($@"$R_{{u,e_{{{k + 1}}}}} = {laTeXComposer.GetMultivectorText(uepRotor.Multivector)}$");
                    Console.WriteLine($@"$M_{{u,e_{{{k + 1}}}}} = {laTeXComposer.GetArrayText(uepRotorMatrix)}$");
                    Console.WriteLine();


                    var uenRotor =
                        u.CreatePureRotor(en);

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

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);


            var clarkeRotation = 
                LinFloat64MatrixRotation.CreateForwardClarkeRotation(n);

            var sequence =
                clarkeRotation.ToVectorToVectorRotationSequence();


            for (var j = 0; j < 100; j++)
            {
                var x =
                    random.GetVector(-10, 10).VectorToLinVector();

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

                var u = r.SourceVector;
                var v = r.TargetVector;

                Console.WriteLine($"Rotation {i}:");
                Console.WriteLine($"   u = {u}");
                Console.WriteLine($"   v = {v}");
                Console.WriteLine($"   Cos Angle = {r.AngleCos}");
                Console.WriteLine($"   Angle = {r.Angle}");
                Console.WriteLine();

                i++;
            }
        }
    
        public static void Example3()
        {
            const int n = 10;

            var random = new Random(10);
        
            var matrix =
                random.GetOrthogonalMatrix(n);

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
                    rotation.SourceVector;

                var targetVector = 
                    rotation.TargetVector;

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

                var u = rotation.SourceVector;
                var v = rotation.TargetVector;
                var uvSubspace = LinFloat64PlaneSubspace.CreateFromUnitVectors(u, v);
            
                var x = random.GetNumber() * u + random.GetNumber() * v;
                var y = rotationSequence.MapVector(x);
            
                Debug.Assert(
                    uvSubspace.NearContains(y) &&
                    (x.GetAngleCos(y) - u.GetAngleCos(v)).IsNearZero()
                );

                y = (rotationMatrix * x.ToVector(n)).CreateLinVector();

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
                var rotationSequence = LinFloat64VectorToVectorRotationSequence.CreateRandomOrthogonal(
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
                    var u = rotation.SourceVector;
                    var v = rotation.TargetVector;

                    var v1 = rotationSequence.MapVector(u);
                    var v2 = (rotationMatrix * u.ToVector(n)).CreateLinVector();

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
                        var x = random.GetFloat64Tuple(n).CreateLinVector();

                        var y1 = rotationSequence.MapVector(x);
                        var y2 = (rotationMatrix * x.ToVector(n)).CreateLinVector();
                
                        Debug.Assert(
                            (y1 - y2).GetVectorNormSquared().IsNearZero()
                        );
                    }
                }
            }

        
            for (var rotationCount = 1; rotationCount <= n; rotationCount++)
            {
                // Create a rotation sequence of 1 or more general rotations
                var rotationSequence = LinFloat64VectorToVectorRotationSequence.CreateRandom(
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
                    var x = random.GetFloat64Tuple(n).CreateLinVector();

                    var y1 = rotationSequence.MapVector(x);
                    var y2 = (rotationMatrix * x.ToVector(n)).CreateLinVector();
                
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

            var u = random.GetFloat64Tuple(n).CreateUnitLinVector();
            var v = random.GetFloat64Tuple(n).CreateUnitLinVector();

            var rotation = 
                LinFloat64VectorToVectorRotation.Create(u, v);

            for (var i = 0; i < 100; i++)
            {
                var x = random.GetFloat64Tuple(n).CreateLinVector();

                var y1 = rotation.MapVectorProjection(x);
                var y2 = rotation.MapVector(rotation.ProjectOnRotationPlane(x));

                Debug.Assert(
                    (y1 - y2).GetVectorNormSquared().IsNearZero()
                );
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

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            for (var j = 0; j < 10; j++)
            {
                var u =
                    random.GetVector(-1, 1).DivideByNorm();

                var v =
                    random.GetVector(-1, 1).DivideByNorm();

                var uvRotor =
                    u.GetEuclideanRotorTo(v);

                var uvVectorRotation =
                    LinFloat64VectorToVectorRotation.Create(
                        u.VectorToLinVector(),
                        v.VectorToLinVector()
                    );

                for (var axisIndex = 0; axisIndex < n; axisIndex++)
                {
                    var x =
                        geometricProcessor.CreateVector(axisIndex);

                    var y1 = uvRotor.OmMap(x).VectorToLinVector();
                    var y2 = uvVectorRotation.MapBasisVector(axisIndex);

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                }

                for (var i = 0; i < 100; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).VectorToLinVector();
                    var y2 = uvVectorRotation.MapVector(x.VectorToLinVector());

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );

                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).VectorToLinVector();
                    var z2 = uvVectorRotation.MapVector(x1.VectorToLinVector());
                    var z3 = uvVectorRotation.MapVectorProjection(x.VectorToLinVector());

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

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            for (var j = 0; j < 10; j++)
            {
                var uAxisIndex =
                    random.GetBasisVectorIndex();

                var u =
                    geometricProcessor.CreateVector(uAxisIndex);

                if (uAxisNegative)
                    u = -u;

                var v =
                    random.GetVector(-1, 1).DivideByNorm();

                var uvRotor =
                    u.GetEuclideanRotorTo(v);

                var uvVectorRotation =
                    new LinFloat64AxisToVectorRotation(
                        uAxisIndex,
                        uAxisNegative,
                        v.VectorToLinVector()
                    );

                for (var axisIndex = 0; axisIndex < n; axisIndex++)
                {
                    var x =
                        geometricProcessor.CreateVector(axisIndex);

                    var y1 = uvRotor.OmMap(x).VectorToLinVector();
                    var y2 = uvVectorRotation.MapBasisVector(axisIndex);

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                }

                for (var i = 0; i < 100; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).VectorToLinVector();
                    var y2 = uvVectorRotation.MapVector(x.VectorToLinVector());

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                
                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).VectorToLinVector();
                    var z2 = uvVectorRotation.MapVector(x1.VectorToLinVector());
                    var z3 = uvVectorRotation.MapVectorProjection(x.VectorToLinVector());

                    Debug.Assert(
                        (z1 - z2).GetVectorNorm().IsNearZero()
                    );

                    Debug.Assert(
                        (z1 - z3).GetVectorNorm().IsNearZero()
                    );
                }
            }
        }

        private static void ValidationExample3(bool vAxisNegative)
        {
            const int n = 10;

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            for (var j = 0; j < 10; j++)
            {
                var vAxisIndex =
                    random.GetBasisVectorIndex();

                var u =
                    random.GetVector(-1, 1).DivideByNorm();

                var v =
                    geometricProcessor.CreateVector(vAxisIndex);

                if (vAxisNegative)
                    v = -v;

                var uvRotor =
                    u.GetEuclideanRotorTo(v);

                var uvVectorRotation =
                    LinFloat64VectorToAxisRotation.Create(
                        u.VectorToLinVector(),
                        vAxisIndex,
                        vAxisNegative
                    );

                for (var axisIndex = 0; axisIndex < n; axisIndex++)
                {
                    var x =
                        geometricProcessor.CreateVector(axisIndex);

                    var y1 = uvRotor.OmMap(x).VectorToLinVector();
                    var y2 = uvVectorRotation.MapBasisVector(axisIndex);

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                }

                for (var i = 0; i < 100; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).VectorToLinVector();
                    var y2 = uvVectorRotation.MapVector(x.VectorToLinVector());

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                
                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).VectorToLinVector();
                    var z2 = uvVectorRotation.MapVector(x1.VectorToLinVector());
                    var z3 = uvVectorRotation.MapVectorProjection(x.VectorToLinVector());

                    Debug.Assert(
                        (z1 - z2).GetVectorNorm().IsNearZero()
                    );

                    Debug.Assert(
                        (z1 - z3).GetVectorNorm().IsNearZero()
                    );
                }
            }
        }

        private static void ValidationExample4(bool uAxisNegative, bool vAxisNegative)
        {
            const int n = 10;

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            for (var uAxisIndex = 0; uAxisIndex < n; uAxisIndex++)
            {
                var u =
                    geometricProcessor.CreateVector(uAxisIndex);

                if (uAxisNegative)
                    u = -u;

                for (var vAxisIndex = 0; vAxisIndex < n; vAxisIndex++)
                {
                    if (uAxisIndex == vAxisIndex) continue;

                    var v =
                        geometricProcessor.CreateVector(vAxisIndex);

                    if (vAxisNegative)
                        v = -v;

                    var uvRotor =
                        u.GetEuclideanRotorTo(v);

                    var uvVectorRotation =
                        new LinFloat64AxisToAxisRotation(
                            uAxisIndex,
                            uAxisNegative,
                            vAxisIndex,
                            vAxisNegative
                        );

                    for (var axisIndex = 0; axisIndex < n; axisIndex++)
                    {
                        var x =
                            geometricProcessor.CreateVector(axisIndex);

                        var y1 = uvRotor.OmMap(x).VectorToLinVector();
                        var y2 = uvVectorRotation.MapBasisVector(axisIndex);

                        Debug.Assert(
                            (y1 - y2).GetVectorNorm().IsNearZero()
                        );
                    }

                    for (var i = 0; i < n; i++)
                    {
                        var x =
                            random.GetVector(-1, 1);

                        var y1 = uvRotor.OmMap(x).VectorToLinVector();
                        var y2 = uvVectorRotation.MapVector(x.VectorToLinVector());

                        Debug.Assert(
                            (y1 - y2).GetVectorNorm().IsNearZero()
                        );
                    
                        var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                        var x1 = bv.ToSubspace().Project(x);

                        var z1 = uvRotor.OmMap(x1).VectorToLinVector();
                        var z2 = uvVectorRotation.MapVector(x1.VectorToLinVector());
                        var z3 = uvVectorRotation.MapVectorProjection(x.VectorToLinVector());

                        Debug.Assert(
                            (z1 - z2).GetVectorNorm().IsNearZero()
                        );

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

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            var axisArray = new[]
            {
                Axis3D.PositiveX,
                Axis3D.PositiveY,
                Axis3D.PositiveZ,
                Axis3D.NegativeX,
                Axis3D.NegativeY,
                Axis3D.NegativeZ
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
                    new VectorToVectorRotation3D(
                        u.GetTuple3D(),
                        v.GetTuple3D()
                    );

                foreach (var axisIndex in axisArray)
                {
                    var y = axisIndex.GetVector3D();

                    var x =
                        geometricProcessor.CreateVector(y.X, y.Y, y.Z);

                    var y1 = uvRotor.OmMap(x).GetTuple3D();
                    var y2 = uvVectorRotation.MapAxis(axisIndex);

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                }

                for (var i = 0; i < 100; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).GetTuple3D();
                    var y2 = uvVectorRotation.MapVector(x.GetTuple3D());

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                
                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).GetTuple3D();
                    var z2 = uvVectorRotation.MapVector(x1.GetTuple3D());
                    var z3 = uvVectorRotation.MapVectorProjection(x.GetTuple3D());

                    Debug.Assert(
                        (z1 - z2).GetVectorNorm().IsNearZero()
                    );

                    Debug.Assert(
                        (z1 - z3).GetVectorNorm().IsNearZero()
                    );
                }
            }
        }

        private static void ValidationExample6(bool uAxisNegative)
        {
            const int n = 3;

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            var axisArray = new[]
            {
                Axis3D.PositiveX,
                Axis3D.PositiveY,
                Axis3D.PositiveZ,
                Axis3D.NegativeX,
                Axis3D.NegativeY,
                Axis3D.NegativeZ
            };

            for (var j = 0; j < 10; j++)
            {
                var uAxisIndex =
                    random.GetBasisVectorIndex();

                var uAxis = uAxisIndex switch
                {
                    0 => uAxisNegative ? Axis3D.NegativeX : Axis3D.PositiveX,
                    1 => uAxisNegative ? Axis3D.NegativeY : Axis3D.PositiveY,
                    2 => uAxisNegative ? Axis3D.NegativeZ : Axis3D.PositiveZ,
                    _ => throw new NotImplementedException()
                };

                var u =
                    geometricProcessor.CreateVector(uAxisIndex);

                if (uAxisNegative)
                    u = -u;

                var v =
                    random.GetVector(-1, 1).DivideByNorm();

                var uvRotor =
                    u.GetEuclideanRotorTo(v);

                var uvVectorRotation =
                    new AxisToVectorRotation3D(
                        uAxis,
                        v.GetTuple3D()
                    );

                foreach (var axisIndex in axisArray)
                {
                    var y = axisIndex.GetVector3D();

                    var x =
                        geometricProcessor.CreateVector(y.X, y.Y, y.Z);

                    var y1 = uvRotor.OmMap(x).GetTuple3D();
                    var y2 = uvVectorRotation.MapAxis(axisIndex);

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                }

                for (var i = 0; i < 100; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).GetTuple3D();
                    var y2 = uvVectorRotation.MapVector(x.GetTuple3D());

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                
                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).GetTuple3D();
                    var z2 = uvVectorRotation.MapVector(x1.GetTuple3D());
                    var z3 = uvVectorRotation.MapVectorProjection(x.GetTuple3D());

                    Debug.Assert(
                        (z1 - z2).GetVectorNorm().IsNearZero()
                    );

                    Debug.Assert(
                        (z1 - z3).GetVectorNorm().IsNearZero()
                    );
                }
            }
        }

        private static void ValidationExample7(bool vAxisNegative)
        {
            const int n = 3;

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            var axisArray = new[]
            {
                Axis3D.PositiveX,
                Axis3D.PositiveY,
                Axis3D.PositiveZ,
                Axis3D.NegativeX,
                Axis3D.NegativeY,
                Axis3D.NegativeZ
            };

            for (var j = 0; j < 10; j++)
            {
                var u =
                    random.GetVector(-1, 1).DivideByNorm();

                var vAxisIndex =
                    random.GetBasisVectorIndex();

                var vAxis = vAxisIndex switch
                {
                    0 => vAxisNegative ? Axis3D.NegativeX : Axis3D.PositiveX,
                    1 => vAxisNegative ? Axis3D.NegativeY : Axis3D.PositiveY,
                    2 => vAxisNegative ? Axis3D.NegativeZ : Axis3D.PositiveZ,
                    _ => throw new NotImplementedException()
                };

                var v =
                    geometricProcessor.CreateVector(vAxisIndex);

                if (vAxisNegative)
                    v = -v;

                var uvRotor =
                    u.GetEuclideanRotorTo(v);

                var uvVectorRotation =
                    new VectorToAxisRotation3D(
                        u.GetTuple3D(),
                        vAxis
                    );

                foreach (var axisIndex in axisArray)
                {
                    var y = axisIndex.GetVector3D();

                    var x =
                        geometricProcessor.CreateVector(y.X, y.Y, y.Z);

                    var y1 = uvRotor.OmMap(x).GetTuple3D();
                    var y2 = uvVectorRotation.MapAxis(axisIndex);

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                }

                for (var i = 0; i < 100; i++)
                {
                    var x =
                        random.GetVector(-1, 1);

                    var y1 = uvRotor.OmMap(x).GetTuple3D();
                    var y2 = uvVectorRotation.MapVector(x.GetTuple3D());

                    Debug.Assert(
                        (y1 - y2).GetVectorNorm().IsNearZero()
                    );
                
                    var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                    var x1 = bv.ToSubspace().Project(x);

                    var z1 = uvRotor.OmMap(x1).GetTuple3D();
                    var z2 = uvVectorRotation.MapVector(x1.GetTuple3D());
                    var z3 = uvVectorRotation.MapVectorProjection(x.GetTuple3D());

                    Debug.Assert(
                        (z1 - z2).GetVectorNorm().IsNearZero()
                    );

                    Debug.Assert(
                        (z1 - z3).GetVectorNorm().IsNearZero()
                    );
                }
            }
        }

        private static void ValidationExample8(bool uAxisNegative, bool vAxisNegative)
        {
            const int n = 3;

            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            var axisArray = new[]
            {
                Axis3D.PositiveX,
                Axis3D.PositiveY,
                Axis3D.PositiveZ,
                Axis3D.NegativeX,
                Axis3D.NegativeY,
                Axis3D.NegativeZ
            };

            for (var uAxisIndex = 0; uAxisIndex < n; uAxisIndex++)
            {
                var uAxis = uAxisIndex switch
                {
                    0 => uAxisNegative ? Axis3D.NegativeX : Axis3D.PositiveX,
                    1 => uAxisNegative ? Axis3D.NegativeY : Axis3D.PositiveY,
                    2 => uAxisNegative ? Axis3D.NegativeZ : Axis3D.PositiveZ,
                    _ => throw new NotImplementedException()
                };

                var u =
                    geometricProcessor.CreateVector(uAxisIndex);

                if (uAxisNegative)
                    u = -u;

                for (var vAxisIndex = 0; vAxisIndex < n; vAxisIndex++)
                {
                    if (uAxisIndex == vAxisIndex) continue;

                    var vAxis = vAxisIndex switch
                    {
                        0 => vAxisNegative ? Axis3D.NegativeX : Axis3D.PositiveX,
                        1 => vAxisNegative ? Axis3D.NegativeY : Axis3D.PositiveY,
                        2 => vAxisNegative ? Axis3D.NegativeZ : Axis3D.PositiveZ,
                        _ => throw new NotImplementedException()
                    };

                    var v =
                        geometricProcessor.CreateVector(vAxisIndex);

                    if (vAxisNegative)
                        v = -v;

                    var uvRotor =
                        u.GetEuclideanRotorTo(v);

                    var uvVectorRotation =
                        new AxisToAxisRotation3D(
                            uAxis,
                            vAxis
                        );

                    foreach (var axis in axisArray)
                    {
                        var y = axis.GetVector3D();

                        var x =
                            geometricProcessor.CreateVector(y.X, y.Y, y.Z);

                        var y1 = uvRotor.OmMap(x).GetTuple3D();
                        var y2 = uvVectorRotation.MapAxis(axis);

                        Debug.Assert(
                            (y1 - y2).GetVectorNorm().IsNearZero()
                        );
                    }

                    for (var i = 0; i < n; i++)
                    {
                        var x =
                            random.GetVector(-1, 1);

                        var y1 = uvRotor.OmMap(x).GetTuple3D();
                        var y2 = uvVectorRotation.MapVector(x.GetTuple3D());

                        Debug.Assert(
                            (y1 - y2).GetVectorNorm().IsNearZero()
                        );
                    
                        var (_, bv) = uvRotor.GetEuclideanAngleBivector();

                        var x1 = bv.ToSubspace().Project(x);

                        var z1 = uvRotor.OmMap(x1).GetTuple3D();
                        var z2 = uvVectorRotation.MapVector(x1.GetTuple3D());
                        var z3 = uvVectorRotation.MapVectorProjection(x.GetTuple3D());

                        Debug.Assert(
                            (z1 - z2).GetVectorNorm().IsNearZero()
                        );

                        Debug.Assert(
                            (z1 - z3).GetVectorNorm().IsNearZero()
                        );
                    }
                }
            }
        }

        private static void ValidationExample9()
        {
            const int n = 10;
            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            var u =
                random.GetVector(-1, 1).DivideByNorm();

            var v =
                random.GetVector(-1, 1).DivideByNorm();

            var uvRotation =
                LinFloat64VectorToVectorRotation.Create(
                    u.VectorToLinVector(),
                    v.VectorToLinVector()
                );

            var uvRotationSequence = 
                uvRotation.ToMatrix(n, n).GetVectorToVectorRotationSequence();

            for (var axisIndex = 0; axisIndex < n; axisIndex++)
            {
                //var x =
                //    geometricProcessor.CreateVector(axisIndex);

                var y1 = uvRotation.MapBasisVector(axisIndex);
                var y2 = uvRotationSequence.MapBasisVector(axisIndex);

                Debug.Assert(
                    (y1 - y2).GetVectorNorm().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1).VectorToLinVector();

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
            var scalarProcessor =
                ScalarProcessorFloat64.DefaultProcessor;

            var geometricProcessor =
                XGaFloat64Processor.Euclidean;

            var textComposer =
                TextComposerFloat64.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerFloat64.DefaultComposer;

            var random =
                geometricProcessor.CreateXGaRandomComposer(10);

            var scaling = LinFloat64VectorDirectionalScaling.Create(
                random.GetScalarValue(-10, 10),
                random.RandomGenerator.GetFloat64Tuple(n).CreateUnitLinVector()
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

                var y1 = (matrix * x.ToVector(n)).CreateLinVector();
                var y2 = mapSequence.MapBasisVector(axisIndex);

                Debug.Assert(
                    (y1 - y2).GetVectorNormSquared().IsNearZero()
                );
            }

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1).VectorToLinVector();

                var y1 = matrix.ToArray().MatrixProduct(x.ToArray(n)).CreateLinVector();
                var y2 = mapSequence.MapVector(x);

                Debug.Assert(
                    (y1 - y2).GetVectorNormSquared().IsNearZero()
                );
            }
        }
    }
}