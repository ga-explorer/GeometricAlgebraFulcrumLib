using System;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.ValueAccess
{
    /// <summary>
    /// This class represents a single step in the value access process using a constant key of type K
    /// For example in the statement 'a[3] = x' the value access of array 'a' is done using a step with
    /// fixed integer key of value '3'. In the statement 'obj.attr = 7' the value access of object 'obj' 
    /// is done using a step with fixed string data member name 'attr'.
    /// </summary>
    public sealed class ValueAccessStepByKey<TK> : ValueAccessStep where TK : IComparable<TK>
    {
        /// <summary>
        /// The constant value of the key used for access
        /// </summary>
        public TK AccessKey { get; }

        public override string AccessName => "[\"" + AccessKey + "\"]";


        //private ValueAccessStepByKey(ValueAccessStep parent_component, ILanguageType component_type, K component_key)
        //    : base(parent_component, component_type)
        //{
        //    ComponentKey = component_key;
        //}

        private ValueAccessStepByKey(ILanguageType componentType, TK componentKey)
            : base(componentType)
        {
            AccessKey = componentKey;
        }


        public override ValueAccessStep Duplicate()
        {
            return new ValueAccessStepByKey<TK>(AccessStepType, AccessKey);
        }


        public static ValueAccessStepByKey<TK> Create(ILanguageType componentType, TK componentKey)
        {
            return new ValueAccessStepByKey<TK>(componentType, componentKey);
        }
    }
}

