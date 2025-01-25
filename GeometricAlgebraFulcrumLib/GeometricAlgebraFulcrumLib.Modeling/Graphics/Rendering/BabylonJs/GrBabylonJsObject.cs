using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public abstract class GrBabylonJsObject :
    IGrBabylonJsCodeElement
{
    public string ConstName { get; }

    public bool UseLetDeclaration { get; set; }

    protected abstract string ConstructorName { get; }

    public abstract GrBabylonJsObjectOptions? ObjectOptions { get; }

    public abstract GrBabylonJsObjectProperties? ObjectProperties { get; }


    protected GrBabylonJsObject(string constName)
    {
        ConstName = constName;
    }


    protected abstract IEnumerable<string> GetConstructorArguments();

    public string GetConstructorCode()
    {
        var constructorArguments = 
            GetConstructorArguments()
                .Where(p => !string.IsNullOrEmpty(p))
                .Concatenate(", ");

        return $"{ConstructorName}({constructorArguments});";
    }

    public string GetPropertiesCode()
    {
        if (ObjectProperties is null)
            return string.Empty;

        return string.IsNullOrEmpty(ConstName)
            ? ObjectProperties.GetAttributeSetCode()
            : ObjectProperties.GetCode(ConstName);
    }

    public virtual string GetBabylonJsCode()
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
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);

        return composer.ToString();
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(ConstName)
            ? GetBabylonJsCode() 
            : ConstName;
    }
}