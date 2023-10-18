using System;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface IEventSignatureOperator : IFunctionalityMarker
    {
        public bool Are_Equal_ByValue(EventSignature a, EventSignature b)
        {
            var output = Instances.SignatureOperator.Are_Equal_ByValue_SignatureOnly(a, b,
                (a, b) =>
                {
                    var output = true;

                    output &= a.EventName == b.EventName;
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.DeclaringType,
                        b.DeclaringType);
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.EventHandlerType,
                        b.EventHandlerType);

                    return output;
                });

            return output;
        }

        public string ToString(EventSignature eventSignature)
        {
            var declaringTypeName = eventSignature.DeclaringType.ToString();

            var typeNamedEventName = Instances.NamespacedTypeNameOperator.Combine(
                declaringTypeName,
                eventSignature.EventName);

            var eventHandlerTypeName = eventSignature.EventHandlerType.ToString();

            var output = Instances.TypeNameOperator.Append_OutputTypeName(
                typeNamedEventName,
                eventHandlerTypeName);

            return output;
        }
    }
}
