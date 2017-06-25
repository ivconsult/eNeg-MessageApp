
#region → Usings   .
using System;
using citPOINT.eNeg.Common;
using citPOINT.MessageApp.Data.Web;
using System.ComponentModel;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 06.12.11    Yousra         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.MessageApp.Common
{
    /// <summary>
    /// Interface for message template Model
    /// </summary>
    public interface IMessageTemplateModel
    {
        #region → Properties     .
        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        bool HasChanges { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        bool IsBusy { get; }
        #endregion

        #region → Properties     .

        /// <summary>
        /// Occurs when [get negotiation phase complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationPhase>> GetNegotiationPhaseComplete;

        /// <summary>
        /// Occurs when [get message type complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageType>> GetMessageTypeComplete;

        /// <summary>
        /// Occurs when [get neg phase messages complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegPhaseMessage>> GetNegPhaseMessagesComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion

        #region → Methods        .

        /// <summary>
        /// Gets the neg phase messages async.
        /// </summary>
        void GetNegPhaseMessagesAsync();

        /// <summary>
        /// Gets the message type async.
        /// </summary>
        void GetMessageTypeAsync();

        /// <summary>
        /// Gets the negotiation phase async.
        /// </summary>
        void GetNegotiationPhaseAsync();

        /// <summary>
        /// Adds the type of the message.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        MessageType AddMessageType(bool setInContext);

        /// <summary>
        /// Adds the negotiation phase.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        NegotiationPhase AddNegotiationPhase(bool setInContext);

        /// <summary>
        /// Adds the neg phase message.
        /// </summary>
        /// <param name="messageTypeID">The message type ID.</param>
        /// <param name="negotiationPhaseID">The negotiation phase ID.</param>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        NegPhaseMessage AddNegPhaseMessage(Guid messageTypeID, Guid negotiationPhaseID, bool setInContext);

        /// <summary>
        /// Removes the type of the message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        void RemoveMessageType(MessageType messageType);

        /// <summary>
        /// Removes the negotiation phase.
        /// </summary>
        /// <param name="negotiationPhase">The negotiation phase.</param>
        void RemoveNegotiationPhase(NegotiationPhase negotiationPhase);

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();
        #endregion
    }
}
