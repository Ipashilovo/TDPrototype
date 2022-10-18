using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Refactoring.Editor
{
    public enum IdType
    {
        String,
        Long,
        Double
    }

    public class IdClassGenerator : ScriptableWizard
    {
        private string _path;

        [SerializeField]
        private IdType _type;

        [SerializeField]
        private string _name;
        
        [MenuItem("Assets/Create/Id")]
        public static void CreateId(MenuCommand cmd)
        {
            string path = "Assets/";
            if (Selection.activeObject != null)
            {
                path = AssetDatabase.GetAssetPath(Selection.activeObject);
                if (!Directory.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                }
            }

            var wiz = CreateInstance<IdClassGenerator>();
            wiz.Init(path);
            wiz.Show();
        }

        private void Init(string path)
        {
            _type = IdType.String;
            _name = "";
            _path = path;
        }

        private void OnWizardCreate()
        {
            if (_name == null)
            {
                throw new Exception("You should specify the name");
            }
            var type = _type.ToString().ToLower();
            var template = File.ReadAllText("Assets/Editor/CodeGenTools/Id/IdScriptTemplate.cstemplate");
            template = template.Replace("$Name$", _name);
            template = template.Replace("$Type$", type);
            File.WriteAllText(Path.Combine(_path, $"{_name}.cs"), template);
            AssetDatabase.Refresh();
        }
    }
}