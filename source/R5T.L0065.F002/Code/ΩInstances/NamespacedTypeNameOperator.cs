using System;


namespace R5T.L0065.F002
{
    public class NamespacedTypeNameOperator : INamespacedTypeNameOperator
    {
        #region Infrastructure

        public static INamespacedTypeNameOperator Instance { get; } = new NamespacedTypeNameOperator();


        private NamespacedTypeNameOperator()
        {
        }

        #endregion
    }
}
