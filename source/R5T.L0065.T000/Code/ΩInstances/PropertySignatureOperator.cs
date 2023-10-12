using System;


namespace R5T.L0065.T000
{
    public class PropertySignatureOperator : IPropertySignatureOperator
    {
        #region Infrastructure

        public static IPropertySignatureOperator Instance { get; } = new PropertySignatureOperator();


        private PropertySignatureOperator()
        {
        }

        #endregion
    }
}
