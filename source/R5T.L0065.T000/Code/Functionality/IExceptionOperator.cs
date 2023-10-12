using System;

using R5T.T0132;


namespace R5T.L0065.T000
{
    [FunctionalityMarker]
    public partial interface IExceptionOperator : IFunctionalityMarker
    {
        public Exception Get_UnrecognizedSignatureType(Signature signature)
        {
            var signatureTypeName = Instances.TypeNameOperator.Get_TypeNameOf(signature);

            var output = new Exception($"{signatureTypeName}: unrecognized signature type.");
            return output;
        }
    }
}
