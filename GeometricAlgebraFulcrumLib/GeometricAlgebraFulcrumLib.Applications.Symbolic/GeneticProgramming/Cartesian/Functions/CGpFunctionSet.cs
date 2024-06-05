using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions
{
    //public sealed class CGpFunctionSet
    //{
    //    public int Count { get; }

    //    public Func<int, Pair<int>> GetArityRange { get; }

    //    public Func<int, int, CGpFloat64Function> GetFunction { get; }


    //    public CGpFunctionSet(int count, Func<int, Pair<int>> getArityRange, Func<int, int, CGpFloat64Function> getFunction)
    //    {
    //        Count = count;
    //        GetArityRange = getArityRange;
    //        GetFunction = getFunction;
    //    }

        
    //    public CGpFloat64Function GetRandomFunction(int parametersMaxArity)
    //    {
    //        var functionIndex = Count switch
    //        {
    //            < 1 => throw new InvalidOperationException(),
    //            1 => 0,
    //            _ => CGpParameters.GetRandomIndex(Count)
    //        };

    //        var (minArity, maxArity) = 
    //            GetArityRange(functionIndex);

    //        maxArity = maxArity < 0
    //            ? parametersMaxArity
    //            : Math.Min(maxArity, parametersMaxArity);

    //        if (minArity > maxArity)
    //            throw new InvalidOperationException();

    //        var arity = CGpParameters.GetRandomInteger(minArity, maxArity);

    //        return GetFunction(functionIndex, arity);
    //    }
    //}

    public sealed class CGpFunctionSet :
        IReadOnlyList<CGpFloat64Function>
    {
        private readonly KeyValueList<string, CGpFloat64Function> _functionList
            = new KeyValueList<string, CGpFloat64Function>();


        public int Count
            => _functionList.Count;

        public CGpFloat64Function this[int index]
            => _functionList.GetValueByIndex(index);

        public CGpFloat64Function this[string name]
            => _functionList.GetValueByKey(name);


        public CGpFunctionSet Clear()
        {
            _functionList.Clear();

            return this;
        }

        public bool ContainsFunction(string name)
        {
            return _functionList.ContainsKey(name);
        }

        public bool TryGetFunction(string name, out CGpFloat64Function fn)
        {
            return _functionList.TryGetValue(name, out fn);
        }

        public CGpFunctionSet AddFunction(CGpFloat64Function fn)
        {
            _functionList.Append(fn.Name, fn);

            return this;
        }

        public CGpFunctionSet AddFunctions(params CGpFloat64Function[] fnList)
        {
            foreach (var fn in fnList)
                _functionList.Append(fn.Name, fn);

            return this;
        }

        public void GetRandomFunction(int parametersMaxArity, out int functionIndex, out int arity)
        {
            functionIndex = Count switch
            {
                < 1 => throw new InvalidOperationException(),
                1 => 0,
                _ => CGpParameters.GetRandomIndex(Count)
            };

            var (minArity, maxArity) = 
                _functionList[functionIndex].ArityRange;

            maxArity = maxArity < 0
                ? parametersMaxArity
                : Math.Min(maxArity, parametersMaxArity);

            if (minArity > maxArity)
                throw new InvalidOperationException();

            arity = CGpParameters.GetRandomInteger(minArity, maxArity);
        }

        public IEnumerator<CGpFloat64Function> GetEnumerator()
        {
            return _functionList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return _functionList.Select(
                fn => fn.Name
            ).Concatenate(", ");
        }
    }
}
