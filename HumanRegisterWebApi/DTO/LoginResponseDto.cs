using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.DTO
{
    public record LoginResponseDTO(bool isSuccess, Role? Role = null);//panasiai kipa const
}
