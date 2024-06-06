using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.LinearAlgebra;

public static class OrthogonalLinearMapSequenceSamples
{
    public static void Example1()
    {
        const int n = 10;

        var random = new Random(10);

        var scaling =
            random.GetVectorDirectionalScaling(n, -10, 10);

        var scalingFactor =
            scaling.ScalingFactor;

        var scalingVector =
            scaling.ScalingVector;

        var y =
            scaling.MapVector(scalingVector);

        Debug.Assert(
            (y - scalingFactor * scalingVector).IsNearZero()
        );

        var scalingMatrix =
            scaling.ToMatrix(n, n);

        var subspaceList =
            scalingMatrix.GetSimpleEigenSubspaces();

        Console.WriteLine($"Scaling Factor: {scalingFactor}");
        Console.WriteLine($"Scaling Vector: {scalingVector}");
        Console.WriteLine();

        var i = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {i}:");
            Console.WriteLine(subspace);

            i++;
        }
    }

    public static void Example2()
    {
        const int n = 10;

        var random = new Random(10);

        var scalingFactorList =
            random
                .GetNumbers(n / 2, -5, 5)
                .ToArray();

        var scalingVectorList =
            random
                .GetMathNetOrthonormalVectors(n, n / 2)
                .Select(v => v.ToArray().CreateLinVector())
                .ToArray();

        var scalingSequence =
            LinFloat64VectorDirectionalScalingSequence.Create(n);

        for (var k = 0; k < n / 2; k++)
        {
            var scaling =
                LinFloat64VectorDirectionalScaling.Create(
                    scalingFactorList[k],
                    scalingVectorList[k]
                );

            scalingSequence.AppendMap(scaling);

            var scalingFactor =
                scaling.ScalingFactor;

            var scalingVector =
                scaling.ScalingVector;

            Console.WriteLine($"Scaling {k + 1}:");
            Console.WriteLine($"Scaling Factor: {scalingFactor}");
            Console.WriteLine($"Scaling Vector: {scalingVector}");
            Console.WriteLine();
        }

        //var y = 
        //    scaling.MapVector(scalingVector);

        //Debug.Assert(
        //    (y - scalingFactor * scalingVector).IsNearZero()
        //);

        var scalingMatrix =
            scalingSequence.ToMatrix(n, n);

        for (var k = 0; k < n / 2; k++)
        {
            var s = scalingSequence[k].ScalingFactor;
            var x = scalingSequence[k].ScalingVector;
            var y = scalingSequence.MapVector(x);

            Debug.Assert(
                (y - s * x).GetVectorNormSquared().IsNearZero()
            );

            y = (scalingMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

            Debug.Assert(
                (y - s * x).GetVectorNormSquared().IsNearZero()
            );
        }

        var subspaceList =
            scalingMatrix.GetSimpleEigenSubspaces();

        var i = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {i}:");
            Console.WriteLine(subspace);

            i++;
        }
    }

    public static void Example3()
    {
        const int n = 3;

        var random = new Random(10);

        //var scalingFactorList =
        //    random
        //        .GetNumbers(n / 2, -5, 5)
        //        .ToArray();

        //var scalingVectorList =
        //    random
        //        .GetOrthonormalVectors(n, n / 2)
        //        .Select(v => v.ToArray().CreateTuple())
        //        .ToArray();

        //var scalingSequence = 
        //    VectorDirectionalScalingSequence.Create(n);

        var matrix =
            Float64ArrayUtils.CreateClarkeRotationArray(n).ToMatrix();
        //random.GetOrthogonalMatrix(n);
        //random.GetFloat64Array2D(n, n).ToMatrix();
        //random.GetCirculantColumnArray(n).ToMatrix();

        var sysExpr =
            matrix.ToComplex().Evd();

        var eigenPairCount = sysExpr.EigenValues.Count;

        for (var j = 0; j < eigenPairCount; j++)
        {
            var eigenValue = sysExpr.EigenValues[j];
            var eigenVector = sysExpr.EigenVectors.Column(j);

            var subspace = new LinFloat64SimpleEigenSubspace(
                eigenValue,
                eigenVector
            );

            Console.WriteLine($"Subspace {j + 1}:");
            Console.WriteLine($"Eigen Vector Real Part: {eigenVector.Real().ToVector()}");
            Console.WriteLine($"Eigen Vector Imag Part: {eigenVector.Imaginary().ToVector()}");
            Console.WriteLine(subspace);
        }

        var mapSequence =
            matrix.GetBasicLinearMapSequence();

        Debug.Assert(
            mapSequence.IsNearOrthogonalMapsSequence()
        );

        var mapSequenceMatrix =
            mapSequence.ToMatrix(n, n);

        //var mapSequenceMatrix1 = 
        //    mapSequence.GetMatrix().GetBasicLinearMapSequence().GetMatrix();

        Console.WriteLine("Original Matrix:");
        Console.WriteLine(matrix.ToMatrixString());
        Console.WriteLine();

        Console.WriteLine("Mapping Matrix:");
        Console.WriteLine(mapSequenceMatrix.ToMatrixString());
        Console.WriteLine();

        //Console.WriteLine("Mapping Matrix 1:");
        //Console.WriteLine(mapSequenceMatrix1.ToMatrixString());
        //Console.WriteLine();

        //Debug.Assert(
        //    (mapSequenceMatrix1 - mapSequenceMatrix).L2Norm().IsNearZero()
        //);

        Debug.Assert(
            (matrix - mapSequenceMatrix).L2Norm().IsNearZero()
        );

        for (var k = 0; k < mapSequence.RotationSequence.Count; k++)
        {
            var rotation = mapSequence.RotationSequence[k];

            var sourceVector =
                rotation.BasisVector1;

            var targetVector =
                rotation.MapBasisVector1();

            Console.WriteLine($"Rotation {k + 1}:");
            Console.WriteLine($"Source Vector: {sourceVector}");
            Console.WriteLine($"Target Vector: {targetVector}");
            Console.WriteLine();
        }

        for (var k = 0; k < mapSequence.ReflectionSequence.Count; k++)
        {
            var reflection = mapSequence.ReflectionSequence[k];

            var normalVector =
                reflection.ReflectionNormal;

            Console.WriteLine($"Reflection {k + 1}:");
            Console.WriteLine($"Normal Vector: {normalVector}");
            Console.WriteLine();
        }

        var subspaceList =
            matrix.GetSimpleEigenSubspaces();

        var i = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {i}:");
            Console.WriteLine(subspace);

            i++;
        }
    }

    /// <summary>
    /// Validate the rotation and reflection properties of arbitrary
    /// orthogonal linear map sequences
    /// </summary>
    public static void Example4()
    {
        const int n = 8;

        var random = new Random(10);

        for (var mapCount = 1; mapCount <= n; mapCount++)
        {
            // Create a sequence of 1 or more orthogonal maps
            var mapSequence = LinFloat64OrthogonalLinearMapSequence.CreateRandomOrthogonal(
                random,
                n,
                mapCount
            );

            var mapMatrix =
                mapSequence.ToMatrix(n, n);

            // Make sure the result is actually an orthogonal matrix
            Debug.Assert(
                mapMatrix.Determinant().Abs().IsNearOne(1e-7)
            );

            // Make sure the sequence contains only pair-wise orthogonal rotations
            Debug.Assert(
                mapSequence.IsNearOrthogonalMapsSequence()
            );

            var subspaceList =
                mapMatrix.GetSimpleEigenSubspaces();

            Console.WriteLine($"Orthogonal maps number: {mapCount}");

            var j = 1;
            foreach (var subspace in subspaceList)
            {
                Console.WriteLine($"Subspace {j++}");
                Console.WriteLine(subspace);
            }

            foreach (var rotation in mapSequence.RotationSequence)
            {
                var u = rotation.BasisVector1;
                var v = rotation.MapBasisVector1();

                var v1 = mapSequence.MapVector(u);
                var v2 = (mapMatrix * MathNetNumericsUtils.ToMathNetVector(u, n)).CreateLinVector();

                // Make sure each rotation is performed independently from the other maps
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
                // map sequence computations
                for (var i = 0; i < 100; i++)
                {
                    var x = random.GetLinVector(n).CreateLinVector();

                    var y1 = mapSequence.MapVector(x);
                    var y2 = (mapMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

                    Debug.Assert(
                        (y1 - y2).GetVectorNormSquared().IsNearZero()
                    );
                }
            }

            foreach (var reflection in mapSequence.ReflectionSequence)
            {
                var u = reflection.ReflectionNormal;
                var v = -reflection.ReflectionNormal;

                var v1 = mapSequence.MapVector(u);
                var v2 = (mapMatrix * MathNetNumericsUtils.ToMathNetVector(u, n)).CreateLinVector();

                // Make sure each reflection is performed independently from the other maps
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
                // map sequence computations
                for (var i = 0; i < 100; i++)
                {
                    var x = random.GetLinVector(n).CreateLinVector();

                    var y1 = mapSequence.MapVector(x);
                    var y2 = (mapMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

                    Debug.Assert(
                        (y1 - y2).GetVectorNormSquared().IsNearZero()
                    );
                }
            }
        }


        for (var mapCount = 1; mapCount <= 2 * n; mapCount++)
        {
            // Create a rotation sequence of 1 or more general rotations
            var mapSequence = LinFloat64OrthogonalLinearMapSequence.CreateRandom(
                random,
                n,
                mapCount
            );

            var mapMatrix =
                mapSequence.ToMatrix(n, n);

            // Make sure the result is actually an orthogonal matrix
            Debug.Assert(
                mapMatrix.Determinant().Abs().IsNearOne(1e-7)
            );

            var subspaceList =
                mapMatrix.GetSimpleEigenSubspaces();

            Console.WriteLine($"General maps number: {mapCount}");

            var j = 1;
            foreach (var subspace in subspaceList)
            {
                Console.WriteLine($"Subspace {j++}");
                Console.WriteLine(subspace);
            }

            // Make sure matrix multiplication is the same as
            // map sequence computations
            for (var i = 0; i < 100; i++)
            {
                var x = random.GetLinVector(n).CreateLinVector();

                var y1 = mapSequence.MapVector(x);
                var y2 = (mapMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

                Debug.Assert(
                    (y1 - y2).GetVectorNormSquared().IsNearZero()
                );
            }
        }
    }

    public static void Example5()
    {
        const int n = 3;

        var random = new Random(10);

        //var scalingFactorList =
        //    random
        //        .GetNumbers(n / 2, -5, 5)
        //        .ToArray();

        //var scalingVectorList =
        //    random
        //        .GetOrthonormalVectors(n, n / 2)
        //        .Select(v => v.ToArray().CreateTuple())
        //        .ToArray();

        //var scalingSequence = 
        //    VectorDirectionalScalingSequence.Create(n);

        var matrix =
            //Float64ArrayUtils.CreateClarkeRotationArray(n).ToMatrix();
            //random.GetOrthogonalMatrix(n);
            random.GetFloat64Array2D(n, n).ToMatrix();
        //random.GetCirculantColumnArray(n).ToMatrix();

        var qr = matrix.QR();
        var matrixQ = qr.Q;
        var matrixR = qr.R;

        var sysExpr =
            matrix.ToComplex().Evd();

        var eigenPairCount =
            sysExpr.EigenValues.Count;

        for (var j = 0; j < eigenPairCount; j++)
        {
            var eigenValue = sysExpr.EigenValues[j];
            var eigenVector = sysExpr.EigenVectors.Column(j);

            var subspace = new LinFloat64SimpleEigenSubspace(
                eigenValue,
                eigenVector
            );

            Console.WriteLine($"Subspace {j + 1}:");
            Console.WriteLine($"Eigen Vector Real Part: {eigenVector.Real().ToVector()}");
            Console.WriteLine($"Eigen Vector Imag Part: {eigenVector.Imaginary().ToVector()}");
            Console.WriteLine(subspace);
        }

        var mapSequenceQ =
            matrixQ.GetBasicLinearMapSequence();

        var mapSequenceR =
            matrixR.GetBasicLinearMapSequence();

        Debug.Assert(
            mapSequenceQ.IsNearOrthogonalMapsSequence()
        );

        Debug.Assert(
            mapSequenceR.IsNearOrthogonalMapsSequence()
        );

        var mapSequenceMatrixQ =
            mapSequenceQ.ToMatrix(n, n);

        var mapSequenceMatrixR =
            mapSequenceR.ToMatrix(n, n);

        Console.WriteLine("Original Matrix:");
        Console.WriteLine(matrix.ToMatrixString());
        Console.WriteLine();

        Console.WriteLine("Original Matrix Q:");
        Console.WriteLine(matrixQ.ToMatrixString());
        Console.WriteLine();

        Console.WriteLine("Original Matrix R:");
        Console.WriteLine(matrixR.ToMatrixString());
        Console.WriteLine();

        Console.WriteLine("Mapping Matrix Q:");
        Console.WriteLine(mapSequenceMatrixQ.ToMatrixString());
        Console.WriteLine();

        Console.WriteLine("Mapping Matrix R:");
        Console.WriteLine(mapSequenceMatrixR.ToMatrixString());
        Console.WriteLine();

        //Console.WriteLine("Mapping Matrix 1:");
        //Console.WriteLine(mapSequenceMatrix1.ToMatrixString());
        //Console.WriteLine();

        //Debug.Assert(
        //    (mapSequenceMatrix1 - mapSequenceMatrix).L2Norm().IsNearZero()
        //);

        Debug.Assert(
            (matrixQ - mapSequenceMatrixQ).L2Norm().IsNearZero()
        );

        Debug.Assert(
            (matrixR - mapSequenceMatrixR).L2Norm().IsNearZero()
        );

        for (var k = 0; k < mapSequenceQ.RotationSequence.Count; k++)
        {
            var rotation = mapSequenceQ.RotationSequence[k];

            var sourceVector =
                rotation.BasisVector1;

            var targetVector =
                rotation.MapBasisVector1();

            Console.WriteLine($"Rotation {k + 1}:");
            Console.WriteLine($"Source Vector: {sourceVector}");
            Console.WriteLine($"Target Vector: {targetVector}");
            Console.WriteLine();
        }

        for (var k = 0; k < mapSequenceQ.ReflectionSequence.Count; k++)
        {
            var reflection = mapSequenceQ.ReflectionSequence[k];

            var normalVector =
                reflection.ReflectionNormal;

            Console.WriteLine($"Reflection {k + 1}:");
            Console.WriteLine($"Normal Vector: {normalVector}");
            Console.WriteLine();
        }


        for (var k = 0; k < mapSequenceR.RotationSequence.Count; k++)
        {
            var rotation = mapSequenceR.RotationSequence[k];

            var sourceVector =
                rotation.BasisVector1;

            var targetVector =
                rotation.MapBasisVector1();

            Console.WriteLine($"Rotation {k + 1}:");
            Console.WriteLine($"Source Vector: {sourceVector}");
            Console.WriteLine($"Target Vector: {targetVector}");
            Console.WriteLine();
        }

        for (var k = 0; k < mapSequenceR.ReflectionSequence.Count; k++)
        {
            var reflection = mapSequenceR.ReflectionSequence[k];

            var normalVector =
                reflection.ReflectionNormal;

            Console.WriteLine($"Reflection {k + 1}:");
            Console.WriteLine($"Normal Vector: {normalVector}");
            Console.WriteLine();
        }

        //var subspaceList = 
        //    matrix.GetSimpleEigenSubspaces();

        //var i = 1;
        //foreach (var subspace in subspaceList)
        //{
        //    Console.WriteLine($"Subspace {i}:");
        //    Console.WriteLine(subspace);

        //    i++;
        //}
    }

}