﻿namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public sealed class SteFixedCode : SteSyntaxElement
{
    public string Text { get; private set; }


    public SteFixedCode(string text)
    {
        Text = text;
    }
}