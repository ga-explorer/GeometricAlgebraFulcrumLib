using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaBivector<T> :
    XGaKVector<T>
{
    private readonly IReadOnlyDictionary<IndexSet, T> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Bivector";

    public override int Count
        => _idScalarDictionary.Count;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 2;
        }
    }

    public override int Grade
        => 2;

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<T> Scalars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _idScalarDictionary.Values;
    }

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _idScalarDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaBivector(XGaProcessor<T> processor)
        : base(processor)
    {
        _idScalarDictionary = new EmptyDictionary<IndexSet, T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaBivector(XGaProcessor<T> processor, KeyValuePair<IndexSet, T> basisScalarPair)
        : base(processor)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, T>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaBivector(XGaProcessor<T> processor, IReadOnlyDictionary<IndexSet, T> scalarDictionary)
        : base(processor)
    {
        _idScalarDictionary = scalarDictionary;

        Debug.Assert(IsValid());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidBivectorDictionary(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, T> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart()
    {
        return Processor.VectorZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart(Func<int, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetBivectorPart()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivectorPart(Func<int, int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = 
            _idScalarDictionary.Where(term => 
                filterFunc(term.Key.FirstIndex, term.Key.LastIndex)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetPart(Func<T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }


    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Processor.BasisBlade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Scalar()
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetBasisBladeScalar(IndexSet basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
            ? ScalarProcessor.ScalarFromValue(scalar)
            : ScalarProcessor.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out T scalar)
    {
        scalar = ScalarProcessor.ZeroValue;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out T scalar)
    {
        if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }


    public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, T>(Processor.BasisBlade(p.Key), p.Value)
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Simplify()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivector<T> ToGradedMultivector()
    {
        if (IsZero)
            return Processor.GradedMultivectorZero;

        var gradeKVectorDictionary = 
            new SingleItemDictionary<int, XGaKVector<T>>(2, this);

        return new XGaGradedMultivector<T>(Processor, gradeKVectorDictionary);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> ToUniformMultivector()
    {
        return ToComposer().GetUniformMultivector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Convert(XGaFloat64Processor metric)
    {
        if (IsZero)
            return metric.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    ScalarProcessor.ToFloat64(term.Value)
                )
            );

        return metric
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
    {
        if (IsZero)
            return metric.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
    {
        if (IsZero)
            return metric.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T2>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> MapScalars(ScalarTransformer<T> transformer)
    {
        return MapScalars(transformer.MapScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> ReflectOn(XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(this)
            .Gp(subspace.Inverse())
            .GetBivectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
    {
        return ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
    {
        return ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = ReflectOn(subspace);

        var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> ReflectDualOnDual(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return Fdp(subspaceInverse).Gp(subspace).GetBivectorPart();
    }
    
    /// <summary>
    /// Create a simple rotor from an angle and a 2-blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <returns></returns>
    public XGaPureRotor<T> CreatePureRotor(LinPolarAngle<T> rotationAngle)
    {
        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();
        
        var rotationBladeScalar =
            sinHalfAngle / (-ESpSquared()).Sqrt();

        return XGaPureRotor<T>.Create(
            cosHalfAngle.ScalarValue,
            rotationBladeScalar * this
        );
    }

    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is computed automatically using the given processor which must
    /// be of numerical type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public XGaPureRotor<T> CreatePureRotor()
    {
        var processor = ScalarProcessor;

        if (!processor.IsNumeric)
            throw new InvalidOperationException();

        var bladeSignature = SpSquared();

        if (bladeSignature.IsNearZero())
            return XGaPureRotor<T>.Create(
                processor.OneValue,
                this
            );

        if (bladeSignature.IsNegative())
        {
            var alpha = (-bladeSignature).Sqrt();
            var scalar = alpha.Cos().ScalarValue;
            var bivector = alpha.Sin() / alpha * this;

            return XGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            var scalar = alpha.Cosh().ScalarValue;
            var bivector = alpha.Sinh() / alpha * this;

            return XGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
    }

    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is given by the user
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bladeSignatureSign"></param>
    /// <returns></returns>
    public XGaPureRotor<T> CreatePureRotor(IntegerSign bladeSignatureSign)
    {
        var processor = ScalarProcessor;

        if (bladeSignatureSign.IsZero)
            return XGaPureRotor<T>.Create(
                processor.OneValue,
                this
            );

        var bladeSignature = SpSquared();

        if (bladeSignatureSign.IsNegative)
        {
            var alpha = (-bladeSignature).Sqrt();
            var scalar = alpha.Cos().ScalarValue;
            var bivector = alpha.Sin() / alpha * this;

            return XGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            var scalar = alpha.Cosh().ScalarValue;
            var bivector = alpha.Sinh() / alpha * this;

            return XGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
    }
 

    
    public T[] BivectorToArray1D()
    {
        var array = ScalarProcessor.CreateArrayZero1D((int) KvSpaceDimensions);

        foreach (var (id, scalar) in IdScalarPairs)
        {
            var index1 = id.FirstIndex;
            var index2 = id.LastIndex;

            var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            array[index] = scalar;
        }

        return array;
    }

    public T[] BivectorToArray1D(int arraySize)
    {
        if ((ulong) arraySize < KvSpaceDimensions)
            throw new InvalidOperationException();

        var array = ScalarProcessor.CreateArrayZero1D(arraySize);

        foreach (var (id, scalar) in IdScalarPairs)
        {
            var index1 = id.FirstIndex;
            var index2 = id.LastIndex;

            var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            array[index] = scalar;
        }

        return array;
    }

    public  T[,] BivectorToArray2D()
    {
        var array = ScalarProcessor.CreateArrayZero2D(VSpaceDimensions);

        foreach (var (id, scalar) in IdScalarPairs)
        {
            var index1 = id.FirstIndex;
            var index2 = id.LastIndex;
            
            array[index1, index2] = scalar;
            array[index2, index1] = ScalarProcessor.Negative(scalar).ScalarValue;
        }

        return array;
    }
    
    public  T[,] BivectorToArray2D(int arraySize)
    {
        if (arraySize < VSpaceDimensions)
            throw new InvalidOperationException();

        var array = ScalarProcessor.CreateArrayZero2D(arraySize);

        foreach (var (id, scalar) in IdScalarPairs)
        {
            var index1 = id.FirstIndex;
            var index2 = id.LastIndex;
            
            array[index1, index2] = scalar;
            array[index2, index1] = ScalarProcessor.Negative(scalar).ScalarValue;
        }

        return array;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (IsZero)
            return $"'{ScalarProcessor.Zero.ToText()}'<>";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id.Count)
            .ThenBy(p => p.Key.Id)
            .Select(p => $"'{ScalarProcessor.ToText(p.Value)}'{p.Key}")
            .Concatenate(" + ");
    }
}