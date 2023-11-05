using System;
using System.Linq;

using R5T.L0062.L001;
using R5T.L0063.T000;
using R5T.N0000;
using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F004
{
    [FunctionalityMarker]
    public partial interface ISignatureStringOperator : IFunctionalityMarker,
        L0063.F000.ISignatureStringOperator
    {
        public Signature Get_Signature(ISignatureString signatureString)
        {
            var output = this.Get_Signature(signatureString.Value);
            return output;
        }

        public Signature Get_Signature(string signatureString)
        {
            var kindMarker = this.Get_KindMarker(signatureString);

            var signatureStringValueMaybeObsolete = this.Get_SignatureStringValue(signatureString);

            var signatureStringValue = this.Get_SignatureStringValue_WithoutObsolete_IfObsolete(
                signatureStringValueMaybeObsolete,
                out var isObsolete);

            Signature output = kindMarker switch
            {
                IKindMarkers.Error_Constant => throw Instances.ExceptionOperator.Get_ErrorSignatureDoesNotExistException(),
                IKindMarkers.Event_Constant => this.Get_EventSignature(signatureStringValue),
                IKindMarkers.Field_Constant => this.Get_FieldSignature(signatureStringValue),
                IKindMarkers.Method_Constant => this.Get_MethodSignature(signatureStringValue),
                IKindMarkers.Namespace_Constant => throw Instances.ExceptionOperator.Get_NamespaceSignatureDoesNotExistException(),
                IKindMarkers.Property_Constant => this.Get_PropertySignature(signatureStringValue),
                IKindMarkers.Type_Constant => this.Get_TypeSignature(signatureStringValue),
                _ => throw Instances.ExceptionOperator.Get_UnrecognizedKindMarkerException(kindMarker)
            };

            output.IsObsolete = isObsolete;

            return output;
        }

        public EventSignature Get_EventSignature(string eventSignatureStringValue)
        {
            (string signatureStringPart, string outputTypeName) = this.Get_OutputTypeParts(eventSignatureStringValue);

            var (namespacedTypeName, modifiedEventName) = this.Get_LastNamespaceParts(signatureStringPart);

            var declaringType = this.Get_TypeSignature(namespacedTypeName);
            var eventHandlerType = this.Get_TypeSignature(outputTypeName);

            var eventName = this.Modify_MemberName_ForMemberName(modifiedEventName);
            
            var output = new EventSignature
            {
                DeclaringType = declaringType,
                EventHandlerType = eventHandlerType,
                EventName = eventName,
                //IsObsolete = , // Handled in top-level caller.
            };

            return output;
        }

        public FieldSignature Get_FieldSignature(string fieldSignatureStringValue)
        {
            (string signatureStringPart, string outputTypeName) = this.Get_OutputTypeParts(fieldSignatureStringValue);

            var (namespacedTypeName, modifiedFieldName) = this.Get_LastNamespaceParts(signatureStringPart);

            var declaringType = this.Get_TypeSignature(namespacedTypeName);
            var fieldType = this.Get_TypeSignature(outputTypeName);

            var fieldName = this.Modify_MemberName_ForMemberName(modifiedFieldName);

            var output = new FieldSignature
            {
                DeclaringType = declaringType,
                FieldType = fieldType,
                FieldName = fieldName,
                //IsObsolete = , // Handled in top-level caller.
            };

            return output;
        }

        public MethodSignature Get_MethodSignature(string methodSignatureStringValue)
        {
            (string signatureStringPart, string outputTypeName) = this.Get_OutputTypeParts(methodSignatureStringValue);

            // Does the method have a return type (constructor methods do not).
            var hasReturnType = outputTypeName != null;

            var returnType = hasReturnType
                ? this.Get_TypeSignature(outputTypeName)
                : null
                ;

            var (declaringTypeSignature, modifiedMethodName, methodGenericTypesListValue, parameterListValue) = this.Decompose_MethodSignatureSegments(signatureStringPart);

            var declaringType = this.Get_TypeSignature(declaringTypeSignature);

            // If the method is a contructor, the return type is the declaring type.
            var isConstructor = modifiedMethodName == "#ctor";
            if (isConstructor)
            {
                returnType = declaringType;
            }

            var parameters = this.Parse_MethodParameters(parameterListValue);

            var methodGenericTypeInputs = this.Parse_TypesListValue(methodGenericTypesListValue);

            var methodName = this.Modify_MemberName_ForMemberName(modifiedMethodName);

            var output = new MethodSignature
            {
                DeclaringType = declaringType,
                MethodName = methodName,
                GenericTypeInputs = methodGenericTypeInputs,
                Parameters = parameters,
                ReturnType = returnType,
                //IsObsolete = , // Handled in top-level caller.
            };

            return output;
        }

        public PropertySignature Get_PropertySignature(string propertySignatureStringValue)
        {
            (string signatureStringPart, string outputTypeName) = this.Get_OutputTypeParts(propertySignatureStringValue);

            var propertyType = this.Get_TypeSignature(outputTypeName);

            var (declaringTypeSignature, modifiedPropertyName, parameterListValue) = this.Decompose_PropertySignatureSegments(signatureStringPart);

            var declaringType = this.Get_TypeSignature(declaringTypeSignature);

            var parameters = this.Parse_MethodParameters(parameterListValue);

            var propertyName = this.Modify_MemberName_ForMemberName(modifiedPropertyName);

            var output = new PropertySignature
            {
                DeclaringType = declaringType,
                Parameters = parameters,
                PropertyName = propertyName,
                PropertyType = propertyType,
                //IsObsolete = , // Handled in top-level caller.
            };

            return output;
        }

        public bool If_HasElementType_SetElementTypeValues(
            string typeSignatureStringValue,
            TypeSignature output)
        {
            bool If_Array_SetArrayElementType(
                string typeSignatureStringValue,
                TypeSignature output)
            {
                var isArray = this.Is_Array(typeSignatureStringValue);
                if (isArray)
                {
                    output.ElementTypeRelationships |= ElementTypeRelationships.Array;

                    var elementTypeValue = this.Get_ArrayElementType(typeSignatureStringValue);

                    output.ElementType = this.Get_TypeSignature(elementTypeValue);
                }

                return isArray;
            }

            void If_Reference_SetReferenceElementType(
                string typeSignatureStringValue,
                TypeSignature output)
            {
                var isReference = this.Is_Reference(typeSignatureStringValue);
                if (isReference)
                {
                    output.ElementTypeRelationships |= ElementTypeRelationships.Reference;

                    var elementTypeValue = this.Get_ReferenceElementType(typeSignatureStringValue);

                    output.ElementType = this.Get_TypeSignature(elementTypeValue);
                }
            }

            void If_Pointer_SetPointerElementType(
                string typeSignatureStringValue,
                TypeSignature output)
            {
                var isPointer = this.Is_Pointer(typeSignatureStringValue);
                if (isPointer)
                {
                    output.ElementTypeRelationships |= ElementTypeRelationships.Pointer;

                    var elementTypeValue = this.Get_PointerElementType(typeSignatureStringValue);

                    output.ElementType = this.Get_TypeSignature(elementTypeValue);
                }
            }

            var hasElementType = this.Has_ElementType(typeSignatureStringValue);
            if (hasElementType)
            {
                output.Has_ElementType = true;

                // TODO: which comes first if you have a reference array (or array reference)???
                If_Array_SetArrayElementType(
                    typeSignatureStringValue,
                    output);

                If_Reference_SetReferenceElementType(
                    typeSignatureStringValue,
                    output);

                If_Pointer_SetPointerElementType(
                    typeSignatureStringValue,
                    output);
            }

            return hasElementType;
        }

        public TypeSignature Get_TypeSignature(string typeSignatureStringValue)
        {
            var output = new TypeSignature();

            // Has element type.
            var hasElementType = this.If_HasElementType_SetElementTypeValues(
                typeSignatureStringValue,
                output);
            if(hasElementType)
            {
                // We are done.
                return output;
            }

            // Is this a type parameter of a type definition?
            var isGenericTypeParameterType = this.Is_GenericTypeParameterTypeName(typeSignatureStringValue);
            if (isGenericTypeParameterType)
            {
                var genericTypeParameterTypeName = this.Get_GenericTypeParameterTypeName(typeSignatureStringValue);

                output.Is_GenericTypeParameter = true;
                output.TypeName = genericTypeParameterTypeName;

                // Done.
                return output;
            }

            // Is this is type parameter of a method definition?
            var isGenericMethodParameterType = this.Is_GenericMethodParameterTypeName(typeSignatureStringValue);
            if (isGenericMethodParameterType)
            {
                var genericMethodParameterTypeName = this.Get_GenericMethodParameterTypeName(typeSignatureStringValue);

                output.Is_GenericMethodParameter = true;
                output.TypeName = genericMethodParameterTypeName;

                return output;
            }

            // Get namespace and type name.
            // Is the type nested?
            var isNested = this.Is_Nested(typeSignatureStringValue);
            if (isNested)
            {
                output.Is_Nested = true;

                var indicesOfNestedTypeNameTokenSeparators = this.Get_IndicesOfNestedTypeNameTokenSeparators(
                    typeSignatureStringValue);

                var indexOfLastNestedTypeTokenSeparator = indicesOfNestedTypeNameTokenSeparators.Max();

                var (nestedTypeParentTypeSignatureStringValue, nestedTypeName) = Instances.StringOperator.Partition(
                    indexOfLastNestedTypeTokenSeparator,
                    typeSignatureStringValue);

                output.NestedTypeParent = this.Get_TypeSignature(nestedTypeParentTypeSignatureStringValue);

                var hasGenericInputsList = this.Has_GenericInputsList(nestedTypeName);
                if(hasGenericInputsList)
                {
                    var genericInputsListRanges = this.Get_GenericInputListRanges(nestedTypeName);

                    // There should only be one for types!
                    var genericInputsListRange = genericInputsListRanges.Single();

                    var typeName = Instances.StringOperator.Get_Substring_Upto_Exclusive(
                        genericInputsListRange.Start.Value,
                        nestedTypeName);

                    var genericInputsList = nestedTypeName[genericInputsListRange];

                    output.GenericTypeInputs = this.Parse_TypesList(genericInputsList);
                    output.TypeName = typeName;
                }
                else
                {
                    output.TypeName = nestedTypeName;
                }

                // No namespace for nested types (their namespace is the namespace and type name of their parent type).

                // We are done.
                return output;
            }
            else
            {
                // Not nested.
                var hasGenericInputsList = this.Has_GenericInputsList(typeSignatureStringValue);
                if (hasGenericInputsList)
                {
                    var genericInputsListRanges = this.Get_GenericInputListRanges(typeSignatureStringValue);

                    // There should only be one for types!
                    var genericInputsListRange = genericInputsListRanges.Single();

                    var namespacedTypeName = Instances.StringOperator.Get_Substring_Upto_Exclusive(
                        genericInputsListRange.Start.Value,
                        typeSignatureStringValue);

                    var genericInputsList = typeSignatureStringValue[genericInputsListRange];

                    output.GenericTypeInputs = this.Parse_TypesList(genericInputsList);

                    var hasNamespace = this.Has_Namespace(namespacedTypeName);
                    if(hasNamespace)
                    {
                        var indexOfLastNamespaceTokenSeparator = Instances.StringOperator.Get_LastIndexOf(
                            Instances.TokenSeparators.NamespaceTokenSeparator,
                            namespacedTypeName);

                        var (namespaceName, typeName) = Instances.StringOperator.Partition(
                            indexOfLastNamespaceTokenSeparator,
                            namespacedTypeName);

                        output.NamespaceName = namespaceName;
                        output.TypeName = typeName;
                    }
                    else
                    {
                        output.TypeName = namespacedTypeName;
                    }
                }
                else
                {
                    var hasNamespace = this.Has_Namespace(typeSignatureStringValue);
                    if(hasNamespace)
                    {
                        var indexOfLastNamespaceTokenSeparator = Instances.StringOperator.Get_LastIndexOf(
                            Instances.TokenSeparators.NamespaceTokenSeparator,
                            typeSignatureStringValue);

                        var (namespaceName, typeName) = Instances.StringOperator.Partition(
                            indexOfLastNamespaceTokenSeparator,
                            typeSignatureStringValue);

                        output.NamespaceName = namespaceName;
                        output.TypeName = typeName;
                    }
                    else
                    {
                        output.TypeName = typeSignatureStringValue;
                    }
                }
            }

            return output;
        }

        public TypeSignature[] Parse_TypesList(string typesList)
        {
            var typesListValue = this.Get_TypesListValue_FromTypesList(typesList);

            var output = this.Parse_TypesListValue(typesListValue);
            return output;
        }

        public TypeSignature[] Parse_TypesListValue(string typesListValue)
        {
            var listItems = this.Get_ListItems(typesListValue);

            var output = listItems
                .Select(listItem =>
                {
                    var typeSignature = this.Get_TypeSignature(listItem);
                    return typeSignature;
                })
                .Now();

            return output;
        }

        public MethodParameter[] Parse_MethodParameters(string methodParametersListValue)
        {
            var listItems = this.Get_ListItems(methodParametersListValue);

            var output = listItems
                .Select(listItem =>
                {
                    // The parameter name token separator (' ', space) might also be present in the generic types list separator (', ', comma-space).
                    // The parameter name will never have spaces in it, so the last parameter name token separator.
                    var lastIndexOfParameterNameTokenSeparator = Instances.StringOperator.Get_LastIndexOf(
                        Instances.TokenSeparators.ParameterNameTokenSeparator,
                        listItem);

                    var (parameterTypeNamespacedTypeName, parameterName) = Instances.StringOperator.Partition(
                        lastIndexOfParameterNameTokenSeparator,
                        listItem);

                    var parameterType = this.Get_TypeSignature(parameterTypeNamespacedTypeName);

                    var output = new MethodParameter
                    {
                        ParameterName = parameterName,
                        ParameterType = parameterType,
                    };

                    return output;
                })
                .Now();

            return output;
        }
    }
}
