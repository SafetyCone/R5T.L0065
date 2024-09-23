using System;

using R5T.T0131;


namespace R5T.L0065.F003
{
    [ValuesMarker]
    public partial interface ITokens : IValuesMarker
    {
        /// <summary>
        /// <para><description>"<value>()</value>", empty set of open and close parentheses.</description></para>
        /// </summary>
        public string EmptyParameterListToken => "()";

        /// <summary>
        /// <para><description>"<value>[]</value>", empty set of open and close brackets.</description></para>
        /// </summary>
        public string ArrayTypeToken => "[]";

        /// <summary>
        /// <para><description>"<value>&amp;</value>", ampersand.</description></para>
        /// </summary>
        public string ReferenceTypeToken => "&";

        /// <summary>
        /// <para><description>"<value>*</value>", asterix.</description></para>
        /// </summary>
        public string PointerTypeToken => "*";
    }
}
