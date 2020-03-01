using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Extensions
{
    /// <summary>Determines if the given gameObject is the same as this one.</summary>
    /// <param name="gameObject"></param>
    /// <param name="other">The gameObject to compare against.</param>
    /// <returns>True: If the gameObject is the same.</returns>
    public static bool Is (this GameObject gameObject, GameObject other)
    {
        return Equals (gameObject, other);
    }

    /// <summary>Determines if the given component is the same as this one.</summary>
    /// <param name="component"></param>
    /// <param name="other">The component to compare against.</param>
    /// <returns>True: If the component is the same.</returns>
    public static bool Is (this Component component, Component other)
    {
        return Equals (component, other);
    }
    
    /// <summary>Determines if the component has the tag specified.</summary>
    /// <param name="component"></param>
    /// <param name="tagToLookFor">The tag to look for on the component.</param>
    /// <returns>True: If the component has the specified tag.</returns>
    public static bool HasTag (this Component component, string tagToLookFor)
    {
        return component.CompareTag (tagToLookFor);
    }
    
    /// <summary>Determines if the component has the tag specified.</summary>
    /// <param name="component"></param>
    /// <param name="tagToLookFor">The tag to check for.</param>
    /// <returns>True: If the component has the specified tag.</returns>
    public static bool HasTag (this Component component, EditorTags tagToLookFor)
    {
        var tagComponent = component.GetComponent<TagComponent> ();

        if (tagComponent != null)
        {
            if (tagComponent.Tag == tagToLookFor)
                return true; 
        }
        
        return component.HasTag (component.tag);
    }

    /// <summary>Determines if the component has the tag specified.</summary>
    /// <param name="component"></param>
    /// <param name="tag">The tag of this component.</param>
    /// <param name="tagToLookFor">The tag to check for.</param>
    /// <returns>True: If the component has the specified tag.</returns>
    public static bool HasTag (this Component component, EditorTags tag, EditorTags tagToLookFor)
    {
        return tag == tagToLookFor;
    }

    /// <summary>Determines if the component has any of the tags provided.</summary>
    /// <param name="component"></param>
    /// <param name="tagsToLookFor">The tags to check against.</param>
    /// <returns>True: If the object has one or more of the given tags.</returns>
    public static bool HasTags (this Component component, string[] tagsToLookFor)
    {
        foreach (var currentTag in tagsToLookFor)
        {
            if (component.CompareTag (currentTag))
            {
                return true;
            }
        }

        return false;
    }
    
    /// <summary>Determines if the component has any of the tags provided.</summary>
    /// <param name="component"></param>
    /// <param name="tagsToLookFor">The tags to check against.</param>
    /// <returns>True: If the object has one or more of the given tags.</returns>
    public static bool HasTags (this Component component, EditorTags[] tagsToLookFor)
    {
        var taggedBehaviour = component.GetComponent<TagComponent> ();

        if (taggedBehaviour != null && tagsToLookFor != null)
        {
            foreach (var currentTag in tagsToLookFor)
            {
                if (taggedBehaviour.Tag == currentTag)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    
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

    public static Vector3 X (this Vector3 vector3, float x)
    {
        return new Vector3 (x, vector3.y, vector3.z);
    }
    
    public static Vector3 Y (this Vector3 vector3, float y)
    {
        return new Vector3 (vector3.x, y, vector3.z);
    }
    
    public static Vector3 Z (this Vector3 vector3, float z)
    {
        return new Vector3 (vector3.x, vector3.y, z);
    }
}
