﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Composers;

public sealed class LinFloat64RotationComposer3D :
    LinFloat64Rotation3D,
    IReadOnlyList<LinFloat64Rotation3D>
{
    
    public static LinFloat64RotationComposer3D Create()
    {
        return new LinFloat64RotationComposer3D();
    }

    
    public static LinFloat64RotationComposer3D CreateFromMatrix(SquareMatrix3 matrix)
    {
        var composer = new LinFloat64RotationComposer3D();

        composer.AppendRotation(matrix.GetRotationPart());

        return composer;
    }

    
    public static LinFloat64RotationComposer3D CreateFromRotation(LinFloat64Rotation3D rotation)
    {
        var composer = new LinFloat64RotationComposer3D();

        composer.AppendRotation(rotation);

        return composer;
    }

    
    public static LinFloat64RotationComposer3D CreateRandom(Random random)
    {
        var composer = new LinFloat64RotationComposer3D();

        var u = random.GetLinVector3D();
        var v = random.GetLinVector3D();
        var angle = random.GetPolarAngle();

        composer.AppendRotation(
            LinFloat64PlanarRotation3D.CreateFromSpanningVectors(u, v, angle)
        );

        return composer;
    }


    private readonly List<LinFloat64Rotation3D> _rotationList
        = new List<LinFloat64Rotation3D>();


    public int Count
        => _rotationList.Count;

    public LinFloat64Rotation3D this[int index]
        => _rotationList[index];


    
    private LinFloat64RotationComposer3D()
    {
    }

    
    private LinFloat64RotationComposer3D(IEnumerable<LinFloat64Rotation3D> rotationList)
    {
        _rotationList = rotationList.ToList();
    }


    
    public override bool IsValid()
    {
        return _rotationList.All(r => r.IsValid());
    }

    public override bool IsIdentity()
    {
        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, zeroEpsilon);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }


    
    public LinFloat64RotationComposer3D AppendRotation(LinFloat64Rotation3D rotation)
    {
        if (rotation.IsIdentity())
            return this;

        _rotationList.Add(rotation);

        return this;
    }

    
    public LinFloat64RotationComposer3D AppendRotation(ILinFloat64Vector3D vector, ILinFloat64Vector3D rotatedVector, bool useShortArc = true)
    {
        return AppendRotation(
            LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                vector,
                rotatedVector,
                useShortArc
            )
        );
    }

    
    public LinFloat64RotationComposer3D AppendRotation(ILinFloat64Vector3D spanningVector1, ILinFloat64Vector3D spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        return AppendRotation(
            LinFloat64PlanarRotation3D.CreateFromSpanningVectors(
                spanningVector1,
                spanningVector2,
                rotationAngle
            )
        );
    }

    public LinFloat64RotationComposer3D AppendBasisAlignmentRotation(ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        if (vector1.IsNearZero() && vector2.IsNearZero())
            return this;

        if (vector2.IsNearZero())
            return AppendRotation(
                LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                    vector1,
                    LinFloat64Vector3D.E1
                )
            );

        if (vector1.IsNearZero())
            return AppendRotation(
                LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                    vector2,
                    LinFloat64Vector3D.E2
                )
            );

        // TODO: This needs handling of case where vector1 = -e1
        var rotation1 = LinFloat64PlanarRotation3D.CreateFromRotatedVector(
            vector1,
            LinBasisVector.Px.ToLinVector3D()
        );

        vector2 =
            rotation1
                .MapVector(vector2)
                .RejectOnAxis(LinBasisVector.Px);

        var rotation2 = LinFloat64PlanarRotation3D.CreateFromRotatedVector(
            vector2,
            LinBasisVector.Py.ToLinVector3D()
        );

        AppendRotation(rotation1);
        AppendRotation(rotation2);

        return this;
    }

    
    public LinFloat64RotationComposer3D PrependRotation(LinFloat64Rotation3D rotation)
    {
        if (rotation.IsIdentity())
            return this;

        _rotationList.Insert(0, rotation);

        return this;
    }

    
    public LinFloat64RotationComposer3D InsertRotation(int index, LinFloat64Rotation3D rotation)
    {
        if (rotation.IsIdentity())
            return this;

        _rotationList.Insert(index, rotation);

        return this;
    }

    //public double[] MapVectorInPlace(double[] vector)
    //{
    //    foreach (var rotation in _rotationList)
    //    {
    //        var u = rotation.BasisVector1;
    //        var t = rotation.BasisVector2;
    //        var v = rotation.GetRotatedBasisVector1();

    //        //var r = vector.ESp(TargetOrthogonalVector);
    //        //var s = vector.ESp(SourceVector);

    //        //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

    //        var r = vector.VectorDot(t);
    //        var s = vector.VectorDot(u);
    //        var rsPlus = r + s;
    //        var rsMinus = r - s;

    //        for (var i = 0; i < VSpaceDimensions; i++)
    //            vector[i] -= rsPlus * u[i] + rsMinus * v[i];
    //    }

    //    return vector;
    //}

    
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        return basisIndex switch
        {
            0 => MapVector(LinFloat64Vector3D.E1),
            1 => MapVector(LinFloat64Vector3D.E2),
            2 => MapVector(LinFloat64Vector3D.E3),
            _ => throw new IndexOutOfRangeException()
        };
    }

    
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return _rotationList.Aggregate(
            vector.ToLinVector3D(),
            (rotatedVector, rotation) =>
                rotation.MapVector(rotatedVector)
        );
    }


    
    public LinFloat64RotationComposer3D SelfInverse()
    {
        if (_rotationList.Count == 0)
            return this;

        if (_rotationList.Count == 0)
            return this;

        var rotation = GetInverseRotation();

        _rotationList.Clear();

        return AppendRotation(rotation);
    }

    /// <summary>
    /// Reduce this sequence to the minimum number of pair-wise
    /// orthogonal rotations equivalent to this one
    /// </summary>
    /// <returns></returns>
    
    public LinFloat64RotationComposer3D SelfReduce()
    {
        if (_rotationList.Count == 0)
            return this;

        var rotation = GetRotation();

        _rotationList.Clear();

        return AppendRotation(rotation);
    }

    
    public override LinFloat64Quaternion GetQuaternion()
    {
        return GetRotation().GetQuaternion();
    }

    
    public LinFloat64Rotation3D GetRotation()
    {
        return _rotationList.Count switch
        {
            0 => LinFloat64IdentityLinearMap3D.Instance,
            1 => _rotationList[0],
            _ => this.ToSquareMatrix3().GetPlanarRotation3D()
        };
    }

    
    public LinFloat64PlanarRotation3D GetPlanarRotation()
    {
        return _rotationList.Count == 0
            ? LinFloat64PlanarRotation3D.Identity
            : this.ToSquareMatrix3().GetPlanarRotation3D();
    }

    
    public override LinFloat64Rotation3D GetInverseRotation()
    {
        return _rotationList.Count switch
        {
            0 => LinFloat64IdentityLinearMap3D.Instance,
            1 => _rotationList[0].GetInverseRotation(),
            _ => this.ToSquareMatrix3().Transpose().GetRotationPart()
        };
    }

    public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
    {
        var reflection =
            LinFloat64HyperPlaneNormalReflectionSequence3D.Create();

        var (r1, r2) =
            GetPlanarRotation().GetHyperPlaneReflectionPair();

        reflection
            .AppendMap(r1)
            .AppendMap(r2);

        return reflection;
    }

    
    public IEnumerator<LinFloat64Rotation3D> GetEnumerator()
    {
        return _rotationList.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}