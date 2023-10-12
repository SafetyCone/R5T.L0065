using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface ITypeSignatureOperator : IFunctionalityMarker
    {
        public bool Are_Equal_ByValue(TypeSignature a, TypeSignature b)
        {
            var output = Instances.SignatureOperator.Are_Equal_ByValue_SignatureOnly(a, b,
                (a, b) =>
                {
                    var output = true;

                    output &= a.TypeName == b.TypeName;
                    output &= a.NamespaceName == b.NamespaceName;
                    output &= a.Is_Nested == b.Is_Nested;
                    output &= this.Are_Equal_ByValue(
                        a.NestedTypeParent,
                        b.NestedTypeParent);
                    output &= a.Is_GenericMethodParameter == b.Is_GenericMethodParameter;
                    output &= a.Is_GenericTypeParameter == b.Is_GenericTypeParameter;
                    output &= Instances.ArrayOperator.Are_Equal(
                        a.GenericTypeInputs,
                        b.GenericTypeInputs,
                        this.Are_Equal_ByValue);
                    output &= a.Has_ElementType == b.Has_ElementType;
                    output &= a.ElementTypeRelationships == b.ElementTypeRelationships;
                    output &= this.Are_Equal_ByValue(
                        a.ElementType,
                        b.ElementType);

                    return output;
                });

            return output;
        }

        public void Add_NestedTypeGenericTypeInputs(
            TypeSignature typeSignature,
            List<TypeSignature> genericTypeInputs)
        {
            if (typeSignature.Is_Nested)
            {
                this.Add_NestedTypeGenericTypeInputs(
                    typeSignature.NestedTypeParent,
                    genericTypeInputs);
            }

            var hasTypeGenericTypeInputs = Instances.NullOperator.Is_NotNull(typeSignature.GenericTypeInputs);
            if (hasTypeGenericTypeInputs)
            {
                // All generic type parameter names are unique within the context of a signature.
                var newGenericTypeInputs = typeSignature.GenericTypeInputs
                    .Except(
                        genericTypeInputs,
                        TypeNameBasedTypeSignatureEqualityComparer.Instance);

                genericTypeInputs.AddRange(newGenericTypeInputs);
            }
        }

        public TypeSignature Copy(TypeSignature typeSignature)
        {
            var output = new TypeSignature
            {
                GenericTypeInputs = typeSignature.GenericTypeInputs,
                IsObsolete = typeSignature.IsObsolete,
                Is_GenericMethodParameter = typeSignature.Is_GenericMethodParameter,
                Is_GenericTypeParameter = typeSignature.Is_GenericTypeParameter,
                Is_Nested = typeSignature.Is_Nested,
                KindMarker = typeSignature.KindMarker,
                NamespaceName = typeSignature.NamespaceName,
                NestedTypeParent = typeSignature.NestedTypeParent,
                TypeName = typeSignature.TypeName,
                ElementType = typeSignature.ElementType,
                ElementTypeRelationships = typeSignature.ElementTypeRelationships,
                Has_ElementType = typeSignature.Has_ElementType
            };

            return output;
        }

        public TypeSignature[] Get_NestedTypeGenericTypeInputs(TypeSignature typeSignature)
        {
            var genericTypeInputs = new List<TypeSignature>();

            this.Add_NestedTypeGenericTypeInputs(
                typeSignature,
                genericTypeInputs);

            var output = genericTypeInputs.ToArray();
            return output;
        }

        public TypeSignature[] Get_NestedTypeGenericTypeParameters(TypeSignature typeSignature)
        {
            var nestedTypeGenericTypeInputs = this.Get_NestedTypeGenericTypeInputs(typeSignature);

            var output = nestedTypeGenericTypeInputs
                .Where(x => x.Is_GenericTypeParameter)
                .Now();

            return output;
        }

        public void Reset(TypeSignature typeSignature)
        {
            typeSignature.GenericTypeInputs = default;
            typeSignature.IsObsolete = default;
            typeSignature.Is_GenericMethodParameter = default;
            typeSignature.Is_GenericTypeParameter = default;
            typeSignature.Is_Nested = default;
            typeSignature.KindMarker = Instances.KindMarkers.Type;
            typeSignature.NamespaceName = default;
            typeSignature.NestedTypeParent = default;
            typeSignature.TypeName = default;
            typeSignature.Has_ElementType = default;
            typeSignature.ElementType = default;
            typeSignature.ElementTypeRelationships = default;
        }
    }
}
