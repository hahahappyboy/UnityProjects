using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataManager {
   
   //单例
   private static SceneDataManager ins;
   //传输数据
   private Dictionary<string, object> sceneOneshotData = null;
   //管理星星的数量
   private Dictionary<int, int> starDic;

   public Dictionary<int, int> StarDic {
      get { return starDic; }
   }

   public SceneDataManager() {
      starDic = new Dictionary<int, int>();
   }
   
   
   public static SceneDataManager GetInstance() {
      if (ins == null) {
         ins = new SceneDataManager();
      }
      return ins;
   }

  

   //设置数据
   private void WriteSceneData(Dictionary<string, object> data) {
      sceneOneshotData = data;
   }

   //取出数据
   public Dictionary<string, object> ReadSceneData() {
      Dictionary<string, object> tempData = sceneOneshotData;
      sceneOneshotData = null;//清空
      return tempData;
   }

   //前往新场景
   public void ToNewScene(string sceneName, Dictionary<string, object> param = null) {
      //写入数据
      this.WriteSceneData(param);
      //加载新场景
      SceneManager.LoadScene(sceneName);
   }    

   
}
