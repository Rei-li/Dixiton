using AutoMapper;
using Dixiton.DataAccess.Entities;
using Dixiton.Dtos;

namespace Dixiton.DataAccess.AutomapperMappings
{
    /// <summary>
    /// Automapper configuration Entity->Dto & Dto->Entity
    /// </summary>
    public class EntityDtoMappings
    {
        public EntityDtoMappings()
        {
            UserMapping();
            //EmployeeMapping();
            //CustomerMapping();
            //UserMapping();
            //ActivityMapping();
            //PeriodMapping();
            //EmployeeMonthlyInfoMapping();
            //UserRoleMapping();
            //DepartmentMapping();
            //Report_EstimationAnalysisMapping();
            //Report_FinancialAnalysisMapping();
        }

        private void UserMapping()
        {
            Mapper.CreateMap<UserEntity, UserDto>();
            Mapper.CreateMap<UserDto, UserEntity>();

        }

        //private void EmployeeMapping()
        //{
        //    Mapper.CreateMap<EmployeeEntity, EmployeeDto>();
        //    Mapper.CreateMap<EmployeeDto, EmployeeEntity>();

        //    Mapper.CreateMap<EmployeeEntity, EmployeeListDto>();
        //    Mapper.CreateMap<EmployeeListDto, EmployeeEntity>();
        //}

        //private void UserMapping()
        //{
        //    Mapper.CreateMap<UserEntity, UserDto>();
        //    Mapper.CreateMap<UserDto, UserEntity>();

        //    Mapper.CreateMap<UserEntity, UserListDto>();
        //    Mapper.CreateMap<UserListDto, UserEntity>();
        //}

        //private void PeriodMapping()
        //{
        //    Mapper.CreateMap<PeriodEntity, PeriodDto>();
        //    Mapper.CreateMap<PeriodDto, PeriodEntity>();
        //}

        // private void ActivityMapping()
        // {
        //     Mapper.CreateMap<ActivityDto, ActivityEntity>();
        //     Mapper.CreateMap<ActivityEntity, EmployeeMonthlyBonusInfoDto>();  
        // }


        // private void CustomerMapping()
        // {
        //     Mapper.CreateMap<CustomerDto, CustomerEntity>();
        //     Mapper.CreateMap<CustomerEntity, CustomerDto>();
        // }

        // private void EmployeeMonthlyInfoMapping()
        // {
        //     Mapper.CreateMap<EmployeeMonthlyInfoDto, EmployeeMonthlyInfoEntity>();
        //     Mapper.CreateMap<EmployeeMonthlyInfoEntity, EmployeeMonthlyInfoDto>();
        // }

        // private void UserRoleMapping()
        // {
        //     Mapper.CreateMap<UserRoleDto, UserRoleEntity>();
        //     Mapper.CreateMap<UserRoleEntity, UserRoleDto>();
        // }

        //private void DepartmentMapping()
        //{
        //    Mapper.CreateMap<DepartmentEntity, DepartmentDto>();
        //    Mapper.CreateMap<DepartmentDto, DepartmentEntity>();

        //    Mapper.CreateMap<DepartmentEntity, DepartmentListDto>();
        //    Mapper.CreateMap<DepartmentListDto, DepartmentEntity>();
        //}
        //private void Report_EstimationAnalysisMapping()
        //{
        //    Mapper.CreateMap<Report_EstimationAnalysisEntity, SaveEstimationDto>();
        //    Mapper.CreateMap<SaveEstimationDto, Report_EstimationAnalysisEntity>();

        //    Mapper.CreateMap<Report_EstimationAnalysisEntity, SaveViewEstimationDto>();
        //    Mapper.CreateMap<SaveViewEstimationDto, Report_EstimationAnalysisEntity>();

        //    Mapper.CreateMap<Report_EstimationAnalysisEntity, ViewEstimationDto>();
        //    Mapper.CreateMap<ViewEstimationDto, Report_EstimationAnalysisEntity>();

        //    Mapper.CreateMap<Report_EstimationAnalysisEntity, ViewEstimationListDto>();
        //    Mapper.CreateMap<ViewEstimationListDto, Report_EstimationAnalysisEntity>();
        //}

        //private void Report_FinancialAnalysisMapping()
        //{
        //    Mapper.CreateMap<Report_FinancialAnalysisEntity, SaveFinancialAnalysisDto>();
        //    Mapper.CreateMap<SaveFinancialAnalysisDto, Report_FinancialAnalysisEntity>();

        //    Mapper.CreateMap<Report_FinancialAnalysisEntity, ViewFinancialAnalysisListDto>();
        //    Mapper.CreateMap<ViewFinancialAnalysisListDto, Report_FinancialAnalysisEntity>();
        //}
    }
}