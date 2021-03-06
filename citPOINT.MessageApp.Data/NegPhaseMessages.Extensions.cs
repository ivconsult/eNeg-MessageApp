
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *06.12.11     M.Wahab     creation
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


    /// <summary>
    /// NegPhaseMessages class client-side extensions
    /// </summary>
    public partial class NegPhaseMessage
    {


        #region → Fields         .

        #endregion

        #region → Properties     .
        
        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the NegPhaseMessages class
        /// </summary>
        /// <returns>True Or False </returns>
        public bool TryValidateObject()
        {


            ValidationContext context = new ValidationContext(this, null, null);
            var validationResults = new Collection<ValidationResult>();

            if (Validator.TryValidateObject(this, context, validationResults, false) == false)
            {
                foreach (ValidationResult error in validationResults)
                {
                    this.ValidationErrors.Add(error);
                }
                return false;
            }


            return true;
        }


        /// <summary>    
        /// Try Try Validate by Property name  
        /// </summary> 
        /// <returns>True Or False </returns> 
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "NegPhaseMessagesID"
             || propertyName == "MessageTypeID"
             || propertyName == "NegotiationPhaseID"
             || propertyName == "MessageContent"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "NegPhaseMessagesID")
                    return Validator.TryValidateProperty(this.NegPhaseMessagesID, context, validationResults);
                if (propertyName == "MessageTypeID")
                    return Validator.TryValidateProperty(this.MessageTypeID, context, validationResults);
                if (propertyName == "NegotiationPhaseID")
                    return Validator.TryValidateProperty(this.NegotiationPhaseID, context, validationResults);
                if (propertyName == "MessageContent")
                    return Validator.TryValidateProperty(this.MessageContent, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedBy")
                    return Validator.TryValidateProperty(this.DeletedBy, context, validationResults);
                if (propertyName == "DeletedOn")
                    return Validator.TryValidateProperty(this.DeletedOn, context, validationResults);
            }
            return false;
        }


        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>return new Instance of NegPhaseMessages</returns>
        public NegPhaseMessage Clone()
        {
            NegPhaseMessage mNegPhaseMessages = new NegPhaseMessage
                                        {
                                            NegPhaseMessagesID = this.NegPhaseMessagesID,
                                            MessageTypeID = this.MessageTypeID,
                                            NegotiationPhaseID = this.NegotiationPhaseID,
                                            MessageContent = this.MessageContent,
                                            Deleted = this.Deleted,
                                            DeletedBy = this.DeletedBy,
                                            DeletedOn = this.DeletedOn,


                                        };

            return mNegPhaseMessages;
        }




        #endregion Methods

    }

}
