﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using Newtonsoft.Json;
using System;

namespace QuantConnect
{
    /// <summary>
    /// Represents a unique security identifier. This is made of two components,
    /// the unique SID and the Value. The value is the current ticker symbol while
    /// the SID is constant over the life of a security
    /// </summary>
    [JsonConverter(typeof(SymbolJsonConverter))]
    public sealed class Symbol : IEquatable<Symbol>, IComparable
    {
        /// <summary>
        /// Represents an unassigned symbol. This is intended to be used as an
        /// uninitialized, default value
        /// </summary>
        public static readonly Symbol Empty = new Symbol(SecurityIdentifier.Empty, string.Empty);

        /// <summary>
        /// Gets the current symbol for this ticker
        /// </summary>
        public string Value;

        /// <summary>
        /// Gets the security identifier for this symbol
        /// </summary>
        public SecurityIdentifier ID;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class
        /// </summary>
        /// <param name="sid">The security identifier for this symbol</param>
        /// <param name="value">The current ticker symbol value</param>
        public Symbol(SecurityIdentifier sid, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            ID = sid;
            Value = value.ToUpper();
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            // compare strings just as you would a symbol object
            var sidString = obj as string;
            if (sidString != null)
            {
                SecurityIdentifier sid;
                if (SecurityIdentifier.TryParse(sidString, out sid))
                {
                    return ID.Equals(sid);
                }
            }

            // compare a sid just as you would a symbol object
            if (obj is SecurityIdentifier)
            {
                return ID.Equals((SecurityIdentifier)obj);
            }

            if (obj.GetType() != GetType()) return false;
            return Equals((Symbol)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            // only SID is used for comparisons
            unchecked { return ID.GetHashCode(); }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj"/> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj"/>. Greater than zero This instance follows <paramref name="obj"/> in the sort order. 
        /// </returns>
        /// <param name="obj">An object to compare with this instance. </param><exception cref="T:System.ArgumentException"><paramref name="obj"/> is not the same type as this instance. </exception><filterpriority>2</filterpriority>
        public int CompareTo(object obj)
        {
            var str = obj as string;
            if (str != null)
            {
                return string.Compare(Value, str, StringComparison.OrdinalIgnoreCase);
            }
            var sym = obj as Symbol;
            if (sym != null)
            {
                return string.Compare(Value, sym.Value, StringComparison.OrdinalIgnoreCase);
            }

            throw new ArgumentException("Object must be of type Symbol or string.");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return SymbolCache.GetTicker(this);
        }

        #endregion

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Symbol other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            // only SID is used for comparisons
            return ID.Equals(other.ID);
        }

        /// <summary>
        /// Equals operator 
        /// </summary>
        /// <param name="left">The left operand</param>
        /// <param name="right">The right operand</param>
        /// <returns>True if both symbols are equal, otherwise false</returns>
        public static bool operator ==(Symbol left, Symbol right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        /// <summary>
        /// Not equals operator 
        /// </summary>
        /// <param name="left">The left operand</param>
        /// <param name="right">The right operand</param>
        /// <returns>True if both symbols are not equal, otherwise false</returns>
        public static bool operator !=(Symbol left, Symbol right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
            return !left.Equals(right);
        }

        #endregion

        #region String methods

        // in order to maintain better compile time backwards compatibility,
        // we'll redirect a few common string methods to Value, but mark obsolete
#pragma warning disable 1591
        [Obsolete("Symbol.Contains is a pass-through for Symbol.Value.Contains")]
        public bool Contains(string value) { return Value.Contains(value); }
        [Obsolete("Symbol.EndsWith is a pass-through for Symbol.Value.EndsWith")]
        public bool EndsWith(string value) { return Value.EndsWith(value); }
        [Obsolete("Symbol.StartsWith is a pass-through for Symbol.Value.StartsWith")]
        public bool StartsWith(string value) { return Value.StartsWith(value); }
        [Obsolete("Symbol.ToLower is a pass-through for Symbol.Value.ToLower")]
        public string ToLower() { return Value.ToLower(); }
        [Obsolete("Symbol.ToUpper is a pass-through for Symbol.Value.ToUpper")]
        public string ToUpper() { return Value.ToUpper(); }
#pragma warning restore 1591

        #endregion
    }
}
