using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Random;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Maps;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;

namespace NumericalGeometryLib.Random
{
    public class RandomGaMultivectorComposer
    {
        public GaBasisSet BasisSet { get; }

        public System.Random RandomGenerator { get; }


        public RandomGaMultivectorComposer([NotNull] GaBasisSet basisSet)
        {
            BasisSet = basisSet;
            RandomGenerator = new System.Random();
        }

        public RandomGaMultivectorComposer([NotNull] GaBasisSet basisSet, int seed) 
        {
            BasisSet = basisSet;
            RandomGenerator = new System.Random(seed);
        }

        public RandomGaMultivectorComposer([NotNull] GaBasisSet basisSet, [NotNull] System.Random randomGenerator)
        {
            BasisSet = basisSet;
            RandomGenerator = randomGenerator;
        }

        
        public GaMultivector GetScalar()
        {
            var mv = BasisSet.CreateZero();

            mv[0] = RandomGenerator.GetNumber();

            return mv;
        }

        public GaMultivector GetVector()
        {
            var mv = BasisSet.CreateZero();

            for (var index = 0UL; index < BasisSet.VSpaceDimension; index++)
            {
                var id = 1UL << (int) index;

                mv[id] = RandomGenerator.GetNumber();
            }

            return mv;
        }
        
        public GaMultivector GetBivector()
        {
            var mv = BasisSet.CreateZero();

            for (var index1 = 0UL; index1 < BasisSet.VSpaceDimension - 1; index1++)
            for (var index2 = index1 + 1; index2 < BasisSet.VSpaceDimension; index2++)
            {
                var id = (1UL << (int) index1) | (1UL << (int) index2);

                mv[id] = RandomGenerator.GetNumber();
            }

            return mv;
        }
        
        public GaMultivector GetKVector(uint grade)
        {
            var mv = BasisSet.CreateZero();

            var kvSpaceDimension = 
                BasisSet.KvSpaceDimension(grade);

            for (var index = 0UL; index < kvSpaceDimension; index++)
                mv[grade, index] = RandomGenerator.GetNumber();

            return mv;
        }

        public GaMultivector GetMultivector()
        {
            var mv = BasisSet.CreateZero();

            for (var id = 0UL; id < BasisSet.GaSpaceDimension; id++)
                mv[id] = RandomGenerator.GetNumber();

            return mv;
        }


        public GaMultivector GetSparseVector()
        {
            var termsCount = 
                RandomGenerator.GetInteger(1, (int) BasisSet.VSpaceDimension);

            return GetSparseVector(termsCount);
        }

        public GaMultivector GetSparseVector(int termsCount)
        {
            var indexList =
                RandomGenerator.GetUniqueIndices((int) BasisSet.VSpaceDimension, termsCount);

            var mv = BasisSet.CreateZero();

            foreach (var index in indexList)
                mv[1UL << index] = RandomGenerator.GetNumber();

            return mv;
        }
        
        public GaMultivector GetSparseBivector()
        {
            var termsCount = 
                RandomGenerator.GetInteger(1, (int) BasisSet.KvSpaceDimension(2));

            return GetSparseKVector(2, termsCount);
        }

        public GaMultivector GetSparseBivector(int termsCount)
        {
            return GetSparseKVector(2, termsCount);
        }
        
        public GaMultivector GetSparseKVector(uint grade)
        {
            var termsCount = 
                RandomGenerator.GetInteger(1, (int) BasisSet.KvSpaceDimension(grade));

            return GetSparseKVector(grade, termsCount);
        }

        public GaMultivector GetSparseKVector(uint grade, int termsCount)
        {
            var kvSpaceDimension = 
                BasisSet.KvSpaceDimension(grade);

            var indexList =
                RandomGenerator.GetUniqueIndices((int) kvSpaceDimension, termsCount);

            var mv = BasisSet.CreateZero();

            foreach (var index in indexList)
                mv[grade, (ulong) index] = RandomGenerator.GetNumber();

            return mv;
        }
        
        public GaMultivector GetSparseMultivector()
        {
            var termsCount = 
                RandomGenerator.GetInteger(1, (int) BasisSet.GaSpaceDimension);

            return GetSparseMultivector(termsCount);
        }

        public GaMultivector GetSparseMultivector(int termsCount)
        {
            var idList =
                RandomGenerator.GetUniqueIndices((int) BasisSet.GaSpaceDimension, termsCount);

            var mv = BasisSet.CreateZero();

            foreach (var id in idList)
                mv[(ulong) id] = RandomGenerator.GetNumber();

            return mv;
        }


        public GaMultivector GetBlade(uint grade)
        {
            if (grade < 1 || grade > BasisSet.VSpaceDimension - 1)
                return GetKVector(grade);

            grade--;
            var blade = GetVector();

            while (grade > 0)
            {
                blade = blade.Op(GetVector());

                grade--;
            }

            Debug.Assert(!blade.IsZero());

            return blade;
        }
        
        public GaMultivector GetVersor(uint grade)
        {
            if (grade < 1 || grade > BasisSet.VSpaceDimension - 1)
                return GetKVector(grade);

            grade--;
            var blade = GetVector();

            while (grade > 0)
            {
                blade = blade.Gp(GetVector());

                grade--;
            }

            Debug.Assert(!blade.IsZero());

            return blade;
        }
        
        public GaMultivector GetEVersor(uint grade)
        {
            if (grade < 1 || grade > BasisSet.VSpaceDimension - 1)
                return GetKVector(grade);

            grade--;
            var blade = GetVector();

            while (grade > 0)
            {
                blade = blade.EGp(GetVector());

                grade--;
            }

            Debug.Assert(!blade.IsZero());

            return blade;
        }
        
        public GaScaledPureRotor GetEuclideanScaledPureRotor()
        {
            var v1 = GetVector();
            var v2 = GetVector();

            return v1.CreateEuclideanScaledPureRotor(v2);
        }

        public GaScaledPureRotor GetEuclideanPureRotor()
        {
            var v1 = GetVector().DivideByENorm();
            var v2 = GetVector().DivideByENorm();

            return v1.CreateEuclideanPureRotor(v2);
        }
    }
}