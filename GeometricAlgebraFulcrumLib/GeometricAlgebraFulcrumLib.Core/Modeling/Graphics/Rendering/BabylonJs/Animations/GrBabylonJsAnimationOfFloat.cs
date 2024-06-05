using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;
using Newtonsoft.Json.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Animations;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.Animation
/// </summary>
public sealed class GrBabylonJsAnimationOfFloat :
    GrBabylonJsAnimation
{
    public override GrBabylonJsAnimationType TargetPropertyDataType 
        => GrBabylonJsAnimationType.Float;
    
    public override bool IsConstant
        => KeyFramesCache
            .FloatKeyFramesCache[KeyFramesIndex]
            .IsConstant();
    
    
    internal GrBabylonJsAnimationOfFloat(string constName, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionaryCache keyFramesCache, int keyFramesCacheIndex)
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
        throw new NotImplementedException();
        //return KeyFrames.GetJson();
    }

    public override string GetConstantValueCode()
    {
        Debug.Assert(IsConstant);

        return KeyFramesCache
            .FloatKeyFramesCache[KeyFramesIndex]
            .Values
            .First()
            .GetBabylonJsCode();
    }
}