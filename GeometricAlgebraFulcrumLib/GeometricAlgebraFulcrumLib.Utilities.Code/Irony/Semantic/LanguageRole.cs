namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic;

/// <summary>
/// This class represents a role that may be given to a language symbol
/// For example a symbol can be a 'Function Definition', 'Local Variable', 'Class Definition', etc.
/// </summary>
public sealed class LanguageRole
{
    /// <summary>
    /// The name of this role
    /// </summary>
    public string RoleName { get; private set; }

    /// <summary>
    /// A description for this role
    /// </summary>
    public string RoleDescription { get; private set; }

    /// <summary>
    /// Enable reference by full qualification for any symbol having this role. For exmple, local variables can only be
    /// accessed within their blocks not from outside and only thier names are used for access. Where
    /// classes can be accessed from scopes other than the scope of definition and a full qualification 
    /// is required for access.
    /// </summary>
    public bool EnableAccessByQualification { get; private set; }


    public LanguageRole(string roleName, bool enableQual = true)
    {
        RoleName = roleName;
        RoleDescription = roleName;
        EnableAccessByQualification = enableQual;
    }

    public LanguageRole(string roleName, string roleDescription, bool enableQual = true)
    {
        RoleName = roleName;
        RoleDescription = roleDescription;
        EnableAccessByQualification = enableQual;
    }
}