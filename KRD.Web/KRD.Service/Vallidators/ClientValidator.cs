using System.Data;
using FluentValidation;
using KRD.Data.Models;

namespace KRD.Service.Vallidators;

public class ClientValidator:AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(c=>c.FullName).NotEmpty().WithMessage("Имя клиента не должно быть пустым");
        RuleFor(c=>c.Address).NotNull().WithMessage("Адрес не должен быть пустым");
    }
}