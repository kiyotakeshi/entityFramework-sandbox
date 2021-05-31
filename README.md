```shell
dotnet --list-sdks

dotnet new globaljson --sdk-version 3.1.408

dotnet new webapi -n sandbox

# tool の install 
dotnet new tool-manifest

dotnet tool install dotnet-ef --version 3.1.4

# package の追加
dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.4

dotnet add package Microsoft.EntityFrameworkCore --version 3.1.4

dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.4

dotnet new gitignore

dotnet run
```

```shell
$ dotnet ef --version
Entity Framework Core .NET Command-line Tools
3.1.4

dotnet ef migrations add InitialCreate

# 削除する場合
# dotnet ef migrations list
# dotnet ef migrations remove

# DB に適用
dotnet ef database update

# 適用したものを削除
# dotnet ef database drop

$ file sandbox.db
sandbox.db: SQLite 3.x database, last written using SQLite version 3028
```

※DDL を生成した場合  

```sql
-- dotnet ef migrations script
-- CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
--     "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
--     "ProductVersion" TEXT NOT NULL
-- );

CREATE TABLE "Authors" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Authors" PRIMARY KEY AUTOINCREMENT,
    "FirstName" TEXT NULL,
    "LastName" TEXT NULL
);

CREATE TABLE "Books" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Books" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NULL,
    "Published" TEXT NULL
);

CREATE TABLE "Reviwers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reviwers" PRIMARY KEY AUTOINCREMENT,
    "FirstName" TEXT NULL,
    "LastName" TEXT NULL
);

CREATE TABLE "BookAuthors" (
    "BookId" INTEGER NOT NULL,
    "AuthorId" INTEGER NOT NULL,
    CONSTRAINT "PK_BookAuthors" PRIMARY KEY ("BookId", "AuthorId"),
    CONSTRAINT "FK_BookAuthors_Authors_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES "Authors" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_BookAuthors_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Reviews" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reviews" PRIMARY KEY AUTOINCREMENT,
    "Headline" TEXT NULL,
    "ReviewText" TEXT NULL,
    "Rating" INTEGER NOT NULL,
    "ReviewerId" INTEGER NULL,
    "BookId" INTEGER NULL,
    CONSTRAINT "FK_Reviews_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Reviews_Reviwers_ReviewerId" FOREIGN KEY ("ReviewerId") REFERENCES "Reviwers" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_BookAuthors_AuthorId" ON "BookAuthors" ("AuthorId");

CREATE INDEX "IX_Reviews_BookId" ON "Reviews" ("BookId");

CREATE INDEX "IX_Reviews_ReviewerId" ON "Reviews" ("ReviewerId");

-- INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
-- VALUES ('20210531222210_InitialCreate', '3.1.4');
```

- 作成されたDBの確認

```shell
$ sqlite3 sandbox.db 
SQLite version 3.32.3 2020-06-18 14:16:19

sqlite> .database
main: /Users/kiyotakeshi/gitdir/c-charp/c-charp-sandbox/sandbox/sandbox.db

sqlite> .table
Authors                Books                  Reviwers             
BookAuthors            Reviews                __EFMigrationsHistory
```
