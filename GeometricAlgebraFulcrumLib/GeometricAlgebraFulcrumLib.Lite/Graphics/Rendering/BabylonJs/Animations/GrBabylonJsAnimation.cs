using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;
using Newtonsoft.Json.Linq;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Animations;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.Animation
/// </summary>
public abstract class GrBabylonJsAnimation :
    GrBabylonJsObject
{
    protected override string ConstructorName
        => "new BABYLON.Animation";
    
    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public string TargetPropertyName { get; }
    
    public abstract GrBabylonJsAnimationType TargetPropertyDataType { get; }

    public GrBabylonJsKeyFrameDictionaryCache KeyFramesCache { get; }

    public int KeyFramesIndex { get; }

    public string KeyFramesName 
        => GrBabylonJsKeyFrameDictionaryCache.GetNameFromIndex(
            KeyFramesIndex, 
            TargetPropertyDataType
        );

    public GrBabylonJsAnimationSpecs AnimationSpecs { get; }
    
    public override GrBabylonJsObjectProperties? ObjectProperties
        => null;

    public abstract bool IsConstant { get; }

    
    protected GrBabylonJsAnimation(string constName, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionaryCache keyFramesCache, int keyFramesCacheIndex)
        : base(constName)
    {
        TargetPropertyName = targetPropertyName;
        AnimationSpecs = animationSpecs;
        KeyFramesCache = keyFramesCache;
        KeyFramesIndex = keyFramesCacheIndex;
    }


    public abstract JArray GetKeyFramesJson();

    public abstract string GetConstantValueCode();

    public override string GetCode()
    {
        if (IsConstant)
            return string.Empty;

        var composer = new LinearTextComposer();

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();
        
        if (!string.IsNullOrEmpty(ConstName))
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            composer.Append($"{declarationKeyword} {ConstName} = ");
        }

        composer
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);
        
        composer.AppendLine($"{ConstName}.setKeys({KeyFramesName});");
        
        return composer.ToString();
    }
}
