using System;


namespace R5T.L0065.T000
{
    public static class Instances
    {
        public static L0053.IArrayOperator ArrayOperator => L0053.ArrayOperator.Instance;
        public static T0221.IElementTypeRelationshipOperator ElementTypeRelationshipOperator => T0221.ElementTypeRelationshipOperator.Instance;
        public static IEventSignatureOperator EventSignatureOperator => T000.EventSignatureOperator.Instance;
        public static IExceptionOperator ExceptionOperator => T000.ExceptionOperator.Instance;
        public static IFieldSignatureOperator FieldSignatureOperator => T000.FieldSignatureOperator.Instance;
        public static L0062.L001.IKindMarkers KindMarkers => L0062.L001.KindMarkers.Instance;
        public static L0053.IMemberInfoOperator MemberInfoOperator => L0053.MemberInfoOperator.Instance;
        public static IMethodParameterOperator MethodParameterOperator => T000.MethodParameterOperator.Instance;
        public static IMethodSignatureOperator MethodSignatureOperator => T000.MethodSignatureOperator.Instance;
        public static IPropertySignatureOperator PropertySignatureOperator => T000.PropertySignatureOperator.Instance;
        public static L0053.INamespacedTypeNameOperator NamespacedTypeNameOperator => L0053.NamespacedTypeNameOperator.Instance;
        public static L0053.INullOperator NullOperator => L0053.NullOperator.Instance;
        public static ISignatureOperator SignatureOperator => T000.SignatureOperator.Instance;
        public static L0066.ITypeNameAffixes TypeNameAffixes => L0066.TypeNameAffixes.Instance;
        public static L0053.ITypeNameOperator TypeNameOperator => L0053.TypeNameOperator.Instance;
        public static L0053.ITypeOperator TypeOperator => L0053.TypeOperator.Instance;
        public static ITypeSignatureOperator TypeSignatureOperator => T000.TypeSignatureOperator.Instance;
    }
}