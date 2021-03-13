namespace abtestreal.VM
{
    public class ListResponse<TItem> where TItem : class
    {
        public TItem[] Items { get; set; }
    }
}