using System;


namespace R5T.L0065.F003
{
    public class FullTypeNameOperator : IFullTypeNameOperator
    {
        #region Infrastructure

        public static IFullTypeNameOperator Instance { get; } = new FullTypeNameOperator();


        private FullTypeNameOperator()
        {
        }

        #endregion
    }
}
