using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs;

public abstract class GrKonvaJsObject :
    IGrKonvaJsObject
{
    public string ConstName { get; }

    public bool UseLetDeclaration { get; set; }

    protected abstract string ConstructorName { get; }

    public abstract GrKonvaJsObjectOptions? ObjectOptions { get; }

    public abstract GrKonvaJsObjectProperties? ObjectProperties { get; }


    protected GrKonvaJsObject(string constName)
    {
        if (string.IsNullOrEmpty(constName))
            throw new ArgumentException(nameof(constName));

        ConstName = constName;
    }


    protected virtual IEnumerable<string> GetConstructorArguments()
    {
        if (ObjectOptions is null)
            yield return "{}";
        else
            yield return ObjectOptions.GetCode();
    }

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
            ? ObjectProperties.GetCode()
            : ObjectProperties.GetCode(ConstName);
    }

    public abstract string GetCode();
    //public virtual string GetCode()
    //{
    //    var composer = new StringBuilder();

    //    var constructorCode = GetConstructorCode();
    //    var propertiesCode = GetPropertiesCode();
            
    //    if (!string.IsNullOrEmpty(ConstName))
    //    {
    //        var declarationKeyword = UseLetDeclaration ? "let" : "const";

    //        composer.Append($"{declarationKeyword} {ConstName} = ");
    //    }

    //    composer
    //        .AppendLine(constructorCode)
    //        .AppendLine(propertiesCode);

    //    return composer.ToString();
    //}

    public override string ToString()
    {
        return string.IsNullOrEmpty(ConstName)
            ? GetCode() 
            : ConstName;
    }
}