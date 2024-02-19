using CodeComposerLib.Irony.DSLException;
using Irony.Parsing;

namespace CodeComposerLib.Irony.SourceCode;

public static class SourceCodeUtils
{
    public static T RaiseParserError<T>(this LanguageCompilationLog compilationLog, string description, int absolutePos)
    {
        var err = new ParserException(description);

        return compilationLog.RaiseErrorMessage<T>(err, absolutePos);
    }

    public static T RaiseGeneratorError<T>(this LanguageCompilationLog compilationLog, string description, ParseTreeNode node)
    {
        var err = new AstGeneratorException(description);

        return compilationLog.RaiseErrorMessage<T>(err, node);
    }

    public static T RaiseTypeMismatchError<T>(this LanguageCompilationLog compilationLog, string description, ParseTreeNode node)
    {
        var err = new AstGeneratorTypeMismatchException(description);

        return compilationLog.RaiseErrorMessage<T>(err, node);
    }

    public static T RaiseParserError<T>(this LanguageCompilationLog compilationLog, string description, Token token)
    {
        var err = new ParserException(description);

        return compilationLog.RaiseErrorMessage<T>(err, token);
    }


}