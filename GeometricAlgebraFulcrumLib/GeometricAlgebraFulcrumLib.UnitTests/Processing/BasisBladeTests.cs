using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing;

[TestFixture]
public sealed class BasisBladeTests
{
    public int VSpaceDimensions 
        => 6;

    public ulong GaSpaceDimensions 
        => 1UL << VSpaceDimensions;

    public XGaFloat64Processor BasisSet { get; }
        = XGaFloat64Processor.Create(2, 2);

        
    [Test]
    public void TestIdGradeIndexConversion()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var (grade, index) = id.ToUInt64IndexSet().BasisBladeIdToGradeIndex();
            var kvSpaceDimensions = VSpaceDimensions.GetBinomialCoefficient((int) grade);
            var id2 = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);
            var kvSpaceDimension2 = VSpaceDimensions.KVectorSpaceDimensions((int) grade);

            Debug.Assert(kvSpaceDimensions == kvSpaceDimension2);
            Assert.That(kvSpaceDimensions == kvSpaceDimension2);

            Debug.Assert(grade == BitOperations.PopCount(id));
            Assert.That(grade == BitOperations.PopCount(id));

            Debug.Assert(grade <= VSpaceDimensions);
            Assert.That(grade <= VSpaceDimensions);

            Debug.Assert(index < kvSpaceDimensions);
            Assert.That(index < kvSpaceDimensions);

            Debug.Assert(id == id2.ToUInt64());
            Assert.That(id == id2.ToUInt64());
        }

        for (var grade = 0U; grade <= VSpaceDimensions; grade++)
        {
            var kvSpaceDimensions = VSpaceDimensions.KVectorSpaceDimensions((int) grade);

            for (var index = 0UL; index < kvSpaceDimensions; index++)
            {
                var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);
                var (grade2, index2) = id.BasisBladeIdToGradeIndex();
                    
                Debug.Assert(grade == grade2);
                Assert.That(grade == grade2);
                    
                Debug.Assert(index == index2);
                Assert.That(index == index2);
                    
                Debug.Assert(id.ToUInt64() < GaSpaceDimensions);
                Assert.That(id.ToUInt64() < GaSpaceDimensions);
            }
        }
    }

    [Test]
    public void TestGradeInvolution()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var grade = id.ToUInt64IndexSet().BasisBladeIdToGrade();
            var sign = IntegerSign.Negative.Power((int) grade);
                

            var equalFlag =
                sign == 
                grade.GradeInvolutionSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().GradeInvolutionSignOfBasisBladeId() == 
                grade.GradeInvolutionSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().GradeInvolutionIsNegativeOfBasisBladeId() == 
                grade.GradeInvolutionIsNegativeOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().GradeInvolutionIsPositiveOfBasisBladeId() == 
                grade.GradeInvolutionIsPositiveOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().GradeInvolutionIsNegativeOfBasisBladeId() == 
                !id.ToUInt64IndexSet().GradeInvolutionIsPositiveOfBasisBladeId();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().GradeInvolutionIsNegativeOfBasisBladeId() == 
                (id.ToUInt64IndexSet().GradeInvolutionSignOfBasisBladeId().IsNegative);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().GradeInvolutionIsPositiveOfBasisBladeId() == 
                (id.ToUInt64IndexSet().GradeInvolutionSignOfBasisBladeId().IsPositive);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
        }
    }

    [Test]
    public void TestReverse()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var grade = id.ToUInt64IndexSet().BasisBladeIdToGrade();
            var sign = IntegerSign.Negative.Power((int) (grade * (grade - 1) / 2));
                

            var equalFlag =
                sign == 
                grade.ReverseSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().ReverseSignOfBasisBladeId() == 
                grade.ReverseSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().ReverseIsNegativeOfBasisBladeId() == 
                grade.ReverseIsNegativeOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().ReverseIsPositiveOfBasisBladeId() == 
                grade.ReverseIsPositiveOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().ReverseIsNegativeOfBasisBladeId() == 
                !id.ToUInt64IndexSet().ReverseIsPositiveOfBasisBladeId();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().ReverseIsNegativeOfBasisBladeId() == 
                (id.ToUInt64IndexSet().ReverseSignOfBasisBladeId().IsNegative);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().ReverseIsPositiveOfBasisBladeId() == 
                (id.ToUInt64IndexSet().ReverseSignOfBasisBladeId().IsPositive);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
        }
    }
        
    [Test]
    public void TestCliffordConjugate()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var grade = id.ToUInt64IndexSet().BasisBladeIdToGrade();
            var sign = IntegerSign.Negative.Power((int)(grade * (grade + 1) / 2));

            var equalFlag =
                sign == 
                grade.CliffordConjugateSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().CliffordConjugateSignOfBasisBladeId() == 
                grade.CliffordConjugateSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().CliffordConjugateIsNegativeOfBasisBladeId() == 
                grade.CliffordConjugateIsNegativeOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ToUInt64IndexSet().CliffordConjugateIsPositiveOfBasisBladeId() == 
                grade.CliffordConjugateIsPositiveOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().CliffordConjugateIsNegativeOfBasisBladeId() == 
                !id.ToUInt64IndexSet().CliffordConjugateIsPositiveOfBasisBladeId();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().CliffordConjugateIsNegativeOfBasisBladeId() == 
                (id.ToUInt64IndexSet().CliffordConjugateSignOfBasisBladeId().IsNegative);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ToUInt64IndexSet().CliffordConjugateIsPositiveOfBasisBladeId() == 
                (id.ToUInt64IndexSet().CliffordConjugateSignOfBasisBladeId().IsPositive);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
        }
    }
        
    [Test]
    public void TestGpSquared()
    {
        for (var id1 = 0UL; id1 < GaSpaceDimensions; id1++)
        {
            var is1 = (IndexSet)id1;

            var gpSign1 = BasisSet.GpSign(is1, is1);
            var gpSign2 = BasisSet.GpSquaredSign(is1);
                
            var egpSign1 = BasisSet.EGpSign(is1, is1);
            var egpSign2 = BasisSet.EGpSquaredSign(is1);

            Debug.Assert(gpSign1 == gpSign2);
            Assert.That(gpSign1 == gpSign2);
                
            Debug.Assert(egpSign1 == egpSign2);
            Assert.That(egpSign1 == egpSign2);
        }
    }

    [Test]
    public void TestGpReverse()
    {
        for (var id1 = 0UL; id1 < GaSpaceDimensions; id1++)
        {
            var is1 = (IndexSet)id1;

            var gpSign1 = BasisSet.GpSign(is1, is1) * is1.ReverseSignOfBasisBladeId();
            var gpSign2 = BasisSet.GpReverseSign(is1, is1);
            var gpSign3 = BasisSet.GpReverseSign(is1);
                
            var egpSign1 = BasisSet.EGpSign(is1, is1) * is1.ReverseSignOfBasisBladeId();
            var egpSign2 = BasisSet.EGpReverseSign(is1, is1);
            var egpSign3 = BasisSet.EGpReverseSign(is1);

            Debug.Assert(gpSign1 == gpSign2);
            Assert.That(gpSign1 == gpSign2);
                
            Debug.Assert(gpSign2 == gpSign3);
            Assert.That(gpSign2 == gpSign3);
                
            Debug.Assert(gpSign3 == gpSign1);
            Assert.That(gpSign3 == gpSign1);
                
            Debug.Assert(egpSign1 == egpSign2);
            Assert.That(egpSign1 == egpSign2);
                
            Debug.Assert(egpSign2 == egpSign3);
            Assert.That(egpSign2 == egpSign3);
                
            Debug.Assert(egpSign3 == egpSign1);
            Assert.That(egpSign3 == egpSign1);

            for (var id2 = 0UL; id2 < GaSpaceDimensions; id2++)
            {
                var is2 = (IndexSet)id2;

                gpSign1 = BasisSet.GpSign(is1, is2) * is2.ReverseSignOfBasisBladeId();
                gpSign2 = BasisSet.GpReverseSign(is1, is2);
                    
                egpSign1 = BasisSet.EGpSign(is1, is2) * is2.ReverseSignOfBasisBladeId();
                egpSign2 = BasisSet.EGpReverseSign(is1, is2);

                Debug.Assert(gpSign1 == gpSign2);
                Assert.That(gpSign1 == gpSign2);
                    
                Debug.Assert(egpSign1 == egpSign2);
                Assert.That(egpSign1 == egpSign2);
            }
        }
    }
}