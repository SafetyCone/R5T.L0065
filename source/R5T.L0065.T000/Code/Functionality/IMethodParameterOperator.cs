using System;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface IMethodParameterOperator : IFunctionalityMarker
    {
        public bool Are_Equal_ByValue(MethodParameter a, MethodParameter b)
        {
            var output = true;

            output &= a.ParameterName == b.ParameterName;
            output &= Instances.TypeSignatureOperator.Are_Equal_ByValue(
                a.ParameterType,
                b.ParameterType);

            return output;
        }

        public string ToString(MethodParameter methodParameter)
        {
            var parameterTypeName = Instances.TypeSignatureOperator.ToString(methodParameter.ParameterType);

            var output = $"{methodParameter.ParameterName}: {parameterTypeName}";
            return output;
        }
    }
}
