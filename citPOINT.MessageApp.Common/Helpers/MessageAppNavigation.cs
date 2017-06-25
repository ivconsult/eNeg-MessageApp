#region → Usings   .
using System;
using System.Windows.Controls;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 12.12.11     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion

namespace citPOINT.MessageApp.Common
{
    /// <summary>
    /// for navigating to Any url in case of web platform or addon and OOB Add-on
    /// </summary>
    public class MessageAppNavigation
    {

        private class Navigation : HyperlinkButton
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="Navigation"/> class.
            /// </summary>
            /// <param name="NavigateUri">The navigate URI.</param>
            /// <param name="IsBlank">if set to <c>true</c> [is blank].</param>
            internal Navigation(string NavigateUri, bool IsBlank = false)
            {
                    base.NavigateUri = new Uri(NavigateUri);
                    if (IsBlank)
                        TargetName = "_blank";
                    else
                        TargetName = "_self";
                    base.OnClick();
            }
        }

        /// <summary>
        /// Navigates to URL.
        /// </summary>
        /// <param name="NavigateUri">The navigate URI.</param>
        /// <param name="IsBlank">if set to <c>true</c> [is blank].</param>
        public static void NavigateToUrl(string NavigateUri, bool IsBlank = false)
        {
            Navigation MessageAppNavigation = new Navigation(NavigateUri, IsBlank);
        }
    }
}
