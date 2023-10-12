using System;
using System.Reflection;

using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F001
{
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker
    {
        public Signature Get_Signature(MemberInfo memberInfo)
        {
            var output = Instances.MemberInfoOperator.Get_Signature(memberInfo);
            return output;
        }
    }
}
