using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;

public interface IMetaExpressionArrayAccess :
    IMetaExpressionComposite
{
    MetaExpressionHeadSpecsArrayAccess ArrayAccessHeadSpecs { get; }
}