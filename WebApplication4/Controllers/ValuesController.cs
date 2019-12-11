using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models.EntityModel;

namespace WebApplication4.Controllers
{
    public class ValuesController : ApiController
    {
        DB_APIEntities db = new DB_APIEntities();
        // GET api/values
        public IEnumerable<TBL_USER_INFO> Get()
        {
            IEnumerable<TBL_USER_INFO> oList = db.TBL_USER_INFO.ToList();
            return oList;
        }

        // GET api/values/5
        public TBL_USER_INFO Get(int id)
        {
            TBL_USER_INFO obj = db.TBL_USER_INFO.FirstOrDefault(user => user.Id == id);
            return obj;
        }

        // POST api/values
        public int Post([FromBody]TBL_USER_INFO value)
        {
            try
            {
                using (DB_APIEntities dbContext = new DB_APIEntities())
                {
                    if (value.Id > 0)
                    {
                        TBL_USER_INFO oTBL_USER_INFO = dbContext.TBL_USER_INFO.FirstOrDefault(c => c.Id == value.Id);
                        oTBL_USER_INFO.FirstName = value.FirstName;
                        oTBL_USER_INFO.LastName = value.LastName;
                        oTBL_USER_INFO.Password = value.Password;
                        oTBL_USER_INFO.EMail = value.EMail;
                        oTBL_USER_INFO.PhoneNumber = value.PhoneNumber;

                        dbContext.Entry(oTBL_USER_INFO).State = EntityState.Modified;
                    }
                    else
                    {
                        value.CreatedDate = DateTime.Now;
                        value.Gender = "";
                        value.GroupId = 1;
                        value.IsActive = true;
                        dbContext.TBL_USER_INFO.Add(value);
                    }
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        // PUT api/values/5
        public int Put(int id, [FromBody]TBL_USER_INFO value)
        {
            try
            {
                using (DB_APIEntities dbContext = new DB_APIEntities())
                {
                    TBL_USER_INFO oTBL_USER_INFO = dbContext.TBL_USER_INFO.FirstOrDefault(c => c.Id == id);
                    oTBL_USER_INFO.FirstName = value.FirstName;
                    oTBL_USER_INFO.LastName = value.LastName;
                    oTBL_USER_INFO.Password = value.Password;
                    oTBL_USER_INFO.EMail = value.EMail;
                    oTBL_USER_INFO.PhoneNumber = value.PhoneNumber;

                    dbContext.Entry(oTBL_USER_INFO).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            try
            {
                using (DB_APIEntities dbContext = new DB_APIEntities())
                {
                    TBL_USER_INFO oTBL_USER_INFO = dbContext.TBL_USER_INFO.Find(id);
                    dbContext.TBL_USER_INFO.Remove(oTBL_USER_INFO);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
