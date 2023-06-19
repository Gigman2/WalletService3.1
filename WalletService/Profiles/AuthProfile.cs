using AutoMapper;
using WalletService.Dtos;
using WalletService.Models;

namespace WalletService.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Auth, AuthMainDto>();
            CreateMap<AuthInsertDto, Auth>();
        }
    }
}