
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using citPOINT.MessageApp.Common;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;
using citPOINT.eNeg.Common;
using citPOINT.MessageApp.Data.Web;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 07.12.11     M.Wahab         • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.MessageApp.MVVM.UnitTest
{
    /// <summary>
    /// Mock Message Template Model
    /// </summary>
    public class MockMessageTemplateModel : IMessageTemplateModel
    {
        #region → Fields         .

        private MessageAppContext mContext;
        private List<NegotiationPhase> mPhaseSource;
        private List<MessageType> mTypeSource;
        private List<NegPhaseMessage> mMessageSource;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// property with a getter only to can use eNegService
        /// </summary>
        public MessageAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new MessageAppContext(new Uri("http://localhost:9004/citPOINT-MessageApp-Data-Web-MessageAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }


        #region → <1> Phase   .

        /// <summary>
        /// Gets the phase sources.
        /// </summary>
        /// <value>The phase sources.</value>
        public List<NegotiationPhase> PhaseSources
        {
            get
            {
                if (mPhaseSource == null)
                {
                    mPhaseSource = new List<NegotiationPhase>()
                    {
                        new NegotiationPhase()
                        {
                            NegotiationPhaseID=Guid.NewGuid(),
                            NegotiationPhaseName="Phase 1",
                            Deleted=false,
                            DeletedBy=MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        },
                        
                        new NegotiationPhase()
                        {
                            NegotiationPhaseID=Guid.NewGuid(),
                            NegotiationPhaseName="Phase 2",
                            Deleted=false,
                            DeletedBy=MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        },

                        new NegotiationPhase()
                        {
                            NegotiationPhaseID=Guid.NewGuid(),
                            NegotiationPhaseName="Phase 3",
                            Deleted=false,
                            DeletedBy=MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        },
                        
                    };
                }
                return mPhaseSource;
            }
        }

        #endregion

        #region → <2> Type    .


        /// <summary>
        /// Gets the type source.
        /// </summary>
        /// <value>The type source.</value>
        public List<MessageType> TypeSource
        {
            get
            {
                if (mTypeSource == null)
                {
                    mTypeSource = new List<MessageType>()
                    {
                        new MessageType()
                        {
                            MessageTypeID = Guid.NewGuid(),
                            MessageTypeName="Type 1",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        },
                        new MessageType()
                        {
                            MessageTypeID = Guid.NewGuid(),
                            MessageTypeName="Type 2",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        },
                        new MessageType()
                        {
                            MessageTypeID = Guid.NewGuid(),
                            MessageTypeName="Type 3",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        }
                    };
                }
                return mTypeSource;
            }
        }
        #endregion

        #region → <3> Message .

        /// <summary>
        /// Gets the message sources.
        /// </summary>
        /// <value>The message sources.</value>
        public List<NegPhaseMessage> MessageSources
        {
            get
            {
                if (mMessageSource == null)
                {
                    mMessageSource = new List<NegPhaseMessage>()
                    {
                        new NegPhaseMessage()
                        {
                            NegPhaseMessagesID=Guid.NewGuid(),
                            MessageTypeID=this.TypeSource[0].MessageTypeID,
                            MessageType=this.TypeSource[0],
                            NegotiationPhaseID=this.PhaseSources[0].NegotiationPhaseID,
                            NegotiationPhase=this.PhaseSources[0],
                            MessageContent="Message 1",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        },
                        new NegPhaseMessage()
                        {
                            NegPhaseMessagesID=Guid.NewGuid(),
                            MessageTypeID=this.TypeSource[1].MessageTypeID,
                            MessageType=this.TypeSource[1],
                            NegotiationPhaseID=this.PhaseSources[1].NegotiationPhaseID,
                            NegotiationPhase=this.PhaseSources[1],
                            MessageContent="Message 2",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        },
                        new NegPhaseMessage()
                        {
                            NegPhaseMessagesID=Guid.NewGuid(),
                            MessageTypeID=this.TypeSource[2].MessageTypeID,
                            MessageType=this.TypeSource[2],
                            NegotiationPhaseID=this.PhaseSources[2].NegotiationPhaseID,
                            NegotiationPhase=this.PhaseSources[2],
                            MessageContent="Message 3",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        }
                    };
                }
                return mMessageSource;
            }
        }

        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockMessageTemplateModel"/> class.
        /// </summary>
        public MockMessageTemplateModel()
        {
            MessageAppConfigurations.CurrentLoginUser = new LoginUser();
            MessageAppConfigurations.CurrentLoginUser.UserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB");
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

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
            if (GetMessageTypeComplete != null)
            {
                GetMessageTypeComplete(this, new eNegEntityResultArgs<MessageType>(TypeSource));
            }
        }

        /// <summary>
        /// Gets the negotiation phase async.
        /// </summary>
        public void GetNegotiationPhaseAsync()
        {
            if (GetNegotiationPhaseComplete != null)
            {
                GetNegotiationPhaseComplete(this, new eNegEntityResultArgs<NegotiationPhase>(PhaseSources));
            }
        }

        /// <summary>
        /// Gets the neg phase messages async.
        /// </summary>
        public void GetNegPhaseMessagesAsync()
        {
            if (GetNegPhaseMessagesComplete != null)
            {
                GetNegPhaseMessagesComplete(this, new eNegEntityResultArgs<NegPhaseMessage>(MessageSources));
            }
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
                MessageTypeName = "New Message Type",
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID
            };

            if (setInContext)
            {
                this.TypeSource.Add(msgType);
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
                NegotiationPhaseName = "New Negotiation Phase",
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID
            };

            if (setInContext)
            {
                this.PhaseSources.Add(negPhase);
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
                this.MessageSources.Add(negPhaseMsg);
            }

            return negPhaseMsg;
        }

        /// <summary>
        /// Removes the type of the message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        public void RemoveMessageType(MessageType messageType)
        {
            if (this.TypeSource.Contains(messageType))
            {
                var NegPhaseMsgs = this.MessageSources
                                       .Where(s => s.MessageTypeID == messageType.MessageTypeID);

                while (NegPhaseMsgs.Count() > 0)
                {
                    this.MessageSources.Remove(NegPhaseMsgs.First());
                }

                this.TypeSource.Remove(messageType);
            }
        }

        /// <summary>
        /// Removes the negotiation phase.
        /// </summary>
        /// <param name="negotiationPhase">The negotiation phase.</param>
        public void RemoveNegotiationPhase(NegotiationPhase negotiationPhase)
        {
            if (this.PhaseSources.Contains(negotiationPhase))
            {
                var NegPhaseMsgs = this.MessageSources
                                       .Where(s => s.NegotiationPhaseID == negotiationPhase.NegotiationPhaseID);

                while (NegPhaseMsgs.Count() > 0)
                {
                    this.MessageSources.Remove(NegPhaseMsgs.First());
                }

                this.PhaseSources.Remove(negotiationPhase);
            }
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
           
        }

        #endregion  Public

        #endregion Methods


    }
}
