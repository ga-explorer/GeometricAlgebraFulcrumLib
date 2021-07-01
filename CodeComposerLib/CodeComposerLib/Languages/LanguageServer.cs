namespace CodeComposerLib.Languages
{
    public abstract class LanguageServer
    {
        public abstract string DefaultFileExtension { get; }

        /// <summary>
        /// The code generator for this language
        /// </summary>
        public LanguageCodeComposer CodeComposer { get; }

        /// <summary>
        /// The syntax factory for this language
        /// </summary>
        public LanguageSyntaxFactory SyntaxFactory { get; private set; }

        /// <summary>
        /// The main information of the target language
        /// </summary>
        public LanguageInfo LanguageInfo => CodeComposer.LanguageInfo;

        /// <summary>
        /// The scalar (i.e. real) type name of this language
        /// </summary>
        public string ScalarTypeName
        {
            get { return CodeComposer.ScalarTypeName; }
            set { CodeComposer.ScalarTypeName = value; }
        }

        /// <summary>
        /// The value of the zero scalar
        /// </summary>
        public string ScalarZero
        {
            get { return CodeComposer.ScalarZero; }
            set { CodeComposer.ScalarZero = value; }
        }


        protected LanguageServer(LanguageCodeComposer codeComposer, LanguageSyntaxFactory syntaxFactory)
        {
            CodeComposer = codeComposer;
            SyntaxFactory = syntaxFactory;
        }
    }
}
