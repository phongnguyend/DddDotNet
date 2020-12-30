using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Web
{
    public interface IWebNotification<T>
    {
        void Send(T message);
        Task SendAsync(T message);
    }
}
