using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TM.Business;
using TM.Business.Request;

namespace TaskManager.Controllers
{
    [RoutePrefix("v1/users")]
    public class UsersController : ApiController
    {
        readonly IUserBusiness _userBusiness;
        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<UserRequest> GetAll()
        {
            return _userBusiness.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var user = _userBusiness.GetAll().FirstOrDefault(e => e.UserId == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult Save(UserRequest model)
        {
            _userBusiness.Save(model);
            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            _userBusiness.Delete(id);
            return Ok();
        }
    }
}
