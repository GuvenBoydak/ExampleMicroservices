using System.ComponentModel.DataAnnotations;

namespace ExampleMicroservice.UI_MVC.Models.Catalogs
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs süre")]
        public int Duration { get; set; }
    }
}