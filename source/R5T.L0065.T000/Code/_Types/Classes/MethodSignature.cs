using System;

using R5T.T0142;


namespace R5T.L0065.T000
{
    [DataTypeMarker]
    public class MethodSignature : Signature,
        IHasDeclaringType,
        IHasParameters
    {
        public TypeSignature DeclaringType { get; set; }
        public string MethodName { get; set; }
        public TypeSignature ReturnType { get; set; }

        /// <summary>
        /// If the property is an indexer, it will have input parameters.
        /// </summary>
        public MethodParameter[] Parameters { get; set; }

        /// <summary>
        /// Generic type inputs are either 1) parameters or 2) arguments.
        /// If the method is a generic method definition (open generic method without specified generic type arguments), then it will have generic type parameter names.
        /// If the method is a constructed generic method (closed generic method with specified generic type arguments), then it will have generic type arguments.
        /// </summary>
        public TypeSignature[] GenericTypeInputs { get; set; }


        public MethodSignature()
        {
            this.KindMarker = Instances.KindMarkers.Method;
        }
    }
}
