using FluentValidation;

namespace Ecommerce.Models.Suppliers.AddSupplier
{
    public class AddSupplierModelValidator : AbstractValidator<AddSupplierModel>
    {
        public AddSupplierModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}
