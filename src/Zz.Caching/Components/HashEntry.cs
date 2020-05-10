using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Zz.Caching.Components
{
    public readonly struct HashEntry : IEquatable<HashEntry>
    {
        internal readonly string field, value;

        public HashEntry(string field, string value)
        {
            this.field = field;
            this.value = value;
        }

        public string Field => field;

        public string Value => value;

        /// <summary>转为 KeyValuePair</summary>
        public static implicit operator KeyValuePair<string, string>(HashEntry value) =>
            new KeyValuePair<string, string>(value.field, value.value);

        /// <summary>转为 HashEntry</summary>
        public static implicit operator HashEntry(KeyValuePair<string, string> value) =>
            new HashEntry(value.Key, value.Value);

        public override string ToString() => field + ": " + value;

        public override int GetHashCode() => field.GetHashCode() ^ value.GetHashCode();

        #region equality
        public override bool Equals(object obj) => obj is HashEntry heObj && Equals(heObj);

        public bool Equals(HashEntry other) => field == other.field && value == other.value;

        public static bool operator ==(HashEntry x, HashEntry y) => x.field == y.field && x.value == y.value;

        public static bool operator !=(HashEntry x, HashEntry y) => x.field != y.field || x.value != y.value;
        #endregion
    }
}
