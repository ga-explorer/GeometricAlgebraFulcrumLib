using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.SyntaxTree;
using CodeComposerLib.SyntaxTree.Expressions;

namespace CodeComposerLib.Languages;

public class CclLanguageSyntaxFactory
{
    #region General Syntax Elements

    public SteEmptyLines EmptyLine()
    {
        return new SteEmptyLines();
    }

    public SteEmptyLines EmptyLines(int count)
    {
        return new SteEmptyLines(count);
    }

    public SteFixedCode FixedCode(string codeText)
    {
        return new SteFixedCode(codeText);
    }

    #endregion

    #region Syntax List

    public SteSyntaxElementsList SyntaxElementsList()
    {
        return new SteSyntaxElementsList();
    }

    public SteSyntaxElementsList SyntaxElementsList(int capacity)
    {
        return new SteSyntaxElementsList(capacity);
    }

    public SteSyntaxElementsList SyntaxElementsList(IEnumerable<ISyntaxTreeElement> items)
    {
        return new SteSyntaxElementsList(items);
    }

    public SteSyntaxElementsList SyntaxElementsList(IEnumerable<string> items)
    {
        return new SteSyntaxElementsList(items.Select(t => new SteFixedCode(t)));
    }

    public SteSyntaxElementsList SyntaxElementsList(params ISyntaxTreeElement[] items)
    {
        return new SteSyntaxElementsList(items);
    }

    public SteSyntaxElementsList SyntaxElementsList(params string[] items)
    {
        return new SteSyntaxElementsList(items.Select(t => new SteFixedCode(t)));
    }

    #endregion

    #region Comments

    public SteComment Comment()
    {
        return new SteComment();
    }

    public SteComment Comment(int emptyLinesCount, bool useSingleLine = true)
    {
        return new SteComment(emptyLinesCount) { SingleLineComment = useSingleLine };
    }

    public SteComment Comment(string commentedText, bool useSingleLine = true)
    {
        return new SteComment(commentedText) { SingleLineComment = useSingleLine };
    }

    public SteComment Comment(IEnumerable<string> commentedTextStrings, bool useSingleLine = true)
    {
        return new SteComment(commentedTextStrings) { SingleLineComment = useSingleLine };
    }

    public SteComment Comment(params string[] commentedTextStrings)
    {
        return new SteComment(commentedTextStrings);
    }

    public SteComment SingleLineComment()
    {
        return new SteComment() { SingleLineComment = true };
    }

    public SteComment SingleLineComment(int emptyLinesCount)
    {
        return new SteComment(emptyLinesCount) { SingleLineComment = true };
    }

    public SteComment SingleLineComment(string commentedText)
    {
        return new SteComment(commentedText){ SingleLineComment = true};
    }

    public SteComment SingleLineComment(IEnumerable<string> commentedTextStrings)
    {
        return new SteComment(commentedTextStrings) { SingleLineComment = true };
    }

    public SteComment SingleLineComment(params string[] commentedTextStrings)
    {
        return new SteComment(commentedTextStrings) { SingleLineComment = true };
    }

    public SteComment MultiLineComment()
    {
        return new SteComment() { MultiLineComment = true };
    }

    public SteComment MultiLineComment(int emptyLinesCount)
    {
        return new SteComment(emptyLinesCount) { MultiLineComment = true };
    }

    public SteComment MultiLineComment(string commentedText)
    {
        return new SteComment(commentedText) { MultiLineComment = true };
    }

    public SteComment MultiLineComment(IEnumerable<string> commentedTextStrings)
    {
        return new SteComment(commentedTextStrings) { MultiLineComment = true };
    }

    public SteComment MultiLineComment(params string[] commentedTextStrings)
    {
        return new SteComment(commentedTextStrings) { MultiLineComment = true };
    }

    #endregion

    #region Declarations and Assignments

    public SteDeclareDataStore DeclareLocalVariable(string varType, string varName)
    {
        return new SteDeclareDataStore()
        {
            LocalDataStore = true,
            DataStoreKind = CclDeclarationKinds.VariableDeclaration,
            DataStoreType = varType,
            DataStoreName = varName,
        };
    }

    public SteDeclareDataStore DeclareLocalVariable(string varType, string varName, string varValue)
    {
        return new SteDeclareDataStore()
        {
            LocalDataStore = true,
            DataStoreKind = CclDeclarationKinds.VariableDeclaration,
            DataStoreType = varType,
            DataStoreName = varName,
            InitialValue = new SteFixedCode(varValue)
        };
    }

    public SteDeclareDataStore DeclareLocalVariable(string varType, string varName, SteExpression varValue)
    {
        return new SteDeclareDataStore()
        {
            LocalDataStore = true,
            DataStoreKind = CclDeclarationKinds.VariableDeclaration,
            DataStoreType = varType,
            DataStoreName = varName,
            InitialValue = varValue
        };
    }

    public SteDeclareFixedSizeArray DeclareLocalArray(string arrayItemType, string arrayVarName, string arraySize)
    {
        return new SteDeclareFixedSizeArray()
        {
            LocalDataStore = true,
            DataStoreKind = CclDeclarationKinds.ArrayDeclaration,
            DataStoreType = arrayItemType,
            DataStoreName = arrayVarName,
            ArraySize = arraySize
        };
    }

    public SteAssign AssignToLocalVariable(string varName, string varValue)
    {
        return new SteAssign()
        {
            LocalAssignment = true,
            LeftHandSide = new SteFixedCode(varName),
            RightHandSide = new SteFixedCode(varValue)
        };
    }

    public SteAssign AssignToLocalVariable(string varName, SteExpression varValue)
    {
        return new SteAssign()
        {
            LocalAssignment = true,
            LeftHandSide = new SteFixedCode(varName),
            RightHandSide = varValue
        };
    }

    public SteAssign AssignToArrayItem(string varName, string itemIndex, string varValue)
    {
        return new SteAssign()
        {
            LeftHandSide = new SteAccessArrayItem()
            {
                ReadAccess = false,
                VariableName = varName,
                ItemIndex = new SteFixedCode(itemIndex)
            },
            RightHandSide = new SteFixedCode(varValue)
        };
    }

    public SteAccessArrayItem ReadArrayItem(string varName, string itemIndex)
    {
        return new SteAccessArrayItem()
        {
            ReadAccess = true,
            VariableName = varName,
            ItemIndex = new SteFixedCode(itemIndex)
        };
    }

    #endregion

    #region If Else

    public SteIf If(string condition, string code)
    {
        return new SteIf()
        {
            Condition = new SteFixedCode(condition),
            TrueCode = new SteFixedCode(code)
        };
    }

    public SteIf If(string condition, ISyntaxTreeElement code)
    {
        return new SteIf()
        {
            Condition = new SteFixedCode(condition),
            TrueCode = code
        };
    }

    public SteIf If(ISyntaxTreeElement condition, string code)
    {
        return new SteIf()
        {
            Condition = condition,
            TrueCode = new SteFixedCode(code)
        };
    }

    public SteIf If(ISyntaxTreeElement condition, ISyntaxTreeElement code)
    {
        return new SteIf()
        {
            Condition = condition,
            TrueCode = code
        };
    }

    public SteIfElse IfElse(string condition, string trueCode, string elseCode)
    {
        return new SteIfElse()
        {
            Condition = new SteFixedCode(condition),
            TrueCode = new SteFixedCode(trueCode),
            ElseCode = new SteFixedCode(elseCode)
        };
    }

    public SteIfElse IfElse(ISyntaxTreeElement condition, ISyntaxTreeElement trueCode, ISyntaxTreeElement elseCode)
    {
        return new SteIfElse()
        {
            Condition = condition,
            TrueCode = trueCode,
            ElseCode = elseCode
        };
    }

    public SteIfElseIfElse IfElseIf(IEnumerable<SteIf> ifList)
    {
        var item = new SteIfElseIfElse();

        item.IfList.AddRange(ifList);

        return item;
    }

    public SteIfElseIfElse IfElseIfElse(IEnumerable<SteIf> ifList, ISyntaxTreeElement elseCode)
    {
        var item = new SteIfElseIfElse()
        {
            ElseCode = elseCode
        };

        item.IfList.AddRange(ifList);

        return item;
    }

    #endregion

    #region Loops

    public SteForLoop ForLoop(string loopInit, string loopCond, string loopUpdate, string loopCode)
    {
        return new SteForLoop()
        {
            LoopInitialization = new SteFixedCode(loopInit),
            LoopCondition = new SteFixedCode(loopCond),
            LoopUpdate = new SteFixedCode(loopUpdate),
            LoopCode = new SteFixedCode(loopCode)
        };
    }

    public SteForLoop ForLoop(ISyntaxTreeElement loopInit, ISyntaxTreeElement loopCond, ISyntaxTreeElement loopUpdate, ISyntaxTreeElement loopCode)
    {
        return new SteForLoop()
        {
            LoopInitialization = loopInit,
            LoopCondition = loopCond,
            LoopUpdate = loopUpdate,
            LoopCode = loopCode
        };
    }

    public SteForEachLoop ForEachLoop(string loopVarType, string loopVarName, ISyntaxTreeElement loopCollection, ISyntaxTreeElement loopCode)
    {
        return new SteForEachLoop()
        {
            LoopVariableName = loopVarName,
            LoopVariableType = loopVarType,
            LoopCollection = loopCollection,
            LoopCode = loopCode
        };
    }

    public SteWhileLoop WhileLoop(string loopCond, ISyntaxTreeElement loopCode)
    {
        return new SteWhileLoop()
        {
            DoLoop = false,
            LoopCondition = new SteFixedCode(loopCond),
            LoopCode = loopCode
        };
    }

    public SteWhileLoop WhileLoop(ISyntaxTreeElement loopCond, ISyntaxTreeElement loopCode)
    {
        return new SteWhileLoop()
        {
            DoLoop = false,
            LoopCondition = loopCond,
            LoopCode = loopCode
        };
    }

    public SteWhileLoop DoWhileLoop(string loopCond, ISyntaxTreeElement loopCode)
    {
        return new SteWhileLoop()
        {
            DoLoop = true,
            LoopCondition = new SteFixedCode(loopCond),
            LoopCode = loopCode
        };
    }

    public SteWhileLoop DoWhileLoop(ISyntaxTreeElement loopCond, ISyntaxTreeElement loopCode)
    {
        return new SteWhileLoop()
        {
            DoLoop = true,
            LoopCondition = loopCond,
            LoopCode = loopCode
        };
    }

    #endregion

    #region Exceptions

    public SteThrowException ThrowException(ISyntaxTreeElement exceptionCode)
    {
        return new SteThrowException()
        {
            ExceptionCode = exceptionCode
        };
    }

    public SteThrowException ThrowException(string exceptionCode)
    {
        return new SteThrowException()
        {
            ExceptionCode = new SteFixedCode(exceptionCode)
        };
    }

    public SteTryCatch TryCatch(ISyntaxTreeElement tryCode)
    {
        return new SteTryCatch()
        {
            TryCode = tryCode
        };
    }

    public SteTryCatch TryCatch(string tryCode)
    {
        return new SteTryCatch()
        {
            TryCode = new SteFixedCode(tryCode)
        };
    }

    public SteTryCatch TryCatch(ISyntaxTreeElement tryCode, ISyntaxTreeElement catchException, ISyntaxTreeElement catchCode)
    {
        var item = new SteTryCatch()
        {
            TryCode = tryCode
        };

        item.CatchItems.Add(
            new SteTryCatchItem()
            {
                CatchException = catchException, CatchCode = catchCode
            });

        return item;
    }

    public SteTryCatch TryCatch(ISyntaxTreeElement tryCode, string catchException, ISyntaxTreeElement catchCode)
    {
        var item = new SteTryCatch()
        {
            TryCode = tryCode
        };

        item.CatchItems.Add(
            new SteTryCatchItem()
            {
                CatchException = new SteFixedCode(catchException),
                CatchCode = catchCode
            });

        return item;
    }

    public SteTryCatch TryFinally(ISyntaxTreeElement tryCode, ISyntaxTreeElement finallyCode)
    {
        return new SteTryCatch()
        {
            TryCode = tryCode,
            FinallyCode = finallyCode
        };
    }

    public SteTryCatch TryFinally(string tryCode, string finallyCode)
    {
        return new SteTryCatch()
        {
            TryCode = new SteFixedCode(tryCode),
            FinallyCode = new SteFixedCode(finallyCode)
        };
    }

    #endregion

    #region Expressions

    /// <summary>
    /// Create an atomic expression from a variable name
    /// </summary>
    /// <param name="varName"></param>
    /// <returns></returns>
    public SteExpression Variable(string varName)
    {
        return SteExpression.CreateVariable(varName);
    }

    /// <summary>
    /// Create a symbolic number expression like "Pi" and "e".
    /// </summary>
    /// <param name="numberText"></param>
    /// <returns></returns>
    public SteExpression SymbolicNumber(string numberText)
    {
        return SteExpression.CreateSymbolicNumber(numberText);
    }

    /// <summary>
    /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public SteExpression LiteralNumber(int number)
    {
        return SteExpression.CreateLiteralNumber(number);
    }

    /// <summary>
    /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public SteExpression LiteralNumber(double number)
    {
        return SteExpression.CreateLiteralNumber(number);
    }

    /// <summary>
    /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public SteExpression LiteralNumber(float number)
    {
        return SteExpression.CreateLiteralNumber(number);
    }

    /// <summary>
    /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
    /// </summary>
    /// <param name="numberText"></param>
    /// <returns></returns>
    public SteExpression LiteralNumber(string numberText)
    {
        return SteExpression.CreateLiteralNumber(numberText);
    }

    /// <summary>
    /// Create an operator expression without any arguments
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public SteExpression Function(string head)
    {
        return SteExpression.CreateFunction(head);
    }

    /// <summary>
    /// Create an operator expression from a head and a set of expressions as its arguments
    /// </summary>
    /// <param name="head"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public SteExpression Function(string head, params SteExpression[] args)
    {
        return SteExpression.CreateFunction(head, args);
    }

    /// <summary>
    /// Create an operator expression from a head and a set of expressions as its arguments
    /// </summary>
    /// <param name="head"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public SteExpression Function(string head, IEnumerable<SteExpression> args)
    {
        return SteExpression.CreateFunction(head, args);
    }

    /// <summary>
    /// Create an operator expression without any arguments
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public SteExpression Operator(SteOperatorSpecs head)
    {
        return SteExpression.CreateOperator(head);
    }

    /// <summary>
    /// Create an operator expression from a head and a set of expressions as its arguments
    /// </summary>
    /// <param name="head"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public SteExpression Operator(SteOperatorSpecs head, params SteExpression[] args)
    {
        return SteExpression.CreateOperator(head, args);
    }

    /// <summary>
    /// Create an operator expression from a head and a set of expressions as its arguments
    /// </summary>
    /// <param name="head"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public SteExpression Operator(SteOperatorSpecs head, IEnumerable<SteExpression> args)
    {
        return SteExpression.CreateOperator(head, args);
    }

    /// <summary>
    /// Create a copy of the given text expression tree
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public SteExpression CopyExpression(SteExpression expr)
    {
        return expr.CreateCopy();
    }

    #endregion

    #region Exit, Return, and Continue

    public SteReturn ReturnNothing()
    {
        return new SteReturn();
    }

    public SteReturn ReturnValue(ISyntaxTreeElement value)
    {
        return new SteReturn() { ReturnedValue = value };
    }

    #endregion

    #region Code Modules

    public SteImportNamespaces ImportNamespaces(params string[] namespacesList)
    {
        return new SteImportNamespaces(namespacesList);
    }

    public SteImportNamespaces ImportNamespaces(IEnumerable<string> namespacesList)
    {
        return new SteImportNamespaces(namespacesList);
    }

    public SteIncludeCodeModules IncludeCodeModules(params string[] codeModulesList)
    {
        return new SteIncludeCodeModules(codeModulesList);
    }

    public SteIncludeCodeModules IncludeCodeModules(IEnumerable<string> codeModulesList)
    {
        return new SteIncludeCodeModules(codeModulesList);
    }

    public SteSetNamespace SetNamespace(string namespaceName)
    {
        return new SteSetNamespace()
        {
            NamespaceName = namespaceName
        };
    }

    public SteSetNamespace SetNamespace(string namespaceName, ISyntaxTreeElement code)
    {
        return new SteSetNamespace()
        {
            NamespaceName = namespaceName,
            SubCode = code
        };
    }

    #endregion
}