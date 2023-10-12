using System;


namespace R5T.L0065.T000
{
    public class MethodSignatureOperator : IMethodSignatureOperator
    {
        #region Infrastructure

        public static IMethodSignatureOperator Instance { get; } = new MethodSignatureOperator();


        private MethodSignatureOperator()
        {
        }

        #endregion
    }
}
