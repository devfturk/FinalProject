using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    //
    public class ProductValidator : AbstractValidator<Product>//
    {
        public ProductValidator() // kurallar ctor içine yazılır (DTO içinde olabilir.)
        {
            RuleFor(p => p.ProductName).NotEmpty(); //boş olamaz
            RuleFor(p => p.ProductName).MinimumLength(2);//min 2 karakter olmalı
            RuleFor(p => p.UnitPrice).NotEmpty();//boş olamaz
            RuleFor(p => p.UnitPrice).GreaterThan(0);//0'dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//Category id 1(içecek kategorisinde) olduğunda unitprice 10 yada daha fazla olmalı
            RuleFor(p => p.ProductName).Must(StartsWithA).WithMessage("Ürünler A harfi ile başlamalı");//ProductName
            //ürünlerimin ismi a ile başlamalı. Must içine true false dönen bir fonksiyon yazdık. (generate method)
            //with message-> hata durumunda istediğin mesajı vermeni sağlar.

        }

        private bool StartsWithA(string arg)//arg productname
        {
            return arg.StartsWith("A");//A' ile başlıyorsa true döner.
        }
    }
}
