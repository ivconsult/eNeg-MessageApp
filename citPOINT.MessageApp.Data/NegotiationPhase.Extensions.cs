
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
    /// NegotiationPhase class client-side extensions
    /// </summary>
    public partial class NegotiationPhase
    {


        #region → Fields         .
    
        private bool mIsSelected = false;
    
        #endregion

        #region → Properties     .
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return mIsSelected;
            }
            set
            {
                mIsSelected = value;
                this.RaiseDataMemberChanged("IsSelected");
            }
        }
        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the NegotiationPhase class
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
            if (propertyName == "NegotiationPhaseID"
             || propertyName == "NegotiationPhaseName"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "NegotiationPhaseID")
                    return Validator.TryValidateProperty(this.NegotiationPhaseID, context, validationResults);
                if (propertyName == "NegotiationPhaseName")
                    return Validator.TryValidateProperty(this.NegotiationPhaseName, context, validationResults);
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
        /// <returns>return new Instance of NegotiationPhase</returns>
        public NegotiationPhase Clone()
        {
            NegotiationPhase mNegotiationPhase = new NegotiationPhase
                                        {
                                            NegotiationPhaseID = this.NegotiationPhaseID,
                                            NegotiationPhaseName = this.NegotiationPhaseName,
                                            Deleted = this.Deleted,
                                            DeletedBy = this.DeletedBy,
                                            DeletedOn = this.DeletedOn,


                                        };

            return mNegotiationPhase;
        }




        #endregion Methods

    }

}
