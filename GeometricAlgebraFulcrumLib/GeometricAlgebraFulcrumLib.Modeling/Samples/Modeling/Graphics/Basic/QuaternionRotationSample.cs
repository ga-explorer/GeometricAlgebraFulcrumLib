﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.Basic;

public static class QuaternionRotationSample
{
    /// <summary>
    /// Generate switch cases for axis-to-axis rotation quaternions
    /// </summary>
    public static void Example1()
    {
        var axisList = new[]
        {
            LinBasisVector3D.Px,
            LinBasisVector3D.Py,
            LinBasisVector3D.Pz,
            LinBasisVector3D.Nx,
            LinBasisVector3D.Ny,
            LinBasisVector3D.Nz
        };

        var axisNameList = new[]
        {
            "Axis3D.Px",
            "Axis3D.Py",
            "Axis3D.Pz",
            "Axis3D.Nx",
            "Axis3D.Ny",
            "Axis3D.Nz"
        };

        var composer = new LinearTextComposer();

        composer
            .AppendLine("return axis1 switch")
            .AppendLine("{")
            .IncreaseIndentation();

        for (var i1 = 0; i1 < 6; i1++)
        {
            var axis1 = axisList[i1];
            var axisName1 = axisNameList[i1];
            var axisVector1 = axis1.ToLinVector3D();

            composer
                .AppendLineAtNewLine($"{axisName1} => axis2 switch")
                .AppendLine("{")
                .IncreaseIndentation();

            for (var i2 = 0; i2 < 6; i2++)
            {
                var axis2 = axisList[i2];
                var axisName2 = axisNameList[i2];
                var axisVector2 = axis2.ToLinVector3D();

                if (axis1 == axis2)
                {
                    composer.AppendLineAtNewLine($"{axisName2} => Tuple4D.QuaternionIdentity,");
                }
                else if (axis1.IsOppositeTo(axis2))
                {
                    var rotationAxis = axis1 switch
                    {
                        LinBasisVector3D.Px or LinBasisVector3D.Nx => LinFloat64Vector3D.E3,
                        LinBasisVector3D.Py or LinBasisVector3D.Ny => LinFloat64Vector3D.E1,
                        _ => LinFloat64Vector3D.E2
                    };

                    var q = rotationAxis.RotationAxisAngleToQuaternion(LinFloat64PolarAngle.Angle180);

                    composer
                        .AppendLineAtNewLine($"{axisName2} => new Tuple4D({q.ScalarI}, {q.ScalarJ}, {q.ScalarK}, {q.Scalar}),");
                }
                else
                {
                    var v3 = axisVector1.VectorCross(axisVector2);
                    var rotationAxis = LinFloat64Vector3D.Create(v3.X, v3.Y, v3.Z);

                    var q = rotationAxis.RotationAxisAngleToQuaternion(LinFloat64PolarAngle.Angle90);

                    composer
                        .AppendLineAtNewLine($"{axisName2} => new Tuple4D({q.ScalarI}, {q.ScalarJ}, {q.ScalarK}, {q.Scalar}),");
                }
            }

            composer
                .DecreaseIndentation()
                .AppendLineAtNewLine("},");
        }

        composer
            .DecreaseIndentation()
            .AppendLineAtNewLine("};");

        Console.WriteLine(composer.ToString());
    }

    /// <summary>
    /// Validate axis-to-axis rotation quaternions
    /// </summary>
    public static void Example2()
    {
        var axisList = new[]
        {
            LinBasisVector3D.Px,
            LinBasisVector3D.Py,
            LinBasisVector3D.Pz,
            LinBasisVector3D.Nx,
            LinBasisVector3D.Ny,
            LinBasisVector3D.Nz
        };

        for (var i1 = 0; i1 < 6; i1++)
        {
            var axis1 = axisList[i1];

            for (var i2 = 0; i2 < 6; i2++)
            {
                var axis2 = axisList[i2];

                var quaternion = axis1.VectorToVectorRotationQuaternion(axis2);

                var v2 = quaternion.RotateVector(axis1).MapComponents(
                    s => Math.Round(s, 3)
                );

                if ((v2 - axis2.ToLinVector3D()).VectorENorm().IsNearZero(1e-7))
                    continue;

                Console.WriteLine($"              Axis1: {axis1}");
                Console.WriteLine($"              Axis2: {axis2}");
                Console.WriteLine($"Computed Quaternion: {quaternion}");
                Console.WriteLine($"     Computed Axis2: {v2}");
                Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// Validate nearest axis-to-vector rotation quaternions
    /// </summary>
    public static void Example3()
    {
        var k = LinFloat64Vector3D.Create(1, 1, 1);
        var vectorList = new[]
        {
            (LinBasisVector3D.Px.ToLinVector3D() + k).ToUnitLinVector3D(),
            (LinBasisVector3D.Py.ToLinVector3D() + k).ToUnitLinVector3D(),
            (LinBasisVector3D.Pz.ToLinVector3D() + k).ToUnitLinVector3D(),
            (LinBasisVector3D.Nx.ToLinVector3D() - k).ToUnitLinVector3D(),
            (LinBasisVector3D.Ny.ToLinVector3D() - k).ToUnitLinVector3D(),
            (LinBasisVector3D.Nz.ToLinVector3D() - k).ToUnitLinVector3D()
        };

        for (var i1 = 0; i1 < 6; i1++)
        {
            var vector = vectorList[i1];

            var (axis, quaternion) =
                vector.NearestBasisToVectorRotationQuaternion();

            var axisVector =
                axis.ToLinVector3D();

            var (u, a) =
                axisVector.VectorToVectorRotationAxisAngle(vector);

            var q = u.RotationAxisAngleToQuaternion(a);

            if ((quaternion - q).IsNearZero())
                continue;

            var v2 = quaternion.RotateVector(axis);
            var v3 = q.RotateVector(axis);

            Console.WriteLine($"             Vector: {vector}");
            Console.WriteLine($"               Axis: {axis}");
            Console.WriteLine($"  Actual Quaternion: {q}");
            Console.WriteLine($"Computed Quaternion: {quaternion}");
            Console.WriteLine($"  Actual Quaternion Rotated Vector: {v3}");
            Console.WriteLine($"Computed Quaternion Rotated Vector: {v2}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Validate axis-to-vector rotation quaternions
    /// </summary>
    public static void Example4()
    {
        var axisList = new[]
        {
            LinBasisVector3D.Px,
            LinBasisVector3D.Py,
            LinBasisVector3D.Pz,
            LinBasisVector3D.Nx,
            LinBasisVector3D.Ny,
            LinBasisVector3D.Nz
        };

        var k = LinFloat64Vector3D.Create(1, 1, 1);
        var vectorList = new[]
        {
            (LinBasisVector3D.Px.ToLinVector3D() + k).ToUnitLinVector3D(),
            (LinBasisVector3D.Py.ToLinVector3D() + k).ToUnitLinVector3D(),
            (LinBasisVector3D.Pz.ToLinVector3D() + k).ToUnitLinVector3D(),
            (LinBasisVector3D.Nx.ToLinVector3D() - k).ToUnitLinVector3D(),
            (LinBasisVector3D.Ny.ToLinVector3D() - k).ToUnitLinVector3D(),
            (LinBasisVector3D.Nz.ToLinVector3D() - k).ToUnitLinVector3D()
        };

        for (var i1 = 0; i1 < 6; i1++)
        {
            var vector = vectorList[i1];

            for (var i2 = 0; i2 < 6; i2++)
            {
                var axis = axisList[i2];
                var axisVector =
                    axis.ToLinVector3D();

                var quaternion =
                    axis.VectorToVectorRotationQuaternion(vector);

                var (u, a) =
                    axisVector.VectorToVectorRotationAxisAngle(vector);

                var q = u.RotationAxisAngleToQuaternion(a);

                var v1 = quaternion.RotateVector(axis);
                var v2 = q.RotateVector(axis);

                var l1 = (v1 - vector).VectorENormSquared();
                var l2 = (v2 - vector).VectorENormSquared();

                var qDiff = (quaternion - q).NormSquared();

                if (l1.IsNearZero() && l2.IsNearZero() && qDiff.IsNearZero())
                    continue;

                var nearestAxisVector = vector.SelectNearestBasisVector();

                Console.WriteLine($"              Vector: {vector}");
                Console.WriteLine($"                Axis: {axis}");
                Console.WriteLine($"        Nearest Axis: {nearestAxisVector}");
                Console.WriteLine($"   Actual Quaternion: {q}");
                Console.WriteLine($" Computed Quaternion: {quaternion}");
                Console.WriteLine($"   Actual Quaternion Rotated Axis: {v2}");
                Console.WriteLine($" Computed Quaternion Rotated Axis: {v1}");
                Console.WriteLine();
            }
        }
    }
}