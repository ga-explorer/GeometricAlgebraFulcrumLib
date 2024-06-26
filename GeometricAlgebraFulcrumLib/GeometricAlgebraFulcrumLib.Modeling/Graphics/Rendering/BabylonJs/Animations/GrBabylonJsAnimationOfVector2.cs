﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using Newtonsoft.Json.Linq;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.Animation
/// </summary>
public sealed class GrBabylonJsAnimationOfVector2 :
    GrBabylonJsAnimation
{
    public override GrBabylonJsAnimationType TargetPropertyDataType
        => GrBabylonJsAnimationType.Vector2;

    public override bool IsConstant
        => KeyFramesCache
            .Vector2KeyFramesCache[KeyFramesIndex]
            .IsConstant();

    
    internal GrBabylonJsAnimationOfVector2(string constName, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionaryCache keyFramesCache, int keyFramesCacheIndex)
        : base(
            constName, 
            targetPropertyName, 
            animationSpecs, 
            keyFramesCache, 
            keyFramesCacheIndex
        )
    {
    }
    
    
    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return TargetPropertyName.DoubleQuote();
        yield return AnimationSpecs.FrameRate.GetBabylonJsCode();
        yield return TargetPropertyDataType.GetBabylonJsCode();
        yield return AnimationSpecs.LoopMode.GetBabylonJsCode();
        yield return AnimationSpecs.EnableBlending.GetBabylonJsCode();
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
            .Vector2KeyFramesCache[KeyFramesIndex]
            .Values
            .First()
            .GetBabylonJsCode();
    }
}