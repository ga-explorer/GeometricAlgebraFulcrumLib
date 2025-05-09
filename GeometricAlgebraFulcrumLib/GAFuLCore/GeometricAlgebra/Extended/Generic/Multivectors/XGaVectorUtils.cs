using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

public static class XGaVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] VectorToArray1D<T>(this XGaVector<T> vector)
    {
        var array = vector
            .ScalarProcessor
            .CreateArrayZero1D(vector.VSpaceDimensions);

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[id.FirstIndex] = scalar;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] VectorToArray1D<T>(this XGaVector<T> vector, int vectorSize)
    {
        if (vectorSize < vector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = vector
            .ScalarProcessor
            .CreateArrayZero1D(vectorSize);

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[id.FirstIndex] = scalar;

        return array;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] VectorToRowArray2D<T>(this XGaVector<T> vector, int vectorSize)
    {
        if (vectorSize < vector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = vector
            .ScalarProcessor
            .CreateArrayZero2D(1, vectorSize);

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[0, id.FirstIndex] = scalar;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] VectorToColumnArray2D<T>(this XGaVector<T> vector, int vectorSize)
    {
        if (vectorSize < vector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = vector
            .ScalarProcessor
            .CreateArrayZero2D(vectorSize, 1);

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[id.FirstIndex, 0] = scalar;

        return array;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToFloat64Vector3D(this XGaVector<double> vector)
    {
        return LinFloat64Vector3D.Create(
            vector.Scalar(0).ScalarValue,
            vector.Scalar(1).ScalarValue,
            vector.Scalar(2).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToLinFloat64Vector(this XGaVector<double> vector)
    {
        return LinFloat64Vector.Create(
            vector.VectorToArray1D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetEuclideanAngle<T>(this XGaVector<T> vector1, XGaVector<T> vector2, bool assumeUnitVectors = false)
    {
        var angle = vector1.ESp(vector2).Scalar();

        if (!assumeUnitVectors)
            angle /= (vector1.ENorm() * vector2.ENorm());

        return angle.ArcCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> GetUnitBisector<T>(this XGaVector<T> vector1, XGaVector<T> vector2, bool assumeEqualNormVectors = false)
    {
        var v = assumeEqualNormVectors
            ? vector1 + vector2
            : vector1.DivideByENorm() + vector2.DivideByENorm();

        return v.DivideByENorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> OmMapUsing<T>(this XGaVector<T> vector, IXGaOutermorphism<T> om)
    {
        return om.OmMap(vector);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<Vector<T>> OmMap<T>(this IOutermorphism<T> om, params Vector<T>[] vectorsList)
    //{
    //    return vectorsList.Select(v => v.OmMapUsing(om));
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> OmMap<T>(this IXGaOutermorphism<T> om, IEnumerable<XGaVector<T>> vectorsList)
    {
        return vectorsList.Select(v => v.OmMapUsing(om));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> OmMapUsing<T>(this IEnumerable<XGaVector<T>> vectorsList, IXGaOutermorphism<T> om)
    {
        return vectorsList.Select(v => v.OmMapUsing(om));
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector<T> Project<T>(this IXGaSubspace<T> subspace, Vector<T> vector)
    //{
    //    var processor = subspace.GeometricProcessor;

    //    return new Vector<T>(
    //        processor,
    //        subspace.Project(vector)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector<T> ProjectOn<T>(this Vector<T> vector, IXGaSubspace<T> subspace)
    //{
    //    var processor = subspace.GeometricProcessor;

    //    return new Vector<T>(
    //        processor,
    //        subspace.Project(vector.VectorStorage)
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> ProjectVectors<T>(this IXGaSubspace<T> subspace, params XGaVector<T>[] vectorsList)
    {
        return vectorsList.Select(subspace.Project);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> Project<T>(this IXGaSubspace<T> subspace, IEnumerable<XGaVector<T>> vectorsList)
    {
        return vectorsList.Select(subspace.Project);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ProjectOnVector<T>(this XGaVector<T> vector, XGaVector<T> subspace)
    {
        return vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ProjectOnBivector<T>(this XGaVector<T> vector, XGaBivector<T> subspace)
    {
        return vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ProjectOnKVector<T>(this XGaVector<T> vector, XGaKVector<T> subspace)
    {
        return vector.ProjectOn(subspace.ToSubspace());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ProjectOn<T>(this XGaVector<T> vector, IXGaSubspace<T> subspace)
    {
        return subspace.Project(vector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> RejectOnVector<T>(this XGaVector<T> vector, XGaVector<T> subspace)
    {
        return vector - vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> RejectOnBivector<T>(this XGaVector<T> vector, XGaBivector<T> subspace)
    {
        return vector - vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> RejectOnKVector<T>(this XGaVector<T> vector, XGaKVector<T> subspace)
    {
        return vector - vector.ProjectOn(subspace.ToSubspace());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> RejectOn<T>(this XGaVector<T> vector, IXGaSubspace<T> subspace)
    {
        return vector - subspace.Project(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> ProjectOn<T>(this IEnumerable<XGaVector<T>> vectorsList, IXGaSubspace<T> subspace)
    {
        return vectorsList.Select(subspace.Project);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> GetEuclideanRotorFromBasis<T>(this XGaVector<T> vector2, int index)
    {
        var processor = vector2.Processor;

        return processor
            .VectorTerm(index)
            .CreatePureRotor(vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> GetEuclideanRotorFrom<T>(this XGaVector<T> vector2, XGaVector<T> vector1)
    {
        return vector1.CreatePureRotor(vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> GetEuclideanRotorFrom<T>(this XGaVector<T> vector2, XGaVector<T> vector1, bool assumeUnitVectors)
    {
        return vector1.CreatePureRotor(
            vector2,
            assumeUnitVectors
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> GetEuclideanRotorToBasis<T>(this XGaVector<T> vector1, int index)
    {
        var processor = vector1.Processor;

        return vector1.CreatePureRotor(
            processor.VectorTerm(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> GetEuclideanRotorTo<T>(this XGaVector<T> vector1, XGaVector<T> vector2)
    {
        return vector1.CreatePureRotor(vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> GetEuclideanRotorTo<T>(this XGaVector<T> vector1, XGaVector<T> vector2, bool assumeUnitVectors)
    {
        return vector1.CreatePureRotor(
            vector2,
            assumeUnitVectors
        );
    }

    /// <summary>
    /// Find a Euclidean rotor from vector1 to its projection on subspace
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vector1"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> GetEuclideanRotorTo<T>(this XGaVector<T> vector1, XGaSubspace<T> subspace)
    {
        return vector1.CreatePureRotor(
            subspace.Project(vector1)
        );
    }
}