using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64VectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorToArray1D(this XGaFloat64Vector vector)
    {
        var arraySize = vector.VSpaceDimensions;

        var array = new double[arraySize];

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[id.FirstIndex] = scalar;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorToArray1D(this XGaFloat64Vector vector, int arraySize)
    {
        if (arraySize < vector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = new double[arraySize];

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[id.FirstIndex] = scalar;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] VectorToRowArray2D(this XGaFloat64Vector vector, int arraySize)
    {
        if (arraySize < vector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = new double[1, arraySize];

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[0, id.FirstIndex] = scalar;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] VectorToColumnArray2D(this XGaFloat64Vector vector, int arraySize)
    {
        if (arraySize < vector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = new double[arraySize, 1];

        foreach (var (id, scalar) in vector.IdScalarPairs)
            array[id.FirstIndex, 0] = scalar;

        return array;
    }
    
        

    public static XGaIdScalarRecord GetMinScalarMagnitudeIdScalar(this XGaFloat64Multivector mv)
    {
        if (mv.IsZero)
            throw new InvalidOperationException();

        var (minValueId, minValue) = mv.IdScalarPairs.First();
        minValue = minValue.Abs();

        foreach (var (id, scalar) in mv.IdScalarPairs)
        {
            var absNumber = scalar.Abs();

            if (absNumber >= minValue) continue;

            minValue = absNumber;
            minValueId = id;
        }

        return new XGaIdScalarRecord(minValueId, minValue);
    }

    public static XGaFloat64Multivector GetUnitNormalVector(this XGaFloat64Vector vector)
    {
        var metric = vector.Processor;

        var (minValueId, minValue) =
            vector.GetMinScalarMagnitudeIdScalar();

        var composer = vector.ToComposer();

        var sum = 0d;
        foreach (var (id, scalar) in vector.IdScalarPairs)
        {
            if (id == minValueId) continue;

            var signature = metric.Signature(id);

            if (signature.IsPositive)
            {
                sum += scalar;
                composer.SetTerm(id, minValue);
            }
            else if (signature.IsNegative)
            {
                sum -= scalar;
                composer.SetTerm(id, -minValue);
            }
        }

        composer.SetTerm(minValueId, -sum);
        composer.Divide(composer.Norm().ScalarValue);

        return composer.GetVector();
    }

    public static XGaFloat64Vector GetEUnitNormalVector(this XGaFloat64Vector vector)
    {
        var (minValueId, minValue) =
            vector.GetMinScalarMagnitudeIdScalar();

        var composer = vector.ToComposer();

        var sum = 0d;
        foreach (var (id, scalar) in vector.IdScalarPairs)
        {
            if (id == minValueId) continue;

            sum += scalar;
            composer.SetTerm(id, minValue);
        }

        composer.SetTerm(minValueId, -sum);
        composer.Divide(composer.Norm().ScalarValue);

        return composer.GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ToVector2D(this XGaFloat64Vector vector)
    {
        return LinFloat64Vector2D.Create(
            (Float64Scalar)vector.Scalar(0),
            (Float64Scalar)vector.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToVector3D(this XGaFloat64Vector vector)
    {
        return LinFloat64Vector3D.Create(
            vector.Scalar(0),
            vector.Scalar(1),
            vector.Scalar(2)
        );
    }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetTuple3D(this XGaFloat64Vector vector)
    {
        return LinFloat64Vector3D.Create(
            vector[0],
            vector[1],
            vector[2]
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Angle GetEuclideanAngle(this XGaFloat64Vector vector1, XGaFloat64Vector vector2, bool assumeUnitVectors = false)
    {
        var angleCos = vector1.ESp(vector2).Scalar();

        if (!assumeUnitVectors)
            angleCos /= vector1.ENorm() * vector2.ENorm();

        return angleCos.CosToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector GetUnitBisector(this XGaFloat64Vector vector1, XGaFloat64Vector vector2, bool assumeEqualNormVectors = false)
    {
        var v = assumeEqualNormVectors
            ? vector1 + vector2
            : vector1.DivideByENorm() + vector2.DivideByENorm();

        return v.DivideByENorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector OmMapUsing(this XGaFloat64Vector vector, IXGaFloat64Outermorphism om)
    {
        return om.OmMap(vector);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<Vector> OmMap(this IOutermorphism om, params Vector[] vectorsList)
    //{
    //    return vectorsList.Select(v => v.OmMapUsing(om));
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> OmMap(this IXGaFloat64Outermorphism om, IEnumerable<XGaFloat64Vector> vectorsList)
    {
        return vectorsList.Select(v => v.OmMapUsing(om));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> OmMapUsing(this IEnumerable<XGaFloat64Vector> vectorsList, IXGaFloat64Outermorphism om)
    {
        return vectorsList.Select(v => v.OmMapUsing(om));
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector Project(this IXGaFloat64Subspace subspace, Vector vector)
    //{
    //    var processor = subspace.GeometricProcessor;

    //    return new Vector(
    //        processor,
    //        subspace.Project(vector)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector ProjectOn(this Vector vector, IXGaFloat64Subspace subspace)
    //{
    //    var processor = subspace.GeometricProcessor;

    //    return new Vector(
    //        processor,
    //        subspace.Project(vector.VectorStorage)
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> ProjectVectors(this IXGaFloat64Subspace subspace, params XGaFloat64Vector[] vectorsList)
    {
        return vectorsList.Select(subspace.Project);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> Project(this IXGaFloat64Subspace subspace, IEnumerable<XGaFloat64Vector> vectorsList)
    {
        return vectorsList.Select(subspace.Project);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ProjectOnVector(this XGaFloat64Vector vector, XGaFloat64Vector subspace)
    {
        return vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ProjectOnBivector(this XGaFloat64Vector vector, XGaFloat64Bivector subspace)
    {
        return vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ProjectOnKVector(this XGaFloat64Vector vector, XGaFloat64KVector subspace)
    {
        return vector.ProjectOn(subspace.ToSubspace());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ProjectOn(this XGaFloat64Vector vector, IXGaFloat64Subspace subspace)
    {
        return subspace.Project(vector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector RejectOnVector(this XGaFloat64Vector vector, XGaFloat64Vector subspace)
    {
        return vector - vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector RejectOnBivector(this XGaFloat64Vector vector, XGaFloat64Bivector subspace)
    {
        return vector - vector.ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector RejectOnKVector(this XGaFloat64Vector vector, XGaFloat64KVector subspace)
    {
        return vector - vector.ProjectOn(subspace.ToSubspace());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector RejectOn(this XGaFloat64Vector vector, IXGaFloat64Subspace subspace)
    {
        return vector - subspace.Project(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> ProjectOn(this IEnumerable<XGaFloat64Vector> vectorsList, IXGaFloat64Subspace subspace)
    {
        return vectorsList.Select(subspace.Project);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor GetEuclideanRotorFromBasis(this XGaFloat64Vector vector2, int index)
    {
        var metric = vector2.Processor;
            
        return metric
            .VectorTerm(index)
            .CreatePureRotor(vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor GetEuclideanRotorFrom(this XGaFloat64Vector vector2, XGaFloat64Vector vector1)
    {
        return vector1.CreatePureRotor(vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor GetEuclideanRotorFrom(this XGaFloat64Vector vector2, XGaFloat64Vector vector1, bool assumeUnitVectors)
    {
        return vector1.CreatePureRotor(
            vector2,
            assumeUnitVectors
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor GetEuclideanRotorToBasis(this XGaFloat64Vector vector1, int index)
    {
        var metric = vector1.Processor;
            
        return vector1.CreatePureRotor(
            metric.VectorTerm(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor GetEuclideanRotorTo(this XGaFloat64Vector vector1, XGaFloat64Vector vector2)
    {
        return vector1.CreatePureRotor(vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor GetEuclideanRotorTo(this XGaFloat64Vector vector1, XGaFloat64Vector vector2, bool assumeUnitVectors)
    {
        return vector1.CreatePureRotor(
            vector2,
            assumeUnitVectors
        );
    }

    /// <summary>
    /// Find a Euclidean rotor from vector1 to its projection on subspace
    /// </summary>
    /// <param name="vector1"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor GetEuclideanRotorTo(this XGaFloat64Vector vector1, XGaFloat64Subspace subspace)
    {
        return vector1.CreatePureRotor(
            subspace.Project(vector1)
        );
    }
}