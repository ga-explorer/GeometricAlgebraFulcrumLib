﻿using System;
using DataStructuresLib.BitManipulation;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Storage;

public sealed class LibTempVariableSet :
    LibTempStorage
{
    public Func<int, string> VariableNameFunc { get; }

    public override string this[int index]
        => index >= 0 && index <= Count
            ? VariableNameFunc(index)
            : throw new IndexOutOfRangeException();


    public LibTempVariableSet(int count, Func<int, string> variableNameFunc)
        : base(count)
    {
        VariableNameFunc = variableNameFunc;
    }


    public override string GetDeclareCode()
    {
        return Count
            .GetRange(i => $"var {VariableNameFunc(i)} = 0d;")
            .Concatenate(Environment.NewLine);
    }
}