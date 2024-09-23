using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker
    {
        public void SignatureTypeSwitch(
            Signature signature,
            Action<EventSignature> eventSignatureAction,
            Action<FieldSignature> fieldSignatureAction,
            Action<PropertySignature> propertySignatureAction,
            Action<MethodSignature> methodSignatureAction,
            Action<TypeSignature> typeSignatureAction)
        {
            switch (signature)
            {
                case EventSignature eventSignature:
                    eventSignatureAction(eventSignature);
                    break;
                case FieldSignature fieldSignature:
                    fieldSignatureAction(fieldSignature);
                    break;
                case PropertySignature propertySignature:
                    propertySignatureAction(propertySignature);
                    break;
                case MethodSignature methodSignature:
                    methodSignatureAction(methodSignature);
                    break;
                case TypeSignature typeSignature:
                    typeSignatureAction(typeSignature);
                    break;
                default:
                    throw Instances.ExceptionOperator.Get_UnrecognizedSignatureType(signature);
            };
        }

        public TOutput SignatureTypeSwitch<TOutput>(
            Signature signature,
            Func<EventSignature, TOutput> eventSignatureFunction,
            Func<FieldSignature, TOutput> fieldSignatureFunction,
            Func<PropertySignature, TOutput> propertySignatureFunction,
            Func<MethodSignature, TOutput> methodSignatureFunction,
            Func<TypeSignature, TOutput> typeSignatureFunction)
        {
            var output = signature switch
            {
                EventSignature eventSignature => eventSignatureFunction(eventSignature),
                FieldSignature fieldSignature => fieldSignatureFunction(fieldSignature),
                PropertySignature propertySignature => propertySignatureFunction(propertySignature),
                MethodSignature methodSignature => methodSignatureFunction(methodSignature),
                TypeSignature typeSignature => typeSignatureFunction(typeSignature),
                _ => throw Instances.ExceptionOperator.Get_UnrecognizedSignatureType(signature)
            };

            return output;
        }

        public TOutput SignatureTypeSwitch<TOutput>(
            Signature signatureA,
            Signature signatureB,
            Func<EventSignature, EventSignature, TOutput> eventSignatureFunction,
            Func<FieldSignature, FieldSignature, TOutput> fieldSignatureFunction,
            Func<PropertySignature, PropertySignature, TOutput> propertySignatureFunction,
            Func<MethodSignature, MethodSignature, TOutput> methodSignatureFunction,
            Func<TypeSignature, TypeSignature, TOutput> typeSignatureFunction)
        {
            var output = this.SignatureTypeSwitch(
                signatureA,
                eventSignature => eventSignatureFunction(eventSignature, signatureB as EventSignature),
                fieldSignature => fieldSignatureFunction(fieldSignature, signatureB as FieldSignature),
                propertySignature => propertySignatureFunction(propertySignature, signatureB as PropertySignature),
                methodSignature => methodSignatureFunction(methodSignature, signatureB as MethodSignature),
                typeSignature => typeSignatureFunction(typeSignature, signatureB as TypeSignature));

            return output;
        }

        /// <summary>
        /// Handles all signature types.
        /// </summary>
        public bool Are_Equal_ByValue(Signature signatureA, Signature signatureB)
        {
            var typeDeterminesEquality = Instances.TypeOperator.TypeCheckDeterminesEquality(signatureA, signatureB, out var typesAreEqual);
            if(typeDeterminesEquality)
            {
                return typesAreEqual;
            }
            // Now we know the derived types are the same.

            var output = this.SignatureTypeSwitch(
                signatureA,
                signatureB,
                Instances.EventSignatureOperator.Are_Equal_ByValue,
                Instances.FieldSignatureOperator.Are_Equal_ByValue,
                Instances.PropertySignatureOperator.Are_Equal_ByValue,
                Instances.MethodSignatureOperator.Are_Equal_ByValue,
                Instances.TypeSignatureOperator.Are_Equal_ByValue);

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
