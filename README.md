# entityFramework-sandbox

entityFramework を使用し、 DB に migration できることを確認する。

---

## 今回作成する Models の ER ([dbdiagram.io](https://dbdiagram.io/d/60b57275b29a09603d175d98))

- dotnet ef migrations script により作成した DDL から生成

---

## セットアップ

```shell
# ローカルの sdk のバージョンを確認
dotnet --list-sdks

# 今回使用する sdk のバージョンを指定
dotnet new globaljson --sdk-version 3.1.408

# 雛形を使用し、 scafoldding
dotnet new webapi -n sandbox

# ディレクトリ単位で使用する tool のバージョンを指定するためのファイルを生成
dotnet new tool-manifest

# global には tool をインストールしない
dotnet tool install dotnet-ef --version 3.1.4

# package の追加
dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.4

dotnet add package Microsoft.EntityFrameworkCore --version 3.1.4

dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.4

# mssql を使用する場合
# dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.4

# .gitignore の雛形を生成
dotnet new gitignore

# アプリが立ち上がることを確認
dotnet run
```

---

## entityFramework を使用した migration

```shell
# 先ほどインストールした tool のバージョンになっている
$ dotnet ef --version
Entity Framework Core .NET Command-line Tools
3.1.4

# models ディレクトリの entity の定義後に、実施
# Migrations ディレクトリ配下に migration のファイルが自動生成される
dotnet ef migrations add InitialCreate

# 削除する場合
# dotnet ef migrations list
# dotnet ef migrations remove

# DB に適用
dotnet ef database update

# 適用したものを削除する場合
# dotnet ef database drop

# SQLite に対して適用したため、ファイルが生成されている
$ file sandbox.db
sandbox.db: SQLite 3.x database, last written using SQLite version 3028
```

- 作成された DB の確認

```shell
$ sqlite3 sandbox.db
SQLite version 3.32.3 2020-06-18 14:16:19

sqlite> .database
main: /Users/kiyotakeshi/gitdir/c-charp/c-charp-sandbox/sandbox/sandbox.db

sqlite> .table
Authors                Books                  Reviwers
BookAuthors            Reviews                __EFMigrationsHistory
```

※DDL を生成した場合(for SQLite)

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

※DDL を生成した場合(for MSSQL)

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
-- VALUES ('20210531232148_InitialCreate', '3.1.4');
```
