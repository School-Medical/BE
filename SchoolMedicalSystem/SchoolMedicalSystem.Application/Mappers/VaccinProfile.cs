using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class VaccinProfile : Profile
    {
        public VaccinProfile()
        {
            #region VaccinConCampaign
            CreateMap<VaccinCampaign, VaccinCampaignDTOResponse>()
                .ForMember(dest => dest.VaccinCampaignId, opt => opt.MapFrom(src => src.vaccin_campaign_id))
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.campaign_name))
                .ForMember(dest => dest.CampaignDescription, opt => opt.MapFrom(src => src.campaign_description))
                .ForMember(dest => dest.StartAt, opt => opt.MapFrom(src => src.start_at))
                .ForMember(dest => dest.EndAt, opt => opt.MapFrom(src => src.end_at))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.location))
                .ForMember(dest => dest.VaccinName, opt => opt.MapFrom(src => src.vaccin_name))
                .ForMember(dest => dest.VaccinDescription, opt => opt.MapFrom(src => src.vaccin_description))
                .ForMember(dest => dest.RegisterStart, opt => opt.MapFrom(src => src.register_start))
                .ForMember(dest => dest.RegisterClose, opt => opt.MapFrom(src => src.register_close))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.VaccinNotice, opt => opt.MapFrom(src => src.vaccin_notice));

            CreateMap<VaccinCampaignDTORequest, VaccinCampaign>()
                .ForMember(dest => dest.vaccin_campaign_id, opt => opt.Ignore())
                .ForMember(dest => dest.campaign_name, opt => opt.MapFrom(src => src.CampaignName))
                .ForMember(dest => dest.campaign_description, opt => opt.MapFrom(src => src.CampaignDescription))
                .ForMember(dest => dest.start_at, opt => opt.MapFrom(src => src.StartAt))
                .ForMember(dest => dest.end_at, opt => opt.MapFrom(src => src.EndAt))
                .ForMember(dest => dest.location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.vaccin_name, opt => opt.MapFrom(src => src.VaccinName))
                .ForMember(dest => dest.vaccin_description, opt => opt.MapFrom(src => src.VaccinDescription))
                .ForMember(dest => dest.register_start, opt => opt.MapFrom(src => src.RegisterStart))
                .ForMember(dest => dest.register_close, opt => opt.MapFrom(src => src.RegisterClose))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.vaccin_notice, opt => opt.MapFrom(src => src.VaccinNotice));
            #endregion

            #region VaccinConfirmation
            CreateMap<VaccinConfirmation, VaccinConfirmationDTOResponse>()
                .ForMember(dest => dest.VaccinConfirmationId, opt => opt.MapFrom(src => src.vaccin_confirmation_id))
                .ForMember(dest => dest.SubmitAt, opt => opt.MapFrom(src => src.submit_at))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.message))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parent_id))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.parent!.first_name + " " + src.parent!.last_name))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.student!.first_name + " " + src.student!.last_name))
                .ForMember(dest => dest.CampaignId, opt => opt.MapFrom(src => src.campaign_id))
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.campaign!.campaign_name))
                .ForMember(dest => dest.VaccinName, opt => opt.MapFrom(src => src.campaign!.vaccin_name));

            CreateMap<VaccinConfirmationDTORequest, VaccinConfirmation>()
                .ForMember(dest => dest.vaccin_confirmation_id, opt => opt.Ignore())
                .ForMember(dest => dest.submit_at, opt => opt.MapFrom(src => src.SubmitAt))
                .ForMember(dest => dest.message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.parent_id, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.campaign_id, opt => opt.MapFrom(src => src.CampaignId));
            #endregion

            #region vaccinDocument
            CreateMap<VaccinDocument, VaccinDocumentDTOResponse>()
                .ForMember(dest => dest.VaccinDocumentId, opt => opt.MapFrom(src => src.vaccin_document_id))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.student!.first_name + " " + src.student!.last_name))
                .ForMember(dest => dest.CampaignId, opt => opt.MapFrom(src => src.campaign_id))
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.campaign!.campaign_name))
                .ForMember(dest => dest.VaccinConfirmId, opt => opt.MapFrom(src => src.vaccin_confirm_id))
                .ForMember(src => src.VaccinConfirmStatus, opt => opt.MapFrom(src => src.vaccin_confirm!.status))
                .ForMember(src => src.Message, opt => opt.MapFrom(src => src.vaccin_confirm!.message))
                .ForMember(dest => dest.SubmitAt, opt => opt.MapFrom(src => src.vaccin_confirm!.submit_at))
                .ForMember(dest => dest.VaccinName, opt => opt.MapFrom(src => src.campaign!.vaccin_name))
                .ForMember(dest => dest.NurseId, opt => opt.MapFrom(src => src.nurse_id))
                .ForMember(dest => dest.NurseName, opt => opt.MapFrom(src => src.nurse!.first_name + " " + src.nurse!.last_name))
                .ForMember(dest => dest.PreConditionVaccin, opt => opt.MapFrom(src => src.pre_condition_vaccin))
                .ForMember(dest => dest.InjectionTime, opt => opt.MapFrom(src => src.injection_time))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.duration))
                .ForMember(dest => dest.PostConditionVaccin, opt => opt.MapFrom(src => src.post_condition_vaccin))
                .ForMember(dest => dest.AdverseSymptoms, opt => opt.MapFrom(src => src.adverse_symptoms))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status));

            CreateMap<VaccinDocumentDTORequest, VaccinDocument>()
                .ForMember(dest => dest.vaccin_document_id, opt => opt.Ignore())
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.campaign_id, opt => opt.MapFrom(src => src.CampaignId))
                .ForMember(dest => dest.vaccin_confirm_id, opt => opt.MapFrom(src => src.VaccinConfirmId))
                .ForMember(dest => dest.nurse_id, opt => opt.MapFrom(src => src.NurseId))
                .ForMember(dest => dest.pre_condition_vaccin, opt => opt.MapFrom(src => src.PreConditionVaccin))
                .ForMember(dest => dest.injection_time, opt => opt.MapFrom(src => src.InjectionTime))
                .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.post_condition_vaccin, opt => opt.MapFrom(src => src.PostConditionVaccin))
                .ForMember(dest => dest.adverse_symptoms, opt => opt.MapFrom(src => src.AdverseSymptoms))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));
            #endregion
        }
    }
}
