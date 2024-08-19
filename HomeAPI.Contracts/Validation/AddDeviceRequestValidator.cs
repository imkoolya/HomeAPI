using FluentValidation;
using HomeAPI.Contracts.Models.Devices;

namespace HomeAPI.Contracts.Validation
{
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        public AddDeviceRequestValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts).NotEmpty().InclusiveBetween(120, 220);
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.RoomLocation).NotEmpty().Must(BeSupported).WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
        }

        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}