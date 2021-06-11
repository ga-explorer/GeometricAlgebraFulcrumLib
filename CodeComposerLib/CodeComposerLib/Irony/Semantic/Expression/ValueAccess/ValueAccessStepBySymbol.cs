using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.ValueAccess
{
    /// <summary>
    /// This class represents a single step in the value access process using a language symbol.
    /// For example in the statement 'a[x + 3] = 8.9 ' the value access of the array 'a' item 'x + 3' is translated into
    /// 'temp = x + 3' then 'a[temp] = 8.9'. Here 'a[temp]' is a value access step through the symbol 'temp'
    /// </summary>
    public sealed class ValueAccessStepByLValue : ValueAccessStep
    {
        /// <summary>
        /// The language symbol used for access
        /// </summary>
        public SymbolLValue AccessLValue { get; private set; }

        public override string AccessName => "[" + AccessLValue.SymbolAccessName + "]";


        //private ValueAccessStepBySymbol(ValueAccessStep parent_component, ILanguageType component_type, LanguageSymbol component_symbol)
        //    : base(parent_component, component_type)
        //{
        //    ComponentSymbol = component_symbol;
        //}

        private ValueAccessStepByLValue(ILanguageType componentType, SymbolLValue componentSymbol)
            : base(componentType)
        {
            AccessLValue = componentSymbol;
        }


        public ValueAccessStep Duplicate(SymbolLValue componentLvalue)
        {
            return new ValueAccessStepByLValue(AccessStepType, componentLvalue);
        }

        public override ValueAccessStep Duplicate()
        {
            return new ValueAccessStepByLValue(AccessStepType, AccessLValue);
        }

        public ValueAccessStepByLValue ReplaceComponentSymbol(SymbolLValue componentLvalue)
        {
            AccessLValue = componentLvalue;

            return this;
        }


        public static ValueAccessStepByLValue Create(ILanguageType componentType, SymbolLValue componentLvalue)
        {
            return new ValueAccessStepByLValue(componentType, componentLvalue);
        }
    }
}
