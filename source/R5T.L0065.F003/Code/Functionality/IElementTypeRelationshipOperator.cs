using System;

using R5T.T0132;
using R5T.T0221;


namespace R5T.L0065.F003
{
    [FunctionalityMarker]
    public partial interface IElementTypeRelationshipOperator : IFunctionalityMarker,
        T0221.IElementTypeRelationshipOperator
    {
        /// <inheritdoc cref="T0221.IElementTypeRelationshipOperator.Append_ElementTypeRelationshipMarkers(string, ElementTypeRelationships, string, string, string)"/>
        public string Append_ElementTypeRelationshipMarkers(
            string typeName,
            ElementTypeRelationships elementTypeRelationships)
            => Instances.ElementTypeRelationshipOperator.Append_ElementTypeRelationshipMarkers(
                typeName,
                elementTypeRelationships,
                Instances.Tokens.ArrayTypeToken,
                Instances.Tokens.ReferenceTypeToken,
                Instances.Tokens.PointerTypeToken);
    }
}
