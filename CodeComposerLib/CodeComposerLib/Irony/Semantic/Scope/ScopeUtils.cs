using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Symbol;

namespace CodeComposerLib.Irony.Semantic.Scope
{
    /// <summary>
    /// This static class contains some extension methods for searching a list of scopes for symbols in different ways
    /// </summary>
    public static class ScopeUtils
    {
        /// <summary>
        /// Get all distinct scopes in the given list of scopes. Scopes are distinct by unique scope.ObjectID attribute
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <returns></returns>
        public static IEnumerable<LanguageScope> DistinctScopes(this IEnumerable<LanguageScope> scopeList)
        {
            var skipIdsList = new Dictionary<int, int>();

            foreach (var scope in scopeList.Where(scope => skipIdsList.ContainsKey(scope.ObjectId) == false))
            {
                skipIdsList.Add(scope.ObjectId, scope.ObjectId);

                yield return scope;
            }
        }


        /// <summary>
        /// Tests the existance of a symbol with a given name in any of the scopes of the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <returns></returns>
        public static bool SymbolExists(this IEnumerable<LanguageScope> scopeList, string symbolName)
        {
            return scopeList.Any(scope => scope.SymbolExists(symbolName));
        }

        /// <summary>
        /// Tests the existance of a symbol with a given name and role name in any of the scopes of the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="roleName">The role name of the symbol</param>
        /// <returns></returns>
        public static bool SymbolExists(this IEnumerable<LanguageScope> scopeList, string symbolName, string roleName)
        {
            return scopeList.Any(scope => scope.SymbolExists(symbolName, roleName));
        }

        /// <summary>
        /// Tests the existance of a symbol with a given name and having a role name from a given list of role names in any of the scopes of the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="roleNames">A list of possible role names of the symbol</param>
        /// <returns></returns>
        public static bool SymbolExists(this IEnumerable<LanguageScope> scopeList, string symbolName, IEnumerable<string> roleNames)
        {
            return scopeList.Any(scope => scope.SymbolExists(symbolName, roleNames));
        }


        /// <summary>
        /// Search for and return a symbol with a given name in any of the scopes of the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="symbol">A reference to the symbol if it's found in any of the scopes</param>
        /// <returns>True if the symbol is found in any of the scopes</returns>
        public static bool LookupSymbol(this IEnumerable<LanguageScope> scopeList, string symbolName, out LanguageSymbol symbol)
        {
            foreach (var scope in scopeList)
                if (scope.LookupSymbol(symbolName, out symbol))
                    return true;

            symbol = null;
            return false;
        }

        /// <summary>
        /// Search for and return a symbol with a given name in any of the scopes of the given list of scopes
        /// </summary>
        /// <typeparam name="T">The type of the symbol to be found</typeparam>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="symbol">A reference to the symbol if it's found in any of the scopes</param>
        /// <returns>True if the symbol is found in any of the scopes</returns>
        public static bool LookupSymbol<T>(this IEnumerable<LanguageScope> scopeList, string symbolName, out T symbol) where T : LanguageSymbol
        {
            foreach (var scope in scopeList)
                if (scope.LookupSymbol(symbolName, out symbol))
                    return true;

            symbol = null;
            return false;
        }

        /// <summary>
        /// Search for and return a symbol with a given name and role name in any of the scopes of the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="roleName">The role name of the symbol</param>
        /// <param name="symbol">A reference to the symbol if it's found in any of the scopes</param>
        /// <returns>True if the symbol is found in any of the scopes</returns>
        public static bool LookupSymbol(this IEnumerable<LanguageScope> scopeList, string symbolName, string roleName, out LanguageSymbol symbol)
        {
            foreach (var scope in scopeList)
                if (scope.LookupSymbol(symbolName, roleName, out symbol))
                    return true;

            symbol = null;
            return false;
        }

        /// <summary>
        /// Search for and return a symbol with a given name and role name in any of the scopes of the given list of scopes
        /// </summary>
        /// <typeparam name="T">The type of the symbol to be found</typeparam>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="roleName">The role name of the symbol</param>
        /// <param name="symbol">A reference to the symbol if it's found in any of the scopes</param>
        /// <returns>True if the symbol is found in any of the scopes</returns>
        public static bool LookupSymbol<T>(this IEnumerable<LanguageScope> scopeList, string symbolName, string roleName, out T symbol) where T : LanguageSymbol
        {
            foreach (var scope in scopeList)
                if (scope.LookupSymbol(symbolName, roleName, out symbol))
                    return true;

            symbol = null;
            return false;
        }

        /// <summary>
        /// Search for and return a symbol with a given name and having a role name from a given list of role names in any of the scopes of the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="roleNames">A list of possible role names of the symbol</param>
        /// <param name="symbol">A reference to the symbol if it's found in any of the scopes</param>
        /// <returns>True if the symbol is found in any of the scopes</returns>
        public static bool LookupSymbol(this IEnumerable<LanguageScope> scopeList, string symbolName, IEnumerable<string> roleNames, out LanguageSymbol symbol)
        {
            var roleNamesArray = roleNames.ToArray();

            foreach (var scope in scopeList)
                if (scope.LookupSymbol(symbolName, roleNamesArray, out symbol))
                    return true;

            symbol = null;
            return false;
        }


        /// <summary>
        /// Get a symbol with a given name in any of the scopes of the given list of scopes. If the symbol is not found an error is raised
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <returns>A reference to the symbol</returns>
        public static LanguageSymbol GetSymbol(this IEnumerable<LanguageScope> scopeList, string symbolName)
        {
            LanguageSymbol symbol = null;

            if (scopeList.Any(scope => scope.LookupSymbol(symbolName, out symbol)))
                return symbol;

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Get a symbol with a given name and role name in any of the scopes of the given list of scopes. If the symbol is not found an error is raised
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="roleName">The role name of the symbol</param>
        /// <returns>A reference to the symbol</returns>
        public static LanguageSymbol GetSymbol(this IEnumerable<LanguageScope> scopeList, string symbolName, string roleName)
        {
            LanguageSymbol symbol = null;

            if (scopeList.Any(scope => scope.LookupSymbol(symbolName, roleName, out symbol)))
                return symbol;

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Get a symbol with a given name and having a role name from a given list of role names in any of the scopes of the given list of scopes. If the symbol is not found an error is raised
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="symbolName">The name of the symbol</param>
        /// <param name="roleNames">A list of possible role names of the symbol</param>
        /// <returns>A reference to the symbol</returns>
        public static LanguageSymbol GetSymbol(this IEnumerable<LanguageScope> scopeList, string symbolName, IEnumerable<string> roleNames)
        {
            LanguageSymbol symbol = null;

            if (scopeList.Any(scope => scope.LookupSymbol(symbolName, roleNames, out symbol)))
                return symbol;

            throw new KeyNotFoundException();
        }
        
        
        /// <summary>
        /// Get a list of all symbols in the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <returns>A list of symbols</returns>
        public static IEnumerable<LanguageSymbol> Symbols(this IEnumerable<LanguageScope> scopeList)
        {
            return scopeList.SelectMany(scope => scope.Symbols());
        }

        /// <summary>
        /// Get a list of all symbols with a given role name in the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="roleName">The role name</param>
        /// <returns>A list of symbols</returns>
        public static IEnumerable<LanguageSymbol> Symbols(this IEnumerable<LanguageScope> scopeList, string roleName)
        {
            return scopeList.SelectMany(scope => scope.Symbols(roleName));
        }

        /// <summary>
        /// Get a list of all symbols having a role name from a given list of role names in the given list of scopes
        /// </summary>
        /// <param name="scopeList">A list of scopes</param>
        /// <param name="roleNames">A list of possible role names of the symbol</param>
        /// <returns>A list of symbols</returns>
        public static IEnumerable<LanguageSymbol> Symbols(this IEnumerable<LanguageScope> scopeList, IEnumerable<string> roleNames)
        {
            return scopeList.SelectMany(scope => scope.Symbols(roleNames));
        }
    }
}
