# BrickForce Dev Tools

BrickForce Dev Tools is a powerful toolset designed for working with BrickForce assets. It allows users to **unpack maps**, **cook and uncook template files**, and **convert map data** into various formats. Built using **.NET 9** and **Avalonia UI**, it offers a modern and cross-platform experience.

## Features
- **Map Unpacking**: Convert `regmap` and `geometry` files into:
  - 3D models in OBJ format
  - Human-readable JSON metadata
- **Template File Handling**: Cook and uncook **template files**, making it easier to work with game assets.
- **Map Information Extraction**: Retrieve detailed map metadata, including ID, creator, brick count, and game modes.
- **Thumbnail Export**: Automatically extract and export map thumbnails.
- **Cross-Platform Compatibility**: Works on **Windows, Linux, and macOS** thanks to Avalonia UI and .NET 9.
- **User-Friendly UI**: Intuitive and modern interface built with Avalonia UI.

## Installation
### Prerequisites
- Windows, Linux, or macOS
- .NET 9 Runtime (if using a non-self-contained version)

### Download
1. Download the latest release from the [Releases](https://github.com/Brick-Force-Aurora/DevTools/releases) page.
2. Extract the files to your preferred location.
3. Run the `BrickForceDevTools.exe`.

## Important Locations
- **`Assets/bricks.json`**: This file contains a JSON database of all available bricks and their metadata.
- **`BrickForceDevTools/bin/Resources`**: OBJ files need to be placed here alongside the executable.

## How to Use
### Load Files
1. Open the application.
2. Load your `regmap`, `geometry`, or **template** files.

### View Map and Template Information
- The application displays relevant details, including **map ID, creator, brick count, and available game modes**.
- For **template files**, users can see extracted data before cooking or uncooking them.

### Export and Convert Files
- Use the export options to generate **OBJ models, JSON metadata, and map thumbnails**.
- The **Settings menu** allows customization of default export and conversion options.

## Development
### Prerequisites
- **Visual Studio 2022+** or **JetBrains Rider**
- .NET 9 SDK
- Avalonia UI framework

### Steps
1. Clone the project:
   ```sh
   git clone https://github.com/Brick-Force-Aurora/DevTools.git
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

## Contributing
Development is ongoing. If you would like to contribute, please fork the repository and submit a pull request.

## Related Projects
Check out the main **BrickForce Aurora Project**:
- [GitHub Repository](https://github.com/Brick-Force-Aurora)
- [Official Website](https://brick-force-aurora.github.io/Website/)

## License
This project is licensed under the **MIT License**. See the `LICENSE` file for details.

