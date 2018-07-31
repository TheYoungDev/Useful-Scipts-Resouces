using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;

public class NetworkPacketManager<T> where T : class {



    public event System.Action<byte[]> OnRequirePackageTransmit;

    //public List<T> NetPackages;
    public T RecievedPackage;
    public T SendPackage;

    private float m_SendSpeed = 0.2f;
    public float SendSpeed
    {
        get
        {
            if (m_SendSpeed < 0.1f)
                return m_SendSpeed = 0.1f;
            return m_SendSpeed;
        }
        set
        {
            m_SendSpeed = value;
        }
    }
    float nextTick;

    private List<T> m_Packages;
    public List<T> Packages
    {
        get
        {
            if (m_Packages == null)
                m_Packages = new List<T>();
            return m_Packages;
        }
    }
    private List<T> m_NetPackages;
    public List<T> NetPackages
    {
        get
        {
            if (m_NetPackages == null)
                m_NetPackages = new List<T>();
            return m_NetPackages;
        }
    }
    public Queue<T> recievedPackages;

    //not needed?
    public void AddPackage(T package)
    {
        if(package != null)
        {
            //Debug.Log("Add Package"+ NetPackages.Count);

        }
        NetPackages.Add(package);
       // return NetPackages;
    }

    //READ DATA
    //When data is recieved deserialize the data and put it into a Queue
    public void RecieveData(byte[] bytes)
    {
        Debug.Log("isRecieveData ");
        if (recievedPackages == null)
        {
            recievedPackages = new Queue<T>();
            Debug.Log("isRecieveData empty");
        }
            

        
        //T[] Packages = Deserialize(bytes).ToArray();
        
        T RecievedPackage = Deserialize(bytes);
        recievedPackages.Enqueue(RecievedPackage);
        /*T[] _NetPackages = NetPackages.ToArray();
        for (int i = 0; i < _NetPackages.Length; i++)
            recievedPackages.Enqueue(_NetPackages[i]);*/

        //AddPackage(RecievedPackage);//*
        //Packages.Add(RecievedPackage);
        //recievedPackages.Enqueue(RecievedPackage);

    }



    //SEND DATA
    public void Tick()
    {
        //Debug.Log("tick1");
        nextTick += 1 / this.SendSpeed * Time.fixedDeltaTime;
        if (nextTick > 1 && NetPackages.Count >0) //RecievedPackage
        {
           // Debug.Log("tick1RecievedPackage");
            nextTick = 0;
            if (OnRequirePackageTransmit != null)
            {
                /* 

                //byte[] SendData = Serialize(this.NetPackages);
                byte[] SendData = Serialize(this.NetPackages);
                Debug.Log("tick1Serialize");
                NetPackages.Clear();
                //SendPackage = null;
                OnRequirePackageTransmit(SendData);
                */
               // /*
                 byte[] SendData;
                 for (int i = 0;i< this.NetPackages.Count; i++)
                 {
                    SendData = Serialize(this.NetPackages[i]);
                    //Debug.Log("tick1Serialize");
                    NetPackages.Clear();
                    //SendPackage = null;
                    OnRequirePackageTransmit(SendData);
                }
                 // */
            }
        }
    }
    public T GetNextDataRecieved()
    {
        //Debug.Log("GetNextDataRecieved");
        if(recievedPackages == null|| recievedPackages.Count == 0)
        {
            Debug.Log("return default(T)");
            return default(T);
        }
        Debug.Log("recievedDequeue()");
        return recievedPackages.Dequeue();
    }


    public static byte[] Serialize(T obj) //List <T> 
    {
        //Debug.Log("Serialize");
        //Serializer Formatter = new Serializer();
        using (MemoryStream stream = new MemoryStream())
        {
            //stream.SetLength(file.Position);
           // stream.SetLength();
           // stream.Capacity = obj.Length;
            Serializer.SerializeWithLengthPrefix(stream, obj, PrefixStyle.Base128);
            //Serializer.Serialize(stream, obj);
            return stream.ToArray();
        }
    }

    //Deserialize recieved messages
    public static T Deserialize(byte[] data)
    {
        //Debug.Log("tick1Deserialize");
        using (MemoryStream stream = new MemoryStream(data))
        {
            stream.SetLength(data.Length);
            stream.Capacity = data.Length;

            stream.Write(data, 0, data.Length);
            stream.Seek(0, SeekOrigin.Begin);
            //return (T)Serializer.Deserialize
            return (T)Serializer.DeserializeWithLengthPrefix<T>(stream, PrefixStyle.Base128);

            //return (T)Serializer.Deserialize<T>(stream);
            // return Serializer.Deserialize<T>(stream);
            //retunr formatter.desrialize(stream)
            //return (T)Serializer.Deserialize<T>(stream);
        }
    }



    /*public T ReadBytes(T obj)
{
    using (MemoryStream stream = new MemoryStream())
    {
        Serializer.Serialize(stream, obj);
        byte[] data = stream.ToArray();
        stream.Write(data, 0, data.Length);
        stream.Seek(0, SeekOrigin.Begin);
        return Deserialize(data);
    }
}*/

    /*List<T> ReadBytes<T>(byte[] bytes)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using(MemoryStream ms)

    }//*/

}
