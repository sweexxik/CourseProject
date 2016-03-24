using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using SphinxConnector.FluentApi;

namespace CourseProject.Repositories
{
    class CreativesRepository : IRepository<Creative>
    {
        private readonly AuthContext db;

        public CreativesRepository(AuthContext context)
        {
           
            db = context;
        }

        public IEnumerable<Creative> GetAll()
        {
            return db.Creatives.ToList();
        }

        public async Task<Creative> Get(int id)
        {
            return await db.Creatives.FindAsync(id);
        }

        public IEnumerable<Creative> Find(Func<Creative, bool> predicate)
        {
            return db.Creatives.Where(predicate).ToList();
        }

        public void Add(Creative item)
        {
            db.Creatives.Add(item);
        }

        public void Update(Creative item)
        {
           db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Creative> Remove(int id)
        {
            var item = await db.Creatives.FindAsync(id);

            if (item == null) return null;

            db.Creatives.Remove(item);

            return item;
        }

        public async Task<IEnumerable<Creative>> Search(string pattern)
        {
            var ftsResults = db.Creatives.Where(c => c.Name.Contains(pattern)).Select(c => c.Id);

            return db.Creatives.Where(creative => ftsResults.Contains(creative.Id)).ToList();
        }

        public void AddRange(IEnumerable<Creative> range)
        {
            db.Creatives.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Creative> range)
        {
            db.Creatives.RemoveRange(range);
        }

        public IEnumerable<Creative> RunFullTextContainsQuery(Expression<Func<Creative, object>> property, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return Enumerable.Empty<Creative>();
            }

            var unaryExpression = property.Body as UnaryExpression;

            var memberExpression = property.Body as MemberExpression ?? (unaryExpression != null ? unaryExpression.Operand as MemberExpression : null);

            if (memberExpression == null)
            {
                throw new Exception(string.Format("Invalid lambda expression: '{0}'.", property));
            }

            var query = string.Format("SELECT * FROM {0} WHERE CONTAINS( {1}, @search)", GetTableName(), memberExpression.Member.Name);

            return db.Database.SqlQuery<Creative>(query, new SqlParameter("@search", search)).ToList();
        }

        private string GetTableName()
        {
            var objectContext = ((IObjectContextAdapter)db).ObjectContext;
            var sql = objectContext.CreateObjectSet<Creative>().ToTraceString();
            var regex = new Regex(@"FROM\s+(?<table>.+)\s+AS");
            var match = regex.Match(sql);

            return match.Groups["table"].Value;
        }
    }
}
