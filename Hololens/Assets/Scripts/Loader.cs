// loads the raw binary data into a texture saved as a Unity asset 
// (so can be de-activated after a given data cube has been converted)
// adapted from a XNA project by Kyle Hayward 
// http://graphicsrunner.blogspot.ca/2009/01/volume-rendering-101.html
// Gilles Ferrand, University of Manitoba 2016

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO; // to get BinaryReader
using System.Linq; // to get array's Min/Max
using System.Collections.Generic;
using System;

#if !UNITY_EDITOR
using Windows.Networking;
using Windows.Networking.Sockets;
#endif


public class Loader : MonoBehaviour {

    public int[] size;
	public bool mipmap;
    public int noSlices = 0;
    
    void Start() {
        #if !UNITY_EDITOR
        //socketShit();
        #endif

        size = new int[3] { 256, 256, 256 };
        // load the raw data
        Color[] colors = LoadRAWFile();
		// create the texture
		Texture3D texture = new Texture3D (size[0], size[1], size[2], TextureFormat.Alpha8, mipmap);
		texture.SetPixels (colors);
		texture.Apply ();
		// assign it to the material of the parent object
		GetComponent<Renderer>().material.SetTexture ("_Data", texture);
		// save it as an asset for re-use

	}

	private Color[] LoadRAWFile()
	{/*
        Color[] colors;

		Debug.Log ("Opening file "+path+filename+extension);
		FileStream file = new FileStream(path+filename+extension, FileMode.Open);
		Debug.Log ("File length = "+file.Length+" bytes, Data size = "+size[0]*size[1]*size[2]+" points -> "+file.Length/(size[0]*size[1]*size[2])+" byte(s) per point");

		BinaryReader reader = new BinaryReader(file);
		byte[] buffer = new byte[size[0] * size[1] * size[2]]; // assumes 8-bit data
		reader.Read(buffer, 0, sizeof(byte) * buffer.Length);
        reader.BaseStream.Dispose();

		colors = new Color[buffer.Length];
		Color color = Color.black;
		for (int i = 0; i < buffer.Length; i++)
		{
			color.a = (float)buffer[i] / byte.MaxValue; //scale the scalar values to [0, 1]
			colors [i] = color;
		}

		return colors;*/
        
        TextAsset csv = Resources.Load("pixelData") as TextAsset;
        string[] values = csv.text.Split(',');
        size[0] = Convert.ToInt32(values[0]);
        size[1] = Convert.ToInt32(values[1]);
        size[2] = Convert.ToInt32(values[2]);
        int max = Convert.ToInt32(values[3]);
        Color[] colours = new Color[values.Length - 4];
        int index = 0;
        for (int i = 4; i < values.Length - 1; i++)
        {
            colours[index] = Color.black;
            colours[index++].a = (float)(Convert.ToInt32(values[i])) / max;
        }

        return colours;
    }

#if !UNITY_EDITOR
    async public void socketShit()
    {

         HostName hostName;
        StreamSocket socket = null;
            using (socket = new StreamSocket())
            {
                hostName = new HostName("127.0.0.1");

                // Set NoDelay to false so that the Nagle algorithm is not disabled
                socket.Control.NoDelay = false;

                try
                {
                    // Connect to the server
                    await socket.ConnectAsync(hostName, "2500");
                }
                catch (Exception exception)
                {
                    switch (SocketError.GetStatus(exception.HResult))
                    {
                        case SocketErrorStatus.HostNotFound:
                            // Handle HostNotFound Error
                            throw;
                        default:
                            // If this is an unknown status it means that the error is fatal and retry will likely fail.
                            throw;
                    }
                }
            }

    }
#endif
}
