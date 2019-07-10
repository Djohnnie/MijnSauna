using System;
using MijnSauna.Backend.Logic.Validation.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Logic.Validation
{
    public class CreateSessionRequestValidator : IValidator<CreateSessionRequest>
    {
        public Boolean Validate(CreateSessionRequest toValidate)
        {
            return true;
        }
    }
}