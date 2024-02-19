using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

public static class ScalarProcessorTimesUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Half<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Divide(
            scalar,
            scalarProcessor.GetScalarFromNumber(2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NegativeHalf<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Divide(
            scalar,
            scalarProcessor.GetScalarFromNumber(-2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, params T[] scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.GetScalarFromNumber(scalar1),
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, params T[] scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.GetScalarFromNumber(scalar1),
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, params T[] scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.GetScalarFromNumber(scalar1),
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, params T[] scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.GetScalarFromNumber(scalar1),
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, params T[] scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.GetScalarFromNumber(scalar1),
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.Times(
            scalar1,
            scalarProcessor.GetScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.Times(
            scalarProcessor.GetScalarFromNumber(scalar1),
            scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, params T[] scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.GetScalarFromNumber(scalar1),
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.ScalarOne,
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
    {
        return scalarsList.Aggregate(
            scalarProcessor.ScalarOne,
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarsList)
    {
        return scalarProcessor.Negative(
            scalarsList.Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
    {
        return scalarProcessor.Negative(
            scalarsList.Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TimesMapped<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<T, T> mappingFunc)
    {
        return scalarsList
            .MapItems(mappingFunc)
            .Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TimesMapped<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<int, T, T> mappingFunc)
    {
        return scalarsList
            .MapItems(mappingFunc)
            .Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times);
    }
}