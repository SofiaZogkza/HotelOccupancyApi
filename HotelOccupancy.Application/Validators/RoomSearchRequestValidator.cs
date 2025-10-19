using System.Globalization;
using FluentValidation;
using HotelOccupancy.Domain.Models.DTOs;

namespace HotelOccupancy.Application.Validators;

public class RoomSearchRequestValidator : AbstractValidator<RoomSearchRequest>
{
    public RoomSearchRequestValidator()
    {
        RuleFor(x => x.RoomCode)
            .NotEmpty().WithMessage("Room code is required.")
            .Matches(@"^\d{4}$").WithMessage("Room code must be exactly 4 digits.");
        
        RuleFor(x => x.Date)
            .Must(BeAValidDate)
            .When(x => !string.IsNullOrWhiteSpace(x.Date))
            .WithMessage("Date must be in yyyy-MM-dd format.");
    }
    
    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "yyyy-MM-dd", 
            CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}