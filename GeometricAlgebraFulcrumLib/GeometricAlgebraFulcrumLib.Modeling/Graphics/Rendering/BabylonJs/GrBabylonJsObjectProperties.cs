using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public abstract class GrBabylonJsObjectProperties :
    GrBabylonJsAttributeSet
{
    public string? ObjectName { get; set; }


    public string GetCode(string objectName)
    {
        ObjectName = objectName;

        return GetAttributeSetCode();
    }

    public override string GetBabylonJsCode()
    {
        if (string.IsNullOrEmpty(ObjectName))
            throw new InvalidOperationException();

        return GetKeyValueCodePairs().Select(
            p => $"{ObjectName}.{p.Key} = {p.Value};"
        ).Concatenate(Environment.NewLine);
    }
    
    public override string ToString()
    {
        return GetAttributeSetCode();
    }
}