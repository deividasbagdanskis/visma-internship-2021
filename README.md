# visma-internship-2021

Library management console application created for Visma 2021 internship.

## How to use the application:

Main commands:  
add - Add a new book. All of the following arguments must be passed in order to add a new book.  
  ARGUMENTS  
--name="Name of the book"  
--author="The author of the book"  
--category="Category of the book"  
--language="Language the book is written in"  
--publicationDate="Publication date of the book" Use yyyy-MM-dd format  
--ISBN="ISBN of the book"  
delete - Delete a book. All of the following arguments must be passed in order to delete a new book.  
ARGUMENTS  
--ISBN="ISBN of the book" list-all - List all the books.  
filter - Filter books by a given attribute.  
OPTIONS  
--name="Name of the book" - Filter by the name of the book.  
--author="The author of the book" - Filter by the author of the book.  
--category="Category of the book" - Filter by the category of the book.  
--language="Language the book is written in" - Filter by the language the book was written.  
--ISBN="ISBN of the book" - Filter by the ISBN of the book.  
--availability="Available or taken" Filter by the availability of the book. Use only "Available" or "Taken"  
take - Take a book from the library. All of the following arguments must be passed in order to take a book.  
ARGUMENTS  
--ISBN="ISBN of the book which is being taken"  
--borrower="The name of a person who is borrowing the book"  
--period=45 - The time period, in days, for which the book will be borrowed  
return - Return borrowed book. All of the following arguments must be passed in order to return a book.  
ARGUMENTS  
--ISBN="ISBN of the book which is being returned"  
--borrower="The name of a person who is returning the book"  
help - Display manual.
