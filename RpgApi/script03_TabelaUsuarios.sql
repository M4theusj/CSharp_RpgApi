BEGIN TRANSACTION;
DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TB_PERSONAGENS]') AND [c].[name] = N'Nome');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [TB_PERSONAGENS] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [TB_PERSONAGENS] ALTER COLUMN [Nome] nvarchar(max) NOT NULL;

ALTER TABLE [TB_PERSONAGENS] ADD [Derrotas] int NOT NULL DEFAULT 0;

ALTER TABLE [TB_PERSONAGENS] ADD [Disputas] int NOT NULL DEFAULT 0;

ALTER TABLE [TB_PERSONAGENS] ADD [UsuarioId] int NULL;

ALTER TABLE [TB_PERSONAGENS] ADD [Vitorias] int NOT NULL DEFAULT 0;

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TB_ARMAS]') AND [c].[name] = N'Nome');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [TB_ARMAS] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [TB_ARMAS] ALTER COLUMN [Nome] nvarchar(max) NOT NULL;

ALTER TABLE [TB_ARMAS] ADD [PersonagemId] int NOT NULL DEFAULT 0;

CREATE TABLE [TB_HABILIDADES] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Dano] int NOT NULL,
    CONSTRAINT [PK_TB_HABILIDADES] PRIMARY KEY ([Id])
);

CREATE TABLE [TB_USUARIOS] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [Foto] varbinary(max) NULL,
    [Latitude] float NULL,
    [Longitude] float NULL,
    [DataAcesso] datetime2 NULL,
    [Perfil] nvarchar(max) NULL DEFAULT N'Jogador',
    [Email] nvarchar(max) NULL,
    [Classe] int NOT NULL,
    [FotoPersonagem] varbinary(max) NULL,
    [UsuarioId] int NULL,
    [UsuarioOgId] int NULL,
    CONSTRAINT [PK_TB_USUARIOS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_USUARIOS_TB_USUARIOS_UsuarioOgId] FOREIGN KEY ([UsuarioOgId]) REFERENCES [TB_USUARIOS] ([Id])
);

CREATE TABLE [TB_PERSONAGENS_HABILIDADES] (
    [PersonagemId] int NOT NULL,
    [HabilidadeId] int NOT NULL,
    CONSTRAINT [PK_TB_PERSONAGENS_HABILIDADES] PRIMARY KEY ([PersonagemId], [HabilidadeId]),
    CONSTRAINT [FK_TB_PERSONAGENS_HABILIDADES_TB_HABILIDADES_HabilidadeId] FOREIGN KEY ([HabilidadeId]) REFERENCES [TB_HABILIDADES] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TB_PERSONAGENS_HABILIDADES_TB_PERSONAGENS_PersonagemId] FOREIGN KEY ([PersonagemId]) REFERENCES [TB_PERSONAGENS] ([Id]) ON DELETE CASCADE
);

UPDATE [TB_ARMAS] SET [PersonagemId] = 1
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 2
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 3
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 4
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 5
WHERE [Id] = 5;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 6
WHERE [Id] = 6;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 7
WHERE [Id] = 7;
SELECT @@ROWCOUNT;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Dano', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_HABILIDADES]'))
    SET IDENTITY_INSERT [TB_HABILIDADES] ON;
INSERT INTO [TB_HABILIDADES] ([Id], [Dano], [Nome])
VALUES (1, 39, N'Adormecer'),
(2, 41, N'Congelar'),
(3, 37, N'Hipnotizar');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Dano', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_HABILIDADES]'))
    SET IDENTITY_INSERT [TB_HABILIDADES] OFF;

UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [UsuarioId] = 1, [Vitorias] = 0
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [UsuarioId] = 1, [Vitorias] = 0
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [UsuarioId] = 1, [Vitorias] = 0
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [UsuarioId] = 1, [Vitorias] = 0
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [UsuarioId] = 1, [Vitorias] = 0
WHERE [Id] = 5;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [UsuarioId] = 1, [Vitorias] = 0
WHERE [Id] = 6;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [UsuarioId] = 1, [Vitorias] = 0
WHERE [Id] = 7;
SELECT @@ROWCOUNT;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Classe', N'DataAcesso', N'Email', N'Foto', N'FotoPersonagem', N'Latitude', N'Longitude', N'PasswordHash', N'PasswordSalt', N'Perfil', N'Username', N'UsuarioId', N'UsuarioOgId') AND [object_id] = OBJECT_ID(N'[TB_USUARIOS]'))
    SET IDENTITY_INSERT [TB_USUARIOS] ON;
INSERT INTO [TB_USUARIOS] ([Id], [Classe], [DataAcesso], [Email], [Foto], [FotoPersonagem], [Latitude], [Longitude], [PasswordHash], [PasswordSalt], [Perfil], [Username], [UsuarioId], [UsuarioOgId])
VALUES (1, 0, NULL, N'seuEmail@gmail.com', NULL, NULL, -23.520024100000001E0, -46.596497999999997E0, 0xA05A7F5FC90C45FFC39174AC7A5F88EAD451AF5C678D392FF28AA8BD59FB64F4ECD0AB91D5E3D7A28E5F7732D44F79AF7EABAE399BBFC777B0CAC344ABB1BD7E, 0x78D2D5A9CAB09068E30A2F13DBD53682CE05317A83A8E934325E6C88D108820B6B3BB7153E64F5E21E253FF07E04F87436624F86ECCF3E55AA7E86E0A1070CA8FA3F6F366BACE845ED0D068CF5CF23E1FD54A5EB084C4CD06A82DA08C65FF8D0739A659D8AADA6A9B416D26B0B13B6D53A4F9F05B718EB6708C7B403ECA13E9E, N'Admin', N'UsuarioAdmin', NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Classe', N'DataAcesso', N'Email', N'Foto', N'FotoPersonagem', N'Latitude', N'Longitude', N'PasswordHash', N'PasswordSalt', N'Perfil', N'Username', N'UsuarioId', N'UsuarioOgId') AND [object_id] = OBJECT_ID(N'[TB_USUARIOS]'))
    SET IDENTITY_INSERT [TB_USUARIOS] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'HabilidadeId', N'PersonagemId') AND [object_id] = OBJECT_ID(N'[TB_PERSONAGENS_HABILIDADES]'))
    SET IDENTITY_INSERT [TB_PERSONAGENS_HABILIDADES] ON;
INSERT INTO [TB_PERSONAGENS_HABILIDADES] ([HabilidadeId], [PersonagemId])
VALUES (1, 1),
(2, 1),
(2, 2),
(2, 3),
(3, 3),
(3, 4),
(1, 5),
(2, 6),
(3, 7);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'HabilidadeId', N'PersonagemId') AND [object_id] = OBJECT_ID(N'[TB_PERSONAGENS_HABILIDADES]'))
    SET IDENTITY_INSERT [TB_PERSONAGENS_HABILIDADES] OFF;

CREATE INDEX [IX_TB_PERSONAGENS_UsuarioId] ON [TB_PERSONAGENS] ([UsuarioId]);

CREATE INDEX [IX_TB_ARMAS_PersonagemId] ON [TB_ARMAS] ([PersonagemId]);

CREATE INDEX [IX_TB_PERSONAGENS_HABILIDADES_HabilidadeId] ON [TB_PERSONAGENS_HABILIDADES] ([HabilidadeId]);

CREATE INDEX [IX_TB_USUARIOS_UsuarioOgId] ON [TB_USUARIOS] ([UsuarioOgId]);

ALTER TABLE [TB_ARMAS] ADD CONSTRAINT [FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId] FOREIGN KEY ([PersonagemId]) REFERENCES [TB_PERSONAGENS] ([Id]) ON DELETE CASCADE;

ALTER TABLE [TB_PERSONAGENS] ADD CONSTRAINT [FK_TB_PERSONAGENS_TB_USUARIOS_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [TB_USUARIOS] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250414113933_MigracaoUsuario', N'9.0.2');

COMMIT;
GO

