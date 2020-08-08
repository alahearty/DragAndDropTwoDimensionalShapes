using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : IEntityBase
    {

        bool Add(string query, Dictionary<string, object> values);
        bool Update(string query, Dictionary<string, object> values);
        bool Delete(T shapeId);
    }
}
