using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public sealed class SteTryCatchItem : SteSyntaxElement
{
    public ISyntaxTreeElement CatchException { get; set; }

    public ISyntaxTreeElement CatchCode { get; set; }
}

public class SteTryCatch : SteSyntaxElement
{
    public ISyntaxTreeElement TryCode { get; set; }

    public List<SteTryCatchItem> CatchItems { get; }

    public ISyntaxTreeElement FinallyCode { get; set; }


    public SteTryCatch()
    {
        CatchItems = new List<SteTryCatchItem>(); 
    }


    public SteTryCatch AddCatch(ISyntaxTreeElement catchException, ISyntaxTreeElement catchCode)
    {
        CatchItems.Add(
            new SteTryCatchItem()
            {
                CatchException = catchException,
                CatchCode = catchCode
            });

        return this;
    }

    public SteTryCatch AddCatch(string catchException, ISyntaxTreeElement catchCode)
    {
        CatchItems.Add(
            new SteTryCatchItem()
            {
                CatchException = new SteFixedCode(catchException),
                CatchCode = catchCode
            });

        return this;
    }

    public SteTryCatch AddCatch(string catchException, string catchCode)
    {
        CatchItems.Add(
            new SteTryCatchItem()
            {
                CatchException = new SteFixedCode(catchException),
                CatchCode = new SteFixedCode(catchCode)
            });

        return this;
    }
}