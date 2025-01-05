# BrickForce MapUnpacker

BrickForce MapUnpacker is a tool designed to convert `regmap` and `geometry` files into 3D models using OBJ format and human-readable JSON files. It also extracts relevant information about the map and exports its thumbnail. This application is built using the .NET Framework 4.8 and runs on Windows.

## Features

- **File Conversion**: Convert `regmap` and `geometry` files into:
  - 3D models in OBJ format
  - Human-readable JSON metadata
- **Map Information**: Extract and display detailed information about the map.
- **Thumbnail Export**: Automatically export map thumbnails.

## Installation

### Prerequisites
- Windows operating system
- .NET Framework 4.8 installed

### Download
- Download the latest release from the [Releases](https://github.com/Brick-Force-Aurora/MapUnpacker/releases).

### Installation Steps
1. Unzip the downloaded release.
3. Run the `MapUnpacker.exe` file.

## Important Locations

- `DATA/bricks.json`: This file contains a JSON database of all available bricks, along with metadata about these bricks.
- `MapUnpacker/bin`: The directory containing the application build output.

## How to Use

1. **Load Files**:
   - Open the application and load your `regmap` and `geometry` files.
2. **View Map Information**:
   - The application will display relevant map details.
3. **Export Files**:
   - Use the export options to generate OBJ models, JSON metadata, and map thumbnails.

## Development

### Prerequisites
- [Visual Studio](https://visualstudio.microsoft.com/) installed.

### Steps
1. Clone the project:
   ```bash
   git clone https://github.com/Brick-Force-Aurora/MapUnpacker.git
   ```
2. Open the solution in Visual Studio.
3. Build the project.
4. Run the application locally to test your changes.

## Screenshot of UI

![](https://imgur.com/a/p14XVMH)

## Contributing

Development is ongoing. If you would like to contribute, please feel free to fork the repository and submit a pull request.

## Related Projects

Check out the main BrickForce Aurora Project:
- [GitHub Repository](https://github.com/Brick-Force-Aurora/Brick-Force)
- [Official Website](https://brick-force-aurora.github.io/Website/)

## License

This project is licensed under the MIT License. See the LICENSE file for details.