CREATE TABLE roadkill_pages
(
	Id INT AUTO_INCREMENT NOT NULL,
	Title NVARCHAR(255) NOT NULL,
	Tags NVARCHAR(255) NULL,
	CreatedBy NVARCHAR(255) NOT NULL,
	CreatedOn DATETIME NOT NULL,
	IsLocked BOOLEAN NOT NULL,
	ModifiedBy NVARCHAR(255) NULL,
	ModifiedOn DATETIME NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE roadkill_pagecontent
(
	Id NVARCHAR(36) NOT NULL,
	EditedBy NVARCHAR(255) NOT NULL,
	EditedOn DATETIME NOT NULL,
	VersionNumber INT NOT NULL,
	Text MEDIUMTEXT NULL,
	PageId INT NOT NULL,
	PRIMARY KEY (Id)
);

/*ALTER TABLE roadkill_pagecontent ADD CONSTRAINT FK_roadkill_pageid FOREIGN KEY(pageid) REFERENCES roadkill_pages (id);*/

CREATE TABLE roadkill_users
(
	Id VARCHAR(36) NOT NULL,
	ActivationKey NVARCHAR(255) NULL,
	Email NVARCHAR(255) NOT NULL,
	Firstname NVARCHAR(255) NULL,
	Lastname NVARCHAR(255) NULL,
	IsEditor BOOLEAN NOT NULL,
	IsAdmin BOOLEAN NOT NULL,
	IsActivated BOOLEAN NOT NULL,
	Password NVARCHAR(255) NOT NULL,
	PasswordResetKey NVARCHAR(255) NULL,
	Salt NVARCHAR(255) NOT NULL,
	Username NVARCHAR(255) NOT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE roadkill_siteconfiguration
(
	Id VARCHAR(36) NOT NULL,
	Version NVARCHAR(255) NOT NULL,
	Content MEDIUMTEXT NOT NULL,
	PRIMARY KEY (Id)
);