//using System.Diagnostics;
//using System.Diagnostics.CodeAnalysis;
//using DataStructuresLib.Random;
//using NumericalGeometryLib.GeometricAlgebra.Maps;
//using NumericalGeometryLib.GeometricAlgebra.Multivectors;
//using NumericalGeometryLib.GeometricAlgebra.Restricted.Basis;

//namespace NumericalGeometryLib.Random
//{
//    public class RandomGaMultivectorComposer
//    {
//        public RGaMetric BasisSet { get; }

//        public System.Random RandomGenerator { get; }


//        public RandomGaMultivectorComposer([NotNull] RGaMetric basisSet)
//        {
//            BasisSet = basisSet;
//            RandomGenerator = new System.Random();
//        }

//        public RandomGaMultivectorComposer([NotNull] RGaMetric basisSet, int seed) 
//        {
//            BasisSet = basisSet;
//            RandomGenerator = new System.Random(seed);
//        }

//        public RandomGaMultivectorComposer([NotNull] RGaMetric basisSet, [NotNull] System.Random randomGenerator)
//        {
//            BasisSet = basisSet;
//            RandomGenerator = randomGenerator;
//        }

        
//        public XGaMultivector GetScalar()
//        {
//            var mv = BasisSet.CreateZero();

//            mv[0] = RandomGenerator.GetNumber();

//            return mv;
//        }

//        public XGaMultivector GetVector()
//        {
//            var mv = BasisSet.CreateZero();

//            for (var index = 0UL; index < BasisSet.VSpaceDimensions; index++)
//            {
//                var id = 1UL << (int) index;

//                mv[id] = RandomGenerator.GetNumber();
//            }

//            return mv;
//        }
        
//        public XGaMultivector GetBivector()
//        {
//            var mv = BasisSet.CreateZero();

//            for (var index1 = 0UL; index1 < BasisSet.VSpaceDimensions - 1; index1++)
//            for (var index2 = index1 + 1; index2 < BasisSet.VSpaceDimensions; index2++)
//            {
//                var id = (1UL << (int) index1) | (1UL << (int) index2);

//                mv[id] = RandomGenerator.GetNumber();
//            }

//            return mv;
//        }
        
//        public XGaMultivector GetKVector(uint grade)
//        {
//            var mv = BasisSet.CreateZero();

//            var kvSpaceDimensions = 
//                BasisSet.KvSpaceDimensions(grade);

//            for (var index = 0UL; index < kvSpaceDimensions; index++)
//                mv[grade, index] = RandomGenerator.GetNumber();

//            return mv;
//        }

//        public XGaMultivector GetMultivector()
//        {
//            var mv = BasisSet.CreateZero();

//            for (var id = 0UL; id < BasisSet.GaSpaceDimensions; id++)
//                mv[id] = RandomGenerator.GetNumber();

//            return mv;
//        }


//        public XGaMultivector GetSparseVector()
//        {
//            var termsCount = 
//                RandomGenerator.GetInteger(1, (int) BasisSet.VSpaceDimensions);

//            return GetSparseVector(termsCount);
//        }

//        public XGaMultivector GetSparseVector(int termsCount)
//        {
//            var indexList =
//                RandomGenerator.GetUniqueIndices((int) BasisSet.VSpaceDimensions, termsCount);

//            var mv = BasisSet.CreateZero();

//            foreach (var index in indexList)
//                mv[1UL << index] = RandomGenerator.GetNumber();

//            return mv;
//        }
        
//        public XGaMultivector GetSparseBivector()
//        {
//            var termsCount = 
//                RandomGenerator.GetInteger(1, (int) BasisSet.KvSpaceDimensions(2));

//            return GetSparseKVector(2, termsCount);
//        }

//        public XGaMultivector GetSparseBivector(int termsCount)
//        {
//            return GetSparseKVector(2, termsCount);
//        }
        
//        public XGaMultivector GetSparseKVector(uint grade)
//        {
//            var termsCount = 
//                RandomGenerator.GetInteger(1, (int) BasisSet.KvSpaceDimensions(grade));

//            return GetSparseKVector(grade, termsCount);
//        }

//        public XGaMultivector GetSparseKVector(uint grade, int termsCount)
//        {
//            var kvSpaceDimensions = 
//                BasisSet.KvSpaceDimensions(grade);

//            var indexList =
//                RandomGenerator.GetUniqueIndices((int) kvSpaceDimensions, termsCount);

//            var mv = BasisSet.CreateZero();

//            foreach (var index in indexList)
//                mv[grade, (ulong) index] = RandomGenerator.GetNumber();

//            return mv;
//        }
        
//        public XGaMultivector GetSparseMultivector()
//        {
//            var termsCount = 
//                RandomGenerator.GetInteger(1, (int) BasisSet.GaSpaceDimensions);

//            return GetSparseMultivector(termsCount);
//        }

//        public XGaMultivector GetSparseMultivector(int termsCount)
//        {
//            var idList =
//                RandomGenerator.GetUniqueIndices((int) BasisSet.GaSpaceDimensions, termsCount);

//            var mv = BasisSet.CreateZero();

//            foreach (var id in idList)
//                mv[(ulong) id] = RandomGenerator.GetNumber();

//            return mv;
//        }


//        public XGaMultivector GetBlade(uint grade)
//        {
//            if (grade < 1 || grade > BasisSet.VSpaceDimensions - 1)
//                return GetKVector(grade);

//            grade--;
//            var blade = GetVector();

//            while (grade > 0)
//            {
//                blade = blade.Op(GetVector());

//                grade--;
//            }

//            Debug.Assert(!blade.IsZero());

//            return blade;
//        }
        
//        public XGaMultivector GetVersor(uint grade)
//        {
//            if (grade < 1 || grade > BasisSet.VSpaceDimensions - 1)
//                return GetKVector(grade);

//            grade--;
//            var blade = GetVector();

//            while (grade > 0)
//            {
//                blade = blade.Gp(GetVector());

//                grade--;
//            }

//            Debug.Assert(!blade.IsZero());

//            return blade;
//        }
        
//        public XGaMultivector GetEVersor(uint grade)
//        {
//            if (grade < 1 || grade > BasisSet.VSpaceDimensions - 1)
//                return GetKVector(grade);

//            grade--;
//            var blade = GetVector();

//            while (grade > 0)
//            {
//                blade = blade.EGp(GetVector());

//                grade--;
//            }

//            Debug.Assert(!blade.IsZero());

//            return blade;
//        }
        
//        public GaScaledPureRotor GetEuclideanScaledPureRotor()
//        {
//            var v1 = GetVector();
//            var v2 = GetVector();

//            return v1.CreateEuclideanScaledPureRotor(v2);
//        }

//        public GaScaledPureRotor GetEuclideanPureRotor()
//        {
//            var v1 = GetVector().DivideByENorm();
//            var v2 = GetVector().DivideByENorm();

//            return v1.CreateEuclideanPureRotor(v2);
//        }
//    }
//}