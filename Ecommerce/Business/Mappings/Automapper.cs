using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            // AuthManager Mappings
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            //UserService Mappings
            CreateMap<User, UpdateProfileDTO>().ReverseMap();
            //Slider Manager Mappings
            CreateMap<Slider, SliderDTO>().ReverseMap();
            //Address Manager Mappings
            CreateMap<Address, AddressDTO>().ReverseMap();
            //Brand Manager Mappings
            CreateMap<Brand, BrandDTO>().ReverseMap();
            //Option Manager Mappings
            CreateMap<Option, OptionDTO>().ReverseMap();
            CreateMap<Option, OptionsWithValuesDTO>().ReverseMap();
            //OptionValue Manager Mappings
            CreateMap<OptionValue, OptionValueDTO>().ReverseMap();
            CreateMap<OptionValue, OptionValuesWithOptionDTO>().ReverseMap();
            //Category Manager Mappings
            CreateMap<Category, CategoryDTO>().ReverseMap();
            //Product Manager Mappings
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, AdminProductDetails>().ReverseMap();
            CreateMap<Product, AdminProductDetail>().ReverseMap();
            //Review Manager Mappings
            CreateMap<Review, ReviewDTO>().ReverseMap();
            //Role Manager Mappings 
            CreateMap<Role, RoleDTO>().ReverseMap();
        }

    }
}
