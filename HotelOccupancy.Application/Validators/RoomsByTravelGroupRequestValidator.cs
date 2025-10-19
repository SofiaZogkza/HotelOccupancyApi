using FluentValidation;
using HotelOccupancy.Domain.Models.DTOs;

namespace HotelOccupancy.Application.Validators;

public class RoomsByTravelGroupRequestValidator : AbstractValidator<RoomsByTravelGroupRequest>
{
    public RoomsByTravelGroupRequestValidator()
    {
        RuleFor(x => x.GroupId)
            .NotEmpty().WithMessage("GroupId cannot be empty")
            .Matches(@"^[1-9][0-9A-Z]{5}$") // starts with 1-9, 6 chars, max 2 letters
            .WithMessage("GroupId must be 6 characters, can include max 2 letters, and cannot start with 0");
    }
}