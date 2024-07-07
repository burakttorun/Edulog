using UnityEditor;
using UnityEngine;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;
namespace ThePrototype.MyTools
{
   public static class Setup
   {
      [MenuItem("Tools/Setup/Create Default Folders")]
      public static void CreateDefaultFolders()
      {
         Folders.CreateDefault("ThePrototype",
            "Animation",
            "Art",
            "Materials",
            "Prefabs",
            "Scripts/ScriptableObjects",
            "Scripts/UI");
         Refresh();
      }
   }

   static class Folders
   {
      public static void CreateDefault(string root, params string[] folders)
      {
         var fullpath = Combine(Application.dataPath, root);
         foreach (string folder in folders)
         {
            var path = Combine(fullpath, folder);
            if (!Exists(path))
            {
               CreateDirectory(path);
            }
         }
      }
   }
}