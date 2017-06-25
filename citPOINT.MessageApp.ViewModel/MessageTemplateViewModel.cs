#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.MessageApp.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using citPOINT.MessageApp.Data.Web;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using citPOINT.eNeg.Common;
using System.Windows.Controls;
using System.Windows.Browser;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 18.08.11     Y.Mohammed     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.MessageApp.ViewModel
{
    #region → Using  MEF to export MessageTemplateViewModel
    /// <summary>
    /// this class is to Message Template View Model.
    /// </summary>
    [Export(MessageAppViewModelTypes.MessageTemplateViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class MessageTemplateViewModel : ViewModelBase
    {

        #region → Fields         .

        private IMessageTemplateModel mMessageTemplateModel;

        private List<NegotiationPhase> mPhaseSource;
        private List<MessageType> mTypeSource;
        private List<NegPhaseMessage> mMessageSource;
        private bool RunQueueForApplyChanges;
        private bool mIsBusy;

        private NegotiationPhase mCurrentPhase;
        private MessageType mCurrentType;
        private NegPhaseMessage mCurrentMessage;

        private string mMessageContent;
        private bool mMessageHasChanges;

        private RelayCommand<String> mSubmitChangeCommand;
        private RelayCommand mOpenManagePhasesCommand;
        private RelayCommand mOpenManageTypeCommand;
        private RelayCommand mDeletePhaseCommand;
        private RelayCommand mDeleteTypeCommand;
        private RelayCommand mAddNewPhaseCommand;
        private RelayCommand mAddNewTypeCommand;
        private RelayCommand mGenerateEmailCommand;
        private RelayCommand mCopyToClipboardCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return mIsBusy; }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");

                if (!this.IsBusy)
                {
                    if (RunQueueForApplyChanges)
                    {
                        ApplyChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the phase source.
        /// </summary>
        /// <value>The phase source.</value>
        public List<NegotiationPhase> PhaseSource
        {
            get
            {
                return mPhaseSource;
            }
            set
            {
                mPhaseSource = value;
                this.RaisePropertyChanged("PhaseSource");
            }
        }

        /// <summary>
        /// Gets or sets the type source.
        /// </summary>
        /// <value>The type source.</value>
        public List<MessageType> TypeSource
        {
            get { return mTypeSource; }
            set
            {
                mTypeSource = value;
                this.RaisePropertyChanged("TypeSource");
            }
        }

        /// <summary>
        /// Gets or sets the message source.
        /// </summary>
        /// <value>The message source.</value>
        public List<NegPhaseMessage> MessageSource
        {
            get { return mMessageSource; }
            set
            {
                mMessageSource = value;
                this.RaisePropertyChanged("MessageSource");
            }
        }

        /// <summary>
        /// Gets or sets the current phase.
        /// </summary>
        /// <value>The current phase.</value>
        public NegotiationPhase CurrentPhase
        {
            get { return mCurrentPhase; }
            set
            {
                if (mCurrentPhase != value)
                {
                    mCurrentPhase = value;
                    this.RaisePropertyChanged("CurrentPhase");

                    this.SetCurrentMessage();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the current.
        /// </summary>
        /// <value>The type of the current.</value>
        public MessageType CurrentType
        {
            get
            {
                return mCurrentType;
            }
            set
            {
                if (mCurrentType != value)
                {
                    mCurrentType = value;
                    this.RaisePropertyChanged("CurrentType");

                    this.SetCurrentMessage();
                }
            }
        }

        /// <summary>
        /// Gets or sets the current message.
        /// </summary>
        /// <value>The current message.</value>
        public NegPhaseMessage CurrentMessage
        {
            get
            {
                return mCurrentMessage;
            }
            set
            {
                mCurrentMessage = value;
                this.RaisePropertyChanged("CurrentMessage");
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        /// <value>The content of the message.</value>
        public string MessageContent
        {
            get
            {
                return mMessageContent;
            }
            set
            {
                mMessageContent = value;
                this.RaisePropertyChanged("MessageContent");

                #region → Set Message Changes .

                if (this.CurrentMessage != null)
                {
                    if (this.CurrentMessage.EntityState == EntityState.Detached &&
                        !string.IsNullOrEmpty(value))
                    {
                        this.MessageHasChanges = true;
                    }
                    else if (this.CurrentMessage.EntityState == EntityState.Unmodified)
                    {
                        this.MessageHasChanges = this.CurrentMessage.MessageContent != value;
                    }
                    this.RaiseCanExecuteChanged();
                }

                #endregion

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [message has changes].
        /// </summary>
        /// <value><c>true</c> if [message has changes]; otherwise, <c>false</c>.</value>
        public bool MessageHasChanges
        {
            get { return mMessageHasChanges; }
            set
            {
                mMessageHasChanges = value;
                this.RaisePropertyChanged("MessageHasChanges");
            }
        }
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateViewModel"/> class.
        /// </summary>
        /// <param name="MessageTemplateModel">The five dimension model.</param>
        [ImportingConstructor]
        public MessageTemplateViewModel(IMessageTemplateModel MessageTemplateModel)
        {
            this.mMessageTemplateModel = MessageTemplateModel;

            #region → Set up event handling       .
            this.mMessageTemplateModel.GetNegotiationPhaseComplete += new EventHandler<eNegEntityResultArgs<NegotiationPhase>>(mMessageTemplateModel_GetNegotiationPhaseComplete);
            this.mMessageTemplateModel.GetMessageTypeComplete += new EventHandler<eNegEntityResultArgs<MessageType>>(mMessageTemplateModel_GetMessageTypeComplete);
            this.mMessageTemplateModel.GetNegPhaseMessagesComplete += new EventHandler<eNegEntityResultArgs<NegPhaseMessage>>(mMessageTemplateModel_GetNegPhaseMessagesComplete);
            this.mMessageTemplateModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mMessageTemplateModel_PropertyChanged);
            this.mMessageTemplateModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mMessageTemplateModel_SaveChangesComplete);
            #endregion

            #region → Load Lookup tables          .
            GetNegotiationPhaseAsync();
            #endregion

            #region → Register Messages           .
            // register for SubmitChangesMessage
            MessageAppMessanger.SubmitChangesMessage.Register(this, OnSubmitChanges);
            #endregion Register Messages
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Call Back of get neg phase messages.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mMessageTemplateModel_GetNegPhaseMessagesComplete(object sender, eNegEntityResultArgs<NegPhaseMessage> e)
        {
            if (!e.HasError)
            {
                this.MessageSource = e.Results.ToList();
                this.SetCurrentMessage();
            }
            else
            {
                MessageAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call Back of  get message type.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mMessageTemplateModel_GetMessageTypeComplete(object sender, eNegEntityResultArgs<MessageType> e)
        {
            if (!e.HasError)
            {
                this.TypeSource = e.Results.OrderBy(s => s.MessageTypeName).ToList();

                foreach (var item in this.TypeSource)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(ItemProperties_PropertyChanged);
                }
            }
            else
            {
                MessageAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call Back of get negotiation phase.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mMessageTemplateModel_GetNegotiationPhaseComplete(object sender, eNegEntityResultArgs<NegotiationPhase> e)
        {
            if (!e.HasError)
            {
                this.PhaseSource = e.Results.OrderBy(s => s.NegotiationPhaseName).ToList();

                foreach (var item in this.PhaseSource)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(ItemProperties_PropertyChanged);
                }

                MessageAppMessanger.ChangeScreenMessage.Send(MessageAppViewTypes.MainSettingsView);

                this.GetMessageTypeAsync();

                this.GetNegPhaseMessagesAsync();
            }
            else
            {
                MessageAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the ItemProperties control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ItemProperties_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected" ||
                e.PropertyName == "NegotiationPhaseName" ||
                e.PropertyName == "MessageTypeName")
            {
                this.RaiseCanExecuteChanged();

                if (e.PropertyName.IndexOf("Name") != -1)
                {
                    if (sender.GetType().Equals(typeof(NegotiationPhase)))
                    {
                        IsAllPhaseValid();
                    }
                    else if (sender.GetType().Equals(typeof(MessageType)))
                    {
                        IsAllTypeValid();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the mMessageTemplateModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mMessageTemplateModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Handles the SaveChangesComplete event of the mMessageTemplateModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eNeg.Common.SubmitOperationEventArgs"/> instance containing the event data.</param>
        private void mMessageTemplateModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                this.MessageHasChanges = false;
                this.RaiseCanExecuteChanged();
            }
            else
            {
                // notify user if there is any error
                MessageAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        /// <value>The submit change command.</value>
        public RelayCommand<String> SubmitChangeCommand
        {
            get
            {
                if (mSubmitChangeCommand == null)
                {
                    mSubmitChangeCommand = new RelayCommand<String>((saveType) =>
                    {
                        try
                        {
                            #region → Validate user culture and partner culture in case of Always is .

                            switch (saveType.ToLower())
                            {
                                case "phase":
                                    if (!IsAllPhaseValid())
                                    {
                                        return;
                                    }
                                    break;

                                case "type":
                                    if (!IsAllTypeValid())
                                    {
                                        return;
                                    }
                                    break;

                                case "message":
                                    if (!IsMessageValid())
                                    {
                                        return;
                                    }

                                    if (this.CurrentMessage.EntityState == EntityState.Detached)
                                    {
                                        NegPhaseMessage msg = this.mMessageTemplateModel.AddNegPhaseMessage(this.CurrentType.MessageTypeID, this.CurrentPhase.NegotiationPhaseID, true);
                                        msg.MessageContent = this.MessageContent;
                                        this.CurrentMessage = msg;
                                        this.MessageSource.Add(msg);
                                    }
                                    else
                                    {
                                        this.CurrentMessage.MessageContent = MessageContent;
                                    }
                                    break;
                                default:
                                    break;
                            }

                            #endregion

                            #region → Submit changes if this command raised due to changes in context, then in callback update user culture if reuired .
                            if (this.mMessageTemplateModel.HasChanges)
                            {
                                MessageAppMessanger.SubmitChangesMessage.Send();
                            }
                            #endregion

                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            MessageAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (saveType) => (this.CurrentMessage != null && !this.mMessageTemplateModel.IsBusy && saveType == "Message") ||
                        (!this.mMessageTemplateModel.IsBusy && (this.mMessageTemplateModel.HasChanges ||
                        (this.CurrentMessage != null && this.MessageHasChanges))));
                }
                return mSubmitChangeCommand;
            }
        }

        /// <summary>
        /// Gets the open manage phases command.
        /// </summary>
        /// <value>The open manage phases command.</value>
        public RelayCommand OpenManagePhasesCommand
        {
            get
            {
                if (mOpenManagePhasesCommand == null)
                {
                    mOpenManagePhasesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mMessageTemplateModel.IsBusy)
                            {
                                this.RejectChanges();

                                MessageAppMessanger.ChangeScreenMessage.Send(MessageAppViewTypes.ManagePhasesView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            MessageAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mOpenManagePhasesCommand;
            }
        }

        /// <summary>
        /// Gets the open manage type view command.
        /// </summary>
        /// <value>The open manage type view command.</value>
        public RelayCommand OpenManageTypeViewCommand
        {
            get
            {
                if (mOpenManageTypeCommand == null)
                {
                    mOpenManageTypeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mMessageTemplateModel.IsBusy)
                            {
                                this.RejectChanges();
                                MessageAppMessanger.ChangeScreenMessage.Send(MessageAppViewTypes.ManageTypesView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            MessageAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mOpenManageTypeCommand;
            }
        }

        /// <summary>
        /// Gets the delete phase command.
        /// </summary>
        /// <value>The delete phase command.</value>
        public RelayCommand DeletePhaseCommand
        {
            get
            {
                if (mDeletePhaseCommand == null)
                {
                    mDeletePhaseCommand = new RelayCommand(() =>
                    {
                        if (!this.mMessageTemplateModel.IsBusy &&
                            this.PhaseSource.Where(a => a.IsSelected == true).Count() > 0)
                        {
                            #region → Confirmation Message .

                            Action<MessageBoxResult> callBackResult = null;

                            //Firstly ask user to confirm editing that item
                            DialogMessage dialogMessage = new DialogMessage(
                                this,
                                Resources.DeleteCurrentItemMessageBoxText,
                                result => callBackResult(result))
                            {
                                Button = MessageBoxButton.OKCancel,
                                Caption = Resources.ConfirmMessageBoxCaption
                            };

                            eNegMessanger.ConfirmMessage.Send(dialogMessage);

                            #endregion "Confirmation Message"

                            callBackResult = (result) =>
                               {
                                   if (result == MessageBoxResult.Cancel)
                                       return;

                                   #region → Delete Process       .

                                   List<Guid> selectedIDs = new List<Guid>();

                                   foreach (var phaseItem in this.PhaseSource.Where(a => a.IsSelected == true))
                                   {
                                       selectedIDs.Add(phaseItem.NegotiationPhaseID);

                                   }

                                   //So it canceled
                                   this.RejectChanges();

                                   while (selectedIDs.Count > 0)
                                   {
                                       NegotiationPhase phaseObj = this.PhaseSource.Where(s => s.NegotiationPhaseID == selectedIDs[0]).FirstOrDefault();


                                       foreach (var item in phaseObj.NegPhaseMessages)
                                       {
                                           this.MessageSource.Remove(item);
                                       }

                                       this.mMessageTemplateModel.RemoveNegotiationPhase(phaseObj);

                                       this.PhaseSource.Remove(phaseObj);

                                       selectedIDs.Remove(selectedIDs[0]);

                                   }

                                   this.PhaseSource = new List<NegotiationPhase>(this.PhaseSource);

                                   MessageAppMessanger.SubmitChangesMessage.Send(); ;

                                   #endregion
                               };
                        }
                    },
                    () => !this.mMessageTemplateModel.IsBusy &&
                           this.PhaseSource.Where(a => a.IsSelected == true).Count() > 0);
                }

                return mDeletePhaseCommand;
            }
        }

        /// <summary>
        /// Gets the delete Type command.
        /// </summary>
        /// <value>The delete Type command.</value>
        public RelayCommand DeleteTypeCommand
        {
            get
            {
                if (mDeleteTypeCommand == null)
                {
                    mDeleteTypeCommand = new RelayCommand(() =>
                    {

                        if (!this.mMessageTemplateModel.IsBusy &&
                             this.TypeSource.Where(a => a.IsSelected == true).Count() > 0)
                        {

                            #region → Confirmation Message .
                            Action<MessageBoxResult> callBackResult = null;

                            //Firstly ask user to confirm editing that item
                            DialogMessage dialogMessage = new DialogMessage(
                               this,
                               Resources.DeleteCurrentItemMessageBoxText,
                               result => callBackResult(result))
                           {
                               Button = MessageBoxButton.OKCancel,
                               Caption = Resources.ConfirmMessageBoxCaption
                           };

                            eNegMessanger.ConfirmMessage.Send(dialogMessage);

                            #endregion "Confirmation Message"

                            callBackResult = (result) =>
                             {
                                 if (result == MessageBoxResult.Cancel)
                                     return;

                                 #region → Delete Process       .

                                 List<Guid> selectedIDs = new List<Guid>();

                                 foreach (var TypeItem in this.TypeSource.Where(a => a.IsSelected == true))
                                 {
                                     selectedIDs.Add(TypeItem.MessageTypeID);
                                 }

                                 //So it canceled
                                 this.RejectChanges();

                                 while (selectedIDs.Count > 0)
                                 {
                                     MessageType TypeObj = this.TypeSource.Where(s => s.MessageTypeID == selectedIDs[0]).FirstOrDefault();

                                     foreach (var item in TypeObj.NegPhaseMessages)
                                     {
                                         this.MessageSource.Remove(item);
                                     }

                                     this.mMessageTemplateModel.RemoveMessageType(TypeObj);

                                     this.TypeSource.Remove(TypeObj);

                                     selectedIDs.Remove(selectedIDs[0]);

                                 }
                                 this.TypeSource = new List<MessageType>(this.TypeSource);

                                 MessageAppMessanger.SubmitChangesMessage.Send(); ;

                                 #endregion
                             };
                        }
                    },
                    () => !this.mMessageTemplateModel.IsBusy &&
                           this.TypeSource.Where(a => a.IsSelected == true).Count() > 0);
                }

                return mDeleteTypeCommand;
            }
        }

        /// <summary>
        /// Gets the add new phase command.
        /// </summary>
        /// <value>The add new phase command.</value>
        public RelayCommand AddNewPhaseCommand
        {
            get
            {
                if (mAddNewPhaseCommand == null)
                {
                    mAddNewPhaseCommand = new RelayCommand(() =>
                     {

                         if (!this.mMessageTemplateModel.IsBusy && IsAllPhaseValid())
                         {
                             NegotiationPhase tmpPhase = mMessageTemplateModel.AddNegotiationPhase(true);

                             this.PhaseSource.Add(tmpPhase);

                             tmpPhase.PropertyChanged += new PropertyChangedEventHandler(ItemProperties_PropertyChanged);

                             this.PhaseSource = new List<NegotiationPhase>(this.PhaseSource);

                             MessageAppMessanger.EditNegotiationPhase.Send(tmpPhase);
                         }

                     }, () => !this.mMessageTemplateModel.IsBusy);
                }

                return mAddNewPhaseCommand;
            }
        }

        /// <summary>
        /// Gets the add new type command.
        /// </summary>
        /// <value>The add new type command.</value>
        public RelayCommand AddNewTypeCommand
        {
            get
            {
                if (mAddNewTypeCommand == null)
                {
                    mAddNewTypeCommand = new RelayCommand(() =>
                    {

                        if (!this.mMessageTemplateModel.IsBusy && IsAllTypeValid())
                        {

                            MessageType tmpType = mMessageTemplateModel.AddMessageType(true);

                            this.TypeSource.Add(tmpType);

                            tmpType.PropertyChanged += new PropertyChangedEventHandler(ItemProperties_PropertyChanged);

                            this.TypeSource = new List<MessageType>(this.TypeSource);

                            MessageAppMessanger.EditMessageType.Send(tmpType);
                        }

                    }, () => !this.mMessageTemplateModel.IsBusy);
                }

                return mAddNewTypeCommand;
            }
        }

        /// <summary>
        /// Gets the generate email command.
        /// </summary>
        /// <value>The generate email command.</value>
        public RelayCommand GenerateEmailCommand
        {
            get
            {
                if (mGenerateEmailCommand == null)
                {
                    mGenerateEmailCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mMessageTemplateModel.IsBusy)
                            {

                                string content = this.CurrentMessage.MessageContent.Replace("\r\n", "%0A").Replace("\r", "%0A");

                                //string url = string.Format("mailto:{0}?subject={1}&body={2}",
                                //                            MessageAppConfigurations.CurrentLoginUser.EmailAddress,
                                //                            this.CurrentPhase.NegotiationPhaseName + " " + this.CurrentType.MessageTypeName,
                                //                            content);


                                string url = string.Format("mailto:?body={0}", content);


                                MessageAppNavigation.NavigateToUrl(url, true);


                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                            // notify user if there is any error
                            MessageAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.CurrentPhase != null &&
                            this.CurrentType != null &&
                            this.CurrentMessage != null &&
                            !string.IsNullOrEmpty(this.CurrentMessage.MessageContent));
                }
                return mGenerateEmailCommand;
            }
        }

        /// <summary>
        /// Gets the copy to clipboard command.
        /// </summary>
        /// <value>The copy to clipboard command.</value>
        public RelayCommand CopyToClipboardCommand
        {
            get
            {
                if (mCopyToClipboardCommand == null)
                {
                    mCopyToClipboardCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            Clipboard.SetText(this.CurrentMessage.MessageContent.Replace("\r", "\r\n"));
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            MessageAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.CurrentPhase != null &&
                            this.CurrentType != null &&
                            this.CurrentMessage != null &&
                            !string.IsNullOrEmpty(this.CurrentMessage.MessageContent));
                }
                return mCopyToClipboardCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [submit changes].
        /// </summary>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        private void OnSubmitChanges(Boolean flag)
        {
            this.mMessageTemplateModel.SaveChangesAsync();
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            this.SubmitChangeCommand.RaiseCanExecuteChanged();
            this.DeletePhaseCommand.RaiseCanExecuteChanged();
            this.DeleteTypeCommand.RaiseCanExecuteChanged();
            this.GenerateEmailCommand.RaiseCanExecuteChanged();
            this.CopyToClipboardCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Sets the current message.
        /// </summary>
        private void SetCurrentMessage()
        {
            this.RejectChanges();

            NegPhaseMessage msg = null;

            if (this.CurrentPhase != null && this.CurrentType != null)
            {
                msg = this.CurrentPhase
                          .NegPhaseMessages
                          .Where(s => s.MessageTypeID == this.CurrentType.MessageTypeID)
                          .FirstOrDefault();

                if (msg == null)
                    msg = new NegPhaseMessage();
            }

            this.CurrentMessage = msg;

            if (this.CurrentMessage != null)
            {
                this.MessageContent = this.CurrentMessage.MessageContent;
            }
            else
            {
                this.MessageContent = string.Empty;
            }

            this.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Determines whether [is all phase valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is all phase valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllPhaseValid()
        {
            bool isAllValid = true;

            foreach (var phaseItem in PhaseSource)
            {
                phaseItem.ValidationErrors.Clear();

                if (string.IsNullOrEmpty(phaseItem.NegotiationPhaseName))
                {
                    phaseItem.NegotiationPhaseName = string.Empty;
                    isAllValid = false;
                }
                else if (phaseItem.NegotiationPhaseName.Length > 100)
                {
                    phaseItem.ValidationErrors.Add(new ValidationResult(Resources.NameLong, new string[] { "NegotiationPhaseName" }));
                    isAllValid = false;
                }
                else
                {
                    if (PhaseSource.Count(s => (s.NegotiationPhaseName.ToLower() == phaseItem.NegotiationPhaseName.ToLower())
                                            && (s.NegotiationPhaseID != phaseItem.NegotiationPhaseID)) > 0)
                    {
                        (phaseItem as NegotiationPhase).ValidationErrors.Add(new ValidationResult(Resources.RepeatedOne, new string[] { "NegotiationPhaseName" }));
                        isAllValid = false;
                    }
                }

                if (!phaseItem.TryValidateObject())
                {
                    isAllValid = false;
                }

            }

            return isAllValid;
        }

        /// <summary>
        /// Determines whether [is all type valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is all type valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllTypeValid()
        {
            bool isAllValid = true;


            foreach (var TypeItem in TypeSource)
            {
                TypeItem.ValidationErrors.Clear();

                if (string.IsNullOrEmpty(TypeItem.MessageTypeName))
                {
                    TypeItem.MessageTypeName = string.Empty;
                    isAllValid = false;
                }
                else if (TypeItem.MessageTypeName.Length > 100)
                {
                    TypeItem.ValidationErrors.Add(new ValidationResult(Resources.NameLong, new string[] { "MessageTypeName" }));
                    isAllValid = false;
                }
                else
                {
                    if (TypeSource.Count(s => (s.MessageTypeName.ToLower() == TypeItem.MessageTypeName.ToLower())
                                            && (s.MessageTypeID != TypeItem.MessageTypeID)) > 0)
                    {
                        (TypeItem as MessageType).ValidationErrors.Add(new ValidationResult(Resources.RepeatedOne, new string[] { "MessageTypeName" }));
                        isAllValid = false;
                    }
                }

                if (!TypeItem.TryValidateObject())
                {
                    isAllValid = false;
                }

            }

            foreach (var typeItem in TypeSource)
            {
                if (!typeItem.TryValidateObject())
                {
                    isAllValid = false;
                }
            }

            return isAllValid;
        }

        /// <summary>
        /// Determines whether [is message valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is message valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMessageValid()
        {
            bool isAllValid = true;


            if (this.CurrentPhase == null)
            {
                this.CurrentPhase = new NegotiationPhase();
            }

            if (this.CurrentType == null)
            {
                isAllValid = false;
            }

            if (this.CurrentMessage != null &&
                !this.CurrentMessage.TryValidateObject())
            {
                isAllValid = false;
            }

            return isAllValid;
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        private void RejectChanges()
        {
            while (this.PhaseSource != null && this.PhaseSource.Where(a => a.IsSelected == true).Count() > 0)
            {
                this.PhaseSource.Where(a => a.IsSelected == true).First().IsSelected = false;
            }
            while (this.TypeSource != null && this.TypeSource.Where(a => a.IsSelected == true).Count() > 0)
            {
                this.TypeSource.Where(a => a.IsSelected == true).First().IsSelected = false;
            }
            this.mMessageTemplateModel.RejectChanges();

            this.MessageHasChanges = false;
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the negotiation phase async.
        /// </summary>
        public void GetNegotiationPhaseAsync()
        {
            mMessageTemplateModel.GetNegotiationPhaseAsync();
        }

        /// <summary>
        /// Gets the message type async.
        /// </summary>
        public void GetMessageTypeAsync()
        {
            mMessageTemplateModel.GetMessageTypeAsync();
        }

        /// <summary>
        /// Gets the neg phase messages async.
        /// </summary>
        public void GetNegPhaseMessagesAsync()
        {
            mMessageTemplateModel.GetNegPhaseMessagesAsync();
        }

        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            RunQueueForApplyChanges = true;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (!this.IsBusy)
                {
                    RunQueueForApplyChanges = false;

                    this.mMessageTemplateModel.RejectChanges();

                    this.GetNegotiationPhaseAsync();
                }
            });
        }
        #endregion

        #endregion
    }
}