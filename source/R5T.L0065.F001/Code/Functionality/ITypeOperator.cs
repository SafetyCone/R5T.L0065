using System;

using R5T.T0132;


namespace R5T.L0065.F001
{
    [FunctionalityMarker]
    public partial interface ITypeOperator : IFunctionalityMarker,
        L0053.ITypeOperator
    {
        private static L0053.ITypeOperator Platform => L0053.TypeOperator.Instance;


        /// <summary>
        /// Gets the simple name of a type (removing the generic parameter count).
        /// </summary>
        public new string Get_Name(Type type)
        {
            var namePossiblyWithTypeParameterCount = Platform.Get_Name(type);

            var hasParameterCount = Instances.StringOperator.Contains(
                namePossiblyWithTypeParameterCount,
                Instances.TokenSeparators.TypeParameterCountSeparator);

            if(hasParameterCount)
            {
                var indexOfTypeParameterCountTokenSeparator = Instances.StringOperator.Get_IndexOf(
                    namePossiblyWithTypeParameterCount,
                    Instances.TokenSeparators.TypeParameterCountSeparator);

                var (output, _) = Instances.StringOperator.Partition_Exclusive(
                    indexOfTypeParameterCountTokenSeparator,
                    namePossiblyWithTypeParameterCount);

                return output;
            }
            else
            {
                return namePossiblyWithTypeParameterCount;
            }
        }
    }
}
