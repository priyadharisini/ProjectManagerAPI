﻿using System.Web.Http;
using TM.Business;
using TM.Business.Request;

namespace TaskManager.Controllers
{
    [RoutePrefix("v1/tasks")]
    public class TasksController : ApiController
    {
       
            readonly ITaskBusiness _taskBusiness;
            public TasksController(ITaskBusiness taskBusiness)
            {
                _taskBusiness = taskBusiness;
            }

            [HttpGet]
            [Route("parent-tasks")]
            public IHttpActionResult GetAllParentTasks()
            {
                var models = _taskBusiness.GetAllParentTasks();
                return Ok(models);
            }

            [HttpGet]
            [Route("{id}")]
            public IHttpActionResult GetById(int id)
            {
                var task = _taskBusiness.GetById(id);
                return Ok(task);
            }

            [HttpGet]
            [Route("")]
            public IHttpActionResult GetAll()
            {
                var tasks = _taskBusiness.GetAllTasks();
                return Ok(tasks);
            }

            [HttpPost]
            [Route("")]
            public IHttpActionResult Save(TaskRequest model)
            {
                _taskBusiness.Save(model);
                return Ok();
            }

            [HttpPost]
            [Route("complete")]
            public IHttpActionResult Complete(TaskRequest model)
            {
                _taskBusiness.Complete(model);

                var tasks = _taskBusiness.GetAllTasks();
                return Ok(tasks); 
            }
        }
    }

