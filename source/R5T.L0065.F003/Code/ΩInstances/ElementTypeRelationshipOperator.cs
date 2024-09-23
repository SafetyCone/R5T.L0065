using System;


namespace R5T.L0065.F003
{
    public class ElementTypeRelationshipOperator : IElementTypeRelationshipOperator
    {
        #region Infrastructure

        public static IElementTypeRelationshipOperator Instance { get; } = new ElementTypeRelationshipOperator();


        private ElementTypeRelationshipOperator()
        {
        }

        #endregion
    }
}
