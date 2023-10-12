using System;


namespace R5T.L0065.F001
{
    public class MemberInfoOperator : IMemberInfoOperator
    {
        #region Infrastructure

        public static IMemberInfoOperator Instance { get; } = new MemberInfoOperator();


        private MemberInfoOperator()
        {
        }

        #endregion
    }
}
