using System;


namespace R5T.L0065.F003
{
    public static class Instances
    {
        public static L0053.IArrayOperator ArrayOperator => L0053.ArrayOperator.Instance;
        public static T0221.IElementTypeRelationshipOperator ElementTypeRelationshipOperator => T0221.ElementTypeRelationshipOperator.Instance;
        public static T000.IExceptionOperator ExceptionOperator => T000.ExceptionOperator.Instance;
        public static IFullTypeNameOperator FullTypeNameOperator => F003.FullTypeNameOperator.Instance;
        public static L0062.L001.IKindMarkerOperator KindMarkerOperator => L0062.L001.KindMarkerOperator.Instance;
        public static L0063.F000.IMemberNameOperator MemberNameOperator => L0063.F000.MemberNameOperator.Instance;
        public static ISignatureOperator SignatureOperator => F003.SignatureOperator.Instance;
        public static ISignatureStringOperator SignatureStringOperator => F003.SignatureStringOperator.Instance;
        public static L0053.IStringOperator StringOperator => L0053.StringOperator.Instance;
        public static Z0000.IStrings Strings => Z0000.Strings.Instance;
        public static ITokens Tokens => F003.Tokens.Instance;
        public static L0063.Z000.ITokenSeparators TokenSeparators => L0063.Z000.TokenSeparators.Instance;
        public static L0063.Z000.ITypeNameAffixes TypeNameAffixes => L0063.Z000.TypeNameAffixes.Instance;
        public static L0063.F000.ITypeNameOperator TypeNameOperator => L0063.F000.TypeNameOperator.Instance;
    }
}