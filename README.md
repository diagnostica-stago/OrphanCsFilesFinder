# Orphan Cs Files Finder
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
- TopDirectory            The top directory to analyze.

Options:
  - `-g|--include-generated` : Include .generated.cs files in the orphaned files, default is off
  - `-?|-h|--help` : Show help information
