using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using System.Reflection.Metadata.Ecma335;

namespace HumanRegisterWebApi.Services.Adapters
{
    public class UserAddressAdapter : IUserAddressAdapter
    {
        public GetUpsertUserAddressDto Bind(UserAddress userAddress)
        {
            return new GetUpsertUserAddressDto()
            {
                City = userAddress.City,
                Street = userAddress.Street,
                HouseNmber = userAddress.HouseNmber,
                ApartamentNumber = userAddress.ApartamentNumber,
            };
        }
        public UserAddress Bind(GetUpsertUserAddressDto dto)
        {
            return new UserAddress()
            {
                City = dto.City,
                Street = dto.Street,
                HouseNmber = dto.HouseNmber,
                ApartamentNumber = dto.ApartamentNumber,
            };
        }
    }
}
/*
 
         public Car Bind(AddCarDto dto)
        {
            return new Car()
            {
                Color = dto.Color,
                Brand = dto.Brand,
                Model = dto.Model,
                ReleaseYear = dto.ReleaseYear                     
            };
        }
 */