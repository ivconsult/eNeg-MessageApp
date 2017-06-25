
#region → Usings   .

using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Browser;
using System.Threading;
using System.Globalization;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.ClientSide.ExceptionHandling;
using citPOINT.MessageApp.Common;

#endregion

#region → History  .

/* Date         User              Change
 * 21.09.10     Yousra.M       Creation
 * 28.09.10     M.Wahab        XML Comments
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
    /// Mian Class Or Start Point Class
    /// </summary>
    public partial class App : Application
    {
        #region → Fields         .
        /// <summary>
        /// Opened Sub Windows
        /// </summary>
        public static List<object> OpenedSubWindows = new List<object>();
        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            //Initialize used exception handling policies
            ClientExceptionHandlerProvider.RepaireExceptionPolicies();

            // Set the current UI culture.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("En-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("En-US");

            //Register for most used event handlers of App
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Handler for Application Start Up.
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of StartupEventArgs </param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPageView();
        }

        /// <summary>
        /// Handle Application Unhandled Exception
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of ApplicationUnhandledExceptionEventArgs </param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true;

                if (!(e.ExceptionObject is System.InvalidOperationException))
                {
                    Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e.ExceptionObject); });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Used to Report Error To DOM (Mean Html Side)
        /// </summary>
        /// <param name="e">Value of ApplicationUnhandledExceptionEventArgs</param>
        private void ReportErrorToDOM(Exception e)
        {
            try
            {
                string errorMsg = e.Message + e.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Alert("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        #endregion Event Handlers
    }
}
