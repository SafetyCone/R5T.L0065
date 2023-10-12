using System;


namespace R5T.L0065.F001
{
    public static class Instances
    {
        public static L0053.IArrayOperator ArrayOperator => L0053.ArrayOperator.Instance;
        public static L0053.IDictionaryOperator DictionaryOperator => L0053.DictionaryOperator.Instance;
        public static T0221.IElementTypeRelationshipOperator ElementTypeRelationshipOperator => T0221.ElementTypeRelationshipOperator.Instance;
        public static L0053.IEnumerableOperator EnumerableOperator => L0053.EnumerableOperator.Instance;
        public static L0053.IEventInfoOperator EventInfoOperator => L0053.EventInfoOperator.Instance;
        public static L0053.IExceptionOperator ExceptionOperator => L0053.ExceptionOperator.Instance;
        public static L0053.IFieldInfoOperator FieldInfoOperator => L0053.FieldInfoOperator.Instance;
        public static IMemberInfoOperator MemberInfoOperator => F001.MemberInfoOperator.Instance;
        public static L0053.IMethodBaseOperator MethodBaseOperator => L0053.MethodBaseOperator.Instance;
        public static L0053.IMethodInfoOperator MethodInfoOperator => L0053.MethodInfoOperator.Instance;
        public static L0053.IParameterInfoOperator ParameterInfoOperator => L0053.ParameterInfoOperator.Instance;
        public static L0053.IPropertyInfoOperator PropertyInfoOperator => L0053.PropertyInfoOperator.Instance;
        public static L0053.IStringOperator StringOperator => L0053.StringOperator.Instance;
        public static L0053.ITokenSeparators TokenSeparators => L0053.TokenSeparators.Instance;
        public static ITypeOperator TypeOperator => F001.TypeOperator.Instance;
    }
}