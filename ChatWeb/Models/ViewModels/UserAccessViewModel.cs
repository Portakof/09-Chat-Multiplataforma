
using System.ComponentModel.DataAnnotations;


namespace ChatWeb.Models.ViewModels
{
    //Esta clase se crea para representar datos en una vista en esta caso la vista del "Login"
    public class UserAccessViewModel
    {
        [Required]                      //Hace que el campo o textbox se obligatorio ingresarle datos  
        [EmailAddress]                  //valida que el texto incluido se de tipo mail osea "@mail.com"
        public string Email { get; set; }

        [Required]                      //Hace que el campo o textbox se obligatorio ingresarle datos
        public string Password { get; set; }
    }
}