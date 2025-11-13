using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace COL1.Utilities
{
    public struct CSVRow {
        string[] data;

        public CSVRow(string[] strings) => data = strings;
        public string Get(int index) => data[index];


        public override string ToString()
        {
            return $"(CSV_ROW) data: {data.Length}";
        }

    }

    public class CSVDocument
    {
        CSVRow[] rows;
        string[] header;

        public CSVDocument(string path)
        {

            TextAsset textAsset = Resources.Load<TextAsset>(path);
            string[] lines = textAsset.text.Split('\n');
            if (lines.Length == 0) return;

            //generate header
            header = lines[0].Replace("\n","").Replace("\r", "").Split(";");

            //generate rows
            rows = new CSVRow[lines.Length-2];

            for (int i = 1; i < lines.Length; i++) {
                //Debug.Log($"index: {i} \t line: {lines[i]}");
                if (lines[i] == "") continue;
                rows[i-1] = new(lines[i].Split(";"));
            }

        }

        public CSVRow GetRow(int id) => rows[id];
        public CSVRow[] GetRows() => rows;
        public int FromHeader(string col)
        {
            for (int i = 0; i < header.Length; i++)
            {
                //Debug.Log($"{header[i]} != {col}");
                if (header[i] != col) continue;
                return i;
            }
            return -1;
        }


        public string GetRawData(string col, int id)
        {
            int i = FromHeader(col);
            CSVRow row = GetRow(id);
            Debug.Log($"row : {row} | i: {i} | doc_headers:{string.Join(',', header)}");
            return row.Get(i);
        }

        public int GetIntData(string col, int id) {
            string raw = GetRawData(col, id);
            return raw != null ? int.Parse(raw) : 0;
        }

        public float GetFloatData(string col, int id)
        {
            string raw = GetRawData(col, id);
            return raw != null ? float.Parse(raw) : 0;
        }

        public void FilterDoc(string col, Func<string, bool> comparator)
        {
            int i = FromHeader(col);
            List<CSVRow> newRows = new List<CSVRow>();

            foreach (CSVRow row in rows) {
                if (comparator(row.Get(i))) newRows.Add(row);
            }
            rows = newRows.ToArray();
        }

        public int FindFromColValue(string col, string val)
        {
            int colID = FromHeader(col);
            for(int i = 0; i < rows.Length; i++)
            {
                if(rows[i].Get(colID)  == val) return i;
            }
            return -1;
        }
    }
}