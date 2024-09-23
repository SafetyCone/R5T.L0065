using System;


namespace R5T.L0065.T000
{
    public static class Instances
    {
        public static L0066.IArrayOperator ArrayOperator => L0066.ArrayOperator.Instance;
        public static T0221.IElementTypeRelationshipOperator ElementTypeRelationshipOperator => T0221.ElementTypeRelationshipOperator.Instance;
        public static L0066.IEnumerableOperator EnumerableOperator => L0066.EnumerableOperator.Instance;
        public static IEventSignatureOperator EventSignatureOperator => T000.EventSignatureOperator.Instance;
        public static IExceptionOperator ExceptionOperator => T000.ExceptionOperator.Instance;
        public static IFieldSignatureOperator FieldSignatureOperator => T000.FieldSignatureOperator.Instance;
        public static L0066.IHashCodes HashCodes => L0066.HashCodes.Instance;
        public static L0062.L001.IKindMarkers KindMarkers => L0062.L001.KindMarkers.Instance;
        public static L0066.IMemberInfoOperator MemberInfoOperator => L0066.MemberInfoOperator.Instance;
        public static IMethodParameterOperator MethodParameterOperator => T000.MethodParameterOperator.Instance;
        public static IMethodSignatureOperator MethodSignatureOperator => T000.MethodSignatureOperator.Instance;
        public static IPropertySignatureOperator PropertySignatureOperator => T000.PropertySignatureOperator.Instance;
        public static L0066.INamespacedTypeNameOperator NamespacedTypeNameOperator => L0066.NamespacedTypeNameOperator.Instance;
        public static L0066.INullOperator NullOperator => L0066.NullOperator.Instance;
        public static ISignatureOperator SignatureOperator => T000.SignatureOperator.Instance;
        public static L0066.ITypeNameAffixes TypeNameAffixes => L0066.TypeNameAffixes.Instance;
        public static L0053.ITypeNameOperator TypeNameOperator => L0053.TypeNameOperator.Instance;
        public static L0066.ITypeOperator TypeOperator => L0066.TypeOperator.Instance;
        public static ITypeSignatureOperator TypeSignatureOperator => T000.TypeSignatureOperator.Instance;
    }
}