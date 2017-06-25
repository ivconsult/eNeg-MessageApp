
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.MessageApp.Common;
using citPOINT.MessageApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
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
    /// Message Template View Model Test class
    /// </summary>
    [TestClass]
    public class MessageTemplateViewModel_Test
    {
        #region → Fields         .
        private MessageTemplateViewModel MessageTemplatevm;
        private string ErrorMessage;
        string currentScreen = null;
        #endregion

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        /// <value>The VM.</value>
        public MessageTemplateViewModel TheVM
        {
            get { return MessageTemplatevm; }
            set { MessageTemplatevm = value; }
        }
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateViewModel_Test"/> class.
        /// </summary>
        [TestInitialize]
        public void BuildUp()
        {
            TheVM = new MessageTemplateViewModel(new MockMessageTemplateModel());

            #region → Registeration for needed messages in eNegMessenger
            // register for RaiseErrorMessage
            MessageAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            MessageAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreenMessage);

            MessageAppMessanger.ConfirmMessage.Register(this, OnConfirmMessage);

            #endregion
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message   .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;
                }
                else
                    ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region → On Confirm Message    .

        /// <summary>
        /// Called when [confirm message].
        /// </summary>
        /// <param name="dialogMessage">The dialog message.</param>
        private void OnConfirmMessage(DialogMessage dialogMessage)
        {
            if (dialogMessage != null)
            {
                dialogMessage.Callback(MessageBoxResult.OK);
            }
        }

        #endregion

        #region → Change Screen Message .

        /// <summary>
        /// Called when [change screen message].
        /// </summary>
        /// <param name="screenName">Name of the screen.</param>
        private void OnChangeScreenMessage(string screenName)
        {
            this.currentScreen = screenName;
        }

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Gets the Phases without condition return collection.
        /// </summary>
        [TestMethod]
        public void GetPhases_WithoutCondition_ReturnCollection()
        {
            TheVM.GetNegotiationPhaseAsync();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.PhaseSource.Count() > 0, "No Phases Found");
        }

        /// <summary>
        /// Gets the types without condition return collection.
        /// </summary>
        [TestMethod]
        public void GetTypes_WithoutCondition_ReturnCollection()
        {
            TheVM.GetMessageTypeAsync();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.TypeSource.Count() > 0, "No Type Found");
        }

        /// <summary>
        /// Gets the messages without condition return collection.
        /// </summary>
        [TestMethod]
        public void GetMessages_WithoutCondition_ReturnCollection()
        {
            TheVM.GetNegPhaseMessagesAsync();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.MessageSource.Count() > 0, "No Message Found");
        }

        /// <summary>
        /// Add the new phase new record added.
        /// </summary>
        [TestMethod]
        public void Add_NewPhase_NewRecordAdded()
        {
            #region → Arrange .

            int ExpectedCout = TheVM.PhaseSource.Count + 1;

            #endregion

            #region → Act     .

            TheVM.AddNewPhaseCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.PhaseSource.Count == ExpectedCout, "Phase not added successfully");

            #endregion
        }

        /// <summary>
        /// Add the new type new record added.
        /// </summary>
        [TestMethod]
        public void Add_NewType_NewRecordAdded()
        {
            #region → Arrange .

            int ExpectedCout = TheVM.TypeSource.Count + 1;

            #endregion

            #region → Act     .

            TheVM.AddNewTypeCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.TypeSource.Count == ExpectedCout, "Type not added successfully");

            #endregion
        }

        /// <summary>
        /// Deletes the type by selection record deleted.
        /// </summary>
        [TestMethod]
        public void DeleteType_BySelection_RecordDeleted()
        {
            #region → Arrange .

            int ExpectedCout = TheVM.TypeSource.Count - 2;

            TheVM.TypeSource[0].IsSelected = true;
            TheVM.TypeSource[1].IsSelected = true;

            #endregion

            #region → Act     .

            TheVM.DeleteTypeCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.TypeSource.Count == ExpectedCout, "Type not added successfully");

            #endregion
        }

        /// <summary>
        /// Deletes the phase by selection record deleted.
        /// </summary>
        [TestMethod]
        public void DeletePhase_BySelection_RecordDeleted()
        {
            #region → Arrange .

            int ExpectedCout = TheVM.PhaseSource.Count - 2;

            TheVM.PhaseSource[0].IsSelected = true;
            TheVM.PhaseSource[1].IsSelected = true;

            #endregion

            #region → Act     .

            TheVM.DeletePhaseCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.PhaseSource.Count == ExpectedCout, "Phase not added successfully");

            #endregion
        }

        /// <summary>
        /// Navigates the to app settings new window opened.
        /// </summary>
        [TestMethod]
        public void NavigateTo_AppSettings_NewWindowOpened()
        {
            #region → Arrange .

            string screenName = MessageAppViewTypes.AppSettingsView;

            #endregion

            #region → Act     .

            TheVM.OpenAppSettingsViewCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.currentScreen == screenName, "Can not navigate to App Settings");

            #endregion
        }

        /// <summary>
        /// Navigates the to phases view new window opened.
        /// </summary>
        [TestMethod]
        public void NavigateTo_PhasesView_NewWindowOpened()
        {
            #region → Arrange .

            string screenName = MessageAppViewTypes.ManagePhasesView;

            #endregion

            #region → Act     .

            TheVM.OpenManagePhasesCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.currentScreen == screenName, "Can not navigate to phase View");

            #endregion
        }

        /// <summary>
        /// Navigates the to types view new window opened.
        /// </summary>
        [TestMethod]
        public void NavigateTo_TypesView_NewWindowOpened()
        {
            #region → Arrange .

            string screenName = MessageAppViewTypes.ManageTypesView;

            #endregion

            #region → Act     .

            TheVM.OpenManageTypeViewCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.currentScreen == screenName, "Can not navigate to Type View");

            #endregion
        }

        /// <summary>
        /// Navigates the to main view new window opened.
        /// </summary>
        [TestMethod]
        public void NavigateTo_MainView_NewWindowOpened()
        {
            #region → Arrange .

            string screenName = MessageAppViewTypes.GenerateMessagesView;

            #endregion

            #region → Act     .

            TheVM.OpenGenerateMessagesViewCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.currentScreen == screenName, "Can not navigate to Main View");

            #endregion
        }

        #endregion

        #endregion
    }
}
