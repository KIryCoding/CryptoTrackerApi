using AutoMapper;
using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Domain.Entities;

namespace CryptoTrackerApi.Application.Mappings
{
    public class BlockchainMappingProfile : Profile
    {
        public BlockchainMappingProfile()
        {
            // Mapping UTXO Dto
            CreateMap<UtxoBlockchainDataDto, BlockchainData>()
                .ForMember(dest => dest.HighFee, opt => opt.MapFrom(src => src.HighFeePerKb))
                .ForMember(dest => dest.MediumFee, opt => opt.MapFrom(src => src.MediumFeePerKb))
                .ForMember(dest => dest.LowFee, opt => opt.MapFrom(src => src.LowFeePerKb)).ReverseMap();

            // Mapping Account Dto (Ethereum)
            CreateMap<AccountBlockchainDataDto, BlockchainData>()
                .ForMember(dest => dest.HighFee, opt => opt.MapFrom(src => src.HighGasPrice))
                .ForMember(dest => dest.MediumFee, opt => opt.MapFrom(src => src.MediumGasPrice))
                .ForMember(dest => dest.LowFee, opt => opt.MapFrom(src => src.LowGasPrice))
                .ForMember(dest => dest.HighPriorityFee, opt => opt.MapFrom(src => src.HighPriorityFee))
                .ForMember(dest => dest.MediumPriorityFee, opt => opt.MapFrom(src => src.MediumPriorityFee))
                .ForMember(dest => dest.LowPriorityFee, opt => opt.MapFrom(src => src.LowPriorityFee))
                .ForMember(dest => dest.BaseFee, opt => opt.MapFrom(src => src.BaseFee)).ReverseMap();

            CreateMap<BlockchainData, BaseBlockchainDataDto>();
        }
    }
}
