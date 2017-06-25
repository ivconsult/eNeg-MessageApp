
#region → Usings   .

using System.ServiceModel.DomainServices.Server;
using System.Linq;
using System;
using System.Collections.Generic;

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
    public partial class MessageAppService
    {
        #region → Negotiation Phases  .

        /// <summary>
        /// Gets the negotiation phases.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<NegotiationPhase> GetNegotiationPhasesForUserID(Guid userID)
        {
            List<NegotiationPhase> ls = this.ObjectContext
                                            .NegotiationPhases
                                            .Where(s => s.DeletedBy == userID &&
                                                        s.Deleted == false)
                                            .ToList();

            if (ls.Count == 0) // Inserting templates for user.
            {
                this.ObjectContext.ImportMessageTemplates(userID);
            }

            ls = this.ObjectContext
                     .NegotiationPhases
                     .Where(s => s.DeletedBy == userID &&
                                 s.Deleted == false)
                     .ToList();

            return ls.AsQueryable<NegotiationPhase>();
        }

        #endregion

        #region → Neg. Phase Messages .

        /// <summary>
        /// Gets the neg phase messages for user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<NegPhaseMessage> GetNegPhaseMessagesForUserID(Guid userID)
        {
            return this.ObjectContext
                       .NegPhaseMessages
                       .Where(s => s.DeletedBy == userID &&
                                   s.Deleted == false);
        }

        #endregion

        #region → Message Types       .

        /// <summary>
        /// Gets the message types for user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<MessageType> GetMessageTypesForUserID(Guid userID)
        {
            return this.ObjectContext
                       .MessageTypes
                       .Where(s => s.DeletedBy == userID &&
                                   s.Deleted == false);
        }

        #endregion
    }
}


