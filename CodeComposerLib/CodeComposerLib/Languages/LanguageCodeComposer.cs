using CodeComposerLib.SyntaxTree;
using DataStructuresLib;
using Microsoft.CSharp.RuntimeBinder;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.Languages
{
    /// <summary>
    /// This class represents a target language for which code generation is done
    /// </summary>
    public abstract class LanguageCodeComposer : 
        ILanguageCodeComposer
    {
        protected readonly LinearTextComposer TextComposer = new LinearTextComposer();


        public bool UseExceptions { get; set; }

        public bool IgnoreNullElements { get; set; }

        /// <summary>
        /// The main information of the target language
        /// </summary>
        public LanguageInfo LanguageInfo { get; protected set; }

        /// <summary>
        /// Get or set the indentation string for this language
        /// </summary>
        public string Indentation
        {
            get => TextComposer.IndentationDefault;
            set => TextComposer.IndentationDefault = value;
        }

        /// <summary>
        /// The name of the target type equivalent to a real number
        /// </summary>
        public string ScalarTypeName { get; set; }

        /// <summary>
        /// The default value for the scalar type
        /// </summary>
        public string ScalarZero { get; set; }

        ///// <summary>
        ///// This dictionary contains the language specific symbols of common operators 
        ///// like 'plus' = '+', 'times' = '*', etc
        ///// </summary>
        //protected Dictionary<string, string> OperatorsDictionary { get; private set; }

        ///// <summary>
        ///// This dictionary contains the language specific names of common declaration 
        ///// modifiers like 'public', 'class', 'static', etc
        ///// </summary>
        //protected Dictionary<string, string> ModifiersDictionary { get; private set; }


        protected LanguageCodeComposer()
        {
            IgnoreNullElements = true;
            //OperatorsDictionary = new Dictionary<string, string>();
            //ModifiersDictionary = new Dictionary<string, string>();
        }


        //public string TryGetOperator(string universalName)
        //{
        //    string operatorName;

        //    return 
        //        OperatorsDictionary.TryGetValue(universalName, out operatorName) 
        //        ? operatorName : universalName;
        //}

        //public string TryGetModifier(string universalName)
        //{
        //    string operatorName;

        //    return
        //        ModifiersDictionary.TryGetValue(universalName, out operatorName)
        //        ? operatorName : universalName;
        //}

        //public IEnumerable<string> TryGetModifiers(IEnumerable<string> universalNameList)
        //{
        //    string operatorName;

        //    return 
        //        universalNameList.Select(
        //            u => ModifiersDictionary.TryGetValue(u, out operatorName) ? operatorName : u
        //            );
        //}

        protected void AddInternalComment(params string[] commentTextList)
        {
            Visit(new SteComment(commentTextList));
        }


        public virtual void Fallback(ISyntaxTreeElement objItem, RuntimeBinderException excException)
        {
            //If the object is null do nothing
            if (objItem == null) return;

            AddInternalComment("Syntax element not implemented: ", objItem.ToString());
        }


        public abstract void Visit(SteComment code);

        public abstract void Visit(SteDeclareDataStore code);

        public abstract void Visit(SteDeclareFixedSizeArray code);

        public abstract void Visit(SteAssign code);

        public abstract void Visit(SteReturn code);


        public virtual void Visit(SteFixedCode code)
        {
            TextComposer.Append(code.Text);
        }

        public virtual void Visit(SteEmptyLines code)
        {
            for (var i = 0; i <code.LinesCount; i++)
                TextComposer.AppendLineAtNewLine();
        }

        public virtual void Visit(SteSyntaxElementsList code)
        {
            foreach (var syntaxElement in code)
                syntaxElement.AcceptVisitor(this);
        }

        public virtual void Visit(SteEmbeddedCode code)
        {
            var convertedCode = code.Code.AcceptVisitor(code.LanguageConverter);

            convertedCode.AcceptVisitor(this);
        }


        public string GenerateCode(ISyntaxTreeElement syntaxElement)
        {
            TextComposer.Clear();

            syntaxElement.AcceptVisitor(this);

            return TextComposer.ToString();
        }
    }
}
