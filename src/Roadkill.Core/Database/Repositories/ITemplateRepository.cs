using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roadkill.Core.Database;

namespace Roadkill.Core.Database
{
    public interface ITemplateRepository
    {
        Template AddNewTemplate(Template template);

        IEnumerable<Template> AllTemplates();

        void DeleteTemplate(Template template);

        void DeleteAllTemplates();

        Template GetTemplateById(int id);

        Template GetTemplateByName(string name);

        Template SaveOrUpdateTemplate(Template template);

    }
}
