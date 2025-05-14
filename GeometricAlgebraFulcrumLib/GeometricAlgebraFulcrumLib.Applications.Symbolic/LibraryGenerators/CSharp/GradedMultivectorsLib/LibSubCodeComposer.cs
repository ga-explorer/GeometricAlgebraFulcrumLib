using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib;

public abstract class LibSubCodeComposer
{
    public LibCodeComposerSpecs Specs { get; }

    public TextFilesComposer CodeFilesComposer { get; }

    public string GaSpaceName
        => Specs.GaSpaceName;

    public LibTypeKVector ScalarType
        => Specs.ScalarType;

    public LibTypeKVector VectorType
        => Specs.VectorType;

    public LibTypeKVector BivectorType
        => Specs.BivectorType;

    public LibTypeMultivector MultivectorType
        => Specs.MultivectorType;

    public IReadOnlyList<LibTypeKVector> KVectorTypes
        => Specs.KVectorTypes;

    public IReadOnlyList<LibType> Types
        => Specs.Types;

    public int VSpaceDimensions
        => MultivectorType.VSpaceDimensions;
    
    public int GaSpaceDimensions
        => MultivectorType.GaSpaceDimensions;

    public XGaFloat64Processor Metric
        => MultivectorType.Metric;


    protected LibSubCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
    {
        Specs = specs;
        CodeFilesComposer = codeFilesComposer;
    }


    public abstract TextFilesComposer GenerateCode();
}