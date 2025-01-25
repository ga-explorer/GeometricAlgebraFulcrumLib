using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using Newtonsoft.Json.Linq;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

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
    
    
    internal GrBabylonJsAnimationOfFloat(string constName, string targetPropertyName, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionaryCache keyFramesCache, int keyFramesCacheIndex)
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