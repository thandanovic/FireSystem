using FireSys.Entities;
using FireSys.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FireSys.WebAPI.Controllers
{
    public class RoleController : ApiController
    {
        // GET: api/Role
        public IEnumerable<Role> Get()
        {
            return CaWeService.Instance.GetAllRoles();
        }

        // GET: api/Role/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Role
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Role/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Role/5
        public void Delete(int id)
        {
        }
    }
}
