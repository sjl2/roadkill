using System;


namespace Roadkill.Core.Database
{
    /// <summary>
    /// Represents a template in the data store.
    /// </summary>
    public class Template : IDataStoreEntity
    {
        private Guid _objectId;

        /// <summary>
        /// Template ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Template
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Template contents
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// The unique id for this object - for use with document stores that require a unique id for storage.
        /// </summary>
        public Guid ObjectId
        {
            get { return _objectId; }
            set { _objectId = value; }
        }
    }
}
