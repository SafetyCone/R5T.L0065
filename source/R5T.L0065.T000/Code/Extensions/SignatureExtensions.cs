using System;
using System.Reflection;


namespace R5T.L0065.T000.Extensions
{
    public static class SignatureExtensions
    {
        public static TSignature Set_IsObsolete<TSignature>(this TSignature signature,
            MemberInfo memberInfo)
            where TSignature : Signature
        {
            Instances.SignatureOperator.Set_IsObsolete(
                signature,
                memberInfo);

            return signature;
        }
    }
}
