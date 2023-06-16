using System.Collections;
using System.Runtime.CompilerServices;

namespace GraphicsComposerLib.Rendering.BabylonJs.Animations;

public sealed class GrBabylonJsKeyFrameDictionary<T> :
    IReadOnlyDictionary<int, T>
{
    public static GrBabylonJsKeyFrameDictionary<T> Create(IEnumerable<int> frameIndexList, Func<int, int> keyMapping, Func<int, T> valueMapping)
    {
        var keyValueDictionary = new GrBabylonJsKeyFrameDictionary<T>();

        foreach (var frameIndex in frameIndexList)
        {
            keyValueDictionary.Add(
                keyMapping(frameIndex), 
                valueMapping(frameIndex)
            );
        }

        return keyValueDictionary;
    }
    
    public static GrBabylonJsKeyFrameDictionary<T> Create(IEnumerable<int> frameIndexList, Func<int, T> valueMapping)
    {
        var keyValueDictionary = new GrBabylonJsKeyFrameDictionary<T>();

        foreach (var frameIndex in frameIndexList)
        {
            keyValueDictionary.Add(
                frameIndex, 
                valueMapping(frameIndex)
            );
        }

        return keyValueDictionary;
    }

    public static GrBabylonJsKeyFrameDictionary<T> Create(IEnumerable<KeyValuePair<int, T>> frameValuePairs, Func<KeyValuePair<int, T>, int> keyMapping, Func<KeyValuePair<int, T>, T> valueMapping)
    {
        var keyValueDictionary = new GrBabylonJsKeyFrameDictionary<T>();

        foreach (var keyValuePair in frameValuePairs)
        {
            var key = keyMapping(keyValuePair);
            var value = valueMapping(keyValuePair);

            keyValueDictionary.Add(key, value);
        }

        return keyValueDictionary;
    }

    public static GrBabylonJsKeyFrameDictionary<T> Create<T1>(IEnumerable<KeyValuePair<int, T1>> frameValuePairs, Func<KeyValuePair<int, T1>, int> keyMapping, Func<KeyValuePair<int, T1>, T> valueMapping)
    {
        var keyValueDictionary = new GrBabylonJsKeyFrameDictionary<T>();

        foreach (var keyValuePair in frameValuePairs)
        {
            var key = keyMapping(keyValuePair);
            var value = valueMapping(keyValuePair);

            keyValueDictionary.Add(key, value);
        }

        return keyValueDictionary;
    }


    private readonly SortedDictionary<int, T> _keyFrames 
        = new SortedDictionary<int, T>();


    public int Count 
        => _keyFrames.Count;
    
    public IEnumerable<int> Keys 
        => _keyFrames.Keys;

    public IEnumerable<T> Values 
        => _keyFrames.Values;

    public T this[int key] 
        => _keyFrames[key];

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsConstant(Func<T, T, bool> isEqual)
    {
        if (_keyFrames.Count < 2)
            return true;

        var firstValue = Values.First();

        return Values.All(
            value => isEqual(value, firstValue)
        );
    }

    public bool IsSame(GrBabylonJsKeyFrameDictionary<T> keyFrames2, Func<T, T, bool> isEqual)
    {
        if (Count != keyFrames2.Count)
            return false;

        using var enumerator1 = GetEnumerator();
        using var enumerator2 = keyFrames2.GetEnumerator();

        while (enumerator1.MoveNext())
        {
            if (!enumerator2.MoveNext())
                return false;

            if (enumerator1.Current.Key != enumerator2.Current.Key)
                return false;
            
            if (!isEqual(enumerator1.Current.Value, enumerator2.Current.Value))
                return false;
        }

        return !enumerator2.MoveNext();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(int key)
    {
        return _keyFrames.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(int key, out T value)
    {
        return _keyFrames.TryGetValue(key, out value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<T> Clear()
    {
        _keyFrames.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<T> Add(int key, T value)
    {
        _keyFrames.Add(key, value);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<T> SetKeyFrameValue(int frameIndex, T value)
    {
        if (_keyFrames.ContainsKey(frameIndex))
            _keyFrames[frameIndex] = value;
        else
            _keyFrames.Add(frameIndex, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(int key)
    {
        return _keyFrames.Remove(key);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<T> ReduceKeyFrames(IEnumerable<int> frameIndexList)
    {
        return frameIndexList.ToKeyFramesDictionary(
            frameIndex => this[frameIndex]
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
    {
        return _keyFrames.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}