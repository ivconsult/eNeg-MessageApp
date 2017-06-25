
#region → Usings   .
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight;
using citPOINT.MessageApp.ViewModel;
#endregion

#region → History  .

/* Date         User          Change
 * 
 * 24.04.12     Yousra Reda         Creation
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
    /// View Model Repository.
    /// Shared View Models forcing that all view models are intialized.
    /// </summary>
    public class ViewModelRepository : ICleanup
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the message template view model.
        /// </summary>
        /// <value>The message template view model.</value>
        [Import(MessageApp.Common.MessageAppViewModelTypes.MessageTemplateViewModel)]
        public MessageTemplateViewModel MessageTemplateViewModel { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRepository"/> class.
        /// </summary>
        [ImportingConstructor]
        public ViewModelRepository()
        {
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                MessageAppModule.Container.SatisfyImportsOnce(this);
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.MessageTemplateViewModel.Cleanup();
        }

        #endregion
    }
}
