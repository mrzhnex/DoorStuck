using Exiled.API.Interfaces;

namespace DoorStuck
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}