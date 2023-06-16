using System.Collections;
using TextComposerLib.Text.Linear;

namespace WebComposerLib.LaTeX.CodeComposer.Code.Arguments
{
    /// <summary>
    /// This class represents a list of command arguments, each can be required or optional
    /// </summary>
    public sealed class LaTeXArgumentsList : ILaTeXArgumentsList
    {
        private readonly List<LaTeXArgument> _argumentsList;


        public int ArgumentsCount
            => _argumentsList.Count;

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

        public int RequiredArgumentsCount
            => _argumentsList.Count(i => !i.IsOptional);

        public int OptionalArgumentsCount
            => _argumentsList.Count(i => i.IsOptional);

        public bool HasArguments
            => _argumentsList.Count > 0;

        public bool HasRequiredArguments
            => _argumentsList.Any(i => !i.IsOptional);

        public bool HasOptionalArguments
            => _argumentsList.Any(i => i.IsOptional);

        public IEnumerable<LaTeXArgument> Arguments
            => _argumentsList;

        public IEnumerable<LaTeXArgument> RequiredArguments
            => _argumentsList.Where(a => !a.IsOptional);

        public IEnumerable<LaTeXArgument> OptionalArguments
            => _argumentsList.Where(a => a.IsOptional);

        public IEnumerable<ILaTeXCodeElement> ArgumentValues
            => _argumentsList.Select(i => i.Value);

        public IEnumerable<ILaTeXCodeElement> RequiredArgumentValues
            => _argumentsList.Where(i => !i.IsOptional).Select(i => i.Value);

        public IEnumerable<ILaTeXCodeElement> OptionalArgumentValues
            => _argumentsList.Where(i => i.IsOptional).Select(i => i.Value);

        public LaTeXArgument this[int argIndex]
            => _argumentsList[argIndex];


        public LaTeXArgumentsList()
        {
            _argumentsList = new List<LaTeXArgument>();
        }

        public LaTeXArgumentsList(int capacity)
        {
            _argumentsList = new List<LaTeXArgument>(capacity);
        }

        public LaTeXArgumentsList(IEnumerable<LaTeXArgument> items)
        {
            _argumentsList = new List<LaTeXArgument>(items);
        }

        public LaTeXArgumentsList(params LaTeXArgument[] items)
        {
            _argumentsList = new List<LaTeXArgument>(items);
        }


        public IEnumerator<LaTeXArgument> GetEnumerator()
        {
            return _argumentsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _argumentsList.GetEnumerator();
        }


        public LaTeXArgumentsList Clear()
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

        public LaTeXArgumentsList Remove(int argIndex)
        {
            if (argIndex < _fixedArgumentsCount)
                throw new InvalidOperationException("Argument is fixed and can't be removed");

            _argumentsList.RemoveAt(argIndex);

            return this;
        }


        public LaTeXArgumentsList Add()
        {
            _argumentsList.Add(LaTeXArgument.Create());

            return this;
        }

        public LaTeXArgumentsList Add(LaTeXArgument argValue)
        {
            _argumentsList.Add(argValue ?? LaTeXArgument.Create());

            return this;
        }

        public LaTeXArgumentsList Add(ILaTeXCodeElement argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(argValue));

            return this;
        }

        public LaTeXArgumentsList Add(string argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(int argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(long argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(float argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(double argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(IEnumerable<LaTeXArgument> argValueList)
        {
            foreach (var argValue in argValueList)
                _argumentsList.Add(argValue ?? LaTeXArgument.Create());

            return this;
        }

        public LaTeXArgumentsList Add(IEnumerable<ILaTeXCodeElement> argValueList)
        {
            foreach (var argValue in argValueList)
                _argumentsList.Add(LaTeXArgument.Create(argValue));

            return this;
        }

        public LaTeXArgumentsList Add(bool isOptional, ILaTeXCodeElement argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(isOptional, argValue));

            return this;
        }

        public LaTeXArgumentsList Add(bool isOptional, string argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(bool isOptional, int argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(bool isOptional, long argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(bool isOptional, float argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(bool isOptional, double argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Add(bool isOptional, IEnumerable<ILaTeXCodeElement> argValueList)
        {
            foreach (var argValue in argValueList)
                _argumentsList.Add(LaTeXArgument.Create(isOptional, argValue));

            return this;
        }

        public LaTeXArgumentsList AddOptional()
        {
            _argumentsList.Add(LaTeXArgument.Create(true));

            return this;
        }

        public LaTeXArgumentsList AddOptional(ILaTeXCodeElement argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(true, argValue));

            return this;
        }

        public LaTeXArgumentsList AddOptional(string argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList AddOptional(int argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList AddOptional(long argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList AddOptional(float argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList AddOptional(double argValue)
        {
            _argumentsList.Add(LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList AddOptional(IEnumerable<ILaTeXCodeElement> argValueList)
        {
            foreach (var argValue in argValueList)
                _argumentsList.Add(LaTeXArgument.Create(true, argValue));

            return this;
        }


        public LaTeXArgumentsList Set(int argIndex)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create();

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, LaTeXArgument argValue)
        {
            _argumentsList[argIndex] = argValue ?? LaTeXArgument.Create();

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, ILaTeXCodeElement argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(argValue);

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, string argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, int argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, long argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, float argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, double argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, bool isOptional, ILaTeXCodeElement argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(isOptional, argValue);

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, bool isOptional, string argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(isOptional, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, bool isOptional, int argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(isOptional, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, bool isOptional, long argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(isOptional, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, bool isOptional, float argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(isOptional, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList Set(int argIndex, bool isOptional, double argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(isOptional, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList SetOptional(int argIndex)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(true);

            return this;
        }

        public LaTeXArgumentsList SetOptional(int argIndex, ILaTeXCodeElement argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(true, argValue);

            return this;
        }

        public LaTeXArgumentsList SetOptional(int argIndex, string argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(true, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList SetOptional(int argIndex, int argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(true, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList SetOptional(int argIndex, long argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(true, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList SetOptional(int argIndex, float argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(true, argValue.ToLaTeXText());

            return this;
        }

        public LaTeXArgumentsList SetOptional(int argIndex, double argValue)
        {
            _argumentsList[argIndex] = LaTeXArgument.Create(true, argValue.ToLaTeXText());

            return this;
        }


        public LaTeXArgumentsList Insert(int argIndex)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create());

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, LaTeXArgument argValue)
        {
            _argumentsList.Insert(argIndex, argValue ?? LaTeXArgument.Create());

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, ILaTeXCodeElement argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(argValue));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, string argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, int argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, long argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, float argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, double argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, bool isOptional, ILaTeXCodeElement argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(isOptional, argValue));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, bool isOptional, string argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, bool isOptional, int argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, bool isOptional, long argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, bool isOptional, float argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList Insert(int argIndex, bool isOptional, double argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(isOptional, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList InsertOptional(int argIndex)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(true));

            return this;
        }

        public LaTeXArgumentsList InsertOptional(int argIndex, ILaTeXCodeElement argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(true, argValue));

            return this;
        }

        public LaTeXArgumentsList InsertOptional(int argIndex, string argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList InsertOptional(int argIndex, int argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList InsertOptional(int argIndex, long argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList InsertOptional(int argIndex, float argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }

        public LaTeXArgumentsList InsertOptional(int argIndex, double argValue)
        {
            _argumentsList.Insert(argIndex, LaTeXArgument.Create(true, argValue.ToLaTeXText()));

            return this;
        }


        public bool IsEmpty()
        {
            return _argumentsList.Count == 0;
        }

        public void ToText(LinearTextComposer composer)
        {
            foreach (var arg in _argumentsList)
                arg.ToText(composer);
        }
    }
}

