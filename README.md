# ElectronNet/Electron.Net with Blazor starter

## Prerequisites
* Visual Studio 2017 15.9+

## Getting started
### Download dependencies
* Download and install .Net Core 2.1+ sdk: https://dotnet.microsoft.com/download/dotnet-core/2.1
* Download and install Blazor language services: https://marketplace.visualstudio.com/items?itemName=aspnet.blazor

### Test solution in VS
Open the solution in Visual Studio 2017 and test the application in a IIS express debug session with a browser

### Test Electron.Net
Open a command window like Powershell and test if .Net Core is installed:
```console
PS> dotnet --info
[...]
Host (useful for support):
  Version: 2.2.0
  Commit:  1249f08fed
[...]
```

Open a command window in the ElectronBlazorSpa.Server folder and type:
```console
PS> dotnet restore
PS> dotnet electronize start
```

The application should start a window. There might be firewall dialogues popping up.

**Note**: stop the application by shutting down the window. Otherwise some background services might stay active.

## Compile
Compiling will produce the OS specific build of the application. The end result should be an electron app under your /bin/desktop folder. 

### Standard builds
Open Powershell in the ElectronBlazorSpa.Server folder and run one of these commands:
```console
    dotnet electronize build /target win
    dotnet electronize build /target osx
    dotnet electronize build /target linux
```

### Custom builds
If you want to build for something like windows 7 x86, use:
```console
    dotnet electronize build build /target custom win7-x86`;win32 /electron-arch ia32
```
*(Note the escape character for the semicolon)*

## Info
Further reading:
* ElectronNet/Electron.Net: https://github.com/ElectronNET/Electron.NET
* Blazor: https://blazor.net


