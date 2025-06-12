using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors
{
    public partial class XGaFloat64Processor
    {
        
        public XGaFloat64KVectorComposer CreateScalarComposer()
        {
            return new XGaFloat64KVectorComposer(this, 0);
        }

        
        public XGaFloat64KVectorComposer CreateVectorComposer()
        {
            return new XGaFloat64KVectorComposer(this, 1);
        }

        
        public XGaFloat64KVectorComposer CreateBivectorComposer()
        {
            return new XGaFloat64KVectorComposer(this, 2);
        }

        
        public XGaFloat64KVectorComposer CreateTrivectorComposer()
        {
            return new XGaFloat64KVectorComposer(this, 3);
        }

        
        public XGaFloat64KVectorComposer CreateKVectorComposer(int grade)
        {
            return new XGaFloat64KVectorComposer(this, grade);
        }

        
        public XGaFloat64GradedMultivectorComposer CreateMultivectorComposer()
        {
            return new XGaFloat64GradedMultivectorComposer(this);
        }

        /// <summary>
        /// This is kept for debugging and validation only.
        /// You should use CreateKVectorComposer\CreateMultivectorComposer
        /// </summary>
        /// <returns></returns>
        
        public XGaFloat64UniformMultivectorComposer CreateUniformComposer()
        {
            return new XGaFloat64UniformMultivectorComposer(this);
        }


        public XGaFloat64Scalar ParseScalar(string inputText)
        {
            var composer = CreateScalarComposer();
            
            foreach (var (indexArray, scalar) in inputText.XGaParseTerms())
                composer.AddTerm(indexArray, scalar);

            return composer.GetScalar();
        }
        
        public XGaFloat64Vector ParseVector(string inputText)
        {
            var composer = CreateVectorComposer();
            
            foreach (var (indexArray, scalar) in inputText.XGaParseTerms())
                composer.AddTerm(indexArray, scalar);

            return composer.GetVector();
        }
        
        public XGaFloat64Bivector ParseBivector(string inputText)
        {
            var composer = CreateBivectorComposer();
            
            foreach (var (indexArray, scalar) in inputText.XGaParseTerms())
                composer.AddTerm(indexArray, scalar);

            return composer.GetBivector();
        }
        
        public XGaFloat64HigherKVector ParseTrivector(string inputText)
        {
            var composer = CreateTrivectorComposer();
            
            foreach (var (indexArray, scalar) in inputText.XGaParseTerms())
                composer.AddTerm(indexArray, scalar);

            return composer.GetHigherKVector();
        }
        
        public XGaFloat64KVector ParseKVector(string inputText, int grade)
        {
            var composer = CreateKVectorComposer(grade);
            
            foreach (var (indexArray, scalar) in inputText.XGaParseTerms())
                composer.AddTerm(indexArray, scalar);

            return composer.GetKVector();
        }

        public XGaFloat64Multivector Parse(string inputText)
        {
            var composer = CreateMultivectorComposer();

            foreach (var (indexArray, scalar) in inputText.XGaParseTerms())
                composer.AddTerm(indexArray, scalar);

            return composer.GetMultivector();
        }


        
        public XGaFloat64Scalar Scalar(double scalarValue)
        {
            return new XGaFloat64Scalar(this, scalarValue);
        }
        
        public XGaFloat64Scalar ScalarFromSum(double scalar1, double scalar2)
        {
            return new XGaFloat64Scalar(
                this,

                scalar1 + scalar2
            );
        }
        
        public XGaFloat64Scalar ScalarFromSum(params double[] scalarValueList)
        {
            var scalar = 0d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    continue;

                scalar += scalarValue;
            }

            return new XGaFloat64Scalar(this, scalar);
        }
        
        public XGaFloat64Scalar ScalarFromSum(IEnumerable<double> scalarValueList)
        {
            var scalar = 0d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    continue;

                scalar += scalarValue;
            }

            return new XGaFloat64Scalar(this, scalar);
        }
        
        public XGaFloat64Scalar ScalarFromProduct(double scalar1, double scalar2)
        {
            return new XGaFloat64Scalar(
                this,
                scalar1 * scalar2
            );
        }
        
        public XGaFloat64Scalar ScalarFromProduct(int sign, double scalar1, double scalar2)
        {
            return new XGaFloat64Scalar(
                this,
                sign * scalar1 * scalar2
            );
        }
        
        public XGaFloat64Scalar ScalarFromProduct(params double[] scalarValueList)
        {
            var scalar = 1d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    return new XGaFloat64Scalar(this);

                scalar *= scalarValue;
            }

            return new XGaFloat64Scalar(this, scalar);
        }
        
        public XGaFloat64Scalar ScalarFromProduct(IEnumerable<double> scalarValueList)
        {
            var scalar = 1d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    return new XGaFloat64Scalar(this);

                scalar *= scalarValue;
            }

            return new XGaFloat64Scalar(this, scalar);
        }

        
        public XGaFloat64Vector VectorTerm(int index)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, double>(
                    index.ToUnitIndexSet(),
                    1d
                );

            return new XGaFloat64Vector(this, basisScalarDictionary);
        }
        
        public XGaFloat64Vector VectorTerm(int index, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Vector(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, double>(
                    index.ToUnitIndexSet(),
                    scalar
                );

            return new XGaFloat64Vector(this, basisScalarDictionary);
        }
        
        public XGaFloat64Vector VectorTerm(KeyValuePair<int, double> indexScalarPair)
        {
            return VectorTerm(indexScalarPair.Key, indexScalarPair.Value);
        }
        
        public XGaFloat64Vector VectorTerm(ulong basisVector)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, double>(basisVector.ToUInt64IndexSet(), 1d);

            return new XGaFloat64Vector(this, basisScalarDictionary);
        }
        
        public XGaFloat64Vector VectorTerm(ulong basisVector, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Vector(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, double>(basisVector.ToUInt64IndexSet(), scalar);

            return new XGaFloat64Vector(this, basisScalarDictionary);
        }
        
        public XGaFloat64Vector VectorTerm(IndexSet basisVector)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, double>(basisVector, 1d);

            return new XGaFloat64Vector(this, basisScalarDictionary);
        }
        
        public XGaFloat64Vector VectorTerm(IndexSet basisVector, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Vector(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, double>(basisVector, scalar);

            return new XGaFloat64Vector(this, basisScalarDictionary);
        }
        
        public XGaFloat64Vector VectorTerm(KeyValuePair<ulong, double> indexScalarPair)
        {
            return VectorTerm(indexScalarPair.Key, indexScalarPair.Value);
        }
        
        public XGaFloat64Vector VectorTerm(KeyValuePair<IndexSet, double> indexScalarPair)
        {
            return VectorTerm(indexScalarPair.Key, indexScalarPair.Value);
        }
        
        
        public XGaFloat64Vector Vector(IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, double>)
                return VectorZero;

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, double>)
                return VectorTerm(basisScalarDictionary.First());

            return new XGaFloat64Vector(this, basisScalarDictionary);
        }
        
        public XGaFloat64Vector Vector(IReadOnlyDictionary<int, double> basisScalarDictionary)
        {
            return new XGaFloat64Vector(
                this, 
                basisScalarDictionary.ToValidXGaVectorDictionary()
            );
        }
        
        public XGaFloat64Vector Vector(params double[] scalarArray)
        {
            return new XGaFloat64Vector(
                this, 
                scalarArray.ToValidXGaVectorDictionary()
            );
        }
        
        public XGaFloat64Vector Vector(IEnumerable<double> scalarList)
        {
            return new XGaFloat64Vector(
                this, 
                scalarList.ToValidXGaVectorDictionary()
            );
        }
        
        public XGaFloat64Vector Vector(int termsCount, Func<int, double> indexToScalarFunc)
        {
            var composer = CreateVectorComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetVectorTerm(index, scalar);
            }

            return composer.GetVector();
        }
        
        public XGaFloat64Vector Vector(LinFloat64Vector vector)
        {
            var idScalarDictionary =
                vector.GetIndexScalarDictionary().ToDictionary(
                    p => p.Key.ToUnitIndexSet(),
                    p => p.Value
                );

            return Vector(idScalarDictionary);
        }
        
        public XGaFloat64Vector Vector(ILinFloat64Vector2D vector)
        {
            return CreateVectorComposer()
                .SetVectorTerm(0, vector.X)
                .SetVectorTerm(1, vector.Y)
                .GetVector();
        }
        
        public XGaFloat64Vector Vector(ILinFloat64Vector3D vector)
        {
            return CreateVectorComposer()
                .SetVectorTerm(0, vector.X)
                .SetVectorTerm(1, vector.Y)
                .SetVectorTerm(2, vector.Z)
                .GetVector();
        }
        
        public XGaFloat64Vector Vector(ILinFloat64Vector4D vector)
        {
            return CreateVectorComposer()
                .SetVectorTerm(0, vector.X)
                .SetVectorTerm(1, vector.Y)
                .SetVectorTerm(2, vector.Z)
                .SetVectorTerm(3, vector.W)
                .GetVector();
        }
        
        
        public XGaFloat64Vector VectorSymmetric(int count)
        {
            return VectorSymmetric(count, 1d);
        }
        
        public XGaFloat64Vector VectorSymmetric(int count, double scalarValue)
        {
            return count switch
            {
                < 0 => throw new InvalidOperationException(),

                0 => new XGaFloat64Vector(
                    this,
                    new EmptyDictionary<IndexSet, double>()
                ),

                1 => new XGaFloat64Vector(
                    this,
                    new SingleItemDictionary<IndexSet, double>(0.ToUnitIndexSet(), scalarValue)
                ),

                _ => new XGaFloat64Vector(
                    this,
                    new XGaFloat64RepeatedScalarVectorDictionary(count, scalarValue)
                )
            };
        }
        
        public XGaFloat64Vector VectorSymmetricUnit(int count)
        {
            return VectorSymmetric(
                count,
                1d / Math.Sqrt(count)
            );
        }

        
        public XGaFloat64Bivector BivectorTerm(int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            return new XGaFloat64Bivector(
                this,
                new SingleItemDictionary<IndexSet, double>(
                    IndexSet.CreatePair(index1, index2),
                    1d
                )
            );
        }
        
        public XGaFloat64Bivector BivectorTerm(int index1, int index2, double scalar)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            if (scalar.IsZero())
                return new XGaFloat64Bivector(this);

            return new XGaFloat64Bivector(
                this,
                new SingleItemDictionary<IndexSet, double>(
                    IndexSet.CreatePair(index1, index2),
                    scalar
                )
            );
        }
        
        public XGaFloat64Bivector BivectorTerm(IPair<int> indexPair)
        {
            return new XGaFloat64Bivector(
                this,
                new SingleItemDictionary<IndexSet, double>(
                    indexPair.ToPairIndexSet(),
                    1d
                )
            );
        }
        
        public XGaFloat64Bivector BivectorTerm(IPair<int> indexPair, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Bivector(this);

            return new XGaFloat64Bivector(
                this,
                new SingleItemDictionary<IndexSet, double>(
                    indexPair.ToPairIndexSet(),
                    scalar
                )
            );
        }
        
        public XGaFloat64Bivector BivectorTerm(KeyValuePair<Int32Pair, double> indexScalarPair)
        {
            return BivectorTerm(
                indexScalarPair.Key, indexScalarPair.Value);
        }
        
        public XGaFloat64Bivector BivectorTerm(KeyValuePair<IndexSet, double> indexScalarPair)
        {
            return BivectorTerm(indexScalarPair.Key, indexScalarPair.Value);
        }
        
        public XGaFloat64Bivector BivectorTerm(IndexSet basisBlade)
        {
            return new XGaFloat64Bivector(
                this,
                new SingleItemDictionary<IndexSet, double>(basisBlade, 1d)
            );
        }
        
        public XGaFloat64Bivector BivectorTerm(IndexSet basisBlade, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Bivector(this);

            return new XGaFloat64Bivector(
                this,
                new SingleItemDictionary<IndexSet, double>(basisBlade, scalar)
            );
        }
        
        
        public XGaFloat64Bivector Bivector(params double[] scalarArray)
        {
            return new XGaFloat64Bivector(
                this,
                scalarArray.ToValidXGaBivectorDictionary()
            );
        }

        public XGaFloat64Bivector Bivector(IEnumerable<double> scalarList)
        {
            return new XGaFloat64Bivector(
                this, 
                scalarList.ToValidXGaBivectorDictionary()
            );
        }

        public XGaFloat64Bivector Bivector(IReadOnlyDictionary<IndexPair, double> basisScalarDictionary)
        {
            return new XGaFloat64Bivector(
                this,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }
        
        public XGaFloat64Bivector Bivector(IReadOnlyDictionary<Int32Pair, double> basisScalarDictionary)
        {
            return new XGaFloat64Bivector(
                this,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }
        
        public XGaFloat64Bivector Bivector(IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, double>)
                return BivectorZero;

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, double>)
                return BivectorTerm(basisScalarDictionary.First());

            return new XGaFloat64Bivector(
                this,
                basisScalarDictionary
            );
        }
        
        
        public XGaFloat64Bivector Bivector2D(double scalar01)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .GetBivector();
        }
        
        public XGaFloat64Bivector Bivector3D(double scalar01, double scalar02, double scalar12)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .SetBivectorTerm(0, 2, scalar02)
                .SetBivectorTerm(1, 2, scalar12)
                .GetBivector();
        }
        
        public XGaFloat64Bivector Bivector3D(LinFloat64Bivector3D bivector)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, bivector.Xy)
                .SetBivectorTerm(0, 2, bivector.Xz)
                .SetBivectorTerm(1, 2, bivector.Yz)
                .GetBivector();
        }
        
        
        public XGaFloat64HigherKVector HigherKVectorZero(int grade)
        {
            return new XGaFloat64HigherKVector(this, grade);
        }
        
        
        public XGaFloat64HigherKVector HigherKVectorTerm(IndexSet id)
        {
            var grade = id.Count;

            return new XGaFloat64HigherKVector(
                this,
                grade,
                new SingleItemDictionary<IndexSet, double>(id, 1d)
            );
        }
        
        public XGaFloat64HigherKVector HigherKVectorTerm(IndexSet id, double scalar)
        {
            var grade = id.Count;

            return scalar.IsZero()
                ? new XGaFloat64HigherKVector(this, grade)
                : new XGaFloat64HigherKVector(this, grade, new SingleItemDictionary<IndexSet, double>(id, scalar));
        }
        
        public XGaFloat64HigherKVector HigherKVectorTerm(KeyValuePair<IndexSet, double> term)
        {
            var (id, scalar) = term.ToTuple();

            var grade = id.Count;

            return scalar.IsZero()
                ? new XGaFloat64HigherKVector(this, grade)
                : new XGaFloat64HigherKVector(this, grade, new SingleItemDictionary<IndexSet, double>(id, scalar));
        }
        
        
        public XGaFloat64HigherKVector HigherKVector(int grade, params double[] scalarArray)
        {
            return grade >= 3
                ? new XGaFloat64HigherKVector(this, grade, scalarArray.ToValidXGaKVectorDictionary(grade))
                : throw new InvalidOperationException();
        }

        public XGaFloat64HigherKVector HigherKVector(int grade, IEnumerable<double> scalarList)
        {
            return grade >= 3
                ? new XGaFloat64HigherKVector(this, grade, scalarList.ToValidXGaKVectorDictionary(grade))
                : throw new InvalidOperationException();
        }

        public XGaFloat64HigherKVector HigherKVector(int grade, IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, double>)
                return HigherKVectorZero(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, double>)
                return HigherKVectorTerm(basisScalarDictionary.First());

            return new XGaFloat64HigherKVector(
                this,
                grade,
                basisScalarDictionary
            );
        }
        
        
        public XGaFloat64KVector KVectorZero(int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return grade switch
            {
                0 => ScalarZero,
                1 => VectorZero,
                2 => BivectorZero,
                _ => new XGaFloat64HigherKVector(this, grade)
            };
        }
        
        public XGaFloat64KVector KVectorTerm(KeyValuePair<IndexSet, double> term)
        {
            var grade = term.Key.Count;

            return grade switch
            {
                0 => new XGaFloat64Scalar(this, term.Value),
                1 => new XGaFloat64Vector(this, term),
                2 => new XGaFloat64Bivector(this, term),
                _ => new XGaFloat64HigherKVector(this, term)
            };
        }
        
        public XGaFloat64KVector KVectorTerm(IndexSet basisBlade)
        {
            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(basisBlade, 1d)
            );
        }
        
        public XGaFloat64KVector KVectorTerm(IndexSet basisBlade, double scalar)
        {
            var grade = basisBlade.Count;

            if (scalar.IsZero())
                return KVectorZero(grade);

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(basisBlade, scalar)
            );
        }
        
        public XGaFloat64KVector KVectorTerm(ulong basisBlade)
        {
            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(basisBlade.ToUInt64IndexSet(), 1d)
            );
        }
        
        public XGaFloat64KVector KVectorTerm(ulong basisBlade, double scalar)
        {
            var grade = basisBlade.Grade();

            if (scalar.IsZero())
                return KVectorZero(grade);

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(basisBlade.ToUInt64IndexSet(), scalar)
            );
        }
        
        
        public XGaFloat64KVector KVector(int grade, params double[] scalarArray)
        {
            return grade switch
            {
                < 0 => throw new InvalidOperationException(),
                0 => scalarArray.Length switch
                {
                    0 => ScalarZero,
                    1 => Scalar(scalarArray[0]),
                    _ => throw new InvalidOperationException()
                },
                1 => Vector(scalarArray),
                2 => Bivector(scalarArray),
                _ => new XGaFloat64HigherKVector(this, grade, scalarArray.ToValidXGaKVectorDictionary(grade))
            };
        }

        public XGaFloat64KVector KVector(int grade, IEnumerable<double> scalarList)
        {
            return grade switch
            {
                < 0 => throw new InvalidOperationException(),
                0 => scalarList.Count() switch
                {
                    0 => ScalarZero,
                    1 => Scalar(scalarList.First()),
                    _ => throw new InvalidOperationException()
                },
                1 => Vector(scalarList),
                2 => Bivector(scalarList),
                _ => new XGaFloat64HigherKVector(this, grade, scalarList.ToValidXGaKVectorDictionary(grade))
            };
        }

        public XGaFloat64KVector KVector(int grade, IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, double>)
                return KVectorZero(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, double>)
                return KVectorTerm(basisScalarDictionary.First());

            return grade switch
            {
                0 => new XGaFloat64Scalar(this, basisScalarDictionary),
                1 => new XGaFloat64Vector(this, basisScalarDictionary),
                2 => new XGaFloat64Bivector(this, basisScalarDictionary),
                _ => new XGaFloat64HigherKVector(this, grade, basisScalarDictionary)
            };
        }
        
        
        public XGaFloat64KVector PseudoScalar(int vSpaceDimensions)
        {
            var id = GetBasisPseudoScalarId(vSpaceDimensions);

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(id, 1d)
            );
        }
        
        public XGaFloat64KVector PseudoScalar(int vSpaceDimensions, double scalarValue)
        {
            var id = GetBasisPseudoScalarId(vSpaceDimensions);

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(id, scalarValue)
            );
        }

        public XGaFloat64KVector PseudoScalarReverse(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var scalar =
                vSpaceDimensions.ReverseIsNegativeOfGrade()
                    ? -1d : 1d;

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(id, scalar)
            );
        }

        public XGaFloat64KVector PseudoScalarConjugate(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                HermitianConjugateSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToFloat64();

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(id, scalar)
            );
        }

        public XGaFloat64KVector PseudoScalarEInverse(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                EGpSquaredSign(id);

            var scalar = sign.ToFloat64();

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(id, scalar)
            );
        }

        public XGaFloat64KVector PseudoScalarInverse(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                GpSquaredSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToFloat64();

            return KVectorTerm(
                new KeyValuePair<IndexSet, double>(id, scalar)
            );
        }


        public XGaFloat64KVector Op(IEnumerable<XGaFloat64Vector> mvList)
        {
            XGaFloat64KVector blade = ScalarOne;

            foreach (var vector in mvList)
            {
                var newBlade = blade.Op(vector);

                if (newBlade.IsZero)
                    return ScalarZero;

                blade = newBlade;
            }

            return blade;
        }

        public XGaFloat64KVector SpanToBlade(IEnumerable<XGaFloat64Vector> mvList)
        {
            XGaFloat64KVector blade = ScalarOne;

            foreach (var vector in mvList)
            {
                var newBlade = blade.Op(vector);

                if (newBlade.IsNearZero())
                    continue;

                blade = newBlade;
            }

            return blade;
        }
        

        public XGaFloat64GradedMultivector GradedMultivector(IndexSet id)
        {
            var grade = id.Count;

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
                grade,
                KVectorTerm(id, 1d)
            );

            return new XGaFloat64GradedMultivector(
                this,
                gradeKVectorDictionary
            );
        }
        
        public XGaFloat64GradedMultivector GradedMultivector(IndexSet id, double scalar)
        {
            var grade = id.Count;

            if (scalar.IsZero())
                return GradedMultivectorZero;

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
                grade,
                KVectorTerm(id, scalar)
            );

            return new XGaFloat64GradedMultivector(
                this,

                gradeKVectorDictionary
            );
        }
        
        public XGaFloat64GradedMultivector GradedMultivector(KeyValuePair<IndexSet, double> basisScalarPair)
        {
            var (id, scalar) = basisScalarPair.ToTuple();
            var grade = id.Count;

            if (scalar.IsZero())
                return GradedMultivectorZero;

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
                grade,
                KVectorTerm(basisScalarPair)
            );

            return new XGaFloat64GradedMultivector(
                this,

                gradeKVectorDictionary
            );
        }
        
        public XGaFloat64GradedMultivector GradedMultivector(params double[] scalarArray)
        {
            var kVectorGroups =
                scalarArray
                    .ToValidXGaUniformMultivectorDictionary()
                    .GroupBy(p => p.Key.Count);

            var composer = CreateMultivectorComposer();

            foreach (var group in kVectorGroups)
                composer.AddKVectorTerms(group.Key, group);
            
            return composer.GetGradedMultivector();
        }

        public XGaFloat64GradedMultivector GradedMultivector(IEnumerable<double> scalarList)
        {
            var kVectorGroups =
                scalarList
                    .ToValidXGaUniformMultivectorDictionary()
                    .GroupBy(p => p.Key.Count);

            var composer = CreateMultivectorComposer();

            foreach (var group in kVectorGroups)
                composer.AddKVectorTerms(group.Key, group);
            
            return composer.GetGradedMultivector();
        }

        public XGaFloat64GradedMultivector GradedMultivector(IReadOnlyDictionary<IndexSet, double> termList)
        {
            return CreateMultivectorComposer()
                .SetTerms(termList)
                .GetGradedMultivector();
        }
        
        public XGaFloat64GradedMultivector GradedMultivector(IReadOnlyDictionary<int, XGaFloat64KVector> gradeKVectorDictionary)
        {
            if (gradeKVectorDictionary.Count == 0 && gradeKVectorDictionary is not EmptyDictionary<int, XGaFloat64KVector>)
                return GradedMultivectorZero;

            if (gradeKVectorDictionary.Count == 1 && gradeKVectorDictionary is not SingleItemDictionary<int, XGaFloat64KVector>)
                return gradeKVectorDictionary.Values.First().ToGradedMultivector();

            return new XGaFloat64GradedMultivector(this, gradeKVectorDictionary);
        }
        
        public XGaFloat64GradedMultivector GradedMultivector(IEnumerable<KeyValuePair<IndexSet, double>> termList)
        {
            return CreateMultivectorComposer()
                .AddTerms(termList)
                .GetGradedMultivector();
        }
        
        public XGaFloat64GradedMultivector GradedMultivectorFromSum(IEnumerable<XGaFloat64KVector> kVectorList)
        {
            var gradeKVectorDictionary =
                SumToGradeKVectorDictionary(kVectorList);

            return gradeKVectorDictionary.Count switch
            {
                0 => GradedMultivectorZero,

                1 => new XGaFloat64GradedMultivector(
                    this,
                    new SingleItemDictionary<int, XGaFloat64KVector>(
                        gradeKVectorDictionary.First()
                    )
                ),

                _ => new XGaFloat64GradedMultivector(
                    this,
                    gradeKVectorDictionary
                )
            };
        }

        
        public XGaFloat64UniformMultivector UniformMultivector(params double[] scalarList)
        {
            return new XGaFloat64UniformMultivector(
                this, 
                scalarList.ToValidXGaUniformMultivectorDictionary()
            );
        }

        public XGaFloat64UniformMultivector UniformMultivector(IEnumerable<double> scalarList)
        {
            return new XGaFloat64UniformMultivector(
                this, 
                scalarList.ToValidXGaUniformMultivectorDictionary()
            );
        }

        public XGaFloat64UniformMultivector UniformMultivector(IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, double>)
                return UniformMultivectorZero;

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, double>)
                return UniformMultivector(basisScalarDictionary.First());

            return new XGaFloat64UniformMultivector(
                this,

                basisScalarDictionary
            );
        }
        
        public XGaFloat64UniformMultivector UniformMultivector(IndexSet basisBlade)
        {
            return new XGaFloat64UniformMultivector(this,

                new SingleItemDictionary<IndexSet, double>(
                    basisBlade,
                    1d
                ));
        }
        
        public XGaFloat64UniformMultivector UniformMultivector(IndexSet basisBlade, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64UniformMultivector(this);

            return new XGaFloat64UniformMultivector(this,

                new SingleItemDictionary<IndexSet, double>(
                    basisBlade,
                    scalar
                ));
        }
        
        public XGaFloat64UniformMultivector UniformMultivector(KeyValuePair<IndexSet, double> basisScalarPair)
        {
            return UniformMultivector(

                basisScalarPair.Key,
                basisScalarPair.Value
            );
        }

        
        public XGaFloat64Multivector Multivector(params double[] scalarArray)
        {
            var kVectorGroups =
                scalarArray
                    .ToValidXGaUniformMultivectorDictionary()
                    .GroupBy(p => p.Key.Count);

            var composer = CreateMultivectorComposer();

            foreach (var group in kVectorGroups)
                composer.AddKVectorTerms(group.Key, group);
            
            return composer.GetMultivector();
        }

        public XGaFloat64Multivector Multivector(IEnumerable<double> scalarList)
        {
            var kVectorGroups =
                scalarList
                    .ToValidXGaUniformMultivectorDictionary()
                    .GroupBy(p => p.Key.Count);

            var composer = CreateMultivectorComposer();

            foreach (var group in kVectorGroups)
                composer.AddKVectorTerms(group.Key, group);
            
            return composer.GetMultivector();
        }

        public XGaFloat64Multivector Multivector(IReadOnlyDictionary<IndexSet, double> termList)
        {
            return CreateMultivectorComposer()
                .SetTerms(termList)
                .GetMultivector();
        }

        public XGaFloat64Multivector Multivector(IReadOnlyDictionary<int, XGaFloat64KVector> gradeKVectorDictionary)
        {
            if (gradeKVectorDictionary.Count == 0 && gradeKVectorDictionary is not EmptyDictionary<int, XGaFloat64KVector>)
                return ScalarZero;

            if (gradeKVectorDictionary.Count == 1 && gradeKVectorDictionary is not SingleItemDictionary<int, XGaFloat64KVector>)
                return gradeKVectorDictionary.Values.First();

            return new XGaFloat64GradedMultivector(this, gradeKVectorDictionary);
        }

        public XGaFloat64Multivector Multivector(IEnumerable<KeyValuePair<IndexSet, double>> termList)
        {
            return CreateMultivectorComposer()
                .AddTerms(termList)
                .GetMultivector();
        }
        
        public XGaFloat64Multivector MultivectorFromSum(IEnumerable<XGaFloat64KVector> kVectorList)
        {
            var gradeKVectorDictionary =
                SumToGradeKVectorDictionary(kVectorList);

            return gradeKVectorDictionary.Count switch
            {
                0 => ScalarZero,
                1 => gradeKVectorDictionary.Values.First(),
                _ => new XGaFloat64GradedMultivector(
                    this,
                    gradeKVectorDictionary
                )
            };
        }
        
        public XGaFloat64Multivector Multivector2D(double scalar, double vectorScalar0, double vectorScalar1, double bivectorScalar)
        {
            return CreateMultivectorComposer()
                .SetScalarTerm(scalar)
                .SetVectorTerm(0, vectorScalar0)
                .SetVectorTerm(1, vectorScalar1)
                .SetBivectorTerm(0, 1, bivectorScalar)
                .GetMultivector();
        }


        protected Dictionary<int, XGaFloat64KVector> SumToGradeKVectorDictionary(IEnumerable<XGaFloat64KVector> kVectorList)
        {
            var gradeKVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

            foreach (var kVector in kVectorList)
            {
                if (kVector.IsZero) continue;

                var grade = kVector.Grade;

                if (gradeKVectorDictionary.TryGetValue(grade, out var kv1))
                {
                    var kv2 = kv1.AddSameGrade(kVector);

                    if (kv2.IsZero)
                        gradeKVectorDictionary.Remove(grade);
                    else
                        gradeKVectorDictionary[grade] = kv2;

                    continue;
                }

                gradeKVectorDictionary.Add(grade, kVector);
            }

            return gradeKVectorDictionary;
        }


    }
}
