namespace TienKiemV2Remastered.Application.Interfaces.Character
{
    public interface IMagicTreeHandler
    {
        void Update(int id);
        void Flush();
        void HandleNgoc();
    }
}