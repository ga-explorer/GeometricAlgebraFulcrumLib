using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Scope;

namespace CodeComposerLib.Irony.Semantic.Symbol
{
    public class SymbolWithCommandBlock : SymbolWithScope
    {
        /// <summary>
        /// The list of language commands in the main command block body of this symbol
        /// </summary>
        public IEnumerable<LanguageCommand> Commands => SymbolBody.Commands;

        /// <summary>
        /// The command block of the body of commands for this symbol.
        /// </summary>
        protected CommandBlock InternalCommandBlock;


        /// <summary>
        /// The command block of the body of commands for this symbol.
        /// </summary>
        public CommandBlock SymbolBody
        {
            get
            {
                return InternalCommandBlock;
            }
            set
            {
                if (ReferenceEquals(InternalCommandBlock, null) && ReferenceEquals(value, null) == false)
                    InternalCommandBlock = value;
            }
        }



        protected SymbolWithCommandBlock(string symbolName, LanguageScope parentScope, string symbolRoleName)
            : base(symbolName, parentScope, symbolRoleName)
        {
        }


        public static SymbolWithCommandBlock Create(string symbolName, LanguageScope parentScope, string symbolRoleName)
        {
            return new SymbolWithCommandBlock(symbolName, parentScope, symbolRoleName);
        }
    }
}
