using CodeComposerLib.SyntaxTree;

namespace CodeComposerLib.Languages
{
    public interface IObjectOrientedCodeGenerator : ILanguageCodeGenerator
    {
        void Visit(SteDeclareDataStore code);

        void Visit(SteDeclareLanguageConstruct code);
    }
}