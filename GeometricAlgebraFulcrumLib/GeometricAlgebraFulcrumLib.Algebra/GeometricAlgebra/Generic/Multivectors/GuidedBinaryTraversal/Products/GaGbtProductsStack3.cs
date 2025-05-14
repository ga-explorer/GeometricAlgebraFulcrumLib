using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Products;



public sealed class XGaGbtProductsStack3<T> 
    : XGaGbtStack3
{
    public static XGaGbtProductsStack3<T> Create(XGaMultivector<T> mv1, XGaMultivector<T> mv2, XGaMultivector<T> mv3)
    {
        var treeDepth = 
            Math.Max(
                1, 
                Math.Max(
                    mv1.VSpaceDimensions,
                    Math.Max(
                        mv2.VSpaceDimensions, 
                        mv3.VSpaceDimensions
                    )
                )
            );

        var capacity = (treeDepth + 1) * (treeDepth + 1) * (treeDepth + 1);
            
        var stack1 = mv1.CreateGbtStack(treeDepth, capacity);
        var stack2 = mv2.CreateGbtStack(treeDepth, capacity);
        var stack3 = mv3.CreateGbtStack(treeDepth, capacity);

        return new XGaGbtProductsStack3<T>(stack1, stack2, stack3);
    }


    private IXGaGbtMultivectorStorageStack1<T> MultivectorStack1 { get; }

    private IXGaGbtMultivectorStorageStack1<T> MultivectorStack2 { get; }

    private IXGaGbtMultivectorStorageStack1<T> MultivectorStack3 { get; }


    public IScalarProcessor<T> ScalarProcessor 
        => MultivectorStack1.ScalarProcessor;

    public XGaMultivector<T> Storage1 
        => MultivectorStack1.Multivector;

    public XGaMultivector<T> Storage2 
        => MultivectorStack2.Multivector;

    public XGaMultivector<T> Storage3 
        => MultivectorStack3.Multivector;

    public T TosValue1 
        => MultivectorStack1.TosScalar;

    public T TosValue2 
        => MultivectorStack2.TosScalar;

    public T TosValue3 
        => MultivectorStack3.TosScalar;

    public bool TosIsNonZeroOp
        => TosId1.OpIsNonZero(TosId2);

    public bool TosIsNonZeroESp
        => TosId1.ESpIsNonZero(TosId2);

    public bool TosIsNonZeroELcp
        => TosId1.ELcpIsNonZero(TosId2);

    public bool TosIsNonZeroERcp
        => TosId1.ERcpIsNonZero(TosId2);

    public bool TosIsNonZeroEFdp
        => TosId1.EFdpIsNonZero(TosId2);

    public bool TosIsNonZeroEHip
        => TosId1.EHipIsNonZero(TosId2);

    public bool TosIsNonZeroEAcp
        => TosId1.EAcpIsNonZero(TosId2);

    public bool TosIsNonZeroECp
        => TosId1.ECpIsNonZero(TosId2);

    public IndexSet TosChildIdXor000
        => Stack1.TosChildId0 ^ Stack2.TosChildId0 ^ Stack3.TosChildId0;

    public IndexSet TosChildIdXor100
        => Stack1.TosChildId1 ^ Stack2.TosChildId0 ^ Stack3.TosChildId0;

    public IndexSet TosChildIdXor010
        => Stack1.TosChildId0 ^ Stack2.TosChildId1 ^ Stack3.TosChildId0;

    public IndexSet TosChildIdXor110
        => Stack1.TosChildId1 ^ Stack2.TosChildId1 ^ Stack3.TosChildId0;

    public IndexSet TosChildIdXor001
        => Stack1.TosChildId0 ^ Stack2.TosChildId0 ^ Stack3.TosChildId1;

    public IndexSet TosChildIdXor101
        => Stack1.TosChildId1 ^ Stack2.TosChildId0 ^ Stack3.TosChildId1;

    public IndexSet TosChildIdXor011
        => Stack1.TosChildId0 ^ Stack2.TosChildId1 ^ Stack3.TosChildId1;

    public IndexSet TosChildIdXor111
        => Stack1.TosChildId1 ^ Stack2.TosChildId1 ^ Stack3.TosChildId1;

    public int TosChildIdXorGrade000
        => TosChildIdXor000.Grade();

    public int TosChildIdXorGrade100
        => TosChildIdXor100.Grade();

    public int TosChildIdXorGrade010
        => TosChildIdXor010.Grade();

    public int TosChildIdXorGrade110
        => TosChildIdXor110.Grade();

    public int TosChildIdXorGrade001
        => TosChildIdXor001.Grade();

    public int TosChildIdXorGrade101
        => TosChildIdXor101.Grade();

    public int TosChildIdXorGrade011
        => TosChildIdXor011.Grade();

    public int TosChildIdXorGrade111
        => TosChildIdXor111.Grade();


    private XGaGbtProductsStack3(IXGaGbtMultivectorStorageStack1<T> stack1, IXGaGbtMultivectorStorageStack1<T> stack2, IXGaGbtMultivectorStorageStack1<T> stack3) 
        : base(stack1, stack2, stack3)
    {
        MultivectorStack1 = stack1;
        MultivectorStack2 = stack2;
        MultivectorStack3 = stack3;
    }

        
    public bool TosChildMayContainGrade1(int childGrade)
    {
        return
            TosTreeDepth > 1 && childGrade <= 1 ||
            TosTreeDepth == 1 && childGrade == 1;
    }

    public bool TosChildMayContainGrade(int childGrade, int grade)
    {
        return
            TosTreeDepth > 1 && childGrade <= grade ||
            TosTreeDepth == 1 && childGrade == grade;
    }


    public KeyValuePair<IndexSet, T> TosGetTermsEGp()
    {
        var id1 = TosId1;
        var id2 = TosId2;
        var id3 = TosId3;

        var value = id1.EGpIsNegative(id2)
            ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2, TosValue3)
            : ScalarProcessor.Times(TosValue1, TosValue2, TosValue3);

        //Console.Out.WriteLine($"id: ({id1}, {id2}, {id3}), value: {value}");

        return new KeyValuePair<IndexSet, T>(
            id1 ^ id2 ^ id3,
            value.ScalarValue
        );
    }

    //public GeoTerm<T> TosGetTermsGp(IGeoNumMetricOrthogonal metric)
    //{
    //    var id1 = (int)TosId1;
    //    var id2 = (int)TosId2;

    //    var term = metric.Gp(id1, id2);
    //    term.ScalarValue *= TosValue1 * TosValue2;

    //    return term;
    //}

    public KeyValuePair<IndexSet, T> TosGetTermsGp(T basisBladeSignature)
    {
        var id1 = TosId1;
        var id2 = TosId2;
        var id3 = TosId3;

        var value = id1.EGpIsNegative(id2)
            ? ScalarProcessor.NegativeTimes(basisBladeSignature, TosValue1, TosValue2, TosValue3)
            : ScalarProcessor.Times(basisBladeSignature, TosValue1, TosValue2, TosValue3);

        return new KeyValuePair<IndexSet, T>(
            id1 ^ id2 ^ id3,
            value.ScalarValue
        );
    }


    public IEnumerable<KeyValuePair<IndexSet, T>> TraverseForEGpGpTerms()
    {
        PushRootData();

        while (!IsEmpty)
        {
            PopNodeData();

            if (TosIsLeaf)
            {
                yield return TosGetTermsEGp();

                continue;
            }

            var hasChild10 = Stack1.TosHasChild(0);
            var hasChild11 = Stack1.TosHasChild(1);

            var hasChild20 = Stack2.TosHasChild(0);
            var hasChild21 = Stack2.TosHasChild(1);

            var hasChild30 = Stack3.TosHasChild(0);
            var hasChild31 = Stack3.TosHasChild(1);

            if (hasChild31)
            {
                if (hasChild21)
                {
                    if (hasChild11) PushDataOfChild(7); //111
                    if (hasChild10) PushDataOfChild(6); //110
                }

                if (hasChild20)
                {
                    if (hasChild11) PushDataOfChild(5); //101
                    if (hasChild10) PushDataOfChild(4); //100
                }
            }

            if (hasChild30)
            {
                if (hasChild21)
                {
                    if (hasChild11) PushDataOfChild(3); //011
                    if (hasChild10) PushDataOfChild(2); //010
                }

                if (hasChild20)
                {
                    if (hasChild11) PushDataOfChild(1); //001
                    if (hasChild10) PushDataOfChild(0); //000
                }
            }
        }
    }
        
    /// <summary>
    /// (X op Y) lcp Z
    /// </summary>
    /// <returns></returns>
    public IEnumerable<KeyValuePair<IndexSet, T>> TraverseForEOpLcpLaTerms()
    {
        PushRootData();

        while (!IsEmpty)
        {
            PopNodeData();

            if (TosIsLeaf)
            {
                yield return TosGetTermsEGp();

                continue;
            }

            var hasChild10 = Stack1.TosHasChild(0);
            var hasChild11 = Stack1.TosHasChild(1);

            var hasChild20 = Stack2.TosHasChild(0);
            var hasChild21 = Stack2.TosHasChild(1);

            var hasChild30 = Stack3.TosHasChild(0);
            var hasChild31 = Stack3.TosHasChild(1);

            if (hasChild31)
            {
                if (hasChild21)
                {
                    //if (hasChild11) PushDataOfChild(7); //111
                    if (hasChild10) PushDataOfChild(6); //110
                }

                if (hasChild20)
                {
                    if (hasChild11) PushDataOfChild(5); //101
                    if (hasChild10) PushDataOfChild(4); //100
                }
            }

            if (hasChild30)
            {
                //if (hasChild21)
                //{
                //    if (hasChild11) PushDataOfChild(3); //011
                //    if (hasChild10) PushDataOfChild(2); //010
                //}

                if (hasChild20)
                {
                    //if (hasChild11) PushDataOfChild(1); //001
                    if (hasChild10) PushDataOfChild(0); //000
                }
            }
        }
    }
}