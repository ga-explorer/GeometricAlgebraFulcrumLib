using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public abstract partial class XGaKVector<T> :
    XGaMultivector<T>
{
    public abstract int Grade { get; }

    public ulong KvSpaceDimensions
        => IsZero ? 1 : VSpaceDimensions.GetBinomialCoefficient(Grade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaKVector(XGaProcessor<T> processor)
        : base(processor)
    {
    }
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsScalar()
    {
        return IsZero || Grade == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsVector()
    {
        return IsZero || Grade == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsBivector()
    {
        return IsZero || Grade == 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsKVector(int grade)
    {
        return IsZero || Grade == grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd()
    {
        return Grade.IsOdd();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd(int maxGrade)
    {
        return Grade.IsOdd(maxGrade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven()
    {
        return Grade.IsEven();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven(int maxGrade)
    {
        return Grade.IsEven(maxGrade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsScalarPart()
    {
        return !IsZero && Grade == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsVectorPart()
    {
        return !IsZero && Grade == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsBivectorPart()
    {
        return !IsZero && Grade == 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKVectorPart(int grade)
    {
        return !IsZero && Grade == grade;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart()
    {
        return !IsZero && Grade.IsOdd();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart(int maxGrade)
    {
        return !IsZero && Grade.IsOdd(maxGrade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart()
    {
        return !IsZero && Grade.IsEven();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart(int maxGrade)
    {
        return !IsZero && Grade.IsEven(maxGrade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMinGrade()
    {
        return IsZero ? 0 : Grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMaxGrade()
    {
        return IsZero ? 0 : Grade;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetKVectorCount()
    {
        return IsZero ? 0 : 1;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.KVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetFirstKVectorPart()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetEvenPart()
    {
        return IsEven()
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetEvenPart(int maxGrade)
    {
        return IsEven(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetOddPart()
    {
        return IsOdd()
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetOddPart(int maxGrade)
    {
        return IsOdd(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<XGaKVector<T>> GetKVectorParts()
    {
        if (!IsZero) yield return this;
    }

    public abstract IReadOnlyDictionary<IndexSet, T> GetIdScalarDictionary();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, T>> GetKVectorArrayItems()
    {
        return IdScalarPairs.Select(
            term => 
                new KeyValuePair<ulong, T>(
                    term.Key.BasisBladeIdToIndex(), 
                    term.Value
                )
        );
    }
        
        
    /// <summary>
    /// Analyze this k-vector, assumed to be a blade, into a set of orthonormal
    /// vectors, where their outer product is equal to the original blade, up to
    /// a scalar factor
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<XGaVector<T>> BladeToVectors()
    {
        // Find basis blade with the largest scalar magnitude in the current blade
        var maxId = IndexSet.EmptySet;
        var maxScalar = ScalarProcessor.ZeroValue;

        foreach (var (id, scalar) in IdScalarPairs)
        {
            var scalar1 = ScalarProcessor.Abs(scalar).ScalarValue;

            if (!ScalarProcessor.Subtract(scalar1, maxScalar).IsPositive()) 
                continue;

            maxId = id;
            maxScalar = scalar1;
        }

        var probeVectors = 
            maxId
                .Select(index => Processor.VectorTerm(index))
                .ToImmutableArray();

        return BladeToVectors(probeVectors);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> BladeToVectors(params int[] probeBasisVectorIndices)
    {
        var probeVectors = 
            probeBasisVectorIndices
                .Select(index => Processor.VectorTerm(index))
                .ToImmutableArray();

        return BladeToVectors(probeVectors);
    }

    /// <summary>
    /// Analyze this k-vector, assumed to be a blade, into a set of orthonormal
    /// vectors, where their outer product is equal to the original blade, up to
    /// a scalar factor
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> BladeToVectors(IEnumerable<int> probeBasisVectorIndices)
    {
        var probeVectors = 
            probeBasisVectorIndices
                .Select(index => Processor.VectorTerm(index))
                .ToImmutableArray();

        return BladeToVectors(probeVectors);
    }

    public IReadOnlyList<XGaVector<T>> BladeToVectors(IReadOnlyList<XGaVector<T>> probeVectors)
    {
        if (IsZero || Grade == 0)
            return [];

        if (this is XGaVector<T> vectorBlade)
            return [vectorBlade];

        var vectorList = new List<XGaVector<T>>(Grade);

        // All computations are done assuming Euclidean space,
        // independent of the actual metric
            
        // Normalize the current blade
        var oldBlade = DivideByENorm();

        // Repeat until the current blade is a single vector
        var basisVectorIndex = 0;
        while (oldBlade.Grade > 1)
        {
            // Get the next significant basis vector in the original blade
            var basisVector = probeVectors[basisVectorIndex];

            // Get orthogonal complement of basis vector inside the current blade
            // This is the new smaller blade for the next iteration
            var newBlade = 
                basisVector.ELcp(oldBlade).DivideByENorm();

            if (newBlade.Grade == oldBlade.Grade)
                continue;

            // Get the Un-Dual of the new blade inside the current blade
            // This is one vector of the required vectors
            var vector = newBlade.ELcp(oldBlade.EInverse()).GetVectorPart().DivideByENorm();

            vectorList.Add(vector);

            oldBlade = newBlade;
            basisVectorIndex++;
        }
            
        // Add the current blade, which is a single vector
        if (vectorList.Count < Grade)
            vectorList.Add(
                oldBlade.GetVectorPart().DivideByENorm()
            );

        return vectorList;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivector<T> ToGradedMultivector()
    {
        if (IsZero)
            return Processor.GradedMultivectorZero;

        var gradeKVectorDictionary = 
            new SingleItemDictionary<int, XGaKVector<T>>(Grade, this);

        return new XGaGradedMultivector<T>(
            Processor,
            gradeKVectorDictionary
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> ToUniformMultivector()
    {
        return ToComposer().GetUniformMultivector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Convert(XGaFloat64Processor metric)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Convert(metric),
            XGaVector<T> mv1 => mv1.Convert(metric),
            XGaBivector<T> mv1 => mv1.Convert(metric),
            _ => ((XGaHigherKVector<T>)this).Convert(metric)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Convert(metric, scalarMapping),
            XGaVector<T> mv1 => mv1.Convert(metric, scalarMapping),
            XGaBivector<T> mv1 => mv1.Convert(metric, scalarMapping),
            _ => ((XGaHigherKVector<T>)this).Convert(metric, scalarMapping)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Convert(metric, scalarMapping),
            XGaVector<T> mv1 => mv1.Convert(metric, scalarMapping),
            XGaBivector<T> mv1 => mv1.Convert(metric, scalarMapping),
            _ => ((XGaHigherKVector<T>)this).Convert(metric, scalarMapping)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearBlade()
    {
        return Gp(Reverse())
            .GetKVectorParts()
            .All(kv1 => 
                kv1.Grade == 0 || kv1.IsNearZero()
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        return this switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetPart(Func<T, bool> filterFunc)
    {
        return this switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        return this switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> MapScalars(Func<T, T> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> s => s.MapScalar(scalarMapping),
            XGaVector<T> v => v.MapScalars(scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T2> MapScalars<T2>(XGaProcessor<T2> processor, Func<T, T2> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> s => s.MapScalar(scalarMapping),
            XGaVector<T> v => v.MapScalars(scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T2> MapScalars<T2>(XGaProcessor<T2> processor, Func<IndexSet, T, T2> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
    {
        return this switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> MapScalars(ScalarTransformer<T> transformer)
    {
        return MapScalars(transformer.MapScalarValue);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Negative()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Negative(),
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Reverse()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> => this,
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GradeInvolution()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1,
            XGaHigherKVector<T> mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> CliffordConjugate()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Conjugate()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> mv1 => mv1.Conjugate(),
            XGaBivector<T> mv1 => mv1.Conjugate(),
            XGaHigherKVector<T> mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(int scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(double scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(T scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
      
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(Scalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(IScalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(int scalar)
    {
        var scalarValue = ScalarProcessor.ScalarFromNumber(scalar).ScalarValue;

        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(double scalar)
    {
        var scalarValue = ScalarProcessor.ScalarFromNumber(scalar).ScalarValue;

        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(T scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(Scalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(IScalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByENorm()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENorm(),
            XGaVector<T> mv1 => mv1.DivideByENorm(),
            XGaBivector<T> mv1 => mv1.DivideByENorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByENormSquared()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENormSquared(),
            XGaVector<T> mv1 => mv1.DivideByENormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByENormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByNorm()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNorm(),
            XGaVector<T> mv1 => mv1.DivideByNorm(),
            XGaBivector<T> mv1 => mv1.DivideByNorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByNormSquared()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNormSquared(),
            XGaVector<T> mv1 => mv1.DivideByNormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByNormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EInverse()
    {
        return this switch
        {
            XGaScalar<T> s => s.EInverse(),
            XGaVector<T> v => v.EInverse(),
            XGaBivector<T> bv => bv.EInverse(),
            XGaHigherKVector<T> kv => kv.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Inverse()
    {
        return this switch
        {
            XGaScalar<T> s => s.Inverse(),
            XGaVector<T> v => v.Inverse(),
            XGaBivector<T> bv => bv.Inverse(),
            XGaHigherKVector<T> kv => kv.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> PseudoInverse()
    {
        return this switch
        {
            XGaScalar<T> s => s.PseudoInverse(),
            XGaVector<T> v => v.PseudoInverse(),
            XGaBivector<T> bv => bv.PseudoInverse(),
            XGaHigherKVector<T> kv => kv.PseudoInverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EDual(XGaKVector<T> blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Dual(XGaKVector<T> blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalar(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EUnDual(XGaKVector<T> blade)
    {
        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalar(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> UnDual(XGaKVector<T> blade)
    {
        return Lcp(blade);
    }
    
    
    public T[] KVectorToArray(int vSpaceDimensions)
    {
        if (vSpaceDimensions < VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var kvSpaceDimensions =
            (int)vSpaceDimensions.GetBinomialCoefficient(Grade);

        var array = ScalarProcessor.CreateArrayZero1D(kvSpaceDimensions);

        foreach (var (index, scalar) in GetKVectorArrayItems())
            array[index] = scalar;

        return array;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ReflectOn(XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(this)
            .Gp(subspace.Inverse())
            .GetKVectorPart(Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);

        var n = Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);

        var n = Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = ReflectOn(subspace);

        var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ReflectDualOnDual(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);

        var n = (Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return Fdp(subspaceInverse).Gp(subspace).GetKVectorPart(Grade);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSubspace<T> ToSubspace()
    {
        return new XGaSubspace<T>(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSubspace<T> DualToSubspace(int vSpaceDimensions)
    {
        return new XGaSubspace<T>(
            Dual(vSpaceDimensions)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSubspace<T> UnDualToSubspace(int vSpaceDimensions)
    {
        return new XGaSubspace<T>(
            UnDual(vSpaceDimensions)
        );
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