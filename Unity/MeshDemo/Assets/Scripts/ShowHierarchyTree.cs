using UnityEngine;
using System.Collections;
using System;

public class ShowHierarchyTree {
    // Use this for initialization
    public static string GetHierarchy()
    {
        string tree = "";
        // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            // シーン上に存在するオブジェクト、rootオブジェクト
            if (obj.activeInHierarchy && (obj == obj.transform.root.gameObject))
            {
                // GameObjectの名前を表示.
                tree += "+" + obj.name + "\n";
                tree += FindChild(obj, "++");
            }
        }

        return tree;
    }

    private static string FindChild(GameObject obj, string indent)
    {
        string tree = "";
        foreach (Transform child in obj.transform)
        {
            tree += indent + child.name + "\n";
            tree += FindChild(child.gameObject, indent + "+");
        }

        return tree;
    }
}
