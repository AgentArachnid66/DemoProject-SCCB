using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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
    public List<GraphNode> Nodes = new List<GraphNode>();

    public GraphNode Init;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        _instance = this;
    }


    public int RegisterNode(GraphNode node) 
    {
        if(node.id == -1)
        {
            node.id = Nodes.Count;
           
            if (!Nodes.Contains(node))
            {
                Nodes.Add(node);
            }
        }
        return node.id;
    }


    private void OnDrawGizmos()
{

        Color colour = Gizmos.color;
        for(int i = 0; i < Nodes.Count; i++)
        {

            for (int j = 0; j < Nodes[i].GetEdges().Count; j++)
            {
                Gizmos.color = Nodes[i].GetEdges()[j].GetEdgeWeight() > 0 ? Color.green : Nodes[i].GetEdges()[j].GetEdgeWeight() < 0 ? Color.red : Color.yellow;
                Gizmos.DrawLine(Nodes[i].GetEdges()[j].GetSourceNode().transform.position, Nodes[i].GetEdges()[j].GetTargetNode().transform.position);
            }
        }

        Gizmos.color = colour;
    }

    public IEnumerator DepthFirstSearch(bool[] visited, GraphNode initial)
    {
        visited[initial.id] = true;
        
        Debug.Log($"Visiting {initial}");
        initial.VisitNodeDebug();
        List<GraphEdge> edges = initial.GetEdges();
        edges.Sort((a, b) => Mathf.Abs(a.GetEdgeWeight()).CompareTo(Mathf.Abs(b.GetEdgeWeight())));

        foreach (GraphEdge edge in edges)
        {
            Debug.Log($"Edge {edge} weight: {edge.GetEdgeWeight()}");
        }

        foreach(GraphEdge edge in edges) 
        { 
            Debug.Log($"Looking at  {edge.GetTargetNode()}");

            if (visited[edge.GetTargetNode().id])
            {
                Debug.LogWarning($"Already Visited {edge.GetTargetNode()}");
                continue;
            }
            yield return new WaitForSeconds(2f);
            StartCoroutine(DepthFirstSearch(visited, edge.GetTargetNode()));
        }
    }

    public IEnumerator BreadthFirstSearch()
    {
        bool[] visited = new bool[Nodes.Count];
        Queue<GraphNode> NodeQueue = new Queue<GraphNode>();
        EnqueueNodeNeighbours(Init, ref NodeQueue, visited);
        Init.VisitNodeDebug();
        visited[Init.id] = true;
        Debug.Log($"Fully Visited {Init.name}");
        // Keeps going until we have visited all nodes in the graph
        while(NodeQueue.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            GraphNode node = NodeQueue.Dequeue();
            Debug.Log($"Visiting {node.name}");
            if (visited[node.id])
            {
                Debug.LogWarning($"Already Visited {node.name}");
                continue;
            }
            EnqueueNodeNeighbours(node, ref NodeQueue, visited);
            node.VisitNodeDebug();
            visited[node.id] = true;
            Debug.Log($"Fully Visited {node.name}");
        }
    }

    private void EnqueueNodeNeighbours(GraphNode Node, ref Queue<GraphNode> Queue, in bool[] visited)
    {
        // Sorts the relevant edges in ascending order, so the smallest distance is first
        List<GraphEdge> edges = Node.GetEdges();
        edges.Sort((a, b) => Mathf.Abs(a.GetEdgeWeight()).CompareTo(Mathf.Abs(b.GetEdgeWeight())));
        // Iterates through the edges and queues them
        foreach (GraphEdge edge in edges)
        {
            // Checks we aren't visiting the same node more than once
            if (visited[edge.GetTargetNode().id])
            {
                Debug.Log($"Cannot queue {edge.GetTargetNode().name}, already visited");
                continue;
            }
            Debug.Log($"Queuing {edge.GetTargetNode()}");
            Queue.Enqueue(edge.GetTargetNode());
        }
    }
}
[CustomEditor(typeof(GraphManager))]
public class TestManagerInspector : Editor
{
    private SerializedProperty SerializedProperty;

    private void OnEnable()
    {
        SerializedProperty = serializedObject.FindProperty("Init");
    }

    public override void OnInspectorGUI()
    {
        GraphManager myTarget = (GraphManager)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(SerializedProperty);


        Button("Depth First Search", false, () =>
        {

            for (int i = 0; i < myTarget.Nodes.Count; i++)
            {
                if (myTarget.Nodes[i].id > -1)
                {
                    continue;
                }
                myTarget.Nodes[i].id = myTarget.RegisterNode(myTarget.Nodes[i]);
            }


            Debug.Log($"Starting Depth First Search");

            bool[] visited = new bool[myTarget.Nodes.Count];
            myTarget.StartCoroutine(myTarget.DepthFirstSearch(visited, myTarget.Init));
        });
        

        Button("Breadth First Search", false, () =>
        {

            for (int i = 0; i < myTarget.Nodes.Count; i++)
            {
                if (myTarget.Nodes[i].id > -1)
                {
                    continue;
                }
                myTarget.Nodes[i].id = myTarget.RegisterNode(myTarget.Nodes[i]);
            }


            Debug.Log($"Starting Depth First Search");

            myTarget.StartCoroutine(myTarget.BreadthFirstSearch());
        });

        serializedObject.ApplyModifiedProperties();
    }
    public static void Button(in string buttonName, in bool visibleInEditMode, in System.Action action)
    {
        if ((visibleInEditMode || Application.isPlaying) && GUILayout.Button(buttonName)) action.Invoke();
    }
}
