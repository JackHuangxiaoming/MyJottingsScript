# C# __I/O 流 #

## - I/O 3大类  静态类 ##

1. Path 
	1. GetTempPath();//快速取得指定文件的路径		   	取路径
	2. GetFileName();//取得指定路径的文件名          	取文件名及后缀
	3. Combine();//2个路径的string 组合在一起       	组合string
	4. GetFullPath();//返回指定文件的绝对路径       	取绝对路径
	5. GetDirectoryName();//返回指定路径目录信息   	取目录
	6. GetExtension();//返回指定路径文件的后缀名   	取后缀

2. File	
	1. Create();//创建或覆盖某文件					新建文件	
	2. Copy();//复制文件A到B							复制
	3. Move();//移动文件A到B							移动
	4. Delete();//删除文件A 							删除	
	5. ReadAllBytes();//读取指定文件					读取指定文件byte[] 	字单位
	6. ReadAllLines();//打开指定文件					读取指定文件string[] 	行单位
	7. ReadAllText();//打开指定文件					读取指定文件string 	页单位
	8. WriteAllBytes();//写入指定文件					写入指定文件byte[]	字单位
	9. WriteAllLines();//写入指定文件					写入指定文件string[]	行单位
	10. WriteAllText();//写入指定文件					写入指定文件string	页单位
	11. AppendAllText();//追加写入指定字符串			写入指定文件string	

3. Directory

	1. CreateDirectory();//创建文件夹					创建文件夹
	2. GetFiles();//取得指定文件夹下所有目录			取得文件目录string[]
	3. Move();//移动文件夹及文件						移动文件夹
	4. Delete();//删除文件夹							删除文件夹

## String类 ##

1. Concat();//字符串拼接  	返回string
2. Copy();//字符串复制    	返回string
3. Equals();//字符串比较  	返回bool
4. Contains();//字符串包含	返回bool
5. Insert();//插入指定角标的 string				返回string
6. PadLeft();//在左侧填充空格到达指定string长度		返回string
7. PadRight();//在右侧填充空格到达指定string长度	返回string
8. Remove();//删除指定角标至最后					返回string
9. Replace();//替换字符串							返回string
10. StartsWith();//比较开头string				返回bool
11. IndexOfAny();//返回匹配字符串第一次出现角标 	返回int
12. LastIndexOf();//返回匹配字符串最后一次出现角标 	返回int
13. Split();//按指定字符分割string				返回string[]
14. Substring();//取得指定角标后的string			返回string
15. ToLower();//取得string小写副本				返回string
16. ToUpper();//取得string大写副本				返回string
17. Trim();//移除指定string						返回string


## Encoding类 ##
编码格式：指的就是你以怎样的形式来存储字符串

1. Encoding.UFT-8.GetString();//把指定储存格式转换为utf-8的string    	返回string
2. Encoding.Default.GetBytes();//把指定储存格式转换为默认的Byte			返回byte

## 综合案例  ##
## 复制aaa文件夹及内容到bbb文件夹 ##
	Directory.CreateDirectory(@"C:\Users\admin\Desktop\bbb");//创建文件夹
	string[] Fielaaaa = Directory.GetFiles(@"C:\Users\admin\Desktop\aaa");//读取文件夹下的所有目录
	for (int i = 0; i < Fielaaaa.Length; i++)
	{
	 string tempName = Path.GetFileName(Fielaaaa[i]); //取得文件名
	 File.Copy(Fielaaaa[i], @"C: \Users\admin\Desktop\bbb\" + tempName);//复制文件到指定文件夹下+文件名
	}