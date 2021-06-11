namespace CodeComposerLib.Irony.Semantic.Scope
{
    /// <summary>
    /// This class represents a scope that is a direct child for a parent scope
    /// </summary>
    public sealed class ScopeScopeChild : LanguageScope
    {
        private ScopeScopeChild(LanguageScope parentScope)
            : base(parentScope)
        {
        }

        private ScopeScopeChild(LanguageScope parentScope, string scopeName)
            : base(parentScope, scopeName)
        {
        }


        public static ScopeScopeChild Create(LanguageScope parentScope)
        {
            var scope = new ScopeScopeChild(parentScope);

            RegisterChildScope(scope.ParentScope, scope);

            return scope;
        }

        public static ScopeScopeChild Create(LanguageScope parentScope, string scopeName)
        {
            var scope = new ScopeScopeChild(parentScope, scopeName);

            RegisterChildScope(scope.ParentScope, scope);

            return scope;
        }
    }
}
