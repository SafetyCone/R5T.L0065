using System;


namespace R5T.L0065.F003
{
    public class SignatureOperator : ISignatureOperator
    {
        #region Infrastructure

        public static ISignatureOperator Instance { get; } = new SignatureOperator();


        private SignatureOperator()
        {
        }

        #endregion
    }
}
