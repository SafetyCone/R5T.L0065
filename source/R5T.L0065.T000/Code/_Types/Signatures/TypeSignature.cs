using System;

using R5T.T0142;
using R5T.T0221;


namespace R5T.L0065.T000
{
    [DataTypeMarker]
    public class TypeSignature : Signature, IEquatable<TypeSignature>
    {
        public string NamespaceName { get; set; }

        /// <summary>
        /// The simple type name.
        /// </summary>
        public string TypeName { get; set; }

        public bool Is_Nested { get; set; }

        /// <summary>
        /// For nested types, this is the parent type signature string.
        /// </summary>
        public TypeSignature NestedTypeParent { get; set; }

        /// <summary>
        /// Generic type inputs are either 1) parameters or 2) arguments.
        /// If the method is a generic method definition (open generic method without specified generic type arguments), then it will have generic type parameter names.
        /// If the method is a constructed generic method (closed generic method with specified generic type arguments), then it will have generic type arguments.
        /// </summary>
        public TypeSignature[] GenericTypeInputs { get; set; }

        public bool Is_GenericMethodParameter { get; set; }
        public bool Is_GenericTypeParameter { get; set; }

        public bool Has_ElementType { get; set; }

        public TypeSignature ElementType { get; set; }

        public ElementTypeRelationships ElementTypeRelationships { get; set; }


        public TypeSignature()
        {
            this.KindMarker = Instances.KindMarkers.Type;
        }

        public override string ToString()
        {
            var output = Instances.TypeSignatureOperator.ToString(this);
            return output;
        }

        public bool Equals(TypeSignature other)
        {
            var output = Instances.TypeSignatureOperator.Are_Equal_ByValue(
                this,
                other);

            return output;
        }
    }
}
