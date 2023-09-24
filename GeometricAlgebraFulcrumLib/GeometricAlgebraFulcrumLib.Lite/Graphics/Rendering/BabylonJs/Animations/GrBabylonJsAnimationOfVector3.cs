using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;
using Newtonsoft.Json.Linq;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Animations;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.Animation
/// </summary>
public sealed class GrBabylonJsAnimationOfVector3 :
    GrBabylonJsAnimation
{
    public override GrBabylonJsAnimationType TargetPropertyDataType
        => GrBabylonJsAnimationType.Vector3;
    
    public override bool IsConstant
        => KeyFramesCache
            .Vector3KeyFramesCache[KeyFramesIndex]
            .IsConstant();

    
    internal GrBabylonJsAnimationOfVector3(string constName, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionaryCache keyFramesCache, int keyFramesCacheIndex)
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
            .Vector3KeyFramesCache[KeyFramesIndex]
            .Values
            .First()
            .GetBabylonJsCode();
    }
}