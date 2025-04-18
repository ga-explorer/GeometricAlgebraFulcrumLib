﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using Newtonsoft.Json.Linq;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.Animation
/// </summary>
public sealed class GrBabylonJsAnimationOfQuaternion :
    GrBabylonJsAnimation
{
    public override GrBabylonJsAnimationType TargetPropertyDataType
        => GrBabylonJsAnimationType.Quaternion;
    
    public override bool IsConstant
        => KeyFramesCache
            .QuaternionKeyFramesCache[KeyFramesIndex]
            .IsConstant();

    
    internal GrBabylonJsAnimationOfQuaternion(string constName, string targetPropertyName, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionaryCache keyFramesCache, int keyFramesCacheIndex)
        : base(
            constName, 
            targetPropertyName, 
            samplingSpecs, 
            keyFramesCache, 
            keyFramesCacheIndex
        )
    {
    }
    
    
    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return TargetPropertyName.DoubleQuote();
        yield return SamplingSpecs.FrameRate.GetBabylonJsCode();
        yield return TargetPropertyDataType.GetBabylonJsCode();
        yield return SamplingSpecs.LoopMode.GetBabylonJsCode();
        yield return SamplingSpecs.EnableBlending.GetBabylonJsCode();
    }

    public override JArray GetKeyFramesJson()
    {
        //return KeyFrames.GetJson();
        throw new NotImplementedException();
    }
    
    public override string GetConstantValueCode()
    {
        Debug.Assert(IsConstant);

        return KeyFramesCache
            .QuaternionKeyFramesCache[KeyFramesIndex]
            .Values
            .First()
            .GetBabylonJsCode();
    }
}