using System.Collections;
using System.Collections.Immutable;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Combinations;

public class RGaTrilinearCombination<T> : 
    IReadOnlyCollection<RGaTrilinearCombinationTerm<T>>, 
    IRGaLinearCombination
{
    private readonly Dictionary<Quad<ulong>, RGaTrilinearCombinationTerm<T>> _termList;


    public RGaTrilinearCombinationTerm<T>.InputsKind InputsKind { get; }

    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public RGaTrilinearCombination(RGaTrilinearCombinationTerm<T>.InputsKind inputsKind)
    {
        InputsKind = inputsKind;
        _termList = new Dictionary<Quad<ulong>, RGaTrilinearCombinationTerm<T>>();
    }

    public RGaTrilinearCombination(RGaTrilinearCombinationTerm<T>.InputsKind inputsKind, Dictionary<Quad<ulong>, RGaTrilinearCombinationTerm<T>> termList)
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

    public RGaTrilinearCombination<T> Set(RGaTrilinearCombinationTerm<T> term)
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

    public RGaTrilinearCombination<T> Add(RGaTrilinearCombinationTerm<T> term)
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

    public RGaTrilinearCombination<T> Add(IEnumerable<RGaTrilinearCombinationTerm<T>> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public RGaTrilinearCombination<T> Add(Scalar<T> inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong input3BasisBladeId, ulong outputBasisBladeId)
    {
        var term = RGaTrilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            input3BasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public RGaTrilinearCombination<T> Add(RGaProcessor<T> metric, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        var term = RGaTrilinearCombinationTerm<T>.Create(
            metric,
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaTrilinearCombination<T> Add(Scalar<T> inputScalar, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaBasisBlade outputBasisBlade)
    {
        var term = RGaTrilinearCombinationTerm<T>.Create(
            inputScalar,
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaTrilinearCombination<T> Add(RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaMultivector<T> outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = RGaTrilinearCombinationTerm<T>.Create(
                scalar.CreateScalar(metric.ScalarProcessor),
                metric,
                input1BasisBlade.Id,
                input2BasisBlade.Id,
                input3BasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public RGaTrilinearCombination<T> Add(RGaProcessor<T> metric, IEnumerable<RGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input3BasisBladeList, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
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
                metric,
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outputBasisBlade
            );
        }

        return this;
    }

    public RGaTrilinearCombination<T> Add(IEnumerable<RGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<RGaBasisBlade> input3BasisBladeList, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, RGaMultivector<T>> basisMapFunc)
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),
                
            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeIDs(),
                
            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),
                
            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            RGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            RGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeGrades(),
                
            RGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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

    public RGaTrilinearCombination<T> GetSubCombinationWithOutputId(ulong outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaTrilinearCombination<T>(InputsKind, termList);
    }

    public RGaTrilinearCombination<T> GetSubCombination(Func<RGaTrilinearCombinationTerm<T>, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaTrilinearCombination<T>(InputsKind, termList);
    }

    public IEnumerator<RGaTrilinearCombinationTerm<T>> GetEnumerator()
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