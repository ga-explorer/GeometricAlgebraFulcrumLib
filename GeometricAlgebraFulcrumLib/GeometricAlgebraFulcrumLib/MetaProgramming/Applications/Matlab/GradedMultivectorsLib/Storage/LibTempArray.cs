using System;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Storage;

public sealed class LibTempArray :
    LibTempStorage
{
    public string ArrayName { get; }

    public override string this[int index]
        => index >= 0 && index <= Count
            ? $"{ArrayName}[{index}]"
            : throw new IndexOutOfRangeException();


    public LibTempArray(int count, string arrayName)
        : base(count)
    {
        ArrayName = arrayName;
    }


    public override string GetDeclareCode()
    {
        return $"var {ArrayName} = new double[{Count}];";
    }
}