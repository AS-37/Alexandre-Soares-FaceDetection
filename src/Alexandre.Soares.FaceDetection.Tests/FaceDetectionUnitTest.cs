using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Alexandre.Soares.FaceDetection;
using Xunit;

namespace Alexandre.Soares.FaceDetection.Tests;
public class FaceDetectionUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }

        var detectObjectInScenesResults = new
            FaceDetection().DetectInScenes(imageScenesData);
        //J'ai changé les Points avec x et y car j'avais une exception et x,y étaient les attributs qui faisaient sens sinon je ne pouvais pas build
        Assert.Equal("[{\"X\":117,\"Y\":158},{\"X\":87,\"Y\":272},{\"X\":263,\"Y\":294},{\"X\":276,\"Y\":143}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].X));
        Assert.Equal("[{\"X\":117,\"Y\":158},{\"X\":87,\"Y\":272},{\"X\":263,\"Y\":294},{\"X\":276,\"Y\":143}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Y)); 
    }
 private static string GetExecutingPath()
 {
 var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
 var executingPath = Path.GetDirectoryName(executingAssemblyPath);
 return executingPath;
 }
} 
