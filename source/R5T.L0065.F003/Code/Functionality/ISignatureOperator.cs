using System;
using System.Linq;

using R5T.L0053.Extensions;
using R5T.L0063.T000;
using R5T.L0063.T000.Extensions;
using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F003
{
    /// <summary>
    /// Getting signature strings from signature instances.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker
    {
        public ISignatureString Get_SignatureString(Signature signature)
        {
            var signatureStringValueWithoutObsolete = signature switch
            {
                EventSignature eventSignature => this.Get_SignatureString_ForEvent(eventSignature),
                FieldSignature fieldSignature => this.Get_SignatureString_ForField(fieldSignature),
                PropertySignature propertySignature => this.Get_SignatureString_ForProperty(propertySignature),
                MethodSignature methodSignature => this.Get_SignatureString_ForMethod(methodSignature),
                TypeSignature typeSignature => this.Get_SignatureString_ForType(typeSignature),
                _ => throw Instances.ExceptionOperator.Get_UnrecognizedSignatureType(signature)
            };

            var signatureStringValue = signature.IsObsolete
                ? Instances.SignatureStringOperator.Append_ObsoleteToken(signatureStringValueWithoutObsolete)
                : signatureStringValueWithoutObsolete
                ;

            var output = signatureStringValue.ToSignatureString();
            return output;
        }

        public string Get_FullTypeName(TypeSignature typeSignature)
            => Instances.FullTypeNameOperator.Get_FullTypeName(typeSignature);

        public string Get_SimpleTypeName(TypeSignature typeSignature)
            => Instances.FullTypeNameOperator.Get_SimpleTypeName(typeSignature);

        public string Get_SignatureStringValue(Signature signature)
        {
            var signatureString = this.Get_SignatureString(signature);

            var output = Instances.KindMarkerOperator.Remove_TypeKindMarker(signatureString.Value);
            return output;
        }

        public string Get_SignatureString_ForEvent(EventSignature eventSignature)
        {
            var declaringTypeName = this.Get_FullTypeName(eventSignature.DeclaringType);
            var eventHandlerTypeName = this.Get_FullTypeName(eventSignature.EventHandlerType);

            var eventName = Instances.SignatureStringOperator.Modify_MemberName_ForSignatureString(eventSignature.EventName);

            var eventSignatureString = Instances.SignatureStringOperator.Combine(
                declaringTypeName,
                eventName);

            eventSignatureString = Instances.SignatureStringOperator.Append_OutputType(
                eventSignatureString,
                eventHandlerTypeName);

            var output = Instances.SignatureStringOperator.Get_EventSignatureString(eventSignatureString);
            return output;
        }

        public string Get_SignatureString_ForField(FieldSignature fieldSignature)
        {
            var declaringTypeName = this.Get_FullTypeName(fieldSignature.DeclaringType);
            var fieldTypeName = this.Get_FullTypeName(fieldSignature.FieldType);

            var fieldName = Instances.SignatureStringOperator.Modify_MemberName_ForSignatureString(fieldSignature.FieldName);

            var fieldSignatureString = Instances.SignatureStringOperator.Combine(
                declaringTypeName,
                fieldName);

            fieldSignatureString = Instances.SignatureStringOperator.Append_OutputType(
                fieldSignatureString,
                fieldTypeName);

            var output = Instances.SignatureStringOperator.Get_FieldSignatureString(fieldSignatureString);
            return output;
        }

        public string Get_SignatureString_ForMethod(MethodSignature methodSignature)
        {
            var declaringTypeName = this.Get_FullTypeName(methodSignature.DeclaringType);
            var returnTypeName = this.Get_FullTypeName(methodSignature.ReturnType);

            var methodName = Instances.SignatureStringOperator.Modify_MemberName_ForSignatureString(methodSignature.MethodName);

            var methodNameSignatureString = Instances.SignatureStringOperator.Combine(
                declaringTypeName,
                methodName);

            var genericTypeInputsList = this.Get_GenericTypeInputsList(methodSignature.GenericTypeInputs);

            methodNameSignatureString = Instances.SignatureStringOperator.Append_TypeParameterList(
                methodNameSignatureString,
                genericTypeInputsList);

            var parametersList = this.Get_ParametersList_ForMethod(methodSignature);

            methodNameSignatureString = Instances.SignatureStringOperator.Append_ParameterList(
                methodNameSignatureString,
                parametersList);

            methodNameSignatureString = Instances.SignatureStringOperator.Append_OutputType(
                methodNameSignatureString,
                returnTypeName);

            var output = Instances.SignatureStringOperator.Get_MethodSignatureString(methodNameSignatureString);
            return output;
        }

        public string Get_SignatureString_ForProperty(PropertySignature propertySignature)
        {
            var declaringTypeName = this.Get_FullTypeName(propertySignature.DeclaringType);
            var propertyTypeName = this.Get_FullTypeName(propertySignature.PropertyType);

            var propertyName = Instances.SignatureStringOperator.Modify_MemberName_ForSignatureString(propertySignature.PropertyName);

            var propertyNameSignatureString = Instances.SignatureStringOperator.Combine(
                declaringTypeName,
                propertyName);

            var parametersList = this.Get_ParametersList_ForProperty(propertySignature);

            propertyNameSignatureString = Instances.SignatureStringOperator.Append_ParameterList(
                propertyNameSignatureString,
                parametersList);

            propertyNameSignatureString = Instances.SignatureStringOperator.Append_OutputType(
                propertyNameSignatureString,
                propertyTypeName);

            var output = Instances.SignatureStringOperator.Get_PropertySignatureString(propertyNameSignatureString);
            return output;
        }

        public string Get_SignatureString_ForType(TypeSignature typeSignature)
        {
            var typeName = this.Get_FullTypeName(typeSignature);
            
            // No adjustment necessary.
            var typeSignatureStringValue = typeName;

            var output = Instances.SignatureStringOperator.Get_TypeSignatureString(typeSignatureStringValue);
            return output;
        }

        public string Get_ParametersList(
            MethodParameter[] methodParameters,
            string resultIfNullOrEmpty)
        {
            var isNullOrEmpty = Instances.ArrayOperator.Is_NullOrEmpty(methodParameters);
            if(isNullOrEmpty)
            {
                return resultIfNullOrEmpty;
            }

            var parameterListValue = methodParameters
               .Select(this.Get_ParameterToken)
               .Join(Instances.TokenSeparators.ArgumentListSeparator);

            var output = parameterListValue.Wrap(
                Instances.TokenSeparators.ParameterListOpenTokenSeparator,
                Instances.TokenSeparators.ParameterListCloseTokenSeparator);

            return output;
        }

        public string Get_ParameterToken(MethodParameter parameter)
        {
            var parameterTypeIdentityString = this.Get_FullTypeName(parameter.ParameterType);

            var output = Instances.SignatureStringOperator.Append_ParameterName(
                parameterTypeIdentityString,
                parameter.ParameterName);

            return output;
        }

        /// <summary>
        /// For methods, if there are no parameters, there is still a parameter list open-close parenthesis pair.
        /// </summary>
        public string Get_ParametersList_ForMethod(MethodSignature methodSignature)
        {
            var output = this.Get_ParametersList(
                methodSignature.Parameters,
                // For methods, if there are no parameters, there is still a parameter list open-close parenthesis pair.
                Instances.Tokens.EmptyParameterListToken);

            return output;
        }

        /// <summary>
        /// For properties, if there are no parameters, there is no parameter list open-close parenthesis pair.
        /// </summary>
        public string Get_ParametersList_ForProperty(PropertySignature propertySignature)
        {
            var output = this.Get_ParametersList(
                propertySignature.Parameters,
                // For properties, if there are no parameters, there is no parameter list open-close parenthesis pair.
                Instances.Strings.Empty);

            return output;
        }

        /// <summary>
        /// If there are no generic type inputs, the empty string is returned.
        /// </summary>
        public string Get_GenericTypeInputsList(TypeSignature[] genericTypeInputs)
        {
            var isNullOrEmpty = Instances.ArrayOperator.Is_NullOrEmpty(genericTypeInputs);
            if (isNullOrEmpty)
            {
                return String.Empty;
            }

            var output = genericTypeInputs
                .Select(genericTypeInput => Instances.FullTypeNameOperator.Get_FullTypeName(genericTypeInput))
                .Join(Instances.TokenSeparators.ArgumentListSeparator)
                .Wrap(
                    Instances.TokenSeparators.TypeArgumentListOpenTokenSeparator,
                    Instances.TokenSeparators.TypeArgumentListCloseTokenSeparator);

            return output;
        }
    }
}
