using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Commerce_TransactionApp.Models
{


    public class User
    {
        [Required(ErrorMessage = "Username is required")]
        
        public string username { get; set; }
        [Required(ErrorMessage = "First name is required")]
        
        public string password { get; set; }
    
    }
}
