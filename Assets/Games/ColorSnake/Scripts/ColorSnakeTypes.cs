using System;
using System.Linq;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

//Object colors
[Serializable]
public class ColorType
{
    public string Name;
    public int Id;
    public Color Color;
}

//Object types
[Serializable]
public class ObjectType
{
    public string Name;
    public int Id;
    public GameObject Object;
}

//Templates
[Serializable]
public class TemplateType
{
    public string Name;
    public int Id;
    public Transform[] points;
}

public class ColorSnakeTypes : MonoBehaviour
{
    [SerializeField] private ColorType[] m_Colors;
    [SerializeField] private ObjectType[] m_Objects;
    [SerializeField] private TemplateType[] m_Templates;

    public ColorType GetRandomColorType()
    {
        return m_Colors[GetRand(m_Colors)];
    }

    public ColorType GetColorTypeByName(string name)
    {
        ColorType colorType = new ColorType();
        foreach (var m_Colors in m_Colors)
        {
            if (m_Colors.Name.Equals("Red"))
            {
                colorType = m_Colors;
            }
            else if (m_Colors.Name.Equals("Blue"))
            {
                colorType = m_Colors;
            }
            else if (m_Colors.Name.Equals("Green"))
            {                colorType = m_Colors;
                colorType = m_Colors;
            }
            else if (m_Colors.Name.Equals("Green"))
            {
                colorType = m_Colors;
            }
        }

        return colorType;
    }

    public ObjectType GetRandomObjectType()
    {
        return m_Objects[GetRand(m_Objects)];
    }

    public ObjectType GetObjectTypeByName(string name)
    {
        ObjectType objectType = new ObjectType();
        foreach (var type in m_Objects)
        {
            if (type.Name.Equals(name))
            {
                objectType = type;
                return objectType;
            }
        }

        return objectType;
    }

    public TemplateType GetRandomTemplateType()
    {
        return m_Templates[GetRand(m_Templates)];
    }

    public TemplateType GetTemplateByName(string name)
    {
        TemplateType templateType = new TemplateType();
        foreach (var template in m_Templates)
        {
            if (template.Name.Equals(name))
            {
                templateType = template;
                return templateType;
            }
        }

        return templateType;
    }

    public ColorType GetColorType(int id)
    {
        return m_Colors.FirstOrDefault(c => c.Id == id);
    }

    public ObjectType GetObjectType(int id)
    {
        return m_Objects.FirstOrDefault(c => c.Id == id);
    }

    private int GetRand(Object[] objects)
    {
        return Random.Range(0, objects.Length);
    }
}