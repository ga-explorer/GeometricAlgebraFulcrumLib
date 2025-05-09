using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Combinations;

public class RGaUnilinearCombination<T> : 
    IReadOnlyCollection<RGaUnilinearCombinationTerm<T>>,
    IRGaLinearCombination
{
    private readonly Dictionary<Pair<ulong>, RGaUnilinearCombinationTerm<T>> _termList;


    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public RGaUnilinearCombination()
    {
        _termList = new Dictionary<Pair<ulong>, RGaUnilinearCombinationTerm<T>>();
    }

    public RGaUnilinearCombination(Dictionary<Pair<ulong>, RGaUnilinearCombinationTerm<T>> termList)
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


    public RGaUnilinearCombination<T> Set(RGaUnilinearCombinationTerm<T> term)
    {
        var key = new Pair<ulong>(
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

    public RGaUnilinearCombination<T> Add(RGaUnilinearCombinationTerm<T> term)
    {
        if (term.IsInputScalarZero)
            return this;

        var key = new Pair<ulong>(
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

    public RGaUnilinearCombination<T> Add(IEnumerable<RGaUnilinearCombinationTerm<T>> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public RGaUnilinearCombination<T> Add(Scalar<T> inputScalar, RGaProcessor<T> metric, ulong inputBasisBladeId, ulong outputBasisBladeId)
    {
        var term = RGaUnilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public RGaUnilinearCombination<T> Add(RGaProcessor<T> metric, RGaBasisBlade inputBasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        var term = RGaUnilinearCombinationTerm<T>.Create(
            metric,
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaUnilinearCombination<T> Add(Scalar<T> inputScalar, RGaProcessor<T> metric, RGaBasisBlade inputBasisBlade, RGaBasisBlade outputBasisBlade)
    {
        var term = RGaUnilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }
    
    public RGaUnilinearCombination<T> Add(ulong inputBasisBladeId, RGaMultivector<T> outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = RGaUnilinearCombinationTerm<T>.Create(
                scalar.ScalarFromValue(metric.ScalarProcessor),
                metric,
                inputBasisBladeId,
                id
            );

            Add(term);
        }

        return this;
    }

    public RGaUnilinearCombination<T> Add(RGaBasisBlade inputBasisBlade, RGaMultivector<T> outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = RGaUnilinearCombinationTerm<T>.Create(
                scalar.ScalarFromValue(metric.ScalarProcessor),
                metric,
                inputBasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public RGaUnilinearCombination<T> Add(RGaProcessor<T> metric, IEnumerable<RGaBasisBlade> inputBasisBladeList, Func<RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
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

    public RGaUnilinearCombination<T> Add(IEnumerable<RGaBasisBlade> inputBasisBladeList, Func<RGaBasisBlade, RGaMultivector<T>> basisMapFunc)
    {
        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            Add(inputBasisBlade,
                basisMapFunc(inputBasisBlade)
            );
        }

        return this;
    }
    
    public RGaUnilinearCombination<T> AddOutermorphism(RGaProcessor<T> metric, T[,] vectorMapArray)
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


    public IReadOnlyList<ulong> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.InputBasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<ulong> GetOutputBasisBladeIDs()
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

    public RGaUnilinearCombination<T> GetSubCombinationWithOutputId(ulong outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaUnilinearCombination<T>(termList);
    }

    public RGaUnilinearCombination<T> GetSubCombination(Func<RGaUnilinearCombinationTerm<T>, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaUnilinearCombination<T>(termList);
    }

    public IEnumerator<RGaUnilinearCombinationTerm<T>> GetEnumerator()
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