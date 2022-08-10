using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// TS脚本转换CS脚本工具
/// </summary>
public class TsChangeCsTool
{
    static string TsClassPath = Application.dataPath + "/../TsClass";//源TsClass文件夹,ts格式
    static string CsClassPath = Application.dataPath + "/../../PlatformLobbyCode/Data/TSJsonServerData";//生成的c#脚本文件夹

    [MenuItem("TsChangeCs/TsChangeCs转换开始")]
    static void TsChangeCs()
    {
        if (!Directory.Exists(CsClassPath))
        {
            Directory.CreateDirectory(CsClassPath);
        }
        TsChangeCsStart();
    }

    static void TsChangeCsStart()
    {
        Directory.Delete(CsClassPath, true);
        Directory.CreateDirectory(CsClassPath);

        string[] tsPaths = Directory.GetFiles(TsClassPath, "*.ts");
        for (int i = 0; i < tsPaths.Length; i++)
        {
            //0.读ts
            string className;//类型名
            List<string> datasList = new List<string>();//数据

            try
            {
                string excelPath = tsPaths[i];//ts脚本路径 
                className = Path.GetFileNameWithoutExtension(excelPath).ToLower();
                string[] lines = System.IO.File.ReadAllLines(excelPath);
                for (int j = 0; j < lines.Length; ++j)
                {
                    datasList.Add(lines[j]);
                }
            }
            catch (System.Exception exc)
            {
                Debug.LogError("请关闭TS脚本:" + exc.Message);
                return;
            }

            //写Cs
            WriteCsByTs(className, datasList);
        }

        AssetDatabase.Refresh();
    }

    static void WriteCsByTs(string className,List<string> datasList)
    {
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.Numerics;");
            stringBuilder.Append("\n");
            stringBuilder.AppendLine("namespace PlatformLobbyCode");
            stringBuilder.AppendLine("{");
            for (int i = 0; i < datasList.Count; i++)
            {
                string line = datasList[i];
                if (line.Contains("public"))
                {
                    line = line.Replace("public", "");
                }
                if (line.Contains("export type ErrorCodeMsg = {"))
                {
                    line = line.Replace("export type ErrorCodeMsg = {", "    public class ErrorCodeMsg {");
                }
                if (line.Contains("export"))
                {
                    line = line.Replace("export", "    public ");
                }
                if (line.Contains("eventName ="))
                {
                    line = line.Replace("eventName =", "    public string eventName =");
                }
                if (line.Contains("playerName: string = '';"))
                {
                    line = line.Replace("playerName: string = '';", "    public string playerName;");
                }
                if (line.Contains("playerUUID: string = '';"))
                {
                    line = line.Replace("playerUUID: string = '';", "    public string playerUUID;");
                }
                if (line.Contains(": string"))
                {
                    line = line.Replace(": string", "");
                    line = "        public string" + line;
                }
                if (line.Contains("combatAttr: number[] = new Array(16).fill(0);"))
                {
                    line = line.Replace("combatAttr: number[] = new Array(16).fill(0);", "    public List<int> combatAttr;");
                }
                if (line.Contains("combatAttr: number[] = [];"))
                {
                    line = line.Replace("combatAttr: number[] = [];", "    public List<int> combatAttr;");
                }
                if (line.Contains("gangID: number | undefined;"))
                {
                    line = line.Replace("gangID: number | undefined;", "    public int gangID;");
                }
                if (line.Contains("attrChange: number[];"))
                {
                    line = line.Replace("attrChange: number[];", "    public List<int> attrChange;");
                }
                if (line.Contains("attr: number[];"))
                {
                    line = line.Replace("attr: number[];", "    public List<int> attr;");
                }
                if (line.Contains(": number"))
                {
                    line = line.Replace(": number", "");
                    line = "        public int" + line;
                }
                if (line.Contains("interface"))
                {
                    line = line.Replace("interface", "class");
                }
                if (line.Contains(": any"))
                {
                    line = line.Replace(": any", "");
                    line = "        public object" + line;
                }
                if (line.Contains(": boolean"))
                {
                    line = line.Replace(": boolean", "");
                    line = "        public bool" + line;
                }
                if (line.Contains("velocity: Vector2 = { x: -1, y: -1 };"))
                {
                    line = line.Replace("velocity: Vector2 = { x: -1, y: -1 };", "    public Vector2 velocity;");
                }
                if (line.Contains("startPos: Vector2 = { x: -1, y: -1 };"))
                {
                    line = line.Replace("startPos: Vector2 = { x: -1, y: -1 };", "    public Vector2 startPos;");
                }
                if (line.Contains("targetPos: Vector2 = { x: -1, y: -1 };"))
                {
                    line = line.Replace("targetPos: Vector2 = { x: -1, y: -1 };", "    public Vector2 targetPos;");
                }
                if (line.Contains("pos: Vector2 = { x: -1, y: -1 };"))
                {
                    line = line.Replace("pos: Vector2 = { x: -1, y: -1 };", "    public Vector2 pos;");
                }
                if (line.Contains("appearance: Appearance = { hair: 0, cloth: 0, face: 0 };"))
                {
                    line = line.Replace("appearance: Appearance = { hair: 0, cloth: 0, face: 0 };", "    public Appearance appearance;");
                }
                if (line.Contains(": Vector2"))
                {
                    line = line.Replace(": Vector2", "");
                    line = "        public Vector2" + line;
                }
                if (line.Contains("extends"))
                {
                    line = line.Replace("extends", ":");
                }
                if (line.Contains("static"))
                {
                    line = line.Replace("static", "");
                }
                if (line.Contains(": CreatureStat"))
                {
                    line = line.Replace(": CreatureStat", "");
                    line = "        public CreatureStat" + line;
                }
                if (line.Contains("skills: SkillDisPlay[] = [];"))
                {
                    line = line.Replace("skills: SkillDisPlay[] = [];", "    public List<SkillDisPlay> skills;");
                }
                if (line.Contains("takeDamages: Damage[] = [];"))
                {
                    line = line.Replace("takeDamages: Damage[] = [];", "    public List<Damage> takeDamages;");
                }
                if (line.Contains("buffs: BuffDisPlay[] = [];"))
                {
                    line = line.Replace("buffs: BuffDisPlay[] = [];", "    public List<BuffDisPlay> buffs;");
                }
                if (line.Contains("avatar_data: AvatarDisplay[] = [];"))
                {
                    line = line.Replace("avatar_data: AvatarDisplay[] = [];", "    public List<AvatarDisplay> avatar_data;");
                }
                if (line.Contains("frame_data: FrameAction[] = [];"))
                {
                    line = line.Replace("frame_data: FrameAction[] = [];", "    public List<FrameAction> frame_data;");
                }
                if (line.Contains("players: AvatarTeamDisplay[];"))
                {
                    line = line.Replace("players: AvatarTeamDisplay[];", "    public List<AvatarTeamDisplay> players;");
                }
                if (line.Contains(": ErrCode"))
                {
                    line = line.Replace(": ErrCode", "");
                    line = "        public ErrCode" + line;
                }
                if (line.Contains(": ErrorCodeMsg"))
                {
                    line = line.Replace(": ErrorCodeMsg", "");
                    line = "        public ErrorCodeMsg" + line;
                }
                if (line.Contains("data: AvatarDie;"))
                {
                    line = line.Replace("data: AvatarDie;", "    public AvatarDie data;");
                }
                if (line.Contains("data: TakeDamage;"))
                {
                    line = line.Replace("data: TakeDamage;", "    public TakeDamage data;");
                }
                if (line.Contains("data: BuffEffect;"))
                {
                    line = line.Replace("data: BuffEffect;", "    public BuffEffect data;");
                }
                if (line.Contains("data: BuffEnd;"))
                {
                    line = line.Replace("data: BuffEnd;", "    public BuffEnd data;");
                }
                if (line.Contains("data: Move;"))
                {
                    line = line.Replace("data: Move;", "    public Move data;");
                }
                if (line.Contains("data: AvatarFull;"))
                {
                    line = line.Replace("data: AvatarFull;", "    public AvatarFull data;");
                }
                if (line.Contains("data: MonsterFull;"))
                {
                    line = line.Replace("data: MonsterFull;", "    public MonsterFull data;");
                }
                if (line.Contains("data: LeaveWorld;"))
                {
                    line = line.Replace("data: LeaveWorld;", "    public LeaveWorld data;");
                }
                if (line.Contains("data: SkillDisPlay;"))
                {
                    line = line.Replace("data: SkillDisPlay;", "    public SkillDisPlay data;");
                }
                if (line.Contains("data: WXMJInfo;"))
                {
                    line = line.Replace("data: WXMJInfo;", "    public WXMJInfo data;");
                }
                if (line.Contains("data: createTeamParam;"))
                {
                    line = line.Replace("data: createTeamParam;", "    public createTeamParam data;");
                }
                if (line.Contains("data: searchTeamParam;"))
                {
                    line = line.Replace("data: searchTeamParam;", "    public searchTeamParam data;");
                }
                if (line.Contains("data: WXMJTeam[];"))
                {
                    line = line.Replace("data: WXMJTeam[];", "    public List<WXMJTeam> data;");
                }
                if (line.Contains("data: joinTeamParam;"))
                {
                    line = line.Replace("data: joinTeamParam;", "    public joinTeamParam data;");
                }
                if (line.Contains("data: JoinTeamResult;"))
                {
                    line = line.Replace("data: JoinTeamResult;", "    public JoinTeamResult data;");
                }
                if (line.Contains("data: MonsterDie;"))
                {
                    line = line.Replace("data: MonsterDie;", "    public MonsterDie data;");
                }
                if (line.Contains("team: WXMJTeam | undefined;"))
                {
                    line = line.Replace("team: WXMJTeam | undefined;", "    public WXMJTeam team;");
                }
                if (line.Contains("team: WXMJTeam | null;"))
                {
                    line = line.Replace("team: WXMJTeam | null;", "    public WXMJTeam team;");
                }
                if (line.Contains("}"))
                {
                    line = line.Replace("}", "    }");
                }

                stringBuilder.Append(line);
                stringBuilder.Append("\n");
            }
            stringBuilder.Append("\n");
            stringBuilder.Append("}");

            string csPath = CsClassPath + "/" + className + ".cs";
            if (File.Exists(csPath))
            {
                File.Delete(csPath);
            }
            using (StreamWriter sw = new StreamWriter(csPath))
            {
                sw.Write(stringBuilder);
                Debug.Log("生成:" + csPath);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("写入CS失败:" + e.Message);
            throw;
        }
    }
}
