using AutoMapper;
using DATN.DTOs;
using DATN.Models;

namespace DATN.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<DangKyRequest, NguoiDung>();
            CreateMap<KhuVucRequest, KhuVuc>();
            CreateMap<EditUserRequest, NguoiDung>();
            CreateMap<KhoVatTuRequest, KhoVatTu>();
            CreateMap<NhatKyMuaSamRequest, NhatKyMuaSam>();
            CreateMap<NhatKySanXuatRequest, NhatKySanXuat>();
            CreateMap<NhatKySanXuat, NhatKySanXuatEditRequest>();
            CreateMap<NhatKyThuHoachRequest, NhatKyThuHoach>();
            CreateMap<NhatKyThuHoach, NhatKyThuHoachEditRequest>();
            CreateMap<NhatKyBanSanPhamRequest, NhatKyBanSanPham>();
            CreateMap<NhatKyBanSanPham, NhatKyBanSanPhamRequest>();
            CreateMap<NguoiDungRequest, NguoiDung>();
            CreateMap<NguoiDung, NguoiDungRequest>();
            CreateMap<CoSoNuoiTrongRequest, CoSoNuoiTrong>();
            CreateMap<CoSoNuoiTrong, CoSoNuoiTrongRequest>();
        }
    }
}
