using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {

        public WriterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Yazar adı soyadı boş geçilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail adresi boş geçilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçileme<");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapınız");
        }
      

    }
}
