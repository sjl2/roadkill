using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Roadkill.Core.Converters;
using Roadkill.Core.Database;
using Roadkill.Core.Cache;
using Roadkill.Core.Mvc.ViewModels;
using Roadkill.Core.Configuration;
using System.Web;
using Roadkill.Core.Logging;
using Roadkill.Core.Text;
using Roadkill.Core.Plugins;
using StructureMap.Graph;

namespace Roadkill.Core.Services
{
    public class TemplateService : ServiceBase
    {
        private SearchService _searchService;
        private IUserContext _context;
        private MarkupConverter _markupConverter;

        public TemplateService(ApplicationSettings settings, IRepository repository, IPluginFactory pluginFactory)
            : base(settings, repository)
        {
            _markupConverter = new MarkupConverter(settings, repository, pluginFactory);
        }

        public TemplateViewModel AddTemplate(TemplateViewModel model)
        {
            Template template = new Template();
            template.Name = model.Name;
            template.Content = model.Content;

            Repository.AddNewTemplate(template);

            TemplateViewModel savedModel = new TemplateViewModel(template, _markupConverter);
            return savedModel;
        }

        public IEnumerable<TemplateViewModel> AllTemplates()
        {
            try
            {
                IEnumerable<TemplateViewModel> templateModels;
                IEnumerable<Template> templates = Repository.AllTemplates();
                templateModels = from template in templates
                    select new TemplateViewModel(template);

                return templateModels;
            }
            catch (DatabaseException ex)
            {
                throw new DatabaseException(ex, "An error occurred while retrieving all templates from the database");
            }
        }

        public void DeleteTemplate(int templateId)
        {

            try
            {
                Template template = Repository.GetTemplateById(templateId);

                Repository.DeleteTemplate(template);
            }
            catch (DatabaseException ex)
            {
                throw new DatabaseException(ex, "An error occurred while deleting the template id {0} from the database", templateId);
            }

        }

        public TemplateViewModel GetById(int id)
        {
            Template template = Repository.GetTemplateById(id);

            if (template == null)
            {
                return null;
            }

            var templateModel = new TemplateViewModel(template);

            return templateModel;

        }

        public TemplateViewModel GetByName(string name)
        {
            Template template = Repository.GetTemplateByName(name);

            if (template == null)
            {
                return null;
            }

            var templateModel = new TemplateViewModel(template);

            return templateModel;
        }

        public void UpdateTemplate(TemplateViewModel model)
        {
            try
            {
                Template template = Repository.GetTemplateById(model.Id);
                template.Name = model.Name;
                template.Content = model.Content;

                Repository.SaveOrUpdateTemplate(template);

            }
            catch (DatabaseException ex)
            {
                throw new DatabaseException(ex, "An error occurred updating the page with title '{0}' in the database", model.Name);
            }
        }

        /// <summary>
        /// Clears all templates from the database.
        /// </summary>
        /// <exception cref="DatabaseException">An datastore error occurred while clearing the template data.</exception>
        public void ClearTemplateTables()
        {
            try
            {
                Repository.DeleteAllTemplates();
            }
            catch (DatabaseException ex)
            {
                throw new DatabaseException(ex, "An exception occurred while clearing all template tables.");
            }
        }
    }
}
