using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ProtoBuf;
using System;

public class NetworkPacketController : NetworkBehaviour
{


    public enum MovementType
    {
        Idle,
        Walking,
        Running

    }

    public enum GunType
    {
        Rifle,
        Pistol,
        Kinfe,
        Grenade
    }
    public enum MovementState
    {
        Standing,
        Jumping,
        Reloading,
        crouching
    }

    [Serializable] // Binary, you may delete that
    [ProtoContract(SkipConstructor = true)] // Skip the constructor
    public class SendPackagetest
    {
        [ProtoMember(1)] // Mark your variables as serializable
        public int ID { get; set; }


    }

    [Serializable] // Binary, you may delete that
    [ProtoContract(SkipConstructor = true)] // Skip the constructor
    public class SendPackage
    {
        [ProtoMember(1)] // Mark your variables as serializable
        public int ID { get; set; }

        //[ProtoMember(2)]
        //public string Name { get; set; }

        [ProtoMember(3)]
        public MovementType movement_type { get; set; }

        [ProtoMember(4)]
        public GunType gun_type { get; set; }

        [ProtoMember(5)]
        public MovementState movement_state { get; set; }

        [ProtoMember(6)]
        public float Horizontal { get; set; }

        [ProtoMember(7)]
        public float Vertical { get; set; }

        [ProtoMember(2)]
        public float Time_stamp { get; set; }
    }
    [Serializable] // Binary, you may delete that
    [ProtoContract(SkipConstructor = true)] // Skip the constructor
    public class RecievePackage
    {
        [ProtoMember(1)] // Mark your variables as serializable
        public int ID { get; set; }

        [ProtoMember(2)]
        public float X { get; set; }

        [ProtoMember(3)]
        public float Y { get; set; }

        [ProtoMember(4)]
        public float Z { get; set; }

        [ProtoMember(5)]
        public float Time_stamp { get; set; }

        [ProtoMember(6)]
        public MovementType movement_type { get; set; }

        [ProtoMember(7)]
        public GunType gun_type { get; set; }

        [ProtoMember(8)]
        public MovementState movement_state { get; set; }

        [ProtoMember(9)]
        public float hit_points { get; set; }
    }


    [System.Serializable]
    public class SendPackageold
    {
        public float horizontal;
        public float vertical;
        public float timestamp;
    }

    [System.Serializable]
    public class RecievePackageold
    {
        public float X;
        public float Y;
        public float Z;
        public float timestamp;
    }




    // Use this for initialization
    void Start()
    {
        Serializer.PrepareSerializer<RecievePackage>();
        Serializer.PrepareSerializer<List<RecievePackage>>();
        Serializer.PrepareSerializer<SendPackage>();
        Serializer.PrepareSerializer<List<SendPackage>>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private NetworkPacketManager<SendPackage> m_PacketManager;
    public NetworkPacketManager<SendPackage> PacketManager
    {
        get
        {
            if(m_PacketManager == null)
            {
                m_PacketManager = new NetworkPacketManager<SendPackage>();
                if (isLocalPlayer)
                {
                    m_PacketManager.OnRequirePackageTransmit += TransmitPackageToServer;

                }
            }
            return m_PacketManager;
        }
    }
    //server packet Manager
    private NetworkPacketManager<RecievePackage> m_ServerPacketManager;
    public NetworkPacketManager<RecievePackage> ServerPacketManager
    {
        get
        {
            if (m_ServerPacketManager == null)
            {
                m_ServerPacketManager = new NetworkPacketManager<RecievePackage>();
                if (isServer)
                {
                    m_ServerPacketManager.OnRequirePackageTransmit += TransmitPackageToClient;

                }
            }
            return m_ServerPacketManager;
        }
    }


    private void TransmitPackageToServer(byte[] data)
    {
        //Debug.Log("TransmitPackageToServer");
        if(data == null|| data.Length ==0)
            Debug.Log("TransmitPackageToServer dataempty");
        CmdTransmitPackages(data);
    }
    [Command]
    void CmdTransmitPackages(byte[] data)
    {
        //Debug.Log("CmdTransmitPackages");
        if (data == null || data.Length == 0)
            Debug.Log("CmdTransmitPackages dataempty");
        PacketManager.RecieveData(data);
        ////Debug.Log("RecieveData(data)");
    }



    [ClientRpc]
    void RpcRecieveDataOnClient(byte[] data)
    {
        ////Debug.Log("RpcRecieveDataOnClient");
        //Debug.Log(data);

        ServerPacketManager.RecieveData(data);
    }


    private void TransmitPackageToClient(byte[] data)
    {
        RpcRecieveDataOnClient(data);

    }

    public virtual void FixedUpdate()
    {
       // Debug.Log("virtual void FixedUpdate");
        PacketManager.Tick();
        ServerPacketManager.Tick();
    }


}