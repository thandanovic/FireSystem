using FireSys.DB;
using FireSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FireSys.Manager
{
    public class BaseManager<TEntity> where TEntity : class
    {
        #region Fields
        private BaseDB<TEntity> baseDB;

        private BaseDB<TEntity> BaseDB
        {
            get { return baseDB ?? (baseDB = new BaseDB<TEntity>()); }
        }
        #endregion

        #region Shared

        public TEntity Get(string id)
        {
            return BaseDB.Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return BaseDB.GetAll();
        }

        public void Add(TEntity entity)
        {
            BaseDB.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            BaseDB.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            BaseDB.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            BaseDB.RemoveRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return BaseDB.Find(predicate);
        }

        public void Update(TEntity entity)
        {
            BaseDB.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            BaseDB.UpdateRange(entities);
        }

        #endregion
    }

    #region Partials

    public partial class UserManager : BaseManager<FireSys.Entities.AspNetUser> { }
    public partial class UserRoleManager : BaseManager<UserRole> { }
    public partial class RoleManager : BaseManager<Role> { }
    public partial class OrderManager : BaseManager<Order> { }
    public partial class LocationManager : BaseManager<Location> { }
    public partial class UserInfoManager : BaseManager<UserInfo> { }
    #endregion
}
