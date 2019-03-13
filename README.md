# SeleniumMethodsCS
Some static methods which are useful in writing Selenium programs.

## Usage:
Place Methods.cs in the folder where your other C# files are present. You can either user if after importing *SeleniumMethods* namespace and using *Methods* class directly, or by accessing *Methods* class by accesss operator (period):
```
using SeleniumMethods;
...
Methods.MoveToEl(Driver, By.Id("abcd"));
...
```
OR:
```
...
SeleniumMethods.Methods.MoveToEl(Driver, By.Id("abcd"));
...
```
