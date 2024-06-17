using System;

using R5T.L0063.T000;
using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F003
{
    /// <summary>
    /// Gets signature string values from <see cref="Signature"/> instances.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISignatureStringOperator : IFunctionalityMarker,
        L0063.F000.ISignatureStringOperator
    {
        public ISignatureString Get_SignatureString(Signature signature)
        {
            var output = Instances.SignatureOperator.Get_SignatureString(signature);
            return output;
        }
    }
}
