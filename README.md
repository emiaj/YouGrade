Where is CommonAssemblyInfo.cs?
--

CommonAssemblyInfo.cs is generated by the build. The build script requires Ruby with rake installed.

1. Run `InstallGems.bat` to get the ruby dependencies (only needs to be run once per computer)
1. open a command prompt to the root folder and type `rake` to execute rakefile.rb

If you do not have ruby:

1. You need to manually create a src\CommonAssemblyInfo.cs file 

  * type: `echo // > src\CommonAssemblyInfo.cs`

How to get the nuget packages?
--
You can open the command line and type "rake", by doing that it will fetch all the nuget packages and also will compile the app.
Or...
If you only want to fetch the nuget packages, you can open the command line and type "ripple restore".