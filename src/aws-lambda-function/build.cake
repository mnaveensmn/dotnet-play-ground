#addin nuget:?package=Cake.FileHelpers&version=7.0.0
#addin nuget:?package=Cake.Compression&version=0.3.0
#tool dotnet:?package=Amazon.Lambda.Tools&version=5.12.4

using Cake.Compression;
using Cake.Common.IO;

var buildOutputDir = Directory("output");
var artifactDir = buildOutputDir + Directory("artifact");
var tempWebServiceDir = buildOutputDir + Directory("temp-webService");
var appBundleZip = File("bundle.zip");

Task("Hello")
    .Does(() =>
    {
        Console.WriteLine("Hello World");
    });
    
Task("Clean")
    .Does(() =>
{
    CleanDirectories("bin/Release");
    CleanDirectory(buildOutputDir);
});
    
 Task("Build")
     .Does(() =>
 {
     var buildSettings = new DotNetMSBuildSettings();
     DotNetBuild("../../dotnet-play-ground.sln", new DotNetBuildSettings
     {
         Configuration = "Release",
         MSBuildSettings = buildSettings,
         NoRestore = true
     });   
  });

 Task("CreateArtifacts")
     .Does(() =>
 {
      EnsureDirectoryExists(artifactDir);
      
      DotNetPublish("./", new DotNetPublishSettings {
         Configuration = "Release",
         OutputDirectory = tempWebServiceDir,
         PublishReadyToRun = false
      }); 
             
      CopyFile(File("aws-windows-deployment-manifest.json"), tempWebServiceDir + File("aws-windows-deployment-manifest.json"));
      
       Zip(tempWebServiceDir, artifactDir + appBundleZip);
  });
 

Task("Default")
    .IsDependentOn("Hello")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("CreateArtifacts");
    
 RunTarget("Default");