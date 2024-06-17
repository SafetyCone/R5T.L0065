using System;
using System.Collections.Generic;
using System.Linq;

using R5T.L0053.Extensions;
using R5T.L0062.T000;
using R5T.L0062.T000.Extensions;
using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F002
{
    /// <summary>
    /// Identity string generation from <see cref="Signature"/> instances.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker
    {
        public IIdentityString Get_IdentityString(Signature signature)
        {
            var identityStringValue = signature switch
            {
                EventSignature eventSignature => this.Get_IdentityString_ForEvent(eventSignature),
                FieldSignature fieldSignature => this.Get_IdentityString_ForField(fieldSignature),
                PropertySignature propertySignature => this.Get_IdentityString_ForProperty(propertySignature),
                MethodSignature methodSignature => this.Get_IdentityString_ForMethod(methodSignature),
                TypeSignature typeSignature => this.Get_IdentityString_ForType(typeSignature),
                _ => throw Instances.ExceptionOperator.Get_UnrecognizedSignatureType(signature)
            };

            var output = identityStringValue.ToIdentityString();
            return output;
        }

        public string Get_IdentityString_ForEvent(EventSignature eventSignature)
        {
            var declaringTypeIdentityStringValue = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                eventSignature.DeclaringType,
                false);

            var eventName = Instances.IdentityStringOperator.Modify_MemberName(eventSignature.EventName);

            var eventIdentityString = Instances.SignatureStringOperator.Combine(
                declaringTypeIdentityStringValue,
                eventName);

            var output = Instances.IdentityStringOperator.Get_EventIdentityString(eventIdentityString);
            return output;
        }

        public string Get_IdentityString_ForField(FieldSignature fieldSignature)
        {
            var declaringTypeIdentityStringValue = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                fieldSignature.DeclaringType,
                false);

            var fieldName = Instances.IdentityStringOperator.Modify_MemberName(fieldSignature.FieldName);

            var fieldIdentityString = Instances.SignatureStringOperator.Combine(
                declaringTypeIdentityStringValue,
                fieldName);

            var output = Instances.IdentityStringOperator.Get_FieldIdentityString(fieldIdentityString);
            return output;
        }

        public string Get_IdentityString_ForMethod(MethodSignature methodSignature)
        {
            var declaringTypeIdentityStringValue = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                methodSignature.DeclaringType,
                false);

            var methodName = Instances.IdentityStringOperator.Modify_MemberName(methodSignature.MethodName);

            var methodNameIdentityString = Instances.SignatureStringOperator.Combine(
                declaringTypeIdentityStringValue,
                methodName);

            var genericTypeInputsList = Instances.SignatureStringOperator.Get_GenericMethodTypeParameterCountToken(methodSignature.GenericTypeInputs);

            methodNameIdentityString = Instances.SignatureStringOperator.Append_TypeParameterList(
                methodNameIdentityString,
                genericTypeInputsList);

            var isConversionOperator = Instances.MethodNameOperator.Is_ConversionOperator(methodName);

            var parametersList = this.Get_ParametersList(
                methodSignature,
                methodSignature.GenericTypeInputs,
                isConversionOperator);

            methodNameIdentityString = Instances.SignatureStringOperator.Append_ParameterList(
                methodNameIdentityString,
                parametersList);

            // Special handling for explicit and implicit conversion operators.
            if(isConversionOperator)
            {
                var genericTypeParameterPositionsByName = this.Get_GenericTypeParameterPositionsByName(methodSignature.DeclaringType);

                var methodGenericTypeParameterPositionsByName = this.Get_MethodGenericTypeParameterPositionsByName(methodSignature.GenericTypeInputs);

                var outputTypeName = this.Get_ParameterTokenTypeSignatureString(
                    methodSignature.ReturnType,
                    genericTypeParameterPositionsByName,
                    methodGenericTypeParameterPositionsByName,
                    true);

                methodNameIdentityString = Instances.SignatureStringOperator.Append_OutputType(
                    methodNameIdentityString,
                    outputTypeName);
            }

            var output = Instances.IdentityStringOperator.Get_MethodIdentityString(methodNameIdentityString);
            return output;
        }

        public string Get_IdentityString_ForProperty(PropertySignature propertySignature)
        {
            var declaringTypeIdentityStringValue = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                propertySignature.DeclaringType,
                false);

            var propertyName = Instances.IdentityStringOperator.Modify_MemberName(propertySignature.PropertyName);

            var propertyNameIdentityString = Instances.SignatureStringOperator.Combine(
                declaringTypeIdentityStringValue,
                propertyName);

            // A property can never have generic type inputs.
            var methodGenericTypeInputs = Instances.ArrayOperator.Empty<TypeSignature>();

            var parametersList = this.Get_ParametersList(
                propertySignature,
                methodGenericTypeInputs,
                // A property can never be a conversion operator.
                false);

            propertyNameIdentityString = Instances.SignatureStringOperator.Append_ParameterList(
                propertyNameIdentityString,
                parametersList);

            var output = Instances.IdentityStringOperator.Get_PropertyIdentityString(propertyNameIdentityString);
            return output;
        }

        public string Get_IdentityString_ForType(TypeSignature typeSignature)
        {
            string typeIdentityStringValue = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                typeSignature,
                false);

            var output = Instances.IdentityStringOperator.Get_TypeIdentityString(typeIdentityStringValue);
            return output;
        }

        public Dictionary<string, int> Get_GenericTypeParameterPositionsByName(TypeSignature typeSignature)
        {
            var typeGenericTypeInputs = Instances.TypeSignatureOperator.Get_NestedTypeGenericTypeInputs(typeSignature);

            var typeIndex = 0;
            var output = typeGenericTypeInputs
                .ToDictionary(
                    x => x.TypeName,
                    x => typeIndex++);

            return output;
        }

        public Dictionary<string, int> Get_MethodGenericTypeParameterPositionsByName(TypeSignature[] genericTypeInputs)
        {
            var methodGenericTypeInputs = Instances.NullOperator.Is_Null(genericTypeInputs)
                ? Instances.ArrayOperator.Empty<TypeSignature>()
                : genericTypeInputs
                ;

            var methodIndex = 0;
            var output = methodGenericTypeInputs
                .ToDictionary(
                    x => x.TypeName,
                    x => methodIndex++);

            return output;
        }

        public string Get_ParametersList<TMethodBase>(
            TMethodBase methodSignature,
            TypeSignature[] genericTypeInputs,
            // There is special parameter list generic type parameter type name logic for implicit and explicit conversion operators.
            bool isConversionOperator)
            where TMethodBase : IHasDeclaringType, IHasParameters
        {
            var parameters = methodSignature.Parameters;

            var isNull = parameters == default;
            if (isNull)
            {
                return String.Empty;
            }

            var hasParameter = parameters.Any();
            if (!hasParameter)
            {
                return String.Empty;
            }

            var genericTypeParameterPositionsByName = this.Get_GenericTypeParameterPositionsByName(methodSignature.DeclaringType);

            var methodGenericTypeParameterPositionsByName = this.Get_MethodGenericTypeParameterPositionsByName(genericTypeInputs);

            var output = parameters
                .Select(parameter => this.Get_ParameterToken(
                    parameter,
                    genericTypeParameterPositionsByName,
                    methodGenericTypeParameterPositionsByName,
                    isConversionOperator))
                .Join(Instances.TokenSeparators.ArgumentListSeparator)
                .Wrap(
                    Instances.TokenSeparators.ParameterListOpenTokenSeparator,
                    Instances.TokenSeparators.ParameterListCloseTokenSeparator);

            return output;
        }

        public string Get_ParameterTokenTypeSignatureString(
            TypeSignature parameterTypeSignature,
            IDictionary<string, int> genericTypeParameterPositionsByName,
            IDictionary<string, int> methodGenericTypeParameterPositionsByName,
            bool isConversionOperator)
        {
            if (parameterTypeSignature.Has_ElementType)
            {
                var output = this.Get_ParameterTokenTypeSignatureString(
                    parameterTypeSignature.ElementType,
                    genericTypeParameterPositionsByName,
                    methodGenericTypeParameterPositionsByName,
                    isConversionOperator);

                output = Instances.IdentityStringOperator.Append_ElementTypeRelationshipMarkers(
                    output,
                    parameterTypeSignature.ElementTypeRelationships);

                return output;
            }

            if (parameterTypeSignature.Is_GenericTypeParameter)
            {
                // If the generic type parameter is in a conversion operator, special type name rules apply.
                if (isConversionOperator)
                {
                    var output = parameterTypeSignature.TypeName;
                    return output;
                }
                else
                {
                    var index = genericTypeParameterPositionsByName[parameterTypeSignature.TypeName];

                    var output = Instances.TypeNameOperator.Get_PositionalTypeName_ForGenericTypeParameter(index);
                    return output;
                }
            }
            else if (parameterTypeSignature.Is_GenericMethodParameter)
            {
                // No need to consider whether the method generic type parameter is in a conversion operator, since that can never happen.

                var index = methodGenericTypeParameterPositionsByName[parameterTypeSignature.TypeName];

                var output = Instances.TypeNameOperator.Get_PositionalTypeName_ForGenericMethodParameter(index);
                return output;
            }
            else
            {
                var parameterTypeIdentityStringValue = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                    parameterTypeSignature,
                    genericTypeParameterPositionsByName,
                    methodGenericTypeParameterPositionsByName,
                    true);

                // For identity names, the parameter name is not included.
                var output = parameterTypeIdentityStringValue;
                return output;
            }
        }

        public string Get_ParameterToken(
            MethodParameter parameter,
            IDictionary<string, int> genericTypeParameterPositionsByName,
            IDictionary<string, int> methodGenericTypeParameterPositionsByName,
            bool isConversionOperator)
        {
            // Only the type matters for identity strings.
            var output = this.Get_ParameterTokenTypeSignatureString(
                parameter.ParameterType,
                genericTypeParameterPositionsByName,
                methodGenericTypeParameterPositionsByName,
                isConversionOperator);

            return output;
        }
    }
}
