using System;


namespace R5T.L0065.T000
{
    public class EventSignatureOperator : IEventSignatureOperator
    {
        #region Infrastructure

        public static IEventSignatureOperator Instance { get; } = new EventSignatureOperator();


        private EventSignatureOperator()
        {
        }

        #endregion
    }
}
