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
    public partial class ManagePhasesView : UserControl,ICleanup
    {
        #region → Constructor    .
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageNegotiationPhases"/> class.
        /// </summary>
        public ManagePhasesView()
        {
            InitializeComponent();

            #region Registration for needed messages in MessageAppMessanger
            MessageAppMessanger.EditNegotiationPhase.Register(this, OnAddNewPhase);
            #endregion
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [add new phase].
        /// </summary>
        /// <param name="negPhase">The neg phase.</param>
        private void OnAddNewPhase(NegotiationPhase negPhase)
        {
            var NewPhase = uxPhasesGridView.Items[uxPhasesGridView.Items.Count - 1];
            FocusCertainPhase(NewPhase, true);
        }

        /// <summary>
        /// Focuses the certain phase.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="SelectAll">if set to <c>true</c> [select all].</param>
        private void FocusCertainPhase(object item, bool SelectAll)
        {
            uxPhasesGridView.ScrollIntoViewAsync(item, s =>
            {
                var row = s as GridViewRow;
                if (row != null)
                {
                    row.IsCurrent = true;
                    row.Focus();
                    row.Cells[1].Focus();
                    TextBox CurrentCell = (row.Cells[1].Content as TextBox);
                    if (SelectAll)
                    {
                        CurrentCell.SelectAll();
                    }
                    else
                    {
                        CurrentCell.Select(CurrentCell.Text.Length, 0);
                    }
                }
            });
        }

        #endregion

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
