using System.Security.Cryptography;
using System.Xml.Linq;

// Modern C# 8.0+ top-level statements with improved patterns
var currentDirectory = Directory.GetCurrentDirectory();
var gitignorePath = Path.Combine(currentDirectory, ".gitignore");
var excludedFiles = new List<string>();

// Parse .gitignore file
if (File.Exists(gitignorePath))
{
    excludedFiles = File.ReadAllLines(gitignorePath)
        .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
        .Select(line => line.Trim())
        .ToList();
}

// Get all files in the current directory excluding those in .gitignore
var files = Directory.GetFiles(currentDirectory)
    .Where(file => !excludedFiles.Any(excluded =>
        Path.GetFileName(file).Equals(excluded, StringComparison.OrdinalIgnoreCase)))
    .ToList();

// Generate MD5 checksums with modern using declaration
var fileChecksums = new List<(string FileName, string MD5Checksum)>();
using var md5 = MD5.Create();

foreach (var file in files)
{
    using var stream = File.OpenRead(file);
    var hash = md5.ComputeHash(stream);
    var checksum = Convert.ToHexString(hash).ToLowerInvariant();
    fileChecksums.Add((Path.GetFileName(file), checksum));
}

// Create XML document with modern LINQ
var xmlDocument = new XDocument(
    new XElement("MD5Checksums",
        fileChecksums.Select(fc =>
            new XElement("File",
                new XAttribute("Name", fc.FileName),
                new XAttribute("Checksum", fc.MD5Checksum)
            )
        )
    )
);

// Save to md5.xml
var xmlPath = Path.Combine(currentDirectory, "md5.xml");
xmlDocument.Save(xmlPath);

Console.WriteLine($"MD5 checksums have been saved to {xmlPath}");
