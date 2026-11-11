using Microsoft.AspNetCore.Mvc;
using Gharshare.Views.Shared.Components.SearchBar;

namespace Gharshare.Views.Shared.Components.SearchBar
{
    public class SearchBarViewComponent:ViewComponent
    {
        public SearchBarViewComponent()
        {

        }

        public IViewComponentResult Invoke(Search search)
        {
            return View("Default",search);
        }
    }
}
