CREATE DATABASE JD_TP_RC_Recipes;

USE JD_TP_RC_Recipes;

CREATE TABLE Category
(id int IDENTITY (1,1) PRIMARY KEY,
name nvarchar (255) NOT NULL
);

CREATE TABLE Difficulty
(id int IDENTITY (1,1) PRIMARY KEY,
name nvarchar (255) NOT NULL
);

CREATE TABLE Ingredients
(id int IDENTITY (1,1) PRIMARY KEY,
name nvarchar (255),
);

CREATE TABLE Users
(id int IDENTITY (1,1) PRIMARY KEY,
first_name nvarchar (255),
last_name nvarchar (255),
username nvarchar (255) NOT NULL,
password nvarchar (255) NOT NULL,
is_admin bit DEFAULT 0
);

CREATE TABLE Recipes
(id int IDENTITY (1,1) PRIMARY KEY,
title nvarchar (255) NOT NULL,
preparation_method nvarchar (1000),
preparation_time int,
image_url nvarchar (500) NULL,
category_id int,
difficulty_id int,
user_id int,
is_approved bit DEFAULT 0,
FOREIGN KEY (category_id) REFERENCES Category(id),
FOREIGN KEY (difficulty_id) REFERENCES Difficulty(id),
FOREIGN KEY (user_id) REFERENCES Users(id),
);

CREATE TABLE Comments
(id int IDENTITY (1,1) PRIMARY KEY,
comment_text nvarchar (255),
user_id int,
recipe_id int,
reply_id int NULL,
FOREIGN KEY (user_id) REFERENCES Users(id),
FOREIGN KEY (recipe_id) REFERENCES Recipes(id),
FOREIGN KEY (reply_id) REFERENCES Comments(id)
);

CREATE TABLE IngredientLine
(id int IDENTITY (1,1) PRIMARY KEY,
quantity decimal,
measure nvarchar (100),
ingredient_id int,
recipe_id int,
FOREIGN KEY (ingredient_id) REFERENCES Ingredients(id),
FOREIGN KEY (recipe_id) REFERENCES Recipes(id)
);

CREATE TABLE Rating
(id int IDENTITY (1,1) PRIMARY KEY,
score int,
user_id int,
recipe_id int,
FOREIGN KEY (user_id) REFERENCES Users(id),
FOREIGN KEY (recipe_id) REFERENCES Recipes(id)
);

CREATE TABLE Favorites 
(id int IDENTITY (1,1) PRIMARY KEY,
user_id int NOT NULL,
recipe_id int NOT NULL,
FOREIGN KEY (user_id) REFERENCES Users(id),
FOREIGN KEY (recipe_id) REFERENCES Recipes(id),
UNIQUE(user_id, recipe_id)
);

INSERT INTO Users (first_name, last_name, username, password, is_admin)
Values ('Tiago', 'Pinto', 'admin', 'admin', 1);

INSERT INTO Category (name) VALUES ('Italian');
INSERT INTO Category (name) VALUES ('Dessert');
INSERT INTO Category (name) VALUES ('Healthy');
INSERT INTO Category (name) VALUES ('Fast Food');

INSERT INTO Difficulty(name) VALUES ('Easy');
INSERT INTO Difficulty (name) VALUES ('Medium');
INSERT INTO Difficulty (name) VALUES ('Hard');

INSERT INTO Recipes 
(title, preparation_method, preparation_time, category_id, difficulty_id, user_id, is_approved)
VALUES 
('Spaghetti Carbonara',
 'Cook pasta. Fry pancetta. Mix eggs and cheese. Combine all.',
 25,
 1,
 2,
 1,
 1);

 INSERT INTO Recipes 
(title, preparation_method, preparation_time, category_id, difficulty_id, user_id, is_approved)
VALUES 
('Chocolate Cake',
 'Mix ingredients. Bake at 180°C for 40 minutes.',
 60,
 2,
 2,
 1,
 1);

 INSERT INTO Recipes 
(title, preparation_method, preparation_time, category_id, difficulty_id, user_id, is_approved)
VALUES 
('Greek Salad',
 'Chop vegetables. Add feta. Drizzle olive oil.',
 10,
 3,
 1,
 1,
 1);

 INSERT INTO Recipes 
(title, preparation_method, preparation_time, category_id, difficulty_id, user_id, is_approved)
VALUES 
('Cheeseburger',
 'Grill beef patty. Add cheese. Assemble burger.',
 15,
 4,
 1,
 1,
 1);

 INSERT INTO Recipes 
(title, preparation_method, preparation_time, category_id, difficulty_id, user_id, is_approved)
VALUES 
('Beef Wellington',
 'Wrap beef in pastry. Bake carefully.',
 120,
 1,
 3,
 1,
 1);

 INSERT INTO Ingredients (name) VALUES ('Pasta');
INSERT INTO Ingredients (name) VALUES ('Eggs');
INSERT INTO Ingredients (name) VALUES ('Flour');
INSERT INTO Ingredients (name) VALUES ('Tomatoes');
INSERT INTO Ingredients (name) VALUES ('Beef');

SELECT * FROM Users;
SELECT * FROM Recipes;
SELECT * FROM Ingredients;
SELECT * FROM Favorites;

DROP DATABASE JD_TP_RC_Recipes;



ALTER TABLE Recipes
ADD image_url NVARCHAR(500);

UPDATE Recipes
SET image_url = 'https://images.unsplash.com/photo-1589308078059-be1415eab4c3'
WHERE title = 'Chocolate Cake';

UPDATE Recipes
SET image_url = 'https://images.unsplash.com/photo-1604908176997-431a0b4b0c55'
WHERE title = 'Spaghetti Carbonara';

UPDATE Users
SET password = '$2a$11$RJA2GnIm7r/9jWeDn.N/Eu3Upe2XdqwO.ksur3Urt4qs7tghEGHtW'
WHERE username = 'superchef';

ALTER TABLE Category ADD is_approved bit NOT NULL DEFAULT 1;
ALTER TABLE Difficulty ADD is_approved bit NOT NULL DEFAULT 1;

ALTER TABLE Users
ADD avatar_url nvarchar(500) NULL;
