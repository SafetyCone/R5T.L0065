using System;
using System.Reflection;

using R5T.T0132;

using R5T.L0065.T000;


namespace R5T.L0065
{
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker,
        F001.ISignatureOperator,
        F002.ISignatureOperator
    {
        
    }
}
