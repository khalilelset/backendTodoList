namespace Entity
{
    public class User
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        
    }
    public class todo
    {
        public int todo_id { get; set; }
        public string prefix { get; set; }
        public string title { get; set; }
        public DateTime duedate { get; set; }
        public string category { get; set; }
        public int estimate { get; set; }
        public string EstimateUnit { get; set; }

        public string importance { get; set; }
        public int user_id { get; set; } 
    }

    public class Etodo
    {
        public int todo_id { get; set; }
        public string prefix { get; set; }
        public string title { get; set; }
        public DateTime duedate { get; set; }
        public string category { get; set; }
        public int estimate { get; set; }
        public string EstimateUnit { get; set; }

        public string importance { get; set; }
        
    }

}