using System.Collections.Generic;

namespace MijnSauna.Backend.Mappers.Interfaces
{
    public interface IMapper<TModel, TDto>
    {
        TModel Map(TDto dto);

        IList<TModel> Map(IList<TDto> dtos);

        TDto Map(TModel model);

        IList<TDto> Map(IList<TModel> models);
    }
}