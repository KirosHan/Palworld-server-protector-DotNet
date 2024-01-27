

namespace Palworld_server_protector_DotNet;

public class PalUserInfo
{
    public string name;
    public string uid;
    public string steam_id;

    public PalUserInfo(string row)
    {
        var splits = row.Split(',');
        name     = splits[0];
        uid      = splits[1];
        steam_id = splits[2];
    }
}

public static class RconUtils
{
    private static RconClient _client;

    public static void TestConnection(string rconHost, Int32 rconPort, string passWord)
    {

        _client = new RconClient(rconHost, rconPort);

        if(!_client.Authenticate(passWord))
        {
            throw new Exception("password is incorrect");
        }
    }

    public static bool SelectPlayer(out string steam_id)
    {
        steam_id = string.Empty;

        var players = ShowPlayers();

        if(players.Count <= 0)
        {
           
            return false;
        }

  //      var player = AnsiConsole.Prompt(new SelectionPrompt<PalUserInfo>().UseConverter(p => p.name));

  //      steam_id = player.steam_id;
        return true;
    }

    public static List<PalUserInfo> ShowPlayers()
    {
        var result = new List<PalUserInfo>();

        _client.SendCommand("showplayers", out var resp);

        if(string.IsNullOrEmpty(resp.Body))
        {
            return result;
        }

        var lines = resp.Body.Split('\n');

        if(lines.Length <= 1)
        {
            return result;
        }

        for(int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];

            if(string.IsNullOrEmpty(line))
            {
                continue;
            }

            result.Add(new PalUserInfo(line));
        }

        return result;
    }

    public static string SendMsg(string command)
    {
        _client.SendCommand(command, out var resp);

        return resp.Body;
    }
}