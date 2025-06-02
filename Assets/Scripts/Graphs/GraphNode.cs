using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



[System.Serializable]
public class GraphEdge
{
    [SerializeField]
    protected GraphNode SourceGraphNode;

    [SerializeField]
    protected GraphNode TargetGraphNode = null;


    [SerializeField]
    protected int weight = -1;


    public void Initialise(GraphNode SourceNode, GraphNode TargetNode, int Weight)
    {
        SourceGraphNode = SourceNode;
        TargetGraphNode = TargetNode;
        weight = Weight;

        if (Weight > -1 && SourceNode != null && TargetGraphNode != null && !TargetGraphNode.IsConnectedTo(SourceNode))
        {
            TargetGraphNode.AddEdge(TargetNode, SourceNode, Weight);
        }
    }

    public int GetEdgeWeight()
    {
        return weight;
    }

    public GraphNode GetSourceNode()
    {
        return SourceGraphNode;
    }

    public GraphNode GetTargetNode()
    {
        return TargetGraphNode;
    }
}

[System.Serializable]
public class GraphNode : MonoBehaviour
{
    public int id = -1;

    [SerializeField]
    protected List<GraphEdge> edges = new List<GraphEdge>();


    private void Start()
    {
        id = GraphManager.Instance.RegisterNode(this);
        Debug.Log($"Registered Node as {id}");

        for (int i = 0; i < edges.Count; i++)
        {
            edges[i].Initialise(edges[i].GetSourceNode(), edges[i].GetTargetNode(), edges[i].GetEdgeWeight());
        }
    }

    public bool IsConnectedTo(GraphNode Other)
    {
        for(int i = 0; i < edges.Count; i++)
        {
            if(edges[i].GetTargetNode() == Other)
            {
                return true;
            }
        }
        return false;
    }
    

    [ContextMenu("Add Edge")]
    public void EditorAddEdge()
    {
        AddEdge(this, null, 0);
    }

    public void VisitNodeDebug()
    {
        LTDescr descr = LeanTween.scale(gameObject, new Vector3(1.25f, 1.25f), 1f);
    }

    public void AddEdge(GraphNode Source, GraphNode Target, int weight)
    {
        GraphEdge edge = new GraphEdge();
        edge.Initialise(Source, Target, weight);
        edges.Add(edge);
    }

    public List<GraphEdge> GetEdges()
    { 
        return edges;
    }
}
