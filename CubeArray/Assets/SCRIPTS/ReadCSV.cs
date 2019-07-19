using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;

public class ReadCSV : MonoBehaviour {

    public List<List<object>> getList (string filename) {

        List<List<object>> Data = new List<List<object>>();
        string path = "Assets/CSV/" + filename;
        Debug.Log(path);
        using (TextFieldParser parser = new TextFieldParser(path))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                List<object> list = new List<object>();
                string[] row = parser.ReadFields();
                foreach (string field in row)
                {
                    list.Add(field);
                }
                Data.Add(list);
            }
        }
        return Data;
    }
}
