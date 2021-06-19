using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TodoListGQL.Data;
using TodoListGQL.GraphQl.DataItem;
using TodoListGQL.Models;

namespace TodoListGQL.GraphQl.List
{
    public class Mutation
    {
        [UseDbContext(typeof(ApiDbContext))]
        public async Task<AddListPayload> AddListAsync(AddListInput input,[ScopedService] ApiDbContext context)
        {
            var list=new ItemList{
                Name=input.name
            };
            context.lists.Add(list);
            await context.SaveChangesAsync();
            return new AddListPayload(list);
        }
        [UseDbContext(typeof(ApiDbContext))]
        public async Task<AddItemPayload> AddItemAsync(AddItemInput input,[ScopedService] ApiDbContext context)
        {
            var item= new ItemData
            {
              IsDone=input.isDone,
              Description=input.description,
              ListId=input.listId,
              Title=input.title
            
            };
            context.items.Add(item);
            await context.SaveChangesAsync();
            return new AddItemPayload(item);
        }
    }
}