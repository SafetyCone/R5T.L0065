using System;


namespace R5T.L0065.T000
{
    public class MethodParameterOperator : IMethodParameterOperator
    {
        #region Infrastructure

        public static IMethodParameterOperator Instance { get; } = new MethodParameterOperator();


        private MethodParameterOperator()
        {
        }

        #endregion
    }
}
