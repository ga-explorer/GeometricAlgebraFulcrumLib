using CodeComposerLib.SyntaxTree;

namespace CodeComposerLib.Languages
{
    public interface ICclObjectOrientedCodeGenerator : 
        ICclLanguageCodeGenerator
    {
        void Visit(SteDeclareDataStore code);

        void Visit(SteDeclareLanguageConstruct code);
    }
}