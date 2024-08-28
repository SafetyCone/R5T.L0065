using System;

using R5T.T0132;


namespace R5T.L0065
{
    [FunctionalityMarker]
    public partial interface ISignatureOperator : IFunctionalityMarker,
        F000.ISignatureOperator,
        F001.ISignatureOperator,
        F002.ISignatureOperator,
        F003.ISignatureOperator,
        F004.ISignatureOperator,
        T000.ISignatureOperator
    {
        
    }
}
