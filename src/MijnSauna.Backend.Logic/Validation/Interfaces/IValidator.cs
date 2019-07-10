using System;

namespace MijnSauna.Backend.Logic.Validation.Interfaces
{
    public interface IValidator<T>
    {
        Boolean Validate(T toValidate);
    }
}