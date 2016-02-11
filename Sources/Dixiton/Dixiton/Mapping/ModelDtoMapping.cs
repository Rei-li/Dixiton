using AutoMapper;
using Dixiton.Common.Helpers;
using Dixiton.Common.Interfaces;
using Dixiton.Dtos;
using Dixiton.Models;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dixiton.Mapping
{
    public sealed class ModelDtoMapping
    {
        private readonly IResourceProvider _resourceProvider;

        public ModelDtoMapping(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;

            Mapper.CreateMap<RegisterModel, UserDto>().
                ForMember(model => model.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(model => model.Login, opt => opt.MapFrom(dto => dto.Login))
                .ForMember(model => model.Password, opt => opt.MapFrom(dto => dto.Password))
                .ForMember(model => model.ConfirmPassword, opt => opt.MapFrom(dto => dto.ConfirmPassword))
                .ForMember(model => model.Email, opt => opt.MapFrom(dto => dto.Email));
            Mapper.CreateMap<UserDto, RegisterModel>();
            Mapper.CreateMap<LoginModel, UserDto>();


            //Mapper.CreateMap<ActivityModel, ActivityDto>()
            //    .ForMember(dto => dto.ActivityType, opt => opt.MapFrom(src => src.SelectedType));
            //Mapper.CreateMap<EmployeeModel, EmployeeDto>();
            //Mapper.CreateMap<EmployeeModel, EmployeeListDto>();
            //Mapper.CreateMap<ProjectModel, ProjectDto>();
            //Mapper.CreateMap<ProjectModel, ProjectListDto>();
            //Mapper.CreateMap<ProjectRoleModel, ProjectRoleDto>();
            //Mapper.CreateMap<PeriodModel, PeriodDto>()
            //    .ForMember(ent => ent.Date, opt => opt.MapFrom(src => DateHelper.ParseMonthDate(src.Date)));
            //Mapper.CreateMap<PeriodDto, PeriodModel>()
            //  .ForMember(ent => ent.Date, opt => opt.MapFrom(src => DateHelper.ToMonthYearString(src.Date)));

            //Mapper.CreateMap<ActivityReportModel, ActivityDto>();
            //Mapper.CreateMap<CustomerModel, CustomerDto>();
            //Mapper.CreateMap<EmployeeMonthlyInfoModel, EmployeeMonthlyInfoDto>()
            //    .ForMember(ent => ent.Month, opt => opt.MapFrom(src => DateHelper.ParseMonthDate(src.Month)));
            //Mapper.CreateMap<EmployeeMonthlyInfoDto, EmployeeMonthlyInfoModel>()
            //    .ForMember(ent => ent.Month, opt => opt.MapFrom(src => src.Month.ToMonthYearString()));

            //Mapper.CreateMap<LookupModel, LookupDto>();
            //Mapper.CreateMap<TransferActivityModel, ActivityDto>();
            //Mapper.CreateMap<TransferActivityModel, TransferActivityDto>();
            //Mapper.CreateMap<ResourceCostForPeriodModel, ResourceCostForPeriodDto>();
            //Mapper.CreateMap<MonthlyCostModel, MonthlyCostDto>()
            //         .ForMember(ent => ent.Month, opt => opt.MapFrom(src => DateHelper.ParseMonthDate(src.Month)));
            //Mapper.CreateMap<MonthlyCostDto, MonthlyCostModel>()
            //     .ForMember(ent => ent.Month, opt => opt.MapFrom(src => src.Month.ToMonthYearString()));

            //Mapper.CreateMap<DepartmentModel, DepartmentDto>();
            //Mapper.CreateMap<DepartmentModel, DepartmentListDto>();

            //Mapper.CreateMap<ActivityDto, ActivityModel>();
            //Mapper.CreateMap<ActivityDto, TransferActivityModel>();
            //Mapper.CreateMap<TransferActivityDto, TransferActivityModel>();
            //Mapper.CreateMap<EmployeeDto, EmployeeModel>();
            //Mapper.CreateMap<EmployeeListDto, EmployeeModel>();
            //Mapper.CreateMap<ProjectDto, ProjectModel>();
            //Mapper.CreateMap<ProjectListDto, ProjectModel>();
            //Mapper.CreateMap<ProjectRoleDto, ProjectRoleModel>();
            //Mapper.CreateMap<CustomerDto, CustomerModel>();

            //Mapper.CreateMap<LookupDto, LookupModel>();
            //Mapper.CreateMap<ActivityBaseDto, ActivityProjectReportModel>()
            //    .ForMember(ent => ent.ProjectSelectListItems, opt => opt.MapFrom(src => src.ProjectsDictionary));
            //Mapper.CreateMap<ActivityBaseDto, ActivityEmployeeReportModel>()
            //   .ForMember(ent => ent.EmployeeSelectListItems, opt => opt.MapFrom(src => src.EmployeesDictionary));
            //Mapper.CreateMap<ActivityTypeDto, ActivityTypeModel>();
            //Mapper.CreateMap<ProjectActivitiesReportDto, ProjectActivitiesReportModel>()
            //    .ForMember(ent => ent.DateFrom, opt => opt.MapFrom(src => DateHelper.ParseDate(src.DateFrom)))
            //    .ForMember(ent => ent.DateTo, opt => opt.MapFrom(src => DateHelper.ParseDate(src.DateTo)));
            //Mapper.CreateMap<ProjectActivitiesReportDto, EmployeeActivitiesReportModel>()
            //  .ForMember(ent => ent.DateFrom, opt => opt.MapFrom(src => DateHelper.ParseDate(src.DateFrom)))
            //  .ForMember(ent => ent.DateTo, opt => opt.MapFrom(src => DateHelper.ParseDate(src.DateTo)));
            //Mapper.CreateMap<ActivityDto, ActivityReportModel>()
            //    .ForMember(ent => ent.Date, opt => opt.MapFrom(src => DateHelper.ParseDate(src.Date)));
            //Mapper.CreateMap<BonusCalculationResultModel, BonusCalculationResultDto>();
            //Mapper.CreateMap<BonusCalculationResultDto, BonusCalculationResultModel>();
            //Mapper.CreateMap<ResourceCostForPeriodDto, ResourceCostForPeriodModel>();

            //Mapper.CreateMap<DepartmentDto, DepartmentModel>();
            //Mapper.CreateMap<DepartmentListDto, DepartmentModel>();

            //Mapper.CreateMap<KeyValuePair<int, string>, SelectListItem>()
            // .ForMember(ent => ent.Text, opt => opt.MapFrom(src => src.Value))
            // .ForMember(ent => ent.Value, opt => opt.MapFrom(src => src.Key));

            //Mapper.CreateMap<LookupDto, SelectListItem>()
            //    .ForMember(ent => ent.Text, opt => opt.MapFrom(src => src.Value))
            //    .ForMember(ent => ent.Value, opt => opt.MapFrom(src => src.Id));

            //Mapper.CreateMap<ProjectRoleDto, SelectListItem>()
            //    .ForMember(model => model.Value, opt => opt.MapFrom(dto => dto.Id))
            //    .ForMember(model => model.Text, opt => opt.MapFrom(dto => dto.Name));

            //Mapper.CreateMap<ViewEstimationDto, ShowEstimateModel>()
            //    .ForMember(model => model.ProjectId, opt => opt.MapFrom(dto => dto.Id))
            //    .ForMember(model => model.Testing, opt => opt.MapFrom(dto => dto.ProjectRoles))
            //    .ForMember(model => model.Development, opt => opt.MapFrom(dto => dto.ProjectRoles))
            //    .ForMember(model => model.Investigation, opt => opt.MapFrom(dto => dto.ProjectRoles));

            //Mapper.CreateMap<ShowEstimateModel, SaveViewEstimationDto>()
            //    .ForMember(dto => dto.Id, opt => opt.MapFrom(model => model.ProjectId))
            //    .ForMember(dto => dto.Development, opt => opt.MapFrom(model => model.Development.Where(i => i.Selected).Select(i => int.Parse(i.Value)).ToArray()))
            //    .ForMember(dto => dto.Investigation, opt => opt.MapFrom(model => model.Investigation.Where(i => i.Selected).Select(i => int.Parse(i.Value)).ToArray()))
            //    .ForMember(dto => dto.Testing, opt => opt.MapFrom(model => model.Testing.Where(i => i.Selected).Select(i => int.Parse(i.Value)).ToArray()));

            //Mapper.CreateMap<ViewEstimationListDto, ProjectsEstimationModel>()
            //    .ForMember(dto => dto.RegistrationDate, opt => opt.MapFrom(model => DateHelper.ParseDate(model.RegistrationDate)));

            //Mapper.CreateMap<Dtos.Report_FinancialAnalysis.ViewFinancialAnalysisListDto, FinancialAnalysisModel>()
            //    .ForMember(dto => dto.State, opt => opt.MapFrom(model => EnumHelper.GetEnumDisplayName(typeof(State), (int)model.State, _resourceProvider)))
            //    .ForMember(dto => dto.ProjectType, opt => opt.MapFrom(model => EnumHelper.GetEnumDisplayName(typeof(ProjectType), (int)model.ProjectType, _resourceProvider)))
            //    .ForMember(dto => dto.RegistrationDate, opt => opt.MapFrom(model => DateHelper.ParseDate(model.RegistrationDate)));



           // EmployeeSalaryInfoDuringPeriodMapping();
        }

        //private void EmployeeSalaryInfoDuringPeriodMapping()
        //{
        //    Mapper.CreateMap<EmployeeSalaryInfoDuringPeriodDto, EmployeeSalaryInfoDuringPeriodModel>()
        //        .ForMember(ent => ent.BonusesSumForPeriod,
        //            opt => opt.MapFrom(src => src.MonthlySalaryInfos.Sum(x => x.BonusList.Sum(y => y.Bonus))));
        //    Mapper.CreateMap<MonthlySalaryInfoDto, MonthlySalaryInfoModel>()
        //        .ForMember(ent => ent.BonusesSum, opt => opt.MapFrom(src => src.BonusList.Sum(x => x.Bonus)))
        //        .ForMember(ent => ent.Month, opt => opt.MapFrom(src => src.Month.ToMonthYearString()));
        //    Mapper.CreateMap<BonusInfoDto, BonusInfoModel>();
        //}
    }
}