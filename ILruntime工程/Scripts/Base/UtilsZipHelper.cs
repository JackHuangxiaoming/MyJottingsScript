using System;
using System.IO;


using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections;
using UnityEngine.Networking;
using ICSharpCode.SharpZipLib.Zip;

public class UtilsZipHelper
{
    public const string KeyWord = "";

    public static string ZipName = "";
    public static bool IsOk = false;

    public static long CurrentUnCompressIndex = 0;
    public static double CurrentUnCompress = 0;
    public static double CurrentUnCompressSize = 0;

    public static void CompressOrDeCompressAsset(object pathObj)
    {
        DirectoryInfo source = new DirectoryInfo(pathObj.ToString());
        CompressOrDeCompressAsset(source, false);
    }


    public static void CompressOrDeCompressAsset(DirectoryInfo source, bool isCompress)
    {
        ListFiles(source, isCompress);

        if (isCompress)
            Console.WriteLine("压缩全部完成");
        else
        {
            Console.WriteLine("解压全部完成");
            ZipName = "";
            IsOk = true;
        }

    }

    private static void CreateZipFile(string filesPath, string zipFilePath)
    {

        if (!File.Exists(filesPath))
        {
            Console.WriteLine("Cannot find directory '{0}'", filesPath);
            return;
        }

        try
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
            {

                s.SetLevel(9); // 压缩级别 0-9
                s.Password = KeyWord; //Zip压缩文件密码
                byte[] buffer = new byte[4096]; //缓冲区大小
                ZipEntry entry = new ZipEntry(Path.GetFileName(filesPath));
                Console.WriteLine("压缩开始：" + entry.Name);
                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);
                using (FileStream fs = File.OpenRead(filesPath))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
                s.Finish();
                s.Close();
                Console.WriteLine("------压缩结束：" + entry.Name);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception during processing {0}", ex);
        }
    }

    static Dictionary<string, Coroutine> CurrentCoroutinDic = new Dictionary<string, Coroutine>();
    /// <summary>
    /// 不可重复使用 懒得做
    /// </summary>
    internal static void CopyFileByWebRequest(List<string> assetsList, string sourcePath, string savePath, Action<bool, string> callback)
    {
        TestCurrentFileNumberTotal = assetsList.Count;
        TestCurrentFileNumber = 0;

        for (int i = 0; i < assetsList.Count; i++)
        {
            assetsList[i] = assetsList[i].Replace("\r", "");
            assetsList[i] = assetsList[i].Replace("\n", "");
            assetsList[i] = assetsList[i].Replace("\\", "/");
            string uuid = Guid.NewGuid().ToString();
            CurrentCoroutinDic[uuid] = Utils.StartDoCoroutine(CopyFileByWebRequestCoroutine(Path.Combine(sourcePath, assetsList[i]), Path.Combine(savePath, assetsList[i].Replace("jzsh/", "")), callback, uuid));
        }
    }

    private static IEnumerator CopyFileByWebRequestCoroutine(string sourcePath, string savePath, Action<bool, string> callback, string uuid)
    {
        //Utils.Log("CopyFileByWebRequestCoroutine Start：" + sourcePath + "[" + savePath + "[" + uuid);
        yield return 0;

        UnityWebRequest uwr = UnityWebRequest.Get(sourcePath);
        yield return uwr.SendWebRequest();//读取数据

        if (string.IsNullOrEmpty(uwr.error))
        {
            byte[] data = uwr.downloadHandler.data;
            uwr.Abort();

            if (File.Exists(savePath))
                File.Delete(savePath);
            int index = savePath.LastIndexOf('/');
            if (index != -1)
            {
                string dirPath = savePath.Substring(0, index);
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                using (FileStream ws = File.OpenWrite(savePath))
                {
                    ws.Write(data, 0, data.Length);
                }
                TestCurrentFileNumber++;
            }
        }
        else
        {
            Utils.Log("拷贝初始文件失败：" + sourcePath);
            uwr.Abort();
        }
        //Utils.Log("CopyFileByWebRequestCoroutine End：" + sourcePath + "[" + savePath + "[" + uuid);
        if (CurrentCoroutinDic.ContainsKey(uuid))
        {
            CurrentCoroutinDic.Remove(uuid);
        }
        if (CurrentCoroutinDic.Count == 0 && callback != null)
        {
            callback(true, "");
        }
    }

    internal static async void CopyFileMultiSync(string sourcePath, string savePath, Action<bool, string> callback)
    {
        //sourcePath = sourcePath.Replace('/', '\\');
        TestCurrentUnCompressSize = 0;
        TestCurrentUnCompress = 0;
        TestCurrentFileNumber = 0;
        TestCurrentFileNumberTotal = 0;


        Utils.LogError("开始拷贝 " + sourcePath);
        if (!Directory.Exists(sourcePath))
        {
            Utils.LogError("Cannot find Directory " + sourcePath);
            if (callback != null)
            {
                callback(false, sourcePath);
            }
            return;
        }
        ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
        StartCopyFileMultiSync(sourcePath, savePath, tasks);
        await Task.WhenAll(tasks.ToArray());
        if (callback != null)
        {
            callback(true, sourcePath);
        }
    }

    private static void StartCopyFileMultiSync(string sourcePath, string savePath, ConcurrentQueue<Task> tasks)
    {
        DirectoryInfo dirInfo = Directory.CreateDirectory(sourcePath);
        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                StartCopyFileMultiSync(files[i].FullName, Path.Combine(savePath, files[i].Name), tasks);
            }
            else if (files[i].Extension != ".meta")
            {
                //tasks.Enqueue(Task.Run(async () => await CopyFile((FileInfo)files[i], savePath)));
                FileInfo fileInfo = (FileInfo)files[i];
                tasks.Enqueue(Task.Run(async () => await CopyFile(fileInfo, savePath)));
            }
        }
    }
    private static async Task CopyFile(FileInfo file, string savePath)
    {
        if (file == null)
        {
            return;
        }
        if (!Directory.Exists(savePath))//创建文件夹
        {
            Directory.CreateDirectory(savePath);
        }
        savePath = Path.Combine(savePath, file.Name);

        using (FileStream SourceStream = file.OpenRead())
        {
            using (FileStream DestinationStream = File.Create(savePath))
            {
                await SourceStream.CopyToAsync(DestinationStream);
            }
        }
        //Thread.Sleep(10000);       
        //File.Copy(file.FullName, savePath, true);
    }
    private static void Copy(FileInfo file, string savePath)
    {

        //bool zipOk = false;
        //try
        //{
        //    using (Ionic.Zip.ZipInputStream zs = new Ionic.Zip.ZipInputStream(File.OpenRead(sourcePath)))
        //    {
        //        Ionic.Zip.ZipEntry theEntry;
        //        while ((theEntry = zs.GetNextEntry()) != null)
        //        {
        //            TestCurrentUnCompressSize += theEntry.CompressedSize;
        //            TestCurrentFileNumberTotal++;
        //        }
        //    }
        //    using (Ionic.Zip.ZipInputStream zipStream = new Ionic.Zip.ZipInputStream(File.OpenRead(sourcePath)))
        //    {
        //        zipStream.ProvisionalAlternateEncoding = System.Text.Encoding.UTF8;
        //        Ionic.Zip.ZipEntry theEntry;
        //        while ((theEntry = zipStream.GetNextEntry()) != null)
        //        {
        //            TestCurrentFileNumber++;
        //            theEntry.AlternateEncoding = System.Text.Encoding.UTF8;
        //            ZipName = theEntry.FileName;
        //            IsOk = false;
        //            string directoryName = Path.GetDirectoryName(theEntry.FileName);
        //            string fileName = Path.GetFileName(theEntry.FileName);

        //            // create directory
        //            if (directoryName.Length > 0)
        //            {
        //                Directory.CreateDirectory(Path.Combine(savePath, directoryName));
        //            }

        //            if (fileName != String.Empty)
        //            {
        //                using (FileStream streamWriter = File.Create(Path.Combine(savePath, theEntry.FileName)))
        //                {
        //                    int size = 4096;
        //                    byte[] data = new byte[4096];
        //                    while (true)
        //                    {
        //                        size = await zipStream.ReadAsync(data, 0, 4096);
        //                        if (size > 0)
        //                        {
        //                            TestCurrentUnCompress += size;
        //                            await streamWriter.WriteAsync(data, 0, size);
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            //Console.WriteLine("------解压结束：" + theEntry.Name);
        //        }
        //    }
        //    zipOk = true;
        //}
        //catch (Exception)
        //{
        //    zipOk = false;
        //    Utils.LogError("解压出错：" + sourcePath);
        //}

        //return zipOk;
    }

    public static long TestCurrentUnCompressSize = 0;
    public static long TestCurrentUnCompress = 0;
    public static int TestCurrentFileNumber = 0;
    public static int TestCurrentFileNumberTotal = 0;    

    internal static async void UnZipFileSync(string zipFilePath, string savePath, Action<bool, string> callback)
    {
        bool zipOk = false;
        if (!File.Exists(zipFilePath))
        {
            Console.WriteLine("Cannot find file '{0}'", zipFilePath);
            if (callback != null)
            {
                callback(false, zipFilePath);
            }
            return;
        }
        try
        {
            using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry theEntry;
                zipStream.Password = KeyWord;
                while ((theEntry = zipStream.GetNextEntry()) != null)
                {
                    CurrentUnCompressSize += theEntry.Size;
                }
            }
            using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry theEntry;
                zipStream.Password = KeyWord;
                CurrentUnCompress = 0;
                while ((theEntry = zipStream.GetNextEntry()) != null)
                {
                    CurrentUnCompressIndex = theEntry.ZipFileIndex;
                    //Console.WriteLine("解压开始：" + theEntry.Name);
                    ZipName = theEntry.Name;
                    IsOk = false;
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(Path.Combine(savePath, directoryName));
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(Path.Combine(savePath, theEntry.Name)))
                        {
                            int size = 1048576;
                            byte[] data = new byte[1048576];
                            while (true)
                            {
                                size = await zipStream.ReadAsync(data, 0, 1048576);
                                if (size > 0)
                                {
                                    CurrentUnCompress += size;
                                    await streamWriter.WriteAsync(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    //Console.WriteLine("------解压结束：" + theEntry.Name);
                }
            }
            zipOk = true;
        }
        catch (Exception)
        {
            zipOk = false;
            Utils.LogError("解压出错：" + zipFilePath);
        }
        if (callback != null)
        {
            callback(zipOk, zipFilePath);
        }
    }

    public static void UnZipFile(string zipFilePath, string savePath, Action<bool, string> callback = null)
    {
        bool zipOk = false;
        CurrentUnCompress = 0;
        if (!File.Exists(zipFilePath))
        {
            Console.WriteLine("Cannot find file '{0}'", zipFilePath);
            if (callback != null)
            {
                callback(false, zipFilePath);
            }
            return;
        }
        try
        {
            using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry theEntry;
                zipStream.Password = KeyWord;
                while ((theEntry = zipStream.GetNextEntry()) != null)
                {
                    CurrentUnCompressSize += theEntry.CompressedSize;
                }
            }
            using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry theEntry;
                zipStream.Password = KeyWord;
                while ((theEntry = zipStream.GetNextEntry()) != null)
                {
                    //Console.WriteLine("解压开始：" + theEntry.Name);
                    ZipName = theEntry.Name;
                    IsOk = false;
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(Path.Combine(savePath, directoryName));
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(Path.Combine(savePath, theEntry.Name)))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = zipStream.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    CurrentUnCompress += size;
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    //Console.WriteLine("------解压结束：" + theEntry.Name);
                }
            }
            zipOk = true;
        }
        catch (Exception e)
        {
            zipOk = false;
            Utils.LogError("解压出错：" + zipFilePath + e.ToString());
        }
        if (callback != null)
        {
            callback(zipOk, zipFilePath);
        }
    }




    private static void ListFiles(FileSystemInfo info, bool isCompress)
    {
        if (!info.Exists)
        {
            return;
        }
        DirectoryInfo dir = info as DirectoryInfo;

        // 不是目录
        if (dir == null)
        {
            return;
        }

        FileSystemInfo[] files = dir.GetFileSystemInfos();

        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i] as FileInfo;

            // 是文件
            if (file != null)
            {
                string fullName = file.FullName;

                // 如果是压缩文件
                if (isCompress)
                {
                    //if (file.Extension == ".unity3d")
                    if (file.Extension != ".zip")
                    {
                        string outputPath = file.FullName.Replace(file.Extension, ".zip");
                        CreateZipFile(fullName, outputPath);
                        File.Delete(fullName);
                    }
                }
                // 如果是解压文件
                else
                {
                    if (file.Extension == ".zip")
                    {
                        UnZipFile(fullName, info.FullName);
                        File.Delete(fullName);
                    }
                }
            }
        }
    }

    public bool UnZip(string fileToUnZip, string zipedFolder)
    {
        bool result = true;
        FileStream fs = null;
        ZipInputStream zipStream = null;
        ZipEntry ent = null;
        string fileName;
        if (!File.Exists(fileToUnZip))
            return false;
        try
        {
            zipStream = new ZipInputStream(File.OpenRead(fileToUnZip));
            while ((ent = zipStream.GetNextEntry()) != null)
            {
                if (!string.IsNullOrEmpty(ent.Name))
                {
                    fileName = zipedFolder;

                    if (fileName.EndsWith("\\"))
                    {
                        Directory.CreateDirectory(fileName);
                        continue;
                    }

                    fs = File.Create(fileName);
                    int size = 2048;
                    byte[] data = new byte[size];
                    while (true)
                    {
                        size = zipStream.Read(data, 0, size);
                        if (size > 0)
                            fs.Write(data, 0, data.Length);
                        else
                            break;
                    }
                }
            }
        }
        catch
        {
            result = false;
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
            if (zipStream != null)
            {
                zipStream.Close();
                zipStream.Dispose();
            }
            if (ent != null)
            {
                ent = null;
            }
            GC.Collect();
            GC.Collect(1);
        }
        return result;
    }

}


