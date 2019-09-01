using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using MapIt.Data;

namespace MapIt.Repository
{
    /// <summary>
    /// The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class Repository<T> : IDisposable where T : class
    {
        private readonly string _connectionStringName;
        private ObjectContext _objectContext;
        private MapItEntities _Entities;

        public ObjectContext Context
        {
            get
            {
                return this._objectContext;
            }
        }

        public MapItEntities Entities
        {
            get { if (_Entities == null)_Entities = new MapItEntities(); return _Entities; }
            set { _Entities = value; }
        }

        public Repository()
        {
            this._objectContext = new MapIt.Data.MapItEntities().ToObjectContext();
        }

        public Repository(ObjectContext objectContext)
        {
            if (objectContext == null)
                throw new ArgumentNullException("objectContext");
            this._objectContext = objectContext;
        }

        public T GetByKey(object keyValue)
        {
            EntityKey key = GeTKey(keyValue);

            object originalItem;
            if (Context.TryGetObjectByKey(key, out originalItem))
            {
                return (T)originalItem;
            }
            return default(T);
        }

        public IQueryable<T> GetQuery()
        {
            var entityName = GeTName();
            var q = Context.CreateQuery<T>(entityName);
            //return ObjectContext.CreateQuery<T>(entityName);
            return q;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return GetQuery();
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().FirstOrDefault(predicate);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().SingleOrDefault(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> criteria)
        {
            return GetQuery().Where(criteria);
        }

        public Boolean Any(Expression<Func<T, bool>> criteria)
        {
            return GetQuery().Any(criteria);
        }

        public virtual void Add(T entity)
        {
            Context.AddObject(GeTName(), entity);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            var fqen = GeTName();

            object originalItem;
            EntityKey key = Context.CreateEntityKey(fqen, entity);

            if (Context.TryGetObjectByKey(key, out originalItem))
            {
                Context.ApplyCurrentValues(key.EntitySetName, entity);
            }

            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Context.DeleteObject(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(object keyValue)
        {
            T entity = GetByKey(keyValue);
            Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> criteria)
        {
            IEnumerable<T> records = Find(criteria);

            foreach (T record in records)
            {
                Context.DeleteObject(record);
            }

            Context.SaveChanges();
        }

        public virtual void Attach(T entity)
        {
            Context.AttachTo(GeTName(), entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        private string GeTName()
        {
            //return string.Format("{0}.{1}", Context.DefaultContainerName, typeof(T).Name);

            var container = Context.MetadataWorkspace.GetEntityContainer(Context.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = container.BaseEntitySets.FirstOrDefault(e => e.ElementType.Name == typeof(T).Name).Name;
            return container.Name + "." + entitySetName;
        }

        private EntityKey GeTKey(object keyValue)
        {
            var entitySetName = GeTName();
            var objectSet = Context.CreateObjectSet<T>();
            var keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers[0].ToString();
            var entityKey = new EntityKey(entitySetName, new[] { new EntityKeyMember(keyPropertyName, keyValue) });
            return entityKey;
        }

        public int Count()
        {
            return GetQuery().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return GetQuery().Count(criteria);
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }

        #endregion
    }
}
