namespace CarStore.Models
{
    public class CarFilter
    {
        public int MakeId {get; set;}
        public int ModelId {get;set;}

        public int MinPrice {get;set;}
        public int MaxPrice {get;set;}
    }
}
