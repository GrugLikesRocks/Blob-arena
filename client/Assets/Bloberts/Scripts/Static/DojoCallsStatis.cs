using Dojo.Starknet;
using dojo_bindings;
using System;
using System.Threading.Tasks;
using UnityEngine;

public static class DojoCallsStatis
{
    //public static readonly string knockoutAddress = "0x66821c7071e66a727918207afb0255e5dac1677b1732a95104c7672fc98d51e";
    //public static readonly string blobertActionsAddress = "0x3e2408ea9affd43d731fcb31e67f7c2c6899cef4c1279dc597dc5d14b240d12";

    //public static readonly string worldAddress = "0x3a0394af68d1727a0019949a94fa584e8bc132903d49bb545888eb1bd427cf4";






    //Local
    public static readonly string knockoutAddress = "0x66821c7071e66a727918207afb0255e5dac1677b1732a95104c7672fc98d51e";
    public static readonly string blobertActionsAddress = "0x3e2408ea9affd43d731fcb31e67f7c2c6899cef4c1279dc597dc5d14b240d12";

    public static readonly string worldAddress = "0x3a0394af68d1727a0019949a94fa584e8bc132903d49bb545888eb1bd427cf4";

    public static readonly string masterAddress = "0xb3ff441a68610b30fd5e2abbf3a1548eb6ba6f3559f2862bf2dc757e5828ca";
    public static readonly string masterPrivateKey = "0x2bbf4f9fd0bbb2e60b0316c1fe0b76cf7a4d0198bd493ced9b8df2a3a24d68a";



    #region structs structure for calls

    public struct EndpointDojoCallStruct
    {
        public Account account;
        public string functionName;
        public string addressOfSystem;
    }

    public struct MintBlobertStruct
    {
        public FieldElement owner;
    }

    public struct CreateNewGameStruct
    {
        public FieldElement player_a;
        public FieldElement player_b;

        public FieldElement blobert_aID;
        public FieldElement blobert_bID;
    }

    public struct CommitMoveStruct
    {
        public FieldElement combat_id;
        public UInt64 hash;
    }

    public struct RevealMoveStruct
    {
        public FieldElement combat_id;
        public BlobertUitls.Move move;
        public UInt64 salt;
    }

    public struct VerifyMoveStruct
    {
        public FieldElement combat_id;
    }

    #endregion

    #region dojo calls

    public static async Task<FieldElement> MintBlobert(MintBlobertStruct dataStruct, EndpointDojoCallStruct endpointData)
    {
        try
        {
            var transaction = await endpointData.account.ExecuteRaw(new dojo.Call[]
            {
                new dojo.Call
                {
                    calldata = new dojo.FieldElement[]
                    {
                       dataStruct.owner.Inner()
                    },
                    selector = endpointData.functionName,
                    to = endpointData.addressOfSystem,
                }
            });

            return transaction;
        }
        catch (Exception ex)
        {
            Debug.Log("issue with mint" + ex.Message);
            return null;
        }
    }

    public static async Task<FieldElement> CreateNewGame(CreateNewGameStruct dataStruct, EndpointDojoCallStruct endpointData)
    {
        try
        {
            var transaction = await endpointData.account.ExecuteRaw(new dojo.Call[]
            {
                new dojo.Call
                {
                    calldata = new dojo.FieldElement[]
                    {
                       dataStruct.player_a.Inner(),
                       dataStruct.player_b.Inner(),
                       dataStruct.blobert_aID.Inner(),
                       dataStruct.blobert_bID.Inner(),
                    },
                    selector = endpointData.functionName,
                    to = endpointData.addressOfSystem,
                }
            });

            return transaction;
        }
        catch (Exception ex)
        {
            Debug.Log("issue with CreateNEwGame" + ex.Message);
            return null;
        }
    }




    public static async Task<FieldElement> CreateCommitTransaction(CommitMoveStruct dataStruct, EndpointDojoCallStruct endpointData)
    {


        try
        {
            var transaction = await endpointData.account.ExecuteRaw(new dojo.Call[]
            {
                new dojo.Call
                {
                    calldata = new dojo.FieldElement[]
                    {
                       dataStruct.combat_id.Inner(),
                       new FieldElement(dataStruct.hash.ToString("X")).Inner(),
                    },
                    selector = endpointData.functionName,
                    to = endpointData.addressOfSystem,
                }
            });

            return transaction;
        }
        catch (Exception ex)
        {
            Debug.Log("issue with CreateCommitTransaction " + ex.Message);
            return null;
        }
    }

    public static async Task<FieldElement> CreateRevealTransaction(RevealMoveStruct dataStruct, EndpointDojoCallStruct endpointData)
    {
        try
        {
            var transaction = await endpointData.account.ExecuteRaw(new dojo.Call[]
            {
                new dojo.Call
                {
                    calldata = new dojo.FieldElement[]
                    {
                       dataStruct.combat_id.Inner(),
                       new FieldElement(dataStruct.move).Inner(),
                       new FieldElement(dataStruct.salt.ToString("X")).Inner(),
                    },
                    selector = endpointData.functionName,
                    to = endpointData.addressOfSystem,
                }
            });

            return transaction;
        }
        catch (Exception ex)
        {
            Debug.Log("issue with CreateRevealTransaction" + ex.Message);
            return null;
        }
    }


    #endregion

}