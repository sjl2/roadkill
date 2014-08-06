using System;
using Roadkill.Core.Database.Repositories;

namespace Roadkill.Core.Database
{
	/// <summary>
	/// Defines a repository for storing and retrieving Roadkill domain objects in a data store.
	/// </summary>
	public interface IRepository : IPageRepository, IUserRepository, ISettingsRepository, ITemplateRepository, IDisposable
	{
		void Startup(DataStoreType dataStoreType, string connectionString, bool enableCache);
		void TestConnection(DataStoreType dataStoreType, string connectionString);

	}
}
