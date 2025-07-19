using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public sealed class TccSwitchCaseItem : SteSyntaxElement
{
    public bool BreakCase { get; set; }

    public ISyntaxTreeElement CaseValue { get; set; }

    public ISyntaxTreeElement CaseCode { get; set; }
}

public class SteSwitchCase : SteSyntaxElement
{
    public ISyntaxTreeElement SwitchExpression { get; set; }

    public List<TccSwitchCaseItem> CasesList { get; }

    public ISyntaxTreeElement DefaultCode { get; set; }


    public SteSwitchCase()
    {
        CasesList = new List<TccSwitchCaseItem>();
    }


    public SteSwitchCase AddCase(ISyntaxTreeElement caseItem, ISyntaxTreeElement caseCode)
    {
        CasesList.Add(
            new TccSwitchCaseItem()
            {
                CaseValue = caseItem, 
                CaseCode = caseCode
            });

        return this;
    }

    public SteSwitchCase AddCase(string caseItem, ISyntaxTreeElement caseCode)
    {
        CasesList.Add(
            new TccSwitchCaseItem()
            {
                CaseValue = new SteFixedCode(caseItem),
                CaseCode = caseCode
            });

        return this;
    }

    public SteSwitchCase AddCase(string caseItem, string caseCode)
    {
        CasesList.Add(
            new TccSwitchCaseItem()
            {
                CaseValue = new SteFixedCode(caseItem),
                CaseCode = new SteFixedCode(caseCode)
            });

        return this;
    }
}