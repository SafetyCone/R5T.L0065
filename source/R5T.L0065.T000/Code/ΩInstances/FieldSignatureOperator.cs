using System;


namespace R5T.L0065.T000
{
    public class FieldSignatureOperator : IFieldSignatureOperator
    {
        #region Infrastructure

        public static IFieldSignatureOperator Instance { get; } = new FieldSignatureOperator();


        private FieldSignatureOperator()
        {
        }

        #endregion
    }
}
