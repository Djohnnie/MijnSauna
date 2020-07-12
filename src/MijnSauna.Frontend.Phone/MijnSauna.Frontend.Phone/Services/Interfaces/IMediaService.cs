using System;
using MijnSauna.Frontend.Phone.Services.Models;

namespace MijnSauna.Frontend.Phone.Services.Interfaces
{
    public interface IMediaService
    {
        void RegisterCallback(Action<MediaInfo> callback);
    }
}