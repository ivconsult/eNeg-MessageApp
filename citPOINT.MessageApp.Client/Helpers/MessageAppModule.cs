#region → Usings   .

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using citPOINT.eNeg.Apps.Common.Interfaces;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using citPOINT.MessageApp.Common;
using citPOINT.MessageApp.Client;
using citPOINT.MessageApp.ViewModel;
using citPOINT.MessageApp.Model;
using citPOINT.MessageApp.Data;
using System.Windows;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 24.04.12     Yousra Reda       Creation
 */

# endregion History

#region → ToDos    .
/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion ToDos

namespace citPOINT.MessageApp.Client
{
    /// <summary>
    /// Message App Module.
    /// </summary>
    [ModuleExport(typeof(MessageAppModule))]
    public class MessageAppModule : IModule
    {
        #region → Fields         .

        private readonly IRegionManager regionManager;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public static CompositionContainer Container { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageAppModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="mainPlatformInfo">The main platform info.</param>
        [ImportingConstructor()]
        public MessageAppModule(IRegionManager regionManager, IMainPlatformInfo mainPlatformInfo)
        {
            this.regionManager = regionManager;

            MessageAppConfigurations.MainPlatformInfo = mainPlatformInfo;

            this.IntializeContainer();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Intializes the container.
        /// </summary>
        private void IntializeContainer()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MessageAppConfigurations).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MessageTemplateViewModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MessageTemplateModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(LoginUser).Assembly));

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(PreferenceSetNeg).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            try
            {
                regionManager.RegisterViewWithRegion
                    (MessageAppConfigurations.AppName.Replace(" ", "") + "Region",
                     typeof(MainPageView));
            }
            catch (System.Exception ex)
            {
                MessageAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, MessageAppConfigurations.AppName);
            }
        }

        #endregion

        #endregion

    }
}