﻿using System.Collections;
using System.Collections.Immutable;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Combinations;

public class XGaTrilinearCombination<T> : 
    IReadOnlyCollection<XGaTrilinearCombinationTerm<T>>, 
    IXGaLinearCombination
{
    private readonly Dictionary<Quad<IIndexSet>, XGaTrilinearCombinationTerm<T>> _termList;


    public XGaTrilinearCombinationTerm<T>.InputsKind InputsKind { get; }

    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public XGaTrilinearCombination(XGaTrilinearCombinationTerm<T>.InputsKind inputsKind)
    {
        InputsKind = inputsKind;
        _termList = new Dictionary<Quad<IIndexSet>, XGaTrilinearCombinationTerm<T>>();
    }

    public XGaTrilinearCombination(XGaTrilinearCombinationTerm<T>.InputsKind inputsKind, Dictionary<Quad<IIndexSet>, XGaTrilinearCombinationTerm<T>> termList)
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

    public XGaTrilinearCombination<T> Set(XGaTrilinearCombinationTerm<T> term)
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

    public XGaTrilinearCombination<T> Add(XGaTrilinearCombinationTerm<T> term)
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

    public XGaTrilinearCombination<T> Add(IEnumerable<XGaTrilinearCombinationTerm<T>> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public XGaTrilinearCombination<T> Add(Scalar<T> inputScalar, XGaMetric metric, IIndexSet input1BasisBladeId, IIndexSet input2BasisBladeId, IIndexSet input3BasisBladeId, IIndexSet outputBasisBladeId)
    {
        var term = XGaTrilinearCombinationTerm<T>.Create(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            input3BasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public XGaTrilinearCombination<T> Add(XGaProcessor<T> metric, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        var term = XGaTrilinearCombinationTerm<T>.Create(
            metric,
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaTrilinearCombination<T> Add(Scalar<T> inputScalar, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        var term = XGaTrilinearCombinationTerm<T>.Create(
            inputScalar,
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaTrilinearCombination<T> Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, XGaMultivector<T> outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = XGaTrilinearCombinationTerm<T>.Create(
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

    public XGaTrilinearCombination<T> Add(XGaProcessor<T> metric, IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input3BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
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

    public XGaTrilinearCombination<T> Add(IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input3BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, XGaMultivector<T>> basisMapFunc)
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

    public IReadOnlyList<IIndexSet> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
            .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<IIndexSet> GetInput12BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<IIndexSet> GetInput13BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<IIndexSet> GetInput23BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
                .ToImmutableSortedSet()
        };
    }

    public IReadOnlyList<IIndexSet> GetInput1BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeIDs(),
                
            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<IIndexSet> GetInput2BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),
                
            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeId)
                .ToImmutableSortedSet()
        };
    }
    
    public IReadOnlyList<IIndexSet> GetInput3BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeIDs(),
                
            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input3BasisBladeId)
                .ToImmutableSortedSet()
        };
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
            .Select(term => term.Input1BasisBladeGrade)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeGrade))
            .Concat(_termList.Values.Select(term => term.Input3BasisBladeGrade))
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<int> GetInput12BasisBladeGrades()
    {
        return InputsKind switch
        {
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
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
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
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
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),
                
            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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
            XGaTrilinearCombinationTerm<T>.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaTrilinearCombinationTerm<T>.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeGrades(),
                
            XGaTrilinearCombinationTerm<T>.InputsKind.EqualSecondThird => 
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

    public XGaTrilinearCombination<T> GetSubCombinationWithOutputId(IIndexSet outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaTrilinearCombination<T>(InputsKind, termList);
    }

    public XGaTrilinearCombination<T> GetSubCombination(Func<XGaTrilinearCombinationTerm<T>, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaTrilinearCombination<T>(InputsKind, termList);
    }

    public IEnumerator<XGaTrilinearCombinationTerm<T>> GetEnumerator()
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