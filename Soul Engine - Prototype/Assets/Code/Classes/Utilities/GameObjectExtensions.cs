using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Extensions
{
    /// <summary>Finds the first instance of an object of the given type. Even if it's inactive.</summary>
    /// <typeparam name="T">The type of object instance to look for.</typeparam>
    /// <param name="gameObject">The class to extend.</param>
    /// <returns>The first given instance of the object type found. Null if none can be found.</returns>
    public static T FindFirstObjectOfType<T> (this GameObject gameObject) where T : class
    {
        var objects = new List<T> ();
        var scene = SceneManager.GetActiveScene ();
        var roots = scene.GetRootGameObjects ();

        foreach (GameObject root in roots)
        {
            objects.AddRange (root.GetComponentsInChildren<T> (true));
        }

        if (objects.Count <= 0)
            return null;
        
        return objects[0];
    }
    
    /// <summary>Determines if the gameobject has any of the tags provided.</summary>
    /// <param name="gameObject"></param>
    /// <param name="tags">The tags to check against.</param>
    /// <returns>True: If the object has one of the given tags.</returns>
    public static bool HasTag (this GameObject gameObject, string[] tags)
    {
        foreach (var currentTag in tags)
        {
            if (gameObject.CompareTag (currentTag))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>Finds all instances of the objects of the given type. Even if they are inactive.</summary>
    /// <typeparam name="T">The type of object instance to look for.</typeparam>
    /// <param name="gameObject">The class to extend.</param>
    /// <returns>An array of all the found instances of the given type.</returns>
    public static List<T> FindAllObjectsOfType<T> (this GameObject gameObject) where T : class
    {
        var objects = new List<T> ();
        var scene = SceneManager.GetActiveScene ();
        var roots = scene.GetRootGameObjects ();

        foreach (GameObject root in roots)
        {
            objects.AddRange (root.GetComponentsInChildren<T> (true));
        }

        return objects;
    }

    /// <summary>Finds all instances of the objects of the given type. Even if they are inactive.</summary>
    /// <typeparam name="T">The type of object instance to look for.</typeparam>
    /// <param name="gameObject">The class to extend.</param>
    /// <returns>An array of all the found instances of the given type.</returns>
    public static List<T> FindAllObjectsOfType<T> () where T : class
    {
        var objects = new List<T> ();
        var scene = SceneManager.GetActiveScene ();
        var roots = scene.GetRootGameObjects ();

        foreach (GameObject root in roots)
        {
            objects.AddRange (root.GetComponentsInChildren<T> (true));
        }

        return objects;
    }

    /// <summary>Changes the alpha component of this color.</summary>
    /// <param name="color">The class to extend.</param>
    /// <param name="alpha">The value of the alpha to change to.</param>
    public static Color Alpha (this Color color, float alpha)
    {
        return new Color (color.r, color.g, color.b, alpha);
    }

    /// <summary>Changes the height component of this rect.</summary>
    /// <param name="rect">The class to extend.</param>
    /// <param name="height">The value of the height to change to.</param>
    /// <returns></returns>
    public static Rect Height (this Rect rect, float height)
    {
        return new Rect (rect.x, rect.y, rect.width, height);
    }

    /// <summary>Changes the width component of this rect.</summary>
    /// <param name="rect">The class to extend.</param>
    /// <param name="width">The value of the width to change to.</param>
    /// <returns></returns>
    public static Rect Width (this Rect rect, float width)
    {
        return new Rect (rect.x, rect.y, width, rect.height);
    }
}
