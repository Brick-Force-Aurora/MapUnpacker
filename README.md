# BrickForce MapUnpacker

BrickForce MapUnpacker is a tool designed to convert `regmap` and `geometry` files into 3D models using the OBJ format and human-readable JSON files. It also extracts relevant information about the map and exports its thumbnail. This application is built using **.NET 9** and **Avalonia UI**, making it cross-platform and efficient.

## Features
- **File Conversion**: Convert `regmap` and `geometry` files into:
  - 3D models in OBJ format
  - Human-readable JSON metadata
- **Map Information**: Extract and display detailed information about the map.
- **Thumbnail Export**: Automatically export map thumbnails.
- **Cross-Platform Support**: Runs on **Windows, Linux, and macOS** thanks to Avalonia UI and .NET 9.
- **User-Friendly UI**: Modern and intuitive interface using Avalonia UI.

## Installation
### Prerequisites
- Windows, Linux, or macOS
- .NET 9 Runtime (if using a non-self-contained version)

### Download
1. Download the latest release from the [Releases](https://github.com/Brick-Force-Aurora/MapUnpacker/releases) page.
2. Extract the files to your preferred location.
3. Run the `MapUnpacker.exe` (Windows) or `MapUnpacker` (Linux/macOS).

## Important Locations
- **`DATA/bricks.json`**: This file contains a JSON database of all available bricks, along with metadata about these bricks.
- **`MapUnpacker/bin/Resources`**: The OBJ files need to be located here next to the executable.

## How to Use
### Load Files
1. Open the application.
2. Load your `regmap` and `geometry` files.

### View Map Information
- The application will display relevant details such as **map ID, creator, brick count, and available game modes**.

### Export Files
- Use the export options to generate **OBJ models, JSON metadata, and map thumbnails**.
- The **Settings menu** allows you to customize default export options.

## Development
### Prerequisites
- **Visual Studio 2022+** or **JetBrains Rider**
- .NET 9 SDK
- Avalonia UI framework

### Steps
1. Clone the project:
   ```sh
   git clone https://github.com/Brick-Force-Aurora/MapUnpacker.git
   ```
2. Open the solution in Visual Studio or Rider.
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Build the project:
   ```sh
   dotnet build
   ```
5. Run the application:
   ```sh
   dotnet run
   ```

## Packaging for Distribution
To create a standalone executable for distribution:
```sh
   dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
```
For Linux/macOS, change `-r win-x64` to `linux-x64` or `osx-x64` accordingly.

## Contributing
Development is ongoing. If you would like to contribute, please fork the repository and submit a pull request.

## Related Projects
Check out the main **BrickForce Aurora Project**:
- [GitHub Repository](https://github.com/Brick-Force-Aurora)
- [Official Website](https://brickforce-aurora.com)

## License
This project is licensed under the **MIT License**. See the `LICENSE` file for details.