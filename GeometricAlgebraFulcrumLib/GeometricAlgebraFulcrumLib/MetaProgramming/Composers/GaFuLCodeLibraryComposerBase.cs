﻿using CodeComposerLib;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Languages;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using TextComposerLib.Loggers.Progress;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers;

public abstract class GaFuLCodeLibraryComposerBase : 
    CclCodeLibraryComposerBase, IGaFuLCodeComposer
{
    public override string ProgressSourceId 
        => Name;

    public override ProgressComposer Progress 
        => null;

    public override CclLanguageServerBase Language 
        => GeoLanguage;

    /// <summary>
    /// The target language of this generator
    /// </summary>
    public GaFuLLanguageServerBase GeoLanguage { get; }

    public MetaContextOptions DefaultContextOptions { get; }
        = new MetaContextOptions();

    public GaFuLMetaContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }
        = new GaFuLMetaContextCodeComposerOptions();


    /// <summary>
    /// All derived classes must take a single AstRoot parameter for uniform operation purposes
    /// </summary>
    /// <param name="targetLanguage"></param>
    protected GaFuLCodeLibraryComposerBase(GaFuLLanguageServerBase targetLanguage)
    {
        GeoLanguage = targetLanguage;
    }


    /// <summary>
    /// Create an un-initialized copy of this library generator
    /// </summary>
    /// <returns></returns>
    public abstract GaFuLCodeLibraryComposerBase CreateEmptyComposer();

    /// <summary>
    /// Create a macro code generator based on this library
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual GaFuLMetaContextCodeComposer CreateMetaContextCodeComposer(MetaContext context)
    {
        return new GaFuLMetaContextCodeComposer(GeoLanguage, context);
    }

    /// <summary>
    /// Generates a single line comment on a separate line in the active file
    /// </summary>
    /// <param name="commentText"></param>
    public void GenerateComment(string commentText)
    {
        ActiveFileTextComposer.AppendLineAtNewLine(
            GeoLanguage.CodeGenerator.GenerateCode(
                SyntaxFactory.Comment(commentText)
            )
        );
    }

    /// <summary>
    /// Return a unique name for the given AST object
    /// </summary>
    /// <param name="astObject"></param>
    /// <returns></returns>
    protected string GetUniqueName(IIronyAstObjectNamed astObject)
    {
        return astObject.ObjectName + astObject.ObjectId.ToString("X4");
    }
}