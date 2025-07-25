﻿using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteIncludeCodeModules : SteSyntaxElement
{
    public List<string> IncludedModules { get; private set; }


    public SteIncludeCodeModules()
    {
        IncludedModules = new List<string>();
    }

    public SteIncludeCodeModules(IEnumerable<string> includedModules)
    {
        IncludedModules = new List<string>(includedModules);
    }

}