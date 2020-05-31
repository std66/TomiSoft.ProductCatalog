using System;
using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.BusinessModels.Concepts {
    public abstract class NumericIdentifierBM : IEquatable<NumericIdentifierBM> {
        public NumericIdentifierBM(long value) {
            Value = value;
        }

        public long Value { get; }

        public override bool Equals(object obj) {
            return Equals(obj as NumericIdentifierBM);
        }

        public bool Equals(NumericIdentifierBM other) {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode() {
            return -1937169414 + Value.GetHashCode();
        }

        public static bool operator ==(NumericIdentifierBM left, NumericIdentifierBM right) {
            return EqualityComparer<NumericIdentifierBM>.Default.Equals(left, right);
        }

        public static bool operator !=(NumericIdentifierBM left, NumericIdentifierBM right) {
            return !(left == right);
        }

        public override string ToString() => Value.ToString();
    }
}
