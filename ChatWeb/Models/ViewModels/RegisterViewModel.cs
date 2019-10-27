
using System.ComponentModel.DataAnnotations;


namespace ChatWeb.Models.ViewModels
{
    //Esta clase se crea porque son los unicos datos que el cliente podra crear y que seran ingresados a la base datos
    //Estos parametros se llenan por el formulario creado en "Register.cshtml"
    //"Register.cshtml" es la vista creada para que el usuario pueda ingresar los datos de registro
    public class RegisterViewModel  
    {
        [Required]                      //Hace que el campo o textbox se obligatorio ingresarle datos
        [StringLength(50)]              //restringe o permite solo cierta cantidad de caracteres "(50)"
        [Display(Name="Nombre")]        //ES el nombre que mostrara la vista
        public string Name {get; set;}

        [Required]
        [StringLength(50)]
        [Display(Name = "Ciudad")]
        public string City {get; set;}

        [Required]
        [StringLength(50)]
        [Display(Name = "Contraseña")]
        public string Password {get; set;}

        [Required]
        [StringLength(50)]
        //realiza una comparacion entre dos campos dentro de los parantesis debe ir el nombre de la variable a comparar
        [Compare("Password")]           //asi se compara "Password" y "Password2"
        [Display(Name = "Confirmar contraseña")]
        public string Password2 { get; set; }

        [Required]
        [EmailAddress]                  //valida que el texto incluido se de tipo mail osea "@mail.com"
        [StringLength(50)]
        [Display(Name = "Correo electronico")]
        public string Email {get; set;}
}
}