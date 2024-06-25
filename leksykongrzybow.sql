-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Cze 17, 2024 at 01:59 PM
-- Wersja serwera: 10.4.32-MariaDB
-- Wersja PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `leksykongrzybow`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `grzyb`
--

CREATE TABLE `grzyb` (
  `ID_Grzyba` int(11) NOT NULL,
  `Nazwa_Naukowa` varchar(100) NOT NULL,
  `Nazwa_Potoczna` varchar(100) DEFAULT NULL,
  `Opis` text DEFAULT NULL,
  `ID_Rodzaju` int(11) DEFAULT NULL,
  `ID_Siedliska` int(11) DEFAULT NULL,
  `ID_Jadalnosci` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `grzyb`
--

INSERT INTO `grzyb` (`ID_Grzyba`, `Nazwa_Naukowa`, `Nazwa_Potoczna`, `Opis`, `ID_Rodzaju`, `ID_Siedliska`, `ID_Jadalnosci`) VALUES
(1, 'Agaricus bisporus', 'Pieczarka', 'Popularny grzyb jadalny.', 1, 1, 1),
(2, 'Hypocrea rufa', 'Trufla czerwona', 'Grzyb workowy, nie nadający się do spożycia.', 2, 2, 3);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `grzyb_siedlisko`
--

CREATE TABLE `grzyb_siedlisko` (
  `ID_Grzyba` int(11) NOT NULL,
  `ID_Siedliska` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `grzyb_siedlisko`
--

INSERT INTO `grzyb_siedlisko` (`ID_Grzyba`, `ID_Siedliska`) VALUES
(1, 1),
(1, 2),
(2, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `jadalnosc`
--

CREATE TABLE `jadalnosc` (
  `ID_Jadalnosci` int(11) NOT NULL,
  `Status_Jadalnosci` enum('Jadalny','Trujacy','Niejadalny') NOT NULL,
  `Opis` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `jadalnosc`
--

INSERT INTO `jadalnosc` (`ID_Jadalnosci`, `Status_Jadalnosci`, `Opis`) VALUES
(1, 'Jadalny', 'Grzyb nadający się do spożycia.'),
(2, 'Trujacy', 'Grzyb toksyczny dla ludzi.'),
(3, 'Niejadalny', 'Grzyb nie nadaje się do spożycia.');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `klasa`
--

CREATE TABLE `klasa` (
  `ID_Klasy` int(11) NOT NULL,
  `Nazwa_Klasy` varchar(100) NOT NULL,
  `Opis` text DEFAULT NULL,
  `ID_Typu` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `klasa`
--

INSERT INTO `klasa` (`ID_Klasy`, `Nazwa_Klasy`, `Opis`, `ID_Typu`) VALUES
(1, 'Agaricomycetes', 'Klasa grzybów kapeluszowych.', 1),
(2, 'Sordariomycetes', 'Klasa grzybów workowych.', 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `rodzaj`
--

CREATE TABLE `rodzaj` (
  `ID_Rodzaju` int(11) NOT NULL,
  `Nazwa_Rodzaju` varchar(100) NOT NULL,
  `Opis` text DEFAULT NULL,
  `ID_Rodziny` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rodzaj`
--

INSERT INTO `rodzaj` (`ID_Rodzaju`, `Nazwa_Rodzaju`, `Opis`, `ID_Rodziny`) VALUES
(1, 'Agaricus', 'Rodzaj obejmujący pieczarki.', 1),
(2, 'Hypocrea', 'Rodzaj grzybów workowych.', 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `rodzina`
--

CREATE TABLE `rodzina` (
  `ID_Rodziny` int(11) NOT NULL,
  `Nazwa_Rodziny` varchar(100) NOT NULL,
  `Opis` text DEFAULT NULL,
  `ID_Rzedu` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rodzina`
--

INSERT INTO `rodzina` (`ID_Rodziny`, `Nazwa_Rodziny`, `Opis`, `ID_Rzedu`) VALUES
(1, 'Agaricaceae', 'Rodzina grzybów kapeluszowych.', 1),
(2, 'Hypocreaceae', 'Rodzina grzybów workowych.', 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `rzad`
--

CREATE TABLE `rzad` (
  `ID_Rzedu` int(11) NOT NULL,
  `Nazwa_Rzedu` varchar(100) NOT NULL,
  `Opis` text DEFAULT NULL,
  `ID_Klasy` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rzad`
--

INSERT INTO `rzad` (`ID_Rzedu`, `Nazwa_Rzedu`, `Opis`, `ID_Klasy`) VALUES
(1, 'Agaricales', 'Rząd grzybów obejmujący pieczarki i inne.', 1),
(2, 'Hypocreales', 'Rząd grzybów workowych.', 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `siedlisko`
--

CREATE TABLE `siedlisko` (
  `ID_Siedliska` int(11) NOT NULL,
  `Nazwa_Siedliska` varchar(100) NOT NULL,
  `Opis` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `siedlisko`
--

INSERT INTO `siedlisko` (`ID_Siedliska`, `Nazwa_Siedliska`, `Opis`) VALUES
(1, 'Las', 'Typowe siedlisko dla wielu gatunków grzybów.'),
(2, 'Łąka', 'Siedlisko dla grzybów preferujących otwarte przestrzenie.');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `typ`
--

CREATE TABLE `typ` (
  `ID_Typu` int(11) NOT NULL,
  `Nazwa_Typu` varchar(100) NOT NULL,
  `Opis` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `typ`
--

INSERT INTO `typ` (`ID_Typu`, `Nazwa_Typu`, `Opis`) VALUES
(1, 'Basidiomycota', 'Typ grzybów obejmujący wiele znanych gatunków kapeluszowych.'),
(2, 'Ascomycota', 'Typ grzybów obejmujący gatunki tworzące worki.');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `grzyb`
--
ALTER TABLE `grzyb`
  ADD PRIMARY KEY (`ID_Grzyba`),
  ADD KEY `ID_Rodzaju` (`ID_Rodzaju`),
  ADD KEY `ID_Siedliska` (`ID_Siedliska`),
  ADD KEY `ID_Jadalnosci` (`ID_Jadalnosci`);

--
-- Indeksy dla tabeli `grzyb_siedlisko`
--
ALTER TABLE `grzyb_siedlisko`
  ADD PRIMARY KEY (`ID_Grzyba`,`ID_Siedliska`),
  ADD KEY `ID_Siedliska` (`ID_Siedliska`);

--
-- Indeksy dla tabeli `jadalnosc`
--
ALTER TABLE `jadalnosc`
  ADD PRIMARY KEY (`ID_Jadalnosci`);

--
-- Indeksy dla tabeli `klasa`
--
ALTER TABLE `klasa`
  ADD PRIMARY KEY (`ID_Klasy`),
  ADD KEY `ID_Typu` (`ID_Typu`);

--
-- Indeksy dla tabeli `rodzaj`
--
ALTER TABLE `rodzaj`
  ADD PRIMARY KEY (`ID_Rodzaju`),
  ADD KEY `ID_Rodziny` (`ID_Rodziny`);

--
-- Indeksy dla tabeli `rodzina`
--
ALTER TABLE `rodzina`
  ADD PRIMARY KEY (`ID_Rodziny`),
  ADD KEY `ID_Rzedu` (`ID_Rzedu`);

--
-- Indeksy dla tabeli `rzad`
--
ALTER TABLE `rzad`
  ADD PRIMARY KEY (`ID_Rzedu`),
  ADD KEY `ID_Klasy` (`ID_Klasy`);

--
-- Indeksy dla tabeli `siedlisko`
--
ALTER TABLE `siedlisko`
  ADD PRIMARY KEY (`ID_Siedliska`);

--
-- Indeksy dla tabeli `typ`
--
ALTER TABLE `typ`
  ADD PRIMARY KEY (`ID_Typu`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `grzyb`
--
ALTER TABLE `grzyb`
  MODIFY `ID_Grzyba` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `jadalnosc`
--
ALTER TABLE `jadalnosc`
  MODIFY `ID_Jadalnosci` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `klasa`
--
ALTER TABLE `klasa`
  MODIFY `ID_Klasy` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `rodzaj`
--
ALTER TABLE `rodzaj`
  MODIFY `ID_Rodzaju` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `rodzina`
--
ALTER TABLE `rodzina`
  MODIFY `ID_Rodziny` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `rzad`
--
ALTER TABLE `rzad`
  MODIFY `ID_Rzedu` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `siedlisko`
--
ALTER TABLE `siedlisko`
  MODIFY `ID_Siedliska` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `typ`
--
ALTER TABLE `typ`
  MODIFY `ID_Typu` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `grzyb`
--
ALTER TABLE `grzyb`
  ADD CONSTRAINT `grzyb_ibfk_1` FOREIGN KEY (`ID_Rodzaju`) REFERENCES `rodzaj` (`ID_Rodzaju`),
  ADD CONSTRAINT `grzyb_ibfk_2` FOREIGN KEY (`ID_Siedliska`) REFERENCES `siedlisko` (`ID_Siedliska`),
  ADD CONSTRAINT `grzyb_ibfk_3` FOREIGN KEY (`ID_Jadalnosci`) REFERENCES `jadalnosc` (`ID_Jadalnosci`);

--
-- Constraints for table `grzyb_siedlisko`
--
ALTER TABLE `grzyb_siedlisko`
  ADD CONSTRAINT `grzyb_siedlisko_ibfk_1` FOREIGN KEY (`ID_Grzyba`) REFERENCES `grzyb` (`ID_Grzyba`),
  ADD CONSTRAINT `grzyb_siedlisko_ibfk_2` FOREIGN KEY (`ID_Siedliska`) REFERENCES `siedlisko` (`ID_Siedliska`);

--
-- Constraints for table `klasa`
--
ALTER TABLE `klasa`
  ADD CONSTRAINT `klasa_ibfk_1` FOREIGN KEY (`ID_Typu`) REFERENCES `typ` (`ID_Typu`);

--
-- Constraints for table `rodzaj`
--
ALTER TABLE `rodzaj`
  ADD CONSTRAINT `rodzaj_ibfk_1` FOREIGN KEY (`ID_Rodziny`) REFERENCES `rodzina` (`ID_Rodziny`);

--
-- Constraints for table `rodzina`
--
ALTER TABLE `rodzina`
  ADD CONSTRAINT `rodzina_ibfk_1` FOREIGN KEY (`ID_Rzedu`) REFERENCES `rzad` (`ID_Rzedu`);

--
-- Constraints for table `rzad`
--
ALTER TABLE `rzad`
  ADD CONSTRAINT `rzad_ibfk_1` FOREIGN KEY (`ID_Klasy`) REFERENCES `klasa` (`ID_Klasy`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
