using System.Collections;
using System.Reflection;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Core.Structures;

/// <summary>
/// Source: https://www.codeproject.com/Tips/876878/Calculating-Optimistic-Memory-Footprint-of-Managed
/// </summary>
public static class DataStructuresLibUtils
{
    /// <summary>
    /// Nice way to calculate the size of managed object!
    /// </summary>
    /// <typeparam name="TT"></typeparam>
    internal class Size<TT>
    {
        private readonly TT _obj;
        private readonly HashSet<object> _references;

        private static int PointerSize { get; }
            = Environment.Is64BitOperatingSystem 
                ? sizeof(long) 
                : sizeof(int);

        public Size(TT obj)
        {
            _obj = obj;
            _references = [_obj];
        }

        public long GetSizeInBytes()
        {
            return GetSizeInBytes(_obj);
        }

        // The core functionality. Recurrently calls itself when an object appears to have fields 
        // until all fields have been  visited, or were "visited" (calculated) already.
        private long GetSizeInBytes<T>(T obj)
        {
            if (obj == null) return sizeof(int);
            var type = obj.GetType();

            if (type.IsPrimitive)
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                        return sizeof(byte);
                    case TypeCode.Char:
                        return sizeof(char);
                    case TypeCode.Single:
                        return sizeof(float);
                    case TypeCode.Double:
                        return sizeof(double);
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                        return sizeof(short);
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                        return sizeof(int);
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    default:
                        return sizeof(long);
                }
            }
            else if (obj is decimal)
            {
                return sizeof(decimal);
            }
            else if (obj is string)
            {
                return sizeof(char) * obj.ToString().Length;
            }
            else if (type.IsEnum)
            {
                return sizeof(int);
            }
            else if (type.IsArray)
            {
                long size = PointerSize;
                var casted = (IEnumerable)obj;
                foreach (var item in casted)
                {
                    size += GetSizeInBytes(item);
                }
                return size;
            }
            else if (obj is Pointer)
            {
                return PointerSize;
            }
            else
            {
                long size = 0;
                var t = type;
                while (t != null)
                {
                    size += PointerSize;
                    var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                             BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    foreach (var field in fields)
                    {
                        var tempVal = field.GetValue(obj);
                        if (!_references.Contains(tempVal))
                        {
                            _references.Add(tempVal);
                            size += GetSizeInBytes(tempVal);
                        }
                    }
                    t = t.BaseType;
                }
                return size;
            }
        }
    }

    // The actual, exposed method:
    public static long SizeInBytes<T>(this T someObject)
    {
        var temp = new Size<T>(someObject);
        var tempSize = temp.GetSizeInBytes();
        return tempSize;
    }

    /// <summary>
    /// Return the method signature as a string.
    /// </summary>
    ///
    /// <param name="property">
    /// The property to act on.
    /// </param>
    ///
    /// <returns>
    /// Method signature.
    /// </returns>
    public static string GetSignature(this PropertyInfo property)
    {
        var getter = property.GetGetMethod();
        var setter = property.GetSetMethod();

        var sigBuilder = new StringBuilder();
        var primaryDef = LeastRestrictiveVisibility(getter, setter);


        BuildReturnSignature(sigBuilder, primaryDef);
        sigBuilder.Append(" { ");
        if (getter != null)
        {
            if (primaryDef != getter)
            {
                sigBuilder.Append(Visibility(getter) + " ");
            }
            sigBuilder.Append("get; ");
        }
        if (setter != null)
        {
            if (primaryDef != setter)
            {
                sigBuilder.Append(Visibility(setter) + " ");
            }
            sigBuilder.Append("set; ");
        }
        sigBuilder.Append("}");
        return sigBuilder.ToString();

    }

    /// <summary>
    /// Return the method signature as a string.
    /// </summary>
    ///
    /// <param name="method">
    /// The Method.
    /// </param>
    /// <param name="callable">
    /// Return as an callable string(public void a(string b) would return a(b))
    /// </param>
    ///
    /// <returns>
    /// Method signature.
    /// </returns>
    public static string GetSignature(this MethodInfo method, bool callable = false)
    {
        var sigBuilder = new StringBuilder();

        BuildReturnSignature(sigBuilder, method, callable);

        sigBuilder.Append("(");
        var firstParam = true;
        var secondParam = false;

        var parameters = method.GetParameters();

        foreach (var param in parameters)
        {
            if (firstParam)
            {
                firstParam = false;
                if (method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false))
                {
                    if (callable)
                    {
                        secondParam = true;
                        continue;
                    }
                    sigBuilder.Append("this ");
                }
            }
            else if (secondParam)
                secondParam = false;
            else
                sigBuilder.Append(", ");

            if (param.IsOut)
                sigBuilder.Append("out ");
            else if (param.ParameterType.IsByRef)
                sigBuilder.Append("ref ");

            if (IsParamArray(param))
                sigBuilder.Append("params ");

            if (!callable)
            {
                sigBuilder.Append(TypeName(param.ParameterType));
                sigBuilder.Append(' ');
            }


            sigBuilder.Append(param.Name);

            if (param.IsOptional)
                sigBuilder.Append(" = " + (param.DefaultValue ?? "null"));
        }
        sigBuilder.Append(")");

        // generic constraints


        foreach (var arg in method.GetGenericArguments())
        {
            var constraints = arg.GetGenericParameterConstraints().Select(TypeName).ToList();

            var attrs = arg.GenericParameterAttributes;

            if (attrs.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint))
            {
                constraints.Add("class");
            }
            if (attrs.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint))
            {
                constraints.Add("struct");
            }
            if (attrs.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
            {
                constraints.Add("new()");
            }
            if (constraints.Count > 0)
            {
                sigBuilder.Append(" where " + TypeName(arg) + ": " + string.Join(", ", constraints));
            }
        }




        return sigBuilder.ToString();
    }

    /// <summary>
    /// Get full type name with full namespace names.
    /// </summary>
    ///
    /// <param name="type">
    /// Type. May be generic or nullable.
    /// </param>
    ///
    /// <returns>
    /// Full type name, fully qualified namespaces.
    /// </returns>
    public static string TypeName(Type type)
    {
        var nullableType = Nullable.GetUnderlyingType(type);
        if (nullableType != null)
        {
            return TypeName(nullableType) + "?";
        }


        if (!type.IsGenericType)
        {
            if (type.IsArray)
            {
                return TypeName(type.GetElementType()) + "[]";
            }

            //if (type.Si
            switch (type.Name)
            {
                case "String": return "string";
                case "Int16": return "short";
                case "UInt16": return "ushort";
                case "Int32": return "int";
                case "UInt32": return "uint";
                case "Int64": return "long";
                case "UInt64": return "ulong";
                case "Decimal": return "decimal";
                case "Double": return "double";
                case "Object": return "object";
                case "Void": return "void";

                default:
                {
                    return string.IsNullOrWhiteSpace(type.FullName) ? type.Name : type.FullName;
                }
            }
        }

        var sb = new StringBuilder(type.Name.Substring(0,
            type.Name.IndexOf('`'))
        );

        sb.Append('<');
        var first = true;
        foreach (var t in type.GetGenericArguments())
        {
            if (!first)
                sb.Append(',');
            sb.Append(TypeName(t));
            first = false;
        }
        sb.Append('>');
        return sb.ToString();
    }

    private static void BuildReturnSignature(StringBuilder sigBuilder, MethodInfo method, bool callable = false)
    {
        var firstParam = true;
        if (callable == false)
        {
            sigBuilder.Append(Visibility(method) + " ");

            if (method.IsStatic)
                sigBuilder.Append("static ");
            sigBuilder.Append(TypeName(method.ReturnType));
            sigBuilder.Append(' ');
        }
        sigBuilder.Append(method.Name);

        // Add method generics
        if (method.IsGenericMethod)
        {
            sigBuilder.Append("<");
            foreach (var g in method.GetGenericArguments())
            {
                if (firstParam)
                    firstParam = false;
                else
                    sigBuilder.Append(", ");
                sigBuilder.Append(TypeName(g));
            }
            sigBuilder.Append(">");
        }

    }

    private static string Visibility(MethodInfo method)
    {
        if (method.IsPublic)
            return "public";
        else if (method.IsPrivate)
            return "private";
        else if (method.IsAssembly)
            return "internal";
        else if (method.IsFamily)
            return "protected";
        else
        {
            throw new Exception("I wasn't able to parse the visibility of this method.");
        }
    }

    private static MethodInfo LeastRestrictiveVisibility(MethodInfo member1, MethodInfo member2)
    {
        if (member1 != null && member2 == null)
        {
            return member1;
        }
        else if (member2 != null && member1 == null)
        {
            return member2;
        }

        var vis1 = VisibilityValue(member1);
        var vis2 = VisibilityValue(member2);
        if (vis1 < vis2)
        {
            return member1;
        }
        else
        {
            return member2;
        }
    }

    private static int VisibilityValue(MethodInfo method)
    {
        if (method.IsPublic)
            return 1;
        else if (method.IsFamily)
            return 2;
        else if (method.IsAssembly)
            return 3;
        else if (method.IsPrivate)
            return 4;
        else
        {
            throw new Exception("I wasn't able to parse the visibility of this method.");
        }
    }

    private static bool IsParamArray(ParameterInfo info)
    {
        return Attribute.GetCustomAttribute(info, typeof(ParamArrayAttribute), true) != null;
    }
}