using System;
using Mindscape.LightSpeed;

namespace Roadkill.Core.Database.LightSpeed
{
    [Table("roadkill_templates", IdentityMethod = IdentityMethod.IdentityColumn)]
    public class TemplateEntity : Entity<int>
    {
        [Column("Name")]
        private string _name;

        [Column("Content")]
        private string _content;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set<string>(ref _name, value);
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                Set<string>(ref _content, value);
            }
        }

    }
}
