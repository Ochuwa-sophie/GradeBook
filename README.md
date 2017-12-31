# GradeBook
Asp.Net Core MVC/Razor Page simple Grade book for teachers.

This is a fully functional piece based on https://unison.school web portal for music teachers.

The bigger project was created in MVC with controllers, but on this port, I tried out Razor pages. I found the page to not be able to handle a lot of methods, so I also added in a GradebookController class, which is called directly or via the typescript file.

The Razor Page layout is quite detailed and "spaghetti-code", and I'll try to clean it up/move more into the c# files in the future.

Features
- Create Teacher accounts
- Create/Edit/Delete School Classes
- Create/Edit Assignments (Delete not complete)
- Create/Edit/Delete Students and student grades
- Calculates grades based on weighting

Upcoming
- Customized rubrics with multiple assessments in one assignment, usable across multiple classes.
