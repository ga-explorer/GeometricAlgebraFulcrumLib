using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Combinations;

public class XGaBilinearCombination<T> : 
    IReadOnlyCollection<XGaBilinearCombinationTerm<T>>, 
    IXGaLinearCombination
{
    private readonly Dictionary<Triplet<IndexSet>, XGaBilinearCombinationTerm<T>> _termList;


    public bool AssumeEqualInputs { get; }

    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public XGaBilinearCombination(bool assumeEqualInputs)
    {
        AssumeEqualInputs = assumeEqualInputs;
        _termList = new Dictionary<Triplet<IndexSet>, XGaBilinearCombinationTerm<T>>();
    }

    public XGaBilinearCombination(bool assumeEqualInputs, Dictionary<Triplet<IndexSet>, XGaBilinearCombinationTerm<T>> termList)
    {
        AssumeEqualInputs = assumeEqualInputs;
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

    public XGaBilinearCombination<T> Set(XGaBilinearCombinationTerm<T> term)
    {
        var key = 
            term.GetUniqueKey(AssumeEqualInputs);

        if (term.IsInputScalarZero)
            _termList.Remove(key);

        else if (_termList.TryGetValue(key, out var oldTerm))
            oldTerm.InputScalar = term.InputScalar;

        else
            _termList.Add(key, term);

        return this;
    }

    public XGaBilinearCombination<T> Add(XGaBilinearCombinationTerm<T> term)
    {
        if (term.IsInputScalarZero)
            return this;

        var key = 
            term.GetUniqueKey(AssumeEqualInputs);

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

    public XGaBilinearCombination<T> Add(IEnumerable<XGaBilinearCombinationTerm<T>> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public XGaBilinearCombination<T> Add(Scalar<T> inputScalar, XGaProcessor<T> metric, IndexSet input1BasisBladeId, IndexSet input2BasisBladeId, IndexSet outputBasisBladeId)
    {
        var term = XGaBilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public XGaBilinearCombination<T> Add(XGaProcessor<T> metric, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        var term = XGaBilinearCombinationTerm<T>.Create(
            metric,
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaBilinearCombination<T> Add(Scalar<T> inputScalar, XGaProcessor<T> metric, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        var term = XGaBilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaBilinearCombination<T> Add(XGaProcessor<T> metric, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaMultivector<T> outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector)
        {
            var term = XGaBilinearCombinationTerm<T>.Create(
                scalar.ScalarFromValue(metric.ScalarProcessor),
                metric,
                input1BasisBlade.Id,
                input2BasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaBilinearCombination<T> Add(XGaProcessor<T> metric, IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
            foreach (var input2BasisBlade in input2BasisBladeList)
            {
                Add(
                    metric,
                    input1BasisBlade,
                    input2BasisBlade,
                    basisMapFunc(input1BasisBlade, input2BasisBlade)
                );
            }

        return this;
    }

    public XGaBilinearCombination<T> Add(XGaProcessor<T> metric, IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, XGaMultivector<T>> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
            foreach (var input2BasisBlade in input2BasisBladeList)
            {
                Add(
                    metric,
                    input1BasisBlade,
                    input2BasisBlade,
                    basisMapFunc(input1BasisBlade, input2BasisBlade)
                );
            }

        return this;
    }

    public ImmutableSortedSet<IndexSet> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
            .ToImmutableSortedSet();
    }

    public ImmutableSortedSet<IndexSet> GetInput1BasisBladeIDs()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeIDs();

        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .ToImmutableSortedSet();
    }

    public ImmutableSortedSet<IndexSet> GetInput2BasisBladeIDs()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeIDs();

        return _termList
            .Values
            .Select(term => term.Input2BasisBladeId)
            .ToImmutableSortedSet();
    }

    public ImmutableSortedSet<IndexSet> GetOutputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeId)
            .ToImmutableSortedSet();
    }

    public IndexSet GetInputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.Input1BasisBladeGrade)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeGrade))
            .ToIndexSet(false);
    }

    public IndexSet GetInput1BasisBladeGrades()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeGrades();

        return _termList
            .Values
            .Select(term => term.Input1BasisBladeGrade)
            .ToIndexSet(false);
    }

    public IndexSet GetInput2BasisBladeGrades()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeGrades();

        return _termList
            .Values
            .Select(term => term.Input2BasisBladeGrade)
            .ToIndexSet(false);
    }

    public IndexSet GetOutputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeGrade)
            .ToIndexSet(false);
    }

    public XGaBilinearCombination<T> GetSubCombinationWithOutputId(IndexSet outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaBilinearCombination<T>(AssumeEqualInputs, termList);
    }

    public XGaBilinearCombination<T> GetSubCombination(Func<XGaBilinearCombinationTerm<T>, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaBilinearCombination<T>(AssumeEqualInputs, termList);
    }

    public IEnumerator<XGaBilinearCombinationTerm<T>> GetEnumerator()
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