using FluentValidation;
using HomeAPI.Contracts.Models.Devices;

namespace HomeAPI.Contracts.Validation
{
    public class EditDeviceRequestValidator : AbstractValidator<EditDeviceRequest>
    {
        public EditDeviceRequestValidator() 
        {
            RuleFor(x => x.NewName).NotEmpty(); 
            RuleFor(x => x.NewRoom).NotEmpty().Must(BeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
        }

        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}