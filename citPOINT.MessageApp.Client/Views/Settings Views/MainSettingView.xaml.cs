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

    public partial class MainSettingView : UserControl, ICleanup
    {
        #region → Constructor    .
        /// <summary>
        /// Initializes a new instance of the <see cref="MainSettingView"/> class.
        /// </summary>
        public MainSettingView()
        {
            InitializeComponent();
        }
        #endregion

        #region → Methods        .

        #region → Public         .

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
