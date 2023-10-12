using System;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface IMethodSignatureOperator : IFunctionalityMarker
    {
        public bool Are_Equal_ByValue(MethodSignature a, MethodSignature b)
        {
            var output = Instances.SignatureOperator.Are_Equal_ByValue_SignatureOnly(a, b,
                (a, b) =>
                {
                    var output = true;

                    output &= a.MethodName == b.MethodName;
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.DeclaringType,
                        b.DeclaringType);
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.ReturnType,
                        b.ReturnType);
                    output &= Instances.ArrayOperator.Are_Equal(
                        a.Parameters,
                        b.Parameters,
                        Instances.MethodParameterOperator.Are_Equal_ByValue);
                    output &= Instances.ArrayOperator.Are_Equal(
                        a.GenericTypeInputs,
                        b.GenericTypeInputs,
                        Instances.TypeSignatureOperator.Are_Equal_ByValue);

                    return output;
                });

            return output;
        }
    }
}
