using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Animations;

public sealed class GrBabylonJsKeyFrameDictionaryCache
{
    public sealed class KeyFrameDictionaryCache<T> :
        IReadOnlyDictionary<int, GrBabylonJsKeyFrameDictionary<T>>
    {
        private int _nameSequence;

        private readonly Dictionary<int, GrBabylonJsKeyFrameDictionary<T>> _keyFramesCache
            = new Dictionary<int, GrBabylonJsKeyFrameDictionary<T>>();


        public GrBabylonJsAnimationType TargetPropertyDataType { get; }
        
        public int Count 
            => _keyFramesCache.Count;
        
        public IEnumerable<int> Keys 
            => _keyFramesCache.Keys;

        public IEnumerable<GrBabylonJsKeyFrameDictionary<T>> Values 
            => _keyFramesCache.Values;
        
        public GrBabylonJsKeyFrameDictionary<T> this[int index] 
            => _keyFramesCache[index];
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal KeyFrameDictionaryCache(GrBabylonJsAnimationType targetPropertyDataType)
        {
            TargetPropertyDataType = targetPropertyDataType;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KeyFrameDictionaryCache<T> Clear()
        {
            _nameSequence = 0;
            _keyFramesCache.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetNameFromIndex(int index)
        {
            return GrBabylonJsKeyFrameDictionaryCache.GetNameFromIndex(
                index, 
                TargetPropertyDataType
            );
        }
        

        public KeyValuePair<int, GrBabylonJsKeyFrameDictionary<T>> AddOrGetKeyFrames(GrBabylonJsKeyFrameDictionary<T> keyFrames, Func<T, T, bool> isEqual)
        {
            foreach (var namedKeyFrames in _keyFramesCache)
                if (namedKeyFrames.Value.IsSame(keyFrames, isEqual))
                    return namedKeyFrames;

            var index = _nameSequence++;
            
            _keyFramesCache.Add(index, keyFrames);

            return new KeyValuePair<int, GrBabylonJsKeyFrameDictionary<T>>(
                index, 
                keyFrames
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(int key)
        {
            return _keyFramesCache.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(int key, out GrBabylonJsKeyFrameDictionary<T> value)
        {
            return _keyFramesCache.TryGetValue(key, out value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<int, GrBabylonJsKeyFrameDictionary<T>>> GetEnumerator()
        {
            return _keyFramesCache.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    } 

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetNameFromIndex(int index, GrBabylonJsAnimationType targetPropertyDataType)
    {
        var typeName = targetPropertyDataType switch
        {
            GrBabylonJsAnimationType.Float => "floatKeyFrames",
            GrBabylonJsAnimationType.Vector2 => "vector2KeyFrames",
            GrBabylonJsAnimationType.Vector3 => "vector3KeyFrames",
            GrBabylonJsAnimationType.Size => "sizeKeyFrames",
            GrBabylonJsAnimationType.Quaternion => "quaternionKeyFrames",
            GrBabylonJsAnimationType.Matrix => "matrixKeyFrames",
            GrBabylonJsAnimationType.Color3 => "color3KeyFrames",
            GrBabylonJsAnimationType.Color4 => "color4KeyFrames",
            _ => throw new NotSupportedException()
        };
            
        return typeName + index.ToString("X4");
    }


    public KeyFrameDictionaryCache<double> FloatKeyFramesCache { get; }
        = new KeyFrameDictionaryCache<double>(GrBabylonJsAnimationType.Float);
    
    public KeyFrameDictionaryCache<LinFloat64Vector2D> Vector2KeyFramesCache { get; }
        = new KeyFrameDictionaryCache<LinFloat64Vector2D>(GrBabylonJsAnimationType.Vector2);

    public KeyFrameDictionaryCache<LinFloat64Vector3D> Vector3KeyFramesCache { get; }
        = new KeyFrameDictionaryCache<LinFloat64Vector3D>(GrBabylonJsAnimationType.Vector3);
    
    public KeyFrameDictionaryCache<LinFloat64Quaternion> QuaternionKeyFramesCache { get; }
        = new KeyFrameDictionaryCache<LinFloat64Quaternion>(GrBabylonJsAnimationType.Quaternion);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionaryCache Clear()
    {
        FloatKeyFramesCache.Clear();
        Vector2KeyFramesCache.Clear();
        Vector3KeyFramesCache.Clear();
        QuaternionKeyFramesCache.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public KeyValuePair<int, GrBabylonJsKeyFrameDictionary<double>> AddOrGetFloatKeyFrames(GrBabylonJsKeyFrameDictionary<double> keyFrames)
    {
        return FloatKeyFramesCache.AddOrGetKeyFrames(
            keyFrames,
            (v1, v2) => 
                (v1 - v2).IsNearZero()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public KeyValuePair<int, GrBabylonJsKeyFrameDictionary<LinFloat64Vector2D>> AddOrGetVector2KeyFrames(GrBabylonJsKeyFrameDictionary<LinFloat64Vector2D> keyFrames)
    {
        return Vector2KeyFramesCache.AddOrGetKeyFrames(
            keyFrames,
            (v1, v2) => 
                (v1 - v2).VectorENormSquared().IsNearZero()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public KeyValuePair<int, GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>> AddOrGetVector3KeyFrames(GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D> keyFrames)
    {
        return Vector3KeyFramesCache.AddOrGetKeyFrames(
            keyFrames,
            (v1, v2) => 
                (v1 - v2).VectorENormSquared().IsNearZero()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public KeyValuePair<int, GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>> AddOrGetQuaternionKeyFrames(GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion> keyFrames)
    {
        return QuaternionKeyFramesCache.AddOrGetKeyFrames(
            keyFrames,
            (v1, v2) => 
                (v1.ToRotationVector() - v2.ToRotationVector()).VectorENormSquared().IsNearZero()
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<double> GetFloatKeyFrames(int index)
    {
        return FloatKeyFramesCache[index];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<LinFloat64Vector2D> GetVector2KeyFrames(int index)
    {
        return Vector2KeyFramesCache[index];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D> GetVector3KeyFrames(int index)
    {
        return Vector3KeyFramesCache[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion> GetQuaternionKeyFrames(int index)
    {
        return QuaternionKeyFramesCache[index];
    }

    
    public string GetCode()
    {
        var composer = new LinearTextComposer();

        foreach (var (index, keyFrames) in FloatKeyFramesCache)
        {
            if (keyFrames.IsConstant())
                continue;

            var name = FloatKeyFramesCache.GetNameFromIndex(index);
            var code = keyFrames.GetBabylonJsCode();

            composer
                .AppendLine($"const {name} = {code};")
                .AppendLine();
        }
        
        foreach (var (index, keyFrames) in Vector2KeyFramesCache)
        {
            if (keyFrames.IsConstant())
                continue;

            var name = Vector2KeyFramesCache.GetNameFromIndex(index);
            var code = keyFrames.GetBabylonCode();

            composer
                .AppendLine($"const {name} = {code};")
                .AppendLine();
        }
        
        foreach (var (index, keyFrames) in Vector3KeyFramesCache)
        {
            if (keyFrames.IsConstant())
                continue;

            var name = Vector3KeyFramesCache.GetNameFromIndex(index);
            var code = keyFrames.GetBabylonCode();

            composer
                .AppendLine($"const {name} = {code};")
                .AppendLine();
        }
        
        foreach (var (index, keyFrames) in QuaternionKeyFramesCache)
        {
            if (keyFrames.IsConstant())
                continue;

            var name = QuaternionKeyFramesCache.GetNameFromIndex(index);
            var code = keyFrames.GetBabylonJsCode();

            composer
                .AppendLine($"const {name} = {code};")
                .AppendLine();
        }

        return composer.ToString();
    }
}

