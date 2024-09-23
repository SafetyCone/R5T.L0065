using System;
using System.Collections.Generic;

using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F000
{
    /// <summary>
    /// Common <see cref="Signature"/>-related operations.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker,
        T000.ISignatureOperator
    {
        /// <summary>
        /// Describes the parts of the signature structure.
        /// </summary>
        public IEnumerable<string> Describe(Signature signature)
        {
            var output = this.SignatureTypeSwitch(
                signature,
                this.Describe,
                this.Describe,
                this.Describe,
                this.Describe,
                this.Describe
            );

            return output;
        }

        /// <inheritdoc cref="Describe(Signature)"/>
        public IEnumerable<string> Describe(EventSignature eventSignature)
        {
            var lines = Instances.EnumerableOperator.From($"{nameof(EventSignature)}:")
                ;

            return lines;
        }

        /// <inheritdoc cref="Describe(Signature)"/>
        public IEnumerable<string> Describe(FieldSignature fieldSignature)
        {
            var lines = Instances.EnumerableOperator.From($"{nameof(FieldSignature)}:")
                ;

            return lines;
        }

        /// <inheritdoc cref="Describe(Signature)"/>
        public IEnumerable<string> Describe(PropertySignature propertySignature)
        {
            var lines = Instances.EnumerableOperator.From($"{nameof(PropertySignature)}: {propertySignature}")
                ;

            return lines;
        }

        /// <inheritdoc cref="Describe(Signature)"/>
        public IEnumerable<string> Describe(MethodSignature methodSignature)
        {
            var lines = Instances.EnumerableOperator.From($"{nameof(MethodSignature)}:")
                ;

            return lines;
        }

        /// <inheritdoc cref="Describe(Signature)"/>
        public IEnumerable<string> Describe(TypeSignature typeSignature)
        {
            var namespacedTypeName = this.Get_NamespacedTypeName(typeSignature);

            var lines = Instances.EnumerableOperator.From($"{nameof(TypeSignature)}: {namespacedTypeName}")
                ;

            return lines;
        }

        /// <summary>
        /// The namespaced type name includes:
        /// <list type="bullet">
        /// <item>Namespace for the type.</item>
        /// <item>Nested parent namespaced type name (if the type is nested).</item>
        /// <item>Element type relationship marker token suffixes for types with element types ("[]" for arrays, "&amp;" for references, and "*" for pointers).</item>
        /// </list>
        /// It does not include:
        /// <list type="bullet">
        /// <item>Generic type parameter and generic method type parameter marker token prefixes ("`" and "``") for generic type and generic method parameters.</item>
        /// <item>Generic type parameters list.</item>
        /// </list>
        /// </summary>
        public string Get_NamespacedTypeName(TypeSignature typeSignature)
        {
            if (typeSignature.Is_Nested)
            {
                var nestedParentType_NamespacedTypeName = this.Get_NamespacedTypeName(typeSignature.NestedTypeParent);

                var output = $"{nestedParentType_NamespacedTypeName}{Instances.TokenSeparators.NestedTypeNameTokenSeparator}{typeSignature.TypeName}";
                return output;
            }
            else
            {
                string typeIdentityString;

                //if (typeSignature.Is_GenericTypeParameter)
                //{
                //    typeIdentityString = Instances.TypeNameOperator.Get_GenericTypeParameterMarkedTypeName(typeSignature.TypeName);
                //}
                //else if (typeSignature.Is_GenericMethodParameter)
                //{
                //    typeIdentityString = Instances.TypeNameOperator.Get_GenericMethodParameterMarkedTypeName(typeSignature.TypeName);
                //}
                //else
                if (typeSignature.Has_ElementType)
                {
                    var elementTypeIdentityString = this.Get_NamespacedTypeName(typeSignature.ElementType);

                    typeIdentityString = Instances.ElementTypeRelationshipOperator.Append_ElementTypeRelationshipMarkers(
                        elementTypeIdentityString,
                        typeSignature.ElementTypeRelationships,
                        "[]",
                        "&",
                        "*");
                }
                else
                {
                    typeIdentityString = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                        typeSignature.NamespaceName,
                        typeSignature.TypeName);
                }

                return typeIdentityString;
            }
        }
    }
}
