using System;
using System.Web.Http;
using Common.Logging;
using Dixiton.Logic;
//using AutoMapper;
//using Dixiton.Dtos.Enums;
//using Dixiton.Web.Attributes.ActionFilters;
//using Dixiton.Web.Models;

//using Resources;

namespace Dixiton.Controllers.Api
{
    /// <summary>
    /// Web API implementation for Activity (using Web API 2)
    /// </summary>
    [RoutePrefix("api/activities")]
    public class ActivityApiController : ApiController
    {

        #region Constants
        private const String CONTROLLER_NAME_ACTIVITIES = "#activities";
        #endregion Constants

        #region Fields
        private ILog _log = LogManager.GetLogger(typeof(ActivityApiController));
        #endregion Fields

        #region Properties

        /// <summary>
        /// Interface for commands dispatchering
        /// </summary>
        public ICommandDispatcher CommandQueryDispatcher { get; set; }

        #endregion Properties

        #region Actions

        //// GET api/<controller>
        ///// <summary>
        ///// Get all activities with filter (UserId, dateFrom, dateTo, activityTypes)
        ///// </summary>
        ///// <returns></returns>
        //[Route("")]
        //public IHttpActionResult Get(int UserId = 0, DateTime? dateFrom = null, DateTime? dateTo = null, [FromUri]int[] activityTypes = null)
        //{
        //    if (activityTypes == null || activityTypes.Length == 0)
        //    {
        //        activityTypes = new[]
        //        {
        //            (int)ActivityType.Undefined,
        //            (int)ActivityType.TransferBetweenUsersGet,
        //            (int)ActivityType.TransferBetweenUsersPut,
        //            (int)ActivityType.Workload,
        //            (int)ActivityType.Bonus,
        //            (int)ActivityType.Income,
        //            (int)ActivityType.UserPayments,
        //            (int)ActivityType.Overworks
        //        };
        //    }
        //    var query = new GetActivitiesForUserQuery
        //    {
        //        Types = activityTypes.ToList(),
        //        UserId = UserId,
        //        DateFrom = dateFrom,
        //        DateTo = dateTo
        //    };

        //    var dto = (UserActivitiesReportDto)CommandQueryDispatcher.ExecuteQuery(query).Data;

        //    return Ok(dto);
        //}

        //// GET api/<controller>/5  
        ///// <summary>
        ///// Get Activity by Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[Route("{id:int}")]
        //public IHttpActionResult Get(int id)
        //{
        //    var query = new GetActivityQuery { ActivityId = id };
        //    var result = CommandQueryDispatcher.ExecuteQuery(query);
        //    if (result.Data == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok((ActivityDto)result.Data);
        //}


        //// GET api/<controller>/ByUser/5
        ///// <summary>
        ///// Get all Activities by User Id
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //[Route("~/api/Users/{UserId}/activities")]
        //[HttpGet]
        //public IHttpActionResult GetActivitiesByUser(int UserId)
        //{
        //    return Get(UserId, null); // call Get all activities with filter (UserId, dateFrom, dateTo, activityTypes)
        //}

        //// POST api/<controller>
        ///// <summary>
        ///// Create new Activity
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[Route("")]
        //[ValidateModel]
        //public IHttpActionResult Post(ActivityModel model)
        //{
        //    if (model == null) // add more check here
        //        return BadRequest();

        //    try
        //    {
        //        var activityDto = Mapper.Map<ActivityModel, ActivityDto>(model);
        //        var saveActivityCommamd = new SaveActivityCommand { ActivityDto = activityDto };
        //        CommandQueryDispatcher.ExecuteCommand(saveActivityCommamd);
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO add log
        //        _log.ErrorFormat("ActivityApiController: Error during save (Post) ActivityDto. {0}", ex);
        //        return InternalServerError(ex);
        //    }

        //    var url = Url.Link(ApiContsts.CONFIG_DEFAULT_API, new { controller = CONTROLLER_NAME_ACTIVITIES, id = model.Id });
        //    var uri = new Uri(url);

        //    return Created(uri, model);

        //}

        //// PUT api/<controller>/5
        ///// <summary>
        ///// Update existing Activity
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[Route("{id:int}")]
        //[ValidateModel]
        //public IHttpActionResult Put(ActivityModel model)
        //{
        //    // the same as POST ?
        //    if (model == null) // add more check here
        //        return BadRequest();

        //    try
        //    {
        //        var activityDto = Mapper.Map<ActivityModel, ActivityDto>(model);
        //        var saveActivityCommamd = new SaveActivityCommand { ActivityDto = activityDto };
        //        var result = CommandQueryDispatcher.ExecuteCommand(saveActivityCommamd);
        //        return Ok((ActivityDto)result.Data);
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO add log
        //        _log.ErrorFormat("ActivityApiController: Error during save (Put) ActivityDto. {0}", ex);
        //        return InternalServerError(ex);
        //    }

        //    return Ok();
        //}

        //// DELETE api/<controller>/5
        ///// <summary>
        ///// Delete Activity
        ///// </summary>
        ///// <param name="activityId"></param>
        ///// <param name="versionId"></param>
        //[Route("{activityId:int}/versions/{versionId:int}")]
        //public void Delete(int activityId, int versionId)
        //{
        //    var command = new DeleteActivityCommand { ActivityId = activityId, Version = versionId };
        //    CommandQueryDispatcher.ExecuteCommand(command);
        //}



        ///// <summary>
        ///// Get messages for all incorect points sum per month by User Id
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //[Route("~/api/Users/{UserId}/incorrectMonthPointsSumNotification")]
        //[HttpGet]
        //public IHttpActionResult GetIncorrectMonthPointsSumNotification(int UserId)
        //{
        //    var result = new List<NotificationModel>();

        //    var incorrectMonthsQuery = new GetIncorrectMonthsPointsSumQuery(UserId);
        //    ExecutionResult resultIncorrectMonths = CommandQueryDispatcher.ExecuteQuery(incorrectMonthsQuery);
        //    var months = (IList<MonthPointsSumDto>)resultIncorrectMonths.Data;
        //    if (months != null && months.Count > 0)
        //    {
        //        foreach (MonthPointsSumDto month in months)
        //        {
        //            string warn = string.Format(Strings.Warning_PointsSumOverdue_Format3, month.Year, month.Month, month.PointsSum);
        //            result.Add(new NotificationModel(NotificationType.Warn, Strings.NotificationTitle_Warning, warn));
        //        }
        //    }

        //    return Ok(result);
        //}
        #endregion Actions
    }
}
