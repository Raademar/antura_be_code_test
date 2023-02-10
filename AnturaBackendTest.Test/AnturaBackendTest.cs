using System.Collections.Generic;
using System.IO;
using System.Text;
using AnturaBackendTest;
using NUnit.Framework;

[TestFixture]
public class Tests
{
    private Util _util;

    [SetUp]
    public void Setup()
    {
        _util = new Util();
        using FileStream fileStream =
            new("file_file.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        string[] lines =
        {
            "file_file file_file", "file_file_file_file file_file", "file_file_ file_file_file_file_file", "hejsan",
            "tjosan", "file_file _file_file_¤)(#)=#"
        };
        foreach (var line in lines)
        {
            fileStream.Write(Encoding.ASCII.GetBytes(line));
        }
    }

    [Test]
    public void CountOccurenceOfName_Returns_Correct_Count()
    {
        const string target = "file_file";
        List<string> lines = new();
        using (StreamReader reader = new("file_file.txt"))
        {
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine() ?? string.Empty);
            }
        }

        var result = _util.CountOccurenceOfName(lines, target);
        Assert.AreEqual(10, result);
    }

    [Test]
    public void CountOccurenceOfName_Fails_With_Wrong_Count()
    {
        const string target = "file_file";
        List<string> lines = new();
        using (StreamReader reader = new("file_file.txt"))
        {
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine() ?? string.Empty);
            }
        }

        var result = _util.CountOccurenceOfName(lines, target);
        Assert.AreNotEqual(1, result);
    }

    [Test]
    public void ValidateArgs_Returns_True_With_Correct_Name()
    {
        string[] args =
        {
            "file_file.txt",
        };

        var res = _util.ValidateArgs(args);

        Assert.AreEqual(true, res.valid);
    }

    [Test]
    public void ValidateArgs_Returns_False_With_Incorrect_Name()
    {
        string[] args =
        {
            "wrong_name.txt",
        };

        var res = _util.ValidateArgs(args);

        Assert.AreEqual(false, res.valid);
    }

    [Test]
    public void ValidateArgs_Returns_False_With_Too_Many_Args_But_Correct_Name()
    {
        string[] args =
        {
            "file_file.txt",
            "extra arg"
        };

        var res = _util.ValidateArgs(args);

        Assert.AreEqual(false, res.valid);
    }

    [Test]
    public void ValidateArgs_Returns_False_With_Too_Many_Args()
    {
        string[] args =
        {
            "wrong_name.txt",
            "extra arg"
        };

        var res = _util.ValidateArgs(args);

        Assert.AreEqual(false, res.valid);
    }
}