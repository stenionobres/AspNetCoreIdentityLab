SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=bin\Debug\netcoreapp3.1\AspNetCoreIdentityLab.Application.exe
cd /d "C:\Program Files\IIS Express\"
iisexpress.exe /config:"D:\Lab\AspNetCoreIdentityLab\.vs\AspNetCoreIdentityLab\config\applicationhost.config" /site:"AspNetCoreIdentityLab.Application" /apppool:"AspNetCoreIdentityLab.Application AppPool"