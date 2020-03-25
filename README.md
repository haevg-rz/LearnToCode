# LearnToCode

![CI](https://github.com/haevg-rz/LearnToCode/workflows/CI/badge.svg)

Learning assets from our students and trainees.

## Examples

1. [Statics](Examples/Static/README.md)
2. [MVVM Beginner Level (no 3rd party libs)](Examples/MVVM%20Beginner%20Level/README.md)
3. [CollectionViewSource - How to filter data in a DataGrid](Examples/CollectionViewSource/README.md)
4. [TemplateSwitching - How to switch between two DataTemplates](Examples/TemplateSwitching/README.md)
5. [Linq](Examples/Linq/README.md)
6. [Push - Send notifications to Android and iPhone](Examples/UsingApiForPush/README.md)
7. [Using JSON configuration files](Examples/JsonConfigFiles/README.md)
8. [Mocking](Examples/Mock/README.md)
9. [Binary Search](Examples/BinarySearch/README.md)
10. [MVVM Bindings](Examples/MVVM%20Binding/README.md) :hammer: (Under construction)
11. [Encryption](Examples/EncryptionDemo/README.md) :hammer: (Under construction)

## Contribute

How these pattern to create a visual studio solution.

### Create a WPF Application

Go in the new folder for your example.

```cmd
dotnet new sln
dotnet new wpf
dotnet sln add .
dotnet build
```

### Create a class lib

Go in the new folder for your example.

```cmd
dotnet new sln
dotnet new classlib
dotnet sln add .
dotnet build
```
