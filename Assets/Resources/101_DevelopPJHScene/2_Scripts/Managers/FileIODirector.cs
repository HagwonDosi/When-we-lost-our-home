using System.IO;
using UnityEngine;
using System.Collections;

public class FileIODirector
{
    static public StreamReader ReadFile(string fPath)
    {
        StreamReader reader = null;
        try
        {
            reader = new StreamReader(fPath);
        }
        catch
        {
            if (reader == null)
            {
                Debug.LogWarning("MapLoader.ReadFile() " + "File doesn't Exist at " + fPath);
            }
        }

        return reader;
    }

    static public StreamWriter WriteFile(string fPath)
    {
        StreamWriter writer = new StreamWriter(fPath);

        if (writer == null)
        {
            Debug.LogWarning("MapLoader.ReadFile() " + "File doesn't Exist at " + fPath);
        }

        return writer;
    }
}