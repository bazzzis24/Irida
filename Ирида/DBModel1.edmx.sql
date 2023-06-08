
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/06/2022 18:35:49
-- Generated from EDMX file: C:\Users\MSI\Desktop\дип\Ирида\Ирида\DBModel1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Irida];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClientsAccommodations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccommodationsSet] DROP CONSTRAINT [FK_ClientsAccommodations];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientsReservations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationsSet] DROP CONSTRAINT [FK_ClientsReservations];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomsReservations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationsSet] DROP CONSTRAINT [FK_RoomsReservations];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomsAccommodations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccommodationsSet] DROP CONSTRAINT [FK_RoomsAccommodations];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO
IF OBJECT_ID(N'[dbo].[ReservationsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReservationsSet];
GO
IF OBJECT_ID(N'[dbo].[ClientsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientsSet];
GO
IF OBJECT_ID(N'[dbo].[RoomsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomsSet];
GO
IF OBJECT_ID(N'[dbo].[AccommodationsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccommodationsSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [IdUsers] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ReservationsSet'
CREATE TABLE [dbo].[ReservationsSet] (
    [IdReservatons] int IDENTITY(1,1) NOT NULL,
    [PriceR] int  NOT NULL,
    [DateReservation] datetime  NOT NULL,
    [DateInR] datetime  NOT NULL,
    [DateOutR] datetime  NOT NULL,
    [Clients_IdClients] int  NOT NULL,
    [Rooms_IdRooms] int  NOT NULL
);
GO

-- Creating table 'ClientsSet'
CREATE TABLE [dbo].[ClientsSet] (
    [IdClients] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Patronomic] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Pasport] nvarchar(max)  NOT NULL,
    [PasportCopy] nvarchar(max)  NULL
);
GO

-- Creating table 'RoomsSet'
CREATE TABLE [dbo].[RoomsSet] (
    [IdRooms] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL
);
GO

-- Creating table 'AccommodationsSet'
CREATE TABLE [dbo].[AccommodationsSet] (
    [IdAccommodations] int IDENTITY(1,1) NOT NULL,
    [DateInA] datetime  NOT NULL,
    [DateOutA] datetime  NOT NULL,
    [PriceA] int  NOT NULL,
    [Clients_IdClients] int  NOT NULL,
    [Rooms_IdRooms] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdUsers] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([IdUsers] ASC);
GO

-- Creating primary key on [IdReservatons] in table 'ReservationsSet'
ALTER TABLE [dbo].[ReservationsSet]
ADD CONSTRAINT [PK_ReservationsSet]
    PRIMARY KEY CLUSTERED ([IdReservatons] ASC);
GO

-- Creating primary key on [IdClients] in table 'ClientsSet'
ALTER TABLE [dbo].[ClientsSet]
ADD CONSTRAINT [PK_ClientsSet]
    PRIMARY KEY CLUSTERED ([IdClients] ASC);
GO

-- Creating primary key on [IdRooms] in table 'RoomsSet'
ALTER TABLE [dbo].[RoomsSet]
ADD CONSTRAINT [PK_RoomsSet]
    PRIMARY KEY CLUSTERED ([IdRooms] ASC);
GO

-- Creating primary key on [IdAccommodations] in table 'AccommodationsSet'
ALTER TABLE [dbo].[AccommodationsSet]
ADD CONSTRAINT [PK_AccommodationsSet]
    PRIMARY KEY CLUSTERED ([IdAccommodations] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Clients_IdClients] in table 'AccommodationsSet'
ALTER TABLE [dbo].[AccommodationsSet]
ADD CONSTRAINT [FK_ClientsAccommodations]
    FOREIGN KEY ([Clients_IdClients])
    REFERENCES [dbo].[ClientsSet]
        ([IdClients])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientsAccommodations'
CREATE INDEX [IX_FK_ClientsAccommodations]
ON [dbo].[AccommodationsSet]
    ([Clients_IdClients]);
GO

-- Creating foreign key on [Clients_IdClients] in table 'ReservationsSet'
ALTER TABLE [dbo].[ReservationsSet]
ADD CONSTRAINT [FK_ClientsReservations]
    FOREIGN KEY ([Clients_IdClients])
    REFERENCES [dbo].[ClientsSet]
        ([IdClients])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientsReservations'
CREATE INDEX [IX_FK_ClientsReservations]
ON [dbo].[ReservationsSet]
    ([Clients_IdClients]);
GO

-- Creating foreign key on [Rooms_IdRooms] in table 'ReservationsSet'
ALTER TABLE [dbo].[ReservationsSet]
ADD CONSTRAINT [FK_RoomsReservations]
    FOREIGN KEY ([Rooms_IdRooms])
    REFERENCES [dbo].[RoomsSet]
        ([IdRooms])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomsReservations'
CREATE INDEX [IX_FK_RoomsReservations]
ON [dbo].[ReservationsSet]
    ([Rooms_IdRooms]);
GO

-- Creating foreign key on [Rooms_IdRooms] in table 'AccommodationsSet'
ALTER TABLE [dbo].[AccommodationsSet]
ADD CONSTRAINT [FK_RoomsAccommodations]
    FOREIGN KEY ([Rooms_IdRooms])
    REFERENCES [dbo].[RoomsSet]
        ([IdRooms])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomsAccommodations'
CREATE INDEX [IX_FK_RoomsAccommodations]
ON [dbo].[AccommodationsSet]
    ([Rooms_IdRooms]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------