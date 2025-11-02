using ApiProject.Data;
using ApiProject.Models;
using ApiProject.Repositories;

namespace ApiProject.unitofwork
{
    public class UnitOfWork
    {
        private  SchoolContext _context;

       
        private GenericRepo<Parent>? _parentRepo;
        private GenericRepo<Student>? _studentRepo;

        //  Constructor
        public UnitOfWork(SchoolContext context)
        {
            _context = context;
        }


        public GenericRepo<Parent> ParentRepo
        {
            get
            {
                if (_parentRepo == null)
                    _parentRepo = new GenericRepo<Parent>(_context);
                return _parentRepo;
            }
        }

        public GenericRepo<Student> StudentRepo
        {
            get
            {
                if (_studentRepo == null)
                    _studentRepo = new GenericRepo<Student>(_context);
                return _studentRepo;
            }
        }

      
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
