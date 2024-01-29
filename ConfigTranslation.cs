using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palworld_server_protector_DotNet
{
    internal class ConfigTranslation
    {
        private Dictionary<string, string> translations;

        public ConfigTranslation()
        {
            translations = new Dictionary<string, string>
        {
            {"Difficulty","难度"},
            {"DayTimeSpeedRate","白天流逝速度"},
            {"NightTimeSpeedRate","夜晚流逝速度"},
            {"ExpRate","经验值倍率"},
            {"PalCaptureRate","捕捉概率倍率"},
            {"PalSpawnNumRate","帕鲁出现数量倍率"},
            {"PalDamageRateAttack","帕鲁攻击伤害倍率"},
            {"PalDamageRateDefense","帕鲁承受伤害倍率"},
            {"PlayerDamageRateAttack","玩家攻击伤害倍率"},
            {"PlayerDamageRateDefense","玩家承受伤害倍率"},
            {"PlayerStomachDecreaceRate","玩家饱食度降低倍率"},
            {"PlayerStaminaDecreaceRate","玩家耐力倍率"},
            {"PlayerAutoHPRegeneRate","玩家生命值恢复倍率"},
            {"PlayerAutoHpRegeneRateInSleep","玩家睡眠时生命恢复倍率"},
            {"PalStomachDecreaceRate","帕鲁饱食度降低倍率"},
            {"PalStaminaDecreaceRate","帕鲁耐力降低倍率"},
            {"PalAutoHPRegeneRate","帕鲁生命值自然恢复倍率"},
            {"PalAutoHpRegeneRateInSleep","帕鲁睡眠时生命恢复倍率"},
            {"BuildObjectDamageRate","对建筑物伤害倍率"},
            {"BuildObjectDeteriorationDamageRate","建筑物劣化速度倍率"},
            {"CollectionDropRate","可采集物品掉落倍率"},
            {"CollectionObjectHpRate","可采集物品生命值倍率"},
            {"CollectionObjectRespawnSpeedRate","可采集物品生成速率"},
            {"EnemyDropItemRate","敌方掉落物品率"},
            {"DeathPenalty","死亡惩罚"},
            {"bEnablePlayerToPlayerDamage","玩家对玩家伤害功能"},
            {"bEnableFriendlyFire","火焰伤害"},
            {"bEnableInvaderEnemy","袭击事件"},
            {"bActiveUNKO","未知"},
            {"bEnableAimAssistPad","瞄准辅助手柄"},
            {"bEnableAimAssistKeyboard","准星开启"},
            {"DropItemMaxNum","掉落物品最大数量"},
            {"DropItemMaxNum_UNKO","掉落物品最大数量_UNKO"},
            {"BaseCampMaxNum","大本营最大数"},
            {"BaseCampWorkerMaxNum","大本营工人最多人数"},
            {"DropItemAliveMaxHours","掉落物品存在最大时长"},
            {"bAutoResetGuildNoOnlinePlayers","自动重置没有在线玩家的公会"},
            {"AutoResetGuildTimeNoOnlinePlayers","无在线玩家时自动重置生成时间"},
            {"GuildPlayerMaxNum","公会玩家最大数量"},
            {"PalEggDefaultHatchingTime","帕鲁蛋默认孵化时间"},
            {"WorkSpeedRate","工作速率"},
            {"bIsMultiplay","多人游戏"},
            {"bIsPvP","PvP"},
            {"bCanPickupOtherGuildDeathPenaltyDrop","可拾取其他公会的死亡掉落物"},
            {"bEnableNonLoginPenalty","不登录惩罚"},
            {"bEnableFastTravel","快速旅行"},
            {"bIsStartLocationSelectByMap","通过地图选择起始位置"},
            {"bExistPlayerAfterLogout","注销后玩家仍然存在"},
            {"bEnableDefenseOtherGuildPlayer","防御其他公会玩家功能"},
            {"CoopPlayerMaxNum","合作玩家最大人数"},
            {"ServerPlayerMaxNum","服务器玩家最大人数"},
            {"ServerName","服务器名称"},
            {"ServerDescription","服务器描述"},
            {"AdminPassword","管理员密码"},
            {"ServerPassword","服务器密码"},
            {"PublicPort","服务器端口"},
            {"PublicIP","服务器ip"},
            {"RCONEnabled","RCON功能"},
            {"RCONPort","RCON端口"},
            {"Region"," 地区"},
            {"bUseAuth","正版验证"},
            {"BanListURL","封禁用户URL"}

        };
        }

        public string GetTranslation(string key)
        {
            if (translations.TryGetValue(key, out string translation))
            {
                return translation;
            }
            return ""; // 如果键不存在，返回空字符串
        }
    }
}
