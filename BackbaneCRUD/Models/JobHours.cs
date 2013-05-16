namespace BackboneCRUD.Models
{
    public class JobHours
    {
        public string Name { get; set; }
        public int Quote { get; set; }
        public int Sum { get; set; }
        public string id { get; set; }

        public JobHours(string name, int quote, int sum)
        {
            id = name;
            Name = name;
            Quote = quote;
            Sum = sum;
        }
        
        public JobHours():base(){}
    }
}