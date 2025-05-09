using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Combinations;

public class RGaFloat64TrilinearCombination : 
    IReadOnlyCollection<RGaFloat64TrilinearCombinationTerm>, 
    IRGaLinearCombination
{
    private readonly Dictionary<Quad<ulong>, RGaFloat64TrilinearCombinationTerm> _termList;


    public RGaFloat64TrilinearCombinationTerm.InputsKind InputsKind { get; }

    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public RGaFloat64TrilinearCombination(RGaFloat64TrilinearCombinationTerm.InputsKind inputsKind)
    {
        InputsKind = inputsKind;
        _termList = new Dictionary<Quad<ulong>, RGaFloat64TrilinearCombinationTerm>();
    }

    public RGaFloat64TrilinearCombination(RGaFloat64TrilinearCombinationTerm.InputsKind inputsKind, Dictionary<Quad<ulong>, RGaFloat64TrilinearCombinationTerm> termList)
    {
        InputsKind = inputsKind;
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

    public RGaFloat64TrilinearCombination Set(RGaFloat64TrilinearCombinationTerm term)
    {
        var key = 
            term.GetUniqueKey(InputsKind);

        if (term.IsInputScalarZero)
            _termList.Remove(key);

        else if (_termList.TryGetValue(key, out var oldTerm))
            oldTerm.InputScalar = term.InputScalar;

        else
            _termList.Add(key, term);

        return this;
    }

    public RGaFloat64TrilinearCombination Add(RGaFloat64TrilinearCombinationTerm term)
    {
        if (term.IsInputScalarZero)
            return this;

        var key = 
            term.GetUniqueKey(InputsKind);

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

    public RGaFloat64TrilinearCombination Add(IEnumerable<RGaFloat64TrilinearCombinationTerm> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public RGaFloat64TrilinearCombination Add(Float64Scalar inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong input3BasisBladeId, ulong outputBasisBladeId)
    {
        var term = RGaFloat64TrilinearCombinationTerm.Create(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            input3BasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public RGaFloat64TrilinearCombination Add(RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        var term = RGaFloat64TrilinearCombinationTerm.Create(
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaFloat64TrilinearCombination Add(Float64Scalar inputScalar, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaBasisBlade outputBasisBlade)
    {
        var term = RGaFloat64TrilinearCombinationTerm.Create(
            inputScalar,
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaFloat64TrilinearCombination Add(RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaFloat64Multivector outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector)
        {
            var term = RGaFloat64TrilinearCombinationTerm.Create(
                scalar,
                input1BasisBlade.Metric,
                input1BasisBlade.Id,
                input2BasisBlade.Id,
                input3BasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public RGaFloat64TrilinearCombination Add(IEnumerable<RGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input3BasisBladeList, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
        foreach (var input2BasisBlade in input2BasisBladeList)
        foreach (var input3BasisBlade in input3BasisBladeList)
        {
            var outputBasisBlade = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade, 
                input3BasisBlade
            );

            Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outputBasisBlade
            );
        }

        return this;
    }

    public RGaFloat64TrilinearCombination Add(IEnumerable<RGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input3BasisBladeList, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, RGaFloat64Multivector> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
        foreach (var input2BasisBlade in input2BasisBladeList)
        foreach (var input3BasisBlade in input3BasisBladeList)
        {
            var outputMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade, 
                input3BasisBlade
            );

            Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outputMultivector
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
            .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<ulong> GetInput12BasisBladeIDs()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<ulong> GetInput13BasisBladeIDs()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<ulong> GetInput23BasisBladeIDs()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
                .ToImmutableSortedSet()
        };
    }

    public IReadOnlyList<ulong> GetInput1BasisBladeIDs()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeIDs(),
                
            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<ulong> GetInput2BasisBladeIDs()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),
                
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeId)
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<ulong> GetInput3BasisBladeIDs()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeIDs(),
                
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input3BasisBladeId)
                .ToImmutableSortedSet()
        };
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
            .Concat(_termList.Values.Select(term => term.Input3BasisBladeGrade))
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<int> GetInput12BasisBladeGrades()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeGrade)
                .Concat(_termList.Values.Select(term => term.Input2BasisBladeGrade))
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<int> GetInput13BasisBladeGrades()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeGrade)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeGrade))
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<int> GetInput23BasisBladeGrades()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeGrade)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeGrade))
                .ToImmutableSortedSet()
        };
    }

    public IReadOnlyList<int> GetInput1BasisBladeGrades()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeGrades(),
                
            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeGrade)
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<int> GetInput2BasisBladeGrades()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),
                
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeGrade)
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<int> GetInput3BasisBladeGrades()
    {
        return InputsKind switch
        {
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeGrades(),
                
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input3BasisBladeGrade)
                .ToImmutableSortedSet()
        };
    }

    public IReadOnlyList<int> GetOutputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public RGaFloat64TrilinearCombination GetSubCombinationWithOutputId(ulong outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId == outputBasisBladeId)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaFloat64TrilinearCombination(InputsKind, termList);
    }

    public RGaFloat64TrilinearCombination GetSubCombination(Func<RGaFloat64TrilinearCombinationTerm, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaFloat64TrilinearCombination(InputsKind, termList);
    }

    public IEnumerator<RGaFloat64TrilinearCombinationTerm> GetEnumerator()
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