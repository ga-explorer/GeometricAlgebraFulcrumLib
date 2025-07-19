using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteSyntaxElementsList : List<ISyntaxTreeElement>, ISyntaxTreeElement
{
    public SteSyntaxElementsList()
    {

    }

    public SteSyntaxElementsList(int capacity) 
        : base(capacity)
    {
            
    }

    public SteSyntaxElementsList(IEnumerable<ISyntaxTreeElement> items)
        : base(items)
    {

    }

    //public TccSyntaxElementsList(params ITccSyntaxElement[] items)
    //    : base(items)
    //{

    //}

    //public TccSyntaxElementsList(IEnumerable<string> items)
    //    : base(items.Select(t => new TccFixedCode(t)))
    //{

    //}

    public SteSyntaxElementsList(params string[] items)
        : base(items.Select(t => new SteFixedCode(t)))
    {

    }


    public new SteSyntaxElementsList Add(ISyntaxTreeElement code)
    {
        base.Add(code);

        return this;
    }

    public SteSyntaxElementsList AddRange(params ISyntaxTreeElement[] codeList)
    {
        foreach (var code in codeList)
            base.Add(code);

        return this;
    }

    public new SteSyntaxElementsList AddRange(IEnumerable<ISyntaxTreeElement> codeList)
    {
        foreach (var code in codeList)
            base.Add(code);

        return this;
    }


    public SteSyntaxElementsList AddFixedCode(string codeText)
    {
        base.Add(new SteFixedCode(codeText));

        return this;
    }

    public SteSyntaxElementsList AddFixedCode(params string[] codeTextList)
    {
        foreach (var codeText in codeTextList)
            base.Add(new SteFixedCode(codeText));

        return this;
    }

    public SteSyntaxElementsList AddFixedCode(IEnumerable<string> codeTextList)
    {
        foreach (var codeText in codeTextList)
            base.Add(new SteFixedCode(codeText));

        return this;
    }


    public SteSyntaxElementsList AddEmptyLine()
    {
        base.Add(new SteEmptyLines());

        return this;
    }

    public SteSyntaxElementsList AddEmptyLines(int count)
    {
        base.Add(new SteEmptyLines(count));

        return this;
    }
}