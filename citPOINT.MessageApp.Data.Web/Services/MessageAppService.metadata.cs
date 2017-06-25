
#region → Usings   .

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;


#endregion

#region → History  .

/* Date         User           Change
 * 
 * 05.12.11     M.Wahab        Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.MessageApp.Data.Web
{
    // The MetadataTypeAttribute identifies MessageTypeMetadata as the class
    // that carries additional metadata for the MessageType class.
    [MetadataTypeAttribute(typeof(MessageType.MessageTypeMetadata))]
    public partial class MessageType
    {

        // This class allows you to attach custom attributes to properties
        // of the MessageType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MessageTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MessageTypeMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid MessageTypeID { get; set; }

            public string MessageTypeName { get; set; }

            public EntityCollection<NegPhaseMessage> NegPhaseMessages { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NegotiationPhaseMetadata as the class
    // that carries additional metadata for the NegotiationPhase class.
    [MetadataTypeAttribute(typeof(NegotiationPhase.NegotiationPhaseMetadata))]
    public partial class NegotiationPhase
    {

        // This class allows you to attach custom attributes to properties
        // of the NegotiationPhase class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationPhaseMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationPhaseMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid NegotiationPhaseID { get; set; }

            public string NegotiationPhaseName { get; set; }

            public EntityCollection<NegPhaseMessage> NegPhaseMessages { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NegPhaseMessageMetadata as the class
    // that carries additional metadata for the NegPhaseMessage class.
    [MetadataTypeAttribute(typeof(NegPhaseMessage.NegPhaseMessageMetadata))]
    public partial class NegPhaseMessage
    {

        // This class allows you to attach custom attributes to properties
        // of the NegPhaseMessage class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegPhaseMessageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegPhaseMessageMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public string MessageContent { get; set; }

            public MessageType MessageType { get; set; }

            public Nullable<Guid> MessageTypeID { get; set; }

            public NegotiationPhase NegotiationPhase { get; set; }

            public Nullable<Guid> NegotiationPhaseID { get; set; }

            public Guid NegPhaseMessagesID { get; set; }
        }
    }
}
