using Microsoft.Extensions.Logging;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace MudaeFarm
{
    public interface INotificationSender
    {
        void sentToast(string s);
    }

    public class NotificationSender : INotificationSender
    {

        readonly ILogger<NotificationSender> _logger;
        private ToastNotifier notifier;
        public NotificationSender(ILogger<NotificationSender> logger){
            notifier = ToastNotificationManager.CreateToastNotifier("MuadeFarm");
            _logger = logger;
        }

        public void sentToast(string s){
            try {
                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
                XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                toastTextElements[0].AppendChild(toastXml.CreateTextNode(s));
                ToastNotification notification = new ToastNotification(toastXml);
                _logger.LogDebug($"Senting Windows toast notification.");
                notifier.Show(notification);
            } catch {
                _logger.LogWarning($"Failed to sent Windows toast notification.");
            }
        }
    }
}