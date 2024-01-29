using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palworld_server_protector_DotNet
{
    internal class Notes
    {
        private Dictionary<string, string> note;

        public Notes()
        {
            note = new Dictionary<string, string>
        {
            {"Difficulty","None或Difficulty"},
{"DeathPenalty","None不掉落,Item只掉物品不掉装备,ItemAndEquipment掉物品和装备All全都掉"},
{"bEnablePlayerToPlayerDamage","True启用,False禁用"},
{"bEnableFriendlyFire","True启用,False禁用"},
{"bEnableInvaderEnemy","True是,False否"},
{"bActiveUNKO","True启用,False禁用"},
{"bEnableAimAssistPad","True启用,False禁用"},
{"bEnableAimAssistKeyboard","True启用,False禁用"},
{"bIsMultiplay","True启用,False禁用"},
{"bIsPvP","True启用,False禁用"},
{"bCanPickupOtherGuildDeathPenaltyDrop","True是,False否"},
{"bEnableNonLoginPenalty","True启用,False禁用"},
{"bEnableFastTravel","True启用,False禁用"},
{"bIsStartLocationSelectByMap","通过地图选择起始位置"},
{"bExistPlayerAfterLogout","True是,False否"},
{"bEnableDefenseOtherGuildPlayer","True启用,False禁用"},
{"RCONEnabled","True启用,False禁用"},
{"RCONPort","RCON端口"},
{"bUseAuth","True启用,False禁用"}


        };
        }

        public string GetNote(string key)
        {
            if (note.TryGetValue(key, out string translation))
            {
                return translation;
            }
            return ""; // 如果键不存在，返回空字符串
        }
    }
}
