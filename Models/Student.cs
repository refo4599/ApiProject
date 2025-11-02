using System;
using System.Collections.Generic;

namespace ApiProject.Models;

public partial class Student
{
    public int Id { get; set; }              
    public string Name { get; set; }          
    public string Grade { get; set; }       
    public DateTime BirthDate { get; set; }  

   
    public int ParentId { get; set; }         
    public Parent Parent { get; set; }
}