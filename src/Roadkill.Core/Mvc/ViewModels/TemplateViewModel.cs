using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Roadkill.Core.Converters;
using Roadkill.Core.Database;
using Roadkill.Core.Text;
using System.ComponentModel.DataAnnotations;
using Roadkill.Core.Localization;



namespace Roadkill.Core.Mvc.ViewModels
{

    public class TemplateViewModel
    {
        private string _content;

        public TemplateViewModel()
        {
            Name = "";
            Content = "";
        }

        public TemplateViewModel(Template template)
        {
            if (template == null)
                throw new ArgumentNullException("template");

            Id = template.Id;
            Name = template.Name;
            PreviousName = Name;
            Content = template.Content;
        }

        public TemplateViewModel(Template template, MarkupConverter converter)
        {
            if (template == null)
                throw new ArgumentNullException("template");
            if (converter == null)
                throw new ArgumentNullException("converter");

            Id = template.Id;
            Name = template.Name;
            PreviousName = Name;
            Content = template.Content;

            PageHtml pageHtml = converter.ToHtml(template.Content);
            ContentAsHtml = pageHtml.Html;
        }

        /// <summary>
        /// Template ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Template Name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (SiteStrings), ErrorMessageResourceName = "Template_Validation_Name")]
        public string Name { get; set; }

        /// <summary>
        /// Name before any update
        /// </summary>
        public string PreviousName { get; set; }

        /// <summary>
        /// Contents of template
        /// </summary>
        [AllowHtml]
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                if (_content == null)
                {
                    _content = "";
                }
            }
        }

        public string ContentAsHtml { get; set; }

        public static List<SelectListItem> MakeDropDownList(IEnumerable<TemplateViewModel> list)
        {
            List<SelectListItem> toReturn = new List<SelectListItem>();

            // Produces blank first item so user does not have to use a template.
            var blank = new SelectListItem()
            {
                Text = "No Template",
                Value = ""
            };

            toReturn.Add(blank);

            foreach (TemplateViewModel template in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Text = template.Name,
                    Value = template.Id.ToString()
                };
                toReturn.Add(listItem);
            }
            return toReturn;
        }

    }
}
