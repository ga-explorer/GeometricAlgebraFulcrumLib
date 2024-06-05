using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Storage;

public abstract class LibTempStorage :
    IReadOnlyList<string>
{
    public static LibTempVariable CreateVariable(string name)
    {
        return new LibTempVariable(name);
    }

    public static LibTempArray CreateArray(string name, int count)
    {
        return new LibTempArray(count, name);
    }

    public static LibTempVariableSet CreateVariableSet(Func<int, string> name, int count)
    {
        return new LibTempVariableSet(count, name);
    }

    public static LibTempStorage CreateAutomatic(string name, int count)
    {
        return count switch
        {
            1 => new LibTempVariable(name),

            <= 10 => new LibTempVariableSet(
                count,
                i => $"tempScalar{i}"
            ),

            _ => new LibTempArray(count, name)
        };
    }


    public int Count { get; }

    public abstract string this[int index] { get; }


    protected LibTempStorage(int count)
    {
        Count = count;
    }


    public abstract string GetDeclareCode();

    public IEnumerator<string> GetEnumerator()
    {
        return Count.GetRange(i => this[i]).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}