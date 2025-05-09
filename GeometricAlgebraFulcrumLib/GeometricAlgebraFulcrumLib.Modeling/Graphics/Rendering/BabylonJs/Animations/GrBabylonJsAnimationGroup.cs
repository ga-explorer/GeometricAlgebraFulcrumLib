using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.AnimationGroup
/// </summary>
public sealed class GrBabylonJsAnimationGroup :
    GrBabylonJsObject,
    IReadOnlyList<Tuple<string, GrBabylonJsAnimation>>
{
    private readonly List<Tuple<string, GrBabylonJsAnimation>> _targetedAnimationList
        = new List<Tuple<string, GrBabylonJsAnimation>>();


    protected override string ConstructorName
        => "new BABYLON.AnimationGroup";
    
    public GrBabylonJsSceneValue? ParentScene { get; set; }

    public string SceneVariableName 
        => ParentScene?.Value.ConstName ?? string.Empty;

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;
    
    public GrBabylonJsAnimationGroupProperties Properties { get; private set; }
        = new GrBabylonJsAnimationGroupProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;
    
    public int Count 
        => _targetedAnimationList.Count;

    public int TargetCount 
        => TargetNames.Count();

    public Tuple<string, GrBabylonJsAnimation> this[int index] 
        => _targetedAnimationList[index];

    public IEnumerable<string> TargetNames 
        => _targetedAnimationList.Select(
            t => t.Item1
        ).Distinct();

    public IEnumerable<Tuple<string, GrBabylonJsAnimation>> ConstantAnimations
        => _targetedAnimationList.Where(t => 
            t.Item2.IsConstant
        );
    
    public IEnumerable<Tuple<string, GrBabylonJsAnimation>> NonConstantAnimations
        => _targetedAnimationList.Where(t => 
            !t.Item2.IsConstant
        );


    public GrBabylonJsAnimationGroup(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsAnimationGroup(string constName, GrBabylonJsSceneValue scene)
        : base(constName)
    {
        ParentScene = scene;
    }


    public GrBabylonJsAnimationGroup AddAnimation(string targetName, GrBabylonJsAnimation animation)
    {
        _targetedAnimationList.Add(
            new Tuple<string, GrBabylonJsAnimation>(targetName, animation)
        );

        return this;
    }
    
    public GrBabylonJsAnimationGroup AddAnimations(string targetName, IPair<GrBabylonJsAnimation> animationList)
    {
        return AddAnimations(
            targetName,
            animationList.Item1,
            animationList.Item2
        );
    }
    
    public GrBabylonJsAnimationGroup AddAnimations(string targetName, ITriplet<GrBabylonJsAnimation> animationList)
    {
        return AddAnimations(
            targetName,
            animationList.Item1,
            animationList.Item2,
            animationList.Item3
        );
    }
    
    public GrBabylonJsAnimationGroup AddAnimations(string targetName, IQuad<GrBabylonJsAnimation> animationList)
    {
        return AddAnimations(
            targetName,
            animationList.Item1,
            animationList.Item2,
            animationList.Item3,
            animationList.Item4
        );
    }

    public GrBabylonJsAnimationGroup AddAnimations(string targetName, params GrBabylonJsAnimation[] animationList)
    {
        foreach (var animation in animationList)
            _targetedAnimationList.Add(
                new Tuple<string, GrBabylonJsAnimation>(targetName, animation)
            );

        return this;
    }

    
    public GrBabylonJsAnimationGroup SetProperties(GrBabylonJsAnimationGroupProperties properties)
    {
        Properties = new GrBabylonJsAnimationGroupProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (ParentScene is null || ParentScene.IsEmpty) yield break;
        yield return ParentScene.GetAttributeValueCode();
    }

    private bool AllSimilarAnimationSpecs(IReadOnlyList<Tuple<string, GrBabylonJsAnimation>> targetedAnimationList)
    {
        if (targetedAnimationList.Count < 2)
            return true;

        var specs1 = targetedAnimationList[0].Item2.SamplingSpecs;

        for (var i = 1; i < targetedAnimationList.Count; i++)
        {
            var specs2 = targetedAnimationList[i].Item2.SamplingSpecs;

            if (specs2.Loop != specs1.Loop)
                return false;

            if (specs2.FrameRange != specs1.FrameRange)
                return false;

            specs1 = specs2;
        }

        return true;
    }

    public Int32Range1D GetFrameRange()
    {
        var frameRange0 = 
            _targetedAnimationList[0].Item2.SamplingSpecs.FrameRange;

        return _targetedAnimationList
            .Select(f => 
                f.Item2.SamplingSpecs.FrameRange
            ).Aggregate(frameRange0, Int32Range1D.Create);
    }

    public override string GetBabylonJsCode()
    {
        var codeComposer = new LinearTextComposer();

        //Generate constant animations first
        {
            foreach (var (targetName, animation) in ConstantAnimations)
            {
                codeComposer.AppendLine($"{targetName}.{animation.TargetPropertyName} = {animation.GetConstantValueCode()};");
            }
        }

        var nonConstantAnimations = 
            NonConstantAnimations.ToImmutableArray();

        if (nonConstantAnimations.Length == 0)
            return codeComposer.ToString();

        // This does not work properly when the targetName is a property of a mesh, 
        // not the mesh itself
        //if (nonConstantAnimations.Length == 1)
        //{
        //    var (targetName, animation) = nonConstantAnimations[0];
        //    var fromFrame = animation.SamplingSpecs.FrameRange.MinValue.GetBabylonJsCode();
        //    var toFrame = animation.SamplingSpecs.FrameRange.MaxValue.GetBabylonJsCode();
        //    var loopAnimation = animation.SamplingSpecs.Loop.GetBabylonJsCode();
        //    var speedRatio = 
        //        Properties is null
        //        ? 1d.GetBabylonJsCode()
        //        : (Properties.SpeedRatio?.GetCode() ?? 1d.GetBabylonJsCode());

        //    return codeComposer
        //        .AppendLine($"{targetName}.animations.push({animation.ConstName});")
        //        .AppendLine($"{SceneVariableName}.beginAnimation({targetName}, {fromFrame}, {toFrame}, {loopAnimation}, {speedRatio});")
        //        .ToString();
        //}

        var targetNames = nonConstantAnimations.Select(
            t => t.Item1
        ).Distinct().ToImmutableArray();

        if (targetNames.Length == 1 && AllSimilarAnimationSpecs(nonConstantAnimations))
        {
            var (targetName, animation1) =
                nonConstantAnimations[0];

            var animationList =
                nonConstantAnimations
                    .Select(p => p.Item2)
                    .ToImmutableArray();

            var animations =
                animationList
                    .Select(a => a.ConstName)
                    .Concatenate(", ", "[", "]");

            var fromFrame = animation1.SamplingSpecs.FrameRange.MinValue.GetBabylonJsCode();
            var toFrame = animation1.SamplingSpecs.FrameRange.MaxValue.GetBabylonJsCode();
            var loopAnimation = animation1.SamplingSpecs.Loop.GetBabylonJsCode();
            var speedRatio =
                Properties.SpeedRatio?.GetAttributeValueCode() ?? 1d.GetBabylonJsCode();

            return codeComposer
                .AppendLine($"{SceneVariableName}.beginDirectAnimation({targetName}, {animations}, {fromFrame}, {toFrame}, {loopAnimation}, {speedRatio});")
                .ToString();
        }

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();
        
        if (!string.IsNullOrEmpty(ConstName))
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            codeComposer.Append($"{declarationKeyword} {ConstName} = ");
        }

        codeComposer
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);
        
        if (nonConstantAnimations.Length == 0)
            return codeComposer.ToString();

        foreach (var (targetName, animation) in nonConstantAnimations)
            codeComposer.AppendLine(
                $"{ConstName}.addTargetedAnimation({animation.ConstName}, {targetName});"
            );

        var (frame1, frame2) = GetFrameRange();
        var loop = 
            Properties.LoopAnimation?.GetAttributeValueCode() ?? true.GetBabylonJsCode();
        
        codeComposer.AppendLine(
            $"{ConstName}.normalize({frame1.GetBabylonJsCode()}, {frame2.GetBabylonJsCode()});"
        );

        codeComposer.AppendLine(
            $"{ConstName}.play({loop});"
        );

        return codeComposer.ToString();
    }

    public IEnumerator<Tuple<string, GrBabylonJsAnimation>> GetEnumerator()
    {
        return _targetedAnimationList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}