namespace StoreSolution.Data.Areas.Stores.ViewModels
{
    public class CategoryVm
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Description { get; set; }
    }
}
