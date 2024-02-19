using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;

public sealed class LinMatrixSparseStorageComposer<T> :
    LinMatrixStorageComposerBase<T>
{
    public Dictionary<RGaKvIndexPairRecord, T> KeyValueDictionary { get; }
        = new Dictionary<RGaKvIndexPairRecord, T>();

    public override int Count
        => KeyValueDictionary.Count;


    internal LinMatrixSparseStorageComposer(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> Clear()
    {
        KeyValueDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> RemoveTerm(RGaKvIndexPairRecord key)
    {
        KeyValueDictionary.Remove(key);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> RemoveZeroTerms()
    {
        var keysMatrixStorage = KeyValueDictionary
            .Where(pair => ScalarProcessor.IsZero(pair.Value))
            .Select(pair => pair.Key)
            .ToArray();

        return RemoveTerms(keysMatrixStorage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> SetTerm(RGaKvIndexPairRecord key, T value)
    {
        if (KeyValueDictionary.ContainsKey(key))
            KeyValueDictionary[key] = value;
        else
            KeyValueDictionary.Add(key, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> AddTerm(RGaKvIndexPairRecord key, T value)
    {
        if (KeyValueDictionary.TryGetValue(key, out var value1))
            KeyValueDictionary[key] = ScalarProcessor.Add(value1, value1);
        else
            KeyValueDictionary.Add(key, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> SubtractTerm(RGaKvIndexPairRecord key, T value)
    {
        if (KeyValueDictionary.TryGetValue(key, out var value1))
            KeyValueDictionary[key] = ScalarProcessor.Subtract(value1, value1);
        else
            KeyValueDictionary.Add(key, ScalarProcessor.Negative(value));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> MapValues(Func<T, T> valueMapping)
    {
        foreach (var (key, value) in KeyValueDictionary)
            KeyValueDictionary[key] = valueMapping(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinMatrixStorageComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping)
    {
        foreach (var (key, value) in KeyValueDictionary)
            KeyValueDictionary[key] = valueMapping(key.KvIndex1, key.KvIndex2, value);

        return this;
    }

    public override ILinMatrixStorage<T> CreateLinMatrixStorage()
    {
        if (KeyValueDictionary.Count == 0)
            return LinMatrixEmptyStorage<T>.EmptyStorage;

        if (KeyValueDictionary.Count == 1)
        {
            var ((key1, key2), value) = KeyValueDictionary.First();

            return value.CreateLinMatrixSingleScalarStorage(key1, key2);
        }

        var denseCount1 =
            1UL + KeyValueDictionary.Keys.Select(key => key.KvIndex1).Max();

        var denseCount2 =
            1UL + KeyValueDictionary.Keys.Select(key => key.KvIndex2).Max();

        var denseCount = denseCount1 * denseCount2;

        if (denseCount / 3 < (ulong)KeyValueDictionary.Count || denseCount1 > int.MaxValue || denseCount2 > int.MaxValue)
            return KeyValueDictionary.CreateLinMatrixStorage();

        var array = ScalarProcessor.CreateArrayZero2D((int)denseCount1, (int)denseCount2);

        foreach (var ((key1, key2), scalar) in KeyValueDictionary)
            array[key1, key2] = scalar;

        return array.CreateLinMatrixDenseStorage();
    }

    public override ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage()
    {
        if (KeyValueDictionary.Count == 0)
            return LinMatrixEmptyStorage<T>.EmptyStorage;

        var denseCount1 =
            1UL + KeyValueDictionary.Keys.Select(key => key.KvIndex1).Max();

        var denseCount2 =
            1UL + KeyValueDictionary.Keys.Select(key => key.KvIndex2).Max();

        if (denseCount1 > int.MaxValue || denseCount2 > int.MaxValue)
            throw new InvalidOperationException();

        var array = ScalarProcessor.CreateArrayZero2D((int)denseCount1, (int)denseCount2);

        foreach (var ((key1, key2), scalar) in KeyValueDictionary)
            array[key1, key2] = scalar;

        return array.CreateLinMatrixDenseStorage();
    }
}