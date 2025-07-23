-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-07-2025 a las 00:32:31
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sistema_transporte`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `asignaciones`
--

CREATE TABLE `asignaciones` (
  `id` int(11) NOT NULL,
  `id_conductor` int(11) DEFAULT NULL,
  `id_autobus` int(11) DEFAULT NULL,
  `id_ruta` int(11) DEFAULT NULL,
  `fecha_asignacion` date NOT NULL,
  `hora_inicio` time NOT NULL,
  `hora_fin` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `asignaciones`
--

INSERT INTO `asignaciones` (`id`, `id_conductor`, `id_autobus`, `id_ruta`, `fecha_asignacion`, `hora_inicio`, `hora_fin`) VALUES
(1, 1, 1, 1, '2025-07-23', '08:00:00', '10:00:00'),
(2, 1, 2, 2, '2025-07-23', '16:45:00', '18:00:00'),
(3, 3, 3, 3, '2025-07-23', '14:00:00', '16:00:00'),
(4, 1, 1, 1, '2025-07-24', '08:00:00', '10:00:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `autobuses`
--

CREATE TABLE `autobuses` (
  `id` int(11) NOT NULL,
  `placa` varchar(20) NOT NULL,
  `modelo` varchar(50) NOT NULL,
  `estado` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `autobuses`
--

INSERT INTO `autobuses` (`id`, `placa`, `modelo`, `estado`) VALUES
(1, 'PBC-0012', 'Mercedes Sprinter', 1),
(2, 'ABC-4567', 'Hyundai County', 1),
(3, 'XYZ-7890', 'Chevrolet NPR', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `conductores`
--

CREATE TABLE `conductores` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `licencia` varchar(1) NOT NULL,
  `estado` tinyint(1) DEFAULT 1,
  `cedula` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `conductores`
--

INSERT INTO `conductores` (`id`, `nombre`, `licencia`, `estado`, `cedula`) VALUES
(1, 'Carlos Pérezz', 'C', 0, '0102030405'),
(2, 'Marta Gómeza', 'B', 1, '1102233445'),
(3, 'Luis Torres', 'E', 1, '0911223344'),
(5, 'David', 'E', 1, '1725564685');

--
-- Disparadores `conductores`
--
DELIMITER $$
CREATE TRIGGER `validar_licencia_before_insert` BEFORE INSERT ON `conductores` FOR EACH ROW BEGIN
    IF NEW.licencia NOT IN ('A', 'B', 'C', 'D', 'E') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Licencia inválida. Debe ser A, B, C, D o E.';
    END IF;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `validar_licencia_before_update` BEFORE UPDATE ON `conductores` FOR EACH ROW BEGIN
    IF NEW.licencia NOT IN ('A', 'B', 'C', 'D', 'E') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Licencia inválida. Debe ser A, B, C, D o E.';
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rutas`
--

CREATE TABLE `rutas` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `kilometraje` decimal(6,2) NOT NULL,
  `estado` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `rutas`
--

INSERT INTO `rutas` (`id`, `nombre`, `kilometraje`, `estado`) VALUES
(1, 'Ruta Centro-Norte', 12.50, 1),
(2, 'Ruta Sur-Este', 18.75, 1),
(3, 'Ruta Periférica', 25.30, 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `asignaciones`
--
ALTER TABLE `asignaciones`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_conductor` (`id_conductor`),
  ADD KEY `id_autobus` (`id_autobus`),
  ADD KEY `id_ruta` (`id_ruta`);

--
-- Indices de la tabla `autobuses`
--
ALTER TABLE `autobuses`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `placa` (`placa`);

--
-- Indices de la tabla `conductores`
--
ALTER TABLE `conductores`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `cedula` (`cedula`);

--
-- Indices de la tabla `rutas`
--
ALTER TABLE `rutas`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nombre` (`nombre`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `asignaciones`
--
ALTER TABLE `asignaciones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `autobuses`
--
ALTER TABLE `autobuses`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `conductores`
--
ALTER TABLE `conductores`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `rutas`
--
ALTER TABLE `rutas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `asignaciones`
--
ALTER TABLE `asignaciones`
  ADD CONSTRAINT `asignaciones_ibfk_1` FOREIGN KEY (`id_conductor`) REFERENCES `conductores` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `asignaciones_ibfk_2` FOREIGN KEY (`id_autobus`) REFERENCES `autobuses` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `asignaciones_ibfk_3` FOREIGN KEY (`id_ruta`) REFERENCES `rutas` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
