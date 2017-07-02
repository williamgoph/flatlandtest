using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class Entity
{
    private Int32 id;
    public Int32 ID 
    { 
        get { return id; }
        set { id = value; }
    }

    private DateTime created;
    public DateTime Created
    {
        get { return created; }
        set { created = value; }
    }

    private String type;
    public String Type
    {
        get { return type; }
        set { type = value; }
    }

    public String content;
    public String Content
    {
        get { return content; }
        set { content = value; }
    }
}


public static class DBTrans
{

    public static DataTable ExecQuery(String sql)
    {
        string DBConnection = GetConfig("Config", "DBConnection");
        SqlDataAdapter sda = new SqlDataAdapter(sql, DBConnection);
        DataTable result = new DataTable();
        return result.Copy();
    }
    
    public static List<Entity> GetEntityByType(String _type)
    {
        DataTable result = DBTrans.ExecQuery("EXEC sp_query_GetEntityByType '" + _type + "'");
        List<Entity> entities = new List<Entity>();

        foreach (DataRow row in result.Rows)
        {
            Entity _entity = new Entity();
            _entity.ID = Int32.Parse(row["id"].ToString());
            _entity.Type = row["type"].ToString();
            _entity.Content = row["content"].ToString();
            _entity.Created = DateTime.Parse(row["created"].ToString());
            entities.Add(_entity);
        }

        return entities;
    }

    public static string GetConfig(params string[] args)
    {

        string configFile = @"[path]\config.json";

        string allText = System.IO.File.ReadAllText(configFile);
        var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var jsonData = oSerializer.Deserialize<dynamic>(allText);

        for (Int16 i = 0; i <= args.Length - 1; i++)
            jsonData = jsonData[args[i]];
        return System.Net.WebUtility.HtmlDecode(jsonData);
    }

}


/*
DataTable result = new DataTable();
result.Columns.Add(new DataColumn("id", System.Type.GetType("System.Int32")));
result.Columns.Add(new DataColumn("created", System.Type.GetType("System.DateTime")));
result.Columns.Add(new DataColumn("type", System.Type.GetType("System.String")));
result.Columns.Add(new DataColumn("content", System.Type.GetType("System.String")));

DataRow r = result.NewRow();
r["id"] = 1;
r["created"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
r["type"] = "type1";
r["content"] = "content";
result.Rows.Add(r);
*/