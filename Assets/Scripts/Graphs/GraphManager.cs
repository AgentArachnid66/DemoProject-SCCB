using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance
    {
        get { return _instance; }
        set
        {
            if(_instance == null)
            {
                _instance = value;
                return;
            }
            
        }
    }

    private static GraphManager _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        _instance = this;

        for(int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i].id > -1)
            {
                continue;
            }
            Nodes[i].id = RegisterNode(Nodes[i]);
        }
    }

    public List<GraphNode> Nodes;

    public int RegisterNode(GraphNode node) 
    {
        if(node.id == -1)
        {
            node.id = Nodes.Count - 1;
            if (!Nodes.Contains(node))
            {
                Nodes.Insert(node.id, node);
            }
        }
        return node.id;
    }

}
