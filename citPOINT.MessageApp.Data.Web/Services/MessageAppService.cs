
#region → Usings   .

using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;

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
    /// <summary>
    /// Message App Service
    /// </summary>
    [EnableClientAccess()]
    public partial class MessageAppService : LinqToEntitiesDomainService<MessageAppEntities>
    {
        #region → Message Types       .

        /// <summary>
        /// Inserts the type of the message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        public void InsertMessageType(MessageType messageType)
        {
            if ((messageType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(messageType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MessageTypes.AddObject(messageType);
            }
        }

        /// <summary>
        /// Updates the type of the message.
        /// </summary>
        /// <param name="currentMessageType">Type of the current message.</param>
        public void UpdateMessageType(MessageType currentMessageType)
        {
            this.ObjectContext.MessageTypes.AttachAsModified(currentMessageType, this.ChangeSet.GetOriginal(currentMessageType));
        }

        /// <summary>
        /// Deletes the type of the message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        public void DeleteMessageType(MessageType messageType)
        {
            if ((messageType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.MessageTypes.Attach(messageType);
            }
            this.ObjectContext.MessageTypes.DeleteObject(messageType);
        }

        #endregion

        #region → Negotiation Phases  .

        /// <summary>
        /// Inserts the negotiation phase.
        /// </summary>
        /// <param name="negotiationPhase">The negotiation phase.</param>
        public void InsertNegotiationPhase(NegotiationPhase negotiationPhase)
        {
            if ((negotiationPhase.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationPhase, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegotiationPhases.AddObject(negotiationPhase);
            }
        }

        /// <summary>
        /// Updates the negotiation phase.
        /// </summary>
        /// <param name="currentNegotiationPhase">The current negotiation phase.</param>
        public void UpdateNegotiationPhase(NegotiationPhase currentNegotiationPhase)
        {
            this.ObjectContext.NegotiationPhases.AttachAsModified(currentNegotiationPhase, this.ChangeSet.GetOriginal(currentNegotiationPhase));
        }

        /// <summary>
        /// Deletes the negotiation phase.
        /// </summary>
        /// <param name="negotiationPhase">The negotiation phase.</param>
        public void DeleteNegotiationPhase(NegotiationPhase negotiationPhase)
        {
            if ((negotiationPhase.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegotiationPhases.Attach(negotiationPhase);
            }
            this.ObjectContext.NegotiationPhases.DeleteObject(negotiationPhase);
        }

        #endregion

        #region → Neg. Phase Messages .

        /// <summary>
        /// Inserts the neg phase message.
        /// </summary>
        /// <param name="negPhaseMessage">The neg phase message.</param>
        public void InsertNegPhaseMessage(NegPhaseMessage negPhaseMessage)
        {
            if ((negPhaseMessage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negPhaseMessage, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegPhaseMessages.AddObject(negPhaseMessage);
            }
        }

        /// <summary>
        /// Updates the neg phase message.
        /// </summary>
        /// <param name="currentNegPhaseMessage">The current neg phase message.</param>
        public void UpdateNegPhaseMessage(NegPhaseMessage currentNegPhaseMessage)
        {
            this.ObjectContext.NegPhaseMessages.AttachAsModified(currentNegPhaseMessage, this.ChangeSet.GetOriginal(currentNegPhaseMessage));
        }

        /// <summary>
        /// Deletes the neg phase message.
        /// </summary>
        /// <param name="negPhaseMessage">The neg phase message.</param>
        public void DeleteNegPhaseMessage(NegPhaseMessage negPhaseMessage)
        {
            if ((negPhaseMessage.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegPhaseMessages.Attach(negPhaseMessage);
            }
            this.ObjectContext.NegPhaseMessages.DeleteObject(negPhaseMessage);
        }

        #endregion
    }
}


