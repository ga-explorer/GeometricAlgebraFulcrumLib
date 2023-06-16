using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public sealed class Float64Vector3DComposer :
        IFloat64Tuple3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer Create()
        {
            return new Float64Vector3DComposer();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer Create(double x, double y, double z)
        {
            return new Float64Vector3DComposer(x, y, z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer Create(ITriplet<double> vector)
        {
            return new Float64Vector3DComposer(
                vector.Item1, 
                vector.Item2, 
                vector.Item3
            );
        }
        

        public Float64Scalar X { get; set; }
        
        public Float64Scalar Y { get; set; }

        public Float64Scalar Z { get; set; }
        
        public int VSpaceDimensions 
            => 3;

        public double Item1 
            => X.Value;

        public double Item2 
            => Y.Value;

        public double Item3 
            => Z.Value;

        public double this[int index]
        {
            get => index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => 0
            };
            set
            {
                Debug.Assert(value.IsValid());

                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                else if (index == 2)
                    Z = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
        {
            get
            {
                if (!X.IsZero())
                    yield return new KeyValuePair<int, double>(0, X);

                if (!Y.IsZero())
                    yield return new KeyValuePair<int, double>(1, Y);

                if (!Z.IsZero())
                    yield return new KeyValuePair<int, double>(2, Z);
            }
        }

        public IEnumerable<KeyValuePair<LinBasisVector, double>> BasisBladeScalarPairs
            => IndexScalarPairs.Select(p => 
                new KeyValuePair<LinBasisVector, double>(
                    p.Key.ToLinBasisVector(),
                    p.Value
                )
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Vector3DComposer()
        {
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Vector3DComposer(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return X.IsValid() && Y.IsValid() && Z.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return X.IsZero() && Y.IsZero() && Z.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer Clear()
        {
            X = Float64Scalar.Zero;
            Y = Float64Scalar.Zero;
            Z = Float64Scalar.Zero;

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer ClearTerm(int index)
        {
            if (index == 0)
                X = Float64Scalar.Zero;
            else if (index == 1)
                Y = Float64Scalar.Zero;
            else if (index == 2)
                Z = Float64Scalar.Zero;
            else
                throw new IndexOutOfRangeException();

            return this;
        }
    
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTermScalarValue(int basisBlade)
        {
            return basisBlade switch
            {
                0 => X.Value,
                1 => Y.Value,
                2 => Z.Value,
                _ => 0
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer RemoveTerm(int basisBlade)
        {
            this[basisBlade] = 0;

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SetTerm(int basisBlade, double scalar)
        {
            this[basisBlade] = scalar;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SetTerm(LinUnitBasisVector3D axis)
        {
            if (axis == LinUnitBasisVector3D.PositiveX)
                X = 1;
            
            else if (axis == LinUnitBasisVector3D.PositiveY)
                Y = 1;
            
            else if (axis == LinUnitBasisVector3D.PositiveZ)
                Z = 1;

            else if (axis == LinUnitBasisVector3D.NegativeX)
                X = -1;
            
            else if (axis == LinUnitBasisVector3D.NegativeY)
                Y = -1;
            
            else
                Z = -1;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SetTerm(LinUnitBasisVector3D axis, double scalar)
        {
            if (axis == LinUnitBasisVector3D.PositiveX)
                X = scalar;
            
            else if (axis == LinUnitBasisVector3D.PositiveY)
                Y = scalar;
            
            else if (axis == LinUnitBasisVector3D.PositiveZ)
                Z = scalar;

            else if (axis == LinUnitBasisVector3D.NegativeX)
                X = -scalar;
            
            else if (axis == LinUnitBasisVector3D.NegativeY)
                Y = -scalar;
            
            else
                Z = -scalar;

            return this;
        }
        
        public Float64Vector3DComposer SetVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            return this;
        }

        public Float64Vector3DComposer SetVector(ITriplet<double> vector)
        {
            X = vector.Item1;
            Y = vector.Item2;
            Z = vector.Item3;

            return this;
        }
    
        public Float64Vector3DComposer SetVectorNegative(ITriplet<double> vector)
        {
            X = -vector.Item1;
            Y = -vector.Item2;
            Z = -vector.Item3;

            return this;
        }

        public Float64Vector3DComposer SetVector(ITriplet<double> vector, double scalingFactor)
        {
            X = scalingFactor * vector.Item1;
            Y = scalingFactor * vector.Item2;
            Z = scalingFactor * vector.Item3;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer AddTerm(int basisBlade, double scalar)
        {
            this[basisBlade] += scalar;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer AddTerm(LinUnitBasisVector3D axis)
        {
            if (axis == LinUnitBasisVector3D.PositiveX)
                X += 1;
            
            else if (axis == LinUnitBasisVector3D.PositiveY)
                Y += 1;
            
            else if (axis == LinUnitBasisVector3D.PositiveZ)
                Z += 1;

            else if (axis == LinUnitBasisVector3D.NegativeX)
                X -= 1;
            
            else if (axis == LinUnitBasisVector3D.NegativeY)
                Y -= 1;
            
            else
                Z -= 1;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer AddTerm(LinUnitBasisVector3D axis, double scalar)
        {
            if (axis == LinUnitBasisVector3D.PositiveX)
                X += scalar;
            
            else if (axis == LinUnitBasisVector3D.PositiveY)
                Y += scalar;
            
            else if (axis == LinUnitBasisVector3D.PositiveZ)
                Z += scalar;

            else if (axis == LinUnitBasisVector3D.NegativeX)
                X -= scalar;
            
            else if (axis == LinUnitBasisVector3D.NegativeY)
                Y -= scalar;
            
            else
                Z -= scalar;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer AddVector(ITriplet<double> vector)
        {
            X += vector.Item1;
            Y += vector.Item2;
            Z += vector.Item3;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer AddVector(ITriplet<double> vector, double scalingFactor)
        {
            X += scalingFactor * vector.Item1;
            Y += scalingFactor * vector.Item2;
            Z += scalingFactor * vector.Item3;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SubtractTerm(int basisBlade, double scalar)
        {
            this[basisBlade] -= scalar;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SubtractTerm(LinUnitBasisVector3D axis)
        {
            if (axis == LinUnitBasisVector3D.PositiveX)
                X -= 1;
            
            else if (axis == LinUnitBasisVector3D.PositiveY)
                Y -= 1;
            
            else if (axis == LinUnitBasisVector3D.PositiveZ)
                Z -= 1;

            else if (axis == LinUnitBasisVector3D.NegativeX)
                X += 1;
            
            else if (axis == LinUnitBasisVector3D.NegativeY)
                Y += 1;
            
            else
                Z += 1;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SubtractTerm(LinUnitBasisVector3D axis, double scalar)
        {
            if (axis == LinUnitBasisVector3D.PositiveX)
                X -= scalar;
            
            else if (axis == LinUnitBasisVector3D.PositiveY)
                Y -= scalar;
            
            else if (axis == LinUnitBasisVector3D.PositiveZ)
                Z -= scalar;

            else if (axis == LinUnitBasisVector3D.NegativeX)
                X += scalar;
            
            else if (axis == LinUnitBasisVector3D.NegativeY)
                Y += scalar;
            
            else
                Z += scalar;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SubtractVector(ITriplet<double> vector)
        {
            X -= vector.Item1;
            Y -= vector.Item2;
            Z -= vector.Item3;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SubtractVector(ITriplet<double> vector, double scalingFactor)
        {
            X -= scalingFactor * vector.Item1;
            Y -= scalingFactor * vector.Item2;
            Z -= scalingFactor * vector.Item3;

            return this;
        }
    
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer AddTerm(int basisBlade, double scalar1, double scalar2)
        {
            this[basisBlade] += scalar1 * scalar2;

            return this;
        }
        
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer SubtractTerm(int basisBlade, double scalar1, double scalar2)
        {
            this[basisBlade] -= scalar1 * scalar2;

            return this;
        }
        

        public Float64Vector3DComposer MapScalars(Func<double, double> mappingFunction)
        {
            X = mappingFunction(X);
            Y = mappingFunction(Y);
            Z = mappingFunction(Z);

            return this;
        }
    
        public Float64Vector3DComposer MapScalars(Func<int, double, double> mappingFunction)
        {
            X = mappingFunction(0, X);
            Y = mappingFunction(1, Y);
            Z = mappingFunction(2, Z);

            return this;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer Negative()
        {
            X = -X;
            Y = -Y;
            Z = -Z;

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer Times(double scalarFactor)
        {
            X *= scalarFactor;
            Y *= scalarFactor;
            Z *= scalarFactor;

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer Divide(double scalarFactor)
        {
            scalarFactor = 1d / scalarFactor;

            X *= scalarFactor;
            Y *= scalarFactor;
            Z *= scalarFactor;

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3DComposer DivideByENorm()
        {
            var scalarFactor = 1d / ENorm();

            X *= scalarFactor;
            Y *= scalarFactor;
            Z *= scalarFactor;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENormSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENorm()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetVector()
        {
            return Float64Vector3D.Create(X, Y, Z);
        }

    }
}