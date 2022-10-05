using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite
{
    public interface IMetaExpressionArrayAccess : 
        IMetaExpressionComposite
    {
        MetaExpressionHeadSpecsArrayAccess ArrayAccessHeadSpecs { get; }
    }
}