namespace ApiProject.Models
{
    public class Parent
    {
        public int Id { get; set; }             
        public string FullName { get; set; }    
        public string Email { get; set; }        
        public string Phone { get; set; }       

        //  (One Parent => Many Students)
        public List<Student> Students { get; set; } = new();
    }
}
