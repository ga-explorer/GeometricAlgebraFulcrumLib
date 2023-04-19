using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean3D
{
    public sealed record Ega3Multivector : 
        IGeometricElement
    {
        public static Ega3Multivector Zero { get; }
            = new Ega3Multivector(EGa3ScalarTerm.Zero);

        public static Ega3Multivector E0 { get; }
            = new Ega3Multivector(EGa3ScalarTerm.One);

        public static Ega3Multivector E0Negative { get; }
            = new Ega3Multivector(EGa3ScalarTerm.NegativeOne);
        
        public static Ega3Multivector E1 { get; }
            = new Ega3Multivector(EGa3Vector.UnitXAxis);
        
        public static Ega3Multivector E1Negative { get; }
            = new Ega3Multivector(EGa3Vector.UnitXAxisNegative);
        
        public static Ega3Multivector E2 { get; }
            = new Ega3Multivector(EGa3Vector.UnitYAxis);
        
        public static Ega3Multivector E2Negative { get; }
            = new Ega3Multivector(EGa3Vector.UnitYAxisNegative);
        
        public static Ega3Multivector E3 { get; }
            = new Ega3Multivector(EGa3Bivector.XyBivector);
        
        public static Ega3Multivector E3Negative { get; }
            = new Ega3Multivector(EGa3Bivector.YxBivector);

        public static Ega3Multivector E4 { get; }
            = new Ega3Multivector(EGa3Vector.UnitZAxis);
        
        public static Ega3Multivector E4Negative { get; }
            = new Ega3Multivector(EGa3Vector.UnitZAxisNegative);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ega3Multivector(double value)
        {
            return new Ega3Multivector(
                new EGa3ScalarTerm(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ega3Multivector(EGa3ScalarTerm mv)
        {
            return new Ega3Multivector(mv);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ega3Multivector(EGa3Vector mv)
        {
            return new Ega3Multivector(mv);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ega3Multivector(EGa3Bivector mv)
        {
            return new Ega3Multivector(mv);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ega3Multivector(EGa3Trivector mv)
        {
            return new Ega3Multivector(mv);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(Ega3Multivector mv)
        {
            return mv;
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(Ega3Multivector mv)
        {
            return mv.IsZero
                ? mv
                : new Ega3Multivector(-mv.KVector0, -mv.KVector1, -mv.KVector2, -mv.KVector3);
        } 


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(Ega3Multivector mv1, EGa3ScalarTerm mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0 + mv2,
                    mv1.KVector1,
                    mv1.KVector2,
                    mv1.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(Ega3Multivector mv1, EGa3Vector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0,
                    mv1.KVector1 + mv2,
                    mv1.KVector2,
                    mv1.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(Ega3Multivector mv1, EGa3Bivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0,
                    mv1.KVector1,
                    mv1.KVector2 + mv2,
                    mv1.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(Ega3Multivector mv1, EGa3Trivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0,
                    mv1.KVector1,
                    mv1.KVector2,
                    mv1.KVector3 + mv2
                );
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(EGa3ScalarTerm mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1 + mv2.KVector0,
                    mv2.KVector1,
                    mv2.KVector2,
                    mv2.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(EGa3Vector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv2.KVector0,
                    mv1 + mv2.KVector1,
                    mv2.KVector2,
                    mv2.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(EGa3Bivector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv2.KVector0,
                    mv2.KVector1,
                    mv1 + mv2.KVector2,
                    mv2.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(EGa3Trivector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv2.KVector0,
                    mv2.KVector1,
                    mv2.KVector2,
                    mv1 + mv2.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(Ega3Multivector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero)
                return mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0 + mv2.KVector0,
                    mv1.KVector1 + mv2.KVector1,
                    mv1.KVector2 + mv2.KVector2,
                    mv1.KVector3 + mv2.KVector3
                );
        } 

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(Ega3Multivector mv1, EGa3ScalarTerm mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0 - mv2,
                    mv1.KVector1,
                    mv1.KVector2,
                    mv1.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(Ega3Multivector mv1, EGa3Vector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0,
                    mv1.KVector1 - mv2,
                    mv1.KVector2,
                    mv1.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(Ega3Multivector mv1, EGa3Bivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0,
                    mv1.KVector1,
                    mv1.KVector2 - mv2,
                    mv1.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(Ega3Multivector mv1, EGa3Trivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0,
                    mv1.KVector1,
                    mv1.KVector2,
                    mv1.KVector3 - mv2
                );
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(EGa3ScalarTerm mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1 - mv2.KVector0,
                    mv2.KVector1,
                    mv2.KVector2,
                    mv2.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(EGa3Vector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv2.KVector0,
                    mv1 - mv2.KVector1,
                    mv2.KVector2,
                    mv2.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(EGa3Bivector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv2.KVector0,
                    mv2.KVector1,
                    mv1 - mv2.KVector2,
                    mv2.KVector3
                );
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(EGa3Trivector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv2.KVector0,
                    mv2.KVector1,
                    mv2.KVector2,
                    mv1 - mv2.KVector3
                );
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(Ega3Multivector mv1, Ega3Multivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero 
                ? mv1 
                : new Ega3Multivector(
                    mv1.KVector0 - mv2.KVector0,
                    mv1.KVector1 - mv2.KVector1,
                    mv1.KVector2 - mv2.KVector2,
                    mv1.KVector3 - mv2.KVector3
                );
        } 


        public EGa3ScalarTerm KVector0 { get; }

        public EGa3Vector KVector1 { get; }

        public EGa3Bivector KVector2 { get; }

        public EGa3Trivector KVector3 { get; }
        
        public bool IsZero { get; }

        public double Scalar0 => KVector0.Scalar0;

        public double Scalar1 => KVector1.Scalar1;

        public double Scalar2 => KVector1.Scalar2;

        public double Scalar12 => KVector2.Scalar12;

        public double Scalar3 => KVector1.Scalar3;

        public double Scalar13 => KVector2.Scalar13;

        public double Scalar23 => KVector2.Scalar23;

        public double Scalar123 => KVector3.Scalar123;

        public double this[int grade, int index]
        {
            get
            {
                if (grade is < 0 or > 3)
                    throw new ArgumentOutOfRangeException(nameof(grade));

                if (grade == 0)
                    return index == 0 
                        ? KVector0.Scalar0 
                        : throw new IndexOutOfRangeException(nameof(index));

                if (grade == 1)
                    return index switch
                    {
                        0 => KVector1.Scalar1,
                        1 => KVector1.Scalar2,
                        2 => KVector1.Scalar3,
                        _ => throw new IndexOutOfRangeException(nameof(index))
                    };

                if (grade == 2)
                    return index switch
                    {
                        0 => KVector2.Scalar12,
                        1 => KVector2.Scalar13,
                        2 => KVector2.Scalar23,
                        _ => throw new IndexOutOfRangeException(nameof(index))
                    };

                return index == 0 
                    ? KVector3.Scalar123 
                    : throw new IndexOutOfRangeException(nameof(index));
            }
        }

        public double this[int id]
        {
            get
            {
                if (id is < 0 or > 7)
                    throw new IndexOutOfRangeException();

                if (id <= 3)
                {
                    if (id <= 1)
                        return id == 0 
                            ? KVector0.Scalar0 
                            : KVector1.Scalar1;
                    
                    return id == 2 
                        ? KVector1.Scalar2 
                        : KVector2.Scalar12;
                }
                
                if (id <= 5)
                    return id == 4 
                        ? KVector1.Scalar3 
                        : KVector2.Scalar13;
                    
                return id == 6 
                    ? KVector2.Scalar23 
                    : KVector3.Scalar123;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector(EGa3ScalarTerm kVector0)
        {
            KVector0 = kVector0 ?? EGa3ScalarTerm.Zero;
            KVector1 = EGa3Vector.Zero;
            KVector2 = EGa3Bivector.Zero;
            KVector3 = EGa3Trivector.Zero;

            IsZero = KVector0.IsZero;

            Debug.Assert(IsValid());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector(EGa3Vector kVector1)
        {
            KVector0 = EGa3ScalarTerm.Zero;
            KVector1 = kVector1 ?? EGa3Vector.Zero;
            KVector2 = EGa3Bivector.Zero;
            KVector3 = EGa3Trivector.Zero;

            IsZero = KVector1.IsZero;

            Debug.Assert(IsValid());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector(EGa3Bivector kVector2)
        {
            KVector0 = EGa3ScalarTerm.Zero;
            KVector1 = EGa3Vector.Zero;
            KVector2 = kVector2 ?? EGa3Bivector.Zero;
            KVector3 = EGa3Trivector.Zero;

            IsZero = KVector2.IsZero;

            Debug.Assert(IsValid());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector(EGa3Trivector kVector3)
        {
            KVector0 = EGa3ScalarTerm.Zero;
            KVector1 = EGa3Vector.Zero;
            KVector2 = EGa3Bivector.Zero;
            KVector3 = kVector3 ?? EGa3Trivector.Zero;

            IsZero = KVector3.IsZero;

            Debug.Assert(IsValid());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector(EGa3ScalarTerm kVector0, EGa3Bivector kVector2)
        {
            KVector0 = kVector0 ?? EGa3ScalarTerm.Zero;
            KVector1 = EGa3Vector.Zero;
            KVector2 = kVector2 ?? EGa3Bivector.Zero;
            KVector3 = EGa3Trivector.Zero;
            
            IsZero = KVector0.IsZero && 
                     KVector2.IsZero;

            Debug.Assert(IsValid());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector(EGa3Vector kVector1, EGa3Trivector kVector3)
        {
            KVector0 = EGa3ScalarTerm.Zero;
            KVector1 = kVector1 ?? EGa3Vector.Zero;
            KVector2 = EGa3Bivector.Zero;
            KVector3 = kVector3 ?? EGa3Trivector.Zero;

            IsZero = KVector1.IsZero && 
                     KVector3.IsZero;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector(EGa3ScalarTerm kVector0, EGa3Vector kVector1, EGa3Bivector kVector2, EGa3Trivector kVector3)
        {
            KVector0 = kVector0 ?? EGa3ScalarTerm.Zero;
            KVector1 = kVector1 ?? EGa3Vector.Zero;
            KVector2 = kVector2 ?? EGa3Bivector.Zero;
            KVector3 = kVector3 ?? EGa3Trivector.Zero;

            IsZero = KVector0.IsZero && 
                     KVector1.IsZero && 
                     KVector2.IsZero &&
                     KVector3.IsZero;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return KVector0.IsValid() && 
                   KVector3.IsValid() && 
                   KVector1.IsValid() && 
                   KVector2.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NormSquared()
        {
            return KVector0.NormSquared() + 
                   KVector1.NormSquared() + 
                   KVector2.NormSquared() + 
                   KVector3.NormSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Norm()
        {
            return Math.Sqrt(Math.Abs(NormSquared()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector GradeInvolution()
        {
            return new Ega3Multivector(
                KVector0,
                KVector1.GradeInvolution(),
                KVector2,
                KVector3.GradeInvolution()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector Reverse()
        {
            return new Ega3Multivector(
                KVector0,
                KVector1,
                KVector2.Reverse(),
                KVector3.Reverse()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector CliffordConjugate()
        {
            return new Ega3Multivector(
                KVector0,
                KVector1.CliffordConjugate(),
                KVector2.CliffordConjugate(),
                KVector3
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({KVector0}) + {KVector1} + {KVector2} + {KVector3}";
        }
    }
}