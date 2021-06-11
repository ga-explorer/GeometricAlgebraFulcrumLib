using System;
using Microsoft.CSharp.RuntimeBinder;

namespace DataStructuresLib
{
    /// <summary>
    /// This class is used for implementing a dynamic visitor pattern
    /// See http://www.codeproject.com/Articles/112944/Polymorphic-Extension-Visitor-with-C
    /// </summary>
    public interface IDynamicTreeVisitor<in TNode> where TNode : class
    {
        /// <summary>
        /// The UseExceptions flag controls if a missing Visit method throws an exception (true) 
        /// or if it executes the Fallback method (false).
        /// </summary>
        bool UseExceptions { get; }

        /// <summary>
        /// If true and the element is null nothing is done, If false, the UseExceptions determines
        /// if the fallback method is called or an exception is raised
        /// </summary>
        bool IgnoreNullElements { get; }

        /// <summary>
        /// The fall back method that is called if: 
        ///    1- No matching visit method is found and the UseExceptions flag is false
        ///    2- The given item is null and UseExceptions is false and IgnoreNullElements is false
        /// </summary>
        /// <param name="item">The item that is visited</param>
        /// <param name="excException">The runtime exception indicating no Visit method is found</param>
        void Fallback(TNode item, RuntimeBinderException excException);
    }

    public interface IDynamicTreeVisitor<in TNode, out TReturnValue> where TNode : class
    {
        /// <summary>
        /// The UseExceptions flag controls if a missing Visit method throws an exception (true) 
        /// or if it executes the Fallback method (false).
        /// </summary>
        bool UseExceptions { get; }

        /// <summary>
        /// If true and the element is null nothing is done, If false, the UseExceptions determines
        /// if the fallback method is called or an exception is raised
        /// </summary>
        bool IgnoreNullElements { get; }

        /// <summary>
        /// The fall back method that is called if: 
        ///    1- No matching visit method is found and the UseExceptions flag is false
        ///    2- The given item is null and UseExceptions is false and IgnoreNullElements is false
        /// </summary>
        /// <param name="item">The item that is visited</param>
        /// <param name="excException">The runtime exception indicating no Visit method is found</param>
        TReturnValue Fallback(TNode item, RuntimeBinderException excException);
    }

    public static class DynamicTreeVisitorExtension
    {
        /// <summary>
        /// Dynamic Accept extension method for dynamic AST node visitor.
        /// 
        /// Making the item a dynamic object enforces polymorphic call of a best matching Visit method.
        /// It takes the exactly matching method or the method that matches the closest base class of the objItem.
        /// 
        /// Making the visitor a dynamic object allows to define polymorphic visitor class hierarchies with 
        /// any additional Visit methods defined on any level of the visitor class hierarchy. 
        /// No need to add new Visit methods in all classes; you may also add the additional Visit in the leaf class only.
        /// 
        /// See http://www.codeproject.com/Articles/112944/Polymorphic-Extension-Visitor-with-C
        /// </summary>
        /// <param name="item"></param>
        /// <param name="visitor"></param>
        public static void AcceptVisitor<TNode>(this TNode item, IDynamicTreeVisitor<TNode> visitor)
            where TNode : class
        {
            //The given item is null
            if (ReferenceEquals(item, null))
            {
                if (visitor.IgnoreNullElements) return;

                //Do not use the fallback method
                if (visitor.UseExceptions)
                    throw new ArgumentNullException(nameof(item));

                //Use the fallback method
                visitor.Fallback(null, null);

                return;
            }

            //The given item is not null
            try
            {
                //Polymorphic Visit call
                ((dynamic)visitor).Visit((dynamic)item);
            }
            catch (RuntimeBinderException excException)
            {
                //Do not use the fallback method
                if (visitor.UseExceptions)
                    throw;

                //Use the fallback method
                visitor.Fallback(item, excException);
            }
        }


        /// <summary>
        /// Dynamic Accept extension method for dynamic AST node visitor.
        /// 
        /// Making the item a dynamic object enforces polymorphic call of a best matching Visit method.
        /// It takes the exactly matching method or the method that matches the closest base class of the objItem.
        /// 
        /// Making the visitor a dynamic object allows to define polymorphic visitor class hierarchies with 
        /// any additional Visit methods defined on any level of the visitor class hierarchy. 
        /// No need to add new Visit methods in all classes; you may also add the additional Visit in the leaf class only.
        /// 
        /// See http://www.codeproject.com/Articles/112944/Polymorphic-Extension-Visitor-with-C
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TReturnValue"></typeparam>
        /// <param name="item"></param>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public static TReturnValue AcceptVisitor<TNode, TReturnValue>(this TNode item, IDynamicTreeVisitor<TNode, TReturnValue> visitor)
            where TNode : class
        {
            //The given item is null
            if (ReferenceEquals(item, null))
            {
                if (visitor.IgnoreNullElements) return default(TReturnValue);

                //Do not use the fallback method
                if (visitor.UseExceptions)
                    throw new ArgumentNullException(nameof(item));

                //Use the fallback method
                return visitor.Fallback(null, null);
            }

            //The given item is not null
            try
            {
                //Polymorphic Visit call
                return ((dynamic)visitor).Visit((dynamic)item);
            }
            catch (RuntimeBinderException excException)
            {
                //Do not use the fallback method
                if (visitor.UseExceptions)
                    throw;

                //Use the fallback method
                return visitor.Fallback(item, excException);
            }
        }
    }
}
