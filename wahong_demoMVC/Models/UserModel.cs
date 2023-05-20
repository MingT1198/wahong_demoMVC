using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace wahong_demoMVC.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Birthday { get; set; }
    }
}
