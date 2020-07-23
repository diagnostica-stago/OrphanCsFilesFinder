using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

namespace OrphanCsFileFinder
{
    public class Program
    {
        [Argument(0, Description = "The directory to analyze.")]
        [Required]
        public string TopDirectory { get; } = string.Empty;

        [Option("-g|--include-generated" , CommandOptionType.NoValue , Description = "Include .generated.cs files in the orphaned files")]
        public bool IncludeGeneratedFiles { get; }

        public static void Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
        }

        private ValidationResult OnValidate()
        {
            if (!Directory.Exists(TopDirectory))
            {
                return new ValidationResult("Directory does not exist");
            }

            return ValidationResult.Success;
        }

        public void OnExecute()
        {
            var directoriesToCheck = new Queue<DirectoryInfo>();
            directoriesToCheck.Enqueue(new DirectoryInfo(TopDirectory));
            var orphanFiles = new List<string>();
            while (directoriesToCheck.TryDequeue(out var currentDirectory))
            {
                if (currentDirectory.EnumerateFiles("*.csproj").Any())
                {
                    continue;
                }

                var csFiles = currentDirectory
                    .EnumerateFiles("*.cs")
                    .Where(f => IncludeGeneratedFiles || !f.Name.EndsWith(".Generated.cs", StringComparison.InvariantCultureIgnoreCase))
                    .Select(f => f.FullName);
                orphanFiles.AddRange(csFiles);

                foreach (var directoryInfo in currentDirectory.EnumerateDirectories())
                {
                    directoriesToCheck.Enqueue(directoryInfo);
                }
            }

            if (orphanFiles.Any())
            {
                Console.WriteLine("Orphaned cs files:");
            }
            foreach (var orphanFile in orphanFiles)
            {
                Console.WriteLine(orphanFile);
            }
            Console.WriteLine("Done");
        }
    }
}
