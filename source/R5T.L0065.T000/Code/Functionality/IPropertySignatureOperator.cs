using System;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface IPropertySignatureOperator : IFunctionalityMarker
    {
        public bool Are_Equal_ByValue(PropertySignature a, PropertySignature b)
        {
            var output = Instances.SignatureOperator.Are_Equal_ByValue_SignatureOnly(a, b,
                (a, b) =>
                {
                    var output = true;

                    output &= a.PropertyName == b.PropertyName;
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.DeclaringType,
                        b.DeclaringType);
                    output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                        a.PropertyType,
                        b.PropertyType);
                    output &= Instances.ArrayOperator.Are_Equal(
                        a.Parameters,
                        b.Parameters,
                        Instances.MethodParameterOperator.Are_Equal_ByValue);

                    return output;
                });

            return output;
        }
    }
}
