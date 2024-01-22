using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Interfaces.Map;
using TienKiemV2Remastered.Application.IO;

namespace TienKiemV2Remastered.Application.Interfaces.Client
{
    public interface IMessageHandler
    {
        void OnConnectionFail(ISession_ME client, bool isMain);

        void OnConnectOK(ISession_ME client, bool isMain);

        void OnDisconnected(ISession_ME client, bool isMain);

        Task OnMessage(Message message);
    }
}