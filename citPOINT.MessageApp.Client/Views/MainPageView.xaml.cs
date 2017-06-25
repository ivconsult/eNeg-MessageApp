#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using citPOINT.MessageApp.Common;
using citPOINT.MessageApp.Data.Web;
using Telerik.Windows.Controls.GridView;
using System.ComponentModel.Composition;
using citPOINT.MessageApp.ViewModel;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Apps.Common.Interfaces;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 07.12.11    Yousra         • creation
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

namespace citPOINT.MessageApp.Client
{
    /// <summary>
    /// UI responsible for adding, deleting, and updating Negotiation Phases
    /// </summary>
    [Export]
    public partial class MainPageView : UserControl, ICleanup, IObserverApp
    {

        #region → Fields         .
        private ManagePhasesView mManagePhasesView;
        private ManageTypesView mManageTypesView;
        private MainSettingView mMainSettingView;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the view model repository.
        /// </summary>
        /// <value>The view model repository.</value>
        private ViewModelRepository ViewModelRepository { get; set; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get { return MessageAppConfigurations.AppName; }
        }

        /// <summary>
        /// Gets the manage phases view.
        /// </summary>
        /// <value>The manage phases view.</value>
        public ManagePhasesView ManagePhasesView
        {
            get
            {
                if (mManagePhasesView == null)
                {
                    mManagePhasesView = new ManagePhasesView();
                }
                return mManagePhasesView;
            }
        }

        /// <summary>
        /// Gets the manage types view.
        /// </summary>
        /// <value>The manage types view.</value>
        public ManageTypesView ManageTypesView
        {
            get
            {
                if (mManageTypesView == null)
                {
                    mManageTypesView = new ManageTypesView();
                }
                return mManageTypesView;
            }
        }

        /// <summary>
        /// Gets the main setting view.
        /// </summary>
        /// <value>The main setting view.</value>
        public MainSettingView MainSettingView
        {
            get
            {
                if (mMainSettingView == null)
                {
                    mMainSettingView = new MainSettingView();
                }
                return mMainSettingView;
            }
        }
        #endregion

        #region → Constructor    .
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageView"/> class.
        /// </summary>
        public MainPageView()
        {
            InitializeComponent();

            #region Registration for needed messages in MessageAppMessanger

            MessageAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreen);
            MessageAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            
            #endregion

            try
            {
                this.ApplyChanges(false);

                MessageAppConfigurations.MainPlatformInfo.TrackChanges.AddObserverApp(this);
            }
            catch (Exception ex)
            {
                MessageAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, MessageAppConfigurations.AppName);
            }
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex"></param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            MessageAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, MessageAppConfigurations.AppName);
        }

        /// <summary>
        /// Called when [change screen].
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        private void OnChangeScreen(string pageName)
        {
            switch (pageName)
            {
                case MessageAppViewTypes.MainSettingsView:
                    uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    uxMainContent.Content = MainSettingView;
                    break;

                case MessageAppViewTypes.ManagePhasesView:
                    uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    PopUpWindow ManagePhasesPopUp = new PopUpWindow("Manage Phases");
                    ManagePhasesPopUp.DataContext = this.DataContext;
                    ManagePhasesPopUp.Content = new ManagePhasesView();// ManagePhasesView;
                    ManagePhasesPopUp.ShowDialog();
                    break;

                case MessageAppViewTypes.ManageTypesView:
                    uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    PopUpWindow ManageTypesPopUp = new PopUpWindow("Manage Message Types");
                    ManageTypesPopUp.DataContext = this.DataContext;
                    ManageTypesPopUp.Content = new ManageTypesView();// ManageTypesView;
                    ManageTypesPopUp.ShowDialog();
                    break;

            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public void ApplyChanges(bool isActive)
        {
            if (isActive)
            {
                this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;

                #region → Change Negotiation      .

                //if (MessageAppConfigurations.MainPlatformInfo.CurrentNegotiation != null)
                //{
                //    MessageAppConfigurations.NegotiationIDParameter = MessageAppConfigurations.MainPlatformInfo.CurrentNegotiation.NegotiationID;
                //}
                //else
                //{
                //    MessageAppConfigurations.NegotiationIDParameter = Guid.Empty;
                //}

                #endregion

                #region → Change Conversation     .

                //if (MessageAppConfigurations.MainPlatformInfo.CurrentConversation != null)
                //{
                //    MessageAppConfigurations.ConversationIDParameter = MessageAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;
                //}
                //else
                //{
                //    MessageAppConfigurations.ConversationIDParameter = Guid.Empty;
                //}

                #endregion

                #region → Change User             .

                if (MessageAppConfigurations.CurrentLoginUser != null && MessageAppConfigurations.CurrentLoginUser.UserID != MessageAppConfigurations.MainPlatformInfo.UserInfo.UserID)
                {
                    if (this.ViewModelRepository != null)
                    {
                        this.ViewModelRepository.Cleanup();

                        this.ViewModelRepository = null;
                    }
                }

                MessageAppConfigurations.CurrentLoginUser = MessageAppConfigurations.MainPlatformInfo.UserInfo;

                #endregion

                #region → View Model Repository   .

                if (ViewModelRepository != null)
                {
                    ViewModelRepository.MessageTemplateViewModel.ApplyChanges();
                }
                else
                {
                    ViewModelRepository = new ViewModelRepository();
                }

                this.DataContext = ViewModelRepository.MessageTemplateViewModel;

                #endregion

                #region → Adjust Widht and Heihgt .

                //this.uxMainContent.Width = MessageAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Width;
                //this.uxMainContent.MinWidth = this.uxMainContent.Width;
                //this.uxMainContent.MaxWidth = this.uxMainContent.Width;

                //this.uxMainContent.Height = MessageAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Height;
                //this.uxMainContent.MinHeight = this.uxMainContent.Height;
                //this.uxMainContent.MaxHeight = this.uxMainContent.Height;

                #endregion
            }
            else
            {
                this.uxMainContent.Content = new RadBusyIndicator() { IsBusy = true };
            }
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

        #endregion  Public

        #endregion Methods

    }
}
