using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/copies/clones
/// </summary>
public sealed class GrBabylonJsMeshClone :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "clone";

    public string ParentMeshConstName { get; }

    public SparseCodeAttributeValue? NewParent { get; init; }

    public GrBabylonJsBooleanValue? DoNotCloneChildren { get; init; }

    public GrBabylonJsBooleanValue? ClonePhysicsImpostor { get; init; }

    public override GrBabylonJsObjectOptions? ObjectOptions
        => null;


    public GrBabylonJsMeshClone(string constName, string parentMeshConstName)
        : base(constName)
    {
        ParentMeshConstName = parentMeshConstName;
    }
    
    
    public GrBabylonJsMeshClone SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }

    
    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        
        if (NewParent is null || NewParent.IsEmpty) yield break;
        yield return NewParent.GetCode();

        if (DoNotCloneChildren is null || DoNotCloneChildren.IsEmpty) yield break;
        yield return DoNotCloneChildren.GetCode();
        
        if (ClonePhysicsImpostor is null || ClonePhysicsImpostor.IsEmpty) yield break;
        yield return ClonePhysicsImpostor.GetCode();
    }
    
    public override string GetCode()
    {
        var composer = new StringBuilder();

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();
        
        if (!string.IsNullOrEmpty(ConstName))
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            composer.Append($"{declarationKeyword} {ConstName} = ");
        }

        composer
            .Append(ParentMeshConstName)
            .Append('.')
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);

        return composer.ToString();
    }
}