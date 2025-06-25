using FluentValidation;
using KRD.Data.Models;

namespace KRD.Service.Vallidators;

public class OrderValidator:AbstractValidator<Orders>
{
    private readonly AddressValidationService _addressValidationService;
    
    public OrderValidator(AddressValidationService addressValidationService)
    {
        _addressValidationService = addressValidationService;
        
        RuleFor(o => o.BuyerFullName).NotEmpty().WithMessage("ФИО обязательно")
            .Matches(@"^[А-ЯЁ][а-яё]+(?:[\s\-][А-ЯЁ][а-яё]+){2}$")
            .WithMessage("ФИО должно состоять из фамилии, имени и отчества, каждое с заглавной буквы");
        RuleFor(o => o.BuyerAddress).NotEmpty()
            .WithMessage("Адрес не должен быть пустым")
            .MustAsync(async (address, cancellation) =>
                await _addressValidationService.IsAddressValidAsync(address))
            .WithMessage("Адрес не найден");
    }
}