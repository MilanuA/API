CREATE TABLE `Games` (
                         `id` int PRIMARY KEY AUTO_INCREMENT,
                         `name` varchar(255) UNIQUE NOT NULL,
                         `release_date` date NOT NULL,
                         `price` int NOT NULL,
                         `engine_id` int NOT NULL,
                         `publisher_id` int NOT NULL
);

CREATE TABLE `Developers` (
                              `id` int PRIMARY KEY AUTO_INCREMENT,
                              `name` varchar(255) NOT NULL,
                              `country` varchar(255) NOT NULL
);

CREATE TABLE `Publishers` (
                              `id` int PRIMARY KEY AUTO_INCREMENT,
                              `name` varchar(255) UNIQUE,
                              `country` varchar(255) NOT NULL
);

CREATE TABLE `Genres` (
                          `id` int PRIMARY KEY AUTO_INCREMENT,
                          `name` varchar(255) UNIQUE NOT NULL
);

CREATE TABLE `Platforms` (
                             `id` int PRIMARY KEY AUTO_INCREMENT,
                             `name` varchar(255) UNIQUE NOT NULL
);

CREATE TABLE `Game_Genres` (
                               `game_id` int,
                               `genre_id` int,
                               PRIMARY KEY (`game_id`, `genre_id`)
);

CREATE TABLE `Game_Platforms` (
                                  `game_id` int,
                                  `platform_id` int,
                                  PRIMARY KEY (`game_id`, `platform_id`)
);

CREATE TABLE `Game_Developers` (
                                   `game_id` int,
                                   `developer_id` int,
                                   PRIMARY KEY (`game_id`, `developer_id`)
);

CREATE TABLE `Game_Reviews` (
                                `game_id` int,
                                `review_id` int,
                                PRIMARY KEY (`game_id`, `review_id`)
);

CREATE TABLE `Reviews` (
                           `id` int PRIMARY KEY AUTO_INCREMENT,
                           `rating` decimal(2,1) COMMENT 'Rating scale from 0 to 10',
                           `review_text` text NOT NULL,
                           `created_at` timestamp NOT NULL,
                           `reviewever_id` int NOT NULL
);

CREATE TABLE `Engines` (
                           `id` int PRIMARY KEY AUTO_INCREMENT,
                           `name` varchar(255) NOT NULL
);

CREATE TABLE `Reviewever` (
                              `id` int PRIMARY KEY AUTO_INCREMENT,
                              `name` varchar(255) NOT NULL
);

ALTER TABLE `Game_Genres` ADD FOREIGN KEY (`game_id`) REFERENCES `Games` (`id`);

ALTER TABLE `Game_Genres` ADD FOREIGN KEY (`genre_id`) REFERENCES `Genres` (`id`);

ALTER TABLE `Game_Platforms` ADD FOREIGN KEY (`game_id`) REFERENCES `Games` (`id`);

ALTER TABLE `Game_Platforms` ADD FOREIGN KEY (`platform_id`) REFERENCES `Platforms` (`id`);

ALTER TABLE `Game_Developers` ADD FOREIGN KEY (`game_id`) REFERENCES `Games` (`id`);

ALTER TABLE `Game_Developers` ADD FOREIGN KEY (`developer_id`) REFERENCES `Developers` (`id`);

ALTER TABLE `Games` ADD FOREIGN KEY (`engine_id`) REFERENCES `Engines` (`id`);

ALTER TABLE `Games` ADD FOREIGN KEY (`publisher_id`) REFERENCES `Publishers` (`id`);

ALTER TABLE `Reviews` ADD FOREIGN KEY (`reviewever_id`) REFERENCES `Reviewever` (`id`);

ALTER TABLE `Game_Reviews` ADD FOREIGN KEY (`game_id`) REFERENCES `Games` (`id`);

ALTER TABLE `Game_Reviews` ADD FOREIGN KEY (`review_id`) REFERENCES `Reviews` (`id`);