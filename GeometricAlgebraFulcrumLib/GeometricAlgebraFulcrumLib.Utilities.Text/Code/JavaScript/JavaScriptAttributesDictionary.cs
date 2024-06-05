using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Attributes;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

public class JavaScriptAttributesDictionary 
    : TextAttributesDictionary
{
    public JavaScriptAttributesDictionary()
    {
        KeyValueSeparator = ": ";
        AttributesSeparator = "," + Environment.NewLine;
    }

}