using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.ISP;

public sealed class GrPovRayPolynomialSurface : 
    GrPovRayPolynomialObject, 
    IGrPovRayInfiniteSolidObject,
    IReadOnlyDictionary<Triplet<int>, GrPovRayFloat32Value>
{
    private readonly Dictionary<Triplet<int>, GrPovRayFloat32Value> _powerCoefDictionary 
        = new Dictionary<Triplet<int>, GrPovRayFloat32Value>();

    public int Order { get; }

    public int Count 
        => _powerCoefDictionary.Count;

    public int MaxCoefsCount 
        => (Order + 1) * (Order + 2) * (Order + 3) / 6;

    public IEnumerable<Triplet<int>> Keys 
        => _powerCoefDictionary.Keys;

    public IEnumerable<GrPovRayFloat32Value> Values 
        => _powerCoefDictionary.Values;

    public GrPovRayFloat32Value this[Triplet<int> key] 
    {
        get
        {
            ValidateXyzPowers(key);

            return _powerCoefDictionary.TryGetValue(key, out var value)
                ? value : GrPovRayFloat32Value.Zero;
        }
        set
        {
            ValidateXyzPowers(key);

            if (value.GetPovRayCode() == "0")
                _powerCoefDictionary.Remove(key);

            else if (_powerCoefDictionary.ContainsKey(key))
                _powerCoefDictionary[key] = value;

            else
                _powerCoefDictionary.Add(key, value);
        }
    }

    public GrPovRayFloat32Value this[int xPower, int yPower, int zPower]
    {
        get
        {
            var key = XyzPowersToKey(xPower, yPower, zPower);

            return _powerCoefDictionary.TryGetValue(key, out var value)
                ? value : GrPovRayFloat32Value.Zero;
        }
        set
        {
            var key = XyzPowersToKey(xPower, yPower, zPower);

            if (value.GetPovRayCode() == "0")
                _powerCoefDictionary.Remove(key);

            else if (_powerCoefDictionary.ContainsKey(key))
                _powerCoefDictionary[key] = value;

            else
                _powerCoefDictionary.Add(key, value);
        }
    }
    

    internal GrPovRayPolynomialSurface(int order)
    {
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order));

        Order = order;
    }


    public bool XyzPowersValid(Triplet<int> key)
    {
        var (xPower, yPower, zPower) = key;

        return xPower >= 0 && 
               yPower >= 0 && 
               zPower >= 0 && 
               xPower + yPower + zPower <= Order;
    }
    
    public bool XyzPowersValid(int xPower, int yPower, int zPower)
    {
        return xPower >= 0 && 
               yPower >= 0 && 
               zPower >= 0 && 
               xPower + yPower + zPower <= Order;
    }
    
    public void ValidateXyzPowers(Triplet<int> key)
    {
        if (!XyzPowersValid(key))
            throw new ArgumentOutOfRangeException();
    }
    
    public void ValidateXyzPowers(int xPower, int yPower, int zPower)
    {
        if (!XyzPowersValid(xPower, yPower, zPower))
            throw new ArgumentOutOfRangeException();
    }
    
    public Triplet<int> XyzPowersToKey(int xPower, int yPower, int zPower)
    {
        return XyzPowersValid(xPower, yPower, zPower)
            ? new Triplet<int>(xPower, yPower, zPower)
            : throw new ArgumentOutOfRangeException();
    }


    public GrPovRayPolynomialSurface Clear()
    {
        _powerCoefDictionary.Clear();

        return this;
    }
    
    public GrPovRayPolynomialSurface Remove(Triplet<int> key)
    {
        ValidateXyzPowers(key);

        _powerCoefDictionary.Remove(key);

        return this;
    }

    public GrPovRayPolynomialSurface Remove(int xPower, int yPower, int zPower)
    {
        var key = XyzPowersToKey(xPower, yPower, zPower);
        
        _powerCoefDictionary.Remove(key);

        return this;
    }
    
    public bool ContainsKey(Triplet<int> key)
    {
        ValidateXyzPowers(key);

        return _powerCoefDictionary.ContainsKey(key);
    }
    
    public bool ContainsKey(int xPower, int yPower, int zPower)
    {
        var key = XyzPowersToKey(xPower, yPower, zPower);
        
        return _powerCoefDictionary.ContainsKey(key);
    }

    public bool TryGetValue(Triplet<int> key, out GrPovRayFloat32Value value)
    {
        ValidateXyzPowers(key);

        return _powerCoefDictionary.TryGetValue(key, out value);
    }
    
    public bool TryGetValue(int xPower, int yPower, int zPower, out GrPovRayFloat32Value value)
    {
        var key = XyzPowersToKey(xPower, yPower, zPower);
        
        return _powerCoefDictionary.TryGetValue(key, out value);
    }

    
    public GrPovRayPolynomialSurface SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    private string GetCoefsString()
    {
        return _powerCoefDictionary
            .OrderByDescending(p => p.Key.Item1)
            .ThenByDescending(p => p.Key.Item2)
            .ThenByDescending(p => p.Key.Item3)
            .Select(p => $"xyz({p.Key.Item1},{p.Key.Item2},{p.Key.Item3}):{p.Value.GetPovRayCode()}")
            .Concatenate("," + Environment.NewLine);
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("polynomial {")
            .IncreaseIndentation()
            .AppendAtNewLine(Order + ",")
            .AppendAtNewLine(GetCoefsString())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }

    public IEnumerator<KeyValuePair<Triplet<int>, GrPovRayFloat32Value>> GetEnumerator()
    {
        return _powerCoefDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}