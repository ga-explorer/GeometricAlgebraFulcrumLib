using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextComposerLib.Text.Parametric;

public class ParametricTextComposerCollection : IDictionary<string, ParametricTextComposer>
{
    private static string[] SplitOnWhitespaces(string text)
    {
        return text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
    }

    private static bool ParseCommentOrEmpty(string fileLine)
    {
        var text = fileLine.Trim();

        //An emprty line
        if (string.IsNullOrEmpty(text))
            return true;

        //A comment
        return (text.Substring(0, 2) == "//");
    }

    private static bool ParseDelimiters(string fileLine, out string leftDel, out string rightDel)
    {
        leftDel = string.Empty;
        rightDel = string.Empty;
        var textParts = SplitOnWhitespaces(fileLine);

        if (textParts.Length < 2 || textParts[0] != "delimiters")
            return false;

        leftDel = textParts[1];

        if (textParts.Length == 2)
        {
            rightDel = leftDel;

            return true;
        }

        rightDel = textParts[2];

        return true;
    }

    private static bool ParseBeginTemplate(string fileLine, out string templateName)
    {
        templateName = string.Empty;
        var textParts = SplitOnWhitespaces(fileLine);

        if (textParts.Length < 2 || textParts[0] != "begin")
            return false;

        templateName = textParts[1];

        return true;
    }

    private static bool ParseEndTemplate(string fileLine, string templateName)
    {
        var textParts = SplitOnWhitespaces(fileLine);

        if (textParts.Length < 2 || textParts[0] != "end")
            return false;

        return templateName == textParts[1];
    }


    private bool _ignoreUndefinedParameters = true;

    private bool _alignMultiLineParameterValues = true;

    protected Dictionary<string, ParametricTextComposer> ComposersDictionary =
        new Dictionary<string, ParametricTextComposer>();


    /// <summary>
    /// If true (the dafault) reading and writting values to binding parameters not defined in the template
    /// is ignored and no errors are raised
    /// </summary>
    public bool IgnoreUndefinedParameters
    {
        get => _ignoreUndefinedParameters;
        set
        {
            _ignoreUndefinedParameters = value;

            foreach (var template in ComposersDictionary.Values)
                template.IgnoreUndefinedParameters = value;
        } 
    }

    /// <summary>
    /// If true, when a parameter value has multiple lines the second and following lines are
    /// added with leading spaces equal to the characters before the parameter placeholder
    /// up to the beginning of line
    /// </summary>
    public bool AlignMultiLineParameterValues 
    {
        get => _alignMultiLineParameterValues;
        set
        {
            _alignMultiLineParameterValues = value;

            foreach (var template in ComposersDictionary.Values)
                template.AlignMultiLineParameterValues = value;
        } 
    }


    /// <summary>
    /// Create a new ParametricTextBuilderCollection object and add some templates from this object
    /// (by reference) to the new object
    /// </summary>
    /// <param name="templatesNames"></param>
    /// <returns></returns>
    public ParametricTextComposerCollection SubCollection(IEnumerable<string> templatesNames)
    {
        var subCollection = new ParametricTextComposerCollection
        {
            _alignMultiLineParameterValues = _ignoreUndefinedParameters,
            _ignoreUndefinedParameters = _ignoreUndefinedParameters
        };

        foreach (var templateName in templatesNames)
        {
            if (
                ComposersDictionary.TryGetValue(templateName, out var template) && 
                subCollection.ComposersDictionary.ContainsKey(templateName) == false
            )
                subCollection.ComposersDictionary.Add(templateName, template);
        }

        return subCollection;
    }

    /// <summary>
    /// Create a new ParametricTextBuilderCollection object and add some templates from this object
    /// (by reference) to the new object
    /// </summary>
    /// <param name="templatesNames"></param>
    /// <returns></returns>
    public ParametricTextComposerCollection SubCollection(params string[] templatesNames)
    {
        return SubCollection((IEnumerable<string>)templatesNames);
    }


    private void AddTemplate(string[] fileLines, int startLine, int endLine, string templateName, string leftDel, string rightDel)
    {
        var templateText = new StringBuilder();

        //Create the template text
        for (var line = startLine; line <= endLine; line++)
            if (line < endLine)
                templateText.AppendLine(fileLines[line]);

            else
                templateText.Append(fileLines[line]);

        //Create the template
        var template =
            new ParametricTextComposer(leftDel, rightDel, templateText.ToString())
            {
                AlignMultiLineParameterValues = AlignMultiLineParameterValues,
                IgnoreUndefinedParameters = IgnoreUndefinedParameters
            };

        //Add the template to the internal dictionary
        if (ComposersDictionary.ContainsKey(templateName))
            ComposersDictionary[templateName] = template;

        else
            ComposersDictionary.Add(templateName, template);
    }

    private void ParseTemplates(string[] fileLines, int curLine, string leftDel, string rightDel)
    {
        while (curLine < fileLines.Length)
        {
            //Ignore all leading empty lines
            while (curLine < fileLines.Length && ParseCommentOrEmpty(fileLines[curLine]))
                curLine++;

            //All lines are empty! add no templates
            if (curLine >= fileLines.Length)
                return;

            //Found a non-empty line. It should be a begin template directive. If not, parse the next line
            if (ParseBeginTemplate(fileLines[curLine], out var templateName) == false)
            {
                curLine++;
                continue;
            }
                
            curLine++;

            var templateStartLine = curLine;

            while (curLine < fileLines.Length && ParseEndTemplate(fileLines[curLine], templateName) == false)
                curLine++;

            ////No end template directive found! add no templates
            //if (curLine >= fileLines.Length)
            //    return;

            AddTemplate(fileLines, templateStartLine, curLine - 1, templateName, leftDel, rightDel);

            curLine++;
        }
    }

    private void ParseLines(string[] fileLines)
    {
        int curLine = 0;

        //Ignore all leading comments and empty lines
        while (curLine < fileLines.Length && ParseCommentOrEmpty(fileLines[curLine]))
            curLine++;

        //All lines are empty! add no templates
        if (curLine >= fileLines.Length)
            return;

        //First directive must specify parameters delimiters used in all following templates
        if (ParseDelimiters(fileLines[curLine], out var leftDel, out var rightDel) == false)
            return;

        ParseTemplates(fileLines, curLine, leftDel, rightDel);
    }

    public void Parse(string text)
    {
        ParseLines(text.SplitLines());
    }

    public void ParseFile(string filePath)
    {
        ParseLines(File.ReadAllLines(filePath));
    }

    public void SaveToFile(string filePath)
    {
        var s = new StringBuilder();

        foreach (var pair in ComposersDictionary)
        {
            s.Append("delimiters ")
                .Append(pair.Value.LeftDelimiter)
                .Append(" ")
                .AppendLine(pair.Value.RightDelimiter);

            s.Append("begin ")
                .AppendLine(pair.Key);

            s.AppendLine(pair.Value.TemplateText);

            s.Append("end ")
                .AppendLine(pair.Key)
                .AppendLine();
        }

        File.WriteAllText(filePath, s.ToString());
    }


    public void Add(string key, ParametricTextComposer value)
    {
        ComposersDictionary.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return ComposersDictionary.ContainsKey(key);
    }

    public ICollection<string> Keys => ComposersDictionary.Keys;

    public bool Remove(string key)
    {
        return ComposersDictionary.Remove(key);
    }

    public bool TryGetValue(string key, out ParametricTextComposer value)
    {
        return ComposersDictionary.TryGetValue(key, out value);
    }

    public ICollection<ParametricTextComposer> Values => ComposersDictionary.Values;

    public ParametricTextComposer this[string key]
    {
        get => ComposersDictionary[key];
        set => ComposersDictionary[key] = value;
    }

    public void Add(KeyValuePair<string, ParametricTextComposer> item)
    {
        ComposersDictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        ComposersDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, ParametricTextComposer> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<string, ParametricTextComposer>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public int Count => ComposersDictionary.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<string, ParametricTextComposer> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<string, ParametricTextComposer>> GetEnumerator()
    {
        return ComposersDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ComposersDictionary.GetEnumerator();
    }
}