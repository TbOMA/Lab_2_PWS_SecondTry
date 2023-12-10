using Lab_2.Views.Shared.Components.Navbar;
using Microsoft.Extensions.Localization;

namespace Lab_2.Models.ViewModel
{
    public class NavbarViewModel
    {
        public List<NavbarViewComponent.MenuItem> MenuItems { get; set; }
        public IStringLocalizer<NavbarViewComponent> Localizer { get; set; }
    }

}
