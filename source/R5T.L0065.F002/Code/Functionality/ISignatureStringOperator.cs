using System;
using System.Linq;

using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065.F002
{
    [FunctionalityMarker]
    public partial interface ISignatureStringOperator : IFunctionalityMarker,
        L0063.F000.ISignatureStringOperator
    {
        public string Get_GenericParameterCountToken(
            string parameterCountTokenSeparator,
            TypeSignature[] genericTypeInputs = default)
        {
            var isNull = genericTypeInputs == default;
            if (isNull)
            {
                return String.Empty;
            }

            var hasGenericTypeInputs = genericTypeInputs.Any();
            if (!hasGenericTypeInputs)
            {
                return String.Empty;
            }

            var genericTypeParameterCount = genericTypeInputs.Length;

            var output = Instances.IdentityStringOperator.Get_GenericParameterCountToken(
                genericTypeParameterCount,
                parameterCountTokenSeparator);

            return output;
        }

        /// <summary>
        /// Note: can handle the null and empty case (returns an empty string).
        /// </summary>
        public string Get_GenericTypeParameterCountToken(TypeSignature[] genericTypeInputs = default)
        {
            var output = this.Get_GenericParameterCountToken(
                Instances.TokenSeparators._Standard.TypeParameterCountSeparator_String,
                genericTypeInputs);

            return output;
        }

        /// <summary>
        /// Note: can handle the null and empty case (returns an empty string).
        /// </summary>
        public string Get_GenericMethodTypeParameterCountToken(TypeSignature[] genericTypeInputs = default)
        {
            var output = this.Get_GenericParameterCountToken(
                Instances.TokenSeparators._Standard.MethodTypeParameterCountSeparator,
                genericTypeInputs);

            return output;
        }
    }
}
