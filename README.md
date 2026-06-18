# GbxGhostToClip

Convert a TrackMania Nations Forever (TMNF) or TrackMania United Forever (TMUF) `.Ghost.Gbx` file into a MediaTracker `.Clip.Gbx` you can import into the replay editor in-game.

This is useful for ghosts downloaded from the [UnitedLadder leaderboards](https://ul.unitedascenders.xyz/leaderboards/tracks).

Built with [GBX.NET](https://github.com/BigBang1112/gbx-net).

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A TMNF or TMUF ghost file (`.Ghost.Gbx`)

## Setup

```bash
git clone git@github.com:ethan-xd/GbxGhostToClip.git
cd GbxGhostToClip
dotnet restore
```

## Build

Release build:

```bash
dotnet build -c Release
```

The executable is written to:

```
bin/Release/net8.0/GbxGhostToClip.exe
```

Optionally, publish a folder you can copy elsewhere (requires .NET 8 runtime on the target machine):

```bash
dotnet publish -c Release -r win-x64 --self-contained false -o publish
```

## Usage

```bash
GbxGhostToClip.exe <path-to-ghost.Ghost.Gbx>
```

Examples:

```bash
# From build output
.\bin\Release\net8.0\GbxGhostToClip.exe "C:\path\to\ghost.Ghost.Gbx"

# During development
dotnet run -c Release -- "C:\path\to\ghost.Ghost.Gbx"
```

The clip is saved in the **current working directory** with a name like:

```
a1b2c3d4_PlayerLogin_(0'12''34).Clip.Gbx
```

Where the first UID is the challenge UID.

If that file already exists, a numeric suffix is appended (`...1.Clip.Gbx`, `...2.Clip.Gbx`, etc.).

### Importing in TMNF/TMUF

1. Open an existing replay driven on the same challenge in the replay editor.
2. Import the generated `.Clip.Gbx`.
3. The ghost block should appear on the track timeline.