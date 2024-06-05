using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code.Arguments;

public sealed class LaTeXOptionalArgumentsList : ILaTeXArgumentsList
{
    private readonly List<ILaTeXCodeElement> _argumentsList;

    private int _fixedArgumentsCount;


    public int FixedArgumentsCount
    {
        get => _fixedArgumentsCount;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _fixedArgumentsCount = value;

            if (_fixedArgumentsCount <= _argumentsList.Count)
                return;

            var addedCount = _fixedArgumentsCount - _argumentsList.Count;
            for (var i = 0; i < addedCount; i++)
                _argumentsList.Add(LaTeXArgument.Create());
        }
    }

    public int ArgumentsCount
        => _argumentsList.Count;

    public int RequiredArgumentsCount
        => 0;

    public int OptionalArgumentsCount
        => _argumentsList.Count;

    public bool HasArguments
        => _argumentsList.Count > 0;

    public bool HasRequiredArguments
        => false;

    public bool HasOptionalArguments
        => _argumentsList.Count > 0;

    public IEnumerable<LaTeXArgument> Arguments
        => _argumentsList.Select(c => c.ToLaTeXOptionalArgument());

    public IEnumerable<LaTeXArgument> RequiredArguments
        => Enumerable.Empty<LaTeXArgument>();

    public IEnumerable<LaTeXArgument> OptionalArguments
        => _argumentsList.Select(c => c.ToLaTeXOptionalArgument());

    public IEnumerable<ILaTeXCodeElement> ArgumentValues
        => _argumentsList;

    public IEnumerable<ILaTeXCodeElement> RequiredArgumentValues
        => Enumerable.Empty<ILaTeXCodeElement>();

    public IEnumerable<ILaTeXCodeElement> OptionalArgumentValues
        => _argumentsList;

    public LaTeXArgument this[int argIndex]
        => _argumentsList[argIndex].ToLaTeXOptionalArgument();


    public LaTeXOptionalArgumentsList()
    {
        _argumentsList = new List<ILaTeXCodeElement>();
    }

    public LaTeXOptionalArgumentsList(int capacity)
    {
        _argumentsList = new List<ILaTeXCodeElement>(capacity);
    }

    public LaTeXOptionalArgumentsList(IEnumerable<ILaTeXCodeElement> argValuesList)
    {
        _argumentsList = new List<ILaTeXCodeElement>(argValuesList);
    }

    public LaTeXOptionalArgumentsList(params ILaTeXCodeElement[] argValuesList)
    {
        _argumentsList = new List<ILaTeXCodeElement>(argValuesList);
    }


    public IEnumerator<LaTeXArgument> GetEnumerator()
    {
        return _argumentsList.Select(c => c.ToLaTeXOptionalArgument()).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _argumentsList.Select(c => c.ToLaTeXOptionalArgument()).GetEnumerator();
    }


    public LaTeXOptionalArgumentsList Clear()
    {
        if (_fixedArgumentsCount == 0)
        {
            _argumentsList.Clear();

            return this;
        }

        for (var i = _argumentsList.Count - 1; i >= _fixedArgumentsCount; i--)
            _argumentsList.RemoveAt(i);

        return this;
    }

    public LaTeXOptionalArgumentsList Remove(int argIndex)
    {
        if (argIndex < _fixedArgumentsCount)
            throw new InvalidOperationException("Argument is fixed and can't be removed");

        _argumentsList.RemoveAt(argIndex);

        return this;
    }


    public bool IsEmpty()
    {
        return _argumentsList.Count == 0;
    }

    public void ToText(LinearTextComposer composer)
    {
        foreach (var arg in Arguments)
            arg.ToText(composer);
    }


    public LaTeXOptionalArgumentsList Add(ILaTeXCodeElement argValue)
    {
        _argumentsList.Add(argValue);

        return this;
    }

    public LaTeXOptionalArgumentsList Add(string argValue)
    {
        _argumentsList.Add(argValue.ToLaTeXText());

        return this;
    }

    public LaTeXOptionalArgumentsList Add(int argValue)
    {
        _argumentsList.Add(argValue.ToLaTeXText());

        return this;
    }

    public LaTeXOptionalArgumentsList Add(long argValue)
    {
        _argumentsList.Add(argValue.ToLaTeXText());

        return this;
    }

    public LaTeXOptionalArgumentsList Add(float argValue)
    {
        _argumentsList.Add(argValue.ToLaTeXText());

        return this;
    }

    public LaTeXOptionalArgumentsList Add(double argValue)
    {
        _argumentsList.Add(argValue.ToLaTeXText());

        return this;
    }

    public LaTeXOptionalArgumentsList Add(IEnumerable<ILaTeXCodeElement> argValueList)
    {
        _argumentsList.AddRange(argValueList);

        return this;
    }

    public LaTeXOptionalArgumentsList Add(params ILaTeXCodeElement[] argValueList)
    {
        _argumentsList.AddRange(argValueList);

        return this;
    }
}