using System;


namespace R5T.L0065.F002
{
    public static class Instances
    {
        public static L0053.IArrayOperator ArrayOperator => L0053.ArrayOperator.Instance;
        public static L0053.IDictionaryOperator DictionaryOperator => L0053.DictionaryOperator.Instance;
        public static T0221.IElementTypeRelationshipOperator ElementTypeRelationshipOperator => T0221.ElementTypeRelationshipOperator.Instance;
        public static T000.IExceptionOperator ExceptionOperator => T000.ExceptionOperator.Instance;
        public static L0062.F000.IIdentityStringOperator IdentityStringOperator => L0062.F000.IdentityStringOperator.Instance;
        public static L0053.IMethodNameOperator MethodNameOperator => L0053.MethodNameOperator.Instance;
        public static INamespacedTypeNameOperator NamespacedTypeNameOperator => F002.NamespacedTypeNameOperator.Instance;
        public static L0053.INullOperator NullOperator => L0053.NullOperator.Instance;
        public static ISignatureOperator SignatureOperator => F002.SignatureOperator.Instance;
        public static ISignatureStringOperator SignatureStringOperator => F002.SignatureStringOperator.Instance;
        public static Z0000.IStrings Strings => Z0000.Strings.Instance;
        public static L0062.F000.ITokenSeparators TokenSeparators => L0062.F000.TokenSeparators.Instance;
        public static L0062.F000.ITypeNameAffixes TypeNameAffixes => L0062.F000.TypeNameAffixes.Instance;
        public static ITypeNameOperator TypeNameOperator => F002.TypeNameOperator.Instance;
        public static L0053.ITypeOperator TypeOperator => L0053.TypeOperator.Instance;
        public static T000.ITypeSignatureOperator TypeSignatureOperator => T000.TypeSignatureOperator.Instance;
    }
}