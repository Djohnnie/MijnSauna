using System;

namespace MijnSauna.Backend.Model.Interfaces
{
    public interface IHasId
    {
        Guid Id { get; set; }
    }
}