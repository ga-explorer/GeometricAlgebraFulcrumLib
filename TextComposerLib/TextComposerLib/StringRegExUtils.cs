using System;
using System.Text.RegularExpressions;

namespace TextComposerLib;

public static class StringRegExUtils
{
    public static string RegExRemoveAll(this string inputString, string regExString, RegexOptions options = RegexOptions.None)
    {
        return inputString.RegExReplaceAll(
            regExString, 
            string.Empty, 
            options
        );
    }

    public static string RegExReplaceAll(this string inputString, string regExString, string replaceString, RegexOptions options = RegexOptions.None)
    {
        return new Regex(regExString, options).Replace(
            inputString, 
            replaceString
        );
    }
    
    public static string RegExReplaceAll(this string inputString, string regExString, Func<string, string> replaceStringFunc, RegexOptions options = RegexOptions.None)
    {
        string MatchEvalFunc(Match match)
        {
            return replaceStringFunc(match.Value);
        }
        
        return new Regex(regExString, options).Replace(
            inputString, 
            new MatchEvaluator(MatchEvalFunc)
        );
    }
}