using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using TodoListGQL.Data;
using TodoListGQL.Models;

namespace TodoListGQL.GraphQl
{
    public class Query
    {
        [UseDbContext(typeof(ApiDbContext))]
        public IQueryable<ItemList> GetList([ScopedService] ApiDbContext context){
            return context.lists;
        }
    }
}