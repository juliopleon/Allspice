CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NUll AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(255) NOT NUll,
        instructions TEXT,
        img VARCHAR(255) NOT NULL DEFAULT 'https://img.freepik.com/free-photo/chicken-wings-barbecue-sweetly-sour-sauce-picnic-summer-menu-tasty-food-top-view-flat-lay_2829-6471.jpg?w=2000',
        category VARCHAR(255) NOT NULL DEFAULT 'misc',
        archived BOOLEAN NOT NULL DEFAULT false,
        creatorId VARCHAR(255) NOT NULL,
        Foreign Key (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(200) NOT NULL,
        creatorId VARCHAR(250) NOT NULL,
        quantity VARCHAR(250) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    favorites(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        recipeId INT NOT NULL,
        accountId VARCHAR(250) NOT NULL,
        Foreign Key (recipeId) REFERENCES recipe (id) ON DELETE CASCADE,
        Foreign Key (accountId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8;

INSERT INTO
    `favorites` (`recipeId`, `accountId`)
VALUES (1, '63c86f7e1eaff3f6f4406ad9');

INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        `creatorId`
    )
VALUES (
        'Chicken Tenders',
        'throw in tenders with hot sauce and boom',
        'https://www.healthyseasonalrecipes.com/wp-content/uploads/2016/01/healthy-baked-chicken-tenders-sq-22-5-640x480.jpg',
        'Healthy',
        '63c86f7e1eaff3f6f4406ad9'
    );

INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        `creatorId`
    )
VALUES (
        'Burgers',
        'cook it well and juicy',
        'https://www.healthyseasonalrecipes.com/wp-content/uploads/2016/01/healthy-baked-chicken-tenders-sq-22-5-640x480.jpg',
        'not healthy',
        '63c86f7e1eaff3f6f4406ad9',
        archived
    )
SELECT re.*, ac.*
FROM recipes re
    JOIN accounts ac ON ac.id = re.`creatorId`;

SELECT ac.*, fa.id
FROM favorites fa
    JOIN accounts ac ON fa.`accountId` = ac.id;