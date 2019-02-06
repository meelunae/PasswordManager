# PasswordManager
Weird password manager in C#, that stores passwords in a SQLite database.


Project realized in just an hour just for fun but I am happy with the outcome so I'm posting the source code on GitHub.

Stores the entire password database in an sqlite database, on a local .db file.

Supports removing, searching, and adding records for both random generated passwords (not using the standard pseudo-random method provided by the Framework in C#) and passwords you already have and want to add.

Cryptography support on the database is a no go for now considering that the lib I'm using doesnt allow to mess with encrypted databases unless you find a workaround (this project's been created in an hour so i didnt even bother lmao)
