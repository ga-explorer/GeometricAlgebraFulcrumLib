using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Combinations;

public class XGaFloat64UnilinearCombination : 
    IReadOnlyCollection<XGaFloat64UnilinearCombinationTerm>,
    IXGaLinearCombination
{
    private readonly Dictionary<Pair<IIndexSet>, XGaFloat64UnilinearCombinationTerm> _termList;


    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public XGaFloat64UnilinearCombination()
    {
        _termList = new Dictionary<Pair<IIndexSet>, XGaFloat64UnilinearCombinationTerm>();
    }

    public XGaFloat64UnilinearCombination(Dictionary<Pair<IIndexSet>, XGaFloat64UnilinearCombinationTerm> termList)
    {
        _termList = termList;
    }


    public bool IsOutputScalar()
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == 0
               );
    }
    
    public bool IsOutputVector()
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == 1
               );
    }
    
    public bool IsOutputBivector()
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == 2
               );
    }

    public bool IsOutputKVector()
    {
        if (IsEmpty) return false;

        var grade = 
            _termList.Values.First().OutputBasisBladeGrade;

        return _termList.Values.All(
            term => term.OutputBasisBladeGrade == grade
        );
    }

    public bool IsOutputKVector(int grade)
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == grade
               );
    }

    public XGaFloat64UnilinearCombination Set(XGaFloat64UnilinearCombinationTerm term)
    {
        var key = new Pair<IIndexSet>(
            term.InputBasisBladeId,
            term.OutputBasisBladeId
        );

        if (term.IsInputScalarZero)
            _termList.Remove(key);

        else if (_termList.TryGetValue(key, out var oldTerm))
            oldTerm.InputScalar = term.InputScalar;

        else
            _termList.Add(key, term);

        return this;
    }

    public XGaFloat64UnilinearCombination Add(XGaFloat64UnilinearCombinationTerm term)
    {
        if (term.IsInputScalarZero)
            return this;

        var key = new Pair<IIndexSet>(
            term.InputBasisBladeId,
            term.OutputBasisBladeId
        );

        if (_termList.TryGetValue(key, out var oldTerm))
        {
            oldTerm.InputScalar += term.InputScalar;

            if (oldTerm.IsInputScalarZero)
                _termList.Remove(key);
        }
        else
        {
            _termList.Add(key, term);
        }

        return this;
    }

    public XGaFloat64UnilinearCombination Add(IEnumerable<XGaFloat64UnilinearCombinationTerm> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public XGaFloat64UnilinearCombination Add(Float64Scalar inputScalar, XGaMetric metric, IIndexSet inputBasisBladeId, IIndexSet outputBasisBladeId)
    {
        var term = XGaFloat64UnilinearCombinationTerm.Create(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public XGaFloat64UnilinearCombination Add(XGaBasisBlade inputBasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64UnilinearCombinationTerm.Create(
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaFloat64UnilinearCombination Add(Float64Scalar inputScalar, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64UnilinearCombinationTerm.Create(
            inputScalar,
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }
    
    public XGaFloat64UnilinearCombination Add(IIndexSet inputBasisBladeId, XGaFloat64Multivector outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = XGaFloat64UnilinearCombinationTerm.Create(
                scalar,
                metric,
                inputBasisBladeId,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaFloat64UnilinearCombination Add(XGaBasisBlade inputBasisBlade, XGaFloat64Multivector outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector)
        {
            var term = XGaFloat64UnilinearCombinationTerm.Create(
                scalar,
                inputBasisBlade.Metric,
                inputBasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaFloat64UnilinearCombination Add(IEnumerable<XGaBasisBlade> inputBasisBladeList, Func<XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            Add(
                inputBasisBlade,
                basisMapFunc(inputBasisBlade)
            );
        }

        return this;
    }

    public XGaFloat64UnilinearCombination Add(IEnumerable<XGaBasisBlade> inputBasisBladeList, Func<XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            Add(
                inputBasisBlade,
                basisMapFunc(inputBasisBlade)
            );
        }

        return this;
    }
    
    public XGaFloat64UnilinearCombination AddOutermorphism(XGaFloat64Processor metric, double[,] vectorMapArray)
    {
        var outerMorphism =
            vectorMapArray
                .ColumnsToLinVectors()
                .ToLinUnilinearMap()
                .ToOutermorphism(metric);

        var vSpaceDimensions = 
            vectorMapArray.GetLength(0);

        var idMultivectorPairs =
            outerMorphism.GetMappedBasisBlades(vSpaceDimensions);

        foreach (var (id, mv) in idMultivectorPairs)
            Add(id, mv);

        return this;
    }


    public IReadOnlyList<IIndexSet> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.InputBasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<IIndexSet> GetOutputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetInputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.InputBasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetOutputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public XGaFloat64UnilinearCombination GetSubCombinationWithOutputId(IIndexSet outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64UnilinearCombination(termList);
    }

    public XGaFloat64UnilinearCombination GetSubCombination(Func<XGaFloat64UnilinearCombinationTerm, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64UnilinearCombination(termList);
    }

    public IEnumerator<XGaFloat64UnilinearCombinationTerm> GetEnumerator()
    {
        return _termList
            .Values
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}