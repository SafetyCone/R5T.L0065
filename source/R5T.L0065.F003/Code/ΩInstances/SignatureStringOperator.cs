using System;


namespace R5T.L0065.F003
{
    public class SignatureStringOperator : ISignatureStringOperator
    {
        #region Infrastructure

        public static ISignatureStringOperator Instance { get; } = new SignatureStringOperator();


        private SignatureStringOperator()
        {
        }

        #endregion
    }
}
