using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Services;
using Portfolio.ViewModels;

namespace Portfolio.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var viewResult = filterContext.Result as ViewResult;
            if (viewResult == null)
                return;

            var viewModel = viewResult.Model as BaseViewModel;
            if (viewModel == null)
                return;

            FillBaseViewModel(viewModel);
        }

        protected virtual void FillBaseViewModel(BaseViewModel viewModel)
        {
            viewModel.Categories = ArticlesService.Instance.GetCategories(this);
            viewModel.FavoritesArticles = ArticlesService.Instance.GetArticles(this, true).Select(a => new ArticleViewModel(a)).ToList();
        }
    }
}
