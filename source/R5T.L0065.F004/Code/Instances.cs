using System;


namespace R5T.L0065.F004
{
    public static class Instances
    {
        public static L0053.IArrayOperator ArrayOperator => L0053.ArrayOperator.Instance;
        public static IExceptionOperator ExceptionOperator => F004.ExceptionOperator.Instance;
        public static L0053.IFlagsOperator FlagsOperator => L0053.FlagsOperator.Instance;
        public static L0053.IIndices Indices => L0053.Indices.Instance;
        public static L0053.IStringOperator StringOperator => L0053.StringOperator.Instance;
        public static L0063.Z000.ITokenSeparators TokenSeparators => L0063.Z000.TokenSeparators.Instance;
        public static L0063.Z000.ITypeNameAffixes TypeNameAffixes => L0063.Z000.TypeNameAffixes.Instance;
        public static T000.ITypeSignatureOperator TypeSignatureOperator => T000.TypeSignatureOperator.Instance;
    }
}