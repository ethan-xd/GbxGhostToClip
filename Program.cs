using GBX.NET;
using GBX.NET.LZO;
using GBX.NET.Engines.Game;
using TmEssentials;

if (args.Length == 0)
{
    Console.Error.WriteLine("Usage: GbxGhostToClip <path-to-ghost.Ghost.Gbx>");
    return 1;
}

var ghostPath = args[0];

if (!File.Exists(ghostPath))
{
    Console.Error.WriteLine($"Ghost file not found: {ghostPath}");
    return 1;
}

Gbx.LZO = new Lzo();

var ghost = Gbx.ParseNode<CGameCtnGhost>(ghostPath);

var mediaBlockGhost = new CGameCtnMediaBlockGhost();
mediaBlockGhost.CreateChunk<CGameCtnMediaBlockGhost.Chunk030E5001>();
mediaBlockGhost.GhostModel = ghost;
mediaBlockGhost.Start = TimeSpan.FromSeconds(0);
mediaBlockGhost.End = ghost.RaceTime.GetValueOrDefault(TimeSpan.FromSeconds(3)) + TimeSpan.FromSeconds(3);
mediaBlockGhost.StartOffset = 0;

var mediaTrack = new CGameCtnMediaTrack();
mediaTrack.CreateChunk<CGameCtnMediaTrack.Chunk03078001>();
mediaTrack.Name = "Ghost:" + ghost.GhostNickname;
mediaTrack.Blocks.Add(mediaBlockGhost);

var mediaClip = new CGameCtnMediaClip();
mediaClip.CreateChunk<CGameCtnMediaClip.Chunk03079005>();
mediaClip.Tracks.Add(mediaTrack);

var filename = ghost.Validate_ChallengeUid?.ToString().Substring(0, 8) + "_" +
    ghost.GhostLogin + "_(" +
    ghost.RaceTime.GetValueOrDefault(TimeSpan.FromSeconds(0)).ToTmString(true, true) + ")";

var ext = ".Clip.Gbx";

var filenameIndexed = filename;

var i = 1;

while (File.Exists(filenameIndexed + ext)) {
    filenameIndexed = filename + i++.ToString();
}

mediaClip.Save(filenameIndexed + ext, new GbxWriteSettings
    {
        PackDescVersion = 2
    }
);

return 0;