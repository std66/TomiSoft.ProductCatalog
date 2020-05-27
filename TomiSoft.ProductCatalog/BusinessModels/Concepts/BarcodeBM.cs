using System;
using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.BusinessModels.Concepts {
    public class BarcodeBM : IEquatable<BarcodeBM> {
        public BarcodeBM(string value) {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj) {
            return Equals(obj as BarcodeBM);
        }

        public bool Equals(BarcodeBM other) {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode() {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

        public static bool operator ==(BarcodeBM left, BarcodeBM right) {
            return EqualityComparer<BarcodeBM>.Default.Equals(left, right);
        }

        public static bool operator !=(BarcodeBM left, BarcodeBM right) {
            return !(left == right);
        }

        public static implicit operator BarcodeBM(string value) => new BarcodeBM(value);
        public static implicit operator string(BarcodeBM barcodeBM) => barcodeBM.Value;
    }
}
