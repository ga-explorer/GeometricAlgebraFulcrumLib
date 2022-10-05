﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using JSONOPTIONS = System.Text.Json.JsonSerializerOptions;

namespace SharpGLTF.IO
{
    /// <summary>
    /// Represents an inmutable json object stored in memory.
    /// </summary>
    /// <remarks>
    /// The data structure is stored in memory as a DOM, using standard objects and collections.<br/>
    /// Use <see cref="Serialize(object, JSONOPTIONS)"/> and <see cref="Deserialize{T}(JSONOPTIONS)"/> to convert to your types.<br/>
    /// Use <see cref="Parse(JsonDocument)"/> and <see cref="ToJson(JSONOPTIONS)"/> to convert from/to raw json text.<br/>
    /// </remarks>
    [System.ComponentModel.ImmutableObject(true)]
    [System.Diagnostics.DebuggerDisplay("{ToDebuggerDisplay(),nq}")]
    public readonly struct JsonContent
    {
        #region debug

        private string ToDebuggerDisplay()
        {
            if (_Content == null) return null;

            var options = new JSONOPTIONS();
            options.WriteIndented = true;
            return ToJson(options);
        }

        #endregion

        #region constructors

        public static implicit operator JsonContent(Boolean value) { return new JsonContent(value); }

        public static implicit operator JsonContent(String value) { return new JsonContent(value); }

        public static implicit operator JsonContent(Int32 value) { return new JsonContent(value); }

        public static implicit operator JsonContent(Int64 value) { return new JsonContent(value); }

        public static implicit operator JsonContent(Single value) { return new JsonContent(value); }

        public static implicit operator JsonContent(Double value) { return new JsonContent(value); }

        public static JsonContent CreateFrom(IConvertible value) { return new JsonContent(value); }
        public static JsonContent CreateFrom(IList value) { return new JsonContent(value); }
        public static JsonContent CreateFrom(IDictionary value) { return new JsonContent(value); }

        internal static JsonContent _Wrap(Object value) { return new JsonContent(value); }

        private JsonContent(Object value)
        {
            _Content = value == null ? null : _JsonStaticUtils.Serialize(value);
            if (_Content is IJsonCollection collection && collection.Count == 0)
                _Content = null;
        }

        public JsonContent DeepClone() { return new JsonContent(_Content); }

        #endregion

        #region data

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly Object _Content;

        public override int GetHashCode()
        {
            // until I figure a correct way of handling this...
            throw new NotSupportedException("Do not use");
        }

        public override bool Equals(object obj)
        {
            // until I figure a correct way of handling this...
            throw new NotSupportedException($"Use {nameof(JsonContent.AreEqualByContent)} instead.");
        }

        /// <summary>
        /// Compares two <see cref="JsonContent"/> objects for equality.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <param name="precission">The precission threshold when comparing floating point values.</param>
        /// <returns>true if the objects are considered equal</returns>
        /// <remarks>
        /// - Comparing json structures is tricky because the values are typeless, so when we parse a json DOM
        /// into memory we don't know which should be the right type to use for comparison.
        /// - Also, System.Text.JSon is roundtrip safe when used in Net Core, but it is not when used in
        /// Net Framework, so depending on the framework we use, floating point roundtrips will behave differently.
        /// </remarks>
        public static bool AreEqualByContent(JsonContent a, JsonContent b, float precission)
        {
            return _JsonStaticUtils.AreEqualByContent(a._Content, b._Content, precission);
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the dynamic json structure.
        /// </summary>
        /// <remarks>
        /// The possible value types can be:<br/>
        /// - An <see cref="IConvertible"/> object.<br/>
        /// - A non empty <see cref="IReadOnlyList{Object}"/> object.<br/>
        /// - A non empty <see cref="IReadOnlyDictionary{String, Object}"/> object.
        /// </remarks>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Collapsed)]
        public Object Content => _Content;

        #endregion

        #region serialization

        /// <summary>
        /// Converts the value of a specified type into a <see cref="JsonContent"/> using <see cref="JsonSerializer"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="options">Options to control the conversion behavior.</param>
        /// <returns>A <see cref="JsonContent"/> object.</returns>
        public static JsonContent Serialize(Object value, JSONOPTIONS options = null)
        {
            if (value == null) return default;

            if (options == null)
            {
                options = new JSONOPTIONS
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true,
                    WriteIndented = true
                };
            }

            var json = JsonSerializer.Serialize(value, value.GetType(), options);

            return Parse(json);
        }

        /// <summary>
        /// Parses a json text an converts it to a <see cref="JsonContent"/>
        /// </summary>
        /// <param name="jsonContent">The json text content.</param>
        /// <param name="options">Parser options.</param>
        /// <returns>A <see cref="JsonContent"/> object</returns>
        public static JsonContent Parse(string jsonContent, JsonDocumentOptions options = default)
        {
            using (var doc = JsonDocument.Parse(jsonContent, options))
            {
                return Parse(doc);
            }
        }

        public static JsonContent Parse(JsonDocument root)
        {
            return root == null ? default : new JsonContent(_JsonStaticUtils.Deserialize(root.RootElement));
        }

        public string ToJson(JSONOPTIONS options = null)
        {
            return _JsonStaticUtils.ToJson(_Content, options);
        }

        public Object Deserialize(Type type, JSONOPTIONS options = null)
        {
            return _JsonStaticUtils.Deserialize(_Content, type, options);
        }

        public T Deserialize<T>(JSONOPTIONS options = null)
        {
            return (T)_JsonStaticUtils.Deserialize(_Content, typeof(T), options);
        }

        #endregion

        #region static API
        public static bool IsJsonSerializable(Object value) { return IsJsonSerializable(value, out _); }

        public static bool IsJsonSerializable(Object value, out Object invalidValue)
        {
            return _JsonStaticUtils.IsJsonSerializable(value, out invalidValue);
        }

        #endregion

        #region API

        public JsonContent GetNode(params IConvertible[] path)
        {
            var value = _JsonStaticUtils.GetNode(this._Content, path);
            return new JsonContent(value);
        }

        public T GetValue<T>(params IConvertible[] path)
            where T : IConvertible
        {
            return _JsonStaticUtils.GetValue<T>(this._Content, path);
        }

        #endregion
    }
}
