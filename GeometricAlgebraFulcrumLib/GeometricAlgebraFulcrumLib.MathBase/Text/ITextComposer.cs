using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

namespace GeometricAlgebraFulcrumLib.MathBase.Text;

public interface ITextComposer<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    string GetBasisVectorText(int index);

    string GetBasisBladeText(ulong id);

    string GetBasisBladeText(uint grade, ulong index);
        
    string GetBasisBladeText(IIndexSet id);
        
    string GetBasisBladeText(IEnumerable<int> indexList);

    string GetBasisBladeText(RGaBasisBlade basisBlade);

    string GetBasisBladeText(XGaBasisBlade basisBlade);

    string GetAngleText(Float64PlanarAngle angle);
        
    string GetAngleText(PlanarAngle<T> angle);

    string GetScalarText(Scalar<T> scalar);

    string GetScalarText(double scalar);

    string GetScalarText(T scalar);

    string GetTermText(ulong id, double scalar);

    string GetTermText(ulong id, T scalar);
        
    string GetTermText(uint grade, int index, double scalar);

    string GetTermText(uint grade, int index, T scalar);
        
    string GetTermText(IIndexSet id, double scalar);

    string GetTermText(IIndexSet id, T scalar);
        
    string GetTermText(RGaBasisBlade basisBlade, double scalar);

    string GetTermText(RGaBasisBlade basisBlade, T scalar);
        
    string GetTermText(XGaBasisBlade basisBlade, double scalar);

    string GetTermText(XGaBasisBlade basisBlade, T scalar);
        
    string GetArrayText(IReadOnlyList<double> array);

    string GetArrayText(IReadOnlyList<T> array);
        
    string GetArrayText(double[,] array);

    string GetArrayText(T[,] array);
        
    string GetVectorText(Float64Vector v);

    string GetVectorText(LinVector<T> v);
        
    string GetMultivectorText(RGaFloat64Multivector mv);

    string GetMultivectorText(RGaMultivector<T> mv);
        
    string GetMultivectorText(XGaFloat64Multivector mv);

    string GetMultivectorText(XGaMultivector<T> mv);
}