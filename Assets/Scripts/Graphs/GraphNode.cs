using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public interface IWeightedEdgeInterface
{
    public float weight { get; set; }

}

[System.Serializable]
public class GraphEdge
{
    [SerializeField]
    public GraphNode TargetGraphNode;

    public void Initialise(GraphNode TargetNode)
    {
        TargetGraphNode = TargetNode;
    }
}

[System.Serializable]
public class WeightedGraphEdge : GraphEdge, IWeightedEdgeInterface
{
    float IWeightedEdgeInterface.weight { get => Weight; set => Weight = value; }

    [SerializeField]
    public float Weight;
}

[System.Serializable]
public class UnDirectionalGraphEdge : GraphEdge
{
    public GraphNode SourceGraphNode;

    public void Initialise(GraphNode Source, GraphNode Target)
    {
        SourceGraphNode = Source;
        Initialise(Target);
    }
}

[System.Serializable]
public class WeightedUnDirectionalGraphEdge : GraphEdge, IWeightedEdgeInterface
{
    public GraphNode SourceGraphNode { get; private set; }
    [SerializeField]
    private float _weight;
    float IWeightedEdgeInterface.weight { get => _weight; set => _weight = value; }

    public void Initialise(GraphNode Source, GraphNode Target)
    {
        SourceGraphNode = Source;
        Initialise(Target);
    }
}

[System.Serializable]
public class GraphNode : MonoBehaviour
{
    public int id = -1;

    [SerializeField]
    public List<GraphEdge> edges = new List<GraphEdge>();
    [SerializeField]
    public List<WeightedGraphEdge> weighted_edges = new List<WeightedGraphEdge>();

    private void Start()
    {
        id = GraphManager.Instance.RegisterNode(this);
    }

    [ContextMenu("Add Directional Edge")]
    public void AddDirectionalEdge()
    {
        edges.Add(new GraphEdge());
    }

    [ContextMenu("Add UnDirectional Edge")]
    public void AddUnDirectionalEdge()
    {
        edges.Add(new UnDirectionalGraphEdge());
    }

    [ContextMenu("Add Weighted Directional Edge")]
    public void AddWeightedDirectionalEdge()
    {
        WeightedGraphEdge edge = new WeightedGraphEdge();
        edges.Add(edge);
        weighted_edges.Add(edge);
    }

    [ContextMenu("Add Weighted UnDirectional Edge")]
    public void AddWeightedUnDirectionalEdge()
    {
        edges.Add(new WeightedUnDirectionalGraphEdge());

    }
}

[CustomEditor(typeof(GraphNode))]
public class TestManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        GraphNode graphNode = (GraphNode)target;
        for (int i = 0; i < graphNode.edges.Count; i++)
        {
            GraphEdge test = graphNode.edges[i];
            EditorGUILayout.LabelField("Level", test.ToString());
            using (new EditorGUI.IndentLevelScope())
            {
                switch (test)
                {
                    case WeightedGraphEdge Weighted:
                        {
                            EditorGUILayout.IntField(Weighted.TargetGraphNode.id);

                        }
                        break;
                    default:
                        {
                        }
                        break;
                }
            };
            EditorGUILayout.Separator();
        }
    }
}
