using System;

using R5T.T0142;


namespace R5T.L0065.T000
{
    /// <summary>
    /// A signature structure for representing event members.
    /// </summary>
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
            var output = Instances.EventSignatureOperator.ToString(this);
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
