using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing;

[TestFixture]
public sealed class BasisBladeTests
{
    public int VSpaceDimensions 
        => 6;

    public ulong GaSpaceDimensions 
        => VSpaceDimensions.ToGaSpaceDimension();

    public RGaFloat64Processor BasisSet { get; }
        = RGaFloat64Processor.Create(2, 2);

        
    [Test]
    public void TestIdGradeIndexConversion()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var (grade, index) = id.BasisBladeIdToGradeIndex();
            var kvSpaceDimensions = VSpaceDimensions.GetBinomialCoefficient((int) grade);
            var id2 = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);
            var kvSpaceDimension2 = VSpaceDimensions.KVectorSpaceDimension((int) grade);

            Debug.Assert(kvSpaceDimensions == kvSpaceDimension2);
            Assert.That(kvSpaceDimensions == kvSpaceDimension2);

            Debug.Assert(grade == BitOperations.PopCount(id));
            Assert.That(grade == BitOperations.PopCount(id));

            Debug.Assert(grade <= VSpaceDimensions);
            Assert.That(grade <= VSpaceDimensions);

            Debug.Assert(index < kvSpaceDimensions);
            Assert.That(index < kvSpaceDimensions);

            Debug.Assert(id == id2);
            Assert.That(id == id2);
        }

        for (var grade = 0U; grade <= VSpaceDimensions; grade++)
        {
            var kvSpaceDimensions = VSpaceDimensions.KVectorSpaceDimension((int) grade);

            for (var index = 0UL; index < kvSpaceDimensions; index++)
            {
                var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);
                var (grade2, index2) = id.BasisBladeIdToGradeIndex();
                    
                Debug.Assert(grade == grade2);
                Assert.That(grade == grade2);
                    
                Debug.Assert(index == index2);
                Assert.That(index == index2);
                    
                Debug.Assert(id < GaSpaceDimensions);
                Assert.That(id < GaSpaceDimensions);
            }
        }
    }

    [Test]
    public void TestGradeInvolution()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var grade = id.BasisBladeIdToGrade();
            var sign = IntegerSign.Negative.Power((int) grade);
                

            var equalFlag =
                sign == 
                grade.GradeInvolutionSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.GradeInvolutionSignOfBasisBladeId() == 
                grade.GradeInvolutionSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.GradeInvolutionIsNegativeOfBasisBladeId() == 
                grade.GradeInvolutionIsNegativeOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.GradeInvolutionIsPositiveOfBasisBladeId() == 
                grade.GradeInvolutionIsPositiveOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.GradeInvolutionIsNegativeOfBasisBladeId() == 
                !id.GradeInvolutionIsPositiveOfBasisBladeId();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.GradeInvolutionIsNegativeOfBasisBladeId() == 
                (id.GradeInvolutionSignOfBasisBladeId().IsNegative);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.GradeInvolutionIsPositiveOfBasisBladeId() == 
                (id.GradeInvolutionSignOfBasisBladeId().IsPositive);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
        }
    }

    [Test]
    public void TestReverse()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var grade = id.BasisBladeIdToGrade();
            var sign = IntegerSign.Negative.Power((int) (grade * (grade - 1) / 2));
                

            var equalFlag =
                sign == 
                grade.ReverseSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ReverseSignOfBasisBladeId() == 
                grade.ReverseSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ReverseIsNegativeOfBasisBladeId() == 
                grade.ReverseIsNegativeOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.ReverseIsPositiveOfBasisBladeId() == 
                grade.ReverseIsPositiveOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ReverseIsNegativeOfBasisBladeId() == 
                !id.ReverseIsPositiveOfBasisBladeId();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ReverseIsNegativeOfBasisBladeId() == 
                (id.ReverseSignOfBasisBladeId().IsNegative);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.ReverseIsPositiveOfBasisBladeId() == 
                (id.ReverseSignOfBasisBladeId().IsPositive);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
        }
    }
        
    [Test]
    public void TestCliffordConjugate()
    {
        for (var id = 0UL; id < GaSpaceDimensions; id++)
        {
            var grade = id.BasisBladeIdToGrade();
            var sign = IntegerSign.Negative.Power((int)(grade * (grade + 1) / 2));

            var equalFlag =
                sign == 
                grade.CliffordConjugateSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.CliffordConjugateSignOfBasisBladeId() == 
                grade.CliffordConjugateSignOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.CliffordConjugateIsNegativeOfBasisBladeId() == 
                grade.CliffordConjugateIsNegativeOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);


            equalFlag =
                id.CliffordConjugateIsPositiveOfBasisBladeId() == 
                grade.CliffordConjugateIsPositiveOfGrade();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.CliffordConjugateIsNegativeOfBasisBladeId() == 
                !id.CliffordConjugateIsPositiveOfBasisBladeId();

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.CliffordConjugateIsNegativeOfBasisBladeId() == 
                (id.CliffordConjugateSignOfBasisBladeId().IsNegative);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
                

            equalFlag =
                id.CliffordConjugateIsPositiveOfBasisBladeId() == 
                (id.CliffordConjugateSignOfBasisBladeId().IsPositive);

            Debug.Assert(equalFlag);
            Assert.That(equalFlag);
        }
    }
        
    [Test]
    public void TestGpSquared()
    {
        for (var id1 = 0UL; id1 < GaSpaceDimensions; id1++)
        {
            var gpSign1 = BasisSet.GpSign(id1, id1);
            var gpSign2 = BasisSet.GpSquaredSign(id1);
                
            var egpSign1 = BasisSet.EGpSign(id1, id1);
            var egpSign2 = BasisSet.EGpSquaredSign(id1);

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
            var gpSign1 = BasisSet.GpSign(id1, id1) * id1.ReverseSignOfBasisBladeId();
            var gpSign2 = BasisSet.GpReverseSign(id1, id1);
            var gpSign3 = BasisSet.GpReverseSign(id1);
                
            var egpSign1 = BasisSet.EGpSign(id1, id1) * id1.ReverseSignOfBasisBladeId();
            var egpSign2 = BasisSet.EGpReverseSign(id1, id1);
            var egpSign3 = BasisSet.EGpReverseSign(id1);

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
                gpSign1 = BasisSet.GpSign(id1, id2) * id2.ReverseSignOfBasisBladeId();
                gpSign2 = BasisSet.GpReverseSign(id1, id2);
                    
                egpSign1 = BasisSet.EGpSign(id1, id2) * id2.ReverseSignOfBasisBladeId();
                egpSign2 = BasisSet.EGpReverseSign(id1, id2);

                Debug.Assert(gpSign1 == gpSign2);
                Assert.That(gpSign1 == gpSign2);
                    
                Debug.Assert(egpSign1 == egpSign2);
                Assert.That(egpSign1 == egpSign2);
            }
        }
    }
}