using System;
using TextComposerLib.Text.Attributes;

namespace TextComposerLib.Code.JavaScript;

public class JavaScriptAttributesDictionary 
    : TextAttributesDictionary
{
    public JavaScriptAttributesDictionary()
    {
        KeyValueSeparator = ": ";
        AttributesSeparator = "," + Environment.NewLine;
    }

}