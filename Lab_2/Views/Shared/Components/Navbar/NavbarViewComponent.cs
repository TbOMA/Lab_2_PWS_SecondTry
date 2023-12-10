using Lab_2.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;


    using Microsoft.Extensions.Localization;



namespace Lab_2.Views.Shared.Components.Navbar
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IStringLocalizer<NavbarViewComponent> _localizer;

        public NavbarViewComponent(IStringLocalizer<NavbarViewComponent> localizer)
        {
            _localizer = localizer;
        }

        public IViewComponentResult Invoke()
        {
            var menuItems = GetNavbarMenuItems();

            var viewModel = new NavbarViewModel
            {
                MenuItems = menuItems,
                Localizer = _localizer
            };

            return View(viewModel);
        }

        private List<MenuItem> GetNavbarMenuItems()
        {
            return new List<MenuItem>
        {
            new MenuItem { Text = "Home", Url = "/" },
            new MenuItem { Text = "About", Url = "/about" },
            new MenuItem { Text = "Services", Url = "/service" },
            new MenuItem { Text = "News", Url = "/news" },
            new MenuItem { Text = "Contact Us", Url = "/contact" },
            new MenuItem { Text = "Login", Url = "#" },
        };
        }

        public class MenuItem
        {
            public string Text { get; set; }
            public string Url { get; set; }
        }
    }


}
