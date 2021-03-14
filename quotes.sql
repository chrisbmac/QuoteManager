SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

CREATE TABLE IF NOT EXISTS `tblquotes` (
    `quoteID` int(10) NOT NULL,
    `author` varchar(100) DEFAULT NULL,
    `quote` LONGTEXT,
    `permalink` varchar(100) DEFAULT NULL,
    `image` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE `tblquotes`
  MODIFY `quoteID` int(10) NOT NULL AUTO_INCREMENT;

INSERT INTO `tblquotes` (`quoteID`, `author`, `quote`, `permalink`, `image`) VALUES
(1, 'chris mac', 'today is today', 'www.google.com', 'airliner.PNG'),
(2, 'don med', 'tonight is a night', 'www.yahoo.com', 'airliner.PNG'),
(3,'mac rom','last night last week','www.youtube.com','airliner.PNG'),
(4,'smeagul','whats a hobbit precious','www.drphil.com','airliner.PNG'),
(5,'tree beard','i hate orcs','www.drsues.com','airliner.PNG'),
(6,'soromon','join us','www.drsues.com','airliner.PNG'),
(7,'sauron','I see you','www.drsues.com','airliner.PNG'),
(8,'aragorn','sit down legolas','www.drsues.com','airliner.PNG'),
(9,'legolas','22','www.amazon.ca','airliner.PNG'),
(10,'frodo','the ring must be destroyed sam','www.drsues.com','airliner.PNG'),
(11,'gandalf','one ring ','www.drsues.com','airliner.PNG'),
(12,'gimli','lotr','www.drsues.com','airliner.PNG'),
(13,'abc','lorem empsum all day', 'www.drsues.com','airliner.PNG'),
(14,'kyle reed','There are two major products that come out of Berkeley: LSD and UNIX.  We don’t believe this to be a coincidence','www.drsues.com','airliner.PNG'),
(15,'cole cole','blue green red rgb','www.colorses.com','airliner.PNG'),
(16,'benard','steak on sundays','www.steaues.com','airliner.PNG'),
(17,'Larry DeLuca','I’ve noticed lately that the paranoid fear of computers becoming intelligent and taking over the world has almost entirely disappeared from the common culture.  Near as I can tell, this coincides with the release of MS-DOS','www.drsues.com','airliner.PNG'),
(18,'Alan Kay','Most software today is very much like an Egyptian pyramid with millions of bricks piled on top of each other, with no structural integrity, but just done by brute force and thousands of slaves','http://quotes.stormconsultancy.co.uk/quotes/38','airliner.PNG');