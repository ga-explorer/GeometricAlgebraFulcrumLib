using System.Collections.Immutable;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs;

public abstract class GrKonvaJsObjectProperties :
    GrKonvaJsAttributeSet
{
    public string? ObjectName { get; set; }

        
    public string GetCode(string objectName)
    {
        ObjectName = objectName;

        return GetCode();
    }

    public override string GetCode()
    {
        if (string.IsNullOrEmpty(ObjectName))
            throw new InvalidOperationException();

        var composer = new StringBuilder();

        var valuePairs = 
            GetKeyValueCodePairs().ToImmutableArray();

        if (valuePairs.Length > 0)
        {
            foreach (var (name, value) in valuePairs)
                composer.AppendLine($"{ObjectName}.{name}({value});");
            //composer.AppendLine($"{ObjectName}.{name} = {value};");

            composer.Length -= Environment.NewLine.Length;
        }

        return composer.ToString();
    }
    
    public override string ToString()
    {
        return GetCode();
    }
}