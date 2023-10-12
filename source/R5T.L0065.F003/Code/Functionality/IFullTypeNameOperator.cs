using System;

using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F003
{
    [FunctionalityMarker]
    public partial interface IFullTypeNameOperator : IFunctionalityMarker
    {
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
                        typeSignature.ElementTypeRelationships,
                        "[]",
                        "&",
                        "*");
                }
                else
                {
                    typeIdentityString = Instances.SignatureStringOperator.Combine(
                        typeSignature.NamespaceName,
                        typeSignature.TypeName);
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
