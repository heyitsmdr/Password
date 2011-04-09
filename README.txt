Password is a command-line tool for quickly saving and retrieving passwords from a local database.
It was written in C#, requires the .Net Framework, and is for Windows only.

Installation
============
You can run pw.exe from anywhere, however to run it from the command line (and also via start > run), move pw.exe to a directory that is in your PATH variable, such as C:\Windows.

Using Password
==============
Usage pw [-amxdhol] [alias] [password]
-h = shows help
-o = opens database in default text viewer
-a = add an entry to the database
-m = modify an existing entry
-d = delete an entry from the database
-x = place password for alias on the clipboard
-l = list all passwords saved

Examples
========
To add a password for 'yahoo'
  pw -a yahoo bob456
To modify yahoo's password
  pw -m yahoo newpass777
To delete yahoo's entry in the database
  pw -d yahoo
To place the password for yahoo on the clipboard (for pasting)
  pw -x yahoo