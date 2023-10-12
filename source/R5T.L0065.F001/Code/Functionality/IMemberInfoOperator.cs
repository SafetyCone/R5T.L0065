using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using R5T.L0053;
using R5T.L0053.Extensions;
using R5T.T0132;

using R5T.L0065.T000;
using R5T.L0065.T000.Extensions;


namespace R5T.L0065.F001
{
    [FunctionalityMarker]
    public partial interface IMemberInfoOperator : IFunctionalityMarker,
        L0053.IMemberInfoOperator
    {
        public Signature Get_Signature(MemberInfo memberInfo)
        {
            Signature output = memberInfo switch
            {
                ConstructorInfo constructorInfo => this.Get_MethodSignature_ForConstructor(constructorInfo),
                EventInfo eventInfo => this.Get_EventSignature(eventInfo),
                FieldInfo fieldInfo => this.Get_FieldSignature(fieldInfo),
                MethodInfo methodInfo => this.Get_MethodSignature(methodInfo),
                PropertyInfo propertyInfo => this.Get_PropertySignature(propertyInfo),
                TypeInfo typeInfo => this.Get_TypeSignature(typeInfo),
                _ => throw Instances.ExceptionOperator.Get_UnrecognizedMemberTypeException(memberInfo),
            };

            // Handle at the root.

            return output;
        }

        public EventSignature Get_EventSignature(EventInfo eventInfo)
        {
            var declaringType = Instances.EventInfoOperator.Get_DeclaringType(eventInfo);
            var eventHandlerType = Instances.EventInfoOperator.Get_EventHandlerType(eventInfo);
            var eventName = Instances.EventInfoOperator.Get_EventName(eventInfo);

            var declaringTypeSignature = this.Get_TypeSignature(declaringType);
            var eventHandlerTypeSignature = this.Get_TypeSignature(eventHandlerType);

            var output = new EventSignature
            {
                DeclaringType = declaringTypeSignature,
                EventHandlerType = eventHandlerTypeSignature,
                EventName = eventName,
            }
            .Set_IsObsolete(eventInfo);

            return output;
        }

        public FieldSignature Get_FieldSignature(FieldInfo fieldInfo)
        {
            var declaringType = Instances.FieldInfoOperator.Get_DeclaringType(fieldInfo);
            var fieldType = Instances.FieldInfoOperator.Get_FieldType(fieldInfo);
            var fieldName = Instances.FieldInfoOperator.Get_FieldName(fieldInfo);

            var declaringTypeSignature = this.Get_TypeSignature(declaringType);
            var fieldTypeSignature = this.Get_TypeSignature(fieldType);

            var output = new FieldSignature
            {
                DeclaringType = declaringTypeSignature,
                FieldName = fieldName,
                FieldType = fieldTypeSignature,
            }
            .Set_IsObsolete(fieldInfo);

            return output;
        }

        public MethodSignature Get_MethodSignature(MethodInfo methodInfo)
        {
            // Constructors do not have method generic type parameters, and asking for them causes an exception.
            // So ask for them here so the method for method bases does not have to.
            var methodGenericTypeInputs = Instances.MethodBaseOperator.Get_GenericTypeInputs_OfMethodOnly(methodInfo);

            var output = this.Get_MethodSignature_ForMethodBase(
                methodInfo,
                methodGenericTypeInputs);

            var returnType = Instances.MethodInfoOperator.Get_ReturnType(methodInfo);

            var returnTypeSignature = this.Get_TypeSignature(
                returnType);

            output.ReturnType = returnTypeSignature;

            return output;
        }

        public MethodSignature Get_MethodSignature_ForMethodBase(
            MethodBase methodBase,
            // Will be empty for constructors, parameter must be here since constructors throw an exception when asked for generic type inputs.
            Type[] methodGenericTypeInputs)
        {
            var declaringType = Instances.MethodBaseOperator.Get_DeclaringType(methodBase);
            var methodName = Instances.MethodBaseOperator.Get_MethodName(methodBase);

            var declaringTypeSignature = this.Get_TypeSignature(
                declaringType);

            var parameters = Instances.MethodBaseOperator.Get_Parameters(
                methodBase);

            var methodParameters = this.Get_MethodParameters(
                parameters);

            var genericTypeInputs = methodGenericTypeInputs
                .Select(this.Get_TypeSignature)
                .ToArray();

            var output = new MethodSignature
            {
                DeclaringType = declaringTypeSignature,
                GenericTypeInputs = genericTypeInputs,
                MethodName = methodName,
                Parameters = methodParameters,
                //ReturnType // Handled in caller since method signatures for constructors should be different than for regular methods.
            }
            .Set_IsObsolete(methodBase);

            return output;
        }

        public MethodSignature Get_MethodSignature_ForConstructor(ConstructorInfo constructorInfo)
        {
            // Constructors cannot have generic type inputs.
            var genericTypeInputs = Instances.ArrayOperator.Empty<Type>();

            var output = this.Get_MethodSignature_ForMethodBase(
                constructorInfo,
                genericTypeInputs);

            // For constructors, the return type is the declaring type.
            output.ReturnType = output.DeclaringType;

            return output;
        }

        public PropertySignature Get_PropertySignature(PropertyInfo propertyInfo)
        {
            var declaringType = Instances.PropertyInfoOperator.Get_DeclaringType(propertyInfo);
            var propertyType = Instances.PropertyInfoOperator.Get_PropertyType(propertyInfo);
            var propertyName = Instances.PropertyInfoOperator.Get_PropertyName(propertyInfo);

            var declaringTypeSignature = this.Get_TypeSignature(declaringType);
            var propertyTypeSignature = this.Get_TypeSignature(propertyType);

            // If the property is an indexer, it will have method parameters.
            var parameters = Instances.PropertyInfoOperator.Get_IndexerParameters(propertyInfo);

            var declaringTypeGenericTypeInputSignaturesByName = declaringTypeSignature.GenericTypeInputs
                .Empty_IfNull()
                .ToDictionary(
                    x => x.TypeName);

            // Properties will never have method-level generic types.
            var methodGenericTypeParameterContext = Instances.DictionaryOperator.Empty<string, TypeSignature>();

            var methodParameters = this.Get_MethodParameters(
                parameters);

            var output = new PropertySignature
            {
                DeclaringType = declaringTypeSignature,
                Parameters = methodParameters,
                PropertyName = propertyName,
                PropertyType = propertyTypeSignature,
            }
            .Set_IsObsolete(propertyInfo);

            return output;
        }

        public TypeSignature Get_TypeSignature(TypeInfo typeInfo)
        {
            var output = this.Get_TypeSignature(typeInfo as Type);
            return output;
        }

        public TypeSignature Get_TypeSignature(
            Type type)
        {
            // If the type is a generic type parameter, it must exist within the current generic type parameter context.
            var isGenericParameter = Instances.TypeOperator.Is_GenericParameter(type);
            if(isGenericParameter)
            {
                var genericTypeParameterTypeSignature = this.Get_GenericTypeInputSignature(type);
                return genericTypeParameterTypeSignature;
            }

            var output = new TypeSignature();

            // Is the type a nested type? If so, we consider the parent type of the nested type first.
            var isNested = Instances.TypeOperator.Is_NestedType(type);
            if (isNested)
            {
                output.Is_Nested = true;

                var nestedTypeParentType = Instances.TypeOperator.Get_NestedTypeParentType(type);

                // Ordinarily, generic type signature values (like whether a generic type parameter is a method parameter or type parameter)
                // can just be read from member objects, but for generic parent types of nested types, the generic type parameters don't flow through.
                // Instead you can just construct the generic type with the type parameters from the nested type.
                var nestedTypeParentIsGenericTypeDefintion = Instances.TypeOperator.Is_GenericTypeDefinition(nestedTypeParentType);
                if(nestedTypeParentIsGenericTypeDefintion)
                {
                    var nestedTypeGenericTypeInputs = Instances.TypeOperator.Get_GenericTypeInputs_OfType(type);

                    var nestedTypeParentTypeGenericTypeInputs = Instances.TypeOperator.Get_GenericTypeInputs_OfType(nestedTypeParentType);

                    // Make a type.
                    var nestedTypeParentTypeGenericInputCount = nestedTypeParentTypeGenericTypeInputs.Length;

                    var nestedTypeGenericTypeArgumentsForParent = nestedTypeGenericTypeInputs.Take(nestedTypeParentTypeGenericInputCount).ToArray();

                    var constructedNestedTypeParentType = nestedTypeParentType.MakeGenericType(nestedTypeGenericTypeArgumentsForParent);

                    var nestedTypeParentTypeSignature = this.Get_TypeSignature(
                        constructedNestedTypeParentType);

                    output.NestedTypeParent = nestedTypeParentTypeSignature;
                }
                else
                {
                    var nestedTypeParentTypeSignature = this.Get_TypeSignature(
                        nestedTypeParentType);

                    output.NestedTypeParent = nestedTypeParentTypeSignature;
                }
            }
            else
            {
                // Else, not a nested type.
                // Handle types with element types.
                var hasElementType = Instances.TypeOperator.Has_ElementType(type);
                if (hasElementType)
                {
                    output.Has_ElementType = hasElementType;

                    output.ElementTypeRelationships = Instances.ElementTypeRelationshipOperator.Get_ElementTypeRelationship(type);

                    var elementType = Instances.TypeOperator.Get_ElementType(type);

                    output.ElementType = this.Get_TypeSignature(
                        elementType);

                    return output;
                }

                output.NamespaceName = Instances.TypeOperator.Get_NamespaceName(type);
            }

            output.TypeName = Instances.TypeOperator.Get_Name(type);

            var genericTypeInputs = Instances.TypeOperator.Get_GenericTypeInputs_NotInParents(type);

            var genericTypeInputSignatures = genericTypeInputs
                .Select(this.Get_TypeSignature)
                .Now()
                .Null_IfEmpty();

            output.GenericTypeInputs = genericTypeInputSignatures;

            return output;
        }

        public TypeSignature[] Get_GenericTypeInputTypeSignatures(
            IEnumerable<Type> genericTypeInputs)
        {
            var output = genericTypeInputs
                .Select(this.Get_TypeSignature)
                .Now();

            return output;
        }

        public MethodParameter[] Get_MethodParameters(
            IEnumerable<ParameterInfo> parameters)
        {
            var output = parameters
                .Select(this.Get_MethodParameter)
                .Now();

            return output;
        }

        public MethodParameter Get_MethodParameter(
            ParameterInfo parameterInfo)
        {
            var parameterType = Instances.ParameterInfoOperator.Get_ParameterType(parameterInfo);
            var parameterName = Instances.ParameterInfoOperator.Get_ParameterName(parameterInfo);

            var parameterTypeSignature = this.Get_TypeSignature(
                parameterType);

            var output = new MethodParameter
            {
                ParameterName = parameterName,
                ParameterType = parameterTypeSignature,
            };

            return output;
        }

        public Dictionary<string, TypeSignature> Get_GenericTypeInputSignaturesByName(IEnumerable<Type> typeGenericTypeInputs)
        {
            var genericTypeInputs = this.Get_GenericTypeInputSignatures(typeGenericTypeInputs);

            var output = genericTypeInputs.ToDictionary(
                genericTypeInput => genericTypeInput.TypeName);

            return output;
        }

        public TypeSignature[] Get_GenericTypeInputSignatures(IEnumerable<Type> typeGenericTypeInputs)
        {
            var output = typeGenericTypeInputs
                .Distinct(NameBasedTypeEqualityComparer.Instance)
                .Select(this.Get_GenericTypeInputSignature)
                .Now();

            return output;
        }

        public TypeSignature Get_GenericTypeInputSignature(Type type)
        {
            var genericTypeName = Instances.TypeOperator.Get_GenericTypeParameterTypeName_ActualName(type);

            var isGenericTypeParameter = Instances.TypeOperator.Is_GenericTypeParameter(type);
            var isGenericMethodParameter = Instances.TypeOperator.Is_GenericMethodParamter(type);

            var output = new TypeSignature
            {
                Is_GenericMethodParameter = isGenericMethodParameter,
                Is_GenericTypeParameter = isGenericTypeParameter,
                TypeName = genericTypeName,
            };

            return output;
        }
    }
}
