using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheLastColony;

namespace WorldState
{
    [TestClass]
    public class WorldRulesTest
    {
        [TestMethod]
        public void EmptyNode_EntityArrived_Occupied()
        {
            NodeState state = NodeState.Empty;

            NodeState testResult = WorldRules.GetNewState(state, "occupied");

            Assert.AreEqual(NodeState.Occupied, testResult);
        }

        [TestMethod]
        public void EmptyNode_NoArrival_Empty()
        {
            NodeState state = NodeState.Empty;

            NodeState testResult = WorldRules.GetNewState(state, "empty");

            Assert.AreEqual(NodeState.Empty, testResult);
        }

        [TestMethod]
        public void EmptyNode_NoStatus_Occupied()
        {
            NodeState state = NodeState.Empty;

            NodeState testResult = WorldRules.GetNewState(state, "");

            Assert.AreEqual(NodeState.Empty, testResult);
        }

        [TestMethod]
        public void OccupiedNode_EntityMoved_Empty()
        {
            NodeState state = NodeState.Occupied;

            NodeState testResult = WorldRules.GetNewState(state, "empty");

            Assert.AreEqual(NodeState.Empty, testResult);
        }

        [TestMethod]
        public void OccupiedNode_NoMove_Occupied()
        {
            NodeState state = NodeState.Occupied;

            NodeState testResult = WorldRules.GetNewState(state, "occupied");

            Assert.AreEqual(NodeState.Occupied, testResult);
        }

        [TestMethod]
        public void OccupiedNode_NoStatus_Occupied()
        {
            NodeState state = NodeState.Occupied;

            NodeState testResult = WorldRules.GetNewState(state, "");

            Assert.AreEqual(NodeState.Occupied, testResult);
        }
    }
}