using CodeComposerLib.SyntaxTree;

namespace CodeComposerLib.Languages
{
    public interface IImperativeCodeComposer : 
        IExpressionTreeCodeComposer
    {
        void Visit(SteDeclareFixedSizeArray code);

        void Visit(SteDeclareMethod code);

        void Visit(SteDeclareSimpleDataStore code);

        void Visit(SteIf code);

        void Visit(SteIfElse code);

        void Visit(SteIfElseIfElse code);

        void Visit(SteForLoop code);

        void Visit(SteForEachLoop code);

        void Visit(SteWhileLoop code);

        void Visit(SteImportNamespaces code);

        void Visit(SteSetNamespace code);

        void Visit(TccSwitchCaseItem code);

        void Visit(SteSwitchCase code);

        void Visit(SteThrowException code);

        void Visit(SteTryCatchItem code);

        void Visit(SteTryCatch code);
    }
}
