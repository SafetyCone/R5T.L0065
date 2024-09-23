using System;

using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F003
{
    [FunctionalityMarker]
    public partial interface IFullTypeNameOperator : IFunctionalityMarker
    {
        /// <summary>
        /// Simple type name includes:
        /// <list type="bullet">
        /// <item>Generic type parameter and generic method type parameter marker token prefixes ("`" and "``") for generic type and generic method parameters.</item>
        /// <item>Element type relationship marker token suffixes for types with element types ("[]" for arrays, "&amp;" for references, and "*" for pointers).</item>
        /// </list>
        /// It does not include:
        /// <list type="bullet">
        /// <item>Namespace for the type.</item>
        /// <item>Nested parent simple type name (if the type is nested).</item>
        /// <item>Generic type parameters list.</item>
        /// </list>
        /// </summary>
        public string Get_SimpleTypeName(TypeSignature typeSignature)
        {
            string typeIdentityString;

            if (typeSignature.Is_GenericTypeParameter)
            {
                typeIdentityString = Instances.TypeNameOperator.Get_GenericTypeParameterMarkedTypeName(typeSignature.TypeName);
            }
            else if (typeSignature.Is_GenericMethodParameter)
            {
                typeIdentityString = Instances.TypeNameOperator.Get_GenericMethodParameterMarkedTypeName(typeSignature.TypeName);
            }
            else if (typeSignature.Has_ElementType)
            {
                var elementTypeIdentityString = this.Get_SimpleTypeName(typeSignature.ElementType);

                typeIdentityString = Instances.ElementTypeRelationshipOperator.Append_ElementTypeRelationshipMarkers(
                    elementTypeIdentityString,
                    typeSignature.ElementTypeRelationships);
            }
            else
            {
                typeIdentityString = typeSignature.TypeName;
            }

            // Do not include the generic type parameters list.
            var output = typeIdentityString;
            return output;
        }

        /// <summary>
        /// Full type name includes:
        /// <list type="bullet">
        /// <item>Namespace for the type.</item>
        /// <item>Nested parent simple type names (if the type is nested).</item>
        /// <item>Generic type parameter and generic method type parameter marker tokens ("`" and "``") for generic type and generic method parameters.</item>
        /// <item>Element type relationship marker token for types with element types ("[]" for arrays, "&amp;" for references, and "*" for pointers).</item>
        /// <item>Generic type parameters list.</item>
        /// </list>
        /// </summary>
        public string Get_FullTypeName(TypeSignature typeSignature)
        {
            string typeIdentityString;

            if (typeSignature.Is_Nested)
            {
                var parentTypeSignature = this.Get_FullTypeName(typeSignature.NestedTypeParent);

                typeIdentityString = Instances.SignatureStringOperator.Append_NestedTypeTypeName(
                    parentTypeSignature,
                    typeSignature.TypeName);
            }
            else
            {
                if (typeSignature.Is_GenericTypeParameter)
                {
                    typeIdentityString = Instances.TypeNameOperator.Get_GenericTypeParameterMarkedTypeName(typeSignature.TypeName);
                }
                else if (typeSignature.Is_GenericMethodParameter)
                {
                    typeIdentityString = Instances.TypeNameOperator.Get_GenericMethodParameterMarkedTypeName(typeSignature.TypeName);
                }
                else if(typeSignature.Has_ElementType)
                {
                    var elementTypeIdentityString = this.Get_FullTypeName(typeSignature.ElementType);

                    typeIdentityString = Instances.ElementTypeRelationshipOperator.Append_ElementTypeRelationshipMarkers(
                        elementTypeIdentityString,
                        typeSignature.ElementTypeRelationships);
                }
                else
                {
                    var namespaceName = typeSignature.NamespaceName;

                    var namespaceNameIsNullOrEmpty = Instances.StringOperator.Is_NullOrEmpty(namespaceName);
                    if (namespaceNameIsNullOrEmpty)
                    {
                        typeIdentityString = typeSignature.TypeName;
                    }
                    else
                    {
                        typeIdentityString = Instances.SignatureStringOperator.Combine(
                            typeSignature.NamespaceName,
                            typeSignature.TypeName);
                    }
                }
            }

            // No need to check if null or empty; if so, an empty string will be returned.
            var genericTypeInputsList = Instances.SignatureOperator.Get_GenericTypeInputsList(typeSignature.GenericTypeInputs);

            var output = Instances.SignatureStringOperator.Append_TypeParameterList(
                typeIdentityString,
                genericTypeInputsList);

            return output;
        }
    }
}
