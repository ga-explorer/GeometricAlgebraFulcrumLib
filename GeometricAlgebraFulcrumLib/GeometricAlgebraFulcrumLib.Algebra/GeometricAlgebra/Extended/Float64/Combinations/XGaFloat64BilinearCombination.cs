using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Combinations;

public class XGaFloat64BilinearCombination : 
    IReadOnlyCollection<XGaFloat64BilinearCombinationTerm>, 
    IXGaLinearCombination
{
    private readonly Dictionary<Triplet<IndexSet>, XGaFloat64BilinearCombinationTerm> _termList;


    public bool AssumeEqualInputs { get; }

    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public XGaFloat64BilinearCombination(bool assumeEqualInputs)
    {
        AssumeEqualInputs = assumeEqualInputs;
        _termList = new Dictionary<Triplet<IndexSet>, XGaFloat64BilinearCombinationTerm>();
    }

    public XGaFloat64BilinearCombination(bool assumeEqualInputs, Dictionary<Triplet<IndexSet>, XGaFloat64BilinearCombinationTerm> termList)
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

    public XGaFloat64BilinearCombination Set(XGaFloat64BilinearCombinationTerm term)
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

    public XGaFloat64BilinearCombination Add(XGaFloat64BilinearCombinationTerm term)
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

    public XGaFloat64BilinearCombination Add(IEnumerable<XGaFloat64BilinearCombinationTerm> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public XGaFloat64BilinearCombination Add(Float64Scalar inputScalar, XGaMetric metric, IndexSet input1BasisBladeId, IndexSet input2BasisBladeId, IndexSet outputBasisBladeId)
    {
        var term = XGaFloat64BilinearCombinationTerm.Create(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public XGaFloat64BilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64BilinearCombinationTerm.Create(
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaFloat64BilinearCombination Add(Float64Scalar inputScalar, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64BilinearCombinationTerm.Create(
            inputScalar,
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaFloat64BilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaFloat64Multivector outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector)
        {
            var term = XGaFloat64BilinearCombinationTerm.Create(
                scalar,
                input1BasisBlade.Metric,
                input1BasisBlade.Id,
                input2BasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaFloat64BilinearCombination Add(IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
            foreach (var input2BasisBlade in input2BasisBladeList)
            {
                Add(
                    input1BasisBlade,
                    input2BasisBlade,
                    basisMapFunc(input1BasisBlade, input2BasisBlade)
                );
            }

        return this;
    }

    public XGaFloat64BilinearCombination Add(IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
            foreach (var input2BasisBlade in input2BasisBladeList)
            {
                Add(
                    input1BasisBlade,
                    input2BasisBlade,
                    basisMapFunc(input1BasisBlade, input2BasisBlade)
                );
            }

        return this;
    }

    public IReadOnlyList<IndexSet> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<IndexSet> GetInput1BasisBladeIDs()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeIDs();

        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<IndexSet> GetInput2BasisBladeIDs()
    {
        if (AssumeEqualInputs)
            return GetInputBasisBladeIDs();

        return _termList
            .Values
            .Select(term => term.Input2BasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<IndexSet> GetOutputBasisBladeIDs()
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

    public XGaFloat64BilinearCombination GetSubCombinationWithOutputId(IndexSet outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64BilinearCombination(AssumeEqualInputs, termList);
    }

    public XGaFloat64BilinearCombination GetSubCombination(Func<XGaFloat64BilinearCombinationTerm, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64BilinearCombination(AssumeEqualInputs, termList);
    }

    public IEnumerator<XGaFloat64BilinearCombinationTerm> GetEnumerator()
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