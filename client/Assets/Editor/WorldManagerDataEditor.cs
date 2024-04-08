using UnityEngine;
using UnityEditor;
using Dojo; // Ensure this is included to access your namespace

[CustomEditor(typeof(WorldManagerData))]
public class WorldManagerDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Draw the default inspector

        WorldManagerData data = (WorldManagerData)target;

      
        if (GUI.changed) // Check if any inspector value has changed
        {
            switch (data.environmentType) // Check the environment type
            {
                case EnvironmentType.LOCAL:

                    data.toriiUrl = "http://localhost:8080";
                    data.rpcUrl = "http://localhost:5050";

                    data.worldAddress = "0x3a0394af68d1727a0019949a94fa584e8bc132903d49bb545888eb1bd427cf4";
                  
                    data.masterAddress = "0xb3ff441a68610b30fd5e2abbf3a1548eb6ba6f3559f2862bf2dc757e5828ca";
                    data.masterPrivateKey = "0x2bbf4f9fd0bbb2e60b0316c1fe0b76cf7a4d0198bd493ced9b8df2a3a24d68a";
                    break;

                case EnvironmentType.TORII:

                    data.toriiUrl = "https://api.cartridge.gg/x/blob/torii";
                    data.rpcUrl = "https://api.cartridge.gg/x/blob/katana";

                    data.worldAddress = "0x182ca5288304dbade256febd4a46251173784acf67ed0fca541afaec08c42db";
                    
                    data.masterAddress = "0xe249d4d781232ebdebfdc4c548238fd302ede7ff875488f7e10d47b2c30bf5";
                    data.masterPrivateKey = "0x24780c5802ff4606624d7e51d65e04aad5d812c15d4f7bb274c96d8d64fb2d7";
                    break;

                case EnvironmentType.TESTNET:

                    data.toriiUrl = "https://api.cartridge.gg/x/rrsepolia/torii";
                    data.rpcUrl = "https://starknet-sepolia.public.blastapi.io/rpc/v0_6";

                   
                    data.masterAddress = "";
                    data.masterPrivateKey = "";

                    break;
            }

            EditorUtility.SetDirty(data); // Mark the object as dirty so Unity knows it has changed
        }
    }
}
