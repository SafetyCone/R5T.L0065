using System;
using System.Reflection;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker
    {
        /// <summary>
        /// Handles all signature types.
        /// </summary>
        public bool Are_Equal_ByValue(Signature a, Signature b)
        {
            var typeDeterminesEquality = Instances.TypeOperator.TypeDeterminesEquality(a, b, out var typesAreEqual);
            if(typeDeterminesEquality)
            {
                return typesAreEqual;
            }
            // Now we know the derived types are the same.

            var output = a switch
            {
                EventSignature eventSignature => Instances.EventSignatureOperator.Are_Equal_ByValue(eventSignature, b as EventSignature),
                FieldSignature fieldSignature => Instances.FieldSignatureOperator.Are_Equal_ByValue(fieldSignature, b as FieldSignature),
                PropertySignature propertySignature => Instances.PropertySignatureOperator.Are_Equal_ByValue(propertySignature, b as PropertySignature),
                MethodSignature methodSignature => Instances.MethodSignatureOperator.Are_Equal_ByValue(methodSignature, b as MethodSignature),
                TypeSignature typeSignature => Instances.TypeSignatureOperator.Are_Equal_ByValue(typeSignature, b as TypeSignature),
                _ => throw Instances.ExceptionOperator.Get_UnrecognizedSignatureType(a)
            };

            return output;
        }

        /// <summary>
        /// Checks only the properties of the signature (does not handle all types like <see cref="Are_Equal_ByValue(Signature, Signature)"/>).
        /// Note: performs null check.
        /// </summary>
        public bool Are_Equal_ByValue_SignatureOnly<TSignature>(TSignature a, TSignature b,
            Func<TSignature, TSignature, bool> equality)
            where TSignature: Signature
        {
            var output = Instances.NullOperator.NullCheckDeterminesEquality_Else(a, b,
                (a, b) =>
                {
                    var output = true;

                    output &= a.KindMarker == b.KindMarker;
                    output &= a.IsObsolete == b.IsObsolete;
                    output &= equality(a, b);

                    return output;
                });

            return output;
        }

        public void Set_IsObsolete<TSignature>(
            TSignature signature,
            MemberInfo memberInfo)
            where TSignature : Signature
        {
            var isObsolete = Instances.MemberInfoOperator.Is_Obsolete(memberInfo);

            signature.IsObsolete = isObsolete;
        }
    }
}
