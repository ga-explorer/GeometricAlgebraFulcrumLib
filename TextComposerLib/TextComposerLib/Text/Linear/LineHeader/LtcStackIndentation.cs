using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextComposerLib.Text.Linear.LineHeader;

public sealed class LtcStackIndentation : LtcLineHeader
{
    private readonly Stack<string> _indentationStack = new Stack<string>();


    public string DefaultIndentation { get; set; } = "    ";

    public int IndentationLevel => _indentationStack.Count;

    public int IndentationWidth
    {
        get
        {
            return _indentationStack.Sum(indent => indent.Length);
        }
    }

    public string IndentationString
    {
        get
        {
            var s = new StringBuilder();

            foreach (var indent in _indentationStack.Reverse())
                s.Append(indent);

            return s.ToString();
        }
    }


    public void PushIndentation()
    {
        _indentationStack.Push(DefaultIndentation);
    }

    public void PushIndentation(string indent)
    {
        _indentationStack.Push(indent);
    }

    public void PopIndentation()
    {
        if (_indentationStack.Count > 0)
            _indentationStack.Pop();
    }

    public override void Reset()
    {
        _indentationStack.Clear();
    }

    public override string GetHeaderText()
    {
        return IndentationString;
    }

    public override string ToString()
    {
        return IndentationString;
    }
}