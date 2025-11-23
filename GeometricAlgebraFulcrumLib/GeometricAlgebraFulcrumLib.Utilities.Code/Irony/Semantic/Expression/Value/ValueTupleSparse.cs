using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Value;

public class ValueTupleSparse : ValueCompositeSparse<int>
{
    public TypeTuple ValueTupleType { get; }

    public override ILanguageType ExpressionType => ValueTupleType;


    protected ValueTupleSparse(TypeTuple valueType)
    {
        ValueTupleType = valueType;
    }



    public override ILanguageValue this[int accessKey]
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public override ILanguageValue DuplicateValue(bool deepCopy)
    {
        throw new NotImplementedException();
    }
}