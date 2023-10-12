using System;


namespace R5T.L0065.F003
{
    public class Tokens : ITokens
    {
        #region Infrastructure

        public static ITokens Instance { get; } = new Tokens();


        private Tokens()
        {
        }

        #endregion
    }
}
