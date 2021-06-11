using System;
using System.Collections.Generic;
using System.Text;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.ValueAccess
{
    /// <summary>
    /// This class represents a single step in the value access process using a list of constant keys of type K (i.e. multiple partial values are read per step)
    /// </summary>
    public sealed class ValueAccessStepByKeyList<TK> : ValueAccessStep where TK : IComparable<TK>
    {
        public List<TK> AccessKeyList { get; } = new List<TK>();

        public override string AccessName 
        { 
            get 
            {
                var s = new StringBuilder();

                s.Append("[");

                if (AccessKeyList.Count > 0)
                {
                    foreach (var key in AccessKeyList)
                    {
                        s.Append("\"");
                        s.Append(key);
                        s.Append("\", ");
                    }

                    s.Length -= 2;
                }

                s.Append("]");

                return s.ToString(); 
            } 
        }


        //private ValueAccessStepByKeyList(ValueAccessStep parent_component, ILanguageType component_type, IEnumerable<K> component_key_list)
        //    : base(parent_component, component_type)
        //{
        //    ComponentKeyList.AddRange(component_key_list);
        //}

        private ValueAccessStepByKeyList(ILanguageType componentType, IEnumerable<TK> componentKeyList)
            : base(componentType)
        {
            AccessKeyList.AddRange(componentKeyList);
        }


        public override ValueAccessStep Duplicate()
        {
            return new ValueAccessStepByKeyList<TK>(AccessStepType, AccessKeyList);
        }


        public static ValueAccessStepByKeyList<TK> Create(ILanguageType componentType, IEnumerable<TK> componentKeyList)
        {
            return new ValueAccessStepByKeyList<TK>(componentType, componentKeyList);
        }
    }
}
