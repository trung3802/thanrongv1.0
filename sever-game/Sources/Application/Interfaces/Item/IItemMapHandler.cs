using System;
using TienKiemV2Remastered.Model.Map;

namespace TienKiemV2Remastered.Application.Interfaces.Item
{
    public interface IItemMapHandler : IDisposable
    {
        void Update(Zone zone);
    }
}