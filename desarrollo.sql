-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 20-02-2020 a las 23:38:32
-- Versión del servidor: 10.1.37-MariaDB
-- Versión de PHP: 7.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `desarrollo`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `autorizaciones`
--

CREATE TABLE `autorizaciones` (
  `codigo` varchar(255) DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `hora` time DEFAULT NULL,
  `vendedor` int(11) DEFAULT NULL,
  `motivo` varchar(255) DEFAULT NULL,
  `estado` varchar(255) DEFAULT NULL,
  `sucursal` varchar(255) DEFAULT NULL,
  `autorizo` int(11) DEFAULT NULL,
  `obs` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `bancos`
--

CREATE TABLE `bancos` (
  `Codigo` int(11) DEFAULT NULL,
  `descripcion` varchar(255) DEFAULT NULL,
  `sucursal` varchar(255) DEFAULT NULL,
  `contacto` varchar(255) DEFAULT NULL,
  `numerocuenta` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `bitacoraordenes`
--

CREATE TABLE `bitacoraordenes` (
  `idorden` int(11) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `informe` varchar(255) DEFAULT NULL,
  `idvendedor` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `bundles`
--

CREATE TABLE `bundles` (
  `IdBundle` int(11) DEFAULT NULL,
  `Nombre` varchar(50) DEFAULT NULL,
  `Porcentaje` float DEFAULT NULL,
  `Precio` float DEFAULT NULL,
  `Activo` tinyint(1) DEFAULT NULL,
  `Cantidad` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cae`
--

CREATE TABLE `cae` (
  `faccodigo` int(11) DEFAULT NULL,
  `cae` varchar(255) DEFAULT NULL,
  `vencimiento` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cajadiaria`
--

CREATE TABLE `cajadiaria` (
  `idcajadiaria` int(11) NOT NULL,
  `idcaja` int(11) NOT NULL,
  `idvendedor` int(11) DEFAULT NULL,
  `fechaapertura` datetime DEFAULT NULL,
  `fechacierre` datetime DEFAULT NULL,
  `saldocierre` float DEFAULT NULL,
  `saldoapertura` float DEFAULT NULL,
  `moneda005` int(11) DEFAULT NULL,
  `moneda010` int(11) DEFAULT NULL,
  `moneda025` int(11) DEFAULT NULL,
  `moneda050` int(11) DEFAULT NULL,
  `moneda1` int(11) DEFAULT NULL,
  `moneda2` int(11) DEFAULT NULL,
  `billete2` int(11) DEFAULT NULL,
  `billete5` int(11) DEFAULT NULL,
  `billete10` int(11) DEFAULT NULL,
  `billete20` int(11) DEFAULT NULL,
  `billete50` int(11) DEFAULT NULL,
  `billete100` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cajas`
--

CREATE TABLE `cajas` (
  `idcaja` int(11) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `idcuentafondo` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE `categorias` (
  `IDCategoria` int(11) NOT NULL,
  `Activo` tinyint(1) DEFAULT NULL,
  `Nombre` varchar(50) DEFAULT NULL,
  `CategoriaPadre` int(11) DEFAULT NULL,
  `Descripcion` varchar(255) DEFAULT NULL,
  `MetaTitulo` varchar(50) DEFAULT NULL,
  `MetaPalabrasClave` varchar(50) DEFAULT NULL,
  `MetaDescripcion` varchar(255) DEFAULT NULL,
  `URLAmigable` varchar(50) DEFAULT NULL,
  `URLImagen` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`IDCategoria`, `Activo`, `Nombre`, `CategoriaPadre`, `Descripcion`, `MetaTitulo`, `MetaPalabrasClave`, `MetaDescripcion`, `URLAmigable`, `URLImagen`) VALUES
(1, 0, 'Pc Escritorio', 0, 'Computadoras de escritorio con tecnologia INTEL y AMD', '', '', '', 'pc-escritorio', ''),
(2, 0, 'Accesorios', 0, '', '', '', '', 'accesorios', ''),
(3, 0, 'Hardware', 0, 'Discos, Grabadoras, Placas madres, Video, Memorias, Gabinetes, fuentes y otros', '', 'hw,hardware', '', 'hardware', ''),
(4, 0, 'Portatiles y Moviles', 0, 'Notebook, Netbook, Tablet PC, Celulares Smartfone y accesorios para notebooks', '', '', '', 'portatiles-y-moviles', 'Portatiles2.jpg'),
(5, 0, 'Insumos', 0, 'Libreria, CD, DVD, Bolsitas, Sobres de papel, Resmas, Papel Foto y Otros', '', 'Libreria, CD, DVD, Bolsitas, sobres, papel foto', '', 'insumos', ''),
(6, 0, 'Cartuchos / Tintas / Cintas', 0, '', '', '', '', 'cartuchos-tintas-cintas', ''),
(7, 0, 'Camaras Digitales', 130, '', '', '', '', 'camaras-digitales', ''),
(8, 0, 'Parlantes', 130, '', '', '', '', 'parlantes', ''),
(9, 0, 'Conectividad y Redes', 0, '', '', '', '', 'conectividad-y-redes', ''),
(10, 0, 'Impresoras', 140, '', '', '', '', 'impresoras', ''),
(11, 0, 'Seguridad', 0, '', '', '', '', 'seguridad', ''),
(12, 0, 'Electronica', 0, '', '', '', '', 'electronica', ''),
(13, 0, 'GPS', 0, '', '', '', '', 'gps', ''),
(14, 0, 'Consolas de juegos', 130, '', '', '', '', 'consolas-de-juegos', ''),
(15, 0, 'Limpieza', 0, '', '', '', '', 'limpieza', ''),
(16, 0, 'Equipos Premium', 1, '', '', '', '', 'equipos-premium', ''),
(17, 0, 'Equipos Home Office', 1, '', '', '', '', 'equipos-home-office', ''),
(18, 0, 'Equipos Media Center', 1, '', '', '', '', 'equipos-media-center', ''),
(19, 0, 'Cables', 2, '', '', '', '', 'cables', ''),
(20, 0, 'Gabinetes', 2, '', '', '', '', 'gabinetes', ''),
(21, 0, 'Camaras Web', 2, '', '', '', '', 'camaras-web', ''),
(22, 0, 'Teclados', 2, '', '', '', '', 'teclados', ''),
(23, 0, 'Mouse y Pads', 2, '', '', '', '', 'mouse-y-pads', ''),
(24, 0, 'Auriculares', 130, '', '', '', '', 'auriculares', ''),
(25, 0, 'Reproductores de Audio y Video', 130, '', '', '', '', 'reproductores-de-audio-y-video', ''),
(26, 0, 'Pendrive / Memorias', 2, '', '', '', '', 'pendrive-memorias', ''),
(27, 0, 'Ventiladores y Disipadores', 2, '', '', '', '', 'ventiladores-y-disipadores', ''),
(28, 0, 'Cargadores / Pilas', 2, '', '', '', '', 'cargadores-pilas', ''),
(29, 0, 'Microfonos', 130, '', '', '', '', 'microfonos', ''),
(30, 0, 'Joystick', 2, '', '', '', '', 'joystick', ''),
(31, 0, 'Paneles Frontales', 2, '', '', '', '', 'paneles-frontales', ''),
(32, 0, 'Adaptadores', 2, '', '', '', '', 'adaptadores', ''),
(33, 0, 'Memorias', 3, 'Memorias ddr/ddr2/ddr3, Micro SD, SD, Pendrive y otras', '', '', '', 'memorias', ''),
(34, 0, 'Tv y Monitores', 130, '', '', '', '', 'tv-y-monitores', ''),
(35, 0, 'Motherboards', 3, '', '', '', '', 'motherboards', ''),
(36, 0, 'Microprocesadores', 3, '', '', '', '', 'microprocesadores', ''),
(37, 0, 'Discos Rigidos', 3, '', '', '', '', 'discos-rigidos', ''),
(38, 0, 'Grabadora/Lectora CD/DVD', 3, '', '', '', '', 'grabadora-lectora-cd-dvd', ''),
(39, 0, 'Fuentes', 3, '', '', '', '', 'fuentes', ''),
(40, 0, 'Estabilizadores/UPS', 3, '', '', '', '', 'estabilizadores-ups', ''),
(41, 0, 'Placas de Video', 3, '', '', '', '', 'placas-de-video', ''),
(42, 0, 'Placas sintonizadoras y Capturadoras', 130, '', '', '', '', 'placas-sintonizadoras-y-capturadoras', ''),
(43, 0, 'Capturadoras', 3, '', '', '', '', 'capturadoras', ''),
(44, 0, 'Placas de sonido', 130, '', '', '', '', 'placas-de-sonido', ''),
(45, 0, 'Lectores de tarjetas', 2, '', '', '', '', 'lectores-de-tarjetas', ''),
(46, 0, 'Papeleria y Libreria', 5, 'resmas, papel fotografico, laminados e insumos para libreria en general', '', '', '', 'papeleria-y-libreria', ''),
(47, 0, 'Sobres', 5, '', '', '', '', 'sobres', ''),
(48, 0, 'Cajas CD/DVD', 5, '', '', '', '', 'cajas-cd-dvd', ''),
(49, 0, 'CD Virgen', 5, '', '', '', '', 'cd-virgen', ''),
(50, 0, 'DVD Virgen', 5, '', '', '', '', 'dvd-virgen', ''),
(51, 0, 'Muebles', 0, '', '', '', '', 'muebles', ''),
(52, 0, 'Celulares', 2, '', '', '', '', 'celulares', ''),
(53, 0, 'Bolsos y Fundas', 2, '\r\n<P>Notebook, Netbook, Tablets, GPS, Celulares, Discos Rigidos</P>', '', '', '', 'bolsos-y-fundas', ''),
(54, 0, 'GAMERS', 0, '', '', '', '', 'gamers', ''),
(55, 0, 'Soportes Multifuncionales', 130, '', '', '', 'Soporte para Monitores', 'soportes-multifuncionales', ''),
(56, 0, 'Rebajas y Descuentos', 0, 'Productos reparados, RF, discontinuos y otros', 'OUTLET', 'productos descontinuos, recertificados', 'Productos discontinuos, Recertificados', 'rebajas-y-descuentos', ''),
(57, 0, 'Acc. P/ Notebook, Netbook y Tablets', 0, 'Baterias, Cargadores, Mouse, Teclados, Pendrive, Lectores de Memoria, Fundas, Hub USB', '', 'Acc. P/ Notebook, Netbook y Tablets', '', 'acc-p-notebook-netbook-y-tablets', 'Acc.-P--Notebook,-Netbook-y-Tablets1.jpg'),
(58, 0, 'ILUMINACION', 0, '', '', '', '', 'iluminacion', ''),
(59, 0, 'Servicios', 0, 'Servicio Reparacion de Pc, Improras, Notebook, Testeo de HW', '', 'servicios', 'servicios', 'servicios', ''),
(60, 0, 'Notebook', 4, '', '', '', '', 'notebook', ''),
(61, 0, 'Netbook', 4, '', '', '', '', 'netbook', ''),
(62, 0, 'Tablet', 4, '', '', '', '', 'tablet', ''),
(63, 0, 'Smartphones', 4, 'Celulares de Gamma Media Alta', '', '', '', 'smartphones', ''),
(64, 0, 'Ultrabook', 4, '', '', '', '', 'ultrabook', ''),
(65, 0, 'RAM', 33, 'ddr/ddr2/ddr3/ddr4', '', '', '', 'ram', ''),
(66, 0, 'Pendrive', 33, 'Pendrive usb 3.0/2.0', '', '', '', 'pendrive', ''),
(67, 0, 'SD y Micro SD', 33, 'Memorias Flash', '', '', '', 'sd-y-micro-sd', ''),
(68, 0, 'Tabletas Graficadoras y Accesorios', 140, 'Diseño de imagenes, Dibujo Profesional', '', '', 'tabletas graficas', 'tabletas-graficadoras-y-accesorios', ''),
(69, 0, '2 en 1', 4, 'tablet 2 en 1', '', '', '', '2-en-1', ''),
(70, 0, 'Flip Cover', 52, 'cubre celulares, protectores y otros', 'Flip Cover', 'Flip Cover', 'Flip Cover', 'flip-cover', ''),
(71, 0, 'TPU', 52, 'Son cubre celulares de plastico rigido. Vienen en diferentes colores y diferentes modelos de celular.', 'TPU', 'TPU', 'celulares TPU', 'tpu', ''),
(72, 0, 'Funda Silicona', 52, 'Cubre celular de silicona flexible. Vienen en diferentes colores y modelos', 'Funda Silicona', 'Funda Silicona', 'Funda Silicona', 'funda-silicona', ''),
(73, 0, 'Film protector', 52, 'FILM PROTECTOR PARA CELULARES, TABLET Y SMARTPHONE.', '', '', 'FILM PROTECTOR', 'film-protector', ''),
(74, 0, 'Lapiz Touch', 2, 'LAPIZ TOUCH PARA SMARTPHONE Y TABLET, PANTALLAS CAPTIVAS, LED, IPS Y OTRAS', '', 'LAPIZ TOUCH', 'LAPIZ TOUCH', 'lapiz-touch', ''),
(75, 0, 'Baterias', 52, 'Baterias para Celulares Originales y Genericas', '', '', 'Baterias Celulares', 'baterias', ''),
(76, 0, 'HDMI', 19, 'CABLES HDMI, MINI Y MICRO PARA PC DISPOSITIVOS MOVILES Y OTROS', '', 'CABLES HDMI', 'CABLES HDMI', 'hdmi', ''),
(77, 0, 'USB (2.0 Y 3.0)', 19, 'CABLE USB PARA IMPRESORAS, CELULARES, DISPOSITIVOS MOVILES, DISCOS RIGIDOS Y OTROS', '', 'CABLES USB', 'CABLES USB', 'usb-2-0-y-3-0', ''),
(78, 0, 'VGA', 19, 'CABLES TIPO VGA, PARA MONITOR, EXTENSION MONITOR Y OTROS', '', 'CABLES VGA PARA MONITORES', 'CABLES VGA PARA MONITORES', 'vga', ''),
(79, 0, 'Camaras GOPRO', 130, 'Accesorios y repuestos para camaras filmadoras deportivas gopro', 'Accesorios para GOPRO', 'Accesorios para GOPRO', 'accesorios para gopro', 'camaras-gopro', ''),
(80, 0, 'Audio', 19, 'RCA-Plug, Opticos, Hdmi, cable de audio Stereo y otros', '', '', 'cables de audio', 'audio', ''),
(81, 0, 'Power ', 19, 'Cables de Energia, para PC, tablet, celulares. Tipo 8 y Trebol.variedad de adaptadores de energia para fuentes de poder comunes y modulares.', '', '', 'cables de energia', 'power', ''),
(82, 0, 'Red UTP', 19, 'CABLE DE RED UTP, PATCHCORD, POR BOBINA. Consultar cantidad. Externo e Interno.', '', '', 'red utp', 'red-utp', ''),
(83, 0, 'Drone', 0, 'Drone, Dispositivos de vuelo sin tripulacion, Accesorios para Drone', 'drone', 'drone', 'drone', 'drone', ''),
(84, 0, 'Productos Destacados', 0, '', '', '', '', 'productos-destacados', ''),
(85, 0, 'Especial en Pc y Notebook', 0, '', '', '', '', 'especial-en-pc-y-notebook', ''),
(86, 0, 'gammer', 22, '', '', '', '', 'gammer', ''),
(87, 0, 'FATHER S DAY', 0, '', '', '', '', 'father-s-day', ''),
(88, 0, 'Spinner ', 0, '', '', '', '', 'spinner', ''),
(89, 0, 'Destacados', 0, '', '', '', '', 'destacados', ''),
(90, 0, 'Auriculares', 89, '', '', '', '', 'auriculares', ''),
(91, 0, 'Fundas y Vidrios para tu Celular', 89, '', '', '', '', 'fundas-y-vidrios-para-tu-celular', ''),
(92, 0, 'Cables y Cargadores', 89, '', '', '', '', 'cables-y-cargadores', ''),
(93, 0, 'Parlantes', 89, '', '', '', '', 'parlantes', ''),
(94, 0, 'Tabletas y Celulares', 89, '', '', '', '', 'tabletas-y-celulares', ''),
(95, 0, 'ACCESORIOS ', 89, '', '', '', '', 'accesorios', ''),
(96, 0, 'Gamer', 89, '', '', '', '', 'gamer', ''),
(97, 0, 'Para PC', 39, '', '', '', '', 'para-pc', ''),
(98, 0, 'Para Notebook', 39, '', '', '', '', 'para-notebook', ''),
(99, 0, 'Para Tablet y Celulares', 39, '', '', '', '', 'para-tablet-y-celulares', ''),
(100, 0, 'Teclados', 54, '', '', '', '', 'teclados', ''),
(101, 0, 'Mouse', 54, '', '', '', '', 'mouse', ''),
(102, 0, 'Refrigeracion', 54, '', '', '', '', 'refrigeracion', ''),
(103, 0, 'Pad Mouse', 54, '', '', '', '', 'pad-mouse', ''),
(104, 0, 'Fuentes de Energia', 54, '', '', '', '', 'fuentes-de-energia', ''),
(105, 0, 'Gabinetes', 54, '', '', '', '', 'gabinetes', ''),
(106, 0, 'Auricular', 54, '', '', '', '', 'auricular', ''),
(107, 0, 'Rebajas y Descuentos', 120, '', '', '', '', 'rebajas-y-descuentos', ''),
(108, 0, 'Sillas', 54, '', '', '', '', 'sillas', ''),
(109, 0, 'Cable UTP', 9, '', '', '', '', 'cable-utp', ''),
(110, 0, 'Fichas RJ45', 9, '', '', '', '', 'fichas-rj45', ''),
(111, 0, 'Placas de Red', 9, '', '', '', '', 'placas-de-red', ''),
(112, 0, 'Router Cableado', 9, '', '', '', '', 'router-cableado', ''),
(113, 0, 'Router WiFI', 9, '', '', '', '', 'router-wifi', ''),
(114, 0, 'Modem Router WiFi', 9, '', '', '', '', 'modem-router-wifi', ''),
(115, 0, 'Amplificadores de Señal', 9, '', '', '', '', 'amplificadores-de-senal', ''),
(116, 0, 'Antenas', 9, '', '', '', '', 'antenas', ''),
(117, 0, 'Acces Point', 9, '', '', '', '', 'acces-point', ''),
(118, 0, 'Switch', 9, '', '', '', '', 'switch', ''),
(119, 0, 'Hasta 50% ', 120, '', '', '', '', 'hasta-50', ''),
(120, 0, 'Ofertas', 0, '', '', '', '', 'ofertas', ''),
(121, 0, 'Hasta 30% ', 120, '', '', '', '', 'hasta-30', ''),
(122, 0, 'Hasta 20% ', 120, '', '', '', '', 'hasta-20', ''),
(123, 0, 'Parlantes y Auriculares', 120, '', '', '', '', 'parlantes-y-auriculares', ''),
(124, 0, 'Herramientas', 9, '', '', '', '', 'herramientas', ''),
(125, 0, 'Joystick y Accesorios', 54, '', '', '', '', 'joystick-y-accesorios', ''),
(126, 0, 'Herramientas', 0, '', '', '', '', 'herramientas', ''),
(127, 0, 'Monitores', 54, '', '', '', '', 'monitores', ''),
(128, 0, 'Parlantes ', 54, '', '', '', '', 'parlantes', ''),
(129, 0, 'Promo Notebooks', 0, '', '', '', '', 'promo-notebooks', ''),
(130, 0, 'Tv, Audio y Video', 0, '', '', '', '', 'tv-audio-y-video', ''),
(131, 0, 'Conversores de audio y video', 130, '', '', '', '', 'conversores-de-audio-y-video', ''),
(132, 0, 'Switch HDMI, KVM Y Splitter ', 130, '', '', '', '', 'switch-hdmi-kvm-y-splitter', ''),
(133, 0, 'Proyectores y Pantallas', 130, '', '', '', '', 'proyectores-y-pantallas', ''),
(134, 0, 'MediaStreaming', 130, '', '', '', '', 'mediastreaming', ''),
(135, 0, 'Cables para Audio y Video', 130, '', '', '', '', 'cables-para-audio-y-video', ''),
(136, 0, ' Ofertas Únicas!!', 0, '', '', '', '', 'ofertas-únicas', ''),
(137, 0, 'Lo Mejor de la Semana', 0, '', '', '', '', 'lo-mejor-de-la-semana', ''),
(138, 0, 'Descuentos de la Semana', 0, '', '', '', '', 'descuentos-de-la-semana', ''),
(139, 0, 'Imperdibles de este Mes', 0, '', '', '', '', 'imperdibles-de-este-mes', ''),
(140, 0, 'Diseño Impresión e Insumos', 0, '', '', '', '', 'diseño-impresión-e-insumos', ''),
(141, 0, 'Hub USB', 2, '', '', '', '', 'hub-usb', ''),
(142, 0, 'Para Camaras e Iluminacion', 39, '', '', '', '', 'para-camaras-e-iluminacion', ''),
(143, 0, 'Tv y Monitores', 55, '', '', '', '', 'tv-y-monitores', ''),
(144, 0, 'Tablets y Celulares', 55, '', '', '', '', 'tablets-y-celulares', ''),
(145, 0, 'Para PC', 27, '', '', '', '', 'para-pc', ''),
(146, 0, 'Para Notebook', 27, '', '', '', '', 'para-notebook', ''),
(147, 0, 'Tablets y Celulares', 27, '', '', '', '', 'tablets-y-celulares', ''),
(148, 0, 'Relojes', 4, '', '', '', '', 'relojes', ''),
(149, 0, 'Relojes Smartwatch', 4, '', '', '', '', 'relojes-smartwatch', ''),
(150, 0, 'Samsung', 0, '', '', '', '', 'samsung', ''),
(151, 0, 'Lista de Deseos Navidad 2018', 0, '', '', '', '', 'lista-de-deseos-navidad-2018', ''),
(152, 0, 'Tintas para Impresoras', 140, '', '', '', '', 'tintas-para-impresoras', ''),
(153, 0, 'Papel Fotografico', 140, '', '', '', '', 'papel-fotografico', ''),
(154, 0, 'Resmas de Papel', 140, '', '', '', '', 'resmas-de-papel', ''),
(155, 0, 'Cartuchos Ink-jet', 140, '', '', '', '', 'cartuchos-ink-jet', ''),
(156, 0, 'Cartuchos e insumos de Toner', 140, '', '', '', '', 'cartuchos-e-insumos-de-toner', ''),
(157, 0, 'HOT SALE', 0, '', '', '', '', 'hot-sale', ''),
(158, 0, 'Escaners y Accesorios', 140, '', '', '', '', 'escaners-y-accesorios', ''),
(159, 0, 'Punteros y presentadores', 130, '', '', '', '', 'punteros-y-presentadores', ''),
(160, 0, 'Calculadoras', 12, '', '', '', '', 'calculadoras', '');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cftarjetas`
--

CREATE TABLE `cftarjetas` (
  `idtarjeta` varchar(255) DEFAULT NULL,
  `cuotas` varchar(255) DEFAULT NULL,
  `cf` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `cftarjetas`
--

INSERT INTO `cftarjetas` (`idtarjeta`, `cuotas`, `cf`) VALUES
('AMERICAN EXPRESS', '1', 0),
('AMERICAN EXPRESS', '2', 8.5),
('AMERICAN EXPRESS', '3', 11.8),
('AMERICAN EXPRESS', 'Ahora 12', 9.15),
('AMERICAN EXPRESS', 'Ahora 18', 13.26),
('AMERICAN EXPRESS', '6', 19.17),
('CABAL', 'Ahora 12', 9.15),
('CABAL', 'Ahora 18', 13.26),
('CABAL', '1', 0),
('CABAL', '2', 11.55),
('CABAL', '3', 11.55),
('CABAL', '6', 19.145),
('CABAL', '12', 0),
('CABAL DEBITO', '1', 0),
('DINERS CLUB', '1', 0),
('DINERS CLUB', '2', 8.61),
('DINERS CLUB', '3', 11.369),
('DINERS CLUB', '6', 18.98),
('DINERS CLUB', 'Ahora 12', 9.15),
('DINERS CLUB', 'Ahora 18', 13.26),
('DINERS CLUB', '12', 0),
('ELECTRON', '1', 0),
('MAESTRO', '1', 0),
('MASTER DEBITO-MAESTRO', '1', 0),
('MASTERCARD', '1', 0),
('MASTERCARD', '2', 8.61),
('MASTERCARD', '3', 11.369),
('MASTERCARD', '6', 18.98),
('MASTERCARD', 'Ahora 12', 9.15),
('MASTERCARD', 'Ahora 18', 13.26),
('MASTERCARD', '12', 0),
('MASTERCARD DEBITO / MAESTRO', '1', 0),
('MERCADOPAGO', '1', 0),
('NARANJA', '1', 0),
('NARANJA', 'Plan Z', 5.4),
('NATIVA', '2', 8.61),
('NATIVA', '3', 11.369),
('NATIVA', '6', 19.98),
('NATIVA', 'Ahora 12', 9.15),
('NATIVA', 'Ahora 18', 13.26),
('NATIVA', '12', 0),
('NATIVA', '1', 0),
('NEVADA', '1', 0),
('TODOPAGO', '1', 0),
('TUYA', '3', 4.5),
('TUYA', '6', 4.5),
('TUYA', '10', 14),
('TUYA', '12', 9),
('TUYA', '18', 13),
('TUYA', '24', 20),
('TUYA', '1', 0),
('TUYA', '2', 4.5),
('VISA', '12', 0),
('VISA', '6', 18.98),
('VISA', 'Ahora 12', 9.15),
('VISA', 'Ahora 18', 13.26),
('VISA', '2', 8.61),
('VISA', '3', 11.369),
('VISA', '1', 0),
('VISA DEBITO', '1', 0),
('VISA DEBITO-ELECTRON', '1', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cheques`
--

CREATE TABLE `cheques` (
  `IdCheque` int(11) DEFAULT NULL,
  `CodigoBanco` int(11) DEFAULT NULL,
  `CodRecibo` int(11) DEFAULT NULL,
  `FechaEmision` datetime DEFAULT NULL,
  `FechaPago` datetime DEFAULT NULL,
  `importe` double DEFAULT NULL,
  `tipoCheque` varchar(255) DEFAULT NULL,
  `Cuit` int(11) DEFAULT NULL,
  `Titular` varchar(255) DEFAULT NULL,
  `numerocheque` int(11) DEFAULT NULL,
  `cuentaactual` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientelista`
--

CREATE TABLE `clientelista` (
  `clicodigo` int(11) DEFAULT NULL,
  `idlista` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `CliCodigo` int(11) NOT NULL,
  `CliRazonSocial` varchar(80) NOT NULL,
  `CliTipoDoc` varchar(4) DEFAULT NULL,
  `CliNumDoc` varchar(15) DEFAULT NULL,
  `IvaCodigo` tinyint(4) DEFAULT NULL,
  `CliDireccion` varchar(150) DEFAULT NULL,
  `CliLocalidad` varchar(90) DEFAULT NULL,
  `CliProvincia` varchar(60) DEFAULT NULL,
  `CliCPostal` varchar(20) DEFAULT NULL,
  `CliTelefono` varchar(80) DEFAULT NULL,
  `CliEmail` varchar(50) DEFAULT NULL,
  `CliEstado` tinyint(4) DEFAULT NULL,
  `CliObservaciones` varchar(255) DEFAULT NULL,
  `CliFechaAlta` date DEFAULT NULL,
  `CliMontoDisponible` double DEFAULT NULL,
  `CliTipoCliente` varchar(150) DEFAULT NULL,
  `CliActualiza` tinyint(4) DEFAULT NULL,
  `CliCtaDias` smallint(6) DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL,
  `cli_recargas` int(11) DEFAULT NULL,
  `cli_recargas_toner` int(11) DEFAULT NULL,
  `idtransporte` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `compras`
--

CREATE TABLE `compras` (
  `ComCodigo` int(11) NOT NULL,
  `ComComprobante` varchar(50) DEFAULT NULL,
  `ComTipoComprobante` varchar(30) NOT NULL,
  `ComFecha` datetime NOT NULL,
  `ComHora` datetime NOT NULL,
  `ProvCodigo` int(11) DEFAULT NULL,
  `ComSubtotal` double NOT NULL,
  `ComDescuento` double DEFAULT NULL,
  `Iva21` double DEFAULT NULL,
  `Iva10` double DEFAULT NULL,
  `ComTotal` double NOT NULL,
  `ComRetGanancia` double DEFAULT NULL,
  `ComRetRentas` double DEFAULT NULL,
  `Pago` varchar(10) DEFAULT NULL,
  `Estado` varchar(255) DEFAULT NULL,
  `FechaVencimiento` datetime DEFAULT NULL,
  `PagoParcial` double DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL,
  `ComRemito` varchar(255) DEFAULT NULL,
  `Observaciones` varchar(255) DEFAULT NULL,
  `ComFechaCarga` datetime DEFAULT NULL,
  `RtoId` int(11) DEFAULT NULL,
  `ImpuestoInterno` double DEFAULT NULL,
  `Cotizacion` int(11) DEFAULT NULL,
  `ComPercepciones` double DEFAULT NULL,
  `ComRetIVA` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `conceptos`
--

CREATE TABLE `conceptos` (
  `ConCodigo` int(11) DEFAULT NULL,
  `Alias` varchar(255) DEFAULT NULL,
  `ConDescripcion` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `configinformes`
--

CREATE TABLE `configinformes` (
  `id` int(11) DEFAULT NULL,
  `categoria` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `configuracionconceptos`
--

CREATE TABLE `configuracionconceptos` (
  `Formulario` varchar(50) DEFAULT NULL,
  `IvaDescripcion` varchar(50) DEFAULT NULL,
  `comprobante` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cotizaciondolar`
--

CREATE TABLE `cotizaciondolar` (
  `cotizacion` float DEFAULT NULL,
  `fecha` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ctas`
--

CREATE TABLE `ctas` (
  `CtaCteID` int(11) DEFAULT NULL,
  `CliCodigo` int(11) DEFAULT NULL,
  `CtaDescripcion` varchar(100) DEFAULT NULL,
  `CtaCteFecha` date DEFAULT NULL,
  `CtaCteDebe` float DEFAULT NULL,
  `CtaCteHaber` float DEFAULT NULL,
  `CtaCteSaldo` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ctasclientes`
--

CREATE TABLE `ctasclientes` (
  `IdCtaDetalle` int(11) DEFAULT NULL,
  `CliCodigo` int(11) DEFAULT NULL,
  `Fecha` date DEFAULT NULL,
  `Comprobante` int(11) DEFAULT NULL,
  `TipoComprobante` varchar(50) DEFAULT NULL,
  `Debe` double DEFAULT NULL,
  `haber` double DEFAULT NULL,
  `saldo` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ctasgenerales`
--

CREATE TABLE `ctasgenerales` (
  `CtaID` int(11) DEFAULT NULL,
  `CtaDenominacion` varchar(255) DEFAULT NULL,
  `Fecha` date DEFAULT NULL,
  `padre` int(11) DEFAULT NULL,
  `imputable` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ctasproveedores`
--

CREATE TABLE `ctasproveedores` (
  `CtaCteID` int(11) DEFAULT NULL,
  `CliCodigo` int(11) DEFAULT NULL,
  `CtaDescripcion` varchar(100) DEFAULT NULL,
  `CtaCteFecha` datetime DEFAULT NULL,
  `CtaCteDebe` float DEFAULT NULL,
  `CtaCteHaber` float DEFAULT NULL,
  `CtaCteSaldo` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuponestarjetas`
--

CREATE TABLE `cuponestarjetas` (
  `idCupon` int(11) DEFAULT NULL,
  `idTarjeta` int(11) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `NroCupon` int(11) DEFAULT NULL,
  `plan` varchar(255) DEFAULT NULL,
  `NroTarjeta` varchar(255) DEFAULT NULL,
  `importe` double DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL,
  `CodRecibo` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleautorizacion`
--

CREATE TABLE `detalleautorizacion` (
  `codigo` varchar(255) DEFAULT NULL,
  `procodigo` int(11) DEFAULT NULL,
  `prodescripcion` varchar(255) DEFAULT NULL,
  `procantidad` int(11) DEFAULT NULL,
  `proprecioventa` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallecompras`
--

CREATE TABLE `detallecompras` (
  `ComCodigo` int(11) DEFAULT NULL,
  `ProCodigo` varchar(20) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` float NOT NULL,
  `DetCosto` double NOT NULL,
  `DetSubtotal` double NOT NULL,
  `DetIva` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallectacte`
--

CREATE TABLE `detallectacte` (
  `CtaCteID` int(11) DEFAULT NULL,
  `DetFecha` datetime DEFAULT NULL,
  `DetConcepto` varchar(255) DEFAULT NULL,
  `DetMonto` float DEFAULT NULL,
  `DetDebitoCredito` varchar(1) DEFAULT NULL,
  `DetComprobante` varchar(50) DEFAULT NULL,
  `DetTipoComprobante` varchar(255) DEFAULT NULL,
  `DetFecCancelacion` varchar(255) DEFAULT NULL,
  `DetParcial` float DEFAULT NULL,
  `escontable` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallectactegeneral`
--

CREATE TABLE `detallectactegeneral` (
  `iddetalle` int(11) NOT NULL,
  `idCtaCteGeneral` int(11) NOT NULL,
  `Fecha` date DEFAULT NULL,
  `NroComprobante` varchar(50) DEFAULT NULL,
  `TipoComprobante` varchar(255) DEFAULT NULL,
  `Debe` float DEFAULT NULL,
  `haber` float DEFAULT NULL,
  `saldo` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallectasproveedores`
--

CREATE TABLE `detallectasproveedores` (
  `CtaCteID` int(11) DEFAULT NULL,
  `DetFecha` datetime DEFAULT NULL,
  `DetConcepto` varchar(250) DEFAULT NULL,
  `DetMonto` float DEFAULT NULL,
  `DetDebitoCredito` varchar(1) DEFAULT NULL,
  `DetComprobante` varchar(250) DEFAULT NULL,
  `DetTipoComprobante` varchar(250) DEFAULT NULL,
  `DetFecCancelacion` varchar(250) DEFAULT NULL,
  `DetParcial` float DEFAULT NULL,
  `DetCodigo` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallelotesstock`
--

CREATE TABLE `detallelotesstock` (
  `idlote` int(11) NOT NULL,
  `idproducto` int(11) DEFAULT NULL,
  `descripcion` varchar(255) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `cantidadstock` int(11) DEFAULT NULL,
  `diferencia` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleoproduccion`
--

CREATE TABLE `detalleoproduccion` (
  `IdOrden` int(11) DEFAULT NULL,
  `ProCodigo` int(11) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` varchar(50) NOT NULL,
  `DetPrecioVenta` double NOT NULL,
  `DetSubtotal` double NOT NULL,
  `DetIva` double DEFAULT NULL,
  `DetPrecioCosto` double DEFAULT NULL,
  `ProSeries` tinyint(4) DEFAULT NULL,
  `Completo` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleoservicio`
--

CREATE TABLE `detalleoservicio` (
  `idorden` int(11) DEFAULT NULL,
  `idproducto` varchar(255) DEFAULT NULL,
  `prodescripcion` varchar(255) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `preciounitario` float DEFAULT NULL,
  `preciototal` float DEFAULT NULL,
  `esgarantia` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallepedidos`
--

CREATE TABLE `detallepedidos` (
  `RtoId` int(11) NOT NULL,
  `ProCodigo` int(11) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallepresu`
--

CREATE TABLE `detallepresu` (
  `PresuID` int(11) DEFAULT NULL,
  `ProCodigo` int(11) DEFAULT NULL,
  `ProDescripcion` varchar(255) DEFAULT NULL,
  `ProCantidad` int(11) DEFAULT NULL,
  `DetPrecio` double DEFAULT NULL,
  `DetSubtotal` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallerecibos`
--

CREATE TABLE `detallerecibos` (
  `ComCodigo` int(11) DEFAULT NULL,
  `ProCodigo` varchar(20) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` varchar(10) NOT NULL,
  `DetCosto` double NOT NULL,
  `DetSubtotal` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleremito`
--

CREATE TABLE `detalleremito` (
  `RtoId` int(11) NOT NULL,
  `ProCodigo` int(11) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleremitoprov`
--

CREATE TABLE `detalleremitoprov` (
  `RtoId` int(11) NOT NULL,
  `ProCodigo` int(11) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` int(11) NOT NULL,
  `ProSeries` tinyint(4) DEFAULT NULL,
  `Completo` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleremitosucursal`
--

CREATE TABLE `detalleremitosucursal` (
  `RtoId` int(11) NOT NULL,
  `ProCodigo` int(11) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` int(11) NOT NULL,
  `ProSeries` tinyint(4) DEFAULT NULL,
  `Completo` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleventas`
--

CREATE TABLE `detalleventas` (
  `FacCodigo` int(11) DEFAULT NULL,
  `ProCodigo` int(11) NOT NULL,
  `ProDescripcion` varchar(255) NOT NULL,
  `ProCantidad` double NOT NULL,
  `DetPrecioVenta` double NOT NULL,
  `DetSubtotal` double NOT NULL,
  `DetIva` double DEFAULT NULL,
  `DetPrecioCosto` double DEFAULT NULL,
  `ProSeries` tinyint(4) DEFAULT NULL,
  `Completo` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `fabricantes`
--

CREATE TABLE `fabricantes` (
  `FabCodigo` smallint(6) NOT NULL,
  `FabDescripcion` varchar(255) DEFAULT NULL,
  `Activo` tinyint(1) DEFAULT NULL,
  `Descripcion` varchar(255) DEFAULT NULL,
  `DescripcionBreve` varchar(255) DEFAULT NULL,
  `MetaTitulo` varchar(50) DEFAULT NULL,
  `MetaPalabrasClave` varchar(50) DEFAULT NULL,
  `MetaDescripcion` varchar(50) DEFAULT NULL,
  `URLImagen` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `gastostarjetas`
--

CREATE TABLE `gastostarjetas` (
  `idrecibo` int(11) DEFAULT NULL,
  `arancel` float DEFAULT NULL,
  `cf` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `gastostemporal`
--

CREATE TABLE `gastostemporal` (
  `GastoId` int(11) DEFAULT NULL,
  `importe` varchar(255) DEFAULT NULL,
  `cuenta` varchar(255) DEFAULT NULL,
  `concepto` varchar(255) DEFAULT NULL,
  `comprobante` varchar(255) DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `guiastransporte`
--

CREATE TABLE `guiastransporte` (
  `idGuia` int(11) DEFAULT NULL,
  `idTransporte` int(11) DEFAULT NULL,
  `RtoId` int(11) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `hora` datetime DEFAULT NULL,
  `bultos` varchar(255) DEFAULT NULL,
  `kilos` varchar(255) DEFAULT NULL,
  `pallet` varchar(255) DEFAULT NULL,
  `pendiente` varchar(4) DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL,
  `ValorEfectivo` double DEFAULT NULL,
  `valorcheques` double DEFAULT NULL,
  `ValorDeclarado` double DEFAULT NULL,
  `valortotal` double DEFAULT NULL,
  `PagaFlete` varchar(255) DEFAULT NULL,
  `DatosEnvio` varchar(255) DEFAULT NULL,
  `remitos` varchar(255) DEFAULT NULL,
  `pedidos` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `imagenes_temp`
--

CREATE TABLE `imagenes_temp` (
  `id_imagen` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cover` int(11) DEFAULT NULL,
  `position` int(11) DEFAULT NULL,
  `ruta` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `kardexproducto`
--

CREATE TABLE `kardexproducto` (
  `IdKardex` int(11) DEFAULT NULL,
  `ProCodigo` int(11) DEFAULT NULL,
  `ProDescripcion` varchar(255) DEFAULT NULL,
  `Tipo` varchar(255) DEFAULT NULL,
  `Cantidad` int(11) DEFAULT NULL,
  `Fecha` date DEFAULT NULL,
  `Comprobante` varchar(255) DEFAULT NULL,
  `NumComprobante` int(11) DEFAULT NULL,
  `Series` varchar(255) DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL,
  `IdSucursal` smallint(6) DEFAULT NULL,
  `Detalle` varchar(255) DEFAULT NULL,
  `saldo` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `listaprecio`
--

CREATE TABLE `listaprecio` (
  `IdLista` int(11) NOT NULL,
  `Nombre` varchar(50) DEFAULT NULL,
  `FechaDesde` date DEFAULT NULL,
  `FechaHasta` date DEFAULT NULL,
  `Importe` float DEFAULT NULL,
  `Porcentaje` float DEFAULT NULL,
  `Condicion` varchar(50) DEFAULT NULL,
  `Descuento` tinyint(4) DEFAULT NULL,
  `defecto` int(11) DEFAULT NULL,
  `sitioweb` int(11) DEFAULT NULL,
  `publicacionespecial` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `listatipo`
--

CREATE TABLE `listatipo` (
  `idlista` int(11) DEFAULT NULL,
  `idtipopago` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `localidad`
--

CREATE TABLE `localidad` (
  `Id_Localidad` int(11) NOT NULL,
  `Nombre` varchar(20) NOT NULL,
  `Id_Provincia` tinyint(4) DEFAULT NULL,
  `Cod_Postal` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `movimientoctasgenerales`
--

CREATE TABLE `movimientoctasgenerales` (
  `IdMovimiento` int(11) DEFAULT NULL,
  `CtaCteID` int(11) DEFAULT NULL,
  `Concepto` varchar(255) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `Debe` float DEFAULT NULL,
  `Haber` float DEFAULT NULL,
  `Saldo` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `movimientos`
--

CREATE TABLE `movimientos` (
  `IdMovimiento` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Serie` varchar(50) DEFAULT NULL,
  `Cantidad` int(11) DEFAULT NULL,
  `Fecha` date DEFAULT NULL,
  `Estado` varchar(50) DEFAULT NULL,
  `SucOrigen` int(11) DEFAULT NULL,
  `SucDestino` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operaciones`
--

CREATE TABLE `operaciones` (
  `CodigoOperacion` smallint(6) DEFAULT NULL,
  `DescripcionOperacion` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ordenesproduccion`
--

CREATE TABLE `ordenesproduccion` (
  `IdOrden` int(11) DEFAULT NULL,
  `Alias` varchar(255) DEFAULT NULL,
  `Fecha` datetime DEFAULT NULL,
  `hora` datetime DEFAULT NULL,
  `Total` float DEFAULT NULL,
  `VendCodigo` int(11) DEFAULT NULL,
  `Estado` varchar(255) DEFAULT NULL,
  `Anulada` tinyint(4) DEFAULT NULL,
  `Seriales` varchar(255) DEFAULT NULL,
  `NumFactura` int(11) DEFAULT NULL,
  `SucursalID` int(11) DEFAULT NULL,
  `Asociado` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ordenservicio`
--

CREATE TABLE `ordenservicio` (
  `idorden` int(11) NOT NULL,
  `idcliente` int(11) DEFAULT NULL,
  `fechaingreso` datetime DEFAULT NULL,
  `motivoingreso` varchar(255) DEFAULT NULL,
  `tipo` varchar(50) DEFAULT NULL,
  `marca` varchar(255) DEFAULT NULL,
  `serie` varchar(255) DEFAULT NULL,
  `prioridad` varchar(250) DEFAULT NULL,
  `hacerbackup` int(11) DEFAULT NULL,
  `cargador` int(11) DEFAULT NULL,
  `recibio` int(11) DEFAULT NULL,
  `fechafin` datetime DEFAULT NULL,
  `motivocierre` varchar(255) DEFAULT NULL,
  `cerro` int(11) DEFAULT NULL,
  `total` float DEFAULT NULL,
  `estado` varchar(50) DEFAULT NULL,
  `fechaaprox` date DEFAULT NULL,
  `garantia` int(11) DEFAULT NULL,
  `fechafact` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `CodigoPago` int(11) DEFAULT NULL,
  `Total` float DEFAULT NULL,
  `FechaPago` datetime DEFAULT NULL,
  `HoraPago` datetime DEFAULT NULL,
  `CliCodigo` int(11) DEFAULT NULL,
  `VendCodigo` int(11) DEFAULT NULL,
  `Observaciones` varchar(255) DEFAULT NULL,
  `saldo` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pedidos`
--

CREATE TABLE `pedidos` (
  `RtoId` int(11) DEFAULT NULL,
  `RtoFecha` datetime DEFAULT NULL,
  `RtoHora` datetime DEFAULT NULL,
  `Clicodigo` int(11) DEFAULT NULL,
  `RtoDescripcion` varchar(100) DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `presu`
--

CREATE TABLE `presu` (
  `PresuId` int(11) NOT NULL,
  `PresTipo` varchar(50) DEFAULT NULL,
  `PresuFecha` date NOT NULL,
  `PresuHora` time NOT NULL,
  `CliCodigo` int(11) DEFAULT NULL,
  `PresuTotal` double NOT NULL,
  `PresuPlazo` varchar(90) DEFAULT NULL,
  `PresuValidez` varchar(90) DEFAULT NULL,
  `PresuCondicion` varchar(80) DEFAULT NULL,
  `PresuEntrega` varchar(70) DEFAULT NULL,
  `PresuDescuento` varchar(50) DEFAULT NULL,
  `PresuObservaciones` varchar(255) DEFAULT NULL,
  `PresuNombre` varchar(70) DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productolista`
--

CREATE TABLE `productolista` (
  `IdLista` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Importe` float DEFAULT NULL,
  `Porcentaje` float DEFAULT NULL,
  `cantidadminima` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `ProCodigo` int(11) DEFAULT NULL,
  `ProCodBar` varchar(255) DEFAULT NULL,
  `ProDescripcion` varchar(255) DEFAULT NULL,
  `ProCantidad` float DEFAULT NULL,
  `ProStockActual` float DEFAULT NULL,
  `ProPrecioCosto` float DEFAULT NULL,
  `ProUtilidadVEfectivo` float DEFAULT NULL,
  `ProUtilidadVCtaCte` float DEFAULT NULL,
  `ProUtilidadVLista` float DEFAULT NULL,
  `ProPrecioNeto` float DEFAULT NULL,
  `ProIVA` float DEFAULT NULL,
  `ProPreEfectivo` float DEFAULT NULL,
  `ProPreLista` float DEFAULT NULL,
  `ProPreCtaCte` float DEFAULT NULL,
  `ProCantMinima` float DEFAULT NULL,
  `ProFecha` date DEFAULT NULL,
  `ProPerfil` varchar(40) DEFAULT NULL,
  `ProSAsociado` tinyint(4) DEFAULT NULL,
  `MonCodigo` int(11) DEFAULT NULL,
  `ProBaja` tinyint(1) DEFAULT NULL,
  `Fabricante` varchar(50) DEFAULT NULL,
  `ProGarantia` smallint(6) DEFAULT NULL,
  `ProWeb` varchar(255) DEFAULT NULL,
  `ProObser1` varchar(255) DEFAULT NULL,
  `ProStockService` float DEFAULT NULL,
  `ProStockFallado` float DEFAULT NULL,
  `ProStockProveedor` float DEFAULT NULL,
  `ProFechaUActualizacion` date DEFAULT NULL,
  `ProGarantiaFabrica` smallint(6) DEFAULT NULL,
  `ProGarantiaCompra` smallint(6) DEFAULT NULL,
  `ProExigeSerie` tinyint(1) DEFAULT NULL,
  `Categoria` varchar(50) DEFAULT NULL,
  `EnOferta` tinyint(1) DEFAULT NULL,
  `ImporteDescuento` float DEFAULT NULL,
  `PorcentajeDescuento` float DEFAULT NULL,
  `DescuentoDesde` date DEFAULT NULL,
  `DescuentoHasta` date DEFAULT NULL,
  `Descripcion` varchar(255) DEFAULT NULL,
  `DescripcionBreve` varchar(255) DEFAULT NULL,
  `MostrarPrecio` tinyint(1) DEFAULT NULL,
  `ExclusivoInternet` tinyint(1) DEFAULT NULL,
  `DisponiblePedidos` tinyint(1) DEFAULT NULL,
  `MetaTitulo` varchar(255) DEFAULT NULL,
  `MetaDescr` varchar(50) DEFAULT NULL,
  `MetaPalabrasClave` varchar(255) DEFAULT NULL,
  `EsBundle` int(11) DEFAULT NULL,
  `alias` varchar(255) DEFAULT NULL,
  `costodolares` float DEFAULT NULL,
  `cotizacion` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productosfallado`
--

CREATE TABLE `productosfallado` (
  `RtoId` int(11) DEFAULT NULL,
  `RtoFecha` datetime DEFAULT NULL,
  `RtoHora` datetime DEFAULT NULL,
  `ProvCodigo` int(11) DEFAULT NULL,
  `RtoObservacion` varchar(70) DEFAULT NULL,
  `RtoValorizado` double DEFAULT NULL,
  `ProCodigo` int(11) DEFAULT NULL,
  `ProDescripcion` varchar(255) DEFAULT NULL,
  `VendCodigo` int(11) DEFAULT NULL,
  `RtoEstado` varchar(255) DEFAULT NULL,
  `RtoNumeroSerie` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos_bundles`
--

CREATE TABLE `productos_bundles` (
  `IdBundle` int(11) DEFAULT NULL,
  `IdProducto` int(11) DEFAULT NULL,
  `Cantidad` int(11) DEFAULT NULL,
  `PrecioFinal` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productotipopago`
--

CREATE TABLE `productotipopago` (
  `IdTipoPago` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedores`
--

CREATE TABLE `proveedores` (
  `ProvCodigo` int(11) NOT NULL,
  `ProvRazonSocial` varchar(150) DEFAULT NULL,
  `ProvTipoDoc` varchar(5) DEFAULT NULL,
  `ProvNumDoc` varchar(15) DEFAULT NULL,
  `IvaCodigo` tinyint(4) DEFAULT NULL,
  `ProvDireccion` varchar(150) DEFAULT NULL,
  `ProvLocalidad` varchar(90) DEFAULT NULL,
  `ProvProvincia` varchar(60) DEFAULT NULL,
  `ProvCPostal` varchar(20) DEFAULT NULL,
  `ProvTelefono` varchar(80) DEFAULT NULL,
  `ProvEmail` varchar(50) DEFAULT NULL,
  `direccionrma` varchar(255) DEFAULT NULL,
  `mailrma` varchar(255) DEFAULT NULL,
  `telefonorma` varchar(255) DEFAULT NULL,
  `contactorma` varchar(255) DEFAULT NULL,
  `gestionrma` varchar(255) DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `provincia`
--

CREATE TABLE `provincia` (
  `Id_Provincia` tinyint(4) NOT NULL,
  `Nombre` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `recibos`
--

CREATE TABLE `recibos` (
  `CodRecibo` int(11) DEFAULT NULL,
  `CodCliente` int(11) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `Total` float DEFAULT NULL,
  `Concepto` varchar(255) DEFAULT NULL,
  `Efectivo` float DEFAULT NULL,
  `Tarjeta` varchar(255) DEFAULT NULL,
  `ImporteTarjeta` float DEFAULT NULL,
  `NumeroCupon` varchar(255) DEFAULT NULL,
  `PlanTarjeta` varchar(255) DEFAULT NULL,
  `NumeroTarjeta` varchar(255) DEFAULT NULL,
  `ContactoTarjeta` varchar(255) DEFAULT NULL,
  `Banco` varchar(255) DEFAULT NULL,
  `ImporteCheque` float DEFAULT NULL,
  `NumeroCheque` varchar(255) DEFAULT NULL,
  `SucursalCheque` varchar(255) DEFAULT NULL,
  `Cuit` varchar(255) DEFAULT NULL,
  `FechaVto` date DEFAULT NULL,
  `ContactoCheque` varchar(255) DEFAULT NULL,
  `hora` time DEFAULT NULL,
  `comprobante` varchar(255) DEFAULT NULL,
  `vendedor` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `registronc`
--

CREATE TABLE `registronc` (
  `faccodigo` int(11) DEFAULT NULL,
  `vendedor` int(11) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `hora` time DEFAULT NULL,
  `motivo` varchar(255) DEFAULT NULL,
  `obs` varchar(255) DEFAULT NULL,
  `comprobantes` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `remito`
--

CREATE TABLE `remito` (
  `RtoId` int(11) DEFAULT NULL,
  `RtoFecha` date DEFAULT NULL,
  `RtoHora` time DEFAULT NULL,
  `Clicodigo` int(11) DEFAULT NULL,
  `RtoObservacion` varchar(255) DEFAULT NULL,
  `RtoTotal` double DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL,
  `RtoFiscal` int(11) DEFAULT NULL,
  `RtoStock` varchar(4) DEFAULT NULL,
  `FacCodigo` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `remitoproveedores`
--

CREATE TABLE `remitoproveedores` (
  `RtoId` int(11) DEFAULT NULL,
  `RtoFecha` datetime DEFAULT NULL,
  `RtoHora` datetime DEFAULT NULL,
  `ProvCodigo` int(11) DEFAULT NULL,
  `RtoObservacion` varchar(255) DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL,
  `RtoProveedor` varchar(255) DEFAULT NULL,
  `Seriales` varchar(255) DEFAULT NULL,
  `TipoMovimiento` varchar(60) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `remitosucursal`
--

CREATE TABLE `remitosucursal` (
  `RtoId` int(11) DEFAULT NULL,
  `RtoFecha` date DEFAULT NULL,
  `RtoHora` time DEFAULT NULL,
  `RtoObservacion` varchar(70) DEFAULT NULL,
  `RtoTotal` double DEFAULT NULL,
  `IdSucursalOrigen` int(11) DEFAULT NULL,
  `IdSucursalDestino` int(11) DEFAULT NULL,
  `VendCodigo` int(11) DEFAULT NULL,
  `Cargado` varchar(255) DEFAULT NULL,
  `Seriales` varchar(100) DEFAULT NULL,
  `StockDestino` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `resiva`
--

CREATE TABLE `resiva` (
  `IvaCodigo` tinyint(4) DEFAULT NULL,
  `IvaDescripcion` varchar(50) DEFAULT NULL,
  `IvaAlias` varchar(50) DEFAULT NULL,
  `IvaAplicable1` float DEFAULT NULL,
  `IvaAplicable2` float DEFAULT NULL,
  `IvaAliasEpson` varchar(5) DEFAULT NULL,
  `IvaAliasHasar` varchar(5) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `resiva`
--

INSERT INTO `resiva` (`IvaCodigo`, `IvaDescripcion`, `IvaAlias`, `IvaAplicable1`, `IvaAplicable2`, `IvaAliasEpson`, `IvaAliasHasar`) VALUES
(1, 'Responsable Inscripto', 'RI', 21, 0, 'I', 'I'),
(2, 'Responsable no Inscripto', 'RNI', 21, 10.5, 'R', 'N'),
(3, 'Exento', 'EXE', 21, 0, 'E', 'E'),
(4, 'No Responsable', 'NRES', 21, 0, 'N', 'A'),
(5, 'Consumidor Final', 'CFIN', 21, 0, 'F', 'C'),
(6, 'Monotributista', 'RMONO', 21, 0, 'M', 'M'),
(7, 'Sujeto no Categorizado', 'SNCAT', 21, 10, 'S', 'T');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rmaestadoproductos`
--

CREATE TABLE `rmaestadoproductos` (
  `IdMovimiento` int(11) DEFAULT NULL,
  `IdRma` int(11) DEFAULT NULL,
  `IdEstado` int(11) DEFAULT NULL,
  `Fecha` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rmaestadoproveedores`
--

CREATE TABLE `rmaestadoproveedores` (
  `IdMovimiento` int(11) DEFAULT NULL,
  `IdRma` int(11) DEFAULT NULL,
  `IdEstado` int(11) DEFAULT NULL,
  `Fecha` datetime DEFAULT NULL,
  `ProvCodigo` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rmaestados`
--

CREATE TABLE `rmaestados` (
  `IdEstado` int(11) DEFAULT NULL,
  `Descripcion` varchar(255) DEFAULT NULL,
  `aplicable` tinyint(4) DEFAULT NULL,
  `prioridad` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rmaproductos`
--

CREATE TABLE `rmaproductos` (
  `IdRma` int(11) DEFAULT NULL,
  `ProCodigo` int(11) DEFAULT NULL,
  `ProDescripcion` varchar(255) DEFAULT NULL,
  `NumComprobante` int(11) DEFAULT NULL,
  `TipoComprobante` varchar(255) DEFAULT NULL,
  `Fecha` date DEFAULT NULL,
  `hora` time DEFAULT NULL,
  `NumeroSerieOrigen` varchar(255) DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL,
  `ComprobanteCambio` int(11) DEFAULT NULL,
  `IdSucursal` int(11) DEFAULT NULL,
  `Estado` varchar(255) DEFAULT NULL,
  `Tramite` varchar(255) DEFAULT NULL,
  `NumFacCompra` varchar(255) DEFAULT NULL,
  `idproveedor` varchar(50) DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rmaproveedores`
--

CREATE TABLE `rmaproveedores` (
  `IdMovimiento` int(11) DEFAULT NULL,
  `ProvCodigo` int(11) DEFAULT NULL,
  `NumeroRma` varchar(50) DEFAULT NULL,
  `FechaEnvio` datetime DEFAULT NULL,
  `VendCodigo` int(11) DEFAULT NULL,
  `Observaciones` varchar(255) DEFAULT NULL,
  `Tramite` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rmaproveedoresdetalle`
--

CREATE TABLE `rmaproveedoresdetalle` (
  `IdMovimiento` int(11) DEFAULT NULL,
  `IdRma` int(11) DEFAULT NULL,
  `NumFacProveedor` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `seriesproductos`
--

CREATE TABLE `seriesproductos` (
  `IdSerie` int(11) DEFAULT NULL,
  `NumeroSerie` varchar(255) DEFAULT NULL,
  `ProCodigo` int(11) DEFAULT NULL,
  `RtoId` int(11) DEFAULT NULL,
  `fechaMovimiento` datetime DEFAULT NULL,
  `Estado` varchar(255) DEFAULT NULL,
  `IdSucursal` smallint(6) DEFAULT NULL,
  `NumComprobante` int(11) DEFAULT NULL,
  `TipoComprobante` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sucursales`
--

CREATE TABLE `sucursales` (
  `IdSucursal` smallint(6) DEFAULT NULL,
  `Nombre` varchar(255) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `localidad` varchar(255) DEFAULT NULL,
  `Provincia` varchar(255) DEFAULT NULL,
  `Telefono` varchar(255) DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL,
  `conexion` varchar(255) DEFAULT NULL,
  `activa` tinyint(4) DEFAULT NULL,
  `imagenes` varchar(255) DEFAULT NULL,
  `baja` tinyint(4) DEFAULT NULL,
  `cuit` float DEFAULT NULL,
  `RazonSocial` varchar(255) DEFAULT NULL,
  `NombreFantasia` varchar(255) DEFAULT NULL,
  `Updated` tinyint(1) DEFAULT NULL,
  `EsCasaCentral` tinyint(1) DEFAULT NULL,
  `mail` varchar(255) DEFAULT NULL,
  `posnet` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tarjetas`
--

CREATE TABLE `tarjetas` (
  `IdTarjeta` int(11) DEFAULT NULL,
  `Descripcion` varchar(255) DEFAULT NULL,
  `coeficiente` float DEFAULT NULL,
  `CodigoBanco` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipospago`
--

CREATE TABLE `tipospago` (
  `idTipoPago` int(11) NOT NULL,
  `Nombre` varchar(50) DEFAULT NULL,
  `Porcentaje` float DEFAULT NULL,
  `defecto` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `transportes`
--

CREATE TABLE `transportes` (
  `idTransporte` int(11) DEFAULT NULL,
  `descripcion` varchar(255) DEFAULT NULL,
  `NumCuit` varchar(255) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `localidad` varchar(255) DEFAULT NULL,
  `provincia` varchar(255) DEFAULT NULL,
  `telefono` varchar(255) DEFAULT NULL,
  `movil` varchar(255) DEFAULT NULL,
  `contacto` varchar(255) DEFAULT NULL,
  `contrareembolso` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `activo` tinyint(4) DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL,
  `direccionalmacenamiento` varchar(255) DEFAULT NULL,
  `zona` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `vendedores`
--

CREATE TABLE `vendedores` (
  `VendCodigo` smallint(6) NOT NULL,
  `VendNombre` varchar(150) DEFAULT NULL,
  `VendComision` int(11) DEFAULT NULL,
  `VendClave` varchar(255) DEFAULT NULL,
  `fechaAlta` varchar(255) DEFAULT NULL,
  `Activo` tinyint(4) DEFAULT NULL,
  `usuario` tinyint(4) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(255) DEFAULT NULL,
  `documento` varchar(255) DEFAULT NULL,
  `contacto` varchar(255) DEFAULT NULL,
  `observaciones` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `vendedores`
--

INSERT INTO `vendedores` (`VendCodigo`, `VendNombre`, `VendComision`, `VendClave`, `fechaAlta`, `Activo`, `usuario`, `nombre`, `apellido`, `documento`, `contacto`, `observaciones`) VALUES
(2, 'Danny', 1, '504504', '', 1, 1, 'DANIEL', 'RENNIS', '25504258', '', ''),
(3, 'Ristoff Alberto', 1, '123', '', 1, 1, 'Ristoff', 'Alberto Antonio Elias', '26163237', '', ''),
(5, 'Ivaniszyn Emiliano', 1, 'girasoles1', '', 1, 1, 'EMILIANO RENE', 'Ivaniszyn ', '20591483', '', ''),
(9, 'Almiron Lucas', 1, '40489227', '', 1, 3, 'Lucas', 'Almiron', '40489227', '3704890479', 'Vendedor'),
(17, 'Riviere Alejandra', 0, '504258', '2011-08-12 00:00:00', 1, 1, 'Alejandra', 'Riviere', '32181882', '', ''),
(46, 'Troccoli Raul', 1, 'ventas', '2014-11-18 00:00:00', 1, 5, 'raul armando vicente', 'Troccoli', '36833954', '', ''),
(51, 'Cardozo Luis', 1, 'ventas16', '2016-03-15 00:00:00', 1, 5, 'Luis alberto', 'Cardozo', '29517686', '', ''),
(53, 'Valle Hugo', 1, 'maraka', '2016-04-16 00:00:00', 1, 3, 'Hugo', 'Valle', '33989445', '', ''),
(54, 'Pallares Mechi', 1, '34614410', '2017-05-08 00:00:00', 1, 5, 'Mercedes', 'Pallares', '34614410', '3624-241445', ''),
(60, 'Llanderal Diego', 1, '30898525', '2017-10-04 00:00:00', 1, 3, 'diego miguel', 'Llanderal', '30898525', '', ''),
(61, 'Gremes Nadina', 1, '39752763', '2017-10-05 00:00:00', 1, 2, 'Nadina', 'Gremes', '39752763', '', ''),
(67, 'Dragneff Nicolas', 1, '34702384', '2018-02-06 00:00:00', 1, 5, 'Nicolas', 'Dragneff Mortcheff', '34702384', '3735-400199', ''),
(68, 'Sabadini Victor', 0, '33072778', '2018-05-02 00:00:00', 1, 2, 'Victor', 'Sabadini Diaz', '33072778', '3624-315856', ''),
(70, 'Rodriguez Gonzalo', 1, '33588119', '2018-06-01 00:00:00', 1, 3, 'Gonzalo', 'Rodriguez', '33588119', '3704822409', ''),
(72, 'Franco Rosana', 1, '39179461', '2018-07-03 00:00:00', 1, 3, 'Rosana', 'Franco', '39179461', '3624-087674', 'Cajera'),
(73, 'DelCampo Pablo', 1, '34012623', '2018-12-03 00:00:00', 1, 6, 'Pablo', 'Del Campo', '34012623', '', ''),
(74, 'Pasantes', 1, '33549340', '2019-01-30 00:00:00', 1, 1, '', '', '33549340', '', ''),
(75, 'Justiniano Romina', 1, '32754452', '2019-03-13 00:00:00', 1, 3, 'Romina', 'Justiniano', '32754452', '1', 'Nuevo'),
(76, 'Garcia Ariel', 1, '38542681', '2019-03-13 00:00:00', 1, 2, 'Ariel', 'Garcia', '38542681', '3705 040844', ''),
(77, 'Mercadolibre', 1, '504504', '2019-03-24 00:00:00', 1, 2, 'Mercado', 'Libre', '504504', '3', ''),
(78, 'Aguirre Cristian', 1, '39306375', '2019-06-13 00:00:00', 1, 3, 'Cristian', 'Aguirre', '39306375', '3625192916', ''),
(79, 'Ortega Rocio', 1, '37771634', '2019-07-18 00:00:00', 1, 3, 'Rocio', 'Ortega', '37771634', '3624569958', 'Cajera A'),
(80, 'Di Nubila Sofia', 1, '37112667', '2019-07-18 00:00:00', 1, 3, 'Sofia', 'Di Nubila', '37112667', '3624833098', 'Cajera A');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE `ventas` (
  `FacCodigo` int(11) NOT NULL,
  `FacId` int(11) NOT NULL,
  `FacTipo` varchar(30) NOT NULL,
  `FacFecha` datetime NOT NULL,
  `FacHora` time NOT NULL,
  `CliCodigo` int(11) DEFAULT NULL,
  `FacSubtotal` double NOT NULL,
  `FacDescuento` double DEFAULT NULL,
  `FacImporteiva1` double DEFAULT NULL,
  `FacImporteiva2` double DEFAULT NULL,
  `FacIva1` double DEFAULT NULL,
  `FacIva2` double DEFAULT NULL,
  `FacTotal` double NOT NULL,
  `CodRemito` varchar(50) DEFAULT NULL,
  `CodPago` varchar(100) DEFAULT NULL,
  `CodVendedor` varchar(50) DEFAULT NULL,
  `VendCodigo` smallint(6) DEFAULT NULL,
  `FacEstado` varchar(255) DEFAULT NULL,
  `FacAnulada` tinyint(4) DEFAULT NULL,
  `FechaImpresionF` date DEFAULT NULL,
  `FechaVencimiento` date DEFAULT NULL,
  `Seriales` varchar(100) DEFAULT NULL,
  `saldo` float DEFAULT NULL,
  `esmayorista` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventasanuladas`
--

CREATE TABLE `ventasanuladas` (
  `nrofiscal` int(11) DEFAULT NULL,
  `compinterno` int(11) DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `tipocomp` varchar(50) DEFAULT NULL,
  `puntodeventa` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventasnegativo`
--

CREATE TABLE `ventasnegativo` (
  `idtrans` int(11) NOT NULL,
  `procodigo` varchar(50) DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `vendcodigo` int(11) DEFAULT NULL,
  `autcodigo` int(11) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `comprobante` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventaspagos`
--

CREATE TABLE `ventaspagos` (
  `idmovimiento` int(11) NOT NULL,
  `idventa` int(11) NOT NULL,
  `idpago` int(11) NOT NULL,
  `importe` float DEFAULT NULL,
  `fecha` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`IDCategoria`),
  ADD KEY `IDCategoria` (`IDCategoria`);

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`CliCodigo`);

--
-- Indices de la tabla `detallectactegeneral`
--
ALTER TABLE `detallectactegeneral`
  ADD KEY `iddetalle` (`iddetalle`);

--
-- Indices de la tabla `fabricantes`
--
ALTER TABLE `fabricantes`
  ADD PRIMARY KEY (`FabCodigo`),
  ADD KEY `FabCodigo` (`FabCodigo`);

--
-- Indices de la tabla `listaprecio`
--
ALTER TABLE `listaprecio`
  ADD KEY `IdLista` (`IdLista`);

--
-- Indices de la tabla `movimientos`
--
ALTER TABLE `movimientos`
  ADD KEY `IdMovimiento` (`IdMovimiento`);

--
-- Indices de la tabla `ordenservicio`
--
ALTER TABLE `ordenservicio`
  ADD KEY `idorden` (`idorden`);

--
-- Indices de la tabla `tipospago`
--
ALTER TABLE `tipospago`
  ADD KEY `idTipoPago` (`idTipoPago`);

--
-- Indices de la tabla `vendedores`
--
ALTER TABLE `vendedores`
  ADD PRIMARY KEY (`VendCodigo`);

--
-- Indices de la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD PRIMARY KEY (`FacCodigo`);

--
-- Indices de la tabla `ventasnegativo`
--
ALTER TABLE `ventasnegativo`
  ADD KEY `idtrans` (`idtrans`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `categorias`
--
ALTER TABLE `categorias`
  MODIFY `IDCategoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=161;

--
-- AUTO_INCREMENT de la tabla `detallectactegeneral`
--
ALTER TABLE `detallectactegeneral`
  MODIFY `iddetalle` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `fabricantes`
--
ALTER TABLE `fabricantes`
  MODIFY `FabCodigo` smallint(6) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `listaprecio`
--
ALTER TABLE `listaprecio`
  MODIFY `IdLista` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `movimientos`
--
ALTER TABLE `movimientos`
  MODIFY `IdMovimiento` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ordenservicio`
--
ALTER TABLE `ordenservicio`
  MODIFY `idorden` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tipospago`
--
ALTER TABLE `tipospago`
  MODIFY `idTipoPago` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ventasnegativo`
--
ALTER TABLE `ventasnegativo`
  MODIFY `idtrans` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
