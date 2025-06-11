using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public static class XGaVectorUtils
{
    

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
    //public static Vector<T> Project<T>(this XGaSubspace<T> subspace, Vector<T> vector)
    //{
    //    var processor = subspace.GeometricProcessor;

    //    return new Vector<T>(
    //        processor,
    //        subspace.Project(vector)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector<T> ProjectOn<T>(this Vector<T> vector, XGaSubspace<T> subspace)
    //{
    //    var processor = subspace.GeometricProcessor;

    //    return new Vector<T>(
    //        processor,
    //        subspace.Project(vector.VectorStorage)
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> ProjectOn<T>(this IEnumerable<XGaVector<T>> vectorsList, XGaSubspace<T> subspace)
    {
        return vectorsList.Select(subspace.Project);
    }

    
}