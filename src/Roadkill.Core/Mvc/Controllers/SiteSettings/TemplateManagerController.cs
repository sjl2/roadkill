using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using Ionic.Zip;
using Roadkill.Core.Converters;
using Roadkill.Core.Localization;
using Roadkill.Core.Configuration;
using Roadkill.Core.Cache;
using Roadkill.Core.Services;
using Roadkill.Core.Import;
using Roadkill.Core.Security;
using Roadkill.Core.Mvc.Attributes;
using Roadkill.Core.Mvc.ViewModels;
using Roadkill.Core.Logging;
using Roadkill.Core.Database.Export;
using Roadkill.Core.Database;
using Roadkill.Core.Plugins;
using Roadkill.Core.Domain.Export;
using Roadkill.Core.Text;

namespace Roadkill.Core.Mvc.Controllers
{
    /// <summary>
    ///  Provides all Template Manager Functionality, including editing and viewing pages.
    /// </summary>
    public class TemplateManagerController : ControllerBase
    {
        private SettingsService _settingsService;
        private TemplateService _templateService;
        private PageService _pageService;

        public TemplateManagerController(ApplicationSettings settings, UserServiceBase userManager,
            SettingsService settingsService, TemplateService templateService, PageService pageService, IUserContext context)
            : base(settings, userManager, context, settingsService)
        {
            _settingsService = settingsService;
            _templateService = templateService;
            _pageService = pageService;
        }

        public ActionResult Index()
        {
            var list = _templateService.AllTemplates();

            return View(list);
        }
        
        public ActionResult Add()
        {
            TemplateViewModel model = new TemplateViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Add(TemplateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            _templateService.AddTemplate(model);
            return RedirectToAction("Index", "TemplateManager");
        }

        public ActionResult Delete(int Id)
        {
            _templateService.DeleteTemplate(Id);
            return RedirectToAction("Index", "TemplateManager");
        }

        public ActionResult Edit(int Id)
        {
            TemplateViewModel template = _templateService.GetById(Id);
            if (template == null)
                return RedirectToAction("Index");
            return View(template);
        }

        [EditorRequired]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TemplateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            _templateService.UpdateTemplate(model);

            return RedirectToAction("Index", "TemplateManager");
        }

        /// <summary>
        /// This action is for JSON calls only. Displays a HTML preview for the provided 
        /// wiki markup/markdown. This action is POST only.
        /// </summary>
        /// <param name="id">The wiki markup.</param>
        /// <returns>The markup as rendered as HTML.</returns>
        /// <remarks>This action requires editor rights.</remarks>
        [ValidateInput(false)]
        [EditorRequired]
        [HttpPost]
        public ActionResult GetPreview(string id)
        {
            PageHtml pagehtml = "";

            if (!string.IsNullOrEmpty(id))
            {
                MarkupConverter converter = _pageService.GetMarkupConverter();
                pagehtml = converter.ToHtml(id);
            }
            return JavaScript(pagehtml.Html);
        }
       
    }
}
