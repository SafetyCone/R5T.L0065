using System;
using System.Reflection;

using R5T.L0063.T000;
using R5T.T0132;


namespace R5T.L0065
{
    [FunctionalityMarker]
    public partial interface ISignatureStringOperator : IFunctionalityMarker,
        F003.ISignatureStringOperator,
        F004.ISignatureStringOperator
    {
        public ISignatureString Get_SignatureString(MemberInfo member)
        {
            var signature = Instances.SignatureOperator.Get_Signature(member);

            var output = this.Get_SignatureString(signature);
            return output;
        }
    }
}
