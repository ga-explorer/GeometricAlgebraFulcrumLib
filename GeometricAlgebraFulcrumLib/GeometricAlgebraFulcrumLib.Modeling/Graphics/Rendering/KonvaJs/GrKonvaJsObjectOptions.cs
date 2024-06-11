using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs;

public abstract class GrKonvaJsObjectOptions :
    GrKonvaJsAttributeSet
{
    public override string GetCode()
    {
        var keyValuePairs = 
            GetKeyValueCodePairs().Select(pair => 
                $"{pair.Key}: {pair.Value}"
            ).Concatenate("," + Environment.NewLine);
            
        return new LinearTextComposer()
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(keyValuePairs)
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
        
    public override string ToString()
    {
        return GetCode();
    }
}