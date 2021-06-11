using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Command
{
    /// <summary>
    /// This class represents a block of commands that can be evaluated into an expression
    /// </summary>
    public class CommandBlock : LanguageCommand, IIronyAstObjectWithScope
    {
        /// <summary>
        /// The child command block scope of this command block
        /// </summary>
        public ScopeCommandBlockChild ChildCommandBlockScope { get; }

        /// <summary>
        /// The child scope of this command block
        /// </summary>
        public LanguageScope ChildScope => ChildCommandBlockScope;

        /// <summary>
        /// The name of the child scope of this command block
        /// </summary>
        public string ChildScopeName => ChildCommandBlockScope.ObjectName;

        /// <summary>
        /// A list of all defined local variables inside the child scope of this command block
        /// </summary>
        public IEnumerable<SymbolLocalVariable> LocalVariables { get; private set; }

        /// <summary>
        /// The list of all commands of this command block (except local variables declaration commands)
        /// </summary>
        protected readonly List<LanguageCommand> CommandsList = new List<LanguageCommand>();

        /// <summary>
        /// If this command block block is the body of a procedure this returns the parent procedure
        /// </summary>
        public SymbolProcedure ParentSymbolProcedure => ParentLanguageSymbol as SymbolProcedure;

        /// <summary>
        /// True if this command block block is the body of a procedure
        /// </summary>
        public bool IsProcedureBody => ParentSymbolProcedure != null;

        /// <summary>
        /// True if this command block is inside another command block
        /// </summary>
        public bool IsChildOfCommandBlock => ParentScope is ScopeCommandBlockChild;


        protected CommandBlock(LanguageScope parentScope)
            : this(parentScope, parentScope.RootAst.LocalVariableRoleName)
        {
        }

        protected CommandBlock(LanguageScope parentScope, string localVariableRoleName)
            : base(parentScope)
        {
            ChildCommandBlockScope = ScopeCommandBlockChild.Create(this, "command_" + ObjectId + "_scope");

            LocalVariables = ChildCommandBlockScope.Symbols(localVariableRoleName).Cast<SymbolLocalVariable>();
        }


        public int CommandsCount => CommandsList.Count;

        /// <summary>
        /// The list of commands inside this block
        /// </summary>
        public IEnumerable<LanguageCommand> Commands => CommandsList;

        /// <summary>
        /// The list of commands inside this block excluding declare commands
        /// </summary>
        public IEnumerable<LanguageCommand> CommandsNoDeclare
        {
            get
            {
                //Return other commands next
                return CommandsList.Where(command => !(command is CommandDeclareVariable));
            }
        }

        public IEnumerable<CommandDeclareVariable> DeclareVariableCommands
        {
            get
            {
                return 
                    CommandsList
                    .Where(command => (command is CommandDeclareVariable))
                    .Cast<CommandDeclareVariable>();
            }
        }

        public virtual CommandDeclareVariable DefineLocalVariable(string symbolName, ILanguageType symbolType, string symbolRoleName)
        {
            var localVar = SymbolLocalVariable.Create(symbolName, symbolType, ChildCommandBlockScope, symbolRoleName);

            var command = new CommandDeclareVariable(ChildCommandBlockScope, localVar);

            CommandsList.Add(command);

            return command;
        }

        public virtual CommandDeclareVariable DefineLocalVariable(string symbolName, ILanguageType symbolType)
        {
            var localVar = SymbolLocalVariable.Create(symbolName, symbolType, ChildCommandBlockScope);

            var command = new CommandDeclareVariable(ChildCommandBlockScope, localVar);

            CommandsList.Add(command);

            return command;
        }

        public virtual CommandDeclareVariable DefineLocalVariable(ILanguageType symbolType)
        {
            var localVar = SymbolLocalVariable.Create(symbolType, ChildCommandBlockScope);

            var command = new CommandDeclareVariable(ChildCommandBlockScope, localVar);

            CommandsList.Add(command);

            return command;
        }

        /// <summary>
        /// Removes the definition of a local variable from this command block
        /// </summary>
        /// <param name="localVar"></param>
        /// <returns></returns>
        public bool UndefineLocalVariable(SymbolLocalVariable localVar)
        {
            var declareCommand =
                DeclareVariableCommands.FirstOrDefault(
                    command => command.DataStore.ObjectName == localVar.ObjectName
                    );

            if (ReferenceEquals(declareCommand, null) == false)
                RemoveCommand(declareCommand);

            return ChildScope.RemoveLocalVariable(localVar);
        }

        public bool ContainsLocalVariable(string symbolName)
        {
            return ChildCommandBlockScope.LookupSymbol(symbolName, RootAst.LocalVariableRoleName, out var symbol);
        }

        public bool LookupLocalVariable(string symbolName, out SymbolLocalVariable localVar)
        {
            if (ChildCommandBlockScope.LookupSymbol(symbolName, RootAst.LocalVariableRoleName, out var symbol))
            {
                localVar = (SymbolLocalVariable)symbol;
                return true;
            }

            localVar = null;
            return false;
        }

        public bool LookupLocalVariableType(string symbolName, out ILanguageType localVarType)
        {
            if (ChildCommandBlockScope.LookupSymbol(symbolName, RootAst.LocalVariableRoleName, out var symbol))
            {
                localVarType = ((SymbolDataStore)symbol).SymbolType;
                return true;
            }

            localVarType = null;
            return false;
        }

        /// <summary>
        /// This method removes all commands from block except for variable declarations
        /// </summary>
        public void ClearCommands()
        {
            CommandsList.Clear();
        }

        public void RemoveCommand(LanguageCommand command)
        {
            var idx = CommandsList.FindIndex(com => com.ObjectId == command.ObjectId);

            if (idx >= 0)
                CommandsList.RemoveAt(idx);
        }

        protected LanguageCommand AddCommandBeforeCommand(LanguageCommand existingCommand, LanguageCommand addedCommand)
        {
            var index = CommandsList.FindIndex(command => command.ObjectId == existingCommand.ObjectId);

            if (index < 0)
                CommandsList.Add(addedCommand);
            else
                CommandsList.Insert(index, addedCommand);

            return addedCommand;
        }

        public LanguageCommand AddCommand(LanguageCommand command)
        {
            CommandsList.Add(command);

            return command;
        }

        public virtual LanguageCommand AddCommandBeforeCommand_Assign(LanguageCommand existingCommand, LanguageValueAccess lhsValue, ILanguageExpression rhsExpr)
        {
            return AddCommandBeforeCommand(existingCommand, new CommandAssign(ChildCommandBlockScope, lhsValue, rhsExpr));
        }

        public virtual LanguageCommand AddCommand_Declare(SymbolDataStore dataStore)
        {
            return AddCommand(new CommandDeclareVariable(ChildCommandBlockScope, dataStore));
        }

        public virtual LanguageCommand AddCommand_Assign(LanguageValueAccess lhsValue, ILanguageExpression rhsExpr)
        {
            return AddCommand(new CommandAssign(ChildCommandBlockScope, lhsValue, rhsExpr));
        }

        public virtual LanguageCommand AddCommand_Comment(string commentText)
        {
            return AddCommand(new CommandComment(ChildCommandBlockScope, commentText));
        }

        //public virtual LanguageCommand AddCommand_Declare(SymbolDataStore variable)
        //{
        //    return this.AddCommand(new CommandDeclareVariable(this.ChildScope, variable));
        //}

        public virtual LanguageCommand AddCommand_ExitCommandBlock(CommandBlock targetBlock)
        {
            return AddCommand(new CommandExitCommandBlock(ChildCommandBlockScope, targetBlock));
        }

        
        public static CommandBlock Create(LanguageScope parentScope)
        {
            return new CommandBlock(parentScope);
        }

        public static CommandBlock Create(LanguageScope parentScope, string localVariableRoleName)
        {
            return new CommandBlock(parentScope, localVariableRoleName);
        }

        public static CommandBlock Create(IIronyAstObjectWithScope parentObject)
        {
            return new CommandBlock(parentObject.ChildScope);
        }

        public static CommandBlock Create(IIronyAstObjectWithScope parentObject, string localVariableRoleName)
        {
            return new CommandBlock(parentObject.ChildScope, localVariableRoleName);
        }
    }
}
