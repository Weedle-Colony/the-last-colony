using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLastColony
{
    public enum NodeState
    {
        Empty,
        Occupied,
        EntityArrived,
        EntityMoved
    }

    public class WorldRules
    {
        public static NodeState GetNewState(NodeState currentState, string status)
        {
            switch (currentState)
            {

                case NodeState.Occupied:
                    if (status == "movedFrom")
                        return NodeState.EntityMoved;
                    break;
                case NodeState.Empty:
                    if (status == "movedTo")
                        return NodeState.EntityArrived;
                    break;
                case NodeState.EntityArrived:
                    if (status == "occupied")
                        return NodeState.Occupied;
                    break;
                case NodeState.EntityMoved:
                    if (status == "empty")
                        return NodeState.Empty;
                    break;
            }

            return currentState;
        }
    }
}
