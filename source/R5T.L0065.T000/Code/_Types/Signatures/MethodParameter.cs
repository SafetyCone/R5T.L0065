using System;

using R5T.T0142;


namespace R5T.L0065.T000
{
    /// <summary>
    /// Represents the parameter to a method (or property).
    /// </summary>
    [DataTypeMarker]
    public class MethodParameter
    {
        public TypeSignature ParameterType { get; set; }
        public string ParameterName { get; set; }


        public override string ToString()
        {
            var output = Instances.MethodParameterOperator.ToString(this);
            return output;
        }
    }
}
