﻿using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

namespace GeometricAlgebraFulcrumLib.MathBase.Text;

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

            var scalarSymbolicName = GetMultivectorScalarSymbolicNameFunc(symbolicName, id);
            var scalarCodeName = GetMultivectorScalarCodeNameFunc(codeName, id);

            composer.SetVectorTerm(
                index, 
                ScalarProcessor.GetScalarFromText(scalarSymbolicName)
            );

            SymbolicToCodeScalarDictionary[scalarSymbolicName] = scalarCodeName;
        }

        return composer.GetVector();
    }


    public string GetBasisVectorText(int index)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(ulong id)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(uint grade, ulong index)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(IIndexSet id)
    {
        throw new NotImplementedException();
    }

    public string GetBasisBladeText(RGaBasisBlade basisBlade)
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

    public string GetAngleText(Float64PlanarAngle angle)
    {
        throw new NotImplementedException();
    }

    public string GetAngleText(PlanarAngle<T> angle)
    {
        throw new NotImplementedException();
    }

    public string GetScalarText(Scalar<T> scalar)
    {
        throw new NotImplementedException();
    }

    public string GetScalarText(double scalar)
    {
        throw new NotImplementedException();
    }

    public abstract string GetScalarText(T scalar);
    public string GetTermText(ulong id, double scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(ulong id, T scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(uint grade, int index, double scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(uint grade, int index, T scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(IIndexSet id, double scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(IIndexSet id, T scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(RGaBasisBlade basisBlade, double scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(uint grade, ulong index, T scalar)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(RGaKvIndexScalarRecord<T> idScalarPair)
    {
        throw new NotImplementedException();
    }

    public string GetTermText(RGaGradeKvIndexScalarRecord<T> idScalarPair)
    {
        throw new NotImplementedException();
    }
        
    public string GetTermText(RGaBasisBlade basisBlade, T scalar)
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

    public string GetTermsText(IEnumerable<RGaKvIndexScalarRecord<T>> idScalarTuples)
    {
        throw new NotImplementedException();
    }

    public string GetTermsText(IEnumerable<RGaGradeKvIndexScalarRecord<T>> idScalarTuples)
    {
        throw new NotImplementedException();
    }

    public string GetTermsText(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarTuples)
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

    public string GetVectorText(Float64Vector v)
    {
        throw new NotImplementedException();
    }

    public string GetVectorText(LinVector<T> v)
    {
        throw new NotImplementedException();
    }

    public string GetMultivectorText(RGaFloat64Multivector mv)
    {
        throw new NotImplementedException();
    }

    public string GetMultivectorText(RGaMultivector<T> mv)
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