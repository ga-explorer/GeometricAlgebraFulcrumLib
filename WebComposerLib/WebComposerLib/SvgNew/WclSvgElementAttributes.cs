﻿using System.Text;
using DataStructuresLib.Basic;

namespace WebComposerLib.SvgNew;

public abstract class WclSvgElementAttributes :
    IWclSvgCodeElement
{
    protected abstract IEnumerable<Pair<string>?> GetNameValuePairs();
        
    public string GetCode()
    {
        var composer = new StringBuilder();

        var valuePairs = 
            GetNameValuePairs().Where(p => p is not null);

        foreach (var (name, value) in valuePairs)
            composer.AppendLine($"{name}={value};");

        return composer.ToString();
    }
    
    public override string ToString()
    {
        return GetCode();
    }
}