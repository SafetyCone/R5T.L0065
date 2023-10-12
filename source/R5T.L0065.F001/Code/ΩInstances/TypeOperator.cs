using System;


namespace R5T.L0065.F001
{
    public class TypeOperator : ITypeOperator
    {
        #region Infrastructure

        public static ITypeOperator Instance { get; } = new TypeOperator();


        private TypeOperator()
        {
        }

        #endregion
    }
}
