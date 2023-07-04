using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Animations;

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

    
    internal GrBabylonJsAnimationOfQuaternion(string constName, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionaryCache keyFramesCache, int keyFramesCacheIndex)
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
            .QuaternionKeyFramesCache[KeyFramesIndex]
            .Values
            .First()
            .GetBabylonJsCode();
    }
}