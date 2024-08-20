


using DocumentFormat.OpenXml.Wordprocessing;
using MuseumManagementSystem.Web.ViewModels.Chart;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class ChartJsViewModel
    {
        
        public ChartJs Chart { get; set; } 
        public string ChartJson { get; set; } 
    }
}