using System.Collections.Generic;
using System.Text;

namespace TextComposerLib.TextExpressions.Ast;

public sealed class TeDictionary : Dictionary<string, ITextExpression>, ICompositeTextExpression
{
    public TeIdentifier Name { get; set; }

    public bool IsNamed => IsNamedDictionary;

    public bool IsNameless => IsNamelessDictionary;

    public IEnumerable<ITextExpression> ChildExpressions => Values;

    public string Text
    {
        get
        {
            var s = new StringBuilder();

            if (IsNamed) s.Append(Name);

            s.Append("{");

            var flag = false;
            foreach (var pair in this)
            {
                if (flag) s.Append(", ");
                else flag = true;

                s.Append(pair.Key ?? string.Empty)
                    .Append(" : ")
                    .Append(ReferenceEquals(pair.Value, null) ? string.Empty : pair.Value.ToString());
            }

            s.Append("}");

            return s.ToString();
        }
    }

    public string TextLiteral => Text.ValueToQuotedLiteral();

    public bool IsSimple => false;

    public bool IsComposite => true;

    public bool IsLiteral => false;

    public bool IsLiteralNumber => false;

    public bool IsLiteralString => false;

    public bool IsIdentifier => false;

    public bool IsSingleIdentifier => false;

    public bool IsQualifiedIdentifier => false;

    public bool IsList => false;

    public bool IsNamedList => false;

    public bool IsNamelessList => false;

    public bool IsDictionary => true;

    public bool IsNamedDictionary => ReferenceEquals(Name, null) == false && Name.HasName;

    public bool IsNamelessDictionary => ReferenceEquals(Name, null) || Name.HasName == false;


    public ITextExpression CreateCopy()
    {
        var newDict = new TeDictionary() { Name = Name };

        foreach (var pair in this)
            newDict.Add(pair.Key, pair.Value.CreateCopy());

        return newDict;
    }

    public override string ToString()
    {
        return Text;
    }
}