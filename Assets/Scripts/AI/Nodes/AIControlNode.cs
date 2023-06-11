using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AIControlNode")]
public class AIControlNode : ScriptableObject, IAINode
{
    [field: SerializeField] public bool FailOnFirstFail { get; set; } = false;
    [field: SerializeField] public bool SucceedOnFirstSuccess { get; set; } = false;
    [field: SerializeField] public List<Object> NodesToRun { get; set; } = new List<Object>();

    private Dictionary<IAINode, AINodeResult> NodeResults { get; set; } = new Dictionary<IAINode, AINodeResult>();

    public void OnStart()
    {
    }

    public bool CanEnter()
    {
        return true;
    }

    public AINodeResult TryRun()
    {
        foreach (Object nodeObject in NodesToRun)
        {
            IAINode node = nodeObject as IAINode;
            if (NodeResults.ContainsKey(node))
            {
                switch (NodeResults[node])
                {
                    case AINodeResult.SUCCESS:
                    case AINodeResult.FAILURE:
                    {
                        // This node is done, run the next ones.
                        continue;
                    }
                    case AINodeResult.IN_PROGRESS:
                    {
                        // Run again.
                        break;
                    }
                }
            }
            else if (node.CanEnter())
            {
                node.OnStart();
            }

            if (node.CanEnter())
            {
                AINodeResult nodeResult = node.TryRun();
                switch (nodeResult)
                {
                    case AINodeResult.SUCCESS:
                    {
                        if (SucceedOnFirstSuccess)
                        {
                            NodeResults.Clear();
                            return AINodeResult.SUCCESS;
                        }
                        break;
                    }
                    case AINodeResult.FAILURE:
                    {
                        if (FailOnFirstFail)
                        {
                            NodeResults.Clear();
                            return AINodeResult.FAILURE;
                        }
                        break;
                    }
                    case AINodeResult.IN_PROGRESS:
                    {
                        NodeResults.Add(node, nodeResult);
                        return AINodeResult.IN_PROGRESS;
                    }
                    default:
                        break;
                }
            }
        }

        NodeResults.Clear();
        return AINodeResult.SUCCESS;
    }
}
