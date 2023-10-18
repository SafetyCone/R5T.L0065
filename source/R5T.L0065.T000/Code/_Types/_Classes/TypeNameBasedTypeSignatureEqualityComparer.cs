using System;
using System.Collections.Generic;


namespace R5T.L0065.T000
{
    public class TypeNameBasedTypeSignatureEqualityComparer : IEqualityComparer<TypeSignature>
    {
        #region Static

        public static TypeNameBasedTypeSignatureEqualityComparer Instance { get; } = new TypeNameBasedTypeSignatureEqualityComparer();

        #endregion


        public bool Equals(TypeSignature x, TypeSignature y)
        {
            var output = x.TypeName == y.TypeName;
            return output;
        }

        public int GetHashCode(TypeSignature obj)
        {
            var output = obj.TypeName.GetHashCode();
            return output;
        }
    }
}
