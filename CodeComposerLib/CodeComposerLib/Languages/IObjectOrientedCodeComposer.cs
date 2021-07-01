using CodeComposerLib.SyntaxTree;

namespace CodeComposerLib.Languages
{
    public interface IObjectOrientedCodeComposer : 
        ILanguageCodeComposer
    {
        void Visit(SteDeclareDataStore code);

        void Visit(SteDeclareLanguageConstruct code);
    }
}