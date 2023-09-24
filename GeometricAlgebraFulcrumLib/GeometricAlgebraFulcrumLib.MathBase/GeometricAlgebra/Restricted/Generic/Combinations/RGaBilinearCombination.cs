using System.Collections;
using System.Collections.Immutable;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Combinations;

public class RGaBilinearCombination<T> : 
    IReadOnlyCollection<RGaBilinearCombinationTerm<T>>, 
    IRGaLinearCombination
{
    private readonly Dictionary<Triplet<ulong>, RGaBilinearCombinationTerm<T>> _termList;


    public bool AssumeEqualInputs { get; }

    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public RGaBilinearCombination(bool assumeEqualInputs)
    {
        AssumeEqualInputs = assumeEqualInputs;
        _termList = new Dictionary<Triplet<ulong>, RGaBilinearCombinationTerm<T>>();
    }

    public RGaBilinearCombination(bool assumeEqualInputs, Dictionary<Triplet<ulong>, RGaBilinearCombinationTerm<T>> termList)
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

    public RGaBilinearCombination<T> Set(RGaBilinearCombinationTerm<T> term)
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

    public RGaBilinearCombination<T> Add(RGaBilinearCombinationTerm<T> term)
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

    public RGaBilinearCombination<T> Add(IEnumerable<RGaBilinearCombinationTerm<T>> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public RGaBilinearCombination<T> Add(Scalar<T> inputScalar, RGaProcessor<T> metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong outputBasisBladeId)
    {
        var term = RGaBilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public RGaBilinearCombination<T> Add(RGaProcessor<T> metric, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        var term = RGaBilinearCombinationTerm<T>.Create(
            metric,
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaBilinearCombination<T> Add(Scalar<T> inputScalar, RGaProcessor<T> metric, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade outputBasisBlade)
    {
        var term = RGaBilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaBilinearCombination<T> Add(RGaProcessor<T> metric, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaMultivector<T> outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector)
        {
            var term = RGaBilinearCombinationTerm<T>.Create(
                scalar.CreateScalar(metric.ScalarProcessor),
                metric,
                input1BasisBlade.Id,
                input2BasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public RGaBilinearCombination<T> Add(RGaProcessor<T> metric, IEnumerable<RGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input2BasisBladeList, Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
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

    public RGaBilinearCombination<T> Add(RGaProcessor<T> metric, IEnumerable<RGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input2BasisBladeList, Func<RGaBasisBlade, RGaBasisBlade, RGaMultivector<T>> basisMapFunc)
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

    public IReadOnlyList<ulong> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<ulong> GetInput1BasisBladeIDs()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeIDs();

        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<ulong> GetInput2BasisBladeIDs()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeIDs();

        return _termList
            .Values
            .Select(term => term.Input2BasisBladeId)
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
            .Select(term => term.Input1BasisBladeGrade)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeGrade))
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetInput1BasisBladeGrades()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeGrades();

        return _termList
            .Values
            .Select(term => term.Input1BasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetInput2BasisBladeGrades()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeGrades();

        return _termList
            .Values
            .Select(term => term.Input2BasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetOutputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public RGaBilinearCombination<T> GetSubCombinationWithOutputId(ulong outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaBilinearCombination<T>(AssumeEqualInputs, termList);
    }

    public RGaBilinearCombination<T> GetSubCombination(Func<RGaBilinearCombinationTerm<T>, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaBilinearCombination<T>(AssumeEqualInputs, termList);
    }

    public IEnumerator<RGaBilinearCombinationTerm<T>> GetEnumerator()
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