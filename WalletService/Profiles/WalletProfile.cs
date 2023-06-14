using AutoMapper;
using WalletService.Dtos;
using WalletService.Models;
namespace WalletService.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<Wallet, WalletMainDto>();
            CreateMap<WalletMainDto, Wallet>();
        }
    }
}