using System.Collections.Generic;
using System.Text;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;
using Microsoft.CSharp.RuntimeBinder;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.Irony.DSLDebug
{
    /// <summary>
    /// This class can be used to generate text based on the parent Irony DSL and its child objects. It can be used for diagnostics and for textual code generation
    /// </summary>
    public abstract class IronyAstDescription : IAstNodeDynamicVisitor
    {
        public static string Generate_StringList(IEnumerable<string> items, string delimiter = ", ")
        {
            var s = new StringBuilder();

            foreach (var text in items)
                s.Append(text).Append(delimiter);

            s.Length = s.Length - delimiter.Length;

            return s.ToString();
        }


        public LinearTextComposer Log { get; }

        public bool EnableLogObjectClass;

        public bool EnableLogSymbolRole;

        public bool EnableLogSymbolType;

        public bool EnableLogExpressionType;


        public virtual bool SimpleLoggingEnabled
        {
            get
            {
                return !(
                    EnableLogObjectClass ||
                    EnableLogSymbolRole ||
                    EnableLogSymbolType ||
                    EnableLogExpressionType
                    );
            }
            set
            {
                EnableLogObjectClass = !value;
                EnableLogSymbolRole = !value;
                EnableLogSymbolType = !value;
                EnableLogExpressionType = !value;
            }
        }

        public virtual bool FullLoggingEnabled
        {
            get
            {
                return
                    EnableLogObjectClass &&
                    EnableLogSymbolRole &&
                    EnableLogSymbolType &&
                    EnableLogExpressionType;
            }
            set
            {
                EnableLogObjectClass = value;
                EnableLogSymbolRole = value;
                EnableLogSymbolType = value;
                EnableLogExpressionType = value;
            }
        }

        public bool IgnoreNullElements => false;

        public bool UseExceptions => false;


        protected IronyAstDescription()
        {
            Log = new LinearTextComposer() { ClearOnRead = true };
        }

        protected IronyAstDescription(LinearTextComposer log)
        {
            Log = log;
            Log.ClearOnRead = true;
        }


        public virtual void Visit(LanguageSymbol langSymbol)
        {
            if (SimpleLoggingEnabled)
            {
                Log.Append(langSymbol.SymbolAccessName);

                return;
            }

            Log.Append("{");

            Log.Append(" <Symbol: " + langSymbol.SymbolAccessName + "> ");

            if (EnableLogObjectClass)
                Log.Append(" <Class: " + langSymbol.GetType().Name + "> ");

            if (EnableLogSymbolRole)
                Log.Append(" <Role: " + langSymbol.SymbolRoleName + "> ");

            Log.Append("} ");
        }

        public virtual void Visit(SymbolDataStore langSymbol)
        {
            if (SimpleLoggingEnabled)
            {
                Log.Append(langSymbol.SymbolAccessName);

                return;
            }

            Log.Append("{");

            Log.Append(" <Symbol: " + langSymbol.SymbolAccessName + "> ");

            if (EnableLogObjectClass)
                Log.Append(" <Class: " + langSymbol.GetType().Name + "> ");

            if (EnableLogSymbolRole)
                Log.Append(" <Role: " + langSymbol.SymbolRoleName + "> ");

            if (EnableLogSymbolType)
                Log.Append(" <Type: " + langSymbol.SymbolType.TypeSignature + "> ");

            Log.Append("} ");
        }

        public virtual void Visit(ILanguageType langType)
        {
            Log.Append(langType.TypeSignature);
        }

        public abstract void Visit(LanguageValueAccess valAccess);

        public abstract void Visit(IronyAst dsl);

        public void Fallback(IIronyAstObject objItem, RuntimeBinderException excException)
        {
            if (ReferenceEquals(objItem, null))
                Log.Append("<Unrecognized null object");

            else
                Log.Append("<Unrecognized object " + objItem + ">");
        }


        public void SaveLog(string filePath)
        {
            Log.SaveToFile(filePath);
        }

        public override string ToString()
        {
            return Log.ToString();
        }
    }
}
