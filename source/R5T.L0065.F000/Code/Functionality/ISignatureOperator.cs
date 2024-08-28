using System;

using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F000
{
    /// <summary>
    /// Common <see cref="T000.Signature"/>-related operations.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker
    {
        public string Get_NamespacedTypeName(TypeSignature typeSignature)
        {
            var output = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                typeSignature.NamespaceName,
                typeSignature.TypeName);

            return output;
        }
    }
}
