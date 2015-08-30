using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


public class Comunicator : IDisposable
{
    Stream newStream;
    protected BinaryReader BinaryReader { get; private set; }
    protected BinaryWriter BinaryWriter { get; private set; }

    public Comunicator(Stream IOStream)
    {
        newStream = IOStream;
        if (newStream != null)
        {
            BinaryWriter = new BinaryWriter(newStream);
            BinaryReader = new BinaryReader(newStream);
        }
    }

    public bool IsStreamDescribed()
    {
        if (newStream == null)
        {
            return false;
        }
        else return true;
    }

    public string ReadText()
    {
        try
        {
            return BinaryReader.ReadString();
        }
        catch (IOException exception)
        {
            MessageBox.Show("Cannot write into stream: (Extendend info) \n" + exception.Message);
            return "!! NO CONNECTION !!";
        }
        catch (NullReferenceException )
        {
            Console.WriteLine("Not Connected yet.");
            return "!! NO CONNECTION !!";
        }
    }

    public void SendText(string text)
    {
        try
        {
            BinaryWriter.Write(text);
        }
        catch (IOException exception)
        {
            MessageBox.Show("No connection, cannot write into stream: (Extended)\n" + exception.Message);
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Not Connected yet.");
        }
    }

    public void Dispose()
    {
        newStream.Close();
        BinaryReader.Close();
        BinaryWriter.Close();
    }
}
