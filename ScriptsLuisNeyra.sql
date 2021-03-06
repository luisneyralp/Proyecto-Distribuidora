USE [master]
GO
/****** Object:  Database [ProyectoLuisNeyra]    Script Date: 13/10/2017 0:55:06 ******/
CREATE DATABASE [ProyectoLuisNeyra]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoLuisNeyra', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ProyectoLuisNeyra.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProyectoLuisNeyra_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ProyectoLuisNeyra_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProyectoLuisNeyra] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoLuisNeyra].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoLuisNeyra] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoLuisNeyra] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ProyectoLuisNeyra] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ProyectoLuisNeyra]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 13/10/2017 0:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 13/10/2017 0:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[MontoTotal] [money] NOT NULL,
	[Definitivo] [int] NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Producto]    Script Date: 13/10/2017 0:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Precio] [real] NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
	[ImagenUri] [nvarchar](50) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[Destacado] [int] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Producto_Pedido]    Script Date: 13/10/2017 0:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto_Pedido](
	[ProductoId] [int] NOT NULL,
	[PedidoId] [int] NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 13/10/2017 0:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Direccion] [nvarchar](50) NOT NULL,
	[TipoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

GO
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (1, N'Almacen')
GO
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (2, N'Limpieza')
GO
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (3, N'Bebidas')
GO
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (4, N'Frescos')
GO
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedido] ON 

GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (1, 2014, 8426.0000, 0)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (2, 2008, 880.0000, 1)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (3, 2015, 2882.0000, 1)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (4, 2017, 1580.0000, 1)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (5, 2020, 7268.0000, 1)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (6, 2013, 2933.0000, 1)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (8, 2011, 0.0000, 0)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (9, 2016, 0.0000, 0)
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [MontoTotal], [Definitivo]) VALUES (12, 2025, 0.0000, 0)
GO
SET IDENTITY_INSERT [dbo].[Pedido] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (1, N'523646', N'Cerveza', 499, N'Stella artois,1L,1Caja', N'~/uploads/productos/bebidas1.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (2, N'1234567', N'Cerveza', 355, N'Quilmes,1L,1caja', N'~/uploads/productos/quilmes.jpg', 3, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (3, N'61061583', N'Leche', 190, N'Serenisima, 1L, 10unidades', N'~/uploads/productos/frescos1.jpg', 4, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (4, N'0628862486', N'Dulce de Leche', 29, N'Serenisima, 500g, unidad', N'~/uploads/productos/almacen3.jpg', 1, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (5, N'13694963', N'Papel', 220, N'Scott, 80m x 4 , 10unidades', N'~/uploads/productos/limpieza1.jpg', 2, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (6, N'987369385', N'Escoba', 31, N'unidad, razor', N'~/uploads/productos/limpieza5.jpg', 2, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (11, N'1353657', N'Azucar', 170, N'Ledesma, 1K, 10unidades', N'~/uploads/productos/almacen6.jpg', 1, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (12, N'963568', N'Arroz', 200, N'Ala, 1K, 10unidades', N'~/uploads/productos/almacen5.jpg', 1, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (13, N'553757', N'Vino', 252, N'Viejo Solar, 1L, 6unidades', N'~/uploads/productos/bebidas5.jpg', 3, 1)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (15, N'12fan2aia', N'Aerosol Ambiente', 42, N'Blem, 1unidad', N'~/uploads/productos/blem.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (16, N'agi13inaw', N'Vino', 270, N'Alvear de 1L, Caja 6unidades', N'~/uploads/productos/alvear.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (17, N'nv103vn1', N'Fernet', 170, N'Branca, 1L, 1 unidad', N'~/uploads/productos/branca.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (18, N'fa2if13g', N'Shampoo', 60, N'Clear men 700cm3, 1unidad', N'~/uploads/productos/clear.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (19, N'12ir1qrw1', N'Escobilla', 29, N'1unidad', N'~/uploads/productos/escobilla.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (20, N'asf112656g', N'Gancia', 85, N'Gancia 1L, 1unidad', N'~/uploads/productos/gancia.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (21, N'135xzvs15', N'Aerosol Ambiente', 36, N'Glade, 1unidad', N'~/uploads/productos/glade.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (22, N'90GJX0D9', N'Jabon', 10, N'Rexona, 1unidad', N'~/uploads/productos/jabon.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (23, N'FN02HFA2', N'Jagermeister', 350, N'Jagermeister 700ml 1unidad', N'~/uploads/productos/jager.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (24, N'FWI103GH', N'Vino', 180, N'Michael Torino tinto 900ml, Caja 6unidades', N'~/uploads/productos/michael.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (25, N'BJX90BX0', N'Lavandina', 25, N'Querubin, 1.5L, 1unidad', N'~/uploads/productos/querubin.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (26, N'89XFBH8', N'Shampoo', 35, N'Sedal, 500cm3, 1unidad', N'~/uploads/productos/sedal.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (27, N'GKLX8X8', N'Gaseosa', 440, N'Sprite, 2.25L Pack 8unidades', N'~/uploads/productos/sprite.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (28, N'J9XDX80', N'Vino', 75, N'Cosecha Tardia 900ml, 1unidad', N'~/uploads/productos/tardia.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (29, N'8ABHE80', N'Vino', 210, N'Tocornal Tinto, 900ml Caja 6unidades', N'~/uploads/productos/tocornal.jpg', 3, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (30, N'0JEA80E', N'Trapo', 17, N'El Trapito, 1unidad', N'~/uploads/productos/trapo.jpg', 2, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (31, N'G1S9G4SH', N'Aceite', 350, N'Natura 1L. Caja 10unidades', N'~/uploads/productos/aceite.jpg', 1, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (32, N'GJ931GJ', N'Harina', 180, N'Blancaflor, 1k ,10unidades', N'~/uploads/productos/harina.jpg', 1, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (33, N'J9GA9JHH', N'Lenteja', 160, N'Coto, no te conozco', N'~/uploads/productos/lentejas.jpg', 1, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (34, N'FG9A3GJ9', N'Fideos', 180, N'Matarazzo, 500g, 10unidades', N'~/uploads/productos/matarasocodito.jpg', 1, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (35, N'F3AGSGS3', N'Savora', 17, N'Savora, 500mg, 1unidad', N'~/uploads/productos/mostaza.jpg', 1, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (36, N'FA3GSG5H', N'Hamburguesa', 470, N'Paty, cajax4. 12unidades', N'~/uploads/productos/paty.jpg', 4, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (37, N'F2AGGE3S', N'Pure de Tomate', 150, N'Campagñola, 500cm3, 10unidades', N'~/uploads/productos/puretomate.jpg', 1, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (38, N'9JG9DJ9', N'Salchicha', 25, N'Vienisima, x6, 1unidad', N'~/uploads/productos/vienisima.jpg', 4, 0)
GO
INSERT [dbo].[Producto] ([Id], [Codigo], [Nombre], [Precio], [Descripcion], [ImagenUri], [CategoriaId], [Destacado]) VALUES (39, N'09BFXJ90', N'Yogurt', 39, N'Serenisima, 1L, 1unidad', N'~/uploads/productos/yogurtcremix.jpg', 4, 0)
GO
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (5, 2, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (3, 1, 3)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (13, 1, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (5, 1, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (1, 1, 7)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (3, 12, 5)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (1, 12, 2)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (6, 3, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (13, 3, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (12, 3, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (16, 5, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (24, 5, 3)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (29, 5, 2)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (28, 5, 6)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (12, 4, 3)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (5, 4, 1)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (3, 4, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (3, 3, 5)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (13, 5, 15)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (11, 6, 4)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (1, 6, 3)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (13, 6, 3)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (2, 1, 5)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (1, 5, 2)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (31, 1, 2)
GO
INSERT [dbo].[Producto_Pedido] ([ProductoId], [PedidoId], [Cantidad]) VALUES (11, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (1, N'Luis', N'Neyra', N'luisneyralp@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'604 e/ 8 y 9', 0)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2008, N'Fiorella', N'Sica', N'fiorella@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'10 y 47', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2011, N'Pedro', N'Picapiedra', N'pedropicalapiedra@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'604 y 72', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2013, N'Mariela', N'Sica', N'mariSica@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'24 y 63', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2014, N'Sebas', N'Secenawro', N'elseba@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'bien lejos', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2015, N'David', N'Blasco', N'david@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'63 y 10', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2016, N'Sofia', N'Rios', N'sofia@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'122 y 123', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2017, N'Javier', N'Acosta', N'javier@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'1 y 508', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2020, N'Mariano', N'Covatti', N'marianocovatti@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'5 y 47', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2025, N'Martin', N'Othinus', N'martinps_cc@gmail.com', N'E59pyTwEJJao6VjsWTBmLGzMr78=', N'122 y 52', 1)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Password], [Direccion], [TipoUsuario]) VALUES (2026, N'Soledad', N'Silveyra', N'soledad@gmail.com', N'btWDPPNShuv4Zit7WUnw10K77D8=', N'12536', 1)
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Usuario]    Script Date: 13/10/2017 0:55:06 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuario] ON [dbo].[Usuario]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Usuario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Usuario]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categoria] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Producto_Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Pedido_Pedido] FOREIGN KEY([PedidoId])
REFERENCES [dbo].[Pedido] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Producto_Pedido] CHECK CONSTRAINT [FK_Producto_Pedido_Pedido]
GO
ALTER TABLE [dbo].[Producto_Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Pedido_Producto] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Producto] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Producto_Pedido] CHECK CONSTRAINT [FK_Producto_Pedido_Producto]
GO
USE [master]
GO
ALTER DATABASE [ProyectoLuisNeyra] SET  READ_WRITE 
GO
