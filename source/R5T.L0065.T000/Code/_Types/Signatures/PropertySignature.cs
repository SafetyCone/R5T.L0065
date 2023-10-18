using System;

using R5T.T0142;


namespace R5T.L0065.T000
{
    /// <summary>
    /// Signature structure for representing properties.
    /// </summary>
    [DataTypeMarker]
    public class PropertySignature : Signature,
        IHasDeclaringType,
        IHasParameters
    {
        public TypeSignature DeclaringType { get; set; }
        public string PropertyName { get; set; }
        public TypeSignature PropertyType { get; set; }

        /// <summary>
        /// If the property is an indexer, it will have input parameters.
        /// </summary>
        public MethodParameter[] Parameters { get; set; }


        public PropertySignature()
        {
            this.KindMarker = Instances.KindMarkers.Property;
        }

        public override string ToString()
        {
            var output = Instances.PropertySignatureOperator.ToString(this);
            return output;
        }
    }
}
