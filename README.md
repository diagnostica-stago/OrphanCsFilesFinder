# OrphanCsFileFinder
Finds .cs files without a csproj in a directory over them

### Installation

```
dotnet tool install -g orphancsfilesfinder
```

### Usage

```
orphancsfilesfinder <TopDirectory>
```

Arguments:
- TopDirectory            The directory to analyze.

Options:
  - `-g|--include-generated` : Include .generated.cs files in the orphaned files
  - `-?|-h|--help` : Show help information
