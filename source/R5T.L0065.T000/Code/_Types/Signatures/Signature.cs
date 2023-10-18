using System;

using R5T.L0062.L001;
using R5T.T0142;


namespace R5T.L0065.T000
{
    /// <summary>
    /// A signature structure base type for representing the various member types in C# (events, fields, methods, properties, types).
    /// </summary>
    [DataTypeMarker]
    public abstract class Signature :
        // Includes the kind marker.
        IWithKindMarker
    {
        public char KindMarker { get; set; }

        public bool IsObsolete { get; set; }
    }
}
