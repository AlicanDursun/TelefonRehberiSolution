namespace ContactService.Api.Core.Application.ViewModel
{
    public class PaginatedItemsViewModel<IEntity> where IEntity : class
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public long Count { get; private set; }

        public IEnumerable<IEntity> Data { get; private set; }

        public PaginatedItemsViewModel(int pageIndex,
            int pageSize,
            long count,
            IEnumerable<IEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
