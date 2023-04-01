
using System.IO;
using LitJson;

public static class MVC_Model {
    public static JsonData ReadJsonData() {
        JsonData d;
        if (!File.Exists(Config.SAVEJSON_PATH)) {//Json文件不存在
            d = new JsonData();
            d["GoldCount"] = 0;
            File.WriteAllText(Config.SAVEJSON_PATH,d.ToJson());
        } else {//Json文件存在
            string json = File.ReadAllText(Config.SAVEJSON_PATH);
            d = JsonMapper.ToObject(json);
        }
        return d;
    }

    public static JsonData UpdateJsonData() {
        JsonData d = ReadJsonData();
        d["GoldCount"] = (int)d["GoldCount"] + 1;
        WriteJsonDate(d);
        return d;
    }

    public static JsonData WriteJsonDate(JsonData d) {
        File.WriteAllText(Config.SAVEJSON_PATH,d.ToJson());
        return d;
    }
}
