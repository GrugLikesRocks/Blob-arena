using System.Collections.Generic;
using UnityEngine;

namespace Dojo
{
    public enum EnvironmentType
    {
        LOCAL,
        TORII,
        TESTNET
    }

    public enum DataString
    {
        TORII_URL,
        RPC_URL,
        WORLD_ADDRESS,
        GAME_ACTIONS_ADDRESS,
        EVENT_ACTIONS_ADDRESS,
        OUTPOST_ACTIONS_ADDRESS,
        REINFORCEMENT_ACTIONS_ADDRESS,
        PAYMENT_ACTIONS_ADDRESS,
        TRADE_OUTPOST_ACTIONS_ADDRESS,
        TRADE_REINFORCEMENT_ACTIONS_ADDRESS,
        MASTER_ADDRESS,
        MASTER_PRIVATE_KEY
    }

    [CreateAssetMenu(fileName = "WorldManagerData", menuName = "ScriptableObjects/WorldManagerData", order = 2)]
    public class WorldManagerData : ScriptableObject
    {
        [Header("Environment")]
        public EnvironmentType environmentType;

        [Space(30)]
        [Header("Enpoint URLs")]
        public string toriiUrl = "http://localhost:8080/graphql";
        public string rpcUrl = "http://localhost:5050";

        public string relayUrl = "/ip4/127.0.0.1/tcp/9090";
        public string relayWebrtcUrl = "/ip4/127.0.0.1/udp/9091/webrtc-direct/certhash/uEiDx-luHsovHTHPuU5qzNHCCFkYYpFPirTn2p7ZIFP0Dig";

        [Space(60)]
        [Header("Game Contracts Addresses")]
        public string worldAddress = "";

        public string gameActionsAddress = "";
        public string eventActionsAddress = "";
        public string outpostActionsAddress = "";
        public string reinforcementActionsAddress = "";
        public string paymentActionsAddress = "";
        public string tradeOutpostActionsAddress = "";
        public string tradeReinforcementActionsAddress = "";

        [Space(30)]
        [Header("Account Addresses")]
        public string masterAddress = "";
        public string masterPrivateKey = "";

        public uint limit = 100;
    }
}
