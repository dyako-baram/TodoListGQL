using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using TodoListGQL.Data;
using TodoListGQL.Models;

namespace TodoListGQL.GraphQl.List
{
    public class ListType : ObjectType<ItemList>
    {
        protected override void Configure(IObjectTypeDescriptor<ItemList> descriptor)
        {
            descriptor.Description("this item is used as item for the list");
            descriptor.Field(x=>x.ItemDatas)
                        .ResolveWith<Resolvers>(x=>x.GetItems(default!,default!))
                        .UseDbContext<ApiDbContext>()
                        .Description("this is the list that the item belongs to");
        }
        private class Resolvers
        {
            public IQueryable<ItemData> GetItems(ItemList list,[ScopedService] ApiDbContext context)
            {
                return context.items.Where(x=>x.ListId==list.Id);
               
            }
        }
    }
}