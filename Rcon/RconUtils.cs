
namespace Palworld_server_protector_DotNet;



public static class RconUtils
{
    public static RconClient GetClient(string rconHost, Int32 rconPort, string passWord)
    {

        var result = new RconClient(rconHost, rconPort);

        if (!result.Authenticate(passWord))
        {
            throw new Exception("password is incorrect");
        }

        return result;
    }



    public static List<PalUserInfo> ShowPlayers(string rconHost, Int32 rconPort, string passWord)
    {
        var result = new List<PalUserInfo>();

        using var client = GetClient(rconHost, rconPort, passWord);

        client.SendCommand("showplayers", out var resp);

        if (string.IsNullOrEmpty(resp.Body))
        {
            return result;
        }

        var lines = resp.Body.Split('\n');

        if (lines.Length <= 1)
        {
            return result;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            result.Add(new PalUserInfo(line));
        }

        return result;
    }

    public static string SendMsg(string rconHost, Int32 rconPort, string passWord,string command)
    {
        using var client = GetClient(rconHost, rconPort, passWord);

        client.SendCommand(command, out var resp);

        return resp.Body;
    }
}