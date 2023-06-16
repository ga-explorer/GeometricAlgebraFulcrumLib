using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using Open.Collections;

namespace GraphicsComposerLib.Rendering.BabylonJs.Animations;

public static class GrBabylonJsKeyFrameDictionaryUtils
{
    private static bool TestNearLinear(this IReadOnlyList<KeyValuePair<int, double>> pointList, int index1, int index2, double yEpsilon)
    {
        var (x1, y1) = pointList[index1];
        var (x2, y2) = pointList[index2];
        var dx = x2 - x1;
        var dy = y2 - y1;
        var epsilon = dx * yEpsilon;

        for (var i = index1 + 1; i < index2; i++)
        {
            var (x, y) = pointList[i];

            var yh = dx * (y - y1) - dy * (x - x1);

            if (Math.Abs(yh) > epsilon) 
                return false;
        }

        return true;
    }
    
    private static bool TestNearLinear(this IReadOnlyList<KeyValuePair<int, Float64Vector2D>> pointList, int index1, int index2, double yEpsilon)
    {
        var (x1, y1) = pointList[index1];
        var (x2, y2) = pointList[index2];
        var dx = x2 - x1;
        var dy = y2 - y1;
        var epsilon = dx * yEpsilon;

        for (var i = index1 + 1; i < index2; i++)
        {
            var (x, y) = pointList[i];

            var yh = dx * (y - y1) - dy * (x - x1);

            if (yh.ENorm() > epsilon) 
                return false;
        }

        return true;
    }
    
    private static bool TestNearLinear(this IReadOnlyList<KeyValuePair<int, Float64Vector3D>> pointList, int index1, int index2, double yEpsilon)
    {
        var (x1, y1) = pointList[index1];
        var (x2, y2) = pointList[index2];
        var dx = x2 - x1;
        var dy = y2 - y1;
        var epsilon = dx * yEpsilon;

        for (var i = index1 + 1; i < index2; i++)
        {
            var (x, y) = pointList[i];

            var yh = dx * (y - y1) - dy * (x - x1);

            if (yh.ENorm() > epsilon) 
                return false;
        }

        return true;
    }
    
    private static bool TestNearLinear(this IReadOnlyList<KeyValuePair<int, Float64Quaternion>> pointList, int index1, int index2, double yEpsilon)
    {
        var (x1, y1) = pointList[index1];
        var (x2, y2) = pointList[index2];
        var dx = x2 - x1;
        var dy = y2 - y1;
        var epsilon = dx * yEpsilon;

        for (var i = index1 + 1; i < index2; i++)
        {
            var (x, y) = pointList[i];

            var yh = dx * (y - y1) - dy * (x - x1);

            if (yh.Norm() > epsilon) 
                return false;
        }

        return true;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<T> ToKeyFramesDictionary<T>(this IEnumerable<int> frameIndexList, Func<int, T> valueMapping)
    {
        return GrBabylonJsKeyFrameDictionary<T>.Create(
            frameIndexList,
            valueMapping
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<T> ToKeyFramesDictionary<T>(this IEnumerable<int> frameIndexList, Func<int, int> keyMapping, Func<int, T> valueMapping)
    {
        return GrBabylonJsKeyFrameDictionary<T>.Create(
            frameIndexList,
            keyMapping,
            valueMapping
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<T> ToKeyFramesDictionary<T>(this IEnumerable<KeyValuePair<int, T>> frameValuePairs, Func<KeyValuePair<int, T>, int> keyMapping, Func<KeyValuePair<int, T>, T> valueMapping)
    {
        return GrBabylonJsKeyFrameDictionary<T>.Create(
            frameValuePairs,
            keyMapping,
            valueMapping
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<T2> ToKeyFramesDictionary<T1, T2>(this IEnumerable<KeyValuePair<int, T1>> frameValuePairs, Func<KeyValuePair<int, T1>, int> keyMapping, Func<KeyValuePair<int, T1>, T2> valueMapping)
    {
        return GrBabylonJsKeyFrameDictionary<T2>.Create(
            frameValuePairs,
            keyMapping,
            valueMapping
        );
    }


    public static IEnumerable<int> OptimizeKeyFrameIndices(this GrBabylonJsKeyFrameDictionary<double> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        if (keyFrameDictionary.Count < 2)
            throw new InvalidOperationException();
        
        var pointArray = 
            keyFrameDictionary.ToImmutableArray();

        var pointIndex1 = 0;
        var pointIndex2 = 2;

        var frameIndex = pointArray[pointIndex1].Key;
        yield return frameIndex;

        while (pointIndex2 < pointArray.Length)
        {
            if (pointArray.TestNearLinear(pointIndex1, pointIndex2, valueEpsilon))
            {
                pointIndex2++;
                continue;
            }

            frameIndex = pointArray[pointIndex2 - 1].Key;
            yield return frameIndex;

            pointIndex1 = pointIndex2;
            pointIndex2 = pointIndex1 + 2;

            frameIndex = pointArray[pointIndex1].Key;
            yield return frameIndex;
        }

        pointIndex2 = Math.Min(pointIndex2, keyFrameDictionary.Count - 1);
        var frameIndex2 = pointArray[pointIndex2].Key;

        if (frameIndex2 != frameIndex)
            yield return frameIndex2;
    }

    public static IEnumerable<int> OptimizeKeyFrameIndices(this GrBabylonJsKeyFrameDictionary<Float64Vector2D> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        if (keyFrameDictionary.Count < 2)
            throw new InvalidOperationException();
        
        var pointArray = 
            keyFrameDictionary.ToImmutableArray();

        var pointIndex1 = 0;
        var pointIndex2 = 2;

        var frameIndex = pointArray[pointIndex1].Key;
        yield return frameIndex;

        while (pointIndex2 < pointArray.Length)
        {
            if (pointArray.TestNearLinear(pointIndex1, pointIndex2, valueEpsilon))
            {
                pointIndex2++;
                continue;
            }
            
            frameIndex = pointArray[pointIndex2 - 1].Key;
            yield return frameIndex;

            pointIndex1 = pointIndex2;
            pointIndex2 = pointIndex1 + 2;

            frameIndex = pointArray[pointIndex1].Key;
            yield return frameIndex;
        }

        pointIndex2 = Math.Min(pointIndex2, keyFrameDictionary.Count - 1);
        var frameIndex2 = pointArray[pointIndex2].Key;

        if (frameIndex2 != frameIndex)
            yield return frameIndex2;
    }

    public static IEnumerable<int> OptimizeKeyFrameIndices(this GrBabylonJsKeyFrameDictionary<Float64Vector3D> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        if (keyFrameDictionary.Count < 2)
            throw new InvalidOperationException();
        
        var pointArray = 
            keyFrameDictionary.ToImmutableArray();

        var pointIndex1 = 0;
        var pointIndex2 = 2;

        var frameIndex = pointArray[pointIndex1].Key;
        yield return frameIndex;

        while (pointIndex2 < pointArray.Length)
        {
            if (pointArray.TestNearLinear(pointIndex1, pointIndex2, valueEpsilon))
            {
                pointIndex2++;
                continue;
            }
            
            frameIndex = pointArray[pointIndex2 - 1].Key;
            yield return frameIndex;

            pointIndex1 = pointIndex2;
            pointIndex2 = pointIndex1 + 2;

            frameIndex = pointArray[pointIndex1].Key;
            yield return frameIndex;
        }

        pointIndex2 = Math.Min(pointIndex2, keyFrameDictionary.Count - 1);
        var frameIndex2 = pointArray[pointIndex2].Key;

        if (frameIndex2 != frameIndex)
            yield return frameIndex2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> OptimizeQuaternionKeyFrameIndices(this GrBabylonJsKeyFrameDictionary<Float64Quaternion> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        return keyFrameDictionary.ToKeyFramesDictionary(
            p => p.Key,
            p => p.Value.ToRotationVector()
        ).OptimizeKeyFrameIndices(valueEpsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<double> OptimizeKeyFrames(this GrBabylonJsKeyFrameDictionary<double> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        return keyFrameDictionary
            .OptimizeKeyFrameIndices(valueEpsilon)
            .ReduceKeyFrames(keyFrameDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<Float64Vector2D> OptimizeKeyFrames(this GrBabylonJsKeyFrameDictionary<Float64Vector2D> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        return keyFrameDictionary
            .OptimizeKeyFrameIndices(valueEpsilon)
            .ReduceKeyFrames(keyFrameDictionary);

        //var keyFrameXDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        //var keyFrameYDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        
        //foreach (var (frameIndex, vector) in keyFrameDictionary)
        //{
        //    keyFrameXDictionary.Add(frameIndex, vector.X);
        //    keyFrameYDictionary.Add(frameIndex, vector.Y);
        //}
        
        //var keyFrameXIndices = 
        //    keyFrameXDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        //var keyFrameYIndices = 
        //    keyFrameYDictionary.OptimizeKeyFrameIndices(valueEpsilon);
        
        //var frameIndexSet = new SortedSet<int>();

        //frameIndexSet.AddRange(keyFrameXIndices);
        //frameIndexSet.AddRange(keyFrameYIndices);
        
        //return keyFrameDictionary.ReduceKeyFrames(frameIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<Float64Vector3D> OptimizeKeyFrames(this GrBabylonJsKeyFrameDictionary<Float64Vector3D> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        return keyFrameDictionary
            .OptimizeKeyFrameIndices(valueEpsilon)
            .ReduceKeyFrames(keyFrameDictionary);

        //var keyFrameXDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        //var keyFrameYDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        //var keyFrameZDictionary = new GrBabylonJsKeyFrameDictionary<double>();

        //foreach (var (frameIndex, vector) in keyFrameDictionary)
        //{
        //    keyFrameXDictionary.Add(frameIndex, vector.X);
        //    keyFrameYDictionary.Add(frameIndex, vector.Y);
        //    keyFrameZDictionary.Add(frameIndex, vector.Z);
        //}
        
        //var keyFrameXIndices = 
        //    keyFrameXDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        //var keyFrameYIndices = 
        //    keyFrameYDictionary.OptimizeKeyFrameIndices(valueEpsilon);
        
        //var keyFrameZIndices = 
        //    keyFrameZDictionary.OptimizeKeyFrameIndices(valueEpsilon);
        
        //var frameIndexSet = new SortedSet<int>();

        //frameIndexSet.AddRange(keyFrameXIndices);
        //frameIndexSet.AddRange(keyFrameYIndices);
        //frameIndexSet.AddRange(keyFrameZIndices);
        
        //return keyFrameDictionary.ReduceKeyFrames(frameIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<Float64Quaternion> OptimizeQuaternionKeyFrames(this GrBabylonJsKeyFrameDictionary<Float64Quaternion> keyFrameDictionary, double valueEpsilon = 1e-4d)
    {
        return keyFrameDictionary
            .OptimizeQuaternionKeyFrameIndices(valueEpsilon)
            .ReduceKeyFrames(keyFrameDictionary);
    }


    public static Pair<GrBabylonJsKeyFrameDictionary<double>> SeparateComponents(this IEnumerable<KeyValuePair<int, Float64Vector2D>> keyFrameIndexVectorPairs, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate, double valueEpsilon = 1e-4d)
    {
        var keyFrameXDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        var keyFrameYDictionary = new GrBabylonJsKeyFrameDictionary<double>();

        foreach (var (frameIndex, vector) in keyFrameIndexVectorPairs)
        {
            keyFrameXDictionary.Add(frameIndex, vector.X);
            keyFrameYDictionary.Add(frameIndex, vector.Y);
        }

        if (optimizationKind == GrBabylonJsKeyFrameOptimizationKind.None)
            return new Pair<GrBabylonJsKeyFrameDictionary<double>>(
                keyFrameXDictionary,
                keyFrameYDictionary
            );
        
        var keyFrameXIndices = 
            keyFrameXDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        var keyFrameYIndices = 
            keyFrameYDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        if (optimizationKind == GrBabylonJsKeyFrameOptimizationKind.Separate)
            return new Pair<GrBabylonJsKeyFrameDictionary<double>>(
                keyFrameXDictionary.ReduceKeyFrames(keyFrameXIndices),
                keyFrameYDictionary.ReduceKeyFrames(keyFrameYIndices)
            );

        var frameIndexSet = new SortedSet<int>();

        frameIndexSet.AddRange(keyFrameXIndices);
        frameIndexSet.AddRange(keyFrameYIndices);
        
        return new Pair<GrBabylonJsKeyFrameDictionary<double>>(
            keyFrameXDictionary.ReduceKeyFrames(frameIndexSet),
            keyFrameYDictionary.ReduceKeyFrames(frameIndexSet)
        );
    }

    public static Triplet<GrBabylonJsKeyFrameDictionary<double>> SeparateComponents(this IEnumerable<KeyValuePair<int, Float64Vector3D>> keyFrameIndexVectorPairs, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate, double valueEpsilon = 1e-4d)
    {
        var keyFrameXDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        var keyFrameYDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        var keyFrameZDictionary = new GrBabylonJsKeyFrameDictionary<double>();

        foreach (var (frameIndex, vector) in keyFrameIndexVectorPairs)
        {
            keyFrameXDictionary.Add(frameIndex, vector.X);
            keyFrameYDictionary.Add(frameIndex, vector.Y);
            keyFrameZDictionary.Add(frameIndex, vector.Z);
        }
        
        if (optimizationKind == GrBabylonJsKeyFrameOptimizationKind.None)
            return new Triplet<GrBabylonJsKeyFrameDictionary<double>>(
                keyFrameXDictionary,
                keyFrameYDictionary,
                keyFrameZDictionary
            );
        
        var keyFrameXIndices = 
            keyFrameXDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        var keyFrameYIndices = 
            keyFrameYDictionary.OptimizeKeyFrameIndices(valueEpsilon);
        
        var keyFrameZIndices = 
            keyFrameZDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        if (optimizationKind == GrBabylonJsKeyFrameOptimizationKind.Separate)
            return new Triplet<GrBabylonJsKeyFrameDictionary<double>>(
                keyFrameXDictionary.ReduceKeyFrames(keyFrameXIndices),
                keyFrameYDictionary.ReduceKeyFrames(keyFrameYIndices),
                keyFrameZDictionary.ReduceKeyFrames(keyFrameZIndices)
            );

        var frameIndexSet = new SortedSet<int>();

        frameIndexSet.AddRange(keyFrameXIndices);
        frameIndexSet.AddRange(keyFrameYIndices);
        frameIndexSet.AddRange(keyFrameZIndices);
        
        return new Triplet<GrBabylonJsKeyFrameDictionary<double>>(
            keyFrameXDictionary.ReduceKeyFrames(frameIndexSet),
            keyFrameYDictionary.ReduceKeyFrames(frameIndexSet),
            keyFrameZDictionary.ReduceKeyFrames(frameIndexSet)
        );
    }

    public static Quad<GrBabylonJsKeyFrameDictionary<double>> SeparateComponents(this IEnumerable<KeyValuePair<int, Float64Quaternion>> keyFrameIndexVectorPairs, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate, double valueEpsilon = 1e-4d)
    {
        var keyFrameXDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        var keyFrameYDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        var keyFrameZDictionary = new GrBabylonJsKeyFrameDictionary<double>();
        var keyFrameWDictionary = new GrBabylonJsKeyFrameDictionary<double>();

        foreach (var (frameIndex, vector) in keyFrameIndexVectorPairs)
        {
            keyFrameXDictionary.Add(frameIndex, vector.ScalarI);
            keyFrameYDictionary.Add(frameIndex, vector.ScalarJ);
            keyFrameZDictionary.Add(frameIndex, vector.ScalarK);
            keyFrameWDictionary.Add(frameIndex, vector.Scalar);
        }
        
        if (optimizationKind == GrBabylonJsKeyFrameOptimizationKind.None)
            return new Quad<GrBabylonJsKeyFrameDictionary<double>>(
                keyFrameXDictionary,
                keyFrameYDictionary,
                keyFrameZDictionary,
                keyFrameWDictionary
            );
        
        var keyFrameXIndices = 
            keyFrameXDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        var keyFrameYIndices = 
            keyFrameYDictionary.OptimizeKeyFrameIndices(valueEpsilon);
        
        var keyFrameZIndices = 
            keyFrameZDictionary.OptimizeKeyFrameIndices(valueEpsilon);
        
        var keyFrameWIndices = 
            keyFrameWDictionary.OptimizeKeyFrameIndices(valueEpsilon);

        if (optimizationKind == GrBabylonJsKeyFrameOptimizationKind.Separate)
            return new Quad<GrBabylonJsKeyFrameDictionary<double>>(
                keyFrameXDictionary.ReduceKeyFrames(keyFrameXIndices),
                keyFrameYDictionary.ReduceKeyFrames(keyFrameYIndices),
                keyFrameZDictionary.ReduceKeyFrames(keyFrameZIndices),
                keyFrameWDictionary.ReduceKeyFrames(keyFrameWIndices)
            );

        var frameIndexSet = new SortedSet<int>();

        frameIndexSet.AddRange(keyFrameXIndices);
        frameIndexSet.AddRange(keyFrameYIndices);
        frameIndexSet.AddRange(keyFrameZIndices);
        frameIndexSet.AddRange(keyFrameWIndices);
        
        return new Quad<GrBabylonJsKeyFrameDictionary<double>>(
            keyFrameXDictionary.ReduceKeyFrames(frameIndexSet),
            keyFrameYDictionary.ReduceKeyFrames(frameIndexSet),
            keyFrameZDictionary.ReduceKeyFrames(frameIndexSet),
            keyFrameWDictionary.ReduceKeyFrames(frameIndexSet)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsConstant(this GrBabylonJsKeyFrameDictionary<double> keyFrames)
    {
        return keyFrames.IsConstant(
            (v1, v2) => 
                (v1 - v2).IsNearZero()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsConstant(this GrBabylonJsKeyFrameDictionary<Float64Vector2D> keyFrames)
    {
        return keyFrames.IsConstant(
            (v1, v2) => 
                v1.GetDistanceSquaredToPoint(v2).IsNearZero()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsConstant(this GrBabylonJsKeyFrameDictionary<Float64Vector3D> keyFrames)
    {
        return keyFrames.IsConstant(
            (v1, v2) => 
                v1.GetDistanceSquaredToPoint(v2).IsNearZero()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsConstant(this GrBabylonJsKeyFrameDictionary<Float64Quaternion> keyFrames)
    {
        return keyFrames.IsConstant(
            (v1, v2) => 
                (v1 - v2).NormSquared().IsNearZero()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsQuaternionConstant(this GrBabylonJsKeyFrameDictionary<Float64Quaternion> keyFrames)
    {
        return keyFrames.IsConstant(
            (v1, v2) => 
                v1.ToRotationVector().GetDistanceSquaredToPoint(v2.ToRotationVector()).IsNearZero()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrBabylonJsKeyFrameDictionary<T> ReduceKeyFrames<T>(this IEnumerable<int> frameIndexList, GrBabylonJsKeyFrameDictionary<T> keyFrameDictionary)
    {
        return frameIndexList.ToKeyFramesDictionary(
            frameIndex => keyFrameDictionary[frameIndex]
        );
    }
}