namespace CodeComposerLib.Languages
{
    public abstract class LanguageServer
    {
        public abstract string DefaultFileExtension { get; }

        /// <summary>
        /// The code generator for this language
        /// </summary>
        public LanguageCodeGenerator CodeGenerator { get; }

        /// <summary>
        /// The syntax factory for this language
        /// </summary>
        public LanguageSyntaxFactory SyntaxFactory { get; private set; }

        /// <summary>
        /// The main information of the target language
        /// </summary>
        public LanguageInfo LanguageInfo => CodeGenerator.LanguageInfo;

        /// <summary>
        /// The scalar (i.e. real) type name of this language
        /// </summary>
        public string ScalarTypeName
        {
            get { return CodeGenerator.ScalarTypeName; }
            set { CodeGenerator.ScalarTypeName = value; }
        }

        /// <summary>
        /// The value of the zero scalar
        /// </summary>
        public string ScalarZero
        {
            get { return CodeGenerator.ScalarZero; }
            set { CodeGenerator.ScalarZero = value; }
        }


        protected LanguageServer(LanguageCodeGenerator codeGenerator, LanguageSyntaxFactory syntaxFactory)
        {
            CodeGenerator = codeGenerator;
            SyntaxFactory = syntaxFactory;
        }
    }
}
