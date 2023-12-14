using System;
using System.Collections.Generic;


namespace R5T.L0065.T000
{
    /// <inheritdoc cref="ITypeSignatureOperator.Are_Equal_ByIdentity(TypeSignature, TypeSignature)"/>
    public class IdentityBasedTypeSignatureEqualityComparer : IEqualityComparer<TypeSignature>
    {
        #region Static

        public static IdentityBasedTypeSignatureEqualityComparer Instance { get; } = new IdentityBasedTypeSignatureEqualityComparer();

        #endregion


        public bool Equals(TypeSignature x, TypeSignature y)
        {
            var output = Instances.TypeSignatureOperator.Are_Equal_ByIdentity(x, y);
            return output;
        }

        public int GetHashCode(TypeSignature obj)
        {
            var output = Instances.TypeSignatureOperator.Get_HashCode_ForIdentity(obj);
            return output;
        }
    }
}
