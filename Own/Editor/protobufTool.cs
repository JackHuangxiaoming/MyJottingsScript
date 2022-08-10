using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class protobufTool
{
    /// <summary>
    /// ��������
    /// </summary>
    private static Process CreateShellExProcess(string cmd, string args, string workingDir = "")
    {
        ProcessStartInfo startInfo = new ProcessStartInfo(cmd, args);
        if (!string.IsNullOrEmpty(workingDir))
        {
            startInfo.WorkingDirectory = workingDir;
        }

        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = true;
        startInfo.RedirectStandardOutput = false;
        startInfo.RedirectStandardError = false;
        startInfo.RedirectStandardInput = false;
        return Process.Start(startInfo);
    }

    /// <summary>
    /// ����bat�ű�
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="args"></param>
    /// <param name="workingDir"></param>
    public static void RunShellExProcess(string cmd, string args, string workingDir = "")
    {
        Process process = CreateShellExProcess(cmd, args, workingDir);
        process.Dispose();
        process.Close();
    }

    [MenuItem("MJTool/����Protobuf�ű�")]
    public static void CreateProtobufCode()
    {
        string createPath = "../PlatformLobbyCode/ProtobufAutoCode";

        if (Directory.Exists(createPath))
        {
            Directory.Delete(createPath, true);
        }

        Directory.CreateDirectory(createPath);

        RunShellExProcess($"{Application.dataPath}/../ProtobufData/RunProtoc.bat", null,
            $"{Application.dataPath}/../ProtobufData");

        //UpdateInclude();
    }

    [MenuItem("MJTool/���°���")]
    public static void UpdateInclude()
    {
        string path = "../PlatformLobbyCode/PlatformLobbyCode.csproj";

        File.WriteAllBytes(path, File.ReadAllBytes(path));

    }
}
