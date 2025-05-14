using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text;

public abstract class TextComposerCSharpBase<T>
    : ITextComposer<T>
{
    protected TextLookupDictionary SymbolicToCodeScalarDictionary { get; }
        = new TextLookupDictionary();

    public XGaProcessor<T> Processor { get; }

    public IScalarProcessor<T> ScalarProcessor
        => Processor.ScalarProcessor;

    public Func<string, ulong, string> GetMultivectorScalarSymbolicNameFunc { get; set; }

    public Func<string, ulong, string> GetMultivectorScalarCodeNameFunc { get; set; }


    protected TextComposerCSharpBase(XGaProcessor<T> metric)
    {
        if (!metric.ScalarProcessor.IsSymbolic)
            throw new ArgumentException("The scalar processor must be symbolic");

        Processor = metric;
    }


    public XGaVector<T> DefineVector(string symbolicName, string codeName, int scalarCount)
    {
        var composer = Processor.CreateComposer();

        for (var index = 0; index < scalarCount; index++)
        {
            var id = index.BasisVectorIndexToId();

            var scalarSymbolicName = GetMultivectorScalarSymbolicNameFunc(symbolicName, id.ToUInt64());
            var scalarCodeName = GetMultivectorScalarCodeNameFunc(codeName, id.ToUInt64());

            composer.SetVectorTerm(
                index,
                ScalarProcessor.ScalarFromText(scalarSymbolicName)
            );

            SymbolicToCodeScalarDictionary[scalarSymbolicName] = scalarCodeName;
        }

        return composer.GetVector();
    }


    public string GetBasisVectorText(int index)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(uint grade, ulong index)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(IndexSet id)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(XGaBasisBlade basisBlade)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(IEnumerable<int> indexList)
    {
        throw new NotImplementedException();
    }

    public string GetAngleText(LinFloat64Angle angle)
    {
        throw new NotImplementedException();
    }

    public string GetAngleText(LinAngle<T> angle)
    {
        throw new NotImplementedException();
    }

    public string GetScalarText(Scalar<T> scalar)
    {
        throw new NotImplementedException();
    }

    public string GetScalarText(IScalar<T> scalar)
    {
        throw new NotImplementedException();
    }

    public string GetScalarText(double scalar)
    {
        throw new NotImplementedException();
    }

    public abstract string GetScalarText(T scalar);

    public string GetTermText(uint grade, int index, double scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(uint grade, int index, T scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(IndexSet id, double scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(IndexSet id, T scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(Tuple<ulong, T> idScalarPair)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(Tuple<uint, ulong, T> idScalarPair)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(XGaBasisBlade basisBlade, double scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(XGaBasisBlade basisBlade, T scalar)
    {
        throw new NotImplementedException();
    }

    public string GetArrayText(IReadOnlyList<double> array)
    {
        throw new NotImplementedException();
    }

    public string GetTermsText(IEnumerable<Tuple<ulong, T>> idScalarTuples)
    {
        throw new NotImplementedException();
    }

    public string GetTermsText(IEnumerable<Tuple<uint, ulong, T>> idScalarTuples)
    {
        throw new NotImplementedException();
    }

    public string GetTermsText(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
    {
        throw new NotImplementedException();
    }

    public string GetArrayText(IReadOnlyList<T> array)
    {
        throw new NotImplementedException();
    }

    public string GetArrayText(double[,] array)
    {
        throw new NotImplementedException();
    }

    public string GetArrayText(T[,] array)
    {
        throw new NotImplementedException();
    }

    public string GetVectorText(LinFloat64Vector v)
    {
        throw new NotImplementedException();
    }

    public string GetVectorText(LinVector<T> v)
    {
        throw new NotImplementedException();
    }

    public string GetMultivectorText(XGaFloat64Multivector mv)
    {
        throw new NotImplementedException();
    }

    public string GetMultivectorText(XGaMultivector<T> mv)
    {
        throw new NotImplementedException();
    }
}