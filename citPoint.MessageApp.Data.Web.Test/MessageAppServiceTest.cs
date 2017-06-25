#region → Usings   .
using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.MessageApp.Common;
using citPOINT.MessageApp.Data.Web;
using citPOINT.eNeg.Data.Web.Test;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 14.08.11     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion
namespace citPOINT.MessageApp.Data.Web.Test
{
    /// <summary>
    /// Class for testing [Insert - Update - Delete] 
    /// operations for MessageApp Database
    /// </summary>
    [TestClass]
    public class MessageAppServiceTest
    {

        #region → Fields         .
        MessageAppContext mContext;
        List<NegotiationPhase> mNegotiationPhases;
        List<MessageType> mMessageTypes;
        List<NegPhaseMessage> mNegPhaseMessages;

        int CountOfAllEntries = 0;
        private TestContext testContextInstance;
        #endregion

        #region → Properties     .

        #region Mock Objects

        #region → <1> NegotiationPhase  .


        /// <summary>
        /// Gets the negotiation phases.
        /// </summary>
        /// <value>The negotiation phases.</value>
        public List<NegotiationPhase> NegotiationPhases
        {
            get
            {
                if (mNegotiationPhases == null)
                {
                    mNegotiationPhases = new List<NegotiationPhase>()
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
                return mNegotiationPhases;
            }
        }
        #endregion

        #region → <2> MessageType       .

        /// <summary>
        /// Gets the message types.
        /// </summary>
        /// <value>The message types.</value>
        public List<MessageType> MessageTypes
        {
            get
            {
                if (mMessageTypes == null)
                {
                    mMessageTypes = new List<MessageType>()
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
                return mMessageTypes;
            }
        }
        #endregion

        #region → <3> NegPhaseMessages  .

        /// <summary>
        /// Gets the neg phase messages.
        /// </summary>
        /// <value>The neg phase messages.</value>
        public List<NegPhaseMessage> NegPhaseMessages
        {
            get
            {
                if (mNegPhaseMessages == null)
                {
                    mNegPhaseMessages = new List<NegPhaseMessage>()
                    {
                        new NegPhaseMessage()
                        {
                            NegPhaseMessagesID=Guid.NewGuid(),
                            MessageTypeID=this.MessageTypes[0].MessageTypeID,
                            NegotiationPhaseID=this.NegotiationPhases[0].NegotiationPhaseID,
                            MessageContent="Message 1",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        },
                        new NegPhaseMessage()
                        {
                            NegPhaseMessagesID=Guid.NewGuid(),
                            MessageTypeID=this.MessageTypes[1].MessageTypeID,
                            NegotiationPhaseID=this.NegotiationPhases[1].NegotiationPhaseID,
                            MessageContent="Message 2",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        },
                        new NegPhaseMessage()
                        {
                            NegPhaseMessagesID=Guid.NewGuid(),
                            MessageTypeID=this.MessageTypes[2].MessageTypeID,
                            NegotiationPhaseID=this.NegotiationPhases[2].NegotiationPhaseID,
                            MessageContent="Message 3",
                            Deleted = false,
                            DeletedBy = MessageAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        }
                    };
                }
                return mNegPhaseMessages;
            }
        }

        #endregion

        #endregion

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
                    mContext = new MessageAppContext(new Uri("http://localhost:9004/citPOINT-MessageApp-Data-Web-MessageAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion Properties

        #region → Constructor    .
        public MessageAppServiceTest()
        {
            MessageAppConfigurations.CurrentLoginUser = new LoginUser() { UserID = Guid.NewGuid() };

            CountOfAllEntries = this.NegotiationPhases.Count +
                                this.NegPhaseMessages.Count +
                                this.MessageTypes.Count;
        }
        #endregion

        #region → Methods        .

        #region Test Insert All Entries
        /// <summary>
        ///A test for Insert All Entries
        ///</summary>
        [TestMethod]
        [Description(@"Test Insert Operations for all entries")]
        public void InsertAllEntries()
        {
            try
            {
                foreach (var item in this. NegotiationPhases)
                {
                    this.Context.NegotiationPhases.Add(item);
                }

                foreach (var item in this.MessageTypes)
                {
                    this.Context.MessageTypes.Add(item);
                }
                
                foreach (var item in this.NegPhaseMessages)
                {
                    this.Context.NegPhaseMessages.Add(item);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(InsertAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntries", ex);
            }
        }

        /// <summary>
        /// Inserts all entries complete.
        /// </summary>
        /// <param name="subOp">The sub op.</param>
        private void InsertAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    UpdateAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", subOp.Error);
            }
        }

        #endregion

        #region Test Update All Entries

        /// <summary>
        ///A test for Update All Entries
        ///</summary>
        public void UpdateAllEntries()
        {
            try
            {
                this.Context.RejectChanges();
                
                foreach (var item in this.NegotiationPhases)
                {
                    item.NegotiationPhaseName += "_Update";
                }

                foreach (var item in this.MessageTypes)
                {
                    item.MessageTypeName += "_Update";
                }


                foreach (var item in this.NegPhaseMessages)
                {
                    item.MessageContent += "_Update";
                }


                this.Context.SubmitChanges(new Action<SubmitOperation>(UpdateAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntries", ex);
            }
        }


        /// <summary>
        /// Event Complete of  Update All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        private void UpdateAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", "Number of Records updated is not right.");
                }
                else
                {
                    DeleteAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", subOp.Error);
            }
        }
        #endregion

        #region Test Delete All Entries


        /// <summary>
        ///A test for Delete All Entries
        ///only for Delete Flag
        ///</summary>
        public void DeleteAllEntries()
        {
            try
            {
                this.Context.RejectChanges();


               

                while (this.NegPhaseMessages.Count > 0)
                {
                    this.Context.NegPhaseMessages.Remove(this.NegPhaseMessages[0]);
                    this.NegPhaseMessages.RemoveAt(0);
                }


                while (this.MessageTypes.Count > 0)
                {
                    this.Context.MessageTypes.Remove(this.MessageTypes[0]);
                    this.MessageTypes.RemoveAt(0);
                }

                while (this.NegotiationPhases.Count > 0)
                {
                    this.Context.NegotiationPhases.Remove(this.NegotiationPhases[0]);
                    this.NegotiationPhases.RemoveAt(0);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(DeleteAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntries", ex);
            }
        }

        /// <summary>
        /// Event Complete of  Delete All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        void DeleteAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    eNegMessageBox.ShowMessageBox(true, "Inset - Update - Delete All Entries ", DeleteString);
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", subOp.Error);
            }
        }
        #endregion

        /// <summary>
        /// get SQL Statement to Clear Database
        /// </summary>
        private string DeleteString
        {
            get
            {
                return @"
---------------------------------------------------
You must run these SQL commands Before retest again
---------------------------------------------------

DELETE [NegotiationPhase];
DELETE [MessageType];
DELETE [NegPhaseMessages];
";
            }
        }
        #endregion Methods
    }
}
