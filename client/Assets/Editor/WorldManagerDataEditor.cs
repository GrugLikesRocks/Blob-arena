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

                    data.toriiUrl = "https://api.cartridge.gg/x/rr/torii";
                    data.rpcUrl = "https://api.cartridge.gg/x/rr/katana";

                    data.worldAddress = "0x739075d3eab7b1463ef8e99ad59afc470dbee7a4d5682fecde6c84c0798e1e7";
                    data.gameActionsAddress = "0x2bbd7c9d7822223e43460fe0c0c8089cab0f4e5c6b960ae7ec38a9ef8e560b2";
                    data.eventActionsAddress = "0x223e3880bb801613e3ad10efc0e3b54f83e9d9a64100c62148dad450178e272";
                    data.outpostActionsAddress = "0x1ce589283a7353cad2c51f9c7ef2407962ab356cfb32f909ae0dfba315f342b";
                    data.reinforcementActionsAddress = "0x7c68de0e3b99ffc19673104c582653b786d16eb18bd721716dcf2bfe5785d2f";
                    data.paymentActionsAddress = "0x7cfca1aa08b1ef734b532c8b16d4e267c41451b9a4b7ba9c5e557400324a102";
                    data.tradeOutpostActionsAddress = "0x4b595653ed5a8e53fafb6c45904eea086ce57eed8082970572a9b5163a4f9a5";
                    data.tradeReinforcementActionsAddress = "0x677359fd0f14a9d4af8cac6e29354a1a0380bde387241deabd27560bf475d95";

                    data.masterAddress = "0x3ebe00c0bce66b6d4bb20726812bff83fbb527226babcaf3d4dac46915cedb";
                    data.masterPrivateKey = "0x1d2ce4b504f4dcf9061d4db9e10e9d5d14f37b4ec595a648d6cd6e005ef937e";
                    break;

                case EnvironmentType.TESTNET:

                    data.toriiUrl = "https://api.cartridge.gg/x/rrsepolia/torii";
                    data.rpcUrl = "https://starknet-sepolia.public.blastapi.io/rpc/v0_6";

                    data.worldAddress = "0x6907ac71d377de72deae6cf331d2bbfabc644441916c3f8c307074cf1195d11";

                    data.gameActionsAddress = "0x1dcf969c721ca064d2bcaedb862c35a9f04c6c7c3b4472115435c1b6a64e2aa";
                    data.eventActionsAddress = "0x687bfa612be231aa826dace8ce13b54466d8407ab10180702b77a0e809ba2fe";
                    data.outpostActionsAddress = "0x7fb64ef6e1532621cc96468571085b6c366b5261e2c4478ea5b52b2053a7b8b";
                    data.reinforcementActionsAddress = "0x179d69c8d553b7ab5d5c65548049eb6a42b6a017f3f54b35a7e4b449d5c64b7";
                    data.paymentActionsAddress = "0x62a89f376123c130700677185c809caf01f4e9831a2cb1c093db37a4b01ef00";
                    data.tradeOutpostActionsAddress = "0x74aeee3fa44ea379cfa2cdee7f724bc86350fc9533186cb4c09a1d0518e8458";
                    data.tradeReinforcementActionsAddress = "0x4357f57f837d8955db2ada89929e62174f5957b3b8bc37c4dee5b1a2867a807";

                    data.masterAddress = "";
                    data.masterPrivateKey = "";

                    break;
            }

            EditorUtility.SetDirty(data); // Mark the object as dirty so Unity knows it has changed
        }
    }
}
