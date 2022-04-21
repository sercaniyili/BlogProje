using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator: AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori başlığını boş geçemezsiniz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Kategori açıklamasını boş geçemezsiniz");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olmalıdır");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Kategori adı en az 5 karakter olmalıdır");
        }


    }
}
