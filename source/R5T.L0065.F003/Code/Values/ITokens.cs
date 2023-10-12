using System;

using R5T.T0131;


namespace R5T.L0065.F003
{
    [ValuesMarker]
    public partial interface ITokens : IValuesMarker
    {
        /// <summary>
        /// <para>"()", empty set of open and close parentheses.</para>
        /// </summary>
        public string EmptyParameterListToken => "()";
    }
}
