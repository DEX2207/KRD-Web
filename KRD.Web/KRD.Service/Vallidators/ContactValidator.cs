using FluentValidation;
using KRD.Data.Models;
using PhoneNumbers;

namespace KRD.Service.Vallidators;

public class ContactValidator:AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
            .Must(BeAValidPhoneNumber).WithMessage("Phone number is invalid");
    }

    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var number=phoneUtil.Parse(phoneNumber, "MD");
            return phoneUtil.IsValidNumber(number);
        }
        catch
        {
            return false;
        }
    }
}