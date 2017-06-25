
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.MessageApp.Data.Web;
using citPOINT.eNeg.Common;
using citPOINT.MessageApp.Data.Web;
using System.ServiceModel.DomainServices.Client;
using citPOINT.MessageApp.Common;
using System.ComponentModel.Composition;
using System.Threading;

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

namespace citPOINT.MessageApp.Model
{
    #region  Using MEF to export FiveDimensionModel
    /// <summary>
    /// Model Layer for Message Templates used as a generator for messages.
    /// </summary>
    [Export(typeof(IMessageTemplateModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class MessageTemplateModel : IMessageTemplateModel
    {
        #region → Fields         .
        private MessageAppContext mContext;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        DateTime LastActionDate = DateTime.Now;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private MessageAppContext Context
        {
            get
            {
                if (mContext == null)
                {

                    if (mContext == null)
                    {
                        mContext = new MessageAppContext(MessageAppConfigurations.MainServiceUri);
                    }

                    mContext.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ctx_PropertyChanged);
                }

                return mContext;
            }
        }

        /// <summary>
        /// True if _ctx.HasChanges is true; otherwise, false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }

            private set
            {
                if (this.mHasChanges != value)
                {
                    this.mHasChanges = value;
                    this.OnPropertyChanged("HasChanges");
                }
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mContext.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = mContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mContext.IsSubmitting;
                    break;
            }
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Occurs when [get message type complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageType>> GetMessageTypeComplete;

        /// <summary>
        /// Occurs when [get negotiation phase complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationPhase>> GetNegotiationPhaseComplete;

        /// <summary>
        /// Occurs when [get neg phase messages complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegPhaseMessage>> GetNegPhaseMessagesComplete;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {
            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        /// <param name="PreName">Name of the pre.</param>
        /// <returns>New Name have postfix of current date time</returns>
        private string GetItemName(string PreName)
        {
            TimeSpan span = DateTime.Now.Subtract(LastActionDate);

            if (span.TotalSeconds < 1)
            {
                Thread.Sleep(1000 - (int)span.TotalMilliseconds);
            }

            LastActionDate = DateTime.Now;

            return PreName + DateTime.Now.ToString(" (MM/dd/yyyy HH:mm:ss)");
        }

        #endregion

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation

        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the message type async.
        /// </summary>
        public void GetMessageTypeAsync()
        {
            PerformQuery<MessageType>(Context.GetMessageTypesForUserIDQuery(MessageAppConfigurations.CurrentLoginUser.UserID),
                GetMessageTypeComplete);
        }

        /// <summary>
        /// Gets the negotiation phase async.
        /// </summary>
        public void GetNegotiationPhaseAsync()
        {
            PerformQuery<NegotiationPhase>(Context.GetNegotiationPhasesForUserIDQuery(MessageAppConfigurations.CurrentLoginUser.UserID),
                GetNegotiationPhaseComplete);
        }

        /// <summary>
        /// Gets the neg phase messages async.
        /// </summary>
        public void GetNegPhaseMessagesAsync()
        {
            PerformQuery<NegPhaseMessage>(Context.GetNegPhaseMessagesForUserIDQuery(MessageAppConfigurations.CurrentLoginUser.UserID),
                GetNegPhaseMessagesComplete);
        }

        /// <summary>
        /// Adds the type of the message.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public MessageType AddMessageType(bool setInContext)
        {
            MessageType msgType = new MessageType()
            {
                MessageTypeID = Guid.NewGuid(),
                MessageTypeName = GetItemName("Message"),
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID
            };
            if (setInContext)
            {
                this.Context.MessageTypes.Add(msgType);
            }
            return msgType;
        }

        /// <summary>
        /// Adds the negotiation phase.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public NegotiationPhase AddNegotiationPhase(bool setInContext)
        {
            NegotiationPhase negPhase = new NegotiationPhase()
            {
                NegotiationPhaseID = Guid.NewGuid(),
                NegotiationPhaseName = GetItemName("Phase"),
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID
            };
            if (setInContext)
            {
                this.Context.NegotiationPhases.Add(negPhase);
            }
            return negPhase;
        }

        /// <summary>
        /// Adds the neg phase message.
        /// </summary>
        /// <param name="messageTypeID">The message type ID.</param>
        /// <param name="negotiationPhaseID">The negotiation phase ID.</param>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public NegPhaseMessage AddNegPhaseMessage(Guid messageTypeID, Guid negotiationPhaseID, bool setInContext)
        {
            NegPhaseMessage negPhaseMsg = new NegPhaseMessage()
            {
                NegPhaseMessagesID = Guid.NewGuid(),
                MessageTypeID = messageTypeID,
                NegotiationPhaseID = negotiationPhaseID,
                MessageContent = String.Empty,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID
            };
            if (setInContext)
            {
                this.Context.NegPhaseMessages.Add(negPhaseMsg);
            }
            return negPhaseMsg;
        }

        /// <summary>
        /// Removes the type of the message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        public void RemoveMessageType(MessageType messageType)
        {
            if (this.Context.MessageTypes.Contains(messageType))
            {
                var NegPhaseMsgs = this.Context.NegPhaseMessages
                                               .Where(s => s.MessageTypeID == messageType.MessageTypeID);

                while (NegPhaseMsgs.Count() > 0)
                {
                    this.Context.NegPhaseMessages.Remove(NegPhaseMsgs.First());
                }

                this.Context.MessageTypes.Remove(messageType);
            }
        }

        /// <summary>
        /// Removes the negotiation phase.
        /// </summary>
        /// <param name="negotiationPhase">The negotiation phase.</param>
        public void RemoveNegotiationPhase(NegotiationPhase negotiationPhase)
        {
            if (this.Context.NegotiationPhases.Contains(negotiationPhase))
            {
                var NegPhaseMsgs = this.Context.NegPhaseMessages
                                              .Where(s => s.NegotiationPhaseID == negotiationPhase.NegotiationPhaseID);

                while (NegPhaseMsgs.Count() > 0)
                {
                    this.Context.NegPhaseMessages.Remove(NegPhaseMsgs.First());
                }

                this.Context.NegotiationPhases.Remove(negotiationPhase);
            }
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        Exception ex = null;
                        if (s.HasError)
                        {
                            ex = s.Error;
                            s.MarkErrorAsHandled();
                        }
                        SaveChangesComplete(this, new SubmitOperationEventArgs(s, ex));
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }
        #endregion  Public

        #endregion Methods
    }
}
