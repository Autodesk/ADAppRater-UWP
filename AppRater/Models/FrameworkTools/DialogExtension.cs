using AppRater.Controls;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppRater.Models.FrameworkTools
{
    /// <summary>
    /// This class contains Extension methods to Dialog classes to accomodate queuing up dialogs if more than one is in current view. 
    /// </summary>
    public static class DialogExtensions
    {
        private static TaskCompletionSource<ContentDialog> _contentDialogShowRequest;
        //private static TaskCompletionSource<MessageDialog> _messageDialogShowRequest;

        /// <summary>
        /// Begins an asynchronous operation showing a dialog.
        /// If another dialog is already shown using ShowAsyncQueue method - it will wait for that previous dialog
        /// to be dismissed before showing the new one.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">This method can only be invoked from UI thread.</exception>
        public static async Task<ContentDialogResult> ShowAsyncQueue(this ContentDialog dialog)
        {
            if (!Window.Current.Dispatcher.HasThreadAccess)
            {
                throw new InvalidOperationException("This method can only be invoked from UI thread.");
            }

            while (_contentDialogShowRequest != null)
            {
                await _contentDialogShowRequest.Task;
            }

            var request = _contentDialogShowRequest = new TaskCompletionSource<ContentDialog>();
            var result = await dialog.ShowAsync();
            _contentDialogShowRequest = null;
            request.SetResult(dialog);

            return result;
        }
    }
}
