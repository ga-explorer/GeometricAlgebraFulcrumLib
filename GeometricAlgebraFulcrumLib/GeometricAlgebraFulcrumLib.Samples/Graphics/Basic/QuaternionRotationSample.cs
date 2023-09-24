using System;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.Basic
{
    public static class QuaternionRotationSample
    {
        /// <summary>
        /// Generate switch cases for axis-to-axis rotation quaternions
        /// </summary>
        public static void Example1()
        {
            var axisList = new []
            {
                LinUnitBasisVector3D.PositiveX,
                LinUnitBasisVector3D.PositiveY,
                LinUnitBasisVector3D.PositiveZ,
                LinUnitBasisVector3D.NegativeX,
                LinUnitBasisVector3D.NegativeY,
                LinUnitBasisVector3D.NegativeZ
            };

            var axisNameList = new[]
            {
                "Axis3D.PositiveX",
                "Axis3D.PositiveY",
                "Axis3D.PositiveZ",
                "Axis3D.NegativeX",
                "Axis3D.NegativeY",
                "Axis3D.NegativeZ"
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
                var axisVector1 = axis1.ToVector3D();

                composer
                    .AppendLineAtNewLine($"{axisName1} => axis2 switch")
                    .AppendLine("{")
                    .IncreaseIndentation();

                for (var i2 = 0; i2 < 6; i2++)
                {
                    var axis2 = axisList[i2];
                    var axisName2 = axisNameList[i2];
                    var axisVector2 = axis2.ToVector3D();

                    if (axis1 == axis2)
                    {
                        composer.AppendLineAtNewLine($"{axisName2} => Tuple4D.QuaternionIdentity,");
                    }
                    else if (axis1.IsOppositeTo(axis2))
                    {
                        var rotationAxis = axis1 switch
                        {
                            LinUnitBasisVector3D.PositiveX or LinUnitBasisVector3D.NegativeX => Float64Vector3D.E3,
                            LinUnitBasisVector3D.PositiveY or LinUnitBasisVector3D.NegativeY => Float64Vector3D.E1,
                            _ => Float64Vector3D.E2
                        };

                        var q = rotationAxis.CreateQuaternion(Math.PI);
                        
                        composer
                            .AppendLineAtNewLine($"{axisName2} => new Tuple4D({q.ScalarI}, {q.ScalarJ}, {q.ScalarK}, {q.Scalar}),");
                    }
                    else
                    {
                        var v3 = axisVector1.VectorCross(axisVector2);
                        var rotationAxis = Float64Vector3D.Create(v3.X, v3.Y, v3.Z);

                        var q = rotationAxis.CreateQuaternion(Math.PI / 2);

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
            var axisList = new []
            {
                LinUnitBasisVector3D.PositiveX,
                LinUnitBasisVector3D.PositiveY,
                LinUnitBasisVector3D.PositiveZ,
                LinUnitBasisVector3D.NegativeX,
                LinUnitBasisVector3D.NegativeY,
                LinUnitBasisVector3D.NegativeZ
            };
            
            for (var i1 = 0; i1 < 6; i1++)
            {
                var axis1 = axisList[i1];

                for (var i2 = 0; i2 < 6; i2++)
                {
                    var axis2 = axisList[i2];

                    var quaternion = axis1.CreateAxisToAxisRotationQuaternion(axis2);

                    var v2 = quaternion.RotateVector(axis1).MapComponents(
                        s => Math.Round(s, 3)
                    );

                    if ((v2 - axis2.ToVector3D()).ENorm().IsNearZero(1e-7))
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
            var k = Float64Vector3D.Create(1, 1, 1);
            var vectorList = new []
            {
                (LinUnitBasisVector3D.PositiveX.ToVector3D() + k).ToUnitVector(),
                (LinUnitBasisVector3D.PositiveY.ToVector3D() + k).ToUnitVector(),
                (LinUnitBasisVector3D.PositiveZ.ToVector3D() + k).ToUnitVector(),
                (LinUnitBasisVector3D.NegativeX.ToVector3D() - k).ToUnitVector(),
                (LinUnitBasisVector3D.NegativeY.ToVector3D() - k).ToUnitVector(),
                (LinUnitBasisVector3D.NegativeZ.ToVector3D() - k).ToUnitVector()
            };

            for (var i1 = 0; i1 < 6; i1++)
            {
                var vector = vectorList[i1];

                var (axis, quaternion) = 
                    vector.CreateNearestAxisToVectorRotationQuaternion();

                var axisVector = 
                    axis.ToVector3D();

                var (u, a) = 
                    axisVector.CreateVectorToVectorRotationAxisAngle(vector);

                var q = u.CreateQuaternion(a);
                
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
            var axisList = new []
            {
                LinUnitBasisVector3D.PositiveX,
                LinUnitBasisVector3D.PositiveY,
                LinUnitBasisVector3D.PositiveZ,
                LinUnitBasisVector3D.NegativeX,
                LinUnitBasisVector3D.NegativeY,
                LinUnitBasisVector3D.NegativeZ
            };

            var k = Float64Vector3D.Create(1, 1, 1);
            var vectorList = new []
            {
                (LinUnitBasisVector3D.PositiveX.ToVector3D() + k).ToUnitVector(),
                (LinUnitBasisVector3D.PositiveY.ToVector3D() + k).ToUnitVector(),
                (LinUnitBasisVector3D.PositiveZ.ToVector3D() + k).ToUnitVector(),
                (LinUnitBasisVector3D.NegativeX.ToVector3D() - k).ToUnitVector(),
                (LinUnitBasisVector3D.NegativeY.ToVector3D() - k).ToUnitVector(),
                (LinUnitBasisVector3D.NegativeZ.ToVector3D() - k).ToUnitVector()
            };

            for (var i1 = 0; i1 < 6; i1++)
            {
                var vector = vectorList[i1];

                for (var i2 = 0; i2 < 6; i2++)
                {
                    var axis = axisList[i2];
                    var axisVector = 
                        axis.ToVector3D();

                    var quaternion = 
                        axis.CreateAxisToVectorRotationQuaternion(vector);

                    var (u, a) = 
                        axisVector.CreateVectorToVectorRotationAxisAngle(vector);

                    var q = u.CreateQuaternion(a);

                    var v1 = quaternion.RotateVector(axis);
                    var v2 = q.RotateVector(axis);

                    var l1 = (v1 - vector).ENormSquared();
                    var l2 = (v2 - vector).ENormSquared();

                    var qDiff = (quaternion - q).NormSquared();

                    if (l1.IsNearZero() && l2.IsNearZero() && qDiff.IsNearZero())
                        continue;
                    
                    var nearestAxisVector = vector.SelectNearestAxis();

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
}
