using System;

using R5T.T0142;


namespace R5T.L0065.T000
{
    [DataTypeMarker]
    public class EventSignature : Signature, IEquatable<EventSignature>
    {
        #region Static

        public static EventSignature Constructor()
        {
            var output = new EventSignature();
            return output;
        }

        #endregion


        public TypeSignature DeclaringType { get; set; }
        public string EventName { get; set; }
        public TypeSignature EventHandlerType { get; set; }


        public EventSignature()
        {
            this.KindMarker = Instances.KindMarkers.Event;
        }

        public override string ToString()
        {
            var declaringTypeName = this.DeclaringType.ToString();

            var typeNamedEventName = Instances.NamespacedTypeNameOperator.Combine(
                declaringTypeName,
                this.EventName);

            var eventHandlerTypeName = this.EventHandlerType.ToString();

            var output = Instances.TypeNameOperator.Append_OutputTypeName(
                typeNamedEventName,
                eventHandlerTypeName);

            return output;
        }

        public bool Equals(EventSignature other)
        {
            var output = Instances.EventSignatureOperator.Are_Equal_ByValue(
                this,
                other);

            return output;
        }
    }
}
