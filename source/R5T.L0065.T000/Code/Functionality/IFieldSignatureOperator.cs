using System;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface IFieldSignatureOperator : IFunctionalityMarker
    {
        public bool Are_Equal_ByValue(FieldSignature a, FieldSignature b)
        {
            var output = Instances.SignatureOperator.Are_Equal_ByValue_SignatureOnly(a, b,
                (a, b) =>
                {
                    var output = true;

                    output &= a.FieldName == b.FieldName;
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.DeclaringType,
                        b.DeclaringType);
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.FieldType,
                        b.FieldType);

                    return output;
                });

            return output;
        }

        public string ToString(FieldSignature fieldSignature)
        {
            var declaringTypeName = fieldSignature.DeclaringType.ToString();

            var typeNamedEventName = Instances.NamespacedTypeNameOperator.Combine(
                declaringTypeName,
                fieldSignature.FieldName);

            var eventHandlerTypeName = fieldSignature.FieldType.ToString();

            var output = Instances.TypeNameOperator.Append_OutputTypeName(
                typeNamedEventName,
                eventHandlerTypeName);

            return output;
        }
    }
}
