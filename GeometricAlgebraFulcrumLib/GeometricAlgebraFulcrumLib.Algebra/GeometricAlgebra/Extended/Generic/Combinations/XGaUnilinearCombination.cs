using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Combinations;

public class XGaUnilinearCombination<T> : 
    IReadOnlyCollection<XGaUnilinearCombinationTerm<T>>,
    IXGaLinearCombination
{
    private readonly Dictionary<Pair<IIndexSet>, XGaUnilinearCombinationTerm<T>> _termList;


    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public XGaUnilinearCombination()
    {
        _termList = new Dictionary<Pair<IIndexSet>, XGaUnilinearCombinationTerm<T>>();
    }

    public XGaUnilinearCombination(Dictionary<Pair<IIndexSet>, XGaUnilinearCombinationTerm<T>> termList)
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


    public XGaUnilinearCombination<T> Set(XGaUnilinearCombinationTerm<T> term)
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

    public XGaUnilinearCombination<T> Add(XGaUnilinearCombinationTerm<T> term)
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

    public XGaUnilinearCombination<T> Add(IEnumerable<XGaUnilinearCombinationTerm<T>> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public XGaUnilinearCombination<T> Add(Scalar<T> inputScalar, XGaProcessor<T> metric, IIndexSet inputBasisBladeId, IIndexSet outputBasisBladeId)
    {
        var term = XGaUnilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public XGaUnilinearCombination<T> Add(XGaProcessor<T> metric, XGaBasisBlade inputBasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        var term = XGaUnilinearCombinationTerm<T>.Create(
            metric,
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaUnilinearCombination<T> Add(Scalar<T> inputScalar, XGaProcessor<T> metric, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        var term = XGaUnilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }
    
    public XGaUnilinearCombination<T> Add(IIndexSet inputBasisBladeId, XGaMultivector<T> outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = XGaUnilinearCombinationTerm<T>.Create(
                scalar.ScalarFromValue(metric.ScalarProcessor),
                metric,
                inputBasisBladeId,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaUnilinearCombination<T> Add(XGaBasisBlade inputBasisBlade, XGaMultivector<T> outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = XGaUnilinearCombinationTerm<T>.Create(
                scalar.ScalarFromValue(metric.ScalarProcessor),
                metric,
                inputBasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaUnilinearCombination<T> Add(XGaProcessor<T> metric, IEnumerable<XGaBasisBlade> inputBasisBladeList, Func<XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            Add(
                metric,
                inputBasisBlade,
                basisMapFunc(inputBasisBlade)
            );
        }

        return this;
    }

    public XGaUnilinearCombination<T> Add(IEnumerable<XGaBasisBlade> inputBasisBladeList, Func<XGaBasisBlade, XGaMultivector<T>> basisMapFunc)
    {
        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            Add(inputBasisBlade,
                basisMapFunc(inputBasisBlade)
            );
        }

        return this;
    }
    
    public XGaUnilinearCombination<T> AddOutermorphism(XGaProcessor<T> metric, T[,] vectorMapArray)
    {
        var outerMorphism =
            vectorMapArray
                .ColumnsToLinVectors(metric.ScalarProcessor)
                .ToLinUnilinearMap(metric.ScalarProcessor)
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

    public XGaUnilinearCombination<T> GetSubCombinationWithOutputId(IIndexSet outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaUnilinearCombination<T>(termList);
    }

    public XGaUnilinearCombination<T> GetSubCombination(Func<XGaUnilinearCombinationTerm<T>, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaUnilinearCombination<T>(termList);
    }

    public IEnumerator<XGaUnilinearCombinationTerm<T>> GetEnumerator()
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