using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public interface ICclObjectOrientedCodeGenerator : 
    ICclLanguageCodeGenerator
{
    void Visit(SteDeclareDataStore code);

    void Visit(SteDeclareLanguageConstruct code);
}