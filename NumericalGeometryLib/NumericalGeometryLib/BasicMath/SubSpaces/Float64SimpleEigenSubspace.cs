using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Maps;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.SubSpaces;

public sealed class Float64SimpleEigenSubspace :
    IFloat64Subspace
{
    public int Dimensions 
        => Subspace.Dimensions;
    
    public int SubspaceDimensions 
        => Subspace.SubspaceDimensions;

    public IEnumerable<Float64Tuple> BasisVectors 
        => Subspace.BasisVectors;

    public IFloat64Subspace Subspace { get; }
    
    public Complex EigenValue { get; }
    

    public Float64SimpleEigenSubspace(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
    {
        EigenValue = eigenValue;

        if (eigenValue.IsNearOne())
        {
            var u = 
                eigenVector.Imaginary().ToArray().CreateTuple();

            // Eigen value is real one
            Subspace = Float64LineSubspace.CreateFromVector(u);

            return;
        }

        var eigenValueRealPart = eigenValue.Real;
        var eigenValueImagPart = eigenValue.Imaginary;

        if (eigenValueImagPart.IsNearZero())
        {
            if (eigenValueRealPart.IsNearZero())
            {
                // Eigen value is near zero
                Subspace = Float64NullSubspace.Create(eigenVector.Count);

                // Make sure the eigen vector is near zero
                Debug.Assert(
                    eigenVector.L2Norm().IsNearZero()
                );
            }
            else
            {
                var u = 
                    eigenVector.Real().ToTuple();

                // Eigen value is real
                Subspace = Float64LineSubspace.CreateFromVector(u);

                Debug.Assert(
                    !eigenVector.Real().IsNearZero()
                );
            }
        }
        else
        {
            // Eigen value is complex
            var u = eigenVector.Imaginary().ToArray().CreateTuple();
            var v = eigenVector.Real().ToArray().CreateTuple();

            Subspace = Float64PlaneSubspace.CreateFromVectors(u, v);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(Float64Tuple vector, double epsilon = 1e-12)
    {
        return Subspace.NearContains(vector, epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(IFloat64Subspace subspace, double epsilon = 1E-12)
    {
        return subspace.Dimensions <= Dimensions && 
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorToVectorRotation GetVectorToVectorRotation()
    {
        if (Subspace is not Float64PlaneSubspace planeSubspace)
            throw new InvalidOperationException();

        var angle = Math.Atan2(
            EigenValue.Imaginary,
            EigenValue.Real
        );

        return VectorToVectorRotation.Create(
            planeSubspace.BasisVector1, 
            planeSubspace.BasisVector2, 
            angle
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<VectorToVectorRotation> GetVectorToVectorRotationMaps()
    {
        if (Subspace is Float64PlaneSubspace planeSubspace)
        {
            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            );

            yield return VectorToVectorRotation.Create(
                planeSubspace.BasisVector1,
                planeSubspace.BasisVector2,
                angle
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<HyperPlaneNormalReflection> GetHyperPlaneNormalReflectionMaps()
    {
        if (Subspace is Float64PlaneSubspace planeSubspace)
        {
            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            );

            var (r1, r2) = 
                VectorToVectorRotation.Create(
                    planeSubspace.BasisVector1,
                    planeSubspace.BasisVector2,
                    angle
                ).GetHyperPlaneReflectionPair();

            yield return r1;
            yield return r2;
        }
        else if (Subspace is Float64LineSubspace lineSubspace)
        {
            if (EigenValue.IsNearMinusOne())
                yield return HyperPlaneNormalReflection.Create(lineSubspace.BasisVector);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<VectorDirectionalScaling> GetVectorDirectionalScalingMaps()
    {
        if (Subspace is Float64PlaneSubspace planeSubspace)
        {
            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            );

            var (r1, r2) = 
                VectorToVectorRotation.Create(
                    planeSubspace.BasisVector1,
                    planeSubspace.BasisVector2,
                    angle
                ).GetHyperPlaneReflectionPair();

            yield return r1.ToVectorDirectionalScaling();
            yield return r2.ToVectorDirectionalScaling();
        }
        else if (Subspace is Float64LineSubspace lineSubspace)
        {
            var r1 = 
                lineSubspace.BasisVector.ScalarArray.CreateDirectionalScaling(EigenValue.Real);

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
}