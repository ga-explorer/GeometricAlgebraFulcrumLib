using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.Irony.Compiler;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Irony.SourceCode;
using Irony.Parsing;

namespace CodeComposerLib.Irony.Semantic.Translator
{
    /// <summary>
    /// This class represents the context (i.e. current scope, parse node, and opened scopes) during AST generation from
    /// the Irony parse tree
    /// </summary>
    public abstract class SymbolTranslatorContext
    {
        /// <summary>
        /// The parent compiler that created this context
        /// </summary>
        public LanguageCompiler ParentCompiler { get; }

        public LanguageProjectCompiler ParentProjectCompiler => ParentCompiler as LanguageProjectCompiler;

        public LanguageTempCodeCompiler ParentTempCodeCompiler => ParentCompiler as LanguageTempCodeCompiler;

        public bool HasParentProjectCompiler => ParentCompiler is LanguageProjectCompiler;

        public bool HasParentTempCodeCompiler => ParentCompiler is LanguageTempCodeCompiler;

        /// <summary>
        /// The Irony AST for translation process
        /// </summary>
        public IronyAst RootAst => ParentCompiler.RootAst;

        /// <summary>
        /// The compilation log for holding errors and warnings
        /// </summary>
        public LanguageCompilationLog CompilationLog => ParentCompiler.CompilationLog;


        /// <summary>
        /// The internal stack of translation context states
        /// </summary>
        protected Stack<SymbolTranslatorContextState> StateStack = new Stack<SymbolTranslatorContextState>();

        /// <summary>
        /// The list of opened scopes used for referencing symbols from the parent Irony DSL
        /// </summary>
        private readonly Stack<LanguageScope> _openedScopes = new Stack<LanguageScope>();


        /// <summary>
        /// True if ths context stack has a check point state
        /// </summary>
        public bool HasActiveCheckPointState 
        { 
            get { return StateStack.Any(state => state.IsCheckPointState); }
        }

        /// <summary>
        /// True if the context stack is not empty
        /// </summary>
        public bool HasActiveState => StateStack.Count > 0;

        /// <summary>
        /// Returns the context state on the top of stack without removing it from the stack
        /// </summary>
        public SymbolTranslatorContextState ActiveState => StateStack.Count > 0 ? StateStack.Peek() : null;

        /// <summary>
        /// Returns the ID of the context state on the top of stack without removing it from the stack
        /// </summary>
        public int ActiveStateId
        {
            get
            {
                if (StateStack.Count > 0)
                    return StateStack.Peek().StateId;
                
                return -1;
            }
        }

        /// <summary>
        /// Returns the symbol role name of the context state on the top of stack without removing it from the stack
        /// </summary>
        public string ActiveSymbolRoleName => StateStack.Count > 0 ? StateStack.Peek().SymbolRoleName : string.Empty;

        /// <summary>
        /// Returns the parse node of the context state on the top of stack without removing it from the stack
        /// </summary>
        public ParseTreeNode ActiveParseNode => StateStack.Count > 0 ? StateStack.Peek().ParseNode : null;

        /// <summary>
        /// Returns the parent scope of the context state on the top of stack without removing it from the stack
        /// </summary>
        public LanguageScope ActiveParentScope => StateStack.Count > 0 ? StateStack.Peek().ParentScope : null;

        /// <summary>
        /// Returns the parent scope as a language symbol (if possible) of the context state on the top of stack 
        /// without removing it from the stack
        /// </summary>
        public SymbolWithScope ActiveParentSymbol 
        { 
            get 
            { 
                var scope = ActiveParentScope as ScopeSymbolChild;

                return ReferenceEquals(scope, null) ? null : scope.ParentLanguageSymbolWithScope;
            } 
        }

        /// <summary>
        /// Returns the parent scope as a block expression (if possible) of the context state on the top of stack 
        /// without removing it from the stack
        /// </summary>
        public CommandBlock ActiveParentCommandBlock
        {
            get
            {
                var scope = ActiveParentScope as ScopeCommandBlockChild;

                return ReferenceEquals(scope, null) ? null : scope.ParentCommandBlock;
            }
        }

        /// <summary>
        /// Returns the parent scope as a block expression (if possible) of the context state on the top of stack 
        /// without removing it from the stack
        /// </summary>
        public CompositeExpression ActiveParentCompositeExpression
        {
            get
            {
                var scope = ActiveParentScope as ScopeCommandBlockChild;

                return ReferenceEquals(scope, null) ? null : scope.ParentCompositeExpression;
            }
        }

        /// <summary>
        /// Returns the role name of the parent scope as a language symbol (if possible) of the context state on the top of stack 
        /// without removing it from the stack
        /// </summary>

        public string ActiveParentSymbolRoleName
        {
            get
            {
                var scope = ActiveParentScope as ScopeSymbolChild;

                return ReferenceEquals(scope, null) ? "" : scope.ParentLanguageSymbol.SymbolRoleName;
            }
        }

        /// <summary>
        /// True if the active parent scope belongs to a language symbol
        /// </summary>
        public bool HasActiveParentSymbol
        {
            get
            {
                var scope = ActiveParentScope as ScopeSymbolChild;

                return ReferenceEquals(scope, null) == false;
            }
        }

        /// <summary>
        /// True if the active parent scope belongs to a block expression
        /// </summary>
        public bool HasActiveParentBlockExpression
        {
            get
            {
                var scope = ActiveParentScope as ScopeCommandBlockChild;

                return ReferenceEquals(scope, null) == false;
            }
        }



        protected SymbolTranslatorContext(LanguageCompiler parentCompiler)
        {
            ParentCompiler = parentCompiler;
        }

        //protected SymbolTranslatorContext(IronyAst rootAst, LanguageCompilationLog compilationLog)
        //{
        //    RootAst = rootAst;
        //    CompilationLog = compilationLog;
        //}


        /// <summary>
        /// If the stack has no states this pushes the given state. Else it pops the active state then pushes the given state
        /// </summary>
        /// <param name="curParentScope"></param>
        /// <param name="curSymbolRoleName"></param>
        /// <param name="curParseNode"></param>
        /// <returns>The newly pushed state</returns>
        public SymbolTranslatorContext SetActiveState(LanguageScope curParentScope, string curSymbolRoleName, ParseTreeNode curParseNode)
        {
            if (!HasActiveState) 
                return PushState(curParentScope, curSymbolRoleName, curParseNode);

            var state = PopState();

            return PushState(curParentScope, curSymbolRoleName, curParseNode, state.IsCheckPointState);
        }

        /// <summary>
        /// Push the given state into the stack
        /// </summary>
        /// <param name="curParentScope"></param>
        /// <param name="curSymbolRoleName"></param>
        /// <param name="curParseNode"></param>
        /// <param name="isCheckpointState"></param>
        /// <returns>The newly pushed state</returns>
        private SymbolTranslatorContext PushState(LanguageScope curParentScope, string curSymbolRoleName, ParseTreeNode curParseNode, bool isCheckpointState)
        {
            var state = 
                new SymbolTranslatorContextState(curParentScope, curSymbolRoleName, curParseNode) 
                { IsCheckPointState = isCheckpointState };

            StateStack.Push(state);

            return this;
        }

        /// <summary>
        /// Push the given state into the stack
        /// </summary>
        /// <param name="curParentScope"></param>
        /// <param name="curSymbolRoleName"></param>
        /// <param name="curParseNode"></param>
        /// <returns></returns>
        public SymbolTranslatorContext PushState(LanguageScope curParentScope, string curSymbolRoleName, ParseTreeNode curParseNode)
        {
            var state = new SymbolTranslatorContextState(curParentScope, curSymbolRoleName, curParseNode);

            StateStack.Push(state);

            return this;
        }

        /// <summary>
        /// Push the given state into the stack. Any missing state information is set to be the active information
        /// </summary>
        /// <param name="curParentScope"></param>
        /// <param name="curSymbolRoleName"></param>
        /// <returns></returns>
        public SymbolTranslatorContext PushState(LanguageScope curParentScope, string curSymbolRoleName)
        {
            var state = new SymbolTranslatorContextState(curParentScope, curSymbolRoleName, ActiveParseNode);

            StateStack.Push(state);

            return this;
        }

        /// <summary>
        /// Push the given state into the stack. Any missing state information is set to be the active information
        /// </summary>
        /// <param name="curSymbolRoleName"></param>
        /// <param name="curParseNode"></param>
        /// <returns></returns>
        public SymbolTranslatorContext PushState(string curSymbolRoleName, ParseTreeNode curParseNode)
        {
            var state = new SymbolTranslatorContextState(ActiveParentScope, curSymbolRoleName, curParseNode);

            StateStack.Push(state);

            return this;
        }

        /// <summary>
        /// Push the given state into the stack. Any missing state information is set to be the active information
        /// </summary>
        /// <param name="curParentScope"></param>
        /// <param name="curParseNode"></param>
        /// <returns></returns>
        public SymbolTranslatorContext PushState(LanguageScope curParentScope, ParseTreeNode curParseNode)
        {
            var state = new SymbolTranslatorContextState(curParentScope, ActiveSymbolRoleName, curParseNode);

            StateStack.Push(state);

            return this;
        }

        /// <summary>
        /// Push the given state into the stack. Any missing state information is set to be the active information
        /// </summary>
        /// <param name="curParentScope"></param>
        /// <returns></returns>
        public SymbolTranslatorContext PushState(LanguageScope curParentScope)
        {
            var state = new SymbolTranslatorContextState(curParentScope, ActiveSymbolRoleName, ActiveParseNode);

            StateStack.Push(state);

            return this;
        }

        /// <summary>
        /// Push the given state into the stack. Any missing state information is set to be the active information
        /// </summary>
        /// <param name="curParseNode"></param>
        /// <returns></returns>
        public SymbolTranslatorContext PushState(ParseTreeNode curParseNode)
        {
            var state = new SymbolTranslatorContextState(ActiveParentScope, ActiveSymbolRoleName, curParseNode);

            StateStack.Push(state);

            return this;
        }

        /// <summary>
        /// Pop the active state
        /// </summary>
        /// <returns></returns>
        public SymbolTranslatorContextState PopState()
        {
            var state = StateStack.Pop();

            return state;
        }


        /// <summary>
        /// Make the current active state a checkpoint state
        /// </summary>
        public void MarkCheckPointState()
        {
            if (HasActiveState && ActiveState.IsCheckPointState == false)
                ActiveState.IsCheckPointState = true;
            else
                throw new Exception("Fatal: Marking a translation context state as checkpoint failed");
        }

        /// <summary>
        /// Make the current active state a normal state (not a checkpoint)
        /// </summary>
        public void UnmarkCheckPointState()
        {
            if (HasActiveState && ActiveState.IsCheckPointState)
                ActiveState.IsCheckPointState = false;
            else
                throw new Exception("Fatal: Unmarking a translation context state as checkpoint failed");
        }

        /// <summary>
        /// Pop all states until a checkpoint state is found. Used for error recovery
        /// </summary>
        public void RestoreToCheckPointState()
        {
            while (HasActiveState && ActiveState.IsCheckPointState == false)
                StateStack.Pop();

            UnmarkCheckPointState();
        }


        /// <summary>
        /// Open a new scope
        /// </summary>
        /// <param name="scope"></param>
        public void OpenScope(LanguageScope scope)
        {
            //if (this._OpenedScopes.Exists((LanguageScope x) => (x.ScopeID == scope.ScopeID)))
            //    return;

            _openedScopes.Push(scope);
        }

        /// <summary>
        /// Open a new scope
        /// </summary>
        /// <param name="symbol"></param>
        public void OpenScope(SymbolWithScope symbol)
        {
            _openedScopes.Push(symbol.ChildSymbolScope);
        }

        /// <summary>
        /// Close the last scope that was opened
        /// </summary>
        /// <param name="scope"></param>
        public void CloseScope(LanguageScope scope)
        {
            if (_openedScopes.Count == 0 || _openedScopes.Peek().ObjectId != scope.ObjectId)
                throw new Exception("Closed scope not matching last opened scope!");

            _openedScopes.Pop();
        }

        /// <summary>
        /// Close the last scope that was be opened
        /// </summary>
        /// <param name="symbol"></param>
        public void CloseScope(SymbolWithScope symbol)
        {
            if (_openedScopes.Count == 0 || _openedScopes.Peek().ObjectId != symbol.ChildSymbolScope.ObjectId)
                throw new Exception("Closed scope not matching last opened scope!");

            _openedScopes.Pop();
        }

        /// <summary>
        /// Close all opened scopes
        /// </summary>
        public void ClearOpenedScopes()
        {
            _openedScopes.Clear();
        }

        /// <summary>
        /// Returns an enumeration of all currently opened scopes (even if repeated) that can be searched for symbols
        /// This returns the active parent scope first then its parents then other opened scopes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LanguageScope> OpenedScopes()
        {
            for (var scope = ActiveParentScope; ReferenceEquals(scope, null) == false; scope = scope.ParentScope)
                yield return scope;

            foreach (var openedScope in _openedScopes)
                for (var scope = openedScope; ReferenceEquals(scope, null) == false; scope = scope.ParentScope)
                    yield return scope;
        }

        public bool LookupSymbolInOpenedDistinctScopes(string symbolName, out LanguageSymbol symbol)
        {
            var skipList = new Dictionary<int, int>();

            for (var scope = ActiveParentScope; ReferenceEquals(scope, null) == false; scope = scope.ParentScope)
                if (skipList.ContainsKey(scope.ObjectId) == false)
                {
                    skipList.Add(scope.ObjectId, scope.ObjectId);

                    if (scope.LookupSymbol(symbolName, out symbol))
                        return true;
                }

            foreach (var openedScope in _openedScopes)
                for (var scope = openedScope; ReferenceEquals(scope, null) == false; scope = scope.ParentScope)
                    if (skipList.ContainsKey(scope.ObjectId) == false)
                    {
                        skipList.Add(scope.ObjectId, scope.ObjectId);

                        if (scope.LookupSymbol(symbolName, out symbol))
                            return true;
                    }

            symbol = null;
            return false;
        }

        /// <summary>
        /// Returns an enumeration of all currently opened distinct scopes that can be searched for symbols
        /// This returns the active parent scope first then its parents then other opened scopes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LanguageScope> OpenedDistinctScopes()
        {
            var skipList = new Dictionary<int, int>();

            for (var scope = ActiveParentScope; ReferenceEquals(scope, null) == false; scope = scope.ParentScope)
                if (skipList.ContainsKey(scope.ObjectId) == false)
                {
                    skipList.Add(scope.ObjectId, scope.ObjectId);

                    yield return scope;
                }

            foreach (var openedScope in _openedScopes)
                for (var scope = openedScope; ReferenceEquals(scope, null) == false; scope = scope.ParentScope)
                    if (skipList.ContainsKey(scope.ObjectId) == false)
                    {
                        skipList.Add(scope.ObjectId, scope.ObjectId);

                        yield return scope;
                    }
        }

        /// <summary>
        /// Search for a built-in type in the root global scope's general role dictionary
        /// </summary>
        /// <param name="typeName">The name of the built-in type</param>
        /// <param name="builtinType">The returned LanguageType object</param>
        /// <returns>True if the builtin type is found</returns>
        public bool LookupTypePrimitive(string typeName, out TypePrimitive builtinType)
        {
            builtinType = null;

            if (!RootAst.RootScope.LookupSymbol(typeName, RootAst.TypePrimitiveRoleName, out var symbol)) 
                return false;

            builtinType = symbol as TypePrimitive;

            return ReferenceEquals(builtinType, null) == false;
        }

        /// <summary>
        /// Given a parse tree node in the current active code unit this creates a code location
        /// object to be added to a translated language symbol
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IronyAstObjectCodeLocation GetCodeLocation(ParseTreeNode node)
        {
            if (HasParentProjectCompiler == false)
                return null;

            return IronyAstObjectCodeLocation.Create(ParentProjectCompiler.Project, node);
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            foreach (var state in StateStack)
                s.Append(state);

            return s.ToString();
        }
    }
}
