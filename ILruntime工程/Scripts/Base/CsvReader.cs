using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CsvReader
{
    private readonly TextReader reader;
    public StringBuilder stb = new StringBuilder();

    private const char Separator = ',';
    private const char NewLine = '\n';
    private const char NewEnterLine = '\r';
    private const char StringSign = '\"';
    private const int ReadEnd = -1;
    private int nowToken = 0;  //-1表示数据全部读取完毕

    //str++: 缓存满，逗号，换行，引号


    //,a"\n

    private int nowRowIndex = -1;

    public int NowRowIndex
    {
        get { return this.nowRowIndex; }
    }

    public readonly Dictionary<string, Hashtable> DataDict;

    public CsvReader(Stream stream)
    {
        this.reader = new StreamReader(stream);//Encoding.GetEncoding("gb2312"));
        DataDict = ReadHashTable();
        reader.Close();
    }
    public CsvReader(TextReader tr)
    {
        this.reader = tr;
        DataDict = ReadHashTable();
        reader.Close();
    }

    public CsvReader(string csvFilePath)
    {
        this.reader = new StreamReader(csvFilePath);//Encoding.GetEncoding("gb2312"));
        DataDict = ReadHashTable();
        reader.Close();
    }

    private List<string[]> ReadCsv()
    {
        var list = new List<string[]>();
        while (this.nowToken != ReadEnd)
        {
            list.Add(this.ReadRow());
        }
        return list;
    }

    private Dictionary<string, Hashtable> ReadHashTable()
    {
        Dictionary<string, Hashtable> dict = new Dictionary<string, Hashtable>();
        List<string[]> list = ReadCsv();
        if (list.Count == 0) return dict;
        string[] keyArr = list[0];
        if (keyArr == null || keyArr.Length == 0) return dict;
        int keyLen = keyArr.Length;
        for (int i = 1, listCount = list.Count; i < listCount; i++)
        {
            if (list[i] == null || list[i].Length < keyLen)
            {
                continue;
            }
            Hashtable hash = new Hashtable();
            for (int keyIndex = 0; keyIndex < keyLen; keyIndex++)
            {
                hash.Add(keyArr[keyIndex], list[i][keyIndex]);
            }
            dict.Add(list[i][0], hash);
        }
        return dict;
    }

    private string[] ReadRow()
    {
        lock (this)
        {
            var list = new List<string>();
            do
            {
                list.Add(this.ReadItem());
                this.nowRowIndex++;
            } while (this.nowToken != ReadEnd && this.nowToken != NewLine);
            return list.ToArray();
        }
    }

    private string ReadItem()
    {
        int chr = this.reader.Read();
        if (chr < 0)
        {
            this.nowToken = ReadEnd;
            return null;
        }
        var nChar = (char)chr;

        if (chr == Separator)
        {
            this.nowToken = Separator;
            return "";
        }
        if (chr == NewLine || chr == NewEnterLine)
        {
            this.nowToken = NewLine;
            return null;
        }
        this.stb.Remove(0, this.stb.Length);
        //this.stb.Clear();

        if (chr == StringSign)
        {
            this.nowToken = StringSign;
            return this.ParserString();
        }

        this.stb.Append(nChar);
        return this.charAppend();
    }

    private string ParserString()
    {
        int chr;
        while ((chr = this.reader.Read()) >= 0)
        {
            char nChar = (char)chr;
            if (nChar == StringSign)
            {
                chr = this.reader.Read();
                if (chr < 0)
                {
                    this.nowToken = ReadEnd;
                    return this.stb.ToString();
                }
                else if (chr == StringSign)
                {
                    this.stb.Append(StringSign);
                }
                else
                {
                    this.nowToken = 0;
                    return this.charAppend(chr);
                }
            }
            else
                this.stb.Append(nChar);
        }
        return this.stb.ToString();
    }
    private string charAppend(int chr)
    {
        if (chr == Separator)
        {
            this.nowToken = Separator;
            return this.stb.ToString();
        }
        else if (chr == NewLine || chr == NewEnterLine)
        {
            this.nowToken = NewLine;
            return this.stb.ToString();
        }
        return this.charAppend();
    }
    private string charAppend()
    {
        int chr;
        while ((chr = this.reader.Read()) >= 0)
        {
            if (chr == Separator)
            {
                this.nowToken = Separator;
                return this.stb.ToString();
            }
            else if (chr == NewLine || chr == NewEnterLine)
            {
                this.nowToken = NewLine;
                return this.stb.ToString();
            }
            this.stb.Append((char)chr);
        }
        this.nowToken = ReadEnd;
        return this.stb.ToString();
    }
}
