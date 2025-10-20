using FluentValidation;
using HotelOccupancy.Domain.Models.DTOs;

namespace HotelOccupancy.Application.Validators;

public class RoomsByTravelGroupRequestValidator : AbstractValidator<RoomsByTravelGroupRequest>
{
    public RoomsByTravelGroupRequestValidator()
    {
        RuleFor(x => x.GroupId)
            .NotEmpty().WithMessage("GroupId cannot be empty")
            .Matches(@"^(?!0)[A-Z0-9]{6}$")
            .WithMessage("GroupId must be 6 characters, can include upper-case letters and digits, cannot start with 0")
            .Must(HaveOneOrTwoLetters).WithMessage("GroupId must contain at least 1 and at most 2 letters");
    }

    private bool HaveOneOrTwoLetters(string groupId)
    {
        if (string.IsNullOrEmpty(groupId)) return false;
        int letters = groupId.Count(char.IsLetter);
        return letters >= 1 && letters <= 2;
    }
}