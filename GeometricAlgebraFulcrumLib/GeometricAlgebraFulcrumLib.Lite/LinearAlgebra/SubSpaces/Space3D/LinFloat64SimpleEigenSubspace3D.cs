﻿using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.Space3D;

public sealed class LinFloat64SimpleEigenSubspace3D :
    ILinFloat64Subspace3D
{
    public int VSpaceDimensions
        => 3;

    public int SubspaceDimensions
        => Subspace.SubspaceDimensions;

    public IEnumerable<Float64Vector3D> BasisVectors
        => Subspace.BasisVectors;

    public ILinFloat64Subspace3D Subspace { get; }

    public Complex EigenValue { get; }


    public LinFloat64SimpleEigenSubspace3D(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
    {
        EigenValue = eigenValue;

        if (eigenValue.IsNearOne())
        {
            var u =
                eigenVector.Imaginary().ToVector3D();

            // Eigen value is real one
            Subspace = LinFloat64LineSubspace3D.CreateFromVector(u);

            return;
        }

        var eigenValueRealPart = eigenValue.Real;
        var eigenValueImagPart = eigenValue.Imaginary;

        if (eigenValueImagPart.IsNearZero())
        {
            if (eigenValueRealPart.IsNearZero())
            {
                // Eigen value is near zero
                Subspace = LinFloat64NullSubspace3D.Instance;

                // Make sure the eigen vector is near zero
                Debug.Assert(
                    eigenVector.L2Norm().IsNearZero()
                );
            }
            else
            {
                var u =
                    eigenVector.Real().ToVector3D();

                // Eigen value is real
                Subspace = LinFloat64LineSubspace3D.CreateFromVector(u);

                Debug.Assert(
                    !eigenVector.Real().IsNearZero()
                );
            }
        }
        else
        {
            // Eigen value is complex
            var u = eigenVector.Imaginary().ToVector3D();
            var v = eigenVector.Real().ToVector3D();

            Subspace = LinFloat64PlaneSubspace3D.CreateFromVectors(u, v);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetVectorProjectionPolarAngle(IFloat64Vector3D vector)
    {
        return Subspace.GetVectorProjectionPolarAngle(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(IFloat64Vector3D vector, double epsilon = 1E-12D)
    {
        return Subspace.NearContains(vector, epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace3D subspace, double epsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D GetPlanarRotation()
    {
        if (Subspace is not LinFloat64PlaneSubspace3D planeSubspace)
            throw new InvalidOperationException();

        var angle = Math.Atan2(
            EigenValue.Imaginary,
            EigenValue.Real
        ).RadiansToAngle();

        return LinFloat64PlanarRotation3D.CreateFromOrthonormalVectors(
            planeSubspace.BasisVector1,
            planeSubspace.BasisVector2,
            angle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64PlanarRotation3D> GetPlanarRotations()
    {
        if (Subspace is LinFloat64PlaneSubspace3D planeSubspace)
        {
            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            );

            yield return LinFloat64PlanarRotation3D.CreateFromOrthonormalVectors(
                planeSubspace.BasisVector1,
                planeSubspace.BasisVector2,
                angle
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64HyperPlaneNormalReflection3D> GetHyperPlaneNormalReflectionMaps()
    {
        if (Subspace is LinFloat64PlaneSubspace3D planeSubspace)
        {
            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            );

            var (r1, r2) =
                LinFloat64PlanarRotation3D.CreateFromOrthonormalVectors(
                    planeSubspace.BasisVector1,
                    planeSubspace.BasisVector2,
                    angle
                ).GetHyperPlaneReflectionPair();

            yield return r1;
            yield return r2;
        }
        else if (Subspace is LinFloat64LineSubspace3D lineSubspace)
        {
            if (EigenValue.IsNearMinusOne())
                yield return LinFloat64HyperPlaneNormalReflection3D.Create(lineSubspace.BasisVector);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64VectorDirectionalScaling3D> GetVectorDirectionalScalingMaps()
    {
        if (Subspace is LinFloat64PlaneSubspace3D planeSubspace)
        {
            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            );

            var (r1, r2) =
                LinFloat64PlanarRotation3D.CreateFromOrthonormalVectors(
                    planeSubspace.BasisVector1,
                    planeSubspace.BasisVector2,
                    angle
                ).GetHyperPlaneReflectionPair();

            yield return r1.ToVectorDirectionalScaling();
            yield return r2.ToVectorDirectionalScaling();
        }
        else if (Subspace is LinFloat64LineSubspace3D lineSubspace)
        {
            var r1 =
                lineSubspace.BasisVector.CreateDirectionalScaling3D(EigenValue.Real);

            yield return r1.ToVectorDirectionalScaling();
        }
    }


    public override string ToString()
    {
        var composer = new StringBuilder();

        composer.AppendLine($"Dimensions: {SubspaceDimensions}");
        composer.AppendLine($"Eigen Value: {EigenValue}");

        if (SubspaceDimensions == 2)
        {
            var angle =
                Math.Atan2(EigenValue.Imaginary, EigenValue.Real).RadiansToDegrees();

            composer.AppendLine($"Rotation Angle: {angle:F3} degrees");
        }

        var j = 1;
        foreach (var vector in BasisVectors)
        {
            composer.AppendLine($"Basis Vector {j}: {vector}");

            j++;
        }

        return composer.ToString();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    public Float64Vector3D GetVectorProjection(IFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

    public Float64Vector3D GetVectorRejection(IFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

}